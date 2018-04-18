/*----------------------------------------------------------          
Proc Name       : [PROC_GET_QQ_MARINECARGO_RISK_DETAILS]          
Created by      : Ruchika Chauhan  
Date            : 20-March-2012        
Purpose   : Demo          
Revison History :          
Used In        : Singapore          
------------------------------------------------------------          
Date     Review By          Comments          
------   ------------       -------------------------*/      

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_GET_QQ_MARINECARGO_RISK_DETAILS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].PROC_GET_QQ_MARINECARGO_RISK_DETAILS
GO


CREATE PROC PROC_GET_QQ_MARINECARGO_RISK_DETAILS  
(@CUSTOMER_ID   INT,                                                    
 @POLICY_ID  INT,                                                   
 @POLICY_VERSION_ID INT,
 @QUOTE_ID INT  
 ) 
 AS  
 BEGIN  
 IF EXISTS (SELECT CUSTOMER_ID FROM QQ_MARINECARGO_RISK_DETAILS  where CUSTOMER_ID=@CUSTOMER_ID and POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID and QUOTE_ID=@QUOTE_ID)  
 BEGIN  
SELECT * FROM QQ_MARINECARGO_RISK_DETAILS 
 WHERE 
 CUSTOMER_ID=@CUSTOMER_ID and POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID and QUOTE_ID=@QUOTE_ID  
 END  
  
END