IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetRatingInformationForAuto_VehicleComponent]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetRatingInformationForAuto_VehicleComponent]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE  PROC [dbo].[Proc_GetRatingInformationForAuto_VehicleComponent]
(                                                                                  
@CUSTOMERID    int,                                                                                  
@APPID    int,                                                                                  
@APPVERSIONID   int,                                                                                  
@VEHICLEID    int                                                                                   
)                                                                                  
AS                                                                                  
                                                                                  
BEGIN                                                               
                                                                              
 set quoted_identifier off                                                                                  
                      
                                                                                  
DECLARE  @VEHICLETYPEUSE nvarchar(100)                                                                                
DECLARE  @VEHICLECLASS nvarchar(100)                                         
DECLARE @VEHICLECLASS_DESC nvarchar(500)                                           
DECLARE  @VEHICLECLASSCOMPONENT1  nvarchar(10)                                                            
DECLARE  @VEHICLECLASSCOMPONENT2  nvarchar(10)                                                                                
DECLARE  @VEHICLETYPE   nvarchar(100)     
DECLARE  @VEHICLETYPEDESC  nvarchar(100)                                        
 DECLARE  @GOODSTUDENT     nvarchar(100)                                                                              
--DECLARE  @SUMOFACCIDENTPOINTS   nvarchar(100)                                                                       
--DECLARE  @SUMOFVIOLATIONPOINTS   nvarchar(100)                                                                      
DECLARE  @PREMIERDRIVER   nvarchar(100)             
DECLARE  @YEAR   nvarchar(100)                                                                                  
DECLARE  @MAKE   nvarchar(300)                                                       
DECLARE  @AGE   nvarchar(300)                      
DECLARE  @MODEL nvarchar(300)                              
DECLARE  @VIN nvarchar(100)                                                                                  
DECLARE  @SYMBOL   nvarchar(100)                                                                                  
DECLARE  @COST   nvarchar(100)                           
DECLARE  @ANNUALMILES   nvarchar(100)                                                                                  
DECLARE  @VEHICLEUSE  nvarchar(100)                                                                                  
DECLARE  @CARPOOL NVARCHAR(10)              
DECLARE  @MILESEACHWAY   nvarchar(100)                                                                      
DECLARE  @ISANTILOCKBRAKESDISCOUNTS   nvarchar(100)                                                                                  
DECLARE  @AIRBAGDISCOUNT   nvarchar(100)                                                                                  
DECLARE  @MULTICARDISCOUNT   nvarchar(100)                                                                                  
DECLARE  @INSURANCEAMOUNT   nvarchar(100)          
DECLARE  @ZIPCODEGARAGEDLOCATION   nvarchar(100)                                                                                  
DECLARE  @TERRCODEGARAGEDLOCATION   nvarchar(100)                       
DECLARE  @GARAGEDLOCATION nvarchar(100)          
DECLARE  @WEARINGSEATBELT   nvarchar(100)                                                               
DECLARE @SAFEDRIVER NVARCHAR(100)                      
declare @YEARSCONTINSUREDWITHWOLVERINE int                                                                                  
DECLARE  @TYPE   nvarchar(100)                                                                                  
DECLARE  @ISUNDERINSUREDMOTORISTS   nvarchar(100)                                                                                  
DECLARE @QUALIFIESTRAIBLAZERPROGRAM nvarchar(10)                                                                                
DECLARE @RADIUSOFUSE nvarchar(10)                                                                                    
DECLARE  @USE nvarchar(10)                                                                                
DECLARE @VEHICLEUSEDESC nvarchar(200)                                                                      
DECLARE @STATE_ID NVARCHAR(20)                                                                    
--Get The Some Filds From Driver table                                                                    
                                                                    
DECLARE    @DRIVERINCOME      nvarchar(100)                                                                                
DECLARE    @NODEPENDENT      nvarchar(100)                                                                                
DECLARE    @WAIVEWORKLOSS    nvarchar(100)                                                  
DECLARE    @MULTICARDIS     nvarchar(2)                                                                              
DECLARE    @LOBID            NVARCHAR(2)                               
DECLARE    @VEHICLECLASS_PER nvarchar(100)                               
DECLARE    @VEHICLECLASS_COM nvarchar(100)                                                                             
DECLARE    @QUOTEEFFECTIVEDATE NVARCHAR(100)                      
DECLARE    @QUOTEEFFDATE       nvarchar(20)                      
DECLARE    @STATEID    SMALLINT          
DECLARE    @ACCIDENT_NUM_YEAR   INT    
DECLARE    @VIOLATION_NUM_YEAR  INT    
declare    @MINOR_VIOLATION_REFER INT    DECLARE    @ACCIDENT_PAID_AMOUNT  INT    
Declare    @VEHICLETYPE_SCO nvarchar(20)    
DECLARE	   @VEHICLE_YEAR INT
DECLARE    @WCEXCCOVS NVARCHAR(100)
SET  @ACCIDENT_NUM_YEAR=3    
SET @VIOLATION_NUM_YEAR=2    
SET  @MINOR_VIOLATION_REFER=3    
SET @ACCIDENT_PAID_AMOUNT=1000                   
                  
                                    
                                    
/* APP_LIST RELATED FIELDS */                                    
       
SELECT @QUOTEEFFECTIVEDATE =  ISNULL(CONVERT(Varchar(20),APP_EFFECTIVE_DATE),'')  FROM APP_LIST WITH (NOLOCK)                      
 WHERE CUSTOMER_ID=@CUSTOMERID AND APP_ID=@APPID AND APP_VERSION_ID=@APPVERSIONID         

                                   
 /* Vehicle Related Fields  */                                                                                  
 SET @ISANTILOCKBRAKESDISCOUNTS=''                                   
 SET @AIRBAGDISCOUNT=''                                                              
                                                                                  
 SELECT                                                                 
                                                                                  
 @VEHICLETYPEUSE = case isnull(USE_VEHICLE,'0')                
         when '11332' then 'PERSONAL'                                                                                    
         when '11333' then 'COMMERCIAL'       
         when '0' then ''                                         
    end,                                                                                   
 @VEHICLECLASS =                   
 case @VEHICLETYPEUSE                                                                            
 when 'PERSONAL' then isnull(CLASS_PER,0)                                                                            
 when 'COMMERCIAL' then isnull(CLASS_COM,0)                            
end,                            
 @VEHICLECLASS_PER =                             
 case @VEHICLETYPEUSE                                                                            
 when 'PERSONAL' then isnull(CLASS_PER,0)                            
end,                                                                            
 @VEHICLECLASS_COM =                            
 case @VEHICLETYPEUSE                                                                            
 when 'COMMERCIAL' then isnull(CLASS_COM,0)                            
 end,                                       
                                          
 @YEAR =VEHICLE_YEAR ,                                                                                  
 @MAKE=MAKE,                                                                                  
 @MODEL=MODEL,                                                                                  
 @VIN=VIN,                                                                                  
 @SYMBOL=    
-- CASE  WHEN SYMBOL < 10    
--    THEN '0'+ CONVERT(NVARCHAR(40),SYMBOL)    
--   ELSE    
--    CONVERT(NVARCHAR(40),SYMBOL)    
-- END,     
CASE  WHEN SYMBOL < 10 
  THEN  
    CASE WHEN charindex('0',CONVERT(NVARCHAR(40),SYMBOL),0)!=1
      THEN '0'+ CONVERT(NVARCHAR(40),SYMBOL)
    END
   ELSE 
		CONVERT(NVARCHAR(40),SYMBOL)
END,                                  
-- @COST= CAST(AMOUNT AS varchar(100)),                             
 @COST= CAST(AMOUNT AS BIGINT),                                                                                   
 @ANNUALMILES=isnull(CAST(ANNUAL_MILEAGE AS varchar(100)),''),                                   
 @VEHICLETYPE=                 
 case @VEHICLETYPEUSE                  
 when 'PERSONAL' then ISNULL(VEHICLE_TYPE_PER,'')            
 when 'COMMERCIAL' then ISNULL(VEHICLE_TYPE_COM,'')                           
 end,                                                                            
            
 @ISANTILOCKBRAKESDISCOUNTS=case ISNULL(ANTI_LOCK_BRAKES,'0')                                                                                
 when '10963' then 'TRUE'                                                                                
 when '10964' then 'FALSE'                                                                              
 else 'FALSE'                                                    
 end,                                                                 
                       
 @AIRBAGDISCOUNT=isnull(AIR_BAG,'0') ,                           
 @AGE=VEHICLE_AGE,  
       
 @MILESEACHWAY= isnull(MILES_TO_WORK,'0'),                
 @CARPOOL = ISNULL(CAR_POOL,''),              
/*@MULTICARDISCOUNT=case isnull(MULTI_CAR,'0')                                                                        
 when '10919' then 'TRUE'                          
 when '10920' then 'TRUE'                                                                                                                                      
 when '10918' then 'FALSE'                                                                               
 else 'FALSE'                                           
 end*/                                                                              
 @ZIPCODEGARAGEDLOCATION  = isnull(GRG_ZIP,'') ,         
 @WEARINGSEATBELT=                                       
   case  ISNULL(PASSIVE_SEAT_BELT,'0')                            
   when  10964 then 'FALSE'                                 
   when  '0' then 'FALSE'                     
   else 'TRUE'                                       
   end ,                                                                               
       @VEHICLEUSE = isnull(VEHICLE_USE,'0'),                                                                                
 @INSURANCEAMOUNT = CAST(AMOUNT AS varchar(100))    
                                      
 FROM APP_VEHICLES WITH (NOLOCK)                                                             
 WHERE CUSTOMER_ID=@CUSTOMERID AND APP_ID=@APPID AND APP_VERSION_ID=@APPVERSIONID AND VEHICLE_ID=@VEHICLEID                       

