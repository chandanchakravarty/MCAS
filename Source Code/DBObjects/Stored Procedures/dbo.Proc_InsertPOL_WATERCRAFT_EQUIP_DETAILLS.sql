IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertPOL_WATERCRAFT_EQUIP_DETAILLS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertPOL_WATERCRAFT_EQUIP_DETAILLS]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



/****** Object:  Stored Procedure dbo.Proc_InsertPOL_WATERCRAFT_EQUIP_DETAILLS    Script Date: 5/15/2006 6:45:49 PM ******/
/*----------------------------------------------------------    
Proc Name       : dbo.Proc_InsertPOL_WATERCRAFT_EQUIP_DETAILLS  
Created by      : Vijay Arora  
Date            : 30-11-2005  
Purpose        :INERTING RECORD INTO POL_WATERCRAFT_EQUIP_DETAILLS    
Revison History :    
Used In        : Wolverine    
------------------------------------------------------------    
Date     Review By          Comments    
------   ------------       -------------------------*/    
CREATE   PROC Dbo.Proc_InsertPOL_WATERCRAFT_EQUIP_DETAILLS    
(    
@CUSTOMER_ID     int,    
@POLICY_ID     int,    
@POLICY_VERSION_ID     smallint,    
@EQUIP_ID     smallint output,    
@EQUIP_NO     int,    
@EQUIP_TYPE     int,    
@SHIP_TO_SHORE     int,    
@YEAR     int,    
@MAKE     nvarchar(130),    
@MODEL     nvarchar(130),    
@SERIAL_NO     nvarchar(130),    
@INSURED_VALUE     decimal(9,2),    
@ASSOCIATED_BOAT     smallint,    
@IS_ACTIVE     nchar(2),    
@CREATED_BY     int,    
@EQUIP_AMOUNT  decimal(9),
@CREATED_DATETIME     datetime,
@EQUIPMENT_TYPE integer = null,
@OTHER_DESCRIPTION nvarchar(200) = null    
)    
AS    
BEGIN    
    
    
Declare @Count int    
 Set @Count= (SELECT count(EQUIP_NO) FROM POL_WATERCRAFT_EQUIP_DETAILLS    
  WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID   
  and  EQUIP_NO=@EQUIP_NO)    
    
    
 if (@Count=0)    
   BEGIN    
    
   DECLARE @EQUIPID INT    
   SELECT @EQUIPID=ISNULL(MAX(EQUIP_ID),0)+1 FROM POL_WATERCRAFT_EQUIP_DETAILLS WHERE CUSTOMER_ID=@CUSTOMER_ID    
   AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID   
       
   INSERT INTO POL_WATERCRAFT_EQUIP_DETAILLS    
   (    
    CUSTOMER_ID,    
    POLICY_ID,    
    POLICY_VERSION_ID,    
    EQUIP_ID,    
    EQUIP_NO,    
    EQUIP_TYPE,    
    SHIP_TO_SHORE,    
    YEAR,    
    MAKE,    
    MODEL,    
    SERIAL_NO,    
    INSURED_VALUE,    
    ASSOCIATED_BOAT,    
    IS_ACTIVE,    
    CREATED_BY,
    EQUIP_AMOUNT,
    CREATED_DATETIME,
   EQUIPMENT_TYPE,
   OTHER_DESCRIPTION
   )    
   VALUES    
   (    
    @CUSTOMER_ID,    
    @POLICY_ID,    
    @POLICY_VERSION_ID,    
    @EQUIPID,    
    @EQUIP_NO,    
    @EQUIP_TYPE,    
    @SHIP_TO_SHORE,    
    @YEAR,    
    @MAKE,    
    @MODEL,    
    @SERIAL_NO,    
    @INSURED_VALUE,    
    @ASSOCIATED_BOAT,    
    @IS_ACTIVE,    
    @CREATED_BY,
    @EQUIP_AMOUNT,
    @CREATED_DATETIME,    
    @EQUIPMENT_TYPE,
    @OTHER_DESCRIPTION
   )    
     
    
   SET @EQUIP_ID=@EQUIPID  
  END    
 ELSE    
  BEGIN    
   SET @EQUIP_ID= -1    
     
  END    
 END    
    
  




GO

