IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_UPDATEPROTECTIVEDEVICE]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_UPDATEPROTECTIVEDEVICE]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

  /*----------------------------------------------------------            
PROC NAME       : DBO.POL_PROTECTIVE_DEVICES            
CREATED BY      : PRADEEP KUSHWAHA   
DATE            : 09/04/2010            
PURPOSE       :TO UPDATE RECORDS IN POL_PROTECTIVE_DEVICES TABLE.            
REVISON HISTORY :            
USED IN        : EBIX ADVANTAGE            
------------------------------------------------------------            
DATE     REVIEW BY          COMMENTS            
------   ------------       -------------------------*/            
--DROP PROC DBO.PROC_UPDATEPROTECTIVEDEVICE     
  
CREATE PROC [dbo].[PROC_UPDATEPROTECTIVEDEVICE]
(  
	@RISK_ID INT=NULL,
	@PROTECTIVE_DEVICE_ID INT,
	@FIRE_EXTINGUISHER INT ,
	@SPL_FIRE_EXTINGUISHER_UNIT INT ,
	@MANUAL_FOAM_SYSTEM INT ,
	@FOAM INT ,
	@INERT_GAS_SYSTEM INT ,
	@MANUAL_INERT_GAS_SYSTEM INT ,
	@COMBAT_CARS INT ,
	@CORRAL_SYSTEM INT ,
	@ALARM_SYSTEM INT ,
	@FREE_HYDRANT INT ,
	@SPRINKLERS INT ,
	@SPRINKLERS_CLASSIFICATION NVARCHAR(140)=NULL,
	@FIRE_FIGHTER NVARCHAR(140)=NULL,
	@QUESTIION_POINTS NUMERIC(25,4)=NULL,
	@LAST_UPDATED_DATETIME DATETIME,
	@MODIFIED_BY INT,
	@LOCATION_ID INT =NULL

)  
AS   

BEGIN  
 
IF (@PROTECTIVE_DEVICE_ID<>'' AND @PROTECTIVE_DEVICE_ID <>0)
BEGIN
	UPDATE POL_PROTECTIVE_DEVICES    
	  SET   
	   RISK_ID=@RISK_ID,
		FIRE_EXTINGUISHER=@FIRE_EXTINGUISHER ,
		SPL_FIRE_EXTINGUISHER_UNIT=@SPL_FIRE_EXTINGUISHER_UNIT,
		MANUAL_FOAM_SYSTEM=@MANUAL_FOAM_SYSTEM,
		FOAM=@FOAM,
		INERT_GAS_SYSTEM=@INERT_GAS_SYSTEM,
		MANUAL_INERT_GAS_SYSTEM=@MANUAL_INERT_GAS_SYSTEM,
		COMBAT_CARS=@COMBAT_CARS,
		CORRAL_SYSTEM =@CORRAL_SYSTEM ,
		ALARM_SYSTEM=@ALARM_SYSTEM,
		FREE_HYDRANT=@FREE_HYDRANT,
		SPRINKLERS =@SPRINKLERS ,
		SPRINKLERS_CLASSIFICATION=@SPRINKLERS_CLASSIFICATION,
		FIRE_FIGHTER=@FIRE_FIGHTER,
		QUESTIION_POINTS=@QUESTIION_POINTS,
		LAST_UPDATED_DATETIME=@LAST_UPDATED_DATETIME,
		MODIFIED_BY=@MODIFIED_BY ,
		LOCATION_ID =@LOCATION_ID 
	WHERE 
		
			 PROTECTIVE_DEVICE_ID=@PROTECTIVE_DEVICE_ID
END
END
GO