DECLARE @MULTICAROPTION NVARCHAR(30)
SELECT @MULTICAROPTION = MULTI_CAR 
FROM APP_VEHICLES WITH (NOLOCK) WHERE CUSTOMER_ID=@CUSTOMERID AND APP_ID=@APPID AND APP_VERSION_ID=@APPVERSIONID and IS_ACTIVE = 'Y'
AND VEHICLE_ID = @VEHICLEID
---- Vehicle Age Group Calculation (Start)    ----
--Declare @MODELMONTHYEAR NVARCHAR(50)
--set @MODELMONTHYEAR='10/01/'+ @YEAR
--SET @AGE = DATEDIFF(YEAR,@MODELMONTHYEAR,@QUOTEEFFDATE)
--SET @AGE = @AGE +1
---- Vehicle Age Group Calculation (End)    ----
DECLARE @PERSVEHICLEIDS INT                                                                                       
DECLARE @COMSVEHICLEIDS INT    
SET @COMSVEHICLEIDS=0    
SET @PERSVEHICLEIDS=0    
SELECT @PERSVEHICLEIDS = COUNT(VEHICLE_ID) FROM APP_VEHICLES WITH (NOLOCK) WHERE CUSTOMER_ID=@CUSTOMERID AND APP_ID=@APPID AND APP_VERSION_ID=@APPVERSIONID and IS_ACTIVE = 'Y' 
AND USE_VEHICLE =11332 and VEHICLE_TYPE_PER !=11870 and VEHICLE_TYPE_PER != 11337    
SELECT @COMSVEHICLEIDS = COUNT(VEHICLE_ID) FROM APP_VEHICLES WITH (NOLOCK) WHERE CUSTOMER_ID=@CUSTOMERID AND APP_ID=@APPID AND APP_VERSION_ID=@APPVERSIONID and IS_ACTIVE = 'Y'  
AND USE_VEHICLE =11333 and VEHICLE_TYPE_COM !=11341 and VEHICLE_TYPE_COM != 11340    
SET @PERSVEHICLEIDS = @PERSVEHICLEIDS + @COMSVEHICLEIDS    
IF(@MULTICAROPTION ='11918' )
BEGIN
	SET @MULTICARDISCOUNT='FALSE'
END
ELSE IF(@MULTICAROPTION = '11919')
BEGIN
	IF ( @PERSVEHICLEIDS > 1)    
	 SET @MULTICARDISCOUNT = 'TRUE'                      
	ELSE                      
	 SET @MULTICARDISCOUNT = 'FALSE'                        
END 
ELSE IF(@MULTICAROPTION = '11920')
BEGIN
	 SET @MULTICARDISCOUNT = 'TRUE'                          
END                                               
/*                                                          
182,640                 
If there is any violation Of seat belt then set it                                                           
@WEARINGSEATBELT=false             
*/                                           
  

            
-----------SHAFI-----------------------------                 
    
--@MULTICARDISCOUNT SET IT Y IF APPLICATION AS MORE THAN ONE VEHICLE ------------------------------------                                
                               
                                                                      
DECLARE @VCOUNT INT                                                                      
SELECT @VCOUNT = COUNT(VEHICLE_ID)         
FROM APP_VEHICLES WITH (NOLOCK)                          
WHERE  CUSTOMER_ID=@CUSTOMERID AND APP_ID=@APPID AND APP_VERSION_ID=@APPVERSIONID                                
        AND  isnull(VEHICLE_TYPE_PER,0) <> 11337                        
           and    isnull(VEHICLE_TYPE_COM,0) NOT IN (11341,11340)                                                 
PRINT @VCOUNT                    
                                                
/*********                                                
If one vehicle is present then ""Multi-car"" discount is optional    
(Pls. note that in case of Trailer - No multi-car"" discount is applicable)                                                
If more than one vehicle (any vehicles except Trailer)  are added                                                
 then ""Multi-car"" discount is automatically given to all vehicles.                                                 
(In case of Trailer even if 2 trailers are added the discount is not applicable or if one vehicle (Other than trailer) and one trailer is added even then the discount is not given to the first vehicle)"                                                
                                                
**********/                                                
                                                
DECLARE @VEHTYPE INT     
DECLARE @SNOWPLOWCONDITION INT                   
                                                
SELECT @VEHTYPE = case isnull(USE_VEHICLE,0)                                                                                
         when 11332 then    VEHICLE_TYPE_PER                                                                     
         when 11333 then VEHICLE_TYPE_COM                                                                          
         when 0 then VEHICLE_TYPE_PER                                                
         end,                
  @SNOWPLOWCONDITION = ISNULL(SNOWPLOW_CONDS,0)    
  FROM APP_VEHICLES  WITH (NOLOCK)                                                          
           WHERE  CUSTOMER_ID=@CUSTOMERID AND APP_ID=@APPID AND APP_VERSION_ID=@APPVERSIONID AND VEHICLE_ID=@VEHICLEID                                                
PRINT @VEHTYPE                                                
                                                
IF  @VEHTYPE=11337 OR @VEHTYPE=11341 OR  @VEHTYPE=11340                                                
 SET @MULTICARDIS='N'                                                
ELSE                                     
SET @MULTICARDIS='Y'                                                 
                                                
                                                                  
                                                                    
                                                              
/*IF @VCOUNT >  1 AND @MULTICARDIS='Y'                                                
 SET @MULTICARDISCOUNT = 'TRUE'                            
ELSE       
 SET @MULTICARDISCOUNT = 'FALSE' */                          
 
PRINT @MULTICARDISCOUNT                    
                                                                                
-- Vehicle Class - depends on use PERSONAL/COMMERCIAL      
SET @VEHICLECLASSCOMPONENT1 = ''                                                                       
SET @VEHICLECLASSCOMPONENT2 = ''              
                                    
if @VEHICLETYPEUSE = 'PERSONAL'                                                                
   begin                                    
 select  @VEHICLECLASS_DESC = isnull(LOOKUP_VALUE_DESC,''), @VEHICLECLASS = RTRIM(LTRIM(isnull(LOOKUP_VALUE_CODE ,'')))    
 FROM MNT_LOOKUP_VALUES  WITH (NOLOCK)  WHERE LOOKUP_UNIQUE_ID = @VEHICLECLASS                           
if   @VEHICLECLASS  != ''                                                                                
  begin         
           SET @VEHICLECLASSCOMPONENT1 = left(@VEHICLECLASS,1)   
    SET @VEHICLECLASSCOMPONENT2 = right(@VEHICLECLASS,1)                                                  
  end                                    
   end                                    
                                     
If @VEHICLETYPEUSE = 'COMMERCIAL'                                     
   begin                                    
  SELECT @VEHICLECLASS_DESC = isnull(LOOKUP_VALUE_DESC,'') FROM                                     
  APP_VEHICLES AV  with (nolock) INNER JOIN MNT_LOOKUP_VALUES MLV with (nolock) ON                   
  AV.CLASS_DESCRIPTION = MLV.LOOKUP_UNIQUE_ID                                    
  WHERE CUSTOMER_ID =@CUSTOMERID AND  APP_ID=@APPID  AND  APP_VERSION_ID=@APPVERSIONID AND                                      
  VEHICLE_ID=@VEHICLEID AND AV.CLASS_DESCRIPTION IS NOT NULL                                     
                                    
  SELECT  @VEHICLECLASS = LTRIM(RTRIM(ISNULL(LOOKUP_VALUE_CODE ,'')))                            
  FROM MNT_LOOKUP_VALUES  WITH (NOLOCK)  WHERE LOOKUP_UNIQUE_ID = @VEHICLECLASS                              
                                
  IF   @VEHICLECLASS  != ''                                                                                
           SET @VEHICLECLASSCOMPONENT1 = @VEHICLECLASS                                                                                                                  
                            
                            
                            
  SELECT  @VEHICLECLASS_COM = ISNULL(LOOKUP_VALUE_CODE ,'')                            
                                  
  FROM MNT_LOOKUP_VALUES  WITH (NOLOCK)  WHERE LOOKUP_UNIQUE_ID = @VEHICLECLASS_COM                             
                            
 end                                   
                                    
                                              
-----------------------------------------------------------------------------------------------------------------------------                                              
-- <vehicleRatingCode> </vehicleRatingCode> only in case of Commercial                                               
                                              
 DECLARE @VEHICLERATINGCODE VARCHAR(10)                                              
 SET @VEHICLERATINGCODE=''                     
                                               
 SELECT @VEHICLERATINGCODE = MNT.TYPE                                              
 FROM  APP_VEHICLES APP WITH (NOLOCK) INNER JOIN  MNT_LOOKUP_VALUES MNT WITH (NOLOCK) ON APP.VEHICLE_TYPE_COM=MNT.LOOKUP_UNIQUE_ID                                              
 WHERE CUSTOMER_ID =@CUSTOMERID AND  APP_ID=@APPID  AND  APP_VERSION_ID=@APPVERSIONID AND  VEHICLE_ID=@VEHICLEID         
                                              
 
-----------------------------------------------------------------------------------------------------------------------------      
                              
        
-- Vehicle Type                                         
SELECT @VEHICLETYPE = ISNULL(LOOKUP_VALUE_CODE,''),     
    @VEHICLETYPEDESC = ISNULL(LOOKUP_VALUE_DESC,'')    
FROM MNT_LOOKUP_VALUES  WITH (NOLOCK) WHERE LOOKUP_UNIQUE_ID = @VEHICLETYPE                                   
IF EXISTS(SELECT * FROM  APP_VEHICLES WITH (NOLOCK) WHERE CUSTOMER_ID =@CUSTOMERID AND  APP_ID=@APPID  AND  APP_VERSION_ID=@APPVERSIONID AND  VEHICLE_ID=@VEHICLEID AND IS_SUSPENDED=10963)  
 BEGIN  
  SET @VEHICLETYPE_SCO='SCO'  
  SET @VEHICLETYPEDESC=@VEHICLETYPEDESC + ' (SUSPENDED-COMP ONLY)'  
 END 
-- Air bag discount               
SELECT @AIRBAGDISCOUNT = ISNULL(LOOKUP_VALUE_CODE,'') FROM MNT_LOOKUP_VALUES WITH (NOLOCK) WHERE  LOOKUP_UNIQUE_ID =@AIRBAGDISCOUNT        
DECLARE @SNOWPLOW_CONDS NVARCHAR(40)    
    
