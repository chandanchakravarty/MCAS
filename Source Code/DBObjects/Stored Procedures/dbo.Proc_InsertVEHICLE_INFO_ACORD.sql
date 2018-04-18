IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertVEHICLE_INFO_ACORD]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertVEHICLE_INFO_ACORD]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- drop proc dbo.Proc_InsertVEHICLE_INFO_ACORD
create PROC [dbo].[Proc_InsertVEHICLE_INFO_ACORD]                                                    
(                                                    
                                                     
  @CUSTOMER_ID     int,                                                    
  @APP_ID     int ,                                                    
  @APP_VERSION_ID     smallint,                                                    
  @VEHICLE_ID     smallint output,                                                    
  @INSURED_VEH_NUMBER     smallint,                                                    
  @VEHICLE_YEAR     int,                                                    
  --@MAKE     nvarchar(150),            
  @MAKE     nvarchar(75),                                                     
  --@MODEL     nvarchar(150),            
  @MODEL     nvarchar(75),                                                     
  --@VIN     nvarchar(150),            
  @VIN     nvarchar(75),                                                    
  --@BODY_TYPE     nvarchar(150),            
  @BODY_TYPE     nvarchar(75),               
  @GRG_ADD1     nvarchar(70),                                                    
  @GRG_ADD2     nvarchar(70),                                                    
  --@GRG_CITY     nvarchar(80),            
  @GRG_CITY     nvarchar(40),                                                    
  --@GRG_COUNTRY     nvarchar(10),            
  @GRG_COUNTRY     nvarchar(5),                                                     
  --@GRG_STATE     nvarchar(10),            
  @GRG_STATE     nvarchar(5),                                                    
  --@GRG_ZIP     nvarchar(20),                                                    
  @GRG_ZIP     varchar(11),             
  --@REGISTERED_STATE     nvarchar(10),            
  @REGISTERED_STATE     nvarchar(5),                                                     
  --@TERRITORY     nvarchar(10),            
  @TERRITORY     nvarchar(5),                                                    
  --@CLASS     nvarchar(150),            
  @CLASS     nvarchar(75),                                                    
  --@ANTI_LCK_BRAKES NVARCHAR(5)=null,                                                    
  @ANTI_LOCK_BRAKES NVARCHAR(5)=null,                                                    
  --@REGN_PLATE_NUMBER     nvarchar(100),            
  @REGN_PLATE_NUMBER     nvarchar(50),                                                     
  @MOTORCYCLE_TYPE int,                                                      
  @MOTORCYCLE_TYPE_CODE VarChar(20),                                                      
  @ST_AMT_TYPE     nvarchar(10),                                                    
  @VEHICLE_TYPE int,                                                    
  @VEHICLE_TYPE_CODE NVarChar(10),                                                    
  @AMOUNT     decimal =null,                                                    
  @SYMBOL     int =null,                                                    
  @VEHICLE_AGE     decimal(9)=null,                                                    
  @CREATED_BY     int,                                                    
  @CREATED_DATETIME     datetime,                                                    
  @PASSIVE_SEAT_BELT NVarChar(5),                                                    
  @AIR_BAG NVarChar(5),                                       
  @PURCHASE_DATE DateTime,                                                    
  @VEHICLE_USE NVarChar(5),                                                
  @ANNUAL_MILEAGE Decimal(18,2),                                    
  @VEHICLE_CC int ,                                  
  @USE_VEHICLE_CODE VarChar(20) ,                              
  @MULTI_CAR NVarChar(5) ,                   
  @APP_VEHICLE_CLASS_CODE VarChar(50),      
  @MILES_TO_WORK nvarchar(5),          
  @CAR_POOL  NVARCHAR(5)=null,                   
  @SNOWPLOW_CODE  NVARCHAR(5)=null,          
  @MISC_AMT     decimal(9)=null,          
  @MISC_DESC_AMT   varchar(100)=null,      
  @RADIUS_OF_USE int= null,       
  @CLASS_DESCRIPTION    varchar(10)=null ,  
  @CLASS_DRIVERID int = null,  
  @IS_SUSPENDED int = null,
  @COMPRH_ONLY int = null
       
)                                                    
AS                                                    
BEGIN                  
                              
 -- Get the mapping unique id for the anti_lck_brakes                                                    
 declare @LookUpUniqueID nvarchar(10)                          
 DECLARE @VEHTYPE_ID Int                                                    
 DECLARE @INSURED_VEH_NUMBER1 Int                                                     
 DECLARE @SEAT_BELT_ID Int                                                     
 DECLARE @AIR_BAG_ID Int                                                    
 DECLARE @VEHICLE_USE_ID Int                                                   
 DECLARE @GRG_STATE_ID Int                                         
 DECLARE @MOTORCYCLE_TYPE_ID Int                                      
 DECLARE @USE_VEHICLE_CODE_ID Int                                  
 DECLARE @CLASS_PER_ID Int                                  
 DECLARE @CLASS_COM_ID Int                                   
 DECLARE @VEHICLE_TYPE_PER Int                                  
 DECLARE @VEHICLE_TYPE_Com Int                                  
 DECLARE @MOT_CLASS Int                    
 DECLARE @SNOW_PLOW_CODE Int                                  
 DECLARE @CLASS_DESCRIPTION_ID varchar(10)              
 DECLARE @APP_EFF_DATE NVARCHAR(50)   
 DECLARE @STATEID  INT                 
 DECLARE @LOB_ID INT
