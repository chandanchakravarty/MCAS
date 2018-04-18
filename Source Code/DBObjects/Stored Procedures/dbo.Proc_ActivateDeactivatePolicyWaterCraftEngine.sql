IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_ActivateDeactivatePolicyWaterCraftEngine]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_ActivateDeactivatePolicyWaterCraftEngine]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROC dbo.Proc_ActivateDeactivatePolicyWaterCraftEngine                             
(                              
 @CUSTOMER_ID  INT,            
 @POLICY_ID  INT,            
 @POLICY_VERSION_ID INT,            
 @ENGINE_ID  INT ,           
 @IS_ACTIVE NCHAR(2)       
)                              
AS                              
BEGIN                                            
 UPDATE POL_WATERCRAFT_ENGINE_INFO  
 SET IS_ACTIVE = @IS_ACTIVE WHERE                      
 CUSTOMER_ID = @CUSTOMER_ID AND                       
 POLICY_ID = @POLICY_ID AND                      
 POLICY_VERSION_ID = @POLICY_VERSION_ID AND                      
 ENGINE_ID = @ENGINE_ID                 
END            
    





GO

