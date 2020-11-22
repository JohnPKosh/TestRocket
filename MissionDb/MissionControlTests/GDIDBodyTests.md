# GDID Testing Notes

## Scenario #1 - Single Row SELECT by Primary Key Column 

### Base Data

10,000,000 Million rows of identical data pulled from 2 tables (GDID_BODY and ROWID_BODY). 
Both tables have identical schema column structures with the only differing factor being 
the assigned clustered primary key columns (GDID binary(12) and ROWID bigint respectively). 

```sql
CREATE TABLE [dbo].[GDID_BODY]
(
  [GDID] [binary](12) NOT NULL,
  [ROWID] [bigint] IDENTITY(1,1) NOT NULL,
  [BODY] NVARCHAR(MAX)  NULL,
  CONSTRAINT [PK_GDID_BODY] PRIMARY KEY CLUSTERED
  (
    [GDID] ASC
  )
)
```

```sql
CREATE TABLE [dbo].[ROWID_BODY]
(
  [GDID] [binary](12) NOT NULL,
  [ROWID] [bigint] IDENTITY(1,1) NOT NULL,
  [BODY] NVARCHAR(MAX)  NULL,
  CONSTRAINT [PK_ROWID_BODY] PRIMARY KEY CLUSTERED
  (
    [ROWID] ASC
  )
)
```

Both tables consume 3,551.141 MB data space on disk. The GDID_BODY table consumes 20.297 MB 
index space for the PK. While the ROWID_BODY table only consumes 16.758 MB as to be expected 
since indexed column row size is 8 bytes vs. 12 bytes.

Row data in each table is represented by GDID equivalent with era, authority, and counter 
(32 bit, 4 bit, and 60 bit) values. A single era was used, however both authority and counter 
were randomly generated. Each table also includes a ROWID auto-incrementing identity and an 
NVARCHAR(MAX) BODY column. Each body field consists of some LOREM IPSOM and is injected with
random GUID values for additional randomization of the text.

|GDID |	ROWID	| BODY |
|-----|-------|------|
| 0x00000000000000019877F4F1 |	1	| 8AE3E884-99F0-4F76-8457-B7AA4C30094ANeque porro quisquam est qui632E835F-D7C1-4296-9F5F-211F2226156F dolorem ipsum quia dolor sit amet, consectetur, adipisci velit |
| 0x00000000000000230CB2531E |	2	| 23BC1F06-66DC-4B8E-8B9C-00E88969FA1BNeque porro quisquam est qui7FC89C66-34D1-49D2-B6F2-32BEC578AB4B dolorem ipsum quia dolor sit amet, consectetur, adipisci velit |
| 0x0000000000000088DD9C195E |	3	| 8AEAD141-90C2-4C33-AF0E-B69B53B13B33Neque porro quisquam est qui6E48ABAB-00A7-41B5-B6AC-E5A40CE02EF2 dolorem ipsum quia dolor sit amet, consectetur, adipisci velit |
| 0x00000000000000D0C130508A |	4	| 38ECB682-15DD-49EE-9425-4CFD80D555FBNeque porro quisquam est qui2323FF2D-F01A-4FF8-BCBD-73960AB8311F dolorem ipsum quia dolor sit amet, consectetur, adipisci velit |
| 0x00000000000000E6FA632B05 |	5	| DA1CCA11-ECF0-4F41-B458-0DBC28BB407FNeque porro quisquam est quiC28F08ED-28CD-4C71-89C6-535275FF7858 dolorem ipsum quia dolor sit amet, consectetur, adipisci velit |
| 0x00000000000000FAD9D3E428 |	6	| F5D2A511-87F4-4D5C-9229-EE048BC0130DNeque porro quisquam est quiE0CAFB6E-16B6-4E47-A644-B295C00E5A13 dolorem ipsum quia dolor sit amet, consectetur, adipisci velit |

### Test Configuration

