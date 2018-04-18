IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdatePolForRules]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdatePolForRules]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



/*----------------------------------------------------------  
Proc Name       : dbo.Proc_UpdatePolForRules  
Created by      : Ashwnai  
Date            : 15 Sep. 2006
Purpose         : Update  
Revison History :  
Used In         :    
------------------------------------------------------------  
Date     Review By          Comments  
------   ------------       -------------------------*/  
CREATE   PROC [dbo].[Proc_UpdatePolForRules]  
(  
 @CUSTOMER_ID     int,  
 @POL_ID     int,  
 @POL_VERSION_ID     smallint,    
 @SHOW_QUOTE     char(1),  
 @APP_VERIFICATION_XML text,
 @RULE_INPUT_XML  text  ,
 @IS_UNDER_REVIEW NCHAR(1)=NULL
)  
AS  
BEGIN  
 UPDATE POL_CUSTOMER_POLICY_LIST  
 SET   
  SHOW_QUOTE= @SHOW_QUOTE  ,  
  APP_VERIFICATION_XML=@APP_VERIFICATION_XML,
  RULE_INPUT_XML=@RULE_INPUT_XML  ,
  IS_UNDER_REVIEW = @IS_UNDER_REVIEW
 WHERE  
 CUSTOMER_ID =@CUSTOMER_ID AND  
 POLICY_ID=@POL_ID AND  
 POLICY_VERSION_ID=@POL_VERSION_ID  
END  










GO

