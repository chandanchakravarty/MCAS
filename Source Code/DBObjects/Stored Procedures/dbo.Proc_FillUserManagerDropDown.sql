IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_FillUserManagerDropDown]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_FillUserManagerDropDown]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------    
Proc Name       : dbo.Proc_FillUserManagerDropDown    
Created by      : Gaurav    
Date            : 3/15/2005    
Purpose         : To select  record in MNT_USER_LIST , Manager Name    
Revison History :    
Used In         :   Wolverine    
------------------------------------------------------------    
Date     Review By          Comments    
------   ------------       -------------------------*/    
-- DROP PROC Dbo.Proc_FillUserManagerDropDown   
CREATE PROC Dbo.Proc_FillUserManagerDropDown    
(  
 @USER_SYSTEM_ID varchar (8)
)    
AS    
    
BEGIN    
    
SELECT  USER_ID, USER_FNAME+ ' ' + USER_LNAME AS [Manager Name]  FROM MNT_USER_LIST where Is_Active= 'Y' and USER_SYSTEM_ID= @USER_SYSTEM_ID ORDER BY USER_FNAME+ ' ' + USER_LNAME ASC    

    
    
END    
  



GO

