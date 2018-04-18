IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_PrefillAPP_HOME_OWNER_GEN_INFO]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_PrefillAPP_HOME_OWNER_GEN_INFO]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------              
Proc Name       : dbo.Proc_PrefillAPP_HOME_OWNER_GEN_INFO  
Created by      : Sumit Chhabra  
Date            : 12/12/2005              
Purpose       : Prefill record in table APP_HOME_OWNER_GEN_INFO              
Revison History :              
Used In  : Brics              
         
    
              
------------------------------------------------------------              
Date     Review By          Comments              
------   ------------       -------------------------*/              
CREATE   PROC Dbo.Proc_PrefillAPP_HOME_OWNER_GEN_INFO              
(              
 @CUSTOMER_ID  int,              
 @APP_ID   int,              
 @APP_VERSION_ID  smallint  
  
)              
AS              
BEGIN        
DECLARE @IS_AUTO_POL_WITH_CARRIER INT    
DECLARE @SECONDARY_HEAT_TYPE INT    
IF(not EXISTS(SELECT CUSTOMER_ID FROM APP_HOME_OWNER_GEN_INFO WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID))   
 BEGIN    
  SELECT @IS_AUTO_POL_WITH_CARRIER=COUNT(IS_AUTO_POL_WITH_CARRIER) FROM APP_HOME_RATING_INFO WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID AND IS_AUTO_POL_WITH_CARRIER='1'    
  SELECT @SECONDARY_HEAT_TYPE=COUNT(SECONDARY_HEAT_TYPE) FROM APP_HOME_RATING_INFO WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID AND SECONDARY_HEAT_TYPE <>'6211'    
  SELECT CASE @IS_AUTO_POL_WITH_CARRIER WHEN 0 THEN '0' ELSE '1' END  MULTI_POLICY_DISC_APPLIED,CASE @SECONDARY_HEAT_TYPE WHEN 0 THEN '0' ELSE '1' END  ANY_HEATING_SOURCE    
 END   
END    



GO

