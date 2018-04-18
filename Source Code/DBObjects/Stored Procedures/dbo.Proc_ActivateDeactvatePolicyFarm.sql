IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_ActivateDeactvatePolicyFarm]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_ActivateDeactvatePolicyFarm]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO





CREATE PROCEDURE Proc_ActivateDeactvatePolicyFarm  
(  
 @CUSTOMER_ID  INT,  
 @POLICY_ID   INT,  
 @POLICY_VERSION_ID INT,  
 @FARM_ID INT,  
 @STATUS   CHAR(1)  
)  
AS  
BEGIN  
 UPDATE POL_UMBRELLA_FARM_INFO
  
 SET IS_ACTIVE = @STATUS  
 WHERE CUSTOMER_ID = @CUSTOMER_ID AND  
  POLICY_ID = @POLICY_ID AND  
  POLICY_VERSION_ID = @POLICY_VERSION_ID AND  
  FARM_ID = @FARM_ID   
END  
  



GO

