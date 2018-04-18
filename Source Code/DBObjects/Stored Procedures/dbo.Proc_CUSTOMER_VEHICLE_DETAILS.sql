IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_CUSTOMER_VEHICLE_DETAILS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_CUSTOMER_VEHICLE_DETAILS]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--BEGIN TRAN
--DROP PROC dbo.Proc_CUSTOMER_VEHICLE_DETAILS
--GO
/*----------------------------------------------------------                                          
Proc Name          : Dbo.Proc_GetCustomerDetails                                          
Created by           : Sibin Thomas Philip                                          
Date                    : 06/11/2009                                          
Purpose               : To get Customer and Vehicle Information for UDI Report                                 
------------------------------------------------------------                                          
            
Date Review By          Comments                                          
------   ------------       -------------------------*/                                          
--DROP PROC dbo.Proc_CUSTOMER_VEHICLE_DETAILS 547,81,2,'pol'                       
CREATE PROC [dbo].[Proc_CUSTOMER_VEHICLE_DETAILS]                                          
(                                          
 @CUSTOMER_ID  INT,
 @APP_POLICY_ID INT,
 @APP_POLICY_VERSION_ID INT,
 @CALLED_FROM VARCHAR(5)                    
)                           
AS                                          
BEGIN                                          

IF(@CALLED_FROM = 'APP')
BEGIN
 SELECT CLT.CUSTOMER_ID,                                           
 ISNULL(CLT.CUSTOMER_FIRST_NAME,'') CUSTOMER_FIRST_NAME,                                          
 ISNULL(CLT.CUSTOMER_MIDDLE_NAME,'') CUSTOMER_MIDDLE_NAME,                                          
 ISNULL(CLT.CUSTOMER_LAST_NAME,'') CUSTOMER_LAST_NAME,
 AV.GRG_ADD1 AS CUSTOMER_ADDRESS1,
 AV.GRG_ADD2 AS CUSTOMER_ADDRESS2,
 AV.GRG_CITY AS CUSTOMER_CITY,
 AV.GRG_STATE AS CUSTOMER_STATE,
 ISNULL(MCSL.STATE_CODE,'') AS CUSTOMER_STATE_CODE,
 AV.GRG_ZIP AS CUSTOMER_ZIP               
                                                  
FROM  CLT_CUSTOMER_LIST CLT LEFT OUTER JOIN APP_LIST AL 
 ON CLT.CUSTOMER_ID = AL.CUSTOMER_ID INNER JOIN
 APP_VEHICLES AV ON
 AL.CUSTOMER_ID = AV.CUSTOMER_ID AND AL.APP_ID = AV.APP_ID 
 AND AL.APP_VERSION_ID = AV.APP_VERSION_ID AND 
 VEHICLE_ID=(SELECT TOP 1 VEHICLE_ID FROM APP_VEHICLES WHERE CUSTOMER_ID = @CUSTOMER_ID 
			AND APP_ID = @APP_POLICY_ID AND APP_VERSION_ID = @APP_POLICY_VERSION_ID) 
 LEFT OUTER JOIN MNT_COUNTRY_STATE_LIST MCSL ON AV.GRG_STATE = MCSL.STATE_ID    
WHERE (AL.CUSTOMER_ID = @CUSTOMER_ID  AND AL.APP_ID = @APP_POLICY_ID 
	  AND AL.APP_VERSION_ID = @APP_POLICY_VERSION_ID)

END 
ELSE IF(@CALLED_FROM = 'POL')
BEGIN       
 SELECT CLT.CUSTOMER_ID,                                           
 ISNULL(CLT.CUSTOMER_FIRST_NAME,'') CUSTOMER_FIRST_NAME,                                          
 ISNULL(CLT.CUSTOMER_MIDDLE_NAME,'') CUSTOMER_MIDDLE_NAME,                                          
 ISNULL(CLT.CUSTOMER_LAST_NAME,'') CUSTOMER_LAST_NAME,
 GENDER AS GENDER,
 PV.GRG_ADD1 AS CUSTOMER_ADDRESS1,
 PV.GRG_ADD2 AS CUSTOMER_ADDRESS2,
 PV.GRG_CITY AS CUSTOMER_CITY,
 PV.GRG_STATE AS CUSTOMER_STATE,
 ISNULL(MCSL.STATE_CODE,'') AS CUSTOMER_STATE_CODE,
 PV.GRG_ZIP AS CUSTOMER_ZIP               
                                                  
 FROM  CLT_CUSTOMER_LIST CLT 
 LEFT OUTER JOIN POL_CUSTOMER_POLICY_LIST PCPL 
 ON CLT.CUSTOMER_ID = PCPL.CUSTOMER_ID INNER JOIN
 POL_VEHICLES PV ON
 PCPL.CUSTOMER_ID = PV.CUSTOMER_ID AND PCPL.POLICY_ID = PV.POLICY_ID 
 AND PCPL.POLICY_VERSION_ID = PV.POLICY_VERSION_ID AND 
 VEHICLE_ID=(SELECT TOP 1 VEHICLE_ID FROM POL_VEHICLES WHERE CUSTOMER_ID = @CUSTOMER_ID 
			 AND POLICY_ID = @APP_POLICY_ID AND POLICY_VERSION_ID = @APP_POLICY_VERSION_ID)
 LEFT OUTER JOIN MNT_COUNTRY_STATE_LIST MCSL ON PV.GRG_STATE = MCSL.STATE_ID    
 WHERE (PCPL.CUSTOMER_ID = @CUSTOMER_ID AND PCPL.POLICY_ID = @APP_POLICY_ID AND 
		PCPL.POLICY_VERSION_ID = @APP_POLICY_VERSION_ID) 
END       
                                
END 

--GO
--EXEC Proc_CUSTOMER_VEHICLE_DETAILS 547,81,2,'pol'
--ROLLBACK TRAN



GO

