# NetPinProc.Game.Sqlite

![.Net](https://img.shields.io/badge/.NET-5C2D91?style=for-the-badge&logo=.net&logoColor=white)  [![netpinproc.game.sqlite.release](https://github.com/FlippingFlips/NetPinProc.Game.Sqlite/actions/workflows/netpinproc.game.sqlite.release-nuget.yml/badge.svg)](https://github.com/FlippingFlips/NetPinProc.Game.Sqlite/actions/workflows/netpinproc.game.sqlite.release-nuget.yml)

[NetPinProc.Game](https://github.com/FlippingFlips/NetPinProc.Game) Controller with Sqlite entity framework database using `net6.0;net7.0`

---

### About
---
This database holds all machine data, adjustments, audits, machine items switches in place of a `machine.json`.

The NetProcDbContext can create a `MachineConfiguration` for use in a game the same way it is when loaded from the usual json file.

### Tables
---

See the interface for Entity Framework DbSets. [INetProcDbContext](src/NetPinProc.Game.Sqlite/INetProcDbContext.cs)

| Table    | Desc |
| -------- | ------- |
| Adjustments  |  Table to hold custom settings to use in service menus and settings   |
| Audits |  Audit by Key(id), same as older games that have an integer value but can have types like enums, see default examples [sql/init_p3roc.sql](src/NetPinProc.Game.Sqlite/sql/init_p3roc.sql)   |
| BallsPlayed    |  Balls played, points and time    |
| Coils    |  Machine coil config   |
| Colors    | Preset colours     |
| GamesPlayed    |  Started, Ended, Adjustments, GameTime   |
| Lamps    |   machine lamps config  |
| Leds    | machine leds config    |
| Lpd8806Leds | PDLED - Serial Leds |
| Machine    |  Machine info, type, title, version   |
| Parts | Parts on the machine, admin use |
| Players    |  id, name, initials, default   |
| Scores    |  points, playerid, gameplayed   |
| Servos    | PDLED Servos  |
| Steppers    | PDLED Steppers  |
| Switches    |   machine switches config   |
| WS281xLeds | PDLED - Serial Leds |


### Machine Database Setup
---

The library should provide example sql init files for `P-ROC` and `P3-ROC` machine setups.

Edit the sql file for your board in [src/sql](src/sql). These files are by P-ROC main controller board type.

### Code use
---

In code you can use `InitializeDatabase` on the `NetProcDbContext` which will seed the database and create a new sqlite file `netproc.db`.

To load the P-ROC device with the machine config from your data use `GetMachineConfiguration` on the `NetProcDbContext`.

### Dependency Injection Service
---
Use the `AddNetProcDataContext` from the extensions to add to a service collection.

### Game Data Controller
---

Derive from [NetProcDataGameController](src/NetPinProc.Game.Sqlite/NetProcDataGameController.cs). This class overrides some game events from NetPinProc.Game and has useful methods for setting data audits, ballsplayed and game saving.

## Creating migrations
---

Make changes to the models to add columns to any table or add new tables code first then create a migration. Migrations are run with the game so your new changes will apply to database.

Open a terminal on the project and run. `dotnet ef migrations add YourMigration`

## License

[License](LICENSE.md)