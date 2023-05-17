# Chip-8 Emulator

This is a simple chip-8 emulator I made using windows forms and C#.
It is very slow and there are better methods that I could have used to make it,
but I wanted to do it with windows forms and C# because I would like to learn
.NET better.

## Functions
This chip-8 emulator has three buttons
- Start: starts the emulator
- Load: load rom into ram
- Reset: resets the program

And it has three optional checks
- Modern Shift: uses the modern shift instruction instead of the original
- Old Increment: uses the old increment instruction instead of the modern
- Modern Jump: uses the modern jump instruction instead of the original
These three instructions increase compatibility with a wide variety of chip-8 games

I added a delay box to change a for loop designed to eat clock cycles. This may not be the best
way to do this in the future but it significantly speeds up the speed of the cpu. Initially I tried
using ``Thread.Sleep(1);`` but this did not provide the performance required to make games truely
playable

## Possible Future Improvements
- Move project to better graphics library (Monogame, FNA)
- Increase instruction speed
- Implement super chip functionality

## What I learned
This was my first serious project using C# and it has helped me develop skills programming
.NET applications. I enjoyed using the technology and found Microsoft's documentation helpful
when writing my application. I want to develop more projects using C# in the future.

I also learned the basics of emulator development, and I would like to work on emulators in the
future, either continuing with C# or using a faster language like C/C++ (I would like to develop
my C++ skills), or possibly even Rust. I may in the future look into NES, Gameboy, or Atari 2600
emulation.
