using System;
using UnityEditor;
using UnityEditor.EditorTools;
using UnityEngine;

namespace JRahmatiNL.Unity3D.CustomEditorTools.Input
{
    // Unity3D treats Macbook touchpad as a zooming device, completely ignoring horizontal input.
    // Use this tool, if you prefer the Blender implementation of Macbook touchpad support!
    [EditorTool("MacBook Touchpad")]
    internal class MacbookTouchpadProcessor : EditorTool
    {
        private const float RotationSpeed = 0.1f;
        private static readonly Vector2 DeadSwipeZone = new Vector2(1f, 1f);
        private float _cameraSizeBeforeSwipeEvent = 1;
        private Vector2? _mousePositionBeforeSwipeEvent = null;

        public override void OnToolGUI(EditorWindow window)
        {
            var currentEvent = Event.current;
            _mousePositionBeforeSwipeEvent = _mousePositionBeforeSwipeEvent ?? currentEvent.mousePosition;
            if (!IsSwipeEvent(currentEvent))
            {
                base.OnToolGUI(window);
                _cameraSizeBeforeSwipeEvent = SceneView.lastActiveSceneView.size;
                _mousePositionBeforeSwipeEvent = currentEvent.mousePosition;
            }
            else
            {
                SceneView.lastActiveSceneView.size = _cameraSizeBeforeSwipeEvent;
                ProcessSwipeEvent(currentEvent);
            }
        }

        private bool IsSwipeEvent(Event currentEvent)
        {
            if (currentEvent.command) return false;
            if (currentEvent.control) return false;
            if (currentEvent.isMouse) return false;
            
            return currentEvent.mousePosition == _mousePositionBeforeSwipeEvent;
        }

        private void ProcessSwipeEvent(Event swipeEvent)
        {
            if(IsSwipeEventInDeadZone(swipeEvent)) return;
            
            if (swipeEvent.shift)
            {
                SceneView.lastActiveSceneView.pivot = GetNewPivotPointByEventDelta(swipeEvent.delta);
            }
            else
            {
                var newRotation = SceneView.lastActiveSceneView.rotation.eulerAngles;
                newRotation.x += swipeEvent.delta.y;
                newRotation.y += swipeEvent.delta.x;

                SceneView.lastActiveSceneView.rotation = Quaternion.Euler(newRotation);
                SceneView.lastActiveSceneView.Repaint();
            }
        }

        private bool IsSwipeEventInDeadZone(Event swipeEvent)
        {
            if(Math.Abs(swipeEvent.delta.x) >= DeadSwipeZone.x) return false;
            if(Math.Abs(swipeEvent.delta.y) >= DeadSwipeZone.y) return false;

            return true;
        }

        private static Vector3 GetNewPivotPointByEventDelta(Vector2 eventDelta)
        {
            var sceneCameraTransform = SceneView.lastActiveSceneView.camera.transform;
            var cameraRight = sceneCameraTransform.TransformDirection(Vector3.right) * RotationSpeed;
            var cameraDown = sceneCameraTransform.TransformDirection(Vector3.down) * RotationSpeed;
            var newPivotPoint = SceneView.lastActiveSceneView.pivot;
            var newCameraOffset = default(Vector3);
            newCameraOffset += cameraRight * eventDelta.x;
            newCameraOffset += cameraDown * eventDelta.y;
            newPivotPoint += newCameraOffset;
            return newPivotPoint;
        }

        private void OnDrawGizmos()
        {
#if UNITY_EDITOR
            // Ensure continuous Update calls.
            if (!Application.isPlaying)
            {
                UnityEditor.EditorApplication.QueuePlayerLoopUpdate();
                UnityEditor.SceneView.RepaintAll();
            }
#endif
        }
    }
}