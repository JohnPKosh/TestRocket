
/*
**** using junk DB ****

Using BCP OUT:

bcp [dbo].[GDID_TEST] OUT "%GIT%\TestRocket\MissionDb\Db\Scripts\GDID_TEST.bcp" -S (localdb)\mssqllocaldb -d junk -T -N -b 100000

Using BCP IN:

bcp [dbo].[GDID_IMPORT] IN "%GIT%\TestRocket\MissionDb\Db\Scripts\GDID_TEST.bcp" -S (localdb)\mssqllocaldb -d junk -T -N -b 100000

**** using MissionControl DB ****

Using BCP OUT:

bcp [dbo].[GDID_TEST] OUT "%GIT%\TestRocket\MissionDb\Db\Scripts\GDID_TEST.bcp" -S (localdb)\mssqllocaldb -d MissionControl -T -N -b 10000

Using BCP IN:

bcp [dbo].[GDID_IMPORT] IN "%GIT%\TestRocket\MissionDb\Db\Scripts\GDID_TEST.bcp" -S (localdb)\mssqllocaldb -d MissionControl -T -N -b 10000


*/

TRUNCATE TABLE [dbo].[GDID_IMPORT];

/* We can us absolute path in SQL Statement */
BULK INSERT [dbo].[GDID_IMPORT]
FROM 'C:\VsCode\github\TestRocket\MissionDb\Db\Scripts\GDID_TEST.bcp'
WITH (DATAFILETYPE='widenative', KEEPIDENTITY,KEEPNULLS);

/*
This will not work unless you wrap in xp_cmdshell or use bcp IN:

BULK INSERT [dbo].[GDID_IMPORT]
FROM '%GIT%\TestRocket\MissionDb\Db\Scripts\GDID_TEST.bcp'
WITH (DATAFILETYPE='widenative', KEEPIDENTITY,KEEPNULLS);

*/