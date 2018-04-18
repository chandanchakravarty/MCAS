IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetPagedGLAccounts]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetPagedGLAccounts]
GO

SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER OFF
GO



/*----------------------------------------------------------
Proc Name          : Dbo.Proc_GetPagedGLAccounts
Created by           : Mohit Gupta
Date                    : 19/05/2005
Purpose               : 
Revison History :
Used In                :   Wolverine  
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
CREATE   PROCEDURE Proc_GetPagedGLAccounts
(
@CurrentPage int,
@PageSize int,
@TotalRecords int output
)
AS
--CREATE TABLE #TempTable
--(
--	 ACCOUNT_ID int,
--	ACC_DESCRIPTION nvarchar(100) 	
 --)
begin
--INSERT INTO #TempTable 
--(
--	  ACCOUNT_ID,
--	ACC_DESCRIPTION  	
--)
--SELECT   ACCOUNT_ID,ACC_DESCRIPTION  	
--FROM  ACT_GL_ACCOUNTS

DECLARE @FirstRec int, @LastRec int
SELECT @FirstRec = (@CurrentPage - 1) * @PageSize
SELECT @LastRec = (@CurrentPage * @PageSize + 1)

SELECT 
  ACCOUNT_ID,	ACC_DESCRIPTION  	
FROM 
ACT_GL_ACCOUNTS
--  #TempTable
WHERE 
 ACCOUNT_ID > @FirstRec 
AND
 ACCOUNT_ID < @LastRec
SELECT @TotalRecords = COUNT(*) FROM ACT_GL_ACCOUNTS 
end


GO

