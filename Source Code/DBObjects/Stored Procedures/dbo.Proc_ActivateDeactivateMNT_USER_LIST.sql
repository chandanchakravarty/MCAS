IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_ActivateDeactivateMNT_USER_LIST]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_ActivateDeactivateMNT_USER_LIST]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
   
/*----------------------------------------------------------        
Proc Name       : dbo.Proc_ActivateDeactivateMNT_USER_LIST        
Created by      : Sumit Chhabra        
Date            : 20/09/2006        
Purpose         : To activate/deactivate a user based on no. of licences for agency  
Revison History :        
Used In         :   Wolverine              
------   ------------       -------------------------*/        
-- drop proc dbo.Proc_ActivateDeactivateMNT_USER_LIST        
CREATE PROC dbo.Proc_ActivateDeactivateMNT_USER_LIST        
(        
 @USER_ID INT,  
 @USER_SYSTEM_ID VARCHAR(8),  
 @IS_ACTIVE NCHAR(2)  
)        
AS        
BEGIN        
 declare @AGENCY_LIC_NUM INT    
 declare @USER_LIC_NUM INT    
    
 IF(UPPER(@IS_ACTIVE)='Y')  
  BEGIN    
 ---Check that the number of licensed users do not exceed the number of licences assigned to current agency    
 SELECT @AGENCY_LIC_NUM=ISNULL(AGENCY_LIC_NUM,0) FROM MNT_AGENCY_LIST WHERE AGENCY_CODE=@USER_SYSTEM_ID    
 SELECT @USER_LIC_NUM=ISNULL(COUNT(USER_ID),0) + 1 FROM MNT_USER_LIST     
 WHERE USER_SYSTEM_ID=@USER_SYSTEM_ID AND LIC_BRICS_USER=10963 AND IS_ACTIVE='Y'    
   
 IF(@USER_LIC_NUM > @AGENCY_LIC_NUM)    
 begin  
  return -2  
 end  
 END  
  
 UPDATE MNT_USER_LIST SET IS_ACTIVE = @IS_ACTIVE WHERE USER_ID = @USER_ID    
 return 1
  
  
END        
        
        
        
        
        
        
        
      
    
  
  
  



GO

