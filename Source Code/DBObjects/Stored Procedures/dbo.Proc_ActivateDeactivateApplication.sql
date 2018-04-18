IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_ActivateDeactivateApplication]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_ActivateDeactivateApplication]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------      
Proc Name       : Proc_ActivateDeactivateApplication  
Created by      : Swastika     
Date            : 27th Jun'06  
Purpose         : To Activate/Deactivate the application      
Revison History :      
Used In         : Wolverine      
------------------------------------------------------------      
Date     Review By          Comments      
------   ------------       -------------------------*/      
--drop proc dbo.Proc_ActivateDeactivateApplication  
CREATE  PROC dbo.Proc_ActivateDeactivateApplication
(      
 @CUSTOMER_ID Int,  
 @APP_ID Int,  
 @APP_VERSION_ID SmallInt,  
 @IS_ACTIVE   NChar(1)      
)      
AS      
BEGIN    
  
 UPDATE APP_LIST  
 SET       
    IS_ACTIVE  = @IS_ACTIVE     
 WHERE      
   CUSTOMER_ID =  @CUSTOMER_ID AND  
   APP_ID = @APP_ID AND  
   APP_VERSION_ID = @APP_VERSION_ID
  
 RETURN 1  
      
END  
  
  



GO

