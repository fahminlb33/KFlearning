# Building KF-MinGW

KF-MinGW is custom Mingw-w64 distributions with added Freeglut and GLEW library.
You'll need to download and build Freeglut and GLEW manually.

Mingw-w64 Toolchain: GCC 8.1.0 (i686)
Freeglut: 3.2.1
GLEW: 2.1.0
CMake: 3.17.2

## Steps

1. Download and extract required files.
2. Add Mingw-w64 `bin` and CMake `bin` to system environment variable.
3. `cd` to Freeglut directory, then execute:
   1.  `cmake -G "MinGW Makefiles" -S . -B . -DCMAKE_INSTALL_PREFIX=C:\mingw64\x86_64-w64-mingw32`
   2.  `mingw32-make all`
   3.  `mingw32-make install`
4. `cd` to GLEW directory, then execute:
   1. `cmake -G "MinGW Makefiles" -S . -B . -DCMAKE_INSTALL_PREFIX=C:\mingw64\x86_64-w64-mingw32`
   2.  `mingw32-make all`
   3.  `mingw32-make install`
5. Open **KFlearning.Mingw.Setup** project, then open Variables.wxi.
6. Change SourceDir variable to your Mingw-w64 directory.
7. (Optional) To make sure you have up-to-date file table, run WiX Harvest Tool (heat).
8. Build the project.