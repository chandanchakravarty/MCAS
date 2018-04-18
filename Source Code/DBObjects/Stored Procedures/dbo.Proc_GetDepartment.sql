IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetDepartment]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetDepartment]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------        
Proc Name       : dbo.Proc_GetDepartment        
Created by      : Ashwani        
Date            : 5/9/2005        
Purpose       : Return the Query       
Revison History :        
Used In   : Wolverine        
------------------------------------------------------------        
Date     Review By          Comments        
------   ------------       -------------------------*/        
CREATE PROC   dbo.Proc_GetDepartment        
(        
   @DEPT_ID  int    
)        
AS        
BEGIN        
  SELECT DEPT_ID,DEPT_CODE,DEPT_NAME,DEPT_ADD1,DEPT_ADD2,DEPT_CITY,DEPT_STATE,DEPT_ZIP,DEPT_COUNTRY,DEPT_PHONE,DEPT_EXT,  
  DEPT_FAX,DEPT_EMAIL,IS_ACTIVE
   FROM MNT_DEPT_LIST      
  WHERE DEPT_ID = @DEPT_ID    
END      
  
  
  



GO

