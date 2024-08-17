# NetPinProc.Game

![.Net](https://img.shields.io/badge/.NET-5C2D91?style=for-the-badge&logo=.net&logoColor=white)  [![netpinproc.release](https://github.com/FlippingFlips/NetPinProc.Game/actions/workflows/netpinproc.game.release-nuget.yml/badge.svg)](https://github.com/FlippingFlips/NetPinProc.Game/actions/workflows/netpinproc.game.release-nuget.yml)

## GameController : IGameController
Core object representing the game itself.
Usually a game developer will create a new game by inheriting this class.

## BaseGameController
This class uses the <see cref="GameController"/> base with added helper methods and setup automation and common game events

## Uses
* [NetPinProc](https://github.com/FlippingFlips/NetPinProc) - For communication with PROC board or Fake PROC.
Uses Domain library for shared classes, modes with no dependencies.
See `Domain.IGameController`

## GameController
This library has default implementations for an `IGameController`. This `IGameController` can initialize a `ProcDevice` or a `FakeProcDevice` depending on the `simulation` parameter.

## BaseGameController
Implements GameController. Base a game on this class to automate Trough building.

### Examples + Tests
🧪 [Tests](tests/NetPinProc.Game.Tests)

🎲 [FakeProcGame P3-ROC](examples/P3-ROC/NetPinProc.FakeProcDevice/)

---

## NetProcGame - Compy, Preble
[NetProcGame](https://github.com/Compy/NetProcGame) is a port of the [pyprocgame](http://www.github.com/preble/pyprocgame) to the C# programming language. The port was done by Jimmy Lipham and includes most of the major functionality of the pyprocgame framework.

## Licence
[License](LICENSE.md)