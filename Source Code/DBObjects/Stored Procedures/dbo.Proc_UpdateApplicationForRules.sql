IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdateApplicationForRules]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdateApplicationForRules]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------  
Proc Name       : dbo.Proc_UpdateApplicationForRules  
Created by      : Nidhi  
Date            : 8/26/2005  
Purpose         : Update  
Revison History :  
Used In         :    
------------------------------------------------------------  
Date     Review By          Comments  
------   ------------       -------------------------*/  
CREATE   PROC Dbo.Proc_UpdateApplicationForRules  
(  
 @CUSTOMER_ID     int,  
 @APP_ID     int,  
 @APP_VERSION_ID     smallint,    
 @SHOW_QUOTE     char(1),  
 @APP_VERIFICATION_XML text,
 @RULE_INPUT_XML  text  
)  
AS  
BEGIN  
 UPDATE APP_LIST  
 SET   
  SHOW_QUOTE= @SHOW_QUOTE  ,  
  APP_VERIFICATION_XML=@APP_VERIFICATION_XML,
  RULE_INPUT_XML=@RULE_INPUT_XML  
 WHERE  
 CUSTOMER_ID =@CUSTOMER_ID AND  
 APP_ID=@APP_ID AND  
 APP_VERSION_ID=@APP_VERSION_ID  
END  





GO

