# Unity3D.CustomEditorTools

This repository contains a set of editor tools to simplify certain aspects of the Unity Editor.
To use this repository you have to be familiar with some basic git & programming concepts.

## Installation

You can use [git submodules](https://git-scm.com/book/en/v2/Git-Tools-Submodules) to clone this repo to the approriate directory of your project. 
In this case the special [Editor](https://docs.unity3d.com/Manual/SpecialFolders.html) folder inside your Unity project.

To accomplish this, switch to your Editor directory in a terminal window:

```
cd [YourUnity3DProjectPath]/Assets/Scripts/Editor
```

Then pull this repo in a subfolder like `JRahmatiNL.Unity3D.CustomEditorTools`:
```
git submodule add -- https://github.com/JRahmatiNL/Unity3D.CustomEditorTools JRahmatiNL.Unity3D.CustomEditorTools
```