SELECT  @SNOWPLOW_CONDS = ISNULL(LOOKUP_VALUE_DESC,'') FROM  APP_VEHICLES APP WITH (NOLOCK)      
 INNER JOIN  MNT_LOOKUP_VALUES MNT WITH (NOLOCK) ON APP.SNOWPLOW_CONDS=MNT.LOOKUP_UNIQUE_ID       
 WHERE CUSTOMER_ID =@CUSTOMERID AND  APP_ID=@APPID  AND  APP_VERSION_ID=@APPVERSIONID AND  VEHICLE_ID=@VEHICLEID                                                                          
-- Vehicle Use                                                             
-- In Case Of Snowplow Assign B (Business)             
SELECT @USE= ISNULL(LOOKUP_VALUE_CODE  ,'') ,     
@VEHICLEUSEDESC = isnull(LOOKUP_VALUE_DESC,'') +     
CASE ISNULL(@SNOWPLOW_CONDS,'0')    
  WHEN '0'    
  THEN '  '    
ELSE    
 ', '+@SNOWPLOW_CONDS     
END,                     
 @VEHICLEUSE = CASE ISNULL(LOOKUP_VALUE_CODE  ,'')                                                               
--  when 'WS' then 'O'                                                         
  when 'SP' THEN ISNULL(LOOKUP_VALUE_CODE  ,'')                                                               
  else     
left(ISNULL(LOOKUP_VALUE_CODE ,''),1)                                                               
  end                                                                  
FROM MNT_LOOKUP_VALUES WITH (NOLOCK) WHERE LOOKUP_UNIQUE_ID = @VEHICLEUSE      
                                                                            
--------------------------------------------------------------        
-- radius of use for commercial vehicle        
Select @RADIUSOFUSE = RADIUS_OF_USE        
from APP_VEHICLES with(nolock) where CUSTOMER_ID=@CUSTOMERID and APP_ID=@APPID and APP_VERSION_ID=@APPVERSIONID and VEHICLE_ID=@VEHICLEID                                                                                  
        
        
-------------------------------------------------------------                
/*       
--Asfa - 06-June-2007              
When USE is "Drive to Work/School" then Check MILES EACH WAY(when <=10 then 'P', 11-25 then 'O', 25+ then 'W')              
*/    
SELECT     
  @STATEID = STATE_ID    
  FROM APP_LIST WHERE CUSTOMER_ID=@CUSTOMERID AND APP_ID=@APPID AND APP_VERSION_ID=@APPVERSIONID    
-- IF STATE INDIANA AND VEHICLE USE SNOW PLOW WITH SNOWPLOW CONDITION "Incidental with out Income"    
--THEN NO SURCHARGE    
IF(@USE='SP' AND @STATEID=14 AND @SNOWPLOWCONDITION=11914)    
BEGIN    
 SET @VEHICLEUSE='O'                
END     
ELSE IF(@USE='SP')    
BEGIN    
 SET @VEHICLEUSE='B'                
END             
IF (@USE='WS')                
BEGIN                
  IF (@MILESEACHWAY<=10)                 
     BEGIN                
  SET @VEHICLEUSE='P'                
     END                
  ELSE IF (@MILESEACHWAY>=11 AND @MILESEACHWAY<=25)                 
     BEGIN                
  SET @VEHICLEUSE='O'                
     END  
  ELSE IF (@MILESEACHWAY>25)          
     BEGIN              
  SET @VEHICLEUSE='W'                
END     
END     
    
IF (@USE='CPW')    
 BEGIN 
  SET @VEHICLEUSE='W'      
 END      
    
    
IF (@USE='CPU')    
 BEGIN    
  SET @VEHICLEUSE='O'      
 END            
                
IF (@VEHICLEUSE='P' AND (SUBSTRING(@VEHICLECLASS,1,1)='2' OR SUBSTRING(@VEHICLECLASS,1,1)='3' OR SUBSTRING(@VEHICLECLASS,1,1)='4' OR SUBSTRING(@VEHICLECLASS,1,1)='6'))              
  BEGIN              
    SET @VEHICLEUSE='O'            
  END              
              
              
IF (@USE='WS' AND @MILESEACHWAY>25 AND (@CARPOOL='10964' OR @CARPOOL='10963'))      
 BEGIN              
    SELECT @CARPOOL=LOOKUP_VALUE_DESC FROM MNT_LOOKUP_VALUES with(nolock) WHERE LOOKUP_UNIQUE_ID=@CARPOOL    
 END              
ELSE              
   SET @CARPOOL=''      
    
-------------------------------------------------------------    
-----------   Start of Extended Non-Owned Coverage for Named Individual(A-35)            
------------------------------------------------------------        
    
DECLARE @TVEHICLEID NVARCHAR(50)    
DECLARE @ENOCNI NVARCHAR(50)    
SET @ENOCNI = 'FALSE'    
SELECT top (1) @TVEHICLEID = VEHICLE_ID FROM APP_VEHICLES with(nolock) WHERE CUSTOMER_ID=@CUSTOMERID and APP_ID=@APPID and APP_VERSION_ID=@APPVERSIONID and USE_VEHICLE=11332 and VEHICLE_TYPE_PER NOT IN (11870,11337,11618) and IS_SUSPENDED <>10963    
IF(@TVEHICLEID=@VEHICLEID)    
 BEGIN    
  IF EXISTS(SELECT * FROM APP_VEHICLE_COVERAGES WHERE  CUSTOMER_ID=@CUSTOMERID and APP_ID=@APPID and APP_VERSION_ID=@APPVERSIONID and VEHICLE_ID=@VEHICLEID AND COVERAGE_CODE_ID IN (52,254))    
   BEGIN      
    SET @ENOCNI = 'TRUE'    
   END    
 END    
ELSE    
 BEGIN    
  SET @ENOCNI = 'FALSE'    
 END    
 ------------------------------------------------------------------
----------------------  WORKERS COMPENSATION OR EXCESS WORK (START)
-------------------------------------------------------------------
IF EXISTS(SELECT VEHICLE_ID FROM APP_VEHICLES WITH(NOLOCK) WHERE CUSTOMER_ID=@CUSTOMERID and APP_ID=@APPID and APP_VERSION_ID=@APPVERSIONID and USE_VEHICLE=11333 AND VEHICLE_ID=@VEHICLEID) 
	BEGIN
			SET @WCEXCCOVS=''
			IF EXISTS(SELECT VEHICLE_ID FROM APP_VEHICLES WITH(NOLOCK) WHERE CUSTOMER_ID=@CUSTOMERID and APP_ID=@APPID and APP_VERSION_ID=@APPVERSIONID and USE_VEHICLE=11333 AND VEHICLE_ID=@VEHICLEID AND COVERED_BY_WC_INSU=10963)
				BEGIN
					SET @WCEXCCOVS = 'WCINSURD'
				END
			IF(@WCEXCCOVS='')
				BEGIN
					IF EXISTS(SELECT VEHICLE_ID FROM APP_VEHICLE_COVERAGES WHERE  CUSTOMER_ID=@CUSTOMERID and APP_ID=@APPID and APP_VERSION_ID=@APPVERSIONID and VEHICLE_ID=@VEHICLEID AND COVERAGE_CODE_ID IN (997))
						BEGIN
							SET @WCEXCCOVS = 'CAB91'
						END
				END
	END
 ------------------------------------------------------------------
----------------------  WORKERS COMPENSATION OR EXCESS WORK (END)
-------------------------------------------------------------------   
-------------------------------------------------------------    
-----------   End of Extended Non-Owned Coverage for Named Individual(A-35)            
------------------------------------------------------------     
--				three years previous date(start)
	----------------------------------------------------------------------------------------------------  
DECLARE @THREEYEARLESSDATE DATETIME
DECLARE @THREEYEARDAYS INT
DECLARE @NINTEENYEARDATE DATETIME
DECLARE @NINTEENYEARDAYS INT
DECLARE @TWENTYFIVEYEARDATE DATETIME
DECLARE @TWENTYFIVEYEARDAYS INT
DECLARE @MAJORVIOLATIONEFFECTIVEDATE DATETIME
DECLARE @MAJORVIOLATIONEFFECTIVEDAYS INT
DECLARE @WAVERWORKLOSSEFFECTIVEDATE DATETIME
DECLARE @WAVERWORKLOSSEFFECTIVEDAYS INT

SET @NINTEENYEARDAYS=0
SET @THREEYEARDAYS=0
SET @TWENTYFIVEYEARDAYS=0
set @MAJORVIOLATIONEFFECTIVEDAYS=0
SET @THREEYEARLESSDATE = DATEADD(YEAR,-3,@QUOTEEFFECTIVEDATE)
SET @THREEYEARDAYS = DATEDIFF(DAY,@THREEYEARLESSDATE,@QUOTEEFFECTIVEDATE)
SET @NINTEENYEARDATE = DATEADD(YEAR,-19,@QUOTEEFFECTIVEDATE)
SET @NINTEENYEARDAYS = DATEDIFF(DAY,@NINTEENYEARDATE,@QUOTEEFFECTIVEDATE)
SET @TWENTYFIVEYEARDATE = DATEADD(YEAR,-25,@QUOTEEFFECTIVEDATE)
SET @TWENTYFIVEYEARDAYS = DATEDIFF(DAY,@TWENTYFIVEYEARDATE,@QUOTEEFFECTIVEDATE)
SET @MAJORVIOLATIONEFFECTIVEDATE = DATEADD(YEAR,-5,@QUOTEEFFECTIVEDATE)
SET @MAJORVIOLATIONEFFECTIVEDAYS = DATEDIFF(DAY,@MAJORVIOLATIONEFFECTIVEDATE,@QUOTEEFFECTIVEDATE)
SET @WAVERWORKLOSSEFFECTIVEDATE = DATEADD(YEAR,-60,@QUOTEEFFECTIVEDATE)
SET @WAVERWORKLOSSEFFECTIVEDAYS = DATEDIFF(DAY,@WAVERWORKLOSSEFFECTIVEDATE,@QUOTEEFFECTIVEDATE)
---------------------------------------------------------------------------------------------------
--				three years previous date(start)
----------------------------------------------------------------------------------------------------                           
 /* Airbag          
 if exists ( SELECT LOOKUP_VALUE_CODE  FROM MNT_LOOKUP_VALUES WHERE LOOKUP_UNIQUE_ID = @AIRBAGDISCOUNT )                 
SELECT      
    @AIRBAGDISCOUNT=ISNULL(LOOKUP_VALUE_CODE,'')                                                                                  
   FROM MNT_LOOKUP_VALUES WHERE LOOKUP_UNIQUE_ID = @AIRBAGDISCOUNT                                                
 else        
  SET @AIRBAGDISCOUNT=''  */        
                                                                     
                                           
                                           
                                                                                 
                                                                    
