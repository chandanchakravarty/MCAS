IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_GETPROTECTIVEDEVICEINFODATA]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_GETPROTECTIVEDEVICEINFODATA]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
  
    
      
/*----------------------------------------------------------                          
PROC NAME      : DBO.[PROC_GETPROTECTIVEDEVICEINFODATA]                          
CREATED BY     : PRADEEP KUSHWAHA                        
DATE           : 19-04-2010                          
PURPOSE        : RETRIEVING DATA FROM POL_PROTECTIVE_DEVICES                          
REVISON HISTORY:                 
MODIFY BY      :                          
DATE           :                          
PURPOSE        :         
USED IN        : EBIX ADVANTAGE                      
------------------------------------------------------------                          
DATE     REVIEW BY          COMMENTS                          
------   ------------       -------------------------*/                          
--DROP PROC DBO.PROC_GETPROTECTIVEDEVICEINFODATA 0,2171,127,1,49
                 
CREATE PROC [dbo].[PROC_GETPROTECTIVEDEVICEINFODATA]    

@RISK_ID INT=NULL,  
@CUSTOMER_ID INT,  
@POLICY_ID INT,  
@POLICY_VERSION_ID INT,  
@LOCATION_ID INT=NULL
AS                          
                          
BEGIN 

IF (@RISK_ID<>'' AND @RISK_ID <>0)

	BEGIN
		SELECT CUSTOMER_ID,  POLICY_ID,  POLICY_VERSION_ID,  RISK_ID,  PROTECTIVE_DEVICE_ID,  
		FIRE_EXTINGUISHER,  SPL_FIRE_EXTINGUISHER_UNIT,  MANUAL_FOAM_SYSTEM,  FOAM,  INERT_GAS_SYSTEM,  
		MANUAL_INERT_GAS_SYSTEM,  COMBAT_CARS,  CORRAL_SYSTEM,  ALARM_SYSTEM,  FREE_HYDRANT,  SPRINKLERS,  
		SPRINKLERS_CLASSIFICATION,  FIRE_FIGHTER,  QUESTIION_POINTS,  IS_ACTIVE,  CREATED_BY,  CREATED_DATETIME,  
		MODIFIED_BY,  LAST_UPDATED_DATETIME  ,LOCATION_ID
		  
	FROM POL_PROTECTIVE_DEVICES  WITH(NOLOCK)
	WHERE  
		CUSTOMER_ID=@CUSTOMER_ID AND  
		POLICY_ID=@POLICY_ID AND  
		POLICY_VERSION_ID=@POLICY_VERSION_ID AND 
		RISK_ID=@RISK_ID   
END
ELSE
	BEGIN
		SELECT CUSTOMER_ID,  POLICY_ID,  POLICY_VERSION_ID,  RISK_ID,  PROTECTIVE_DEVICE_ID,  
		FIRE_EXTINGUISHER,  SPL_FIRE_EXTINGUISHER_UNIT,  MANUAL_FOAM_SYSTEM,  FOAM,  INERT_GAS_SYSTEM,  
		MANUAL_INERT_GAS_SYSTEM,  COMBAT_CARS,  CORRAL_SYSTEM,  ALARM_SYSTEM,  FREE_HYDRANT,  SPRINKLERS,  
		SPRINKLERS_CLASSIFICATION,  FIRE_FIGHTER,  QUESTIION_POINTS,  IS_ACTIVE,  CREATED_BY,  CREATED_DATETIME,  
		MODIFIED_BY,  LAST_UPDATED_DATETIME  ,LOCATION_ID
		  
	FROM POL_PROTECTIVE_DEVICES  WITH(NOLOCK)
	WHERE  
		
		CUSTOMER_ID=@CUSTOMER_ID AND  
		POLICY_ID=@POLICY_ID AND  
		POLICY_VERSION_ID=@POLICY_VERSION_ID AND 
		LOCATION_ID=@LOCATION_ID
		
END
                 
END            
GO

