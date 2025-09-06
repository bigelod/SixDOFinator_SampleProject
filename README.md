# SixDOFinator - Sample Project

It's 6DOF head and hand tracking inside a Windows application running on WinlatorXR on a Meta Quest or Pico device! This example project has a few minigames to show off different types of game mechanics, but they're not fully fleshed out games with win/lose conditions

For the barebones version of this project which requires no external assets and has a player prefab ready to drop into your own projects, check out this repo instead: [SixDOFinator_MinimalProject](https://github.com/bigelod/SixDOFinator_MinimalProject)

If MIT license doesn't suit your needs, and you can use code under "The Unlicense" to your benefit instead, please feel free to consider this sample project as licensed under "The Unlicense" or Public Domain, I would love to see more PCVR on standalone in future!

# Special Thanks

Thanks to Luboš for WinlatorXR and testing/debug help!

Thanks to GmoLargey for testing!

# Get WinlatorXR for your Quest or Pico device

This project is designed to run within a Windows (Wine) container on WinlatorXR, go check that out [here](https://github.com/lvonasek/WinlatorXR)

# Unity Editor Version: 
2022.3.62f1 (LTS)

# How to build (and run):

Download the source code

Install TMP Pro package from Unity editor

Install the free packages (see Assets/AssetPacks/Readme.txt)

Build and run in WinlatorXR on your Meta Quest or Pico device


WinlatorXR sends the XR data via UDP port 7872 to the container

# UDP data example string:

client0 0.213 0.287 -0.933 0.035 0.0 0.0 -0.008 -0.229 -0.173 0.095 -0.296 0.947 -0.077 0.0 0.0 0.154 -0.240 -0.140 0.146 -0.072 0.048 0.985 0.037 0.006 -0.017 0.0678 99.00 103.40 224 TFFFFFFFFFTTTFFFFFT

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

Note: Since this was created, new data has been added by the WinlatorXR developer, these values come in at the end of the above string:

L_GRIP, L_MENU, L_THUMBSTICK_PRESS, L_THUMBSTICK_LEFT, L_THUMBSTICK_RIGHT, L_THUMBSTICK_UP, L_THUMBSTICK_DOWN, L_TRIGGER, L_X, L_Y,
R_A, R_B, R_GRIP, R_THUMBSTICK_PRESS, R_THUMBSTICK_LEFT, R_THUMBSTICK_RIGHT, R_THUMBSTICK_UP, R_THUMBSTICK_DOWN, R_TRIGGER

These values come as either T for TRUE or F for FALSE

Newer versions of WinlatorXR also provide the HMD sending the data, eg: PICO or META, so that some specific offsets can be applied if needed

# WinlatorXR XrApi

The documentation of the XrApi can be found [here](https://github.com/lvonasek/WinlatorXR/releases/tag/winlatorxr_cmod_v13_11)

# Asset credits:

> TMP (Text Mesh Pro) built-in to Unity
> Bombs and Explosions by Cyberwave Orchestra
> Sci-FI Glitch Pack by E.V.I.L Studios
> Sci-FiPistol01 by Game Stuff Studio
> ShootingSound by B.G.M
> Simple FX Kit by Indian Ocean Assets
> Sound Pack Free Edition by University of Games

# Poly.Pizza Assets:

> Fishing Rod by Quaternius (https://poly.pizza/m/skqPvEIpve)
> Chest Gold by Quaternius (https://poly.pizza/m/4jqKCGHpUO)
> Gold Bag by Quaternius (https://poly.pizza/m/vFFblhnHtb)
> Gem Green by Quaternius (https://poly.pizza/m/kbgiCMzdxg)
> Robot Enemy Large by Quaternius (https://poly.pizza/m/mPDR0L5uKx)
> Robot Enemy Flying by Quaternius (https://poly.pizza/m/lF3jeRJwiH)
> Robot Enemy by Quaternius (https://poly.pizza/m/1gNo5ezvmr)

# FreeSound Assets:

> Joy.wav by Seth_Makes_Sounds -- https://freesound.org/s/685124/ -- License: Creative Commons 0
> Brick Drop on Grass 1.wav by dichardson -- https://freesound.org/s/570966/ -- License: Creative Commons 0
> Techno loop by TheNikonProductions -- https://freesound.org/s/361890/ -- License: Creative Commons 0
> Binaural Birds LOOP 2.wav by maarten91 -- https://freesound.org/s/468448/ -- License: Creative Commons 0
