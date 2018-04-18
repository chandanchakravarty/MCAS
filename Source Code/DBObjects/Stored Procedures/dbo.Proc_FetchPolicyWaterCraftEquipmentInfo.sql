IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_FetchPolicyWaterCraftEquipmentInfo]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_FetchPolicyWaterCraftEquipmentInfo]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



/****** Object:  Stored Procedure dbo.Proc_FetchPolicyWaterCraftEquipmentInfo    Script Date: 5/15/2006 5:54:45 PM ******/
/*----------------------------------------------------------    
Proc Name       : dbo.Proc_FetchPolicyWaterCraftEquipmentInfo    
Created by      : Vijay Arora  
Date            : 30-11-2005  
Purpose        : retrieving data from POL_WATERCRAFT_EQUIP_DETAILS    
Revison History :    
Used In        : Wolverine    
------------------------------------------------------------    
Date     Review By          Comments    
------   ------------       -------------------------*/    
CREATE   PROC Dbo.Proc_FetchPolicyWaterCraftEquipmentInfo    
@CUSTOMER_ID INT,    
@POLICY_ID INT,    
@POLICY_VERSION_ID INT,    
@EQUIP_ID INT    
AS    
    
BEGIN    
SELECT    
EQUIP_ID,    
EQUIP_NO,    
EQUIP_TYPE,    
SHIP_TO_SHORE,    
[YEAR],    
MAKE,    
MODEL,    
SERIAL_NO,    
FLOOR(INSURED_VALUE) INSURED_VALUE,    
ASSOCIATED_BOAT,
EQUIP_AMOUNT, 
IS_ACTIVE , OTHER_DESCRIPTION  , EQUIPMENT_TYPE	   
FROM POL_WATERCRAFT_EQUIP_DETAILLS    
WHERE CUSTOMER_ID=@CUSTOMER_ID   
AND POLICY_ID=@POLICY_ID   
AND POLICY_VERSION_ID=@POLICY_VERSION_ID    
AND EQUIP_ID=@EQUIP_ID    
END    
    
    





GO

