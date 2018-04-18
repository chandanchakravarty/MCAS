IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdateAppRuleVerification]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdateAppRuleVerification]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
   
/*----------------------------------------------------------    
Proc Name       : dbo.Proc_UpdateApplicationRuleVerification    
Created by      : Manoj Rathore    
Date            : 06-10-2005    
Purpose         : To showing the images on the Client Top .    
Revison History :    
Used In         :   wolvorine    
    
------------------------------------------------------------    
Date     Review By          Comments    
------   ------------       -------------------------*/    
  
CREATE PROC dbo.Proc_UpdateAppRuleVerification
(    
 @CUSTOMER_ID     int,    
 @APP_ID     int,    
 @APP_VERSION_ID     smallint,
 @RULE_VERIFICATION smallint	   
)    
AS    
BEGIN    
   	 UPDATE APP_LIST   
		 SET RULE_VERIFICATION = @RULE_VERIFICATION      
	 WHERE    
		 CUSTOMER_ID =@CUSTOMER_ID AND    
		 APP_ID=@APP_ID AND    
		 APP_VERSION_ID=@APP_VERSION_ID    
END    
  
  







GO

