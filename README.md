# PCBox Manager
**PCBox Manager** is the official (though optional) configuration manager for the [PCBox emulator](https://github.com/MoochMcGee/PCBox). It's released under the MIT license, so it can be freely distributed with PCBox. See the `LICENSE` file for more information.

It's written in C# with Windows Forms using Visual Studio 2017. Please see the [wiki](https://github.com/MoochMcGee/PCBoxManager/wiki) for additional information.

## Features
* Powerful, lightweight and completely optional
* Create multiple isolated virtual machines
* Give each virtual machine a unique name and an optional description
* Run multiple virtual machines at the same time
* Control virtual machines from the Manager (pause, reset, etc.)
* A tray icon so the Manager window doesn't get in your way

## System requirements
Same as for PCBox. Additionally, the following is required:  

* The latest version of PCBox
* .NET Framework 4.0

## Support
If you have any issues, questions, suggestions, etc., please visit the official PCBox support channels on Discord (see the main PCBox repo for links). Lead developer, MoochMcGee, is often idling there.

## How to use
1. Download the desired build [here](https://github.com/MoochMcGee/PCBoxManager/releases)
2. Run `PCManager.exe`
3. Choose the folder where `PCBox.exe` is located (along with the roms folder) and a folder where your virtual machines will be located (for configs, nvr folders, etc.)
4. Start creating new virtual machines and enjoy

## How to build
1. Clone the repo
2. Open `PCBoxManager.sln` solution file in Visual Studio 2017
3. Make your changes
4. Choose the `Release` configuration and `x86` platform/CPU
5. Build the solution
6. `PCManager.exe` is now in `Bin\x86\Release\`


