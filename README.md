# VL.Avalonia

An [Avalonia](https://avaloniaui.net/) UI framework wrapper for the visual live programming environment [vvvv](https://vvvv.org/).

**VL.Avalonia** aims to integrate Avalonia into vvvv/VL workflows to enable developers to build rich, interactive desktop applications through visual programming.

## Development Status

⚠️ **This project is currently in active development** and may be subject to breaking changes, deprecations, and unstable features. **Not recommended for production use.**

Getting in touch: [VL Avalonia Room on Matrix.org](https://matrix.to/#/!OcgAznOYIIpPAkwOhi:matrix.org?via=matrix.org)

## Installation

In order to use this library with VL you have to install the nuget that is available via nuget.org. For information on how to use nugets with VL, see [Managing Nugets](https://thegraybook.vvvv.org/reference/libraries/dependencies.html#manage-nugets) in the VL documentation. As described there you go to the commandline and then type:

For now you have to install `VL.Avalonia` packages separately:

```sh
# VL.Avalonia core packages with controls, styles, help etc.
nuget install VL.Avalonia

# VL.Avalonia.Skia Avalonia application hosting and rendering in vvvv/vl (AvaloniaLayer)
nuget install VL.Avalonia.Skia

# VL.Avalonia.Stride Experimental Avalonia application renderer for VL.Stride (AvaloniaRenderer)
nuget install VL.Avalonia.Stride

# VL.Avalonia.Custom example project on bringing your own XAML Controls in vvvv/vl
# includes additional controls
nuget install VL.Avalonia.Custom
```

## Compatibility

| Version | vvvv gamma Version |
| :--- | :--- |
| `v0.7.X` | >= `7.4` |
| `v0.6.X` | >= `7.1` |
| `v0.4.X` | >= `7.1-0116` |
| `v0.3.1` - `v0.3.16` | `7.0` |
| `v0.3.0`| `7.0-xxxx`, `6.9` |


## How-to

Demos are available via the Help Browser!

## Contributing

[CONTRIBUTING](CONTRIBUTING.md)

## Credits

- [antokhio](https://github.com/antokhio)
- [woeishi](https://github.com/woeishi)
- [azeno](https://github.com/azeno)
- [bj-rn](https://github.com/bj-rn)
- [cloneproduction](https://github.com/cloneproduction)

Try it with vvvv, the visual live-programming environment for .NET  
Download: http://visualprogramming.net
