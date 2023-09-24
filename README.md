[![License: MIT](https://img.shields.io/badge/License-MIT-green.svg)](https://opensource.org/licenses/MIT)
[![Unity Engine](https://img.shields.io/badge/unity-2023.1.11f1-black.svg?style=flat&logo=unity)](https://unity3d.com/get-unity/download/archive)
[![.NET](https://img.shields.io/badge/.NET-2.0-blueviolet.svg)](https://docs.unity3d.com/2018.3/Documentation/Manual/ScriptingRuntimeUpgrade.html)

# Meta X
> M-x for Unity

## 🔨 Usage

Hit <kbd>Alt</kbd>+<kbd>x</kbd>!

## ❓ How to define your own command?

Here is a simple example that print out `"Hello World!~"` with `Debug.Log`.

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

You can see all complex examples in our source code, under [Assets/Mx/Editor/Commands][]!

## 📌 Dependencies

- [FlxCs](https://github.com/jcs090218/FlxCs)


[Assets/Mx/Editor/Commands]: https://github.com/jcs090218/Unity.Mx/tree/master/Assets/Mx/Editor/Commands
