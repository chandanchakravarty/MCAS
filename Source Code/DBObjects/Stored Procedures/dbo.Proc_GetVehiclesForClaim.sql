IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetVehiclesForClaim]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetVehiclesForClaim]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- 
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
 
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

 
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------


 
/* ----------------------------------------------------------            
Proc Name            : Dbo.Proc_GetVehiclesForClaim    
Created by             : Vijay Arora  
Date                    : 28-09-2006  
Purpose                : To get all the vehicles for against Claim   
Revison History   :            
Used In                 :   Wolverine            
------   ------------       -------------------------*/            
--drop proc Proc_GetVehiclesForClaim    
CREATE PROC dbo.Proc_GetVehiclesForClaim   
(            
	@CLAIM_ID  int,  
	@VEHICLE_ID int = NULL  
)    
AS
BEGIN           
  
 DECLARE @CUSTOMER_ID INT  
 DECLARE @POLICY_ID INT  
 DECLARE @POLICY_VERSION_ID INT  
  
 SELECT @CUSTOMER_ID = CUSTOMER_ID, @POLICY_ID = POLICY_ID, @POLICY_VERSION_ID = POLICY_VERSION_ID  
 FROM CLM_CLAIM_INFO WITH (NOLOCK) WHERE CLAIM_ID = @CLAIM_ID  
  
 IF (@VEHICLE_ID IS NULL)  
  SELECT  VIN + ' - ' + VEHICLE_YEAR + ' - ' + MAKE + ' - ' + MODEL AS VIN_NUMBER,  
  VEHICLE_ID, VEHICLE_YEAR, MAKE, MODEL, VIN, BODY_TYPE,ISNULL(REGISTERED_STATE,'') AS REG_STATE,
  ISNULL(BODY_TYPE,'') AS BODY_TYPE    
  FROM  POL_VEHICLES WITH (NOLOCK) WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID   
  AND POLICY_VERSION_ID = @POLICY_VERSION_ID  AND IS_ACTIVE='Y' 
  AND VEHICLE_ID NOT IN (SELECT ISNULL(POLICY_VEHICLE_ID,0) FROM CLM_INSURED_VEHICLE WHERE CLAIM_ID=@CLAIM_ID AND IS_ACTIVE='Y')

 ELSE  
  SELECT  VIN + ' - ' + VEHICLE_YEAR + ' - ' + MAKE + ' - ' + MODEL AS VIN_NUMBER,  
  VEHICLE_ID, VEHICLE_YEAR, MAKE, MODEL, VIN, BODY_TYPE, APP_USE_VEHICLE_ID,
  ISNULL(REGISTERED_STATE,'') AS REG_STATE,  ISNULL(BODY_TYPE,'') AS BODY_TYPE
  FROM  POL_VEHICLES WITH (NOLOCK) WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID   
  AND POLICY_VERSION_ID = @POLICY_VERSION_ID AND VEHICLE_ID = @VEHICLE_ID AND IS_ACTIVE='Y' 
END            



GO