**Scenario #1** was executed using an xUnit test project targeting .NET Core 3.1 and utilized 
direct ADO.Net IDbCommand ExecuteQueryReader logic synchronously. The target driver used
was the latest available **Microsoft.Data.SqlClient** stable release version 1.1.2. 
The **System.Data.SqlClient** was skipped for this test since differences should be negligable.

A shared connection was opened and used for each SqlCommand and SqlDataReader steps and 
iterations. There are 3 distinct steps executed in sequence to perform the test (Step #1 
is excluded from the results and is used only to arrange the random data to be tested).

The only functional differences of note between Steps #2 and #3 is the fact that the
input variables are changed. The GDID_BODY table is queried using a paramaterized query
using an array of bytes[12] vs. array of long (8 bytes) for querying the ROWID_BODY table.
The parameter input types and values also differ with the SqlCommand (See below).

```csharp
paramROWID.Value = ROWIDs[i]; // 8 byte long (bigint)
m_Fixture.SutROWIDCommand.Parameters.Add(paramROWID);
```

```csharp
paramGDID.Value = GDIDs[i]; // 12 byte array (Binary 12)
m_Fixture.SutGDIDCommand.Parameters.Add(paramGDID);
```

---

#### Test Run #1 - 1 Million Rows

`MissionControlTests.GDIDBodyTests.CanQueryNTimes(nTimes: 1_000_000)`

- Total ROWID rows read: **1000000** / Total Bytes read: **183000000** / Total ms: **285109**

- Total GDID rows read: **1000000** / Total Bytes read: **183000000** / Total ms: **321609**


#### Test Run #2 - 1 Million Rows

`MissionControlTests.GDIDBodyTests.CanQueryNTimes(nTimes: 1_000_000)`

- Total ROWID rows read: **1000000** / Total Bytes read: **183000000** / Total ms: **284066**

- Total GDID rows read: **1000000** / Total Bytes read: **183000000** / Total ms: **338655**


 #### Test Run #3 - 100K Rows

`MissionControlTests.GDIDBodyTests.CanQueryNTimes(nTimes: 100_000)`

- Total ROWID rows read: **100000** / Total Bytes read: **18300000** / Total ms: **21164**

- Total GDID rows read: **100000** / Total Bytes read: **18300000** / Total ms: **41682**


 #### Test Run #4 - 10K Rows

`MissionControlTests.GDIDBodyTests.CanQueryNTimes(nTimes: 10_000)`

- Total ROWID rows read: **10000** / Total Bytes read: **1830000** / Total ms: **1693**

- Total GDID rows read: **10000** / Total Bytes read: **1830000** / Total ms: **3517**


 #### Test Run #5 - 10K Rows

`MissionControlTests.GDIDBodyTests.CanQueryNTimes(nTimes: 10_000)`

- Total ROWID rows read: **10000** / Total Bytes read: **1830000** / Total ms: **1929**

- Total GDID rows read: **10000** / Total Bytes read: **1830000** / Total ms: **4135**

---

## Scenario #2 - Single Row SELECT by Primary Key Column (Parallel) 

### Base Data

(See above) 

### Test Configuration

(See above)

> Step #2 and Step #3 execute in parallel.

---

#### Test Run #1 - 100K Rows

`MissionControlTests.GDIDBodyTests.CanQueryParallelNTimes(nTimes: 100_000)`

- Total ROWID rows read: **100000** / Total Bytes read: **18300000** / Total ms: **26022**

- Total GDID rows read: **100000** / Total Bytes read: **18300000** / Total ms: **36703**


#### Test Run #2 - 100K Rows

`MissionControlTests.GDIDBodyTests.CanQueryParallelNTimes(nTimes: 100_000)`

- Total ROWID rows read: **100000** / Total Bytes read: **18300000** / Total ms: **28851**

- Total GDID rows read: **100000** / Total Bytes read: **18300000** / Total ms: **39226**


#### Test Run #3 - 10K Rows

`MissionControlTests.GDIDBodyTests.CanQueryParallelNTimes(nTimes: 10_000)`

- Total ROWID rows read: **10000** / Total Bytes read: **1830000** / Total ms: **1709**