--EXECUTE @GRG_STATE_ID = Proc_GetSTATE_ID 1,@GRG_STATE                                                  
                                                  
 select @VEHICLE_ID=isnull(max(VEHICLE_ID),0)+1                                                     
 from APP_VEHICLES                                                     
 where CUSTOMER_ID=@CUSTOMER_ID and                                       
 APP_VERSION_ID=@APP_VERSION_ID and APP_ID=@APP_ID                                                    
                                                     
 select @INSURED_VEH_NUMBER1 = isnull(max(INSURED_VEH_NUMBER),0)+1                                                     
 from APP_VEHICLES                                                     
 where CUSTOMER_ID=@CUSTOMER_ID and                                
 APP_VERSION_ID=@APP_VERSION_ID and APP_ID=@APP_ID                            
                         
 SELECT @APP_EFF_DATE = CONVERT(NVARCHAR(50),ISNULL(APP_EFFECTIVE_DATE,'')),  
  @STATEID= STATE_ID ,
  @LOB_ID = ISNULL(APP_LOB,0)                               
  from APP_LIST                                                     
 where CUSTOMER_ID=@CUSTOMER_ID and                                
 APP_VERSION_ID=@APP_VERSION_ID and APP_ID=@APP_ID                                                   
 /*                                                   
 if (@ANTI_LCK_BRAKES is not null)                                                    
  begin                                                
   SELECT   @LookUpUniqueID =  MNT_LOOKUP_VALUES.LOOKUP_UNIQUE_ID                                                    
   FROM         MNT_LOOKUP_TABLES INNER JOIN                                                    
                     MNT_LOOKUP_VALUES ON MNT_LOOKUP_TABLES.LOOKUP_ID = MNT_LOOKUP_VALUES.LOOKUP_ID                                                    
   WHERE MNT_LOOKUP_TABLES.LOOKUP_NAME ='%ALB'                                                     
 AND MNT_LOOKUP_VALUES.LOOKUP_VALUE_CODE = @ANTI_LCK_BRAKES                                                    
  end                                                    
 else                                                    
  set @LookUpUniqueID = @ANTI_LCK_BRAKES                            
  */                          
                                     
 --VHTYPP                                      
                                                    
 IF ( @VEHICLE_TYPE_CODE IS NOT NULL )                                                    
 BEGIN                    
                                 
  EXECUTE @VEHTYPE_ID = Proc_GetLookupID_FROM_DESC 'VHTYPP',@VEHICLE_TYPE_CODE                                                    
 END                                                    
                                       
IF ( @MOTORCYCLE_TYPE_CODE IS NOT NULL )           
 BEGIN                                                    
                                                  
  EXECUTE @MOTORCYCLE_TYPE_ID = Proc_GetLookupID 'CYCTY',@MOTORCYCLE_TYPE_CODE                                                    
 END                                      
             
--commented by Anurag verma on 18/09/2006                                        
/* IF ( @PASSIVE_SEAT_BELT IS NOT NULL )                                                    
 BEGIN                                                    
  EXECUTE @SEAT_BELT_ID = Proc_GetLookupID 'PRTCD',@PASSIVE_SEAT_BELT                                                    
                                                     
  IF ( @SEAT_BELT_ID = 0 )                                                    
  BEGIN                                                    
   SET @SEAT_BELT_ID= NULL                                 
  END                                                     
 END                                                    
 */                          
                                                  
 IF ( @AIR_BAG IS NOT NULL )                                                    
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
      
 select @CLASS_DESCRIPTION_ID = convert(varchar(10),LOOKUP_UNIQUE_ID) FROM MNT_LOOKUP_VALUES WHERE  lookup_value_code = @CLASS_DESCRIPTION      
                             
BEGIN                                  
          
