<br/><br/>

<p align="center">
<img alt="Logo" src="./etc/logo.png" width="30%"/>
</p>

<br/><br/>

<p align="center">
<a href="https://opensource.org/licenses/MIT"><img src="https://img.shields.io/badge/License-MIT-green.svg" alt="License: MIT">
<a href="https://unity3d.com/get-unity/download/archive"><img src="https://img.shields.io/badge/unity-2023.1.11f1-black.svg?style=flat&amp;logo=unity" alt="Unity Engine"></a>
<a href="https://github.com/jcs090218/Unity.Mx/releases/latest"><img src="https://img.shields.io/github/tag/jcs090218/Unity.Mx.svg?label=release&logo=github" alt="Release Tag"></a>
<a href="https://docs.unity3d.com/2018.3/Documentation/Manual/ScriptingRuntimeUpgrade.html"><img src="https://img.shields.io/badge/.NET-2.0-blueviolet.svg" alt=".NET"></a></p>
</p>

> M-x for Unity

<!-- markdown-toc start - Don't edit this section. Run M-x markdown-toc-refresh-toc -->
**Table of Contents**

- [🏆 Features](#🏆-features)
- [💾 Installation](#💾-installation)
- [🔨 Usage](#🔨-usage)
  - [❓ How to define your own command?](#❓-how-to-define-your-own-command)
  - [⚛ `Interactive` Attribute's Properties](#⚛-interactive-attributes-properties)
    - [Summary (`string`)](#summary-string)
    - [Icon (`string`)](#icon-string)
    - [Tooltip (`string`)](#tooltip-string)
    - [Enabled (`boolean`)](#enabled-boolean)
- [📌 Credits](#📌-credits)
- [License](#license)

<!-- markdown-toc end -->

## 🏆 Features

- Out of the box
- Easy to use
- Features 

## 💾 Installation

WIP

## 🔨 Usage

Hit <kbd>Alt</kbd>+<kbd>x</kbd>!

### ❓ How to define your own command?

Here is a simple example that prints out `"Hello World!~"` with `Debug.Log`.

```cs
[Interactive(Summary: "Print Hello World!")]
private static void PrintHelloWorld()
{
    Debug.Log("Hello World!~");
}
```

But you need to define under a class inherit `Mx`!

```cs
using Mx;  // For InteractiveAttribute.cs

public class DummyCommands : Mx.Mx
{
    // Place your command function here!
}
```

You can see all more advanced examples in our source code, under
[Assets/Mx/Editor/Commands][]!

### ⚛ `Interactive` Attribute's Properties

This part of the document explains all properties inside `Interactive`
attribute.

<!-- TODO: Put a explain image here. -->

#### Summary (`string`)

A brief description of your command. It will appear on the right of your
command name.

#### Icon (`string`)

The name of the icon.

See full list of icon in [unity-editor-icons][].

#### Tooltip (`string`)

The full description of your command. It will appear in the popup window when
you hover with your mouse.

#### Enabled (`boolean`)

Enable/Disable your command. If the value is `false`, it will not be shown
inside the completion window.

## 📌 Credits

- [Find Editor Tools][] by **`@phwitti`** - UI extracted here
- [FlxCs][] by **`@jcs090218`** - Fuzzy matching library

## License

Copyright (c) Jen-Chieh Shen. All rights reserved.

See [LICENSE](./LICENSE) for details.


[Assets/Mx/Editor/Commands]: https://github.com/jcs090218/Unity.Mx/tree/master/Assets/Mx/Editor/Commands
[unity-editor-icons]: https://github.com/halak/unity-editor-icons
[FlxCs]: https://github.com/jcs090218/FlxCs
[Find Editor Tools]: https://github.com/phwitti/unity-find-editor-tools
