BEGIN TRANSACTION;

--TODO - untested tables, old

-- Table: Machine
INSERT INTO Machine (Id, machineType, numOfBalls) VALUES (1, 7, 4);
-- Table: PRCoils
INSERT INTO Coils (Id, Name, Number, PulseTime, Bus, Polarity, Search, Tags) VALUES ('flipperLwRMain', NULL, 'C01', 30, '', 0, '');
INSERT INTO Coils (Id, Name, Number, PulseTime, Bus, Polarity, Search, Tags) VALUES ('flipperLwRHold', NULL, 'C02', 30, '', 0, '');
INSERT INTO Coils (Id, Name, Number, PulseTime, Bus, Polarity, Search, Tags) VALUES ('flipperLwLMain', NULL, 'C03', 30, '', 0, '');
INSERT INTO Coils (Id, Name, Number, PulseTime, Bus, Polarity, Search, Tags) VALUES ('flipperLwLHold', NULL, 'C04', 30, '', 0, '');
INSERT INTO Coils (Id, Name, Number, PulseTime, Bus, Polarity, Search, Tags) VALUES ('trough',		   NULL, 'C05', 30, '', 0, 'trough');
INSERT INTO Coils (Id, Name, Number, PulseTime, Bus, Polarity, Search, Tags) VALUES ('slingL',		   NULL, 'C06', 20, '', 0, 'sling');
INSERT INTO Coils (Id, Name, Number, PulseTime, Bus, Polarity, Search, Tags) VALUES ('slingR',		   NULL, 'C07', 20, '', 0, 'sling');
INSERT INTO Coils (Id, Name, Number, PulseTime, Bus, Polarity, Search, Tags) VALUES ('bumper1',		   NULL, 'C08', 20, '', 0, 'bumper');
INSERT INTO Coils (Id, Name, Number, PulseTime, Bus, Polarity, Search, Tags) VALUES ('bumper2',	       NULL, 'C09', 20, '', 0, 'bumper');
INSERT INTO Coils (Id, Name, Number, PulseTime, Bus, Polarity, Search, Tags) VALUES ('bumper3',		   NULL, 'C10', 20, '', 0, 'bumper');
INSERT INTO Coils (Id, Name, Number, PulseTime, Bus, Polarity, Search, Tags) VALUES ('flipperRelay',   NULL, 'C11', 165, '', 0, '');
-- Table: PRLamps
INSERT INTO Lamps (Id, Name, Number, Bus, Polarity) VALUES ('ballSave', NULL, 'L11', 'shootAgain', 0);
INSERT INTO Lamps (Id, Name, Number, Bus, Polarity) VALUES ('key1', NULL, 'L12', '', 0);
INSERT INTO Lamps (Id, Name, Number, Bus, Polarity) VALUES ('key2', NULL, 'L13', '', 0);
INSERT INTO Lamps (Id, Name, Number, Bus, Polarity) VALUES ('key3', NULL, 'L14', '', 0);
INSERT INTO Lamps (Id, Name, Number, Bus, Polarity) VALUES ('key4', NULL, 'L15', '', 0);
INSERT INTO Lamps (Id, Name, Number, Bus, Polarity) VALUES ('key5', NULL, 'L16', '', 0);
INSERT INTO Lamps (Id, Name, Number, Bus, Polarity) VALUES ('b1k', NULL, 'L17', '', 0);
INSERT INTO Lamps (Id, Name, Number, Bus, Polarity) VALUES ('b2k', NULL, 'L18', '', 0);
INSERT INTO Lamps (Id, Name, Number, Bus, Polarity) VALUES ('b3k', NULL, 'L19', '', 0);
INSERT INTO Lamps (Id, Name, Number, Bus, Polarity) VALUES ('b4k', NULL, 'L20', '', 0);
INSERT INTO Lamps (Id, Name, Number, Bus, Polarity) VALUES ('b5k', NULL, 'L21', '', 0);
INSERT INTO Lamps (Id, Name, Number, Bus, Polarity) VALUES ('b6k', NULL, 'L22', '', 0);
INSERT INTO Lamps (Id, Name, Number, Bus, Polarity) VALUES ('b7k', NULL, 'L23', '', 0);
INSERT INTO Lamps (Id, Name, Number, Bus, Polarity) VALUES ('b8k', NULL, 'L24', '', 0);
INSERT INTO Lamps (Id, Name, Number, Bus, Polarity) VALUES ('b9k', NULL, 'L25', '', 0);
INSERT INTO Lamps (Id, Name, Number, Bus, Polarity) VALUES ('b10k', NULL, 'L26', '', 0);
INSERT INTO Lamps (Id, Name, Number, Bus, Polarity) VALUES ('b20k', NULL, 'L27', '', 0);
INSERT INTO Lamps (Id, Name, Number, Bus, Polarity) VALUES ('x2', NULL, 'L28', '', 0);
INSERT INTO Lamps (Id, Name, Number, Bus, Polarity) VALUES ('x3', NULL, 'L29', '', 0);
INSERT INTO Lamps (Id, Name, Number, Bus, Polarity) VALUES ('x5', NULL, 'L30', '', 0);
INSERT INTO Lamps (Id, Name, Number, Bus, Polarity) VALUES ('left25k', NULL, 'L31', '', 0);
INSERT INTO Lamps (Id, Name, Number, Bus, Polarity) VALUES ('right25k', NULL, 'L32', '', 0);
INSERT INTO Lamps (Id, Name, Number, Bus, Polarity) VALUES ('pm0', NULL, 'L33', '', 0);
INSERT INTO Lamps (Id, Name, Number, Bus, Polarity) VALUES ('pm1', NULL, 'L34', '', 0);
INSERT INTO Lamps (Id, Name, Number, Bus, Polarity) VALUES ('pm2', NULL, 'L35', '', 0);
INSERT INTO Lamps (Id, Name, Number, Bus, Polarity) VALUES ('pm3', NULL, 'L36', '', 0);
INSERT INTO Lamps (Id, Name, Number, Bus, Polarity) VALUES ('pm4', NULL, 'L37', '', 0);
INSERT INTO Lamps (Id, Name, Number, Bus, Polarity) VALUES ('xtraball', NULL, 'L38', '', 0);
INSERT INTO Lamps (Id, Name, Number, Bus, Polarity) VALUES ('specialR', NULL, 'L39', '', 0);
INSERT INTO Lamps (Id, Name, Number, Bus, Polarity) VALUES ('grot25k', NULL, 'L40', '', 0);
INSERT INTO Lamps (Id, Name, Number, Bus, Polarity) VALUES ('grot1', NULL, 'L41', '', 0);
INSERT INTO Lamps (Id, Name, Number, Bus, Polarity) VALUES ('grot2', NULL, 'L42', '', 0);
INSERT INTO Lamps (Id, Name, Number, Bus, Polarity) VALUES ('grot3', NULL, 'L43', '', 0);
INSERT INTO Lamps (Id, Name, Number, Bus, Polarity) VALUES ('grot4', NULL, 'L44', '', 0);
INSERT INTO Lamps (Id, Name, Number, Bus, Polarity) VALUES ('grot5', NULL, 'L45', '', 0);
INSERT INTO Lamps (Id, Name, Number, Bus, Polarity) VALUES ('rArrow', NULL, 'L46', '', 0);
-- Table: PRSwitches
INSERT INTO Switches (Name, SwitchType, Number, Tags, SearchReset, SearchStop, ItemType) VALUES ('coin1', 'NO', '00', 'door', '', '', '');
INSERT INTO Switches (Name, SwitchType, Number, Tags, SearchReset, SearchStop, ItemType) VALUES ('coin2', 'NO', '01', 'door', '', '', '');
INSERT INTO Switches (Name, SwitchType, Number, Tags, SearchReset, SearchStop, ItemType) VALUES ('coin3', 'NO', '02', 'door', '', '', '');
INSERT INTO Switches (Name, SwitchType, Number, Tags, SearchReset, SearchStop, ItemType) VALUES ('coin4', 'NO', '03', 'door', '', '', '');
INSERT INTO Switches (Name, SwitchType, Number, Tags, SearchReset, SearchStop, ItemType) VALUES ('exit', 'NO', '04', 'door', '', '', '');
INSERT INTO Switches (Name, SwitchType, Number, Tags, SearchReset, SearchStop, ItemType) VALUES ('down', 'NO', '05', 'door', '', '', '');
INSERT INTO Switches (Name, SwitchType, Number, Tags, SearchReset, SearchStop, ItemType) VALUES ('up', 'NO', '06', 'door', '', '', '');
INSERT INTO Switches (Name, SwitchType, Number, Tags, SearchReset, SearchStop, ItemType) VALUES ('enter', 'NO', '07', 'door', '', '', '');
INSERT INTO Switches (Name, SwitchType, Number, Tags, SearchReset, SearchStop, ItemType) VALUES ('coinDoor', 'NO', '08', 'door', '', '', '');
-- give the flippers itemtype flipper to be picked up in config. Links to the coil with same name to auto fire
INSERT INTO Switches (Name, SwitchType, Number, Tags, SearchReset, SearchStop, ItemType) VALUES ('flipperLwL', 'NO', '09', '', '', 'closed', 'flipper');
INSERT INTO Switches (Name, SwitchType, Number, Tags, SearchReset, SearchStop, ItemType) VALUES ('flipperLwR', 'NO', '11', '', '', 'closed', 'flipper');
INSERT INTO Switches (Name, SwitchType, Number, Tags, SearchReset, SearchStop, ItemType) VALUES ('flipperUpL', 'NO', '13', '', '', 'closed', 'flipper');
INSERT INTO Switches (Name, SwitchType, Number, Tags, SearchReset, SearchStop, ItemType) VALUES ('flipperUpR', 'NO', '15', '', '', 'closed', 'flipper');
INSERT INTO Switches (Name, SwitchType, Number, Tags, SearchReset, SearchStop, ItemType) VALUES ('slamTilt', 'NO', '16', '', '', '', '');
INSERT INTO Switches (Name, SwitchType, Number, Tags, SearchReset, SearchStop, ItemType) VALUES ('tilt', 'NO', '17', '', '', '', '');
INSERT INTO Switches (Name, SwitchType, Number, Tags, SearchReset, SearchStop, ItemType) VALUES ('ballShooter', 'NO', '18', '', '', '', '');
INSERT INTO Switches (Name, SwitchType, Number, Tags, SearchReset, SearchStop, ItemType) VALUES ('startButton', 'NO', '19', '', '', '', '');
INSERT INTO Switches (Name, SwitchType, Number, Tags, SearchReset, SearchStop, ItemType) VALUES ('troughEntry', 'NO', '20', '', '', '', '');
INSERT INTO Switches (Name, SwitchType, Number, Tags, SearchReset, SearchStop, ItemType) VALUES ('trough1', 'NO', '21', 'trough', '', 'closed', '');
INSERT INTO Switches (Name, SwitchType, Number, Tags, SearchReset, SearchStop, ItemType) VALUES ('trough2', 'NO', '22', 'trough', '', 'closed', '');
INSERT INTO Switches (Name, SwitchType, Number, Tags, SearchReset, SearchStop, ItemType) VALUES ('trough3', 'NO', '23', 'trough', '', 'closed', '');
INSERT INTO Switches (Name, SwitchType, Number, Tags, SearchReset, SearchStop, ItemType) VALUES ('trough4', 'NO', '24', 'trough,troughEject', '', '', '');
INSERT INTO Switches (Name, SwitchType, Number, Tags, SearchReset, SearchStop, ItemType) VALUES ('shooter', 'NO', '25', 'shooterLane', '', 'closed', '');
INSERT INTO Switches (Name, SwitchType, Number, Tags, SearchReset, SearchStop, ItemType) VALUES ('leftOutLane', 'NO', '26', 'early', '', '', '');
INSERT INTO Switches (Name, SwitchType, Number, Tags, SearchReset, SearchStop, ItemType) VALUES ('leftReturn', 'NO', '27', '', '', '', '');
INSERT INTO Switches (Name, SwitchType, Number, Tags, SearchReset, SearchStop, ItemType) VALUES ('rightOutLane', 'NO', '28', 'early', '', '', '');
INSERT INTO Switches (Name, SwitchType, Number, Tags, SearchReset, SearchStop, ItemType) VALUES ('rightReturn', 'NO', '29', '', '', '', '');
-- give the bumpers/slings itemtype bumpper to be picked up in config. Links to the coil with same name to auto fire
INSERT INTO Switches (Name, SwitchType, Number, Tags, SearchReset, SearchStop, ItemType) VALUES ('slingL', 'NO', '30', '', 'open', '', 'bumper');
INSERT INTO Switches (Name, SwitchType, Number, Tags, SearchReset, SearchStop, ItemType) VALUES ('slingR', 'NO', '31', '', 'open', '', 'bumper');
INSERT INTO Switches (Name, SwitchType, Number, Tags, SearchReset, SearchStop, ItemType) VALUES ('bumperL', 'NO', '32', '', 'open', '', 'bumper');
INSERT INTO Switches (Name, SwitchType, Number, Tags, SearchReset, SearchStop, ItemType) VALUES ('bumperM', 'NO', '33', '', 'open', '', 'bumper');
INSERT INTO Switches (Name, SwitchType, Number, Tags, SearchReset, SearchStop, ItemType) VALUES ('bumperR', 'NO', '34', '', 'open', '', 'bumper');
INSERT INTO Switches (Name, SwitchType, Number, Tags, SearchReset, SearchStop, ItemType) VALUES ('targetL1', 'NO', '35', '', '', '', '');
INSERT INTO Switches (Name, SwitchType, Number, Tags, SearchReset, SearchStop, ItemType) VALUES ('targetL2', 'NO', '36', '', '', '', '');
INSERT INTO Switches (Name, SwitchType, Number, Tags, SearchReset, SearchStop, ItemType) VALUES ('targetL3', 'NO', '37', '', '', '', '');
INSERT INTO Switches (Name, SwitchType, Number, Tags, SearchReset, SearchStop, ItemType) VALUES ('targetL4', 'NO', '38', '', '', '', '');
INSERT INTO Switches (Name, SwitchType, Number, Tags, SearchReset, SearchStop, ItemType) VALUES ('targetL5', 'NO', '39', '', '', '', '');
INSERT INTO Switches (Name, SwitchType, Number, Tags, SearchReset, SearchStop, ItemType) VALUES ('dropTarget1', 'NO', '40', '', '', '', '');
INSERT INTO Switches (Name, SwitchType, Number, Tags, SearchReset, SearchStop, ItemType) VALUES ('dropTarget2', 'NO', '41', '', '', '', '');
INSERT INTO Switches (Name, SwitchType, Number, Tags, SearchReset, SearchStop, ItemType) VALUES ('dropTarget3', 'NO', '42', '', '', '', '');
INSERT INTO Switches (Name, SwitchType, Number, Tags, SearchReset, SearchStop, ItemType) VALUES ('dropTarget4', 'NO', '43', '', '', '', '');
INSERT INTO Switches (Name, SwitchType, Number, Tags, SearchReset, SearchStop, ItemType) VALUES ('dropTarget5', 'NO', '44', '', '', '', '');
INSERT INTO Switches (Name, SwitchType, Number, Tags, SearchReset, SearchStop, ItemType) VALUES ('lane1', 'NO', '45', '', '', '', '');
INSERT INTO Switches (Name, SwitchType, Number, Tags, SearchReset, SearchStop, ItemType) VALUES ('lane2', 'NO', '46', '', '', '', '');
INSERT INTO Switches (Name, SwitchType, Number, Tags, SearchReset, SearchStop, ItemType) VALUES ('lane3', 'NO', '47', '', '', '', '');
INSERT INTO Switches (Name, SwitchType, Number, Tags, SearchReset, SearchStop, ItemType) VALUES ('lane4', 'NO', '48', '', '', '', '');
INSERT INTO Switches (Name, SwitchType, Number, Tags, SearchReset, SearchStop, ItemType) VALUES ('rightLoop', 'NO', '49', '', '', '', '');
INSERT INTO Switches (Name, SwitchType, Number, Tags, SearchReset, SearchStop, ItemType) VALUES ('saucer', 'NO', '50', '', '', '', '');
INSERT INTO Switches (Name, SwitchType, Number, Tags, SearchReset, SearchStop, ItemType) VALUES ('extraBall', 'NO', '51', '', '', '', '');
INSERT INTO Switches (Name, SwitchType, Number, Tags, SearchReset, SearchStop, ItemType) VALUES ('targetKey', 'NO', '52', '', '', '', '');

