# NetPinProc

- [NetPinProc.Domain](NetPinProc.Domain) = Classes, interfaces and helpers with no 3rd party dependencies.
- [NetPinProc](NetPinProc) = Files for a physical PROC Device board. Uses the Domain library for `IProcDevice`
- [NetPinProc.Dmd](NetPinProc.Dmd) = Mostly for use when using a PROC Dmd controller on an existing machine
- [NetPinProc.Game](NetPinProc.Game) = Base game implementations for a `IGameController`
- [NetPinProc.Game.Sqlite](NetPinProc.Game.Sqlite) = Database Sqlite version of `IGameController` building off the `NetPinProc.Game`
- [NetPinProc.Ports.WSLED](NetPinProc.Ports.WSLED) = Serial LED drivers

## Tools
- [NetPinProc.Game.Server](Tools\NetPinProc.Game.Server) - Manages the database & pinball machine with a client web application.
You can run this locally while developing or serve from your pinball machine. Builds can be found in releases.

![image](Tools/NetPinProc.Game.Server/screen1.jpg)

## Tests
See testing examples for how to test and write your own methods

- [NetPinProc.Tests](.tests/NetPinProc.Tests) - Xunit tests
- [NetPinProc.Game.Tests](.tests/NetPinProc.Game.Tests) - Xunit tests
- [NetPinProc.Game.Sqlite.Tests](.tests/NetPinProc.Game.Sqlite.Tests) - Xunit tests

## Examples
- [.examples](.examples) - P-ROC and P3-ROC console applications