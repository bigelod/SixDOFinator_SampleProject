# SixDOFinator - Sample Project

It's 6DOF head and hand tracking inside a Windows application running on WinlatorXR on a Meta Quest or Pico device! This example project has a few minigames to show off different types of game mechanics, but they're not fully fleshed out games with win/lose conditions

For the barebones version of this project which requires no external assets and has a player prefab ready to drop into your own projects, check out this repo instead: [SixDOFinator_MinimalProject](https://github.com/bigelod/SixDOFinator_MinimalProject)

# Unity Editor Version: 
2022.3.62f1 (LTS)

# How to build (and run):

Download the source code
Install TMP Pro package from Unity editor
Install the free packages (see Assets/AssetPacks/Readme.txt)
Build and run in WinlatorXR on your Meta Quest or Pico device

WinlatorXR sends the XR data via UDP port 7872 to the container

# UDP data example string:

client0 0.080 -0.675 -0.728 -0.090 0.0 0.0 -0.210 -0.212 -0.359 -0.032 -0.609 0.790 -0.063 0.0 0.0 0.174 -0.202 -0.139 -0.036 -0.020 0.007 0.999 0.030 0.003 0.059 0.0681 99.00 103.40 255

If this isn't running inside WinlatorXR, it will create a folder on whatever drive it runs eg: D:/xrtemp

You can put a file with this UDP data string in the name and uncheck "UDP Only" in the ReadPosData script on the player to load that pose data example

# XR UDP string data format:

Left Hand Quaternion X, Left Hand Quaternion Y, Left Hand Quaternion Z, Left Hand Quaternion W, 
Left Hand Thumbstick X, Left Hand Thumbstick Y, Left Hand X Position, Left Hand Y Position, Left Hand Z Position,  
Right Hand Quaternion X, Right Hand Quaternion Y, Right Hand Quaternion Z, Right Hand Quaternion W, 
Right Hand Thumbstick X, Right Hand Thumbstick Y, Right Hand X Position, Right Hand Y Position, Right Hand Z Position,
HMD Quaternion X, HMD Quaternion Y, HMD Quaternion Z, HMD Quaternion W, 
HMD X Position, HMD Y position, HMD Z Position, Current IPD, Current FOV Horizontal, Current FOV Vertical,
XR Frame ID

The last is an INT between 0 and 255, the rest are float/double values