/*******************************Get The Nodes from Driver Table ***********************************/                                                                    
 -- <001  start>   
DECLARE @RATEDDRIVER NVARCHAR(30)    
SELECT @RATEDDRIVER = CLASS_DRIVERID FROM APP_VEHICLES AV  WITH (NOLOCK)  WHERE CUSTOMER_ID= @CUSTOMERID AND APP_ID= @APPID AND APP_VERSION_ID=@APPVERSIONID AND  VEHICLE_ID=@VEHICLEID                                               
                                               
 -- If any one of the rated drivers has DRIVERINCOME = LOW then send <DRIVERINCOME>LOW</DRIVERINCOME>                                                  
 if exists(select * from APP_DRIVER_DETAILS ADDS WITH (NOLOCK)                      
 inner join APP_DRIVER_ASSIGNED_VEHICLE ADAV WITH (NOLOCK) on                      
  ADDS.CUSTOMER_ID = ADAV.CUSTOMER_ID                      
 AND  ADDS.APP_ID = ADAV.APP_ID                      
 AND  ADDS.APP_VERSION_ID = ADAV.APP_VERSION_ID                      
 AND  ADDS.DRIVER_ID = ADAV.DRIVER_ID                      
 where            
 ADAV.APP_VEHICLE_PRIN_OCC_ID<>11931                      
 AND                      
 ADDS.CUSTOMER_ID=@CUSTOMERID and ADDS.APP_ID=@APPID and ADDS.APP_VERSION_ID=@APPVERSIONID AND ADAV.VEHICLE_ID=@VEHICLEID AND ADDS.DRIVER_INCOME <>  11414 AND ADDS.IS_ACTIVE='Y' and ADDS.DRIVER_ID=@RATEDDRIVER)    
   begin            
      set @DRIVERINCOME='HIGH'    
   end                 
   else      
   begin                                                   
      set @DRIVERINCOME='LOW'                                                     
   end                                                   
-- If any one of the assigned drivers has NODEPENDENT = NDEP then send <NODEPENDENT>NDEP</NODEPENDENT>                                                  
                              
 if exists(select NO_DEPENDENTS from APP_DRIVER_DETAILS ADDS WITH (NOLOCK)                      
 inner join APP_DRIVER_ASSIGNED_VEHICLE ADAV WITH (NOLOCK) on                      
  ADDS.CUSTOMER_ID = ADAV.CUSTOMER_ID                      
 AND  ADDS.APP_ID = ADAV.APP_ID                      
 AND  ADDS.APP_VERSION_ID = ADAV.APP_VERSION_ID  
 AND  ADDS.DRIVER_ID = ADAV.DRIVER_ID                      
 where                       
 ADAV.APP_VEHICLE_PRIN_OCC_ID<>11931                      
 AND                      
 ADDS.CUSTOMER_ID=@CUSTOMERID and ADDS.APP_ID=@APPID and ADDS.APP_VERSION_ID=@APPVERSIONID AND ADAV.VEHICLE_ID=@VEHICLEID AND  ADDS.NO_DEPENDENTS=11588 and ADDS.DRIVER_ID=@RATEDDRIVER)            
 begin      
    set @NODEPENDENT='NDEP'                     
 end                                                   
 else                                                  
 begin                                                   
  set   @NODEPENDENT='1MORE'                                                 
 end                                    
-- <001 end>                                              
                                                    
/*         
@WAIVEWORKLOSS Is Handled Through Covg/Endorsement (A-94 and 95) ID=43         
*/     
                                
                      
--if exists (SELECT ENDORSEMENT_ID FROM APP_VEHICLE_ENDORSEMENTS WITH (NOLOCK) WHERE CUSTOMER_ID=@CUSTOMERID AND APP_ID=@APPID AND APP_VERSION_ID=@APPVERSIONID AND                                                     
--            VEHICLE_ID=@VEHICLEID AND ENDORSEMENT_ID=43)                                                
--    SET  @WAIVEWORKLOSS = 'TRUE'                                                    
--ELSE                                              
--     SET  @WAIVEWORKLOSS = 'FALSE'    
SET  @WAIVEWORKLOSS = 'FALSE'    
    
IF EXISTS(SELECT DRIVER_ID FROM APP_DRIVER_DETAILS WHERE CUSTOMER_ID=@CUSTOMERID AND APP_ID=@APPID AND APP_VERSION_ID=@APPVERSIONID AND DRIVER_ID=@RATEDDRIVER AND     
(DATEDIFF(DAY,APP_DRIVER_DETAILS.DRIVER_DOB,@QUOTEEFFECTIVEDATE) >= @WAVERWORKLOSSEFFECTIVEDAYS) AND Waiver_Work_loss_benefits=1)    
BEGIN    
 SET  @WAIVEWORKLOSS = 'TRUE'        
END    
    
   SELECT @LOBID=APP_LOB,
	@QUOTEEFFDATE = ISNULL(CONVERT(Varchar(20),APP_EFFECTIVE_DATE),'')                                       
	FROM APP_LIST  WITH (NOLOCK)                                            
	WHERE CUSTOMER_ID=@CUSTOMERID AND APP_ID=@APPID AND APP_VERSION_ID=@APPVERSIONID                                              
                                                              
 /*          Get The @SUMOFACCIDENTPOINTS EQUAL TO Sum of all Accidents points in previous 3 yrs  */                                              
                        /* ACCIDENT  VIOLATION AFTER ACCIDENT=728  *****/                                                                           
 /*Get Tha Appeffective Date  */   
SELECT @STATE_ID =  convert(nvarchar(20),STATE)                                                                       
     FROM MNT_TERRITORY_CODES WITH (NOLOCK) WHERE ZIP=SUBSTRING(LTRIM(RTRIM(@ZIPCODEGARAGEDLOCATION)),1,5)    AND LOBID=@LOBID     
 AND @QUOTEEFFDATE BETWEEN EFFECTIVE_FROM_DATE  AND ISNULL(EFFECTIVE_TO_DATE,'3000-03-16 16:59:06.630') AND AUTO_VEHICLE_TYPE IS NULL          

         IF (@VEHICLETYPEUSE = 'PERSONAL' OR @STATE_ID = 22)    
    
BEGIN       
          
    SET @GARAGEDLOCATION =( select  top 1 isnull(county,'') +'  County,'+' ' + isnull(city,'')+ ' ' +'('+ zip+'),'+' ' + 'Territory : '+ convert(nvarchar(5),terr)                                                                        
    from MNT_TERRITORY_CODES WITH (NOLOCK) where zip=SUBSTRING(LTRIM(RTRIM(@ZIPCODEGARAGEDLOCATION)),1,5)    AND LOBID=@LOBID     
 AND @QUOTEEFFDATE BETWEEN EFFECTIVE_FROM_DATE  AND ISNULL(EFFECTIVE_TO_DATE,'3000-03-16 16:59:06.630') AND AUTO_VEHICLE_TYPE IS NULL )                         
            
    SET @TERRCODEGARAGEDLOCATION =( select  top 1  convert(nvarchar(5),terr)                  
    from MNT_TERRITORY_CODES WITH (NOLOCK) where zip=SUBSTRING(LTRIM(RTRIM(@ZIPCODEGARAGEDLOCATION)),1,5) AND LOBID=@LOBID     
 AND @QUOTEEFFDATE BETWEEN EFFECTIVE_FROM_DATE  AND ISNULL(EFFECTIVE_TO_DATE,'3000-03-16 16:59:06.630') AND AUTO_VEHICLE_TYPE IS NULL )                          
    
END    
    
    
IF (@VEHICLETYPEUSE = 'COMMERCIAL' AND @STATE_ID = 14)    
    
BEGIN                                                                                 
                      
    SET @GARAGEDLOCATION =( select  top 1 isnull(county,'') +'  County,'+' ' + isnull(city,'')+ ' ' +'('+ zip+'),'+' ' + 'Territory : '+ convert(nvarchar(5),terr)                                
    from MNT_TERRITORY_CODES WITH (NOLOCK) where zip=SUBSTRING(LTRIM(RTRIM(@ZIPCODEGARAGEDLOCATION)),1,5)    AND LOBID=@LOBID     
 AND @QUOTEEFFDATE BETWEEN EFFECTIVE_FROM_DATE  AND ISNULL(EFFECTIVE_TO_DATE,'3000-03-16 16:59:06.630') AND AUTO_VEHICLE_TYPE LIKE '%COM%')                                                                        
             
    SET @TERRCODEGARAGEDLOCATION =( select  top 1  convert(nvarchar(5),terr)          
    from MNT_TERRITORY_CODES WITH (NOLOCK) where zip=SUBSTRING(LTRIM(RTRIM(@ZIPCODEGARAGEDLOCATION)),1,5) AND LOBID=@LOBID     
 AND @QUOTEEFFDATE BETWEEN EFFECTIVE_FROM_DATE  AND ISNULL(EFFECTIVE_TO_DATE,'3000-03-16 16:59:06.630') AND AUTO_VEHICLE_TYPE LIKE '%COM%')                                                                                 
    
END    
                                                        
	-----------------------------------------------------------------------------------------------------    
	--         GOOD STUDENT DISCOUNT (END)    
	-----------------------------------------------------------------------------------------------------   
	---------------------------------------------------------------------------------------------------
	
---------------------------------------------------------------------------------------------------- 
	-- QUALIFIESTRAIBLAZERPROGRAM (Start)
