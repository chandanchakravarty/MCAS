IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertAPP_WATERCRAFT_EQUIP_DETAILS_ACORD]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertAPP_WATERCRAFT_EQUIP_DETAILS_ACORD]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                                                                    
Proc Name       : dbo.Proc_InsertAPP_WATERCRAFT_EQUIP_DETAILS_ACORD                                                                
Created by      : 
Date            : 
Purpose         : 
Revison History : 

Modified By 	: Ravindra
Modified On 	: 08-24-2006
Purpose 	: EQUIP_TYPE was defaulted to -1 , need to be save value imported from make app                                                                    
Used In         : Wolverine                
  
------------------------------------------------------------        */
--Drop proc  Dbo.Proc_InsertAPP_WATERCRAFT_EQUIP_DETAILS_ACORD        
CREATE PROC Dbo.Proc_InsertAPP_WATERCRAFT_EQUIP_DETAILS_ACORD        
(        
	@CUSTOMER_ID     int,        
	@APP_ID     int,        
	@APP_VERSION_ID     smallint,        
	@EQUIP_ID     smallint output,        
	@EQUIP_NO     int,        
	@EQUIP_TYPE     int,        
	@SHIP_TO_SHORE     int,        
	@YEAR     int,        
	--@MAKE     nvarchar(130),        
	--@MODEL     nvarchar(130),        
	--@SERIAL_NO     nvarchar(130),  
	@MAKE     nvarchar(65),        
	@MODEL     nvarchar(65),        
	@SERIAL_NO     nvarchar(65),    
	@INSURED_VALUE     decimal(9,2),        
	@ASSOCIATED_BOAT     smallint,        
	@IS_ACTIVE     nchar(2),        
	@CREATED_BY     int,        
	@CREATED_DATETIME     datetime  ,      
	@EQUIP_AMOUNT decimal(10)  =null,
	@OTHER_DESCRIPTION nvarchar(200),
	@EQUIPMENT_TYPE int    
)        
AS        
BEGIN        
        
        
Declare @Count int        
 Set @Count= (SELECT count(EQUIP_NO) FROM APP_WATERCRAFT_EQUIP_DETAILLS        
  WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID and  EQUIP_NO=@EQUIP_NO)        
        
        
 if (@Count=0)        
   BEGIN        
        
   DECLARE @EQUIPID INT        
   SELECT @EQUIPID=ISNULL(MAX(EQUIP_ID),0)+1 FROM APP_WATERCRAFT_EQUIP_DETAILLS WHERE CUSTOMER_ID=@CUSTOMER_ID        
   AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID        
           
   INSERT INTO APP_WATERCRAFT_EQUIP_DETAILLS        
   (        
    CUSTOMER_ID,        
    APP_ID,        
    APP_VERSION_ID,        
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
    CREATED_DATETIME  ,      
    EQUIP_AMOUNT,
    OTHER_DESCRIPTION,
    EQUIPMENT_TYPE

      
   )        
   VALUES        
   (        
    @CUSTOMER_ID,        
    @APP_ID,        
    @APP_VERSION_ID,        
    @EQUIPID,        
    @EQUIPID,--@EQUIP_NO,        
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
    @CREATED_DATETIME  ,      
    @EQUIP_AMOUNT,
    @OTHER_DESCRIPTION,
    @EQUIPMENT_TYPE      
   )        
   SELECT @EQUIPID=ISNULL(MAX(EQUIP_ID),-1) FROM APP_WATERCRAFT_EQUIP_DETAILLS WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID        
   SET @EQUIP_ID=@EQUIPID          
  END        
 ELSE        
  BEGIN        
   SET @EQUIP_ID= -1        
         
  END        
 END        
      
    
  
  









GO