EXECUTE @CLASS_COM_ID = Proc_GetLookupID 'VHCLSC',@CLASS                                         
--ADDED BY RP -- TEMP IF NULL IS RETUREND THEN DEFAULT CA5          
IF (@CLASS_COM_ID = NULL OR @CLASS_COM_ID = '' OR @CLASS_COM_ID = 0  OR @CLASS_COM_ID = '0')          
   set @CLASS_COM_ID = 11365          
END           
           
END             
  IF(DATEDIFF(DAY,'05/31/2008',@APP_EFF_DATE) >0 AND @STATEID=14)  
 BEGIN  
  SET @APP_EFF_DATE=NULL  
 END  
  
IF ( @USE_VEHICLE_CODE = 'Personal')                                  
BEGIN                                  
                                  
EXECUTE @CLASS_PER_ID = Proc_GetClassLookupID 'VHCLSP',@CLASS,@APP_EFF_DATE,@STATEID         
          
--ADDED BY RP -- TEMP IF NULL IS RETUREND THEN DEFAULT PA5          
IF (@CLASS_PER_ID = NULL OR @CLASS_PER_ID = '' OR @CLASS_PER_ID = 0 )          
   set @CLASS_PER_ID = 11358          
END                                  
-----------------------                           
                                  
--Type of vehicle-------------                                  
IF ( @USE_VEHICLE_CODE = 'Commercial')                                  
BEGIN                                                          
                                  
EXECUTE @VEHICLE_TYPE_COM = Proc_GetLookupID 'VHTYPC',@VEHICLE_TYPE_CODE                                   
                                  
END                                  
                                  
IF ( @USE_VEHICLE_CODE = 'Personal')                                  
BEGIN                                  
                                  
EXECUTE @VEHICLE_TYPE_PER = Proc_GetLookupID 'VHTYPP',@VEHICLE_TYPE_CODE                                        
             
END                                  
--------                             
          
              
                      
--Motorcycle Class-----------                    
EXEC @MOT_CLASS = Proc_GetLookupID 'MCCLAS',@APP_VEHICLE_CLASS_CODE                    
-----------------------------------                    
                                
--Garage Territory-------                                
IF ( @TERRITORY IS NULL OR  @TERRITORY = '' )                            
BEGIN                                
 EXEC @TERRITORY = Proc_GetTerritoryForZip @GRG_ZIP,                                
        @CUSTOMER_ID,                                                    
        @APP_ID ,                                                 
        @APP_VERSION_ID                                  
END                                
                                
---------------                         
                           
-- Stated Amount Type -- must be actual cash value by default          
          
set @ST_AMT_TYPE = '8707'         
  
if(@CLASS_DRIVERID = 0)     
begin     
set @CLASS_DRIVERID = null  
end  
----------------------------          
                                
INSERT INTO APP_VEHICLES                                                     
(                                                    
  CUSTOMER_ID,                                                    
  APP_ID,                                                    
  APP_VERSION_ID,                                                    
  VEHICLE_ID,                                                    
  INSURED_VEH_NUMBER,                                                    
  VEHICLE_YEAR,                                                    
  MAKE,                                                    
  MODEL,                                                   
  VIN,                                                    
  BODY_TYPE,                                                    
  GRG_ADD1,                                                    
  GRG_ADD2,                                                    
  GRG_CITY,                    
  GRG_COUNTRY,                                                    
  GRG_STATE,                                                    
  GRG_ZIP,                                                    
  REGISTERED_STATE,                                                    
  TERRITORY,                                                    
  CLASS,                                                    
  ANTI_LOCK_BRAKES,        
  MOTORCYCLE_TYPE,                      
  REGN_PLATE_NUMBER,                                        
  ST_AMT_TYPE,                                                    
  VEHICLE_TYPE,                                                    
  AMOUNT,                                                    
  SYMBOL,                                                    
  VEHICLE_AGE,                
  IS_ACTIVE,                                                    
  CREATED_BY,                                                    
  CREATED_DATETIME,                                                                                     
  ANNUAL_MILEAGE,                                                    
  PASSIVE_SEAT_BELT,                                                    
  AIR_BAG,                                                    
  PURCHASE_DATE,                                                    
  VEHICLE_USE,                                              
  USE_VEHICLE ,                                    
  VEHICLE_CC ,                                  
  CLASS_PER,                                  
  CLASS_COM,                                  
  VEHICLE_TYPE_COM,                                  
  VEHICLE_TYPE_PER,                              
  MULTI_CAR ,                    
  APP_VEHICLE_CLASS ,              
  MILES_TO_WORK ,          
  CAR_POOL,          
  SNOWPLOW_CONDS,      
  RADIUS_OF_USE,            
  CLASS_DESCRIPTION,  
  CLASS_DRIVERID,  
  IS_SUSPENDED,
  IS_NEW_USED,
  COMPRH_ONLY,
	IS_UPDATED                                             
                                                  
)                                                    
VALUES                                                    
(                                                    
  @CUSTOMER_ID,                                                    
  @APP_ID,                                                    
  @APP_VERSION_ID,                                                    
  @VEHICLE_ID,                                                    
  @INSURED_VEH_NUMBER1,                                                    
  @VEHICLE_YEAR,                                                    
  @MAKE,                                 
  @MODEL,                                                    
  @VIN,                                                    
  @BODY_TYPE,                                                    
  @GRG_ADD1,                                
  @GRG_ADD2,                                    
  @GRG_CITY,                                                    
  '1',            
  @GRG_STATE,                                                    
  @GRG_ZIP,                                                    
  @REGISTERED_STATE,                                                    
  @TERRITORY,                                                    
  @CLASS,                                                    
  --@ANTI_LCK_BRAKES,                                                    
  @ANTI_LOCK_BRAKES,                                                    
  @MOTORCYCLE_TYPE_ID,                       
  @REGN_PLATE_NUMBER,                                                    
  @ST_AMT_TYPE,                                                    
  @VEHTYPE_ID,                                                    
  @AMOUNT,                                          
  @SYMBOL,                                                    
  @VEHICLE_AGE,        
  'Y',                                                    
  @CREATED_BY,                                                    
  @CREATED_DATETIME,                                                    
                                                   
  @ANNUAL_MILEAGE,                                                    
 @PASSIVE_SEAT_BELT,                                                    
  @AIR_BAG_ID,                                                    
  @PURCHASE_DATE,                                                    
  @VEHICLE_USE_ID,                                    
  @USE_VEHICLE_CODE_ID,         
  @VEHICLE_CC,                                  
  CASE WHEN @LOB_ID = 3	THEN @MOT_CLASS ELSE @CLASS_PER_ID END,                                  
  @CLASS_COM_ID ,                                  
   @VEHICLE_TYPE_COM,                                  
  @VEHICLE_TYPE_PER ,                              
  @MULTI_CAR ,                    
  @MOT_CLASS ,        
@MILES_TO_WORK,          
@CAR_POOL,            
@SNOWPLOW_CODE,      
@RADIUS_OF_USE,    
@CLASS_DESCRIPTION_ID,  
@CLASS_DRIVERID ,  
@IS_SUSPENDED,
'',
@COMPRH_ONLY,
10964
  
        
)           
        
IF(@MISC_AMT<>0.0)  
  
BEGIN                                                 
        
 INSERT INTO APP_MISCELLANEOUS_EQUIPMENT_VALUES           
 (          
   CUSTOMER_ID,                                                    
   APP_ID,                                                    
   APP_VERSION_ID,                                                    
   VEHICLE_ID,          
   ITEM_ID,          
   ITEM_VALUE,IS_ACTIVE,CREATED_BY,CREATED_DATETIME,          
   ITEM_DESCRIPTION          
  )          
 VALUES          
 (           
   @CUSTOMER_ID,                                                    
   @APP_ID,             
   @APP_VERSION_ID,                                                    
   @VEHICLE_ID,               
   1,           
   @MISC_AMT,'Y',          
   @CREATED_BY,                                                    
   @CREATED_DATETIME,          
   @MISC_DESC_AMT            
 )        
END    
exec Proc_SetAssgndDrvrType @CUSTOMER_ID,@APP_ID,@APP_VERSION_ID,@VEHICLE_ID      
  
---------------------UPDATE ZIP EXTENSION------------------  
DECLARE @EXTENSION VARCHAR(10)  
DECLARE @CUSTOMER_ZIP VARCHAR(15)  
  
SELECT   
 @CUSTOMER_ZIP = DBO.PIECE(CUSTOMER_ZIP,'-',1),  
 @EXTENSION = DBO.PIECE(CUSTOMER_ZIP,'-',2)   
 FROM   
 CLT_CUSTOMER_LIST WHERE CUSTOMER_ID = @CUSTOMER_ID  
  
  
IF (ISNULL(@EXTENSION,'') <>'')  
BEGIN  
 IF(rtrim(ltrim(@CUSTOMER_ZIP)) = rtrim(ltrim(@GRG_ZIP)))  
 BEGIN  
  UPDATE APP_VEHICLES SET GRG_ZIP = @CUSTOMER_ZIP + '-' + @EXTENSION   
  WHERE   
  CUSTOMER_ID  = @CUSTOMER_ID AND                                                     
  APP_ID   = @APP_ID AND                                                  
  APP_VERSION_ID = @APP_VERSION_ID AND    
  VEHICLE_ID  = @VEHICLE_ID  
 END   
END  
  
------------UPDATE ZIP EXTENSION END------------------    
         
        
        
      
    
  

  
  
  
  
  




GO

