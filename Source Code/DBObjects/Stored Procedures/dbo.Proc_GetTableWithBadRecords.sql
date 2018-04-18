IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetTableWithBadRecords]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetTableWithBadRecords]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE Procedure Proc_GetTableWithBadRecords
As
SET NOCOUNT ON
Declare  Cur Cursor For SELECT a.name,a.id FROM SYSOBJECTS a
left join syscolumns b on a.id = b.id where b.name = 'CREATED_BY'

Declare @name varchar(255)
Declare @id varchar(255)
open cur

FETCH NEXT FROM cur
INTO @name , @id

WHILE @@FETCH_STATUS = 0
BEGIN
	SELECT @name
	exec('SELECT * FROM ' + @name + ' WHERE 
	CREATED_BY Is Null OR Convert(Varchar,CREATED_BY) = '''' OR CREATED_DATETIME Is Null')
	FETCH NEXT FROM cur
	INTO @name , @id
END
close cur
Deallocate cur
SET NOCOUNT OFF






GO