- Total GDID rows read: **10000** / Total Bytes read: **1830000** / Total ms: **3533**


#### Test Run #4 - 10K Rows

`MissionControlTests.GDIDBodyTests.CanQueryParallelNTimes(nTimes: 10_000)`

- Total ROWID rows read: **10000** / Total Bytes read: **1830000** / Total ms: **1853**

- Total GDID rows read: **10000** / Total Bytes read: **1830000** / Total ms: **3358**

#### Test Run #5 - 100K Rows (Server GC true)

- Total ROWID rows read: **100000** / Total Bytes read: **18300000** / Total ms: **26161**

-Total GDID rows read: **100000** / Total Bytes read: **18300000** / Total ms: **33364**

#### Test Run #6 - 100K Rows (Server GC true)

- Total ROWID rows read: **100000** / Total Bytes read: **18300000** / Total ms: **26715**

- Total GDID rows read: **100000** / Total Bytes read: **18300000** / Total ms: **33768**

#### Test Run #7 - 1 Million Rows (Server GC true)

- Total ROWID rows read: **1000000** / Total Bytes read: **56000000** / Total ms: **424292**

- Total GDID rows read: **1000000** / Total Bytes read: **56000000** / Total ms: **526460**

---

## Scenario #3 - Single Row SELECT With JOIN by PK Column (Parallel) 

### Base Data

In addition to the 2 original tables, 2 new reference tables are added to enable a table
JOIN condition. Each of the 2 new tables contain 10,000,000 rows and are indexed on the 
ROWID or GDID respectively. 1 row exists in the new tables for each row of the original data.

```sql
CREATE TABLE [dbo].[GDID_EXT_RES_CHILD]
(
  [GDID] [binary](12) NOT NULL,
  [ROWID] [bigint] NOT NULL,
  [OWNER] SMALLINT NOT NULL,
  [EXTERNALID] NVARCHAR(36) NOT NULL,
  CONSTRAINT [PK_GDID_EXT_RES_CHILD] PRIMARY KEY NONCLUSTERED
  (
    [OWNER] ASC, [EXTERNALID] ASC
  )
)
CREATE CLUSTERED INDEX [IDX_GDID_EXT_RES_CHILD]
  ON [dbo].[GDID_EXT_RES_CHILD]
  ([GDID] ASC)
```

```sql
CREATE TABLE [dbo].[ROWID_EXT_RES_CHILD]
(
  [GDID] [binary](12) NOT NULL,
  [ROWID] [bigint] NOT NULL,
  [OWNER] SMALLINT NOT NULL,
  [EXTERNALID] NVARCHAR(36) NOT NULL,
  CONSTRAINT [PK_ROWID_EXT_RES_CHILD] PRIMARY KEY NONCLUSTERED
  (
    [OWNER] ASC, [EXTERNALID] ASC
  )
)
CREATE CLUSTERED INDEX [IDX_ROWID_EXT_RES_CHILD]
  ON [dbo].[ROWID_EXT_RES_CHILD]
  ([ROWID] ASC)
``` 

### Test Configuration

```sql
SELECT b.[GDID]
	,a.[ROWID]
	,b.[EXTERNALID]
FROM [MissionControl].[dbo].[ROWID_BODY] as a
INNER JOIN [MissionControlRef].[dbo].[ROWID_EXT_RES_CHILD] as b
ON a.[ROWID] = b.[ROWID]
WHERE a.[ROWID] = @ROWID
```

```sql
SELECT b.[GDID]
	,a.[ROWID]
	,b.[EXTERNALID]
FROM [MissionControl].[dbo].[GDID_BODY] as a
INNER JOIN [MissionControlRef].[dbo].[GDID_EXT_RES_CHILD] as b
ON a.[GDID] = b.[GDID]
WHERE a.[GDID] = @GDID
```

> Step #2 and Step #3 execute in parallel.

---

#### Test Run #1 - 100K Rows

- Total ROWID rows read: **100000** / Total Bytes read: **5600000** / Total ms: **45042**

- Total GDID rows read: **100000** / Total Bytes read: **5600000** / Total ms: **58509**

#### Test Run #2 - 100K Rows

- Total ROWID rows read: **100000** / Total Bytes read: **5600000** / Total ms: **45669**

- Total GDID rows read: **100000** / Total Bytes read: **5600000** / Total ms: **57596**

#### Test Run #3 - 100K Rows

- Total ROWID rows read: **100000** / Total Bytes read: **5600000** / Total ms: **41620**

- Total GDID rows read: **100000** / Total Bytes read: **5600000** / Total ms: **53111**


---

## Scenario #4 - SELECT TOP 100000 With JOIN by PK Column (SQL Server Management Studio) 

### Base Data

(Same as Scenario #3)

### Test Configuration

> Note - Test Run #1 and Test Run #2 were executed with different TOP statements.

```sql
SELECT TOP 100000 b.[GDID]
	,a.[ROWID]
	,b.[EXTERNALID]
FROM [MissionControl].[dbo].[ROWID_BODY] as a
INNER JOIN [MissionControlRef].[dbo].[ROWID_EXT_RES_CHILD] as b
ON a.[ROWID] = b.[ROWID]
```

```sql
SELECT TOP 100000 b.[GDID]
	,a.[ROWID]
	,b.[EXTERNALID]
FROM [MissionControl].[dbo].[GDID_BODY] as a
INNER JOIN [MissionControlRef].[dbo].[GDID_EXT_RES_CHILD] as b
ON a.[GDID] = b.[GDID]
```

#### Test Run #1 - 100K Rows x 10 (1 Million Total)

- Total ROWID rows read: **1000000** / Total Bytes read: **97191070** / Total ms: **3220**

- Total GDID rows read: **1000000** / Total Bytes read: **97191070** / Total ms: **3930**


#### Test Run #2 - 1 Million Rows x 10 (10 Million Total)

- Total ROWID rows read: **10000000** / Total Bytes read: **971899500** / Total ms: **30718**

- Total GDID rows read: **10000000** / Total Bytes read: **971899500** / Total ms: **37416**


---


## Scenario #5 - Single Row SELECT With JOIN by PK Column (Parallel) (TestConsole.exe)

The Scenario #5 was executed with the same logic that was run with the JOIN query from
the xUnit tests in Scenario #3. This was used to just rule out any variations caused 
by the test runner.

### Base Data

(Same as Scenario #3)

### Test Configuration

(Same as Scenario #3)

#### Test Run #1 - 100K Rows

- Total ROWID rows read: **100000** / Total Bytes read: **5600000** / Total ms: **39190**

- Total GDID rows read: **100000** / Total Bytes read: **5600000** / Total ms: **55294**


## Scenario #6 - Single Row SELECT by Primary Key Column (Parallel) (TestConsole.exe)

Scenario #6 was switched back to the original single row, single table query from earlier.

### Base Data

(Same as Scenario #2)

### Test Configuration

(Same as Scenario #2)

#### Test Run #1 - 100K Rows

- Total ROWID rows read: **100000** / Total Bytes read: **18300000** / Total ms: **33046**

- Total GDID rows read: **100000** / Total Bytes read: **18300000** / Total ms: **37191**

#### Test Run #2 - 100K Rows

- Total ROWID rows read: **100000** / Total Bytes read: **18300000** / Total ms: **28129**

- Total GDID rows read: **100000** / Total Bytes read: **18300000** / Total ms: **33643**


#### Test Run #3 - 100K Rows

- Total ROWID rows read: **100000** / Total Bytes read: **18300000** / Total ms: **16377**

- Total GDID rows read: **100000** / Total Bytes read: **18300000** / Total ms: **33380**


#### Test Run #3 - 1 Million Rows

- Total ROWID rows read: **1000000** / Total Bytes read: **183000000** / Total ms: **283690**

- Total GDID rows read: **1000000** / Total Bytes read: **183000000** / Total ms: **334997**