----------------------------------------------------------------------------------------------------        
       
	SELECT  @QUALIFIESTRAIBLAZERPROGRAM = case (isnull(APP_SUBLOB,'0'))                                                                                
			WHEN '1' THEN 'Y'                                                        
			ELSE 'N'                                                                                
			END , 
		   @LOBID=APP_LOB,    
		   @QUOTEEFFDATE = ISNULL(CONVERT(Varchar(20),APP_EFFECTIVE_DATE),'')      
	FROM APP_LIST  WITH (NOLOCK)                                            
	WHERE CUSTOMER_ID=@CUSTOMERID AND APP_ID=@APPID AND APP_VERSION_ID=@APPVERSIONID                           
IF(@QUALIFIESTRAIBLAZERPROGRAM='Y')
	BEGIN
		-- MAKE TEMP TABLE TO STORE ALL ASSIGNED DRIVER BELOW 25 YEARS AGE
		CREATE TABLE #ASSIGNDRIVTRAIL                  
		(         
		  [IDEN_ID] INT IDENTITY(1,1) NOT NULL,                    
		  [ASSIGNDRIVERIDS] INT                   
		)    
	    
		INSERT INTO #ASSIGNDRIVTRAIL        
		SELECT ADDS.DRIVER_ID FROM  APP_DRIVER_ASSIGNED_VEHICLE ADDS WITH (NOLOCK) INNER JOIN APP_VEHICLES AV WITH (NOLOCK)                      
		ON	AV.CUSTOMER_ID=ADDS.CUSTOMER_ID AND                      
			AV.APP_ID=ADDS.APP_ID AND                      
			AV.APP_VERSION_ID = ADDS.APP_VERSION_ID AND                       
			AV.VEHICLE_ID=ADDS.VEHICLE_ID AND
			AV.CLASS_DRIVERID = ADDS.DRIVER_ID                     
			INNER JOIN APP_DRIVER_DETAILS WITH (NOLOCK)                      
		ON ADDS.CUSTOMER_ID=APP_DRIVER_DETAILS.CUSTOMER_ID AND                      
			ADDS.APP_ID=APP_DRIVER_DETAILS.APP_ID AND               
			ADDS.APP_VERSION_ID = APP_DRIVER_DETAILS.APP_VERSION_ID                
		AND ADDS.DRIVER_ID=APP_DRIVER_DETAILS.DRIVER_ID                      
		WHERE ADDS.CUSTOMER_ID=@CUSTOMERID AND ADDS.APP_ID=@APPID AND ADDS.APP_VERSION_ID=@APPVERSIONID AND ADDS.VEHICLE_ID=@VEHICLEID      
		AND (DATEDIFF(DAY,APP_DRIVER_DETAILS.DRIVER_DOB,@QUOTEEFFECTIVEDATE) < @TWENTYFIVEYEARDAYS) AND ADDS.APP_VEHICLE_PRIN_OCC_ID IN (11927,11928,11929,11930)  and APP_DRIVER_DETAILS.IS_ACTIVE='Y' 

		IF EXISTS( SELECT ASSIGNDRIVERIDS FROM #ASSIGNDRIVTRAIL)
			BEGIN
				SET @QUALIFIESTRAIBLAZERPROGRAM='N'
			END
		DROP TABLE #ASSIGNDRIVTRAIL
	
			-- CHECK EVERY ASSIGNED DRIVER FOR THIER MAJOR VIOLATION IF ANY HAVE MAJOR VIOLATION THEN DISCARD DISCOUNT ON THAT VEHICLE
		DECLARE @ASSIGNDRIVERIDSS INT
		CREATE TABLE #ASSIGNDRIVTRAILVIO                  
		(         
		  [IDEN_ID] INT IDENTITY(1,1) NOT NULL,                    
		  [ASSIGNDRIVERIDS] INT                   
		)    
	    
		INSERT INTO #ASSIGNDRIVTRAILVIO   
		SELECT ADDS.DRIVER_ID FROM  APP_DRIVER_ASSIGNED_VEHICLE ADDS WITH (NOLOCK) INNER JOIN APP_VEHICLES AV WITH (NOLOCK)                      
		ON	AV.CUSTOMER_ID=ADDS.CUSTOMER_ID AND                      
			AV.APP_ID=ADDS.APP_ID AND                      
			AV.APP_VERSION_ID = ADDS.APP_VERSION_ID AND                       
			AV.VEHICLE_ID=ADDS.VEHICLE_ID 
			INNER JOIN APP_DRIVER_DETAILS WITH (NOLOCK)                      
		ON  ADDS.CUSTOMER_ID=APP_DRIVER_DETAILS.CUSTOMER_ID AND                      
			ADDS.APP_ID=APP_DRIVER_DETAILS.APP_ID AND               
			ADDS.APP_VERSION_ID = APP_DRIVER_DETAILS.APP_VERSION_ID AND               
			ADDS.DRIVER_ID=APP_DRIVER_DETAILS.DRIVER_ID                      
			WHERE ADDS.CUSTOMER_ID=@CUSTOMERID AND ADDS.APP_ID=@APPID AND ADDS.APP_VERSION_ID=@APPVERSIONID AND ADDS.VEHICLE_ID=@VEHICLEID      
			AND ADDS.APP_VEHICLE_PRIN_OCC_ID IN (11398,11399,11925,11926,11927,11928,11929,11930)  and APP_DRIVER_DETAILS.IS_ACTIVE='Y' 

			-- RUN WHILE LOOP TO CHECK VIOLATION POINTS OF ASSIGNED DRIVERS    
			DECLARE @IDENT_COLS INT    
			SET @IDENT_COLS = 1        
			WHILE (1= 1)     
				BEGIN     
					IF NOT EXISTS (SELECT IDEN_ID FROM #ASSIGNDRIVTRAILVIO  WITH(NOLOCK) WHERE IDEN_ID = @IDENT_COLS )     
						 BEGIN     
							BREAK    
						 END    
						SELECT  @ASSIGNDRIVERIDSS = ASSIGNDRIVERIDS	FROM #ASSIGNDRIVTRAILVIO  WITH(NOLOCK)    
						WHERE IDEN_ID = @IDENT_COLS     
	        
					IF EXISTS (SELECT CUSTOMER_ID FROM APP_MVR_INFORMATION A WITH (NOLOCK)                                                  
						LEFT OUTER JOIN MNT_VIOLATIONS M   WITH (NOLOCK)                                                     
						ON A.VIOLATION_TYPE = M.VIOLATION_ID              
						WHERE	A.CUSTOMER_ID = @CUSTOMERID AND                                 
								A.APP_ID = @APPID AND                                           
								A.APP_VERSION_ID = @APPVERSIONID AND                                    
								A.DRIVER_ID = @ASSIGNDRIVERIDSS AND            
								NOT ISNULL(MVR_POINTS,0)<0 
								AND DATEDIFF(DAY,MVR_DATE ,@QUOTEEFFECTIVEDATE) <=@MAJORVIOLATIONEFFECTIVEDAYS
								AND A.IS_ACTIVE='Y' AND isnull(M.VIOLATION_CODE,'0') IN('10000','40000','SUSPN'))   
						BEGIN
							SET @QUALIFIESTRAIBLAZERPROGRAM='N'
						END
					SET @IDENT_COLS = @IDENT_COLS + 1    
				END      
	    
		DROP TABLE #ASSIGNDRIVTRAILVIO    
		-- CHECK ANY PRIOR LOSS ABOVE $750
		IF EXISTS(SELECT LOSS_ID FROM APP_PRIOR_LOSS_INFO with (NOLOCK) WHERE CUSTOMER_ID=@CUSTOMERID AND LOB=2 AND (datediff(day,APP_PRIOR_LOSS_INFO.OCCURENCE_DATE,@QUOTEEFFECTIVEDATE) <= @THREEYEARDAYS) AND AMOUNT_PAID > 750 )
			BEGIN
				SET @QUALIFIESTRAIBLAZERPROGRAM='N'
			END
	
   -- CHECK FOR INSURANCE SCORE
	------------------------------Added By Praveen Kumar----------  
   DECLARE @INSURANCESCORE nvarchar(20)  
   DECLARE @APP_EFFECTIVE_DATE varchar(50)    
      
	SELECT  @APP_EFFECTIVE_DATE = APP_EFFECTIVE_DATE    
	FROM APP_LIST WITH (NOLOCK)              
	WHERE CUSTOMER_ID= @CUSTOMERID AND APP_ID= @APPID AND APP_VERSION_ID=@APPVERSIONID     
     
  
	SELECT @INSURANCESCORE=case CAST(ISNULL(APPLY_INSURANCE_SCORE,-1) AS varchar(20))                               
	WHEN -1 THEN '100'          
	WHEN  -2 THEN 'NOHITNOSCORE'           
	ELSE CONVERT(NVARCHAR(20), APPLY_INSURANCE_SCORE) END         
   	FROM APP_LIST  WITH (NOLOCK)                              
	WHERE CUSTOMER_ID =@CUSTOMERID AND   APP_ID = @APPID AND APP_VERSION_ID=@APPVERSIONID     
    
	IF (@QUALIFIESTRAIBLAZERPROGRAM = 'Y' AND @INSURANCESCORE <> 'NOHITNOSCORE')    
		BEGIN    
			IF ((DATEDIFF(DAY,@APP_EFFECTIVE_DATE,'2007-08-01 00:00:00.000') < 0)AND @INSURANCESCORE > 750)     
				SET @QUALIFIESTRAIBLAZERPROGRAM = 'Y'    
     
			ELSE IF ((DATEDIFF(DAY,@APP_EFFECTIVE_DATE,'2007-08-01 00:00:00.000') > 0) AND (@INSURANCESCORE > 700) )     
				SET @QUALIFIESTRAIBLAZERPROGRAM = 'Y'    
  
			ELSE  
				SET @QUALIFIESTRAIBLAZERPROGRAM = 'N'    
  		END 
	ELSE IF   @INSURANCESCORE = 'NOHITNOSCORE'  
		SET @QUALIFIESTRAIBLAZERPROGRAM = 'N'   
	END
ELSE
	BEGIN
		SET @QUALIFIESTRAIBLAZERPROGRAM='N'
	END     
 ----------------------------------End---------------------   
----------------------------------------------------------------------------------------------------
	-- QUALIFIESTRAIBLAZERPROGRAM (end)
----------------------------------------------------------------------------------------------------

------------------------------------------------------------------------
--    ASSIGNED DRIVER ACCIDENT AND VIOLATION (START)
------------------------------------------------------------------------
DECLARE @IDENT_COLMN INT
DECLARE @ASSIGNPREMSAFEDRIVERIDS INT
DECLARE @PREMIER_SAFE_SUM_MVR_POINTS INT
DECLARE @PREMIER_SAFE_ACCIDENT_POINTS INT
DECLARE @PREMIER_SAFE_COUNT_ACCIDENTS INT
DECLARE @PREMIER_SAFE_MVR_POINTS INT
 SET @PREMIER_SAFE_SUM_MVR_POINTS=0
 SET @PREMIER_SAFE_ACCIDENT_POINTS=0
 SET @PREMIER_SAFE_COUNT_ACCIDENTS=0
 SET @PREMIER_SAFE_MVR_POINTS=0

CREATE TABLE #ASSIGNDRIVSPRMSAFE_DRIVER_VIOLATION_ACCIDENT    
(    
 [SUM_MVR_POINTS]  INT,                                                
 [ACCIDENT_POINTS]  INT,                                                
 [COUNT_ACCIDENTS]  INT,    
 [MVR_POINTS]  INT    
                   
)     
CREATE TABLE #ASSIGNDRIVSPRMSAFE                  
(         
  [IDEN_ID] INT IDENTITY(1,1) NOT NULL,                    
  [ASSIGNPRESAFEDRIVERIDS] INT                   
)  
    
INSERT INTO #ASSIGNDRIVSPRMSAFE    
	SELECT APP_DRIVER_DETAILS.DRIVER_ID FROM  APP_DRIVER_ASSIGNED_VEHICLE ADDS  WITH (NOLOCK)  INNER JOIN APP_VEHICLES AV  WITH (NOLOCK)                      
    ON	AV.CUSTOMER_ID=ADDS.CUSTOMER_ID AND          
		AV.APP_ID=ADDS.APP_ID AND             
		AV.APP_VERSION_ID = ADDS.APP_VERSION_ID AND           
		AV.VEHICLE_ID=ADDS.VEHICLE_ID     
	INNER JOIN APP_DRIVER_DETAILS  WITH (NOLOCK)                      
    ON	ADDS.CUSTOMER_ID=APP_DRIVER_DETAILS.CUSTOMER_ID AND                      
		ADDS.APP_ID=APP_DRIVER_DETAILS.APP_ID AND                      
		ADDS.APP_VERSION_ID = APP_DRIVER_DETAILS.APP_VERSION_ID  AND                      
		ADDS.DRIVER_ID=APP_DRIVER_DETAILS.DRIVER_ID                      
	WHERE ADDS.CUSTOMER_ID=@CUSTOMERID AND ADDS.APP_ID=@APPID AND ADDS.APP_VERSION_ID=@APPVERSIONID AND ADDS.VEHICLE_ID=@VEHICLEID AND
	ADDS.APP_VEHICLE_PRIN_OCC_ID IN (11398,11925,11927,11929) and APP_DRIVER_DETAILS.IS_ACTIVE='Y'
 
	SET @IDENT_COLMN = 1        
	WHILE (1= 1)     
	BEGIN     
		IF NOT EXISTS (SELECT IDEN_ID FROM #ASSIGNDRIVSPRMSAFE  WITH(NOLOCK) WHERE IDEN_ID = @IDENT_COLMN )     
		 BEGIN     
			BREAK    
		 END    
		SELECT  @ASSIGNPREMSAFEDRIVERIDS = ASSIGNPRESAFEDRIVERIDS    
		FROM #ASSIGNDRIVSPRMSAFE  WITH(NOLOCK)    
		WHERE IDEN_ID = @IDENT_COLMN   
			INSERT INTO #ASSIGNDRIVSPRMSAFE_DRIVER_VIOLATION_ACCIDENT   exec GetMVRViolationPoints @CUSTOMERID,@APPID,@APPVERSIONID ,@ASSIGNPREMSAFEDRIVERIDS,@ACCIDENT_NUM_YEAR,@VIOLATION_NUM_YEAR,@MINOR_VIOLATION_REFER,@ACCIDENT_PAID_AMOUNT               
		SET @IDENT_COLMN = @IDENT_COLMN + 1    
	END 
	SELECT @PREMIER_SAFE_SUM_MVR_POINTS = SUM(ISNULL(SUM_MVR_POINTS,0)), @PREMIER_SAFE_ACCIDENT_POINTS = SUM(ISNULL(ACCIDENT_POINTS,0)) FROM  #ASSIGNDRIVSPRMSAFE_DRIVER_VIOLATION_ACCIDENT
    
		IF EXISTS (SELECT VIOLATION_ID FROM APP_MVR_INFORMATION  WITH (NOLOCK) WHERE                                          
			CUSTOMER_ID =@CUSTOMERID AND  APP_ID=@APPID  AND  APP_VERSION_ID=@APPVERSIONID                                               
			AND (VIOLATION_ID=182 OR VIOLATION_ID=640) AND VIOLATION_TYPE =602 AND IS_ACTIVE='Y' AND DRIVER_ID=@ASSIGNPREMSAFEDRIVERIDS)                                              
				BEGIN
					SET  @WEARINGSEATBELT='FALSE' 
				END
	SET @PREMIER_SAFE_SUM_MVR_POINTS = @PREMIER_SAFE_SUM_MVR_POINTS + @PREMIER_SAFE_ACCIDENT_POINTS

	DROP TABLE #ASSIGNDRIVSPRMSAFE_DRIVER_VIOLATION_ACCIDENT
    DROP TABLE #ASSIGNDRIVSPRMSAFE
------------------------------------------------------------------------
--    ASSIGNED DRIVER ACCIDENT AND VIOLATION (START)
------------------------------------------------------------------------

-----------------------------------------------------------------------    
--    RATED DRIVER VIOLATION AND ACCIDENT (START)    
----------------------------------------------------------------------    
DECLARE @ASSPOINTDRIVER NVARCHAR(20),
@SUM_MVR_POINTS INT 
DECLARE @COUNT_ACCIDENTS INT,    
        @ACCIDENT_POINTS INT
SET @ACCIDENT_POINTS=0  
  
SELECT @RATEDDRIVER = CLASS_DRIVERID FROM APP_VEHICLES AV  WITH (NOLOCK)  WHERE CUSTOMER_ID= @CUSTOMERID AND AV.APP_ID=@APPID AND AV.APP_VERSION_ID=@APPVERSIONID AND AV.VEHICLE_ID=@VEHICLEID    
SELECT @ASSPOINTDRIVER = CONVERT(NVARCHAR(50),ADDS.DRIVER_ID) FROM  APP_DRIVER_ASSIGNED_VEHICLE ADDS WITH (NOLOCK) INNER JOIN APP_VEHICLES AV WITH (NOLOCK)                      
	ON	AV.CUSTOMER_ID=ADDS.CUSTOMER_ID AND                      
		AV.APP_ID=ADDS.APP_ID AND                      
		AV.APP_VERSION_ID = ADDS.APP_VERSION_ID AND                       
		AV.VEHICLE_ID=ADDS.VEHICLE_ID AND    
		AV.CLASS_DRIVERID = ADDS.DRIVER_ID      
WHERE AV.CUSTOMER_ID=@CUSTOMERID AND AV.APP_ID=@APPID AND AV.APP_VERSION_ID=@APPVERSIONID AND AV.VEHICLE_ID=@VEHICLEID AND     
ADDS.APP_VEHICLE_PRIN_OCC_ID IN (11398,11925,11927,11929)    
  
CREATE TABLE #RATEDDRIVER_VIOLATION_ACCIDENT    
(    
 [SUM_MVR_POINTS]  INT,                                                
 [ACCIDENT_POINTS]  INT,                                                
 [COUNT_ACCIDENTS]  INT,    
 [MVR_POINTS]  INT    
                   
)     
IF(@ASSPOINTDRIVER IS NOT NULL)    
	BEGIN             
		INSERT INTO #RATEDDRIVER_VIOLATION_ACCIDENT exec GetMVRViolationPoints @CUSTOMERID,@APPID,@APPVERSIONID ,@RATEDDRIVER,@ACCIDENT_NUM_YEAR,@VIOLATION_NUM_YEAR,@MINOR_VIOLATION_REFER,@ACCIDENT_PAID_AMOUNT               
	END    
SET @SUM_MVR_POINTS =0    
SET @ACCIDENT_POINTS =0    
    
SELECT @SUM_MVR_POINTS = SUM_MVR_POINTS,@ACCIDENT_POINTS = ACCIDENT_POINTS FROM #RATEDDRIVER_VIOLATION_ACCIDENT    
 IF(@SUM_MVR_POINTS IS NULL)    
	SET @SUM_MVR_POINTS =0    
 IF(@ACCIDENT_POINTS IS NULL)    
	SET @ACCIDENT_POINTS =0    
SET @SUM_MVR_POINTS= @SUM_MVR_POINTS + @ACCIDENT_POINTS    
DROP TABLE  #RATEDDRIVER_VIOLATION_ACCIDENT    
-----------------------------------------------------------------------    
--    RATED DRIVER VIOLATION AND ACCIDENT (END)    
----------------------------------------------------------------------    
    
-----------------------------------------------    
-- RATED DRIVER OVERRIDE (ITRACK 4544) (START)    
-----------------------------------------------    
SET @GOODSTUDENT = 'FALSE'    
DECLARE @GOODDRIVER NVARCHAR(100)    
SELECT	@GOODDRIVER=CONVERT(NVARCHAR(50),ADDS.DRIVER_ID) FROM  APP_DRIVER_ASSIGNED_VEHICLE ADDS WITH (NOLOCK) INNER JOIN APP_VEHICLES AV WITH (NOLOCK)                      
	ON	AV.CUSTOMER_ID=ADDS.CUSTOMER_ID AND                      
		AV.APP_ID=ADDS.APP_ID AND                      
		AV.APP_VERSION_ID = ADDS.APP_VERSION_ID AND                       
		AV.VEHICLE_ID=ADDS.VEHICLE_ID AND    
		AV.CLASS_DRIVERID = ADDS.DRIVER_ID                  
	INNER JOIN APP_DRIVER_DETAILS WITH (NOLOCK)                      
	ON	ADDS.CUSTOMER_ID=APP_DRIVER_DETAILS.CUSTOMER_ID AND    
		ADDS.APP_ID=APP_DRIVER_DETAILS.APP_ID AND                      
		ADDS.APP_VERSION_ID = APP_DRIVER_DETAILS.APP_VERSION_ID                        
		AND ADDS.DRIVER_ID=APP_DRIVER_DETAILS.DRIVER_ID                      
	WHERE ADDS.CUSTOMER_ID=@CUSTOMERID AND ADDS.APP_ID=@APPID AND ADDS.APP_VERSION_ID=@APPVERSIONID AND ADDS.VEHICLE_ID=@VEHICLEID      
		AND (APP_DRIVER_DETAILS.DRIVER_GOOD_STUDENT=1 AND APP_DRIVER_DETAILS.FULL_TIME_STUDENT=1)     
		AND (DATEDIFF(DAY,APP_DRIVER_DETAILS.DRIVER_DOB,@QUOTEEFFECTIVEDATE) < @TWENTYFIVEYEARDAYS) AND ADDS.APP_VEHICLE_PRIN_OCC_ID IN (11927,11928,11929,11930)  and APP_DRIVER_DETAILS.IS_ACTIVE='Y'            
   
	IF (@GOODDRIVER IS NOT NULL)    
		BEGIN    
			SET @GOODSTUDENT='TRUE'    
		END       
    
    IF(@SUM_MVR_POINTS < 3 AND @GOODDRIVER IS NOT NULL)    
		BEGIN    
			SET @GOODSTUDENT='TRUE'    
		END    
	ELSE    
		BEGIN     
			SET @GOODSTUDENT='FALSE'    
		END    
    
-----------------------------------------------    
-- RATED DRIVER OVERRIDE (ITRACK 4544) (END)    
-----------------------------------------------    

-----------------------------------------------------------------------------------------------------    
--         PREMIER DRIVER DISCOUNT (ITRACK 4712) (START)    
-----------------------------------------------------------------------------------------------------    
 --         Premier Driver DISCOUNT (START) (Driver tab Yes or No)
   
   
    
CREATE TABLE #ASSIGNDRIV                  
(         
  [IDEN_ID] INT IDENTITY(1,1) NOT NULL,                    
  [ASSIGNDRIVERIDS] INT                   
)    
    
INSERT INTO #ASSIGNDRIV                                   
SELECT ADDS.DRIVER_ID FROM  APP_DRIVER_ASSIGNED_VEHICLE ADDS WITH (NOLOCK) INNER JOIN APP_VEHICLES AV WITH (NOLOCK)                      
ON	AV.CUSTOMER_ID=ADDS.CUSTOMER_ID AND                      
	AV.APP_ID=ADDS.APP_ID AND                      
	AV.APP_VERSION_ID = ADDS.APP_VERSION_ID AND                       
	AV.VEHICLE_ID=ADDS.VEHICLE_ID                      
	WHERE ADDS.CUSTOMER_ID=@CUSTOMERID AND ADDS.APP_ID=@APPID AND ADDS.APP_VERSION_ID=@APPVERSIONID AND ADDS.VEHICLE_ID=@VEHICLEID      
	AND ADDS.APP_VEHICLE_PRIN_OCC_ID IN (11398,11399,11925,11926,11927,11928,11929,11930)             
-- RUN WHILE LOOP TO CHECK Driver tab Yes or No    
DECLARE @ASSIGNDRIVERIDS INT,    
		@IDENT_COL INT,    
       	@PREMIERYESNO INT,
		@SAFEYESNO INT    
SET @PREMIERDRIVER = 'TRUE' 
SET @SAFEDRIVER = 'TRUE'        
SET @IDENT_COL = 1        
WHILE (1= 1)     
	BEGIN     
		IF NOT EXISTS (SELECT IDEN_ID FROM #ASSIGNDRIV  WITH(NOLOCK) WHERE IDEN_ID = @IDENT_COL )     
	 BEGIN     
	  BREAK    
	 END    
	SELECT  @ASSIGNDRIVERIDS = ASSIGNDRIVERIDS    
	FROM #ASSIGNDRIV  WITH(NOLOCK)    
	WHERE IDEN_ID = @IDENT_COL     
        
	SELECT @PREMIERYESNO = isnull(DRIVER_PREF_RISK,0),
		   @SAFEYESNO   =  isnull(SAFE_DRIVER_RENEWAL_DISCOUNT,0)
	FROM APP_DRIVER_DETAILS WHERE CUSTOMER_ID=@CUSTOMERID AND APP_ID=@APPID AND APP_VERSION_ID=@APPVERSIONID AND DRIVER_ID =@ASSIGNDRIVERIDS AND IS_ACTIVE = 'Y'    
        
	IF(@PREMIERDRIVER ='TRUE')    
		BEGIN    
			IF(@PREMIERYESNO = 0)    
				BEGIN    
					SET @PREMIERDRIVER='FALSE'    
				END    
		END    
    
	IF(@SAFEDRIVER ='TRUE')    
		BEGIN    
			IF(@SAFEYESNO = 0)    
				BEGIN    
					SET @SAFEDRIVER='FALSE'    
				END    
		END    
	SET @IDENT_COL = @IDENT_COL + 1    
END      
    
DROP TABLE #ASSIGNDRIV   
		--    Premier Driver DISCOUNT (END) (Driver tab Yes or No) 
    
DECLARE @LOSSCOUNT INT    
--SET @PREMIERDRIVER = 'TRUE'     
--IF EXISTS (SELECT DRIVER_ID FROM APP_DRIVER_DETAILS with (NOLOCK) WHERE CUSTOMER_ID= @CUSTOMERID AND APP_ID= @APPID AND APP_VERSION_ID=@APPVERSIONID AND DRIVER_ID = @RATEDDRIVER AND isnull(DRIVER_PREF_RISK,0)=0)    
--	 BEGIN    
--		SET  @PREMIERDRIVER = 'FALSE'     
--	 END      
 
	SELECT  @YEARSCONTINSUREDWITHWOLVERINE = ISNULL(YEARS_INSU_WOL,'')                           
	FROM APP_AUTO_GEN_INFO WITH (NOLOCK) WHERE CUSTOMER_ID= @CUSTOMERID AND APP_ID= @APPID AND APP_VERSION_ID=@APPVERSIONID                                                 
                                                                
	IF @YEARSCONTINSUREDWITHWOLVERINE  IS NULL OR @YEARSCONTINSUREDWITHWOLVERINE=''                                               
		SET  @YEARSCONTINSUREDWITHWOLVERINE='0'                        
                    
IF(@PREMIERDRIVER = 'TRUE')    
 BEGIN    
	IF(@YEARSCONTINSUREDWITHWOLVERINE <= 2)      
		BEGIN                    
			SET  @PREMIERDRIVER = 'TRUE'    
		END                      
	ELSE     
		BEGIN                     
			SET  @PREMIERDRIVER = 'FALSE'      
		END                    
    
    IF(@PREMIER_SAFE_SUM_MVR_POINTS > 0 and @PREMIERDRIVER = 'TRUE')    
		BEGIN     
			SET  @PREMIERDRIVER = 'FALSE'     
		END      
  
  -- PREMIER DRIVER DISCOUNT WILL NOW BE DECIDED ON RATED DRIVER AGE AND LICENCE DATE   
	DECLARE @COUNT_DRIVERS INT    
	DECLARE @LOSSCOUNTDRIVERFREE INT               
	SELECT @COUNT_DRIVERS=COUNT(*) FROM  APP_DRIVER_ASSIGNED_VEHICLE ADDS  WITH (NOLOCK)  INNER JOIN APP_VEHICLES AV  WITH (NOLOCK)                      
		ON	AV.CUSTOMER_ID=ADDS.CUSTOMER_ID AND                      
			AV.APP_ID=ADDS.APP_ID AND             
			AV.APP_VERSION_ID = ADDS.APP_VERSION_ID AND           
			AV.VEHICLE_ID=ADDS.VEHICLE_ID     
		INNER JOIN APP_DRIVER_DETAILS  WITH (NOLOCK)                      
		ON	ADDS.CUSTOMER_ID=APP_DRIVER_DETAILS.CUSTOMER_ID AND                      
			ADDS.APP_ID=APP_DRIVER_DETAILS.APP_ID AND                      
			ADDS.APP_VERSION_ID = APP_DRIVER_DETAILS.APP_VERSION_ID  AND                      
			ADDS.DRIVER_ID=APP_DRIVER_DETAILS.DRIVER_ID                      
		WHERE ADDS.CUSTOMER_ID=@CUSTOMERID AND ADDS.APP_ID=@APPID AND ADDS.APP_VERSION_ID=@APPVERSIONID AND ADDS.VEHICLE_ID=@VEHICLEID 
		AND (((DATEDIFF(DAY,DRIVER_DOB,@QUOTEEFFECTIVEDATE) < @NINTEENYEARDAYS) OR (DATEDIFF(DAY , DATE_LICENSED , @QUOTEEFFECTIVEDATE) < @THREEYEARDAYS)))  and APP_DRIVER_DETAILS.IS_ACTIVE='Y'
		AND ADDS.DRIVER_ID = @RATEDDRIVER                  
    
	IF (@COUNT_DRIVERS > 0 and @PREMIERDRIVER = 'TRUE')                      
		BEGIN                      
			SET  @PREMIERDRIVER = 'FALSE'                      
		END   
          -----------------------------------------------If prior loss is not attached to a driver then apply it to all risk on that policy(start)--------------------             
	IF(@STATE_ID =22)    
		BEGIN      
			IF(DATEDIFF(day,'07/01/2009',@QUOTEEFFECTIVEDATE)>=0)
				BEGIN
					SELECT @LOSSCOUNTDRIVERFREE=COUNT(LOSS_ID) FROM APP_PRIOR_LOSS_INFO with (NOLOCK) WHERE CUSTOMER_ID=@CUSTOMERID AND LOB=2 AND (datediff(day,APP_PRIOR_LOSS_INFO.OCCURENCE_DATE,@QUOTEEFFECTIVEDATE) < @THREEYEARDAYS) AND AMOUNT_PAID > 750 				END
			ELSE
				BEGIN
					SELECT @LOSSCOUNTDRIVERFREE=COUNT(LOSS_ID) FROM APP_PRIOR_LOSS_INFO with (NOLOCK) WHERE CUSTOMER_ID=@CUSTOMERID AND LOB=2 AND (datediff(day,APP_PRIOR_LOSS_INFO.OCCURENCE_DATE,@QUOTEEFFECTIVEDATE) < @THREEYEARDAYS) AND AMOUNT_PAID > 75 				END
			IF(@LOSSCOUNTDRIVERFREE >0 and @PREMIERDRIVER = 'TRUE')    
				BEGIN
					SET  @PREMIERDRIVER = 'FALSE'
				END
		END    
			
	IF(@STATE_ID =14)    
		BEGIN      
			SELECT @LOSSCOUNTDRIVERFREE=COUNT(LOSS_ID) FROM APP_PRIOR_LOSS_INFO with (NOLOCK) WHERE CUSTOMER_ID=@CUSTOMERID AND LOB=2 AND (datediff(day,APP_PRIOR_LOSS_INFO.OCCURENCE_DATE,@QUOTEEFFECTIVEDATE) <= @THREEYEARDAYS) AND AMOUNT_PAID > 750 
			IF(@LOSSCOUNTDRIVERFREE >0 and @PREMIERDRIVER = 'TRUE')    
				BEGIN    
				SET  @PREMIERDRIVER = 'FALSE'     
				END   
 		END    
-----------------------------------------------If prior loss is MORE THAT 750 THEN apply it to all risk on that policy(end)--------------------      
END    
ELSE    
	BEGIN    
		SET  @PREMIERDRIVER = 'FALSE'     
	END 

-----------------------------------------------------------------------------------------------------    
	--         PREMIER DRIVER DISCOUNT (ITRACK 4712) (END)    
-----------------------------------------------------------------------------------------------------    
-----------------------------------------------------------------------------------------------------    
	--         SAFE DRIVER DISCOUNT (ITRACK 4732) (START)    
-----------------------------------------------------------------------------------------------------    
 --SET @SAFEDRIVER = 'TRUE'     
--	IF EXISTS (SELECT DRIVER_ID FROM APP_DRIVER_DETAILS with (NOLOCK) WHERE CUSTOMER_ID= @CUSTOMERID AND APP_ID= @APPID AND APP_VERSION_ID=@APPVERSIONID AND DRIVER_ID = @RATEDDRIVER AND SAFE_DRIVER_RENEWAL_DISCOUNT=0)    
--		BEGIN    
--			SET  @SAFEDRIVER = 'FALSE'     
--		END      
          
IF(@SAFEDRIVER = 'TRUE' )    
	BEGIN    
		IF(@YEARSCONTINSUREDWITHWOLVERINE > 2)    
			BEGIN    
				SET @PREMIERDRIVER = 'FALSE'     
				SET	@SAFEDRIVER = 'TRUE'                      
			END    
		ELSE    
			BEGIN    
				SET @SAFEDRIVER = 'FALSE'                      
		END    
	    
		IF(@PREMIER_SAFE_SUM_MVR_POINTS > 0 and @SAFEDRIVER = 'TRUE')    
			BEGIN    
				SET  @SAFEDRIVER = 'FALSE'     
			END       
	    
		IF(@STATE_ID =22)    
			BEGIN    
			IF(DATEDIFF(day,'07/01/2009',@QUOTEEFFECTIVEDATE)>=0)
				BEGIN
					SELECT @LOSSCOUNT=COUNT(LOSS_ID) FROM APP_PRIOR_LOSS_INFO with (NOLOCK) WHERE CUSTOMER_ID=@CUSTOMERID AND LOB=2 AND (datediff(day,APP_PRIOR_LOSS_INFO.OCCURENCE_DATE,@QUOTEEFFECTIVEDATE) <= @THREEYEARDAYS) AND AMOUNT_PAID > 750  
				END
			ELSE
				BEGIN
					SELECT @LOSSCOUNT=COUNT(LOSS_ID) FROM APP_PRIOR_LOSS_INFO with (NOLOCK) WHERE CUSTOMER_ID=@CUSTOMERID AND LOB=2 AND (datediff(day,APP_PRIOR_LOSS_INFO.OCCURENCE_DATE,@QUOTEEFFECTIVEDATE) <= @THREEYEARDAYS) AND AMOUNT_PAID > 75  
				END				
			END    
		ELSE IF(@STATE_ID =14)    
			BEGIN    
				SELECT @LOSSCOUNT=COUNT(LOSS_ID) FROM APP_PRIOR_LOSS_INFO with (NOLOCK) WHERE CUSTOMER_ID=@CUSTOMERID AND LOB=2 AND (datediff(day,APP_PRIOR_LOSS_INFO.OCCURENCE_DATE,@QUOTEEFFECTIVEDATE) <= @THREEYEARDAYS) AND AMOUNT_PAID > 750 
			END    
	    
		IF (DATEDIFF(day,'02/01/2008',@QUOTEEFFECTIVEDATE)>0)     
			BEGIN    
				IF(@LOSSCOUNT >=2 and @SAFEDRIVER = 'TRUE')    
					BEGIN    
						SET  @SAFEDRIVER = 'FALSE'     
					END     
			END    
		ELSE    
			BEGIN    
				IF(@LOSSCOUNT >= 3 and @SAFEDRIVER = 'TRUE')    
					 BEGIN    
						SET  @SAFEDRIVER = 'FALSE'     
					END     
			END    
    
	END    
ELSE    
	BEGIN    
		SET  @SAFEDRIVER = 'FALSE'     
	END   
 
-----------------------------------------------------------------------------------------------------    
--         SAFE DRIVER DISCOUNT  (ITRACK 4732)(END)    
-----------------------------------------------------------------------------------------------------    
	--  TRAILBLAIZER  ADDED  
IF(@PREMIERDRIVER = 'TRUE' and @QUALIFIESTRAIBLAZERPROGRAM='Y')  
	BEGIN     
		SET  @PREMIERDRIVER = 'FALSE'     
	END      
              
/**************************** END **************************************/                                                                     
                                                                    
                          
                                                                                  
                                                                   
 /* FINAL SELECT */                                                 
 SELECT                                                                    
  @AGE AS AGE,                                                             
  @VEHICLETYPEUSE as VEHICLETYPEUSE,         
  @VEHICLETYPE   AS VEHICLETYPE,     
  @VEHICLETYPEDESC AS VEHICLETYPEDESC,                                         
  @VEHICLERATINGCODE AS VEHICLERATINGCODE,                                                        
  @VEHICLECLASS as VEHICLECLASS,                                           
  @VEHICLECLASSCOMPONENT1  as VEHICLECLASSCOMPONENT1,                           
  @VEHICLECLASSCOMPONENT2 as VEHICLECLASSCOMPONENT2,      
  isnull(@VEHICLECLASS_DESC,'') as VEHICLECLASS_DESC,   
 -- @SUMOFACCIDENTPOINTS   AS SUMOFACCIDENTPOINTS,                                                                                  
 -- @SUMOFVIOLATIONPOINTS   AS SUMOFVIOLATIONPOINTS,                                                                             
                                      
  @YEAR   AS YEAR,                                                                     
  @MAKE   AS MAKE,                                                                                                
  @MODEL   AS MODEL,                                                                                  
  @VIN   AS VIN,                      
  @SYMBOL   AS SYMBOL,                                                                                  
  @COST   AS COST,                                                                  
  @ANNUALMILES   AS ANNUALMILES,  
  @VEHICLEUSE   AS VEHICLEUSE,        
 @RADIUSOFUSE   AS RADIUSOFUSE,                  
  @CARPOOL  AS CARPOOL,              
--  @USE as USE1,  -- ths is not required in rater ..rater uses VehicleUse       
   @VEHICLEUSEDESC as VEHICLEUSEDESC,             
           
  @MILESEACHWAY   AS MILESEACHWAY,     
  @ENOCNI AS ENOCNI,    
  @DRIVERINCOME AS DRIVERINCOME ,                                                    
  @NODEPENDENT   AS DEPENDENTS,                   
  @WAIVEWORKLOSS AS WAIVEWORKLOSS,      
  @ISANTILOCKBRAKESDISCOUNTS   AS ISANTILOCKBRAKESDISCOUNTS,                                                                    
  @AIRBAGDISCOUNT   AS AIRBAGDISCOUNT,                            
  @MULTICARDISCOUNT   AS MULTICARDISCOUNT,                                                                                  
  --@INSURANCEAMOUNT   AS INSURANCEAMOUNT,                         
  @ZIPCODEGARAGEDLOCATION  AS ZIPCODEGARAGEDLOCATION,                       
  @GARAGEDLOCATION AS GARAGEDLOCATION,                                                   
  @TERRCODEGARAGEDLOCATION   AS TERRCODEGARAGEDLOCATION,                                                                                  
  @WEARINGSEATBELT   AS WEARINGSEATBELT   ,                                                           
  @QUALIFIESTRAIBLAZERPROGRAM as QUALIFIESTRAIBLAZERPROGRAM ,                                                      
  --@TYPE   AS TYPE,                              
  @VEHICLECLASS_COM AS VEHICLECLASS_COMM,                            
  ISNULL(@GOODSTUDENT,'FALSE')  AS GOODSTUDENT,                      
  @PREMIERDRIVER as PREMIERDRIVER,                      
  @SAFEDRIVER AS SAFEDRIVER,  
  @VEHICLETYPE_SCO AS VEHICLETYPE_SCO,
  @WCEXCCOVS AS WCEXCCOVS
END                                                                                


GO