INSERT INTO Players (Id, Initials, Name, [Default]) VALUES (1, 'AAA', 'Default', '1');

-- Option Types 0=Range, 1=Array, 2= Enum
INSERT INTO Adjustments (Id, Name, Description, Options, OptionType, ValueDefault, Value, MenuName, SubMenuName) VALUES
('ALLOW_RESTART', 'Allow Restart','Allow game restart from holding start.', 'NO,YES', 2, 1, 1, 'STANDARD_ADJ', 'GENERAL'),
('ATTRACT_MUSIC', 'Attract Music','Allow music to play in attract', 'NO,YES', 2, 1, 1, 'STANDARD_ADJ', 'GENERAL'),
('BALLS_PER_GAME', 'Balls Per Game','Number of balls per game 1-10', '1,10', 0, 3, 3, 'STANDARD_ADJ', 'GENERAL'),
('BALL_SAVE_TIME', 'Ball Save Time','Ball saver time', '0,25', 0, 8, 8, 'STANDARD_ADJ', 'GENERAL'),
('BALL_SEARCH_TIME', 'Ball Search Time','Timeout to search for balls and pulse coils', '8,30', 0, 10, 10, 'STANDARD_ADJ', 'GENERAL'),
('IDLE_SHOOTER_TIMEOUT', 'Idle Shooter Timeout','Auto launch ball if idle in plunger lane, 0 disabled', '0,30,60,90,120,150', 1, 60, 60, 'STANDARD_ADJ', 'GENERAL'),
('MASTER_VOL', 'Master Volume','','-30,0', 0, -6, -6, 'STANDARD_ADJ', 'AUDIO'),
('MUSIC_VOL', 'Music Volume','','-30,0', 0, -6, -6, 'STANDARD_ADJ', 'AUDIO'),
('VOICE_VOL', 'Voice Volume','','-30,0', 0, -6, -6, 'STANDARD_ADJ', 'AUDIO'),
('FX_VOL', 'Sound FX Volume','','-30,0', 0, -6, -6, 'STANDARD_ADJ', 'AUDIO'),
('MATCH_PERCENT', 'Match Percent','Match percent, 0 off', '0,20', 0, 5, 5, 'STANDARD_ADJ', 'GENERAL'),
('DISP_W', 'Display Width','','100,1920', 0, 1920, 800, 'STANDARD_ADJ', 'DISPLAY'),
('DISP_H', 'Display Height','','100,1080', 0, 1080, 600, 'STANDARD_ADJ', 'DISPLAY'),
('DISP_X', 'Display X','','0,1920', 0, 0, 0, 'STANDARD_ADJ', 'DISPLAY'),
('DISP_Y', 'Display Y','','0,1080', 0, 0, 0, 'STANDARD_ADJ', 'DISPLAY'),
('DISP_MODE', 'Display Mode','Defaults to 0 = window','WIN,MIN,MAX,FS,FS_EXCLUSIVE', 2, 0, 0, 'STANDARD_ADJ', 'DISPLAY'),
('DISP_CONT_SCALE_MODE', 'Content Scale Mode','','Disabled,CanvasItems,Viewport', 2, 1, 1, 'STANDARD_ADJ', 'DISPLAY'),
('DISP_CONT_SCALE_ASPECT', 'Content Scale Aspect','','Ignore, Keep, KeepWidth, KeepHeight, Expand', 2, 0, 4, 'STANDARD_ADJ', 'DISPLAY'),
('DISP_TOP', 'Display On Top','','OFF,ON', 2, 1, 1, 'STANDARD_ADJ', 'DISPLAY'),
('TILT_WARNINGS', 'Tilt Warnings','Number of tilt warnings before tilt', '0,20', 0, 3, 3, 'STANDARD_ADJ', 'GENERAL');

-- AUDITS (STANDARD)
INSERT INTO Audits (Id, Value, Type, Description) VALUES
('CREDITS', 0, 0, 'Credits in machine'), 
('CREDITS_TOTAL', 0, 0, 'Total credits used'), 
('GAMES_STARTED', 0, 0, 'Games started log'), 
('GAMES_PLAYED', 0, 0, 'Games completed log'), 
('XB_AWARDED', 0, 0, 'Total extra balls awarded'),
('REPLAYS', 0, 0, 'Total replays awarded'), 
('MATCHES', 0, 0, 'Total Matches Awarded'),
('POWERED_ON_TIMES', 0, 0, 'Times machine powered on'),
('TOTAL_BALLS_PLAYED', 0, 0, 'Total balls played');

COMMIT TRANSACTION;