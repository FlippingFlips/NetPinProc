# NetPinProc.Game.Server
### ❓ What is it?
For managing a NetPinProc.Sqlite.Game database. Machine items, adjustments, audits and tools.

### ⚡ Quick Start / Test
1. Download a server release build, linux or windows [NetPinProc.Server*.zip](https://github.com/FlippingFlips/NetPinProc/releases)
2. Run the server and visit the URL 
![](screen1.jpg)
## 💾 Database
1. A `P3-ROC \ PDB` database will be generated in the directory named `netproc.db` from the
[init_p3roc.sql](NetPinProc.Game.Manager.Server/sql/init_proc.sql) file
You can safely remove `netproc.db` and the cache files `-shm` and `-wal` and it will be created on load again if you need to reset.
**Delete when the server isn't running**.
**DeleteOnInit can also be set from the appsettings.json**.
2. The path to the database can be changed inside the [appsettings.json](appsettings.json) file.
3. Setting `P3Roc` to `false` in the `appsettings.json` will use the [init_proc.sql](NetPinProc.Game.Manager.Server/sql/init_proc.sql) file
to generate rows from the items in the file.
## ❓Client: How To...
## Dashboard
Change the `PRGame` options, like game title , version. For P-ROC boards you can change the machine type here
## Machine Items
Most should be self explanatory but you can hover over the grids header to get a tool tip about the column
and more information on what should be entered.
#### Export to Json
Small button top left to export the whole machine to `machine.json`.
`machine.json` can be used in other applications and other python frameworks, PinGod.
## Playfield
Small set of tools to update and export machine item center point positions.
#### Blueprint
Import an image of the playfield to the database which is used in exports.
If you have a game in `Visual Pinball` this is handy to export the blueprint from there and import it here.
The blueprint can come from anywhere but you would want it to fit with the playfield SVG template and no stretch.
#### Export SVG
By default there is an SVG template in the database which is sized at `514.350mm x 1066.800`, `20.25x42"`, a standard Stern.
**You can export this blank template first, change to your size with `Inkscape`, and reimport it as a template**

The template contains layers which match the machine items, like `SWITCHES`, `DRIVERS`.
The export will insert your machine items into the layers.
If the machine item has an `XPos` or `YPos` then they will be placed in position.

If you use `Inkscape` to load the exported `.svg` you will see your items in the file.
Move items into position and save the export.
#### Import SVG
After you have update items for their center points the machine items `X` and `YPos`'s can be updated by importing the file.

