IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetPDFAuto_MiscEquipment]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetPDFAuto_MiscEquipment]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------
Proc Name          : Dbo.Proc_GetPDFAuto_MiscEquipment
Created by         : Anurag verma
Date               : 26-June-2006
Purpose            : 
Revison History    :
Used In            : Wolverine  
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
--DROP PROCEDURE Proc_GetPDFAuto_MiscEquipment
CREATE  PROCEDURE dbo.Proc_GetPDFAuto_MiscEquipment
(
	@CUSTOMERID 		int,
	@POLID 	             	int,
	@VERSIONID 		int,
	@CALLEDFROM		VARCHAR(20),
	@VEHICLEID		int
	
)
AS
BEGIN
	IF (@CALLEDFROM='APPLICATION')
	BEGIN
		IF(@VEHICLEID=0)
			BEGIN
				SELECT isnull(ITEM_DESCRIPTION,'') ITEM_DESCRIPTION,isnull(ITEM_VALUE,0) ITEM_VALUE	,VEHICLE_ID		
				FROM APP_MISCELLANEOUS_EQUIPMENT_VALUES AV  with(nolock) 
				WHERE AV.CUSTOMER_ID=@CUSTOMERID AND AV.APP_ID=@POLID AND AV.APP_VERSION_ID=@VERSIONID --AND vehicle_id=@VEHICLEID
				and item_description is not null and is_active='Y'
			END
		ELSE
			BEGIN
				SELECT isnull(ITEM_DESCRIPTION,'') ITEM_DESCRIPTION,isnull(ITEM_VALUE,0) ITEM_VALUE	,VEHICLE_ID		
				FROM APP_MISCELLANEOUS_EQUIPMENT_VALUES AV  with(nolock) 
				WHERE AV.CUSTOMER_ID=@CUSTOMERID AND AV.APP_ID=@POLID AND AV.APP_VERSION_ID=@VERSIONID AND vehicle_id=@VEHICLEID
				and item_description is not null and is_active='Y'
			END
	END
	ELSE IF (@CALLEDFROM='POLICY')
	BEGIN	
		IF(@VEHICLEID=0)
			BEGIN	
				SELECT isnull(ITEM_DESCRIPTION,'') ITEM_DESCRIPTION,isnull(ITEM_VALUE,0) ITEM_VALUE,VEHICLE_ID
				FROM POL_MISCELLANEOUS_EQUIPMENT_VALUES PV  with(nolock) 
				WHERE PV.CUSTOMER_ID=@CUSTOMERID AND PV.POLICY_ID=@POLID AND PV.POLICY_VERSION_ID=@VERSIONID --and vehicle_id=@VEHICLEID
				and item_description is not null and is_active='Y'
			END
		ELSE
			BEGIN
				SELECT isnull(ITEM_DESCRIPTION,'') ITEM_DESCRIPTION,isnull(ITEM_VALUE,0) ITEM_VALUE,VEHICLE_ID
				FROM POL_MISCELLANEOUS_EQUIPMENT_VALUES PV  with(nolock) 
				WHERE PV.CUSTOMER_ID=@CUSTOMERID AND PV.POLICY_ID=@POLID AND PV.POLICY_VERSION_ID=@VERSIONID and vehicle_id=@VEHICLEID
				and item_description is not null and is_active='Y'
			END
	END
END














GO

