IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdatePOL_WATERCRAFT_EQUIP_DETAILLS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdatePOL_WATERCRAFT_EQUIP_DETAILLS]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



/****** Object:  Stored Procedure dbo.Proc_UpdatePOL_WATERCRAFT_EQUIP_DETAILLS    Script Date: 5/15/2006 6:24:33 PM ******/
/*----------------------------------------------------------    
Proc Name       : dbo.Proc_UpdatePOL_WATERCRAFT_EQUIP_DETAILLS    
Created by      : Vijay Arora  
Date            : 30-11-2005  
Purpose         :UPDATING RECORD INTO POL_WATERCRAFT_EQUIP_DETAILLS      
Revison History :    
Used In         : Wolverine    
------------------------------------------------------------    
Date     Review By          Comments    
------   ------------       -------------------------*/    
CREATE   PROC Dbo.Proc_UpdatePOL_WATERCRAFT_EQUIP_DETAILLS    
(    
@CUSTOMER_ID     int,    
@POLICY_ID     int,    
@POLICY_VERSION_ID     smallint,    
@EQUIP_ID     smallint,    
@EQUIP_NO     int,    
@EQUIP_TYPE     int,    
@SHIP_TO_SHORE     int,    
@YEAR     int,    
@MAKE     nvarchar(130),    
@MODEL     nvarchar(130),    
@SERIAL_NO     nvarchar(130),    
@INSURED_VALUE     decimal(9,2),    
@ASSOCIATED_BOAT     smallint,    
@MODIFIED_BY     int,
@EQUIP_AMOUNT  decimal(9),
@LAST_UPDATED_DATETIME     datetime,
@EQUIPMENT_TYPE integer = null,
@OTHER_DESCRIPTION nvarchar(200) = null

)    
AS    
BEGIN    
Declare @Count int    
 Set @Count= (SELECT count(EQUIP_NO) FROM POL_WATERCRAFT_EQUIP_DETAILLS    
  WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID and EQUIP_NO=@EQUIP_NO and EQUIP_ID<>@EQUIP_ID)    
    
    
if (@Count=0)    
  BEGIN    
  UPDATE  POL_WATERCRAFT_EQUIP_DETAILLS    
  SET    
   EQUIP_NO=@EQUIP_NO,    
   EQUIP_TYPE=@EQUIP_TYPE,    
   SHIP_TO_SHORE=@SHIP_TO_SHORE,    
   YEAR=@YEAR,    
   MAKE=@MAKE,    
   MODEL=@MODEL,    
   SERIAL_NO=@SERIAL_NO,    
   INSURED_VALUE=@INSURED_VALUE,    
   ASSOCIATED_BOAT=@ASSOCIATED_BOAT,    
   MODIFIED_BY=@MODIFIED_BY,    
   LAST_UPDATED_DATETIME=@LAST_UPDATED_DATETIME    ,
   EQUIP_AMOUNT = @EQUIP_AMOUNT,
   EQUIPMENT_TYPE = @EQUIPMENT_TYPE,
   OTHER_DESCRIPTION = @OTHER_DESCRIPTION

  WHERE    
   CUSTOMER_ID=@CUSTOMER_ID AND    
   POLICY_ID = @POLICY_ID AND    
   POLICY_VERSION_ID=@POLICY_VERSION_ID AND    
   EQUIP_ID=@EQUIP_ID    
 END    
ELSE    
 BEGIN    
  return -1    
 END    
END    
    
    
    
  




GO

