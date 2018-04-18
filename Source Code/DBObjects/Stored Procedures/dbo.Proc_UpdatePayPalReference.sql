IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdatePayPalReference]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdatePayPalReference]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROC dbo.Proc_UpdatePayPalReference  
(  
 @CUSTOMER_ID INT,  
 @POLICY_ID INT,  
 @POLICY_VERSION_ID INT,  
 @PAT_PAL_REF_ID VARCHAR(200)  
)  
AS  
BEGIN  
 UPDATE ACT_POL_CREDIT_CARD_DETAILS  
 SET PAY_PAL_REF_ID = @PAT_PAL_REF_ID  
 WHERE   
 CUSTOMER_ID = @CUSTOMER_ID AND   
 POLICY_ID = @POLICY_ID AND   
 POLICY_VERSION_ID = @POLICY_VERSION_ID 
  
END  
  
GO

