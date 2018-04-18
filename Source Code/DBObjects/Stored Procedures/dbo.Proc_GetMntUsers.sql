IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetMntUsers]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetMntUsers]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

 /*----------------------------------------------------------                            
Proc Name       :  dbo.Proc_GetMntUsers            
Created by      :  Raghav           
Date            :  18-08-2008            
Purpose         :              
Revison History :                            
Used In         :  Wolverine                            
                         
    
------------------------------------------------------------                            
Date     Review By          Comments                            
------   ------------       -------------------------*/                            
---Proc_GetMntUsers 'W001'    
-- drop PROC dbo.Proc_GetMntUsers     
create PROC [dbo].[Proc_GetMntUsers]     
(    
 @USER_SYSTEM_ID varchar (100)    
)    
As    
    
SELECT ISNULL(USER_FNAME,'') + ' ' + ISNULL(USER_LNAME,'') AS FULL_NAME,    
[USER_ID] as USERID    
FROM MNT_USER_LIST     
WHERE USER_SYSTEM_ID = @USER_SYSTEM_ID AND IS_ACTIVE='Y' 
order by FULL_NAME

 
     
    
    
    
GO

