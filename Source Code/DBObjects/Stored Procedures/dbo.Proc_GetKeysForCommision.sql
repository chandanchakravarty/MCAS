IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetKeysForCommision]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetKeysForCommision]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*                                  
----------------------------------------------------------                                      
Proc Name       : dbo.Proc_GetKeysForCommision
Created by      : Ravindra                                    
Date            : 09-18-2006
Purpose         :       
Revison History :                                      
------------------------------------------------------------                                      
Date     Review By          Comments                                      
------   ------------       -------------------------                                     
*/       
--drop proc dbo.Proc_GetKeysForCommision
create proc [dbo].[Proc_GetKeysForCommision]      
(      
 @CUSTOMER_ID INT,      
 @POLICY_ID INT,      
 @POLICY_VERSION_ID INT,      
 @RISK_ID INT      
)      
      
AS      
BEGIN 
--APP_USE_VEHICLE_ID -->>  vehicle Use 11332(Personal)  11333(Commercial)
--APP_VEHICLE_PERTYPE_ID -- >> Vehicle Type for Personal Vehcle 
--APP_VEHICLE_COMTYPE_ID -- >> Vehicle Type for Commercial Vehcle      
--APP_VEHICLE_PERCLASS_ID -- >> Vehicle Class Personal
--APP_VEHICLE_COMCLASS_ID -- >> Vehicle Class Commercial

DECLARE @PERSONAL Int
DECLARE @COMMERCIAL Int

SET @PERSONAL = 11332
SET @COMMERCIAL =11333

DECLARE @RISK_TYPE Int
DECLARE @RISK_USE Int
DECLARE @RISK_CLS Int

SELECT @RISK_USE = ISNULL(VEHICLE_INFO.APP_USE_VEHICLE_ID,0)
FROM  POL_VEHICLES VEHICLE_INFO
WHERE VEHICLE_INFO.CUSTOMER_ID	 = @CUSTOMER_ID      
AND   VEHICLE_INFO.POLICY_ID	 = @POLICY_ID      
AND   VEHICLE_INFO.POLICY_VERSION_ID = @POLICY_VERSION_ID      
AND   VEHICLE_INFO.VEHICLE_ID	= @RISK_ID


SELECT  @RISK_TYPE  = CASE @RISK_USE WHEN @PERSONAL THEN ISNULL(VEHICLE_INFO.APP_VEHICLE_PERTYPE_ID,0) 
		ELSE  ISNULL(VEHICLE_INFO.APP_VEHICLE_COMTYPE_ID,0) END ,
	@RISK_CLS   = CASE @RISK_USE WHEN @PERSONAL THEN ISNULL(VEHICLE_INFO.APP_VEHICLE_PERCLASS_ID,0)
		ELSE ISNULL(VEHICLE_INFO.APP_VEHICLE_COMCLASS_ID,0) END
FROM  POL_VEHICLES VEHICLE_INFO
WHERE VEHICLE_INFO.CUSTOMER_ID	 = @CUSTOMER_ID      
AND   VEHICLE_INFO.POLICY_ID	 = @POLICY_ID      
AND   VEHICLE_INFO.POLICY_VERSION_ID = @POLICY_VERSION_ID      
AND   VEHICLE_INFO.VEHICLE_ID	= @RISK_ID

SELECT       
POL_INFO.APP_EFFECTIVE_DATE AS APP_EFFECTIVE_DATE,      
POL_INFO.POLICY_LOB AS LOB_ID, 
POL_INFO.POLICY_SUBLOB AS SUBLOB_ID,     
POL_INFO.STATE_ID AS STATE_ID,      
ISNULL(POL_INFO.POLICY_TYPE,0) AS PRODUCT_TYPE,
@RISK_TYPE AS RISK_TYPE,
@RISK_USE AS RISK_USE,
@RISK_CLS AS RISK_CLS,
-- Added by Asfa(19-June-2008) - As per mail of Ravindra
POL_INFO.APPLY_INSURANCE_SCORE AS INSURANCE_SCORE
FROM POL_CUSTOMER_POLICY_LIST POL_INFO WITH(NOLOCK)      
WHERE POL_INFO.CUSTOMER_ID	 = @CUSTOMER_ID      
AND   POL_INFO.POLICY_ID	 = @POLICY_ID      
AND   POL_INFO.POLICY_VERSION_ID = @POLICY_VERSION_ID      

END      



GO

