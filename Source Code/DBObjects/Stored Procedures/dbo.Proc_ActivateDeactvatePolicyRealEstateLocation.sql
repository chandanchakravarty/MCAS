IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_ActivateDeactvatePolicyRealEstateLocation]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_ActivateDeactvatePolicyRealEstateLocation]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*  --------------------------------------------------------------------------
CREATE BY    : Ravindra
CREATED DATE : 03-21-2006
Purpose      : To implement the activate deactivate functionlity in policy real estate locaton  
*/  
CREATE PROCEDURE Proc_ActivateDeactvatePolicyRealEstateLocation  
(  
 @CUSTOMER_ID  INT,  
 @POLICY_ID   INT,  
 @POLICY_VERSION_ID INT,  
 @LOCATION_ID INT,  
 @STATUS   CHAR(1)  
)  
AS  
BEGIN  
 UPDATE POL_UMBRELLA_REAL_ESTATE_LOCATION  
  
 SET IS_ACTIVE = @STATUS  
 WHERE CUSTOMER_ID = @CUSTOMER_ID AND  
  POLICY_ID = @POLICY_ID AND  
  POLICY_VERSION_ID = @POLICY_VERSION_ID AND  
  LOCATION_ID = @LOCATION_ID   
END  
  



GO

