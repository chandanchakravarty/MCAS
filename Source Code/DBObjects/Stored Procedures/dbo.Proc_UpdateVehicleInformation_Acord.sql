IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdateVehicleInformation_Acord]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdateVehicleInformation_Acord]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------            
Proc Name       : dbo.Proc_UpdateVehicleInformation            
Created by      : Nidhi            
Date            : 5/4/2005            
Purpose         : To update the record in APP_VEHICLE table            
Modified  by     : Sumit Chhabra
Date            : 29/12/2005            
Purpose         : modified the field anti_lck_brakes to anti_lock_brakes
Revison History :            
Used In         :                
------------------------------------------------------------            
Date     Review By          Comments            
------   ------------       -------------------------*/            
CREATE  PROC Dbo.Proc_UpdateVehicleInformation_Acord            
(            
 @CUSTOMER_ID    int,            
 @APP_ID          int,            
 @APP_VERSION_ID  int,            
 @VEHICLE_ID     smallint,            
 @INSURED_VEH_NUMBER  int,            
 @VEHICLE_YEAR int,            
 @MAKE nvarchar(150),            
 @MODEL  nvarchar(150),            
 @VIN  nvarchar(150),            
 @BODY_TYPE  nvarchar(150),            
 @GRG_ADD1  nvarchar(70),            
 @GRG_ADD2  nvarchar(70),            
 @GRG_CITY  nvarchar(80),            
 @GRG_COUNTRY      nvarchar(10),            
 @GRG_STATE  nvarchar(10),            
 @GRG_ZIP  nvarchar(20),            
 @REGISTERED_STATE  nvarchar(20),            
 @TERRITORY  nvarchar(20),            
 @CLASS  nvarchar(150),            
 @REGN_PLATE_NUMBER nvarchar(150),            
 @ST_AMT_TYPE   nvarchar(10),            
 @VEHICLE_TYPE int,            
 @VEHICLE_TYPE_CODE NVarChar(10),            
 @AMOUNT decimal=null,            
 @SYMBOL int=null,            
 @VEHICLE_AGE decimal=null,            
 @VEHICLE_CC int =0,            
 @MOTORCYCLE_TYPE int =0,            
 @IS_ACTIVE  nchar(1),            
 @MODIFIED_BY     int,            
 @LAST_UPDATED_DATETIME  datetime,            
 @ANTI_LOCK_BRAKES NVARCHAR(1)=null,            
 @PASSIVE_SEAT_BELT NVarChar(5),            
 @AIR_BAG NVarChar(5),            
 @VEHICLE_USE NVarChar(5),            
 @PURCHASE_DATE DateTime,          
  @ANNUAL_MILEAGE Decimal(18,2)  ,    
  @USE_VEHICLE_CODE VarChar(20)  ,  
 @MULTI_CAR NvarChar(5)  
             
)            
AS            
BEGIN            
 DECLARE @VEHTYPE_ID Int            
 -- Get the mapping unique id for the ANTI_LOCK_BRAKES            
             
 declare @LookUpUniqueID nvarchar(10)              
 DECLARE @SEAT_BELT_ID Int            
 DECLARE @AIR_BAG_ID Int            
 DECLARE @VEHICLE_USE_ID Int            
 DECLARE @USE_VEHICLE_CODE_ID Int    
 DECLARE @CLASS_ID Int    
    
 if (@ANTI_LOCK_BRAKES is not null)            
 begin            
  SELECT   @LookUpUniqueID =  MNT_LOOKUP_VALUES.LOOKUP_UNIQUE_ID            
  FROM  MNT_LOOKUP_TABLES INNER JOIN            
        MNT_LOOKUP_VALUES ON MNT_LOOKUP_TABLES.LOOKUP_ID = MNT_LOOKUP_VALUES.LOOKUP_ID            
  WHERE MNT_LOOKUP_TABLES.LOOKUP_NAME ='%ALB'             
  AND MNT_LOOKUP_VALUES.LOOKUP_VALUE_CODE = @ANTI_LOCK_BRAKES            
 end            
 else            
 set @LookUpUniqueID = @ANTI_LOCK_BRAKES             
             
            
 IF ( @VEHICLE_TYPE_CODE IS NOT NULL )            
 BEGIN            
  SELECT @VEHTYPE_ID = MLV.LOOKUP_UNIQUE_ID            
   FROM MNT_LOOKUP_VALUES MLV            
   INNER JOIN MNT_LOOKUP_TABLES MLT ON             
    MLV.LOOKUP_ID = MLT.LOOKUP_ID            
   WHERE MLT.LOOKUP_NAME = 'VEHTYP' AND            
         MLV.LOOKUP_VALUE_CODE = @VEHICLE_TYPE_CODE             
 END            
             
 IF ( @PASSIVE_SEAT_BELT IS NOT NULL )            
 BEGIN            
  EXECUTE @SEAT_BELT_ID = Proc_GetLookupID 'PRTCD',@PASSIVE_SEAT_BELT            
             
  IF ( @SEAT_BELT_ID = 0 )            
  BEGIN            
   SET @SEAT_BELT_ID= NULL            
  END             
 END            
             
 IF ( @AIR_BAG_ID IS NOT NULL )            
 BEGIN            
  EXECUTE @AIR_BAG_ID = Proc_GetLookupID '%AIRB',@AIR_BAG            
             
  IF ( @AIR_BAG_ID = 0 )            
  BEGIN            
   SET @AIR_BAG_ID= NULL            
  END             
 END            
             
 IF ( @VEHICLE_USE IS NOT NULL )         
 BEGIN            
  EXECUTE @VEHICLE_USE_ID = Proc_GetLookupID 'USECD',@VEHICLE_USE            
             
  IF ( @VEHICLE_USE_ID = 0 )            
  BEGIN            
   SET @VEHICLE_USE_ID= NULL            
  END             
 END            
     
--Commercial or Personal    
EXECUTE @USE_VEHICLE_CODE_ID = Proc_GetLookupID_FROM_DESC 'VHUCP',@USE_VEHICLE_CODE           
    
--Class of Vehicle-------------    
IF ( @USE_VEHICLE_CODE = 'Commercial')    
BEGIN    
    
    
EXECUTE @CLASS_ID = Proc_GetLookupID_FROM_DESC 'VHCLSC',@CLASS           
    
END    
    
IF ( @USE_VEHICLE_CODE = 'Personal')    
BEGIN    
    
EXECUTE @CLASS_ID = Proc_GetLookupID_FROM_DESC 'VHCLSP',@CLASS          
    
END    
-----------------------    
    
--Get Customer address as Garaging address---    
EXECUTE Proc_GetCUSTOMER_ADDRESS @CUSTOMER_ID,    
     @GRG_ADD1 OUTPUT,    
     @GRG_ADD2 OUTPUT,    
     @GRG_CITY OUTPUT,    
     @GRG_STATE OUTPUT,    
     @GRG_ZIP OUTPUT    
--------------------    
    
    
 UPDATE APP_VEHICLES            
 SET               
  INSURED_VEH_NUMBER=@INSURED_VEH_NUMBER,            
  VEHICLE_YEAR=@VEHICLE_YEAR,            
  MAKE=@MAKE,            
  MODEL=@MODEL,            
  VIN=@VIN,            
  BODY_TYPE=@BODY_TYPE,            
  GRG_ADD1=@GRG_ADD1,            
  GRG_ADD2=@GRG_ADD2,            
  GRG_CITY=@GRG_CITY,         
  GRG_COUNTRY=@GRG_COUNTRY,            
  GRG_STATE=@GRG_STATE,            
  GRG_ZIP=@GRG_ZIP,            
  REGISTERED_STATE=@REGISTERED_STATE,            
  TERRITORY=@TERRITORY,            
  CLASS=@CLASS,            
  REGN_PLATE_NUMBER=@REGN_PLATE_NUMBER,            
  ST_AMT_TYPE=@ST_AMT_TYPE,            
  VEHICLE_TYPE = @VEHTYPE_ID,            
  AMOUNT=@AMOUNT,            
  SYMBOL=@SYMBOL,            
  VEHICLE_AGE=@VEHICLE_AGE,            
  VEHICLE_CC=@VEHICLE_CC,            
  MOTORCYCLE_TYPE=@MOTORCYCLE_TYPE,            
  IS_ACTIVE  =@IS_ACTIVE,            
  MODIFIED_BY  =@MODIFIED_BY,            
  LAST_UPDATED_DATETIME =@LAST_UPDATED_DATETIME,            
  ANTI_LOCK_BRAKES = @LookUpUniqueID,            
  PASSIVE_SEAT_BELT = @SEAT_BELT_ID,            
  AIR_BAG = @AIR_BAG_ID ,            
  PURCHASE_DATE = @PURCHASE_DATE,            
  VEHICLE_USE= @VEHICLE_USE,          
  ANNUAL_MILEAGE =   @ANNUAL_MILEAGE,    
  USE_VEHICLE = @USE_VEHICLE_CODE_ID,  
 MULTI_CAR = @MULTI_CAR      
 WHERE            
  CUSTOMER_ID  =@CUSTOMER_ID AND            
  APP_ID   =@APP_ID AND            
  APP_VERSION_ID =@APP_VERSION_ID AND            
  VEHICLE_ID  =@VEHICLE_ID            
END      
      
    
  



GO

