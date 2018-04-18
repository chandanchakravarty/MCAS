IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_deleteRecords]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[proc_deleteRecords]
GO

SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER OFF
GO



/*----------------------------------------------------------      
Proc Name     : dbo.proc_deleteRecords      
Created by      : Anurag Verma      
Date                  : 7/07/2005      
Purpose         : To delete record from table given
Revison History :      
Used In         : Wolverine      
------------------------------------------------------------      
Date     Review By          Comments      
------   ------------       -------------------------*/    
CREATE PROCEDURE proc_deleteRecords
(
@LISTID VARCHAR(7000),
@TABLENAME VARCHAR(7000)
)
as
DECLARE @STRQUERY NVARCHAR(3000)

BEGIN
	set @strQuery=N'deLETE FROM ' +  @TABLENAME +' WHERE ' + @listID 
	exec sp_executesql @strQuery
END


GO

