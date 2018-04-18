IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetPolicyFarmInfo]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetPolicyFarmInfo]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO





CREATE PROCEDURE dbo.Proc_GetPolicyFarmInfo
(    
 @CUSTOMER_ID int  ,
 @POLICY_ID  int,
 @POLICY_VERSION_ID int,
 @FARM_ID int
 
)        
AS             

BEGIN                  

SELECT * FROM POL_UMBRELLA_FARM_INFO
WHERE  CUSTOMER_ID=@CUSTOMER_ID
  AND POLICY_ID=@POLICY_ID 
  AND POLICY_VERSION_ID=@POLICY_VERSION_ID
  AND FARM_ID=@FARM_ID


End  
  



GO

