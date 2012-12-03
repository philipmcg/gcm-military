@echo off
set dest=../Military/Generated/
set OLDPATH=%PATH%
set PATH="C:\Python27";%PATH%
python sift.py

move CommanderData.cs %dest%CommanderData.cs
move UnitData.cs %dest%UnitData.cs
move UnitExportData.cs %dest%UnitExportData.cs
move OrganizationData.cs %dest%OrganizationData.cs

move CommanderBattleResultData.cs %dest%CommanderBattleResultData.cs
move UnitBattleResultData.cs %dest%UnitBattleResultData.cs
move OrganizationBattleResultData.cs %dest%OrganizationBattleResultData.cs

move CommanderTurnData.cs %dest%CommanderTurnData.cs
move UnitTurnData.cs %dest%UnitTurnData.cs
move OrganizationTurnData.cs %dest%OrganizationTurnData.cs

move PreviousCommanderData.cs %dest%PreviousCommanderData.cs

copy headers.csv "../../../Games/SCE/SCEWeb/SCE/App_Data/headers.csv"
copy headers.csv "../../../Games/SCE/SCEWeb/SCE/App_Data/gcm/Data/GCSVs/military_headers.csv"
copy headers.csv "../../../Games/SCE/Gcm/Data/GCSVs/military_headers.csv"

pause>nul
