IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_GetDiaryEntry]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_GetDiaryEntry]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

 /*----------------------------------------------------------            
Proc Name        : dbo.Proc_UpdateDiaryEntryStatus    
Created by        : Vijay Arora    
Date                : 22-12-2005    
Purpose          : Update the Diary Entry Status    
Revison History  :            
Used In          : Wolverine            
------------------------------------------------------------            
Date     Review By          Comments      
DROP PROC PROC_GetDiaryEntry
------   ------------       -------------------------*/        
CREATE PROC [dbo].[PROC_GetDiaryEntry]
(
@CUSTOMER_ID INT,
@POLICY_ID INT,
@POLICY_VERSION_ID INT,
@LIST_TYPE_ID INT ,
@MODULE_ID INT
)
AS 
BEGIN
	SELECT * FROM TODOLIST WITH(NOLOCK) WHERE CUSTOMER_ID  = @CUSTOMER_ID AND 
	POLICY_ID = @POLICY_ID AND POLICY_VERSION_ID = @POLICY_VERSION_ID
	AND LISTTYPEID = @LIST_TYPE_ID AND MODULE_ID = @MODULE_ID
END
GO

