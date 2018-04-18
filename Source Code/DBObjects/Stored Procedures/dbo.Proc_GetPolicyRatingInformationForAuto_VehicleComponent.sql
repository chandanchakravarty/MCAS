IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetPolicyRatingInformationForAuto_VehicleComponent]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetPolicyRatingInformationForAuto_VehicleComponent]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                                        
Proc Name               : Dbo.Proc_GetPolicyRatingInformationForAuto_VehicleComponent                                        
Created by              : Shafi.                                        
Date                    : 02/03/06                                      
Purpose                 : To get the information for creating the input xml                                          
Revison History    :                                        
Used In                 :   Creating InputXML for vehicle                                        
    
Reviewed By : Anurag Verma    
Reviewed On : 06-07-2007    
------------------------------------------------------------                                        
Date     Review By          Comments                                        
------   ------------       -------------------------*/                                        
-- drop PROC Dbo.Proc_GetPolicyRatingInformationForAuto_VehicleComponent        
CREATE  PROC [dbo].[Proc_GetPolicyRatingInformationForAuto_VehicleComponent]                                 
(                                        
@CUSTOMERID    int,                                        
@POLICYID    int,                                        
@POLICYVERSIONID   int,                                        
@VEHICLEID    int                                         
)                                        
AS                                        
                                        
BEGIN                                         
                                                         
 set quoted_identifier off                                                            
                               
                                                            
DECLARE  @VEHICLETYPEUSE nvarchar(100)                                                          
DECLARE  @VEHICLECLASS nvarchar(10)                                                          
DECLARE  @VEHICLECLASSCOMPONENT1  nvarchar(10)                                                          
DECLARE  @VEHICLECLASSCOMPONENT2  nvarchar(10)                        
DECLARE @VEHICLECLASS_DESC nvarchar(500)                                                        
DECLARE  @VEHICLETYPE   nvarchar(10)    
DECLARE  @VEHICLETYPEDESC NVARCHAR(100)                           
  DECLARE  @GOODSTUDENT     nvarchar(100)                                                           
DECLARE  @PREMIERDRIVER   nvarchar(100)                             
                        
DECLARE  @YEAR   nvarchar(100)                                                            
DECLARE  @MAKE   nvarchar(100)                                                            
DECLARE  @AGE   nvarchar(100)                                                            
DECLARE  @MODEL   nvarchar(100)                                                            
DECLARE  @VIN   nvarchar(100)                                                            
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
DECLARE @GARAGEDLOCATION nvarchar(100)                                                            
DECLARE  @WEARINGSEATBELT   nvarchar(100)                                                            
DECLARE @SAFEDRIVER NVARCHAR(100)            
declare @YEARSCONTINSUREDWITHWOLVERINE int                                                                
DECLARE  @TYPE   nvarchar(100)                                                            
DECLARE  @ISUNDERINSUREDMOTORISTS   nvarchar(100)                                                            
DECLARE @QUALIFIESTRAIBLAZERPROGRAM nvarchar(10)                                                          
DECLARE @STATE_ID NVARCHAR(20)                                                          
DECLARE  @USE nvarchar(10)                                                          
DECLARE @VEHICLEUSEDESC nvarchar(200)                                                
                                              
--Get The Some Filds From Driver table                                              
                                              
DECLARE    @DRIVERINCOME      nvarchar(100)                                                          
DECLARE    @NODEPENDENT      nvarchar(100)                                                          
DECLARE    @WAIVEWORKLOSS    nvarchar(100)                                                          
DECLARE @LOBID NVARCHAR(2)                         
DECLARE    @VEHICLECLASS_PER nvarchar(100)                     
DECLARE    @VEHICLECLASS_COM nvarchar(100)                              
 DECLARE    @QUOTEEFFECTIVEDATE NVARCHAR(100)               
 DECLARE     @QUOTEEFFDATE   NVARCHAR(20)         
  DECLARE @SNOWPLOWCONDITION INT       
DECLARE    @ACCIDENT_NUM_YEAR   INT    
DECLARE    @VIOLATION_NUM_YEAR  INT    
DECLARE    @VIOLATION_NUM_YEAR_REFER  INT    
DECLARE    @ACCIDENT_PAID_AMOUNT  INT   
DECLARE @INSURANCESCORE            nvarchar(20)  --Praveen  
 DECLARE @POLICYEFFECTIVEDATE           nvarchar(20)    
DECLARE @PROCCESSID INT    
DECLARE @CACELATION_TYPE INT    
DECLARE @PROCESS_STATUS nvarchar(40)    
DECLARE @STATENAME             nvarchar(100)  
DECLARE @TERMFACTOR   nvarchar(5)  
DECLARE @VEHICLETYPE_SCO NVARCHAR(20) 
DECLARE @WCEXCCOVS NVARCHAR(100)   
SET  @ACCIDENT_NUM_YEAR=3    
SET @VIOLATION_NUM_YEAR=2    
SET  @VIOLATION_NUM_YEAR_REFER=3    
SET @ACCIDENT_PAID_AMOUNT=1000     
                      
SELECT @QUOTEEFFECTIVEDATE =  APP_EFFECTIVE_DATE FROM POL_CUSTOMER_POLICY_LIST  WITH (NOLOCK)          
 WHERE CUSTOMER_ID=@CUSTOMERID AND POLICY_ID=@POLICYID AND POLICY_VERSION_ID=@POLICYVERSIONID                         
-- QUALIFIESTRAIBLAZERPROGRAM    
--REMOVED BY PRAVEEN KUMAR ON 31-10-2008                                                        
SELECT  --@QUALIFIESTRAIBLAZERPROGRAM = case (isnull(POLICY_SUBLOB,'0'))                                                          
--  when '1' then 'Y'                                                          
--  else 'N'                                                          
--  end            ,  
   @LOBID=POLICY_LOB,    
   @QUOTEEFFDATE =  ISNULL(CONVERT(Varchar(20),APP_EFFECTIVE_DATE),'')                                           
FROM POL_CUSTOMER_POLICY_LIST  WITH (NOLOCK)                                                           
 WHERE CUSTOMER_ID=@CUSTOMERID AND POLICY_ID=@POLICYID AND POLICY_VERSION_ID=@POLICYVERSIONID                      
          
                      
                                                 
 /* Vehicle Related Fields  */                                                            
 SET @ISANTILOCKBRAKESDISCOUNTS=''                        
 SET @AIRBAGDISCOUNT=''                                            
                                                            
 SELECT                                                       
                            
 @VEHICLETYPEUSE = case isnull(APP_USE_VEHICLE_ID,'0')                            
         when '11332' then 'PERSONAL'                                                          
         when '11333' then 'COMMERCIAL'                                                              
         when '0' then ''                                                              
         end,       
 @VEHICLECLASS =                                                           
 case @VEHICLETYPEUSE                                                      
 when 'PERSONAL' then isnull(APP_VEHICLE_PERCLASS_ID,0)            
 when 'COMMERCIAL' then isnull(APP_VEHICLE_COMCLASS_ID,0)              
 end,                   
                  
 @VEHICLECLASS_PER =                   
 case @VEHICLETYPEUSE                          
 when 'PERSONAL' then isnull(APP_VEHICLE_PERCLASS_ID,0)                  
 end,                                                                  
 @VEHICLECLASS_COM =                  
 case @VEHICLETYPEUSE                                                                  
 when 'COMMERCIAL' then isnull(APP_VEHICLE_COMCLASS_ID,0)                  
 end,                             
                                                     
 @YEAR =VEHICLE_YEAR ,                                                        
 @MAKE=MAKE,                      
 @MODEL=MODEL,                                        
 @VIN=VIN,                                                            
 @SYMBOL=    
-- CASE     
--  WHEN SYMBOL < 10    
--   THEN '0' + CONVERT(NVARCHAR(40),SYMBOL)    
--  ELSE    
--   CONVERT(NVARCHAR(40),SYMBOL)    
 --END,  
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
 when 'PERSONAL' then ISNULL(APP_VEHICLE_PERTYPE_ID,'')                                                           
 when 'COMMERCIAL' then ISNULL(APP_VEHICLE_COMTYPE_ID,'')                                                            
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
 --@MULTICARDISCOUNT=case isnull(MULTI_CAR,'0')                                              
 --when '10919' then 'TRUE'                 
 --when '10920' then 'TRUE'                      
 --when '10918' then 'FALSE'              
 --else 'FALSE'                                                          
 --end,                                     
 @ZIPCODEGARAGEDLOCATION   = isnull(GRG_ZIP,'') ,                                    
  @WEARINGSEATBELT=                                                                           
   case  ISNULL(PASSIVE_SEAT_BELT,'0')                          
   when  10964 then 'FALSE'                                      
   when  '0' then 'FALSE'                                              
   else 'TRUE'                                                         
 end ,                                                          
       @VEHICLEUSE = isnull(VEHICLE_USE,'0'),                                                          
    @INSURANCEAMOUNT = CAST(AMOUNT AS varchar(100)),    
       @SNOWPLOWCONDITION = ISNULL(SNOWPLOW_CONDS,0)                                                        
                                                             
 FROM POL_VEHICLES   WITH (NOLOCK)                                                          
 WHERE CUSTOMER_ID=@CUSTOMERID AND POLICY_ID=@POLICYID AND POLICY_VERSION_ID=@POLICYVERSIONID AND VEHICLE_ID=@VEHICLEID           
DECLARE @MULTICAROPTION NVARCHAR(30)

SELECT  @MULTICAROPTION = MULTI_CAR  FROM POL_VEHICLES WITH (NOLOCK) WHERE CUSTOMER_ID=@CUSTOMERID AND POLICY_ID=@POLICYID AND POLICY_VERSION_ID=@POLICYVERSIONID 
and IS_ACTIVE='Y'  AND VEHICLE_ID =@VEHICLEID 

DECLARE @PERVEHICLEIDS INT       
DECLARE @COMVEHICLEIDS INT    
SET @PERVEHICLEIDS=0    
SET @COMVEHICLEIDS=0        
SELECT @PERVEHICLEIDS = COUNT(VEHICLE_ID) FROM POL_VEHICLES WITH (NOLOCK) WHERE CUSTOMER_ID=@CUSTOMERID AND POLICY_ID=@POLICYID AND POLICY_VERSION_ID=@POLICYVERSIONID and IS_ACTIVE='Y'  AND APP_USE_VEHICLE_ID =11332 and APP_VEHICLE_PERTYPE_ID !=11870 and



APP_VEHICLE_PERTYPE_ID !=11337    
SELECT @COMVEHICLEIDS = COUNT(VEHICLE_ID) FROM POL_VEHICLES WITH (NOLOCK) WHERE CUSTOMER_ID=@CUSTOMERID AND POLICY_ID=@POLICYID AND POLICY_VERSION_ID=@POLICYVERSIONID and IS_ACTIVE='Y'  AND APP_USE_VEHICLE_ID =11333 and APP_VEHICLE_COMTYPE_ID !=11341 and 




APP_VEHICLE_COMTYPE_ID !=11340    
SET @PERVEHICLEIDS = @PERVEHICLEIDS + @COMVEHICLEIDS    
IF(@MULTICAROPTION='11918')
	BEGIN
		SET @MULTICARDISCOUNT = 'FALSE'       
	END
ELSE IF(@MULTICAROPTION='11919')
	BEGIN
		IF (@PERVEHICLEIDS > 1)            
		   SET @MULTICARDISCOUNT = 'TRUE'            
		ELSE            
		 SET @MULTICARDISCOUNT = 'FALSE'  
	END
ELSE IF(@MULTICAROPTION='11920')
	BEGIN
	 SET @MULTICARDISCOUNT = 'TRUE' 
	END       
 

-----------------------------------------------------------------------------------------------------------------------------                                              
-- <vehicleRatingCode> </vehicleRatingCode> only in case of Commercial  (start)
DECLARE @VEHICLERATINGCODE VARCHAR(10)                                              
 SET @VEHICLERATINGCODE='' 
SELECT @VEHICLERATINGCODE=MNT.TYPE                                             
 FROM  pol_VEHICLES APP WITH (NOLOCK) INNER JOIN  MNT_LOOKUP_VALUES MNT WITH (NOLOCK) ON APP.APP_VEHICLE_COMTYPE_ID=MNT.LOOKUP_UNIQUE_ID                                              
 WHERE CUSTOMER_ID =@CUSTOMERID AND  policy_ID=@POLICYID  AND  policy_VERSION_ID=@POLICYVERSIONID AND  VEHICLE_ID=@VEHICLEID          


-----------------------------------------------------------------------------------------------------------------------------                                              
-- <vehicleRatingCode> </vehicleRatingCode> only in case of Commercial   (end)                          
/*                
            
182                          
640                                    
If there is any violation Of seat belt then set it       
@WEARINGSEATBELT=false                                   
                                    
*/ 
  
-----------SHAFI-----------------------------                                                
             
--@MULTICARDISCOUNT SET IT Y IF APPLICATION AS MORE THAN ONE VEHICLE ------------------------------------                                                
  
                                    
DECLARE @VCOUNT INT    
SELECT @VCOUNT = COUNT(VEHICLE_ID)                                           
FROM POL_VEHICLES WITH (NOLOCK)                                                        
WHERE  CUSTOMER_ID=@CUSTOMERID AND POLICY_ID=@POLICYID AND POLICY_VERSION_ID=@POLICYVERSIONID                           
                                                
/*IF @VCOUNT > 1                                                 
 SET @MULTICARDISCOUNT = 'TRUE'                                                
ELSE                                                
 SET @MULTICARDISCOUNT = 'FALSE'                                                          
  */                                                        
-- Vehicle Class - depends on use PERSONAL/COMMERCIAL                                                          
SET @VEHICLECLASSCOMPONENT1 = ''            
SET @VEHICLECLASSCOMPONENT2 = ''                                                          
                 
if @VEHICLETYPEUSE = 'PERSONAL'                                                            
   begin                      
 select  @VEHICLECLASS_DESC = isnull(LOOKUP_VALUE_DESC,''), @VEHICLECLASS = ltrim(rtrim(isnull(LOOKUP_VALUE_CODE ,'')))                       
 FROM MNT_LOOKUP_VALUES  WITH (NOLOCK)  WHERE LOOKUP_UNIQUE_ID = @VEHICLECLASS                                                                  
 if   @VEHICLECLASS  != ''                                                                  
  begin                              
           SET @VEHICLECLASSCOMPONENT1 = left(@VEHICLECLASS,1)                        
    SET @VEHICLECLASSCOMPONENT2 = right(@VEHICLECLASS,1)           
  end                      
   end                      
                       
If @VEHICLETYPEUSE = 'COMMERCIAL'                       
   begin                      
  SELECT @VEHICLECLASS_DESC = ltrim(rtrim(isnull(LOOKUP_VALUE_DESC,''))) FROM                       
  POL_VEHICLES PV WITH (NOLOCK) INNER JOIN MNT_LOOKUP_VALUES MLV WITH (NOLOCK) ON                        
  PV.CLASS_DESCRIPTION = MLV.LOOKUP_UNIQUE_ID                      
  WHERE CUSTOMER_ID =@CUSTOMERID AND  POLICY_ID=@POLICYID  AND  POLICY_VERSION_ID=@POLICYVERSIONID AND      
  VEHICLE_ID=@VEHICLEID AND PV.CLASS_DESCRIPTION IS NOT NULL                      
                     
  SELECT  @VEHICLECLASS = ISNULL(LOOKUP_VALUE_CODE ,'')             
  FROM MNT_LOOKUP_VALUES WITH (NOLOCK)  WHERE LOOKUP_UNIQUE_ID = @VEHICLECLASS                                                                  
  IF   @VEHICLECLASS  != ''                                                                  
           SET @VEHICLECLASSCOMPONENT1 = @VEHICLECLASS                    
                  
                  
                  
  SELECT  @VEHICLECLASS_COM = ISNULL(LOOKUP_VALUE_CODE ,'')                  
                        
  FROM MNT_LOOKUP_VALUES  WITH (NOLOCK)  WHERE LOOKUP_UNIQUE_ID = @VEHICLECLASS_COM                   
  end    
            
                            
-- Vehicle Type                                
SELECT @VEHICLETYPE = ISNULL(LOOKUP_VALUE_CODE,''),    
       @VEHICLETYPEDESC = ISNULL(LOOKUP_VALUE_DESC,'')    
 FROM MNT_LOOKUP_VALUES WITH (NOLOCK)  WHERE LOOKUP_UNIQUE_ID = @VEHICLETYPE                                   
IF EXISTS(SELECT * FROM POL_VEHICLES WHERE CUSTOMER_ID=@CUSTOMERID AND POLICY_ID=@POLICYID AND POLICY_VERSION_ID=@POLICYVERSIONID AND  VEHICLE_ID=@VEHICLEID  AND IS_SUSPENDED=10963)  
 BEGIN  
 SET @VEHICLETYPE_SCO='SCO'  
  SET @VEHICLETYPEDESC=@VEHICLETYPEDESC + ' (SUSPENDED-COMP ONLY)'  
 END  
             
-- Air bag discount  
SELECT @AIRBAGDISCOUNT = ISNULL(LOOKUP_VALUE_CODE,'') FROM MNT_LOOKUP_VALUES WITH (NOLOCK) WHERE  LOOKUP_UNIQUE_ID =@AIRBAGDISCOUNT  
            
-- Vehicle Use          
DECLARE @SNOWPLOW_CONDS NVARCHAR(40)    
SELECT  @SNOWPLOW_CONDS = ISNULL(LOOKUP_VALUE_DESC,'') FROM  POL_VEHICLES PPP WITH (NOLOCK)      
 INNER JOIN  MNT_LOOKUP_VALUES MNT WITH (NOLOCK) ON PPP.SNOWPLOW_CONDS=MNT.LOOKUP_UNIQUE_ID       
 WHERE CUSTOMER_ID =@CUSTOMERID AND  POLICY_ID=@POLICYID  AND  POLICY_VERSION_ID=@POLICYVERSIONID AND  VEHICLE_ID=@VEHICLEID                                       
-- In Case Of Snowplow Assign B (Business)                                             
SELECT @USE= ISNULL(LOOKUP_VALUE_CODE  ,'') ,     
    
@VEHICLEUSEDESC = isnull(LOOKUP_VALUE_DESC,'')+     
CASE ISNULL(@SNOWPLOW_CONDS,'0')    
  WHEN '0'    
  THEN '  '    
ELSE    
 ', '+@SNOWPLOW_CONDS    
END,                                                                                        
 @VEHICLEUSE = case ISNULL(LOOKUP_VALUE_CODE  ,'')     
--  when 'WS' then 'O'                    
  when 'SP' THEN ISNULL(LOOKUP_VALUE_CODE  ,'')                                       
  else left(ISNULL(LOOKUP_VALUE_CODE  ,''),1)                                           
  end                                      
                                          
FROM MNT_LOOKUP_VALUES WITH (NOLOCK) WHERE LOOKUP_UNIQUE_ID = @VEHICLEUSE                                                          
          
-------------------------------------------------------------          
DECLARE @STATEID SMALLINT    
SELECT     
  @STATEID = STATE_ID    
  FROM POL_CUSTOMER_POLICY_LIST  WITH (NOLOCK) WHERE CUSTOMER_ID=@CUSTOMERID AND POLICY_ID=@POLICYID  AND  POLICY_VERSION_ID=@POLICYVERSIONID    
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
    
/*        
--Asfa - 06-June-2007     
When USE is "Drive to Work/School" then Check MILES EACH WAY(when <=10 then 'P', 11-25 then 'O', 25+ then 'W')        
*/        
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
    SELECT @CARPOOL=LOOKUP_VALUE_DESC FROM MNT_LOOKUP_VALUES WITH (NOLOCK) WHERE LOOKUP_UNIQUE_ID=@CARPOOL      
 END     
ELSE        
   SET @CARPOOL=''     
    
-------------------------------------------------------------    
-----------   Start of Extended Non-Owned Coverage for Named Individual(A-35)            
------------------------------------------------------------        
DECLARE @TVEHICLEID NVARCHAR(50)    
DECLARE @ENOCNI NVARCHAR(50)    
SET @ENOCNI = 'FALSE'    
SELECT top 1 @TVEHICLEID = VEHICLE_ID FROM POL_VEHICLES with(nolock) WHERE  CUSTOMER_ID=@CUSTOMERID AND POLICY_ID=@POLICYID AND POLICY_VERSION_ID=@POLICYVERSIONID and APP_USE_VEHICLE_ID=11332 
	and APP_VEHICLE_PERTYPE_ID NOT IN (11870,11337,11618) AND IS_SUSPENDED <>10963
IF(@TVEHICLEID=@VEHICLEID)    
 BEGIN    
  IF EXISTS(SELECT * FROM POL_VEHICLE_COVERAGES  with(nolock) WHERE  CUSTOMER_ID=@CUSTOMERID and POLICY_ID=@POLICYID AND POLICY_VERSION_ID=@POLICYVERSIONID and VEHICLE_ID=@VEHICLEID AND COVERAGE_CODE_ID IN (52,254))    
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
IF EXISTS(SELECT VEHICLE_ID FROM POL_VEHICLES WITH(NOLOCK) WHERE CUSTOMER_ID=@CUSTOMERID and POLICY_ID=@POLICYID and POLICY_VERSION_ID=@POLICYVERSIONID and APP_USE_VEHICLE_ID=11333 AND VEHICLE_ID=@VEHICLEID) 
	BEGIN
			SET @WCEXCCOVS=''
			IF EXISTS(SELECT VEHICLE_ID FROM POL_VEHICLES WITH(NOLOCK) WHERE CUSTOMER_ID=@CUSTOMERID and POLICY_ID=@POLICYID and POLICY_VERSION_ID=@POLICYVERSIONID and APP_USE_VEHICLE_ID=11333 AND VEHICLE_ID=@VEHICLEID AND COVERED_BY_WC_INSU=10963)
				BEGIN
					SET @WCEXCCOVS = 'WCINSURD'
				END
			IF(@WCEXCCOVS='')
				BEGIN
					IF EXISTS(SELECT VEHICLE_ID FROM POL_VEHICLE_COVERAGES WHERE  CUSTOMER_ID=@CUSTOMERID and POLICY_ID=@POLICYID and POLICY_VERSION_ID=@POLICYVERSIONID and VEHICLE_ID=@VEHICLEID AND COVERAGE_CODE_ID IN (997))
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
------------------------------------------------------------                                                                         
 
--------------------------------------------------------------------------------------------------
--  Three Year previous date(start)
--------------------------------------------------------------------------------------------------
DECLARE @THREEYEARLESSDATE DATETIME
DECLARE @THREEYEARDAYS INT
DECLARE @NINTEENYEARDATE DATETIME
DECLARE @NINTEENYEARDAYS INT
DECLARE @TWENTYFIVEYEARDATE DATETIME
DECLARE @TWENTYFIVEYEARDAYS INT
DECLARE @MAJORVIOLATIONEFFECTIVEDATE DATETIME
DECLARE @MAJORVIOLATIONEFFECTIVEDAYS INT
DECLARE @WAIVERWORKLOSSEFFECTIVEDATE DATETIME
DECLARE @WAIVERWORKLOSSEFFECTIVEDAYS INT
set @MAJORVIOLATIONEFFECTIVEDAYS=0
SET @NINTEENYEARDAYS=0
SET @THREEYEARDAYS=0
SET @THREEYEARLESSDATE = DATEADD(YEAR,-3,@QUOTEEFFECTIVEDATE)
SET @THREEYEARDAYS = DATEDIFF(DAY,@THREEYEARLESSDATE,@QUOTEEFFECTIVEDATE)
SET @NINTEENYEARDATE = DATEADD(YEAR,-19,@QUOTEEFFECTIVEDATE)
SET @NINTEENYEARDAYS = DATEDIFF(DAY,@NINTEENYEARDATE,@QUOTEEFFECTIVEDATE)
SET @TWENTYFIVEYEARDATE = DATEADD(YEAR,-25,@QUOTEEFFECTIVEDATE)
SET @TWENTYFIVEYEARDAYS = DATEDIFF(DAY,@TWENTYFIVEYEARDATE,@QUOTEEFFECTIVEDATE)
SET @MAJORVIOLATIONEFFECTIVEDATE = DATEADD(YEAR,-5,@QUOTEEFFECTIVEDATE)
SET @MAJORVIOLATIONEFFECTIVEDAYS = DATEDIFF(DAY,@MAJORVIOLATIONEFFECTIVEDATE,@QUOTEEFFECTIVEDATE)
SET @WAIVERWORKLOSSEFFECTIVEDATE = DATEADD(YEAR,-60,@QUOTEEFFECTIVEDATE)
SET @WAIVERWORKLOSSEFFECTIVEDAYS = DATEDIFF(DAY,@WAIVERWORKLOSSEFFECTIVEDATE,@QUOTEEFFECTIVEDATE)
--------------------------------------------------------------------------------------------------
--  Three Year previous date(start)
--------------------------------------------------------------------------------------------------
     
                 
 /* Airbag                                             
 if exists ( SELECT LOOKUP_VALUE_CODE  FROM MNT_LOOKUP_VALUES WHERE LOOKUP_UNIQUE_ID = @AIRBAGDISCOUNT )                           
  SELECT                                                             
    @AIRBAGDISCOUNT=ISNULL(LOOKUP_VALUE_CODE,'')                                                           
   FROM MNT_LOOKUP_VALUES WHERE LOOKUP_UNIQUE_ID = @AIRBAGDISCOUNT        
 else                            
  SET @AIRBAGDISCOUNT=''  */                  
                                                            
  
                                                            
                                                            
                                              
/*******************************Get The Nodes from Driver Table ***********************************/                                              
                                              
                                              
                                              
                                              
--IF EXISTS ( SELECT VEHICLE_ID FROM POL_DRIVER_DETAILS                                                              
-- WHERE CUSTOMER_ID=@CUSTOMERID AND POLICY_ID=@POLICYID AND POLICY_VERSION_ID=@POLICYVERSIONID AND VEHICLE_ID=@VEHICLEID )                                              
-- BEGIN                                       
                              
/*  SELECT TOP 1              
        @DRIVERINCOME =ISNULL(DRIVER_INCOME,'0'),                           
        @NODEPENDENT  =ISNULL(NO_DEPENDENTS,'0'),               
        @WAIVEWORKLOSS = case ISNULL(WAIVER_WORK_LOSS_BENEFITS,'0')                                                              
                          when '0' then 'FALSE'                                                              
                          when '1' then 'TRUE'                                                              
                           end                                               
      FROM POL_DRIVER_DETAILS                                                              
      WHERE CUSTOMER_ID=@CUSTOMERID AND POLICY_ID=@POLICYID AND POLICY_VERSION_ID=@POLICYVERSIONID AND VEHICLE_ID=@VEHICLEID              
                                              
IF @DRIVERINCOME ='11415' 
         SET @DRIVERINCOME='HIGH'                            
     ELSE                                
         SET @DRIVERINCOME ='LOW'                                               
                                              
     
  IF @NODEPENDENT ='11589'                                                            
   SET  @NODEPENDENT ='1'                                               
  ELSE                                              
   SET  @NODEPENDENT ='0'                                           
                             
 IF  @WAIVEWORKLOSS  != 'TRUE' OR @WAIVEWORKLOSS IS NULL  
 SET @WAIVEWORKLOSS  =  'FALSE'   */         
             
     
DECLARE @RATEDDRIVER NVARCHAR(30)    
    
  SELECT @RATEDDRIVER = CLASS_DRIVERID FROM POL_VEHICLES WITH (NOLOCK) WHERE CUSTOMER_ID=@CUSTOMERID AND POLICY_ID=@POLICYID AND POLICY_VERSION_ID=@POLICYVERSIONID     
AND VEHICLE_ID = @VEHICLEID
        -- <001 start>      
 -- If any one of the rated drivers has DRIVERINCOME = LOW then send <DRIVERINCOME>LOW</DRIVERINCOME>                      
 if exists(select * from POL_DRIVER_DETAILS ADDS WITH (NOLOCK)          
 inner join POL_DRIVER_ASSIGNED_VEHICLE ADAV WITH (NOLOCK) on          
  ADDS.CUSTOMER_ID = ADAV.CUSTOMER_ID          
 AND  ADDS.POLICY_ID = ADAV.POLICY_ID          
 AND  ADDS.POLICY_VERSION_ID = ADAV.POLICY_VERSION_ID          
 AND  ADDS.DRIVER_ID = ADAV.DRIVER_ID          
 where           
 ADAV.APP_VEHICLE_PRIN_OCC_ID<>11931          
 AND          
 ADDS.CUSTOMER_ID=@CUSTOMERID and ADDS.POLICY_ID=@POLICYID and ADDS.POLICY_VERSION_ID=@POLICYVERSIONID AND ADAV.VEHICLE_ID=@VEHICLEID AND ADDS.DRIVER_INCOME <>  11414 AND ADDS.IS_ACTIVE='Y' and ADDS.DRIVER_ID=@RATEDDRIVER)                                



      
   begin                               
      set @DRIVERINCOME='HIGH'                
   end                     
   else                              
   begin                               
      set @DRIVERINCOME='LOW'                                    
   end            
-- If any one of the assigned drivers has NODEPENDENT = NDEP then send <NODEPENDENT>NDEP</NODEPENDENT>                              
 if exists(select NO_DEPENDENTS from POL_DRIVER_DETAILS ADDS WITH (NOLOCK)          
 inner join POL_DRIVER_ASSIGNED_VEHICLE ADAV WITH (NOLOCK) on          
  ADDS.CUSTOMER_ID = ADAV.CUSTOMER_ID          
 AND  ADDS.POLICY_ID = ADAV.POLICY_ID          
 AND  ADDS.POLICY_VERSION_ID = ADAV.POLICY_VERSION_ID          
 AND  ADDS.DRIVER_ID = ADAV.DRIVER_ID          
 where           
 ADAV.APP_VEHICLE_PRIN_OCC_ID<>11931          
 AND          
 ADDS.CUSTOMER_ID=@CUSTOMERID and ADDS.POLICY_ID=@POLICYID and ADDS.POLICY_VERSION_ID=@POLICYVERSIONID AND ADAV.VEHICLE_ID=@VEHICLEID AND  ADDS.NO_DEPENDENTS=11588 AND ADDS.IS_ACTIVE='Y' and ADDS.DRIVER_ID=@RATEDDRIVER)                                   


 

  
   begin                               
      set @NODEPENDENT='NDEP'                                    
 end                      
   else                              
   begin                               
    set   @NODEPENDENT='1MORE'                                    
   end                                
                              
-- @WAIVEWORKLOSS Is Handled Through Covg/Endorsement (A-94 and 95) ID=43                                
                         
-- if exists (select  ENDORSEMENT_ID from POL_VEHICLE_ENDORSEMENTS WITH (NOLOCK)                              
--  WHERE CUSTOMER_ID=@CUSTOMERID AND POLICY_ID=@POLICYID AND POLICY_VERSION_ID=@POLICYVERSIONID AND VEHICLE_ID=@VEHICLEID AND ENDORSEMENT_ID=43)                                
--  set  @WAIVEWORKLOSS = 'TRUE'                                
-- else             
--  set  @WAIVEWORKLOSS = 'FALSE'       
    
SET  @WAIVEWORKLOSS = 'FALSE'                                                   
 IF EXISTS (SELECT DRIVER_ID FROM POL_DRIVER_DETAILS  WITH (NOLOCK) WHERE CUSTOMER_ID=@CUSTOMERID AND POLICY_ID=@POLICYID AND POLICY_VERSION_ID=@POLICYVERSIONID    
AND (DATEDIFF(DAY,POL_DRIVER_DETAILS.DRIVER_DOB,@QUOTEEFFECTIVEDATE) >= @WAIVERWORKLOSSEFFECTIVEDAYS) AND WAIVER_WORK_LOSS_BENEFITS=1 and DRIVER_ID=@RATEDDRIVER and IS_ACTIVE='Y')    
BEGIN    
 SET  @WAIVEWORKLOSS = 'TRUE'      
END 
    
-- <001 end>                                      
                                              
                                        
/*          Get The @SUMOFACCIDENTPOINTS EQUAL TO Sum of all Accidents points in previous 3 yrs  */              
      /* ACCIDENT  VIOLATION AFTER ACCIDENT=728  *****/                                 
                           
                             
     
 
       /*Get Tha Appeffective Date  */  
 DECLARE @APPEFFECTIVEDATE datetime                                           
 --DECLARE @MVR INT                                                      
 SELECT @APPEFFECTIVEDATE = convert(char(10),APP_EFFECTIVE_DATE,101)    FROM POL_CUSTOMER_POLICY_LIST  WITH (NOLOCK)                                                       
 WHERE CUSTOMER_ID=@CUSTOMERID AND POLICY_ID=@POLICYID AND POLICY_VERSION_ID=@POLICYVERSIONID                                                     
                                        
           /* 25 april 2006    SELECT @MVR= SUM(MV.MVR_POINTS)                                    
                FROM MNT_VIOLATIONS MV  WITH (NOLOCK)                                            
                INNER JOIN POL_MVR_INFORMATION AMI WITH (NOLOCK) ON MV.VIOLATION_ID=AMI.VIOLATION_ID                                                      
                WHERE  AMI.CUSTOMER_ID =@CUSTOMERID AND AMI.POLICY_ID=@POLICYID  AND AMI.POLICY_VERSION_ID=@POLICYVERSIONID                                             
AND AMI.DRIVER_ID in             
                           ( SELECT DRIVER_ID FROM POL_DRIVER_DETAILS WITH (NOLOCK) WHERE CUSTOMER_ID=@CUSTOMERID AND POLICY_ID=@POLICYID AND POLICY_VERSION_ID=@POLICYVERSIONID                      
    AND   DRIVER_DRIV_TYPE= 11603)                                     
                AND  VIOLATION_TYPE IN( 728,270)              
                And  AMI.MVR_DATE >=(DATEADD(YEAR,-3,@APPEFFECTIVEDATE))--  AND AMI.MVR_DATE <= @APPEFFECTIVEDATE )                                     
                                                    
                   IF  @MVR IS NULL         
                     SET @SUMOFACCIDENTPOINTS=0                                       
     ELSE                                        
          SET @SUMOFACCIDENTPOINTS=@MVR                */                        
        
                                         
/*              Get The @SUMOFVIOLATIONPOINTS EQUAL TO Sum of all MVR points in previous 2 yrs OTHER THAN ACCIDENT POINTS  */                             
                                                   
          /*25 april 2005      SELECT @MVR= SUM(MV.MVR_POINTS)                                                        
                FROM MNT_VIOLATIONS MV  WITH (NOLOCK)                                                    
                INNER JOIN POL_MVR_INFORMATION AMI WITH (NOLOCK) ON MV.VIOLATION_ID=AMI.VIOLATION_ID                    
WHERE  AMI.CUSTOMER_ID =@CUSTOMERID AND AMI.POLICY_ID=@POLICYID  AND AMI.POLICY_VERSION_ID=@POLICYVERSIONID                                             
                AND AMI.DRIVER_ID in                                        
                              ( SELECT DRIVER_ID FROM POL_DRIVER_DETAILS WITH (NOLOCK) WHERE CUSTOMER_ID=@CUSTOMERID AND POLICY_ID=@POLICYID AND POLICY_VERSION_ID=@POLICYVERSIONID                                                     
     
       AND   DRIVER_DRIV_TYPE= 11603)                          
               AND  VIOLATION_TYPE NOT IN( 728 ,270)                
                And AMI.MVR_DATE >=(DATEADD(YEAR,-2,@APPEFFECTIVEDATE))--  AND AMI.MVR_DATE <= @APPEFFECTIVEDATE )  
         
           IF  @MVR IS NULL                                        
                SET @SUMOFVIOLATIONPOINTS=0                                        
           ELSE                                        
               SET @SUMOFVIOLATIONPOINTS=@MVR */                             
                             
                         
                 
                    
                                        
/********************END OF DRIVER SECTION ***********************************/     
                             
                                      
 /* TO BE REMOVED LATER WHICLE IMPLEMENTING COVERAGES */ 
 /***************************** START **************************************/                                                            
 -- SET @SUMOFACCIDENTPOINTS   ='0'                      
--  SET @SUMOFVIOLATIONPOINTS   ='0'                                                            
  /* SET @COMPREHENSIVEDEDUCTIBLE ='200'                                                            
  SET @COLLISIONDEDUCTIBLE  ='150 BROAD'                                 
  SET @COVGCOLLISIONTYPE   ='BROAD'                                                            
  SET @COVGCOLLISIONDEDUCTIBLE   ='150'                                 
SET @ROADSERVICE   ='50'                                                            
  SET @RENTALREIMBURSEMENT   ='20/600'                        
  SET @RENTALREIMLIMITDAY   ='20'                                                            
  SET @RENTALREIMMAXCOVG   ='600'                                                            
 SET @MINITORTPDLIAB   ='TRUE'                
  SET @LOANLEASEGAP   ='LOAN'       
  SET @IS200SOUNDREPRODUCING  ='TRUE'                      
  SET @SOUNDRECEIVINGTRANSMITTINGSYSTEM   ='800'              
  SET @INSURANCEAMOUNT   ='20000'                                                            
  SET @EXTRAEQUIPCOMPREHENSIVEDEDUCTIBLE  ='500'                                               
  SET @COLLISIONTYPEDED  ='500 BROAD'                        
  SET @EXTRAEQUIPCOLLISIONTYPE  ='BROAD'                                                            
  SET @EXTRAEQUIPCOLLISIONDEDUCTIBLE   ='500'*/                                                            
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
 AND @QUOTEEFFDATE BETWEEN EFFECTIVE_FROM_DATE  AND ISNULL(EFFECTIVE_TO_DATE,'3000-03-16 16:59:06.630') AND AUTO_VEHICLE_TYPE
 IS NULL )                                                          
 
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
    
             
/*SET @GOODSTUDENT  = 'TRUE'     
DECLARE @COUNT_DRIVERS INT             
select     
 @COUNT_DRIVERS = COUNT(ADDS.CUSTOMER_ID) from  POL_DRIVER_ASSIGNED_VEHICLE adds WITH (NOLOCK) inner join POL_VEHICLES av  WITH (NOLOCK)          
on av.CUSTOMER_ID=adds.CUSTOMER_ID and            
   av.POLICY_ID=adds.POLICY_ID and         
   av.POLICY_VERSION_ID = adds.POLICY_VERSION_ID and           
  av.VEHICLE_ID=adds.VEHICLE_ID            
inner join POL_DRIVER_DETAILS with(nolock)             
on adds.CUSTOMER_ID=POL_DRIVER_DETAILS.CUSTOMER_ID and            
   adds.POLICY_ID=POL_DRIVER_DETAILS.POLICY_ID and            
   adds.POLICY_VERSION_ID = POL_DRIVER_DETAILS.POLICY_VERSION_ID              
and  adds.DRIVER_ID=POL_DRIVER_DETAILS.DRIVER_ID            
 where adds.CUSTOMER_ID=@CUSTOMERID AND adds.POLICY_ID=@POLICYID AND adds.POLICY_VERSION_ID=@POLICYVERSIONID and adds.VEHICLE_ID=@VEHICLEID     
AND (POL_DRIVER_DETAILS.DRIVER_GOOD_STUDENT=1 and POL_DRIVER_DETAILS.FULL_TIME_STUDENT=1)     
and (datediff(day,DRIVER_DOB,@QUOTEEFFECTIVEDATE) < 9125) and adds.APP_VEHICLE_PRIN_OCC_ID<>11931           
IF(@COUNT_DRIVERS > 0)       
BEGIN           
 SET @GOODSTUDENT = 'TRUE'    
 END    
ELSE    
BEGIN    
 SET @GOODSTUDENT = 'FALSE'    
END    
  */    
DECLARE @COUNT_ACCIDENTS INT,    
		@ACCIDENT_POINTS INT ,
        @SUM_MVR_POINTS INT    
SET @SUM_MVR_POINTS=0    
 SET @ACCIDENT_POINTS=0 

--------------------------------------------------------------------
--    aSSIGNED dRIVER ACCIDENT AND VIOLATION POINT(START)
--------------------------------------------------------------------  
DECLARE @ASSIGNDRIVERIDS INT
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
SELECT ADDS.DRIVER_ID FROM  POL_DRIVER_ASSIGNED_VEHICLE ADDS WITH (NOLOCK) INNER JOIN POL_VEHICLES AV WITH (NOLOCK)                      
ON AV.CUSTOMER_ID=ADDS.CUSTOMER_ID AND                      
   AV.POLICY_ID=ADDS.POLICY_ID AND                      
   AV.POLICY_VERSION_ID = ADDS.POLICY_VERSION_ID AND                   
   AV.VEHICLE_ID=ADDS.VEHICLE_ID         
WHERE AV.CUSTOMER_ID=@CUSTOMERID AND AV.POLICY_ID=@POLICYID AND AV.POLICY_VERSION_ID=@POLICYVERSIONID AND AV.VEHICLE_ID=@VEHICLEID AND     
ADDS.APP_VEHICLE_PRIN_OCC_ID IN (11398,11925,11927,11929)    
SET @IDENT_COLMN = 1     
WHILE (1= 1)  
BEGIN     
 IF NOT EXISTS (SELECT IDEN_ID FROM #ASSIGNDRIVSPRMSAFE  WITH(NOLOCK) WHERE IDEN_ID = @IDENT_COLMN)     
 BEGIN     
  BREAK    
 END    
 SELECT  @ASSIGNDRIVERIDS = ASSIGNPRESAFEDRIVERIDS    
 FROM #ASSIGNDRIVSPRMSAFE  WITH(NOLOCK)    
 WHERE IDEN_ID = @IDENT_COLMN   
 INSERT INTO #ASSIGNDRIVSPRMSAFE_DRIVER_VIOLATION_ACCIDENT EXEC GetMVRViolationPointsPol @CUSTOMERID,@POLICYID,@POLICYVERSIONID ,@ASSIGNDRIVERIDS,@ACCIDENT_NUM_YEAR,@VIOLATION_NUM_YEAR,@VIOLATION_NUM_YEAR_REFER,@ACCIDENT_PAID_AMOUNT               
 
IF EXISTS (SELECT VIOLATION_ID FROM POL_MVR_INFORMATION WITH (NOLOCK) WHERE                                      
           CUSTOMER_ID =@CUSTOMERID AND                     
           POLICY_ID=@POLICYID  AND                                     
           POLICY_VERSION_ID=@POLICYVERSIONID AND                                     
          (VIOLATION_ID=182 OR VIOLATION_ID=640) AND VIOLATION_TYPE =602 AND IS_ACTIVE='Y' AND DRIVER_ID=@ASSIGNDRIVERIDS)                                      
			BEGIN
				SET  @WEARINGSEATBELT='FALSE' 	
			END
	
	SET @IDENT_COLMN = @IDENT_COLMN + 1    
END      
SELECT @PREMIER_SAFE_SUM_MVR_POINTS = SUM(ISNULL(SUM_MVR_POINTS,0)), @PREMIER_SAFE_ACCIDENT_POINTS = SUM(ISNULL(ACCIDENT_POINTS,0)) FROM  #ASSIGNDRIVSPRMSAFE_DRIVER_VIOLATION_ACCIDENT
   SET @PREMIER_SAFE_SUM_MVR_POINTS = @PREMIER_SAFE_SUM_MVR_POINTS + @PREMIER_SAFE_ACCIDENT_POINTS

DROP TABLE #ASSIGNDRIVSPRMSAFE_DRIVER_VIOLATION_ACCIDENT     
DROP TABLE #ASSIGNDRIVSPRMSAFE    
--------------------------------------------------------------------
--    ASSIGNED dRIVER ACCIDENT AND VIOLATION POINT(END)
--------------------------------------------------------------------  

-----------------------------------------------------------------------    
--    RATED DRIVER VIOLATION AND ACCIDENT (sTART)    
----------------------------------------------------------------------    
DECLARE @ASSPOINTDRIVER NVARCHAR(20)    
SELECT @RATEDDRIVER = CLASS_DRIVERID FROM POL_VEHICLES AV  WITH (NOLOCK)  WHERE CUSTOMER_ID= @CUSTOMERID AND POLICY_ID= @POLICYID AND POLICY_VERSION_ID=@POLICYVERSIONID AND  VEHICLE_ID=@VEHICLEID                                               
SELECT @ASSPOINTDRIVER = CONVERT(NVARCHAR(50),ADDS.DRIVER_ID) FROM  POL_DRIVER_ASSIGNED_VEHICLE ADDS WITH (NOLOCK) INNER JOIN POL_VEHICLES AV WITH (NOLOCK)                      
ON AV.CUSTOMER_ID=ADDS.CUSTOMER_ID AND      
   AV.POLICY_ID=ADDS.POLICY_ID AND         
   AV.POLICY_VERSION_ID = ADDS.POLICY_VERSION_ID AND                   
   AV.VEHICLE_ID=ADDS.VEHICLE_ID AND    
   AV.CLASS_DRIVERID = ADDS.DRIVER_ID      
WHERE AV.CUSTOMER_ID=@CUSTOMERID AND AV.POLICY_ID=@POLICYID AND AV.POLICY_VERSION_ID=@POLICYVERSIONID AND AV.VEHICLE_ID=@VEHICLEID AND     
ADDS.APP_VEHICLE_PRIN_OCC_ID IN (11398,11925,11927,11929)    
    
CREATE TABLE #RATEDDRIVER_VIOLATION_ACCIDENT    
(    
 [SUM_MVR_POINTS]	INT,                       
 [ACCIDENT_POINTS]  INT,          
 [COUNT_ACCIDENTS]  INT,    
 [MVR_POINTS]       INT    
)     
IF(@ASSPOINTDRIVER IS NOT NULL)    
BEGIN             
INSERT INTO #RATEDDRIVER_VIOLATION_ACCIDENT EXEC GetMVRViolationPointsPol @CUSTOMERID,@POLICYID,@POLICYVERSIONID ,@RATEDDRIVER,@ACCIDENT_NUM_YEAR,@VIOLATION_NUM_YEAR,@VIOLATION_NUM_YEAR_REFER,@ACCIDENT_PAID_AMOUNT               
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
-- Rated Driver OverRide (Itrack 4544) (Start)    
-----------------------------------------------    
SET @GOODSTUDENT = 'FALSE'    
    
DECLARE @GOODDRIVER NVARCHAR(100)    
SELECT @GOODDRIVER = ADDS.DRIVER_ID FROM  POL_DRIVER_ASSIGNED_VEHICLE ADDS WITH (NOLOCK) INNER JOIN POL_VEHICLES AV WITH (NOLOCK)                      
ON AV.CUSTOMER_ID=ADDS.CUSTOMER_ID AND                      
   AV.POLICY_ID=ADDS.POLICY_ID AND            
   AV.POLICY_VERSION_ID = ADDS.POLICY_VERSION_ID AND                       
  AV.VEHICLE_ID=ADDS.VEHICLE_ID and    
AV.CLASS_DRIVERID = ADDS.DRIVER_ID               
INNER JOIN POL_DRIVER_DETAILS WITH (NOLOCK)                      
ON ADDS.CUSTOMER_ID=POL_DRIVER_DETAILS.CUSTOMER_ID AND                      
   ADDS.POLICY_ID=POL_DRIVER_DETAILS.POLICY_ID AND                      
   ADDS.POLICY_VERSION_ID = POL_DRIVER_DETAILS.POLICY_VERSION_ID                        
AND  ADDS.DRIVER_ID=POL_DRIVER_DETAILS.DRIVER_ID                      
 WHERE ADDS.CUSTOMER_ID=@CUSTOMERID AND ADDS.POLICY_ID=@POLICYID AND ADDS.POLICY_VERSION_ID=@POLICYVERSIONID AND ADDS.VEHICLE_ID=@VEHICLEID      
AND (POL_DRIVER_DETAILS.DRIVER_GOOD_STUDENT=1 AND POL_DRIVER_DETAILS.FULL_TIME_STUDENT=1)     
AND (DATEDIFF(DAY,POL_DRIVER_DETAILS.DRIVER_DOB,@QUOTEEFFECTIVEDATE) < @TWENTYFIVEYEARDAYS) AND ADDS.APP_VEHICLE_PRIN_OCC_ID IN (11927,11928,11929,11930) and POL_DRIVER_DETAILS.IS_ACTIVE='Y'             
    
IF (@GOODDRIVER IS NOT NULL)    
BEGIN    
 SET @GOODSTUDENT='TRUE'    
END       
    
    
/*    
SELECT @SUM_MVR_POINTS = SUM(ISNULL(POINTS_ASSIGNED,0)) + SUM(ISNULL(ADJUST_VIOLATION_POINTS,0) )    
 FROM POL_MVR_INFORMATION AMI  WITH (NOLOCK) INNER JOIN POL_DRIVER_ASSIGNED_VEHICLE ADDE  WITH (NOLOCK)    
   ON AMI.CUSTOMER_ID = ADDE.CUSTOMER_ID    
 AND  AMI.POLICY_ID = ADDE.POLICY_ID    
 AND AMI.POLICY_VERSION_ID = ADDE.POLICY_VERSION_ID    
 AND AMI.DRIVER_ID = ADDE.DRIVER_ID    
 WHERE  AMI.CUSTOMER_ID=@CUSTOMERID AND AMI.POLICY_ID=@POLICYID AND AMI.POLICY_VERSION_ID=@POLICYVERSIONID AND AMI.DRIVER_ID =@RATEDDRIVER AND AMI.IS_ACTIVE = 'Y' AND ADDE.APP_VEHICLE_PRIN_OCC_ID IN (11929,11927)    
     
SELECT @COUNT_ACCIDENTS=COUNT(CUSTOMER_ID) FROM FETCH_ACCIDENT with(nolock)     
   WHERE CUSTOMER_ID = @CUSTOMERID AND     
     (POLICY_ID IS NULL or POLICY_ID=@POLICYID) AND (POLICY_VERSION_ID IS NULL OR POLICY_VERSION_ID=@POLICYVERSIONID)                                 
AND LOB=2 AND   AT_FAULT=10963 and                      
   ((ISNULL(YEAR(@QUOTEEFFECTIVEDATE),0) - ISNULL(YEAR(ISNULL(OCCURENCE_DATE,0)),0))<=3 and                                            
    (ISNULL(YEAR(@QUOTEEFFECTIVEDATE),0) - ISNULL(YEAR(ISNULL(OCCURENCE_DATE,0)),0))>=0) and          
   (((DRIVER_NAME = '' and DRIVER_ID=@RATEDDRIVER) OR DRIVER_NAME=(CAST(@CUSTOMERID AS VARCHAR) + '^' + CAST(@POLICYID AS VARCHAR) +  '^' +                       
           CAST(@POLICYVERSIONID AS VARCHAR) + '^' + CAST(@RATEDDRIVER AS VARCHAR) + '^' + 'POL')))     
 
IF (@COUNT_ACCIDENTS>0)               
  SET @ACCIDENT_POINTS = (ISNULL(@COUNT_ACCIDENTS,0) * 4 ) - 1     
      
  SET @SUM_MVR_POINTS= @SUM_MVR_POINTS + @ACCIDENT_POINTS    
     */    
     IF(@SUM_MVR_POINTS < 3 AND @GOODDRIVER IS NOT NULL)    
   BEGIN    
    SET @GOODSTUDENT='TRUE'    
   END    
  ELSE    
            BEGIN     
    SET @GOODSTUDENT='FALSE'    
  END 
    
-----------------------------------------------    
-- Rated Driver OverRide (Itrack 4544) (End)    
-----------------------------------------------  
--------------------------------------------------------------------------------------------
------------------ Insurance Score(Start)    
--------------------------------------------------------------------------------------------
	SELECT  @PROCCESSID=PROCESS_ID,    
			@CACELATION_TYPE=CANCELLATION_TYPE,    
			@PROCESS_STATUS=PROCESS_STATUS    
			FROM POL_POLICY_PROCESS WITH (NOLOCK)            
			WHERE CUSTOMER_ID=@CUSTOMERID AND POLICY_ID=@POLICYID AND NEW_POLICY_VERSION_ID=@POLICYVERSIONID     
    
IF((@PROCCESSID=4 OR @PROCCESSID=16) AND  @CACELATION_TYPE=14244 AND @PROCESS_STATUS != 'ROLLBACK')    
	BEGIN    
	 SELECT @INSURANCESCORE =CASE CONVERT(NVARCHAR(20),ISNULL(CUSTOMER_INSURANCE_SCORE,-1))       --BY DEFAULT VALUE FOR SCORE IS 100                                             
							 WHEN -1 THEN '100'            
							 WHEN  -2 THEN 'NOHITNOSCORE'             
					  ELSE CONVERT(NVARCHAR(20),CUSTOMER_INSURANCE_SCORE) END     
			FROM   CLT_CUSTOMER_LIST  WITH (NOLOCK)                                                       
			WHERE  CUSTOMER_ID =@CUSTOMERID    
	END    
ELSE    
	BEGIN              
		SELECT     
			@INSURANCESCORE=CASE CONVERT(NVARCHAR(20),(ISNULL(APPLY_INSURANCE_SCORE,-1)))                         
							WHEN -1 THEN '100'            
							WHEN  -2 THEN 'NOHITNOSCORE'             
							ELSE CONVERT(NVARCHAR(20),APPLY_INSURANCE_SCORE)     
							END            
		FROM POL_CUSTOMER_POLICY_LIST WITH (NOLOCK)                   
		WHERE CUSTOMER_ID =@CUSTOMERID AND POLICY_ID= @POLICYID AND POLICY_VERSION_ID=@POLICYVERSIONID      
	END    

--------------------------------------------------------------------------------------------
------------------ Insurance Score(END)    
-------------------------------------------------------------------------------------------- 
---------------------------------------------------------------------------------------------------- 
	-- QUALIFIESTRAIBLAZERPROGRAM (START)
----------------------------------------------------------------------------------------------------   
---------------------------------------Added By Praveen kumar---------  
	
	SET @QUALIFIESTRAIBLAZERPROGRAM = 'N'  
	SELECT     
		@STATENAME = UPPER(STATE_NAME) ,                         
        @TERMFACTOR = POLICY_TERMS,                            
        @QUOTEEFFDATE = CONVERT(CHAR(10),POL_VER_EFFECTIVE_DATE,101) ,    
		@POLICYEFFECTIVEDATE = APP_EFFECTIVE_DATE,                 
        @QUALIFIESTRAIBLAZERPROGRAM = CASE ISNULL(POLICY_SUBLOB,'0')      
										WHEN '1' THEN 'Y'             
										ELSE 'N'          
										END,                        
        @LOBID=POLICY_LOB                         
	FROM POL_CUSTOMER_POLICY_LIST  WITH (NOLOCK) INNER JOIN MNT_COUNTRY_STATE_LIST WITH (NOLOCK)     
	ON MNT_COUNTRY_STATE_LIST.STATE_ID=POL_CUSTOMER_POLICY_LIST.STATE_ID                           
	WHERE CUSTOMER_ID= @CUSTOMERID AND POLICY_ID= @POLICYID AND POLICY_VERSION_ID=@POLICYVERSIONID           
  IF(@QUALIFIESTRAIBLAZERPROGRAM='Y')
	BEGIN                    
	  -- MAKE TEMP TABLE TO STORE ALL ASSIGNED DRIVER BELOW 25 YEARS AGE
		CREATE TABLE #ASSIGNDRIVTRAIL                  
			(         
			  [IDEN_ID] INT IDENTITY(1,1) NOT NULL,                    
			  [ASSIGNDRIVERIDS] INT                   
			)    
		    
			INSERT INTO #ASSIGNDRIVTRAIL  
			SELECT ADDS.DRIVER_ID FROM  POL_DRIVER_ASSIGNED_VEHICLE ADDS WITH (NOLOCK) INNER JOIN POL_VEHICLES AV WITH (NOLOCK)           
				ON	AV.CUSTOMER_ID=ADDS.CUSTOMER_ID AND                      
					AV.POLICY_ID=ADDS.POLICY_ID AND                      
					AV.POLICY_VERSION_ID = ADDS.POLICY_VERSION_ID AND                       
					AV.VEHICLE_ID=ADDS.VEHICLE_ID AND
					AV.CLASS_DRIVERID = ADDS.DRIVER_ID                      
			INNER JOIN POL_DRIVER_DETAILS WITH (NOLOCK)         
				ON	ADDS.CUSTOMER_ID=POL_DRIVER_DETAILS.CUSTOMER_ID AND                      
					ADDS.POLICY_ID=POL_DRIVER_DETAILS.POLICY_ID AND                      
					ADDS.POLICY_VERSION_ID = POL_DRIVER_DETAILS.POLICY_VERSION_ID                 
					AND ADDS.DRIVER_ID=POL_DRIVER_DETAILS.DRIVER_ID                      
			WHERE ADDS.CUSTOMER_ID=@CUSTOMERID AND ADDS.POLICY_ID=@POLICYID AND ADDS.POLICY_VERSION_ID=@POLICYVERSIONID AND ADDS.VEHICLE_ID=@VEHICLEID      
			AND (DATEDIFF(DAY,POL_DRIVER_DETAILS.DRIVER_DOB,@QUOTEEFFECTIVEDATE) < @TWENTYFIVEYEARDAYS) AND ADDS.APP_VEHICLE_PRIN_OCC_ID IN (11927,11928,11929,11930) AND POL_DRIVER_DETAILS.IS_ACTIVE='Y'            

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
			SELECT PDDS.DRIVER_ID FROM  POL_DRIVER_ASSIGNED_VEHICLE PDDS WITH (NOLOCK) INNER JOIN POL_VEHICLES PV WITH (NOLOCK)                      
					ON	PV.CUSTOMER_ID=PDDS.CUSTOMER_ID AND                      
						PV.POLICY_ID=PDDS.POLICY_ID AND                      
						PV.POLICY_VERSION_ID = PDDS.POLICY_VERSION_ID AND         
						PV.VEHICLE_ID=PDDS.VEHICLE_ID 
						INNER JOIN POL_DRIVER_DETAILS WITH (NOLOCK)                      
					ON  PDDS.CUSTOMER_ID=POL_DRIVER_DETAILS.CUSTOMER_ID AND                      
						PDDS.POLICY_ID=POL_DRIVER_DETAILS.POLICY_ID AND               
						PDDS.POLICY_VERSION_ID = POL_DRIVER_DETAILS.POLICY_VERSION_ID AND          
						PDDS.DRIVER_ID=POL_DRIVER_DETAILS.DRIVER_ID                      
						WHERE PDDS.CUSTOMER_ID=@CUSTOMERID AND PDDS.POLICY_ID=@POLICYID AND PDDS.POLICY_VERSION_ID=@POLICYVERSIONID AND PDDS.VEHICLE_ID=@VEHICLEID      
						AND PDDS.APP_VEHICLE_PRIN_OCC_ID IN (11398,11399,11925,11926,11927,11928,11929,11930)   and POL_DRIVER_DETAILS.IS_ACTIVE='Y'


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
		        
						IF EXISTS (SELECT CUSTOMER_ID FROM           
							POL_MVR_INFORMATION A with(nolock)                                            
							LEFT OUTER JOIN              
							MNT_VIOLATIONS M with(nolock)                                          
							ON  A.VIOLATION_TYPE = M.VIOLATION_ID            
							WHERE                            
							A.CUSTOMER_ID = @CUSTOMERID AND                   
							A.POLICY_ID = @POLICYID AND                                            
							A.POLICY_VERSION_ID = @POLICYVERSIONID AND                                            
							A.DRIVER_ID = @ASSIGNDRIVERIDSS AND                                            
							NOT ISNULL(MVR_POINTS,0)<0 
					   		AND DATEDIFF(DAY,MVR_DATE ,@QUOTEEFFECTIVEDATE) <=@MAJORVIOLATIONEFFECTIVEDAYS        
							AND A.IS_ACTIVE='Y' AND  isnull(M.VIOLATION_CODE,'') IN('10000','40000','SUSPN'))     
					   		BEGIN
								SET @QUALIFIESTRAIBLAZERPROGRAM='N'
							END
						SET @IDENT_COLS = @IDENT_COLS + 1    
					END 	    
			DROP TABLE #ASSIGNDRIVTRAILVIO   


----------------- if renewal process launch then insurance score condition will be changed to 701 from 750
			DECLARE @HAVE_RENEWED NVARCHAR(20)
			SET @HAVE_RENEWED = 'N'
			IF EXISTS(SELECT PROCESS_ID FROM POL_POLICY_PROCESS   WITH(NOLOCK)                        
				WHERE CUSTOMER_ID=@CUSTOMERID AND POLICY_ID=@POLICYID AND PROCESS_STATUS!='ROLLBACK' AND PROCESS_ID IN (5,18))
					BEGIN
						SET @HAVE_RENEWED='Y'
					END

			IF(@HAVE_RENEWED='N')
				BEGIN
				-- CHECK ANY PRIOR LOSS ABOVE $750 EXCEPT RENWAL
				IF EXISTS (SELECT CUSTOMER_ID  FROM CLM_CLAIM_INFO with (NOLOCK) WHERE CUSTOMER_ID=@CUSTOMERID AND LOB_ID=2 and DATEDIFF(DAY,isnull(CLM_CLAIM_INFO.LOSS_DATE,'0'),@QUOTEEFFECTIVEDATE)<=@THREEYEARDAYS    
					AND (ISNULL(PAID_LOSS,0)+ ISNULL(PAID_EXPENSE,0) > 750))
					BEGIN
						SET @QUALIFIESTRAIBLAZERPROGRAM='N'
					END
				IF EXISTS (SELECT CUSTOMER_ID FROM APP_PRIOR_LOSS_INFO with (NOLOCK) WHERE CUSTOMER_ID=@CUSTOMERID AND LOB=2 AND (datediff(day,APP_PRIOR_LOSS_INFO.OCCURENCE_DATE,@QUOTEEFFECTIVEDATE) <= @THREEYEARDAYS) AND AMOUNT_PAID > 750)
					BEGIN
						SET @QUALIFIESTRAIBLAZERPROGRAM='N'
					END
				END

			-- DATE CHECK HAS BEEN APPLIED ON QUALIFIES TRAIBLAZERPROGRAM         
			--Calculate    @QUALIFIESTRAIBLAZERPROGRAM if @INSURANCESCORE is not NOHITNOSCORE : praveen 02/20/2008    
			if(@INSURANCESCORE <> 'NOHITNOSCORE')    
			  BEGIN    
				IF (@QUALIFIESTRAIBLAZERPROGRAM = 'Y')    
					IF ((DATEDIFF(DAY,@POLICYEFFECTIVEDATE,'2007-08-01 00:00:00.000') < 0)AND @INSURANCESCORE > 750 AND (@PROCCESSID<>5 OR @PROCCESSID<>18) AND @HAVE_RENEWED='N')     
					  BEGIN    
							SET @QUALIFIESTRAIBLAZERPROGRAM = 'Y'    
					  END  
					ELSE IF(@HAVE_RENEWED='Y' AND @INSURANCESCORE >700)  
						BEGIN  
							SET @QUALIFIESTRAIBLAZERPROGRAM = 'Y'    
						END     
					ELSE IF ((DATEDIFF(DAY,@POLICYEFFECTIVEDATE,'2007-08-01 00:00:00.000') > 0) AND @INSURANCESCORE > 700)     
						BEGIN    
							SET @QUALIFIESTRAIBLAZERPROGRAM = 'Y'    
						END   
					ELSE  
						BEGIN  
							SET @QUALIFIESTRAIBLAZERPROGRAM = 'N'    
						END   
			  END
			ELSE IF   (@INSURANCESCORE = 'NOHITNOSCORE')
				SET @QUALIFIESTRAIBLAZERPROGRAM = 'N'     
    
 END
ELSE
	BEGIN
		SET @QUALIFIESTRAIBLAZERPROGRAM='N'
	END
--------------------------------------END-----------------------------  
 ----------------------------------------------------------------------------------------------------
	-- QUALIFIESTRAIBLAZERPROGRAM (END)
----------------------------------------------------------------------------------------------------        
-----------------------------------------------------------------------------------------------------    
--         GOOD STUDENT DISCOUNT (END)    
-----------------------------------------------------------------------------------------------------    
    
     --         Premier Driver DISCOUNT (START) (Driver tab Yes or No)   
	-- As discussed with ravinder we will consider policy base version completed datetime for same term in claim loss
	-- loss calculation
DECLARE @PROCESSCOMPLETED_DATETIME DATETIME
DECLARE @BASE_POLICY_VERSION INT
IF(@PROCCESSID IN (5,18,25,24,31,32))
	BEGIN
		SELECT @PROCESSCOMPLETED_DATETIME=ISNULL(COMPLETED_DATETIME,@QUOTEEFFECTIVEDATE) FROM  POL_POLICY_PROCESS with(nolock)
		WHERE CUSTOMER_ID=@CUSTOMERID AND POLICY_ID=@POLICYID AND NEW_POLICY_VERSION_ID=@POLICYVERSIONID
	END
ELSE
	BEGIN
		SELECT @BASE_POLICY_VERSION=MAX(NEW_POLICY_VERSION_ID) FROM POL_POLICY_PROCESS  with(nolock)
		WHERE CUSTOMER_ID=@CUSTOMERID AND POLICY_ID=@POLICYID AND NEW_POLICY_VERSION_ID<=@POLICYVERSIONID AND 
		PROCESS_ID IN (5,18,25,24,31,32)
		SELECT @PROCESSCOMPLETED_DATETIME=ISNULL(COMPLETED_DATETIME,@QUOTEEFFECTIVEDATE) FROM  POL_POLICY_PROCESS  with(nolock)
		WHERE CUSTOMER_ID=@CUSTOMERID AND POLICY_ID=@POLICYID AND NEW_POLICY_VERSION_ID=@BASE_POLICY_VERSION
	END

CREATE TABLE #ASSIGNDRIV                  
(         
  [IDEN_ID] INT IDENTITY(1,1) NOT NULL,                    
  [ASSIGNDRIVERIDS] INT                   
)    
    
INSERT INTO #ASSIGNDRIV                                         
SELECT ADDS.DRIVER_ID FROM  POL_DRIVER_ASSIGNED_VEHICLE ADDS WITH (NOLOCK) INNER JOIN POL_VEHICLES AV WITH (NOLOCK)           
ON AV.CUSTOMER_ID=ADDS.CUSTOMER_ID AND                      
  AV.POLICY_ID=ADDS.POLICY_ID AND                      
   AV.POLICY_VERSION_ID = ADDS.POLICY_VERSION_ID AND                       
   AV.VEHICLE_ID=ADDS.VEHICLE_ID                      
   WHERE ADDS.CUSTOMER_ID=@CUSTOMERID AND ADDS.POLICY_ID=@POLICYID AND ADDS.POLICY_VERSION_ID=@POLICYVERSIONID AND ADDS.VEHICLE_ID=@VEHICLEID      
   AND ADDS.APP_VEHICLE_PRIN_OCC_ID IN (11398,11399,11925,11926,11927,11928,11929,11930)        
-- RUN WHILE LOOP TO CHECK  Driver tab Yes or No    
   
DECLARE	@IDENT_COL INT,    
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
		FROM #ASSIGNDRIV  WITH(NOLOCK)  WHERE IDEN_ID = @IDENT_COL     
     
	 SELECT @PREMIERYESNO =  isnull(DRIVER_PREF_RISK,0),
			@SAFEYESNO =  isnull(SAFE_DRIVER_RENEWAL_DISCOUNT,0)  
	 FROM POL_DRIVER_DETAILS WHERE CUSTOMER_ID=@CUSTOMERID AND POLICY_ID=@POLICYID AND POLICY_VERSION_ID=@POLICYVERSIONID AND DRIVER_ID =@ASSIGNDRIVERIDS AND IS_ACTIVE = 'Y'    
	     
		    
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

         --         Premier Driver DISCOUNT (END) (Driver tab Yes or No)    
--set @PREMIERDRIVER = 'TRUE'       
--    
--IF EXISTS (SELECT DRIVER_ID FROM POL_DRIVER_DETAILS with (NOLOCK) WHERE CUSTOMER_ID= @CUSTOMERID AND POLICY_ID= @POLICYID AND POLICY_VERSION_ID=@POLICYVERSIONID AND DRIVER_ID = @RATEDDRIVER AND isnull(DRIVER_PREF_RISK,0)=0)    
-- BEGIN    
--  SET  @PREMIERDRIVER = 'FALSE'     
-- END  
--  
     SELECT             
   @YEARSCONTINSUREDWITHWOLVERINE = isnull(YEARS_INSU_WOL,'')                                        
   FROM POL_AUTO_GEN_INFO WITH (NOLOCK) WHERE CUSTOMER_ID= @CUSTOMERID AND POLICY_ID= @POLICYID AND POLICY_VERSION_ID=@POLICYVERSIONID                                       
                                                        
   IF @YEARSCONTINSUREDWITHWOLVERINE  is null or @YEARSCONTINSUREDWITHWOLVERINE=''   
     SET  @YEARSCONTINSUREDWITHWOLVERINE='0'

--------------------------------------- Prior Loss information--------------------------------
  DECLARE @APPID INT    
  DECLARE @APPVERSIONID INT    
  DECLARE @LOSSCOUNTAPP INT   
  DECLARE @LOSSCOUNT INT    
  DECLARE @LOSSCOUNTDRIVERFREE INT     
  SELECT @APPID=APP_ID, @APPVERSIONID=APP_VERSION_ID     
  FROM POL_CUSTOMER_POLICY_LIST with (NOLOCK) WHERE CUSTOMER_ID=@CUSTOMERID AND POLICY_ID=@POLICYID AND POLICY_VERSION_ID=@POLICYVERSIONID    
  set @LOSSCOUNT=0 
 set @LOSSCOUNTDRIVERFREE=0   
  -- check paid loss for $75   

 IF(@STATEID = '22')    
	BEGIN    
		/*SELECT @LOSSCOUNTAPP=COUNT(LOSS_ID) FROM APP_PRIOR_LOSS_INFO with (NOLOCK) WHERE CUSTOMER_ID=@CUSTOMERID AND LOB=2 AND (datediff(day,APP_PRIOR_LOSS_INFO.OCCURENCE_DATE,@QUOTEEFFECTIVEDATE) <= @THREEYEARDAYS) AND AMOUNT_PAID > 75 AND     
		DRIVER_NAME=(CAST(@CUSTOMERID AS VARCHAR) + '^' + CAST(@APPID AS VARCHAR) +  '^' +                 
		CAST(@APPVERSIONID AS VARCHAR) + '^' + CAST(@RATEDDRIVER AS VARCHAR) + '^' + 'APP')    
    
		SELECT @LOSSCOUNT=COUNT(LOSS_ID) FROM APP_PRIOR_LOSS_INFO with (NOLOCK) WHERE CUSTOMER_ID=@CUSTOMERID AND LOB=2 AND (datediff(day,APP_PRIOR_LOSS_INFO.OCCURENCE_DATE,@QUOTEEFFECTIVEDATE) <= @THREEYEARDAYS) AND AMOUNT_PAID > 75    
		AND  ((DRIVER_NAME = '' and DRIVER_ID=@RATEDDRIVER) OR DRIVER_NAME=(CAST(@CUSTOMERID AS VARCHAR) + '^' + CAST(@POLICYID AS VARCHAR) +  '^' +                              
		CAST(@POLICYVERSIONID AS VARCHAR) + '^' + CAST(@RATEDDRIVER AS VARCHAR) + '^' + 'POL'))  */
	-----------------------------------------------If prior loss is not attached to a driver then apply it to all risk on that policy--------------------   
		IF (DATEDIFF(day,'07/01/2009',@QUOTEEFFECTIVEDATE)>=0)     
			BEGIN    
				SELECT @LOSSCOUNTDRIVERFREE=COUNT(LOSS_ID) FROM APP_PRIOR_LOSS_INFO with (NOLOCK) WHERE CUSTOMER_ID=@CUSTOMERID AND LOB=2 AND (datediff(day,APP_PRIOR_LOSS_INFO.OCCURENCE_DATE,@QUOTEEFFECTIVEDATE) <= @THREEYEARDAYS) AND AMOUNT_PAID > 750    
			END
		ELSE
			BEGIN
				SELECT @LOSSCOUNTDRIVERFREE=COUNT(LOSS_ID) FROM APP_PRIOR_LOSS_INFO with (NOLOCK) WHERE CUSTOMER_ID=@CUSTOMERID AND LOB=2 AND (datediff(day,APP_PRIOR_LOSS_INFO.OCCURENCE_DATE,@QUOTEEFFECTIVEDATE) <= @THREEYEARDAYS) AND AMOUNT_PAID > 75    
			END
	END    
 ELSE IF(@STATEID = '14')    
	BEGIN    
		/*SELECT @LOSSCOUNTAPP=COUNT(LOSS_ID) FROM APP_PRIOR_LOSS_INFO with (NOLOCK) WHERE CUSTOMER_ID=@CUSTOMERID AND LOB=2 AND (datediff(day,APP_PRIOR_LOSS_INFO.OCCURENCE_DATE,@QUOTEEFFECTIVEDATE) <= @THREEYEARDAYS) AND AMOUNT_PAID > 750 AND     
		DRIVER_NAME=(CAST(@CUSTOMERID AS VARCHAR) + '^' + CAST(@APPID AS VARCHAR) +  '^' +                                  
		CAST(@APPVERSIONID AS VARCHAR) + '^' + CAST(@RATEDDRIVER AS VARCHAR) + '^' + 'APP')    
    
		SELECT @LOSSCOUNT=COUNT(LOSS_ID) FROM APP_PRIOR_LOSS_INFO with (NOLOCK) WHERE CUSTOMER_ID=@CUSTOMERID AND LOB=2 AND (datediff(day,APP_PRIOR_LOSS_INFO.OCCURENCE_DATE,@QUOTEEFFECTIVEDATE) <= @THREEYEARDAYS) AND AMOUNT_PAID > 750    
		AND  ((DRIVER_NAME = '' and DRIVER_ID=@RATEDDRIVER) OR DRIVER_NAME=(CAST(@CUSTOMERID AS VARCHAR) + '^' + CAST(@POLICYID AS VARCHAR) +  '^' +                              
		CAST(@POLICYVERSIONID AS VARCHAR) + '^' + CAST(@RATEDDRIVER AS VARCHAR) + '^' + 'POL'))    */ 
	-----------------------------------------------If prior loss is not attached to a driver then apply it to all risk on that policy--------------------   
		SELECT @LOSSCOUNTDRIVERFREE=COUNT(LOSS_ID) FROM APP_PRIOR_LOSS_INFO with (NOLOCK) WHERE CUSTOMER_ID=@CUSTOMERID AND LOB=2 AND (datediff(day,APP_PRIOR_LOSS_INFO.OCCURENCE_DATE,@QUOTEEFFECTIVEDATE) <= @THREEYEARDAYS) AND AMOUNT_PAID > 750    
	END    
	--	SET @LOSSCOUNT = @LOSSCOUNT + @LOSSCOUNTAPP    
	----------------------------------------------------------------prior loss information--------------------------------------------------------------------------------     
    
IF(@PREMIERDRIVER ='TRUE')    
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
	     
	   -- PREMIER DRIVER DISCOUNT WILL NOW BE DECIDED ON RATED DRIVER           
	  DECLARE @COUNT_DRIVER INT            
	      
		SELECT @COUNT_DRIVER=COUNT(*) FROM  POL_DRIVER_ASSIGNED_VEHICLE ADDS WITH (NOLOCK) INNER JOIN POL_VEHICLES AV  WITH (NOLOCK)          
		ON	AV.CUSTOMER_ID=ADDS.CUSTOMER_ID AND            
			AV.POLICY_ID=ADDS.POLICY_ID AND            
			AV.POLICY_VERSION_ID = ADDS.POLICY_VERSION_ID AND             
			AV.VEHICLE_ID=ADDS.VEHICLE_ID            
		INNER JOIN POL_DRIVER_DETAILS   WITH (NOLOCK)          
		ON	ADDS.CUSTOMER_ID=POL_DRIVER_DETAILS.CUSTOMER_ID AND            
			ADDS.POLICY_ID=POL_DRIVER_DETAILS.POLICY_ID AND            
			ADDS.POLICY_VERSION_ID = POL_DRIVER_DETAILS.POLICY_VERSION_ID              
		AND ADDS.DRIVER_ID=POL_DRIVER_DETAILS.DRIVER_ID            
		WHERE ADDS.CUSTOMER_ID=@CUSTOMERID AND ADDS.POLICY_ID=@POLICYID AND ADDS.POLICY_VERSION_ID=@POLICYVERSIONID AND ADDS.VEHICLE_ID=@VEHICLEID AND (--ADDS.APP_VEHICLE_PRIN_OCC_ID IN (11398,11925,11927,11929,11931)   OR         
		(DATEDIFF(DAY,DRIVER_DOB,@QUOTEEFFECTIVEDATE) < @NINTEENYEARDAYS) OR (DATEDIFF(DAY , DATE_LICENSED , @QUOTEEFFECTIVEDATE) <@THREEYEARDAYS))           
		AND ADDS.DRIVER_ID = @RATEDDRIVER   and POL_DRIVER_DETAILS.IS_ACTIVE='Y'         
	     
		IF (@COUNT_DRIVER > 0 and @PREMIERDRIVER = 'TRUE')                      
			BEGIN          
				SET  @PREMIERDRIVER = 'FALSE'                      
			END                      
	    
	     
	  -- Check for claim if risk has paid claim over $75     
		DECLARE @PAIDLOSS int    
		SET @PAIDLOSS=0    
	   
	  IF(@PREMIERDRIVER = 'TRUE')    
		BEGIN 
			IF(@STATEID = '22')
				BEGIN
					IF (DATEDIFF(day,'07/01/2009',@QUOTEEFFECTIVEDATE)>=0)     
						BEGIN  
							SELECT @PAIDLOSS=count(CUSTOMER_ID)  FROM CLM_CLAIM_INFO with (NOLOCK) WHERE CUSTOMER_ID=@CUSTOMERID AND LOB_ID=2 and DATEDIFF(DAY,isnull(CLM_CLAIM_INFO.LOSS_DATE,'0'),@QUOTEEFFECTIVEDATE)<=@THREEYEARDAYS 
							AND DATEDIFF(DAY,isnull(CLM_CLAIM_INFO.LOSS_DATE,'0'),@QUOTEEFFECTIVEDATE)>=0  and DATEDIFF(DAY,isnull(CLM_CLAIM_INFO.LOSS_DATE,'0'),@PROCESSCOMPLETED_DATETIME)<=@THREEYEARDAYS 
							AND DATEDIFF(DAY,isnull(CLM_CLAIM_INFO.LOSS_DATE,'0'),@PROCESSCOMPLETED_DATETIME)>=0     
							AND	(ISNULL(PAID_LOSS,'0')+ ISNULL(PAID_EXPENSE,'0') > 750)  
						END
					ELSE
						BEGIN
								SELECT @PAIDLOSS=count(CUSTOMER_ID)  FROM CLM_CLAIM_INFO with (NOLOCK) WHERE CUSTOMER_ID=@CUSTOMERID AND LOB_ID=2 and DATEDIFF(DAY,isnull(CLM_CLAIM_INFO.LOSS_DATE,'0'),@QUOTEEFFECTIVEDATE)<=@THREEYEARDAYS    
								and DATEDIFF(DAY,isnull(CLM_CLAIM_INFO.LOSS_DATE,'0'),@QUOTEEFFECTIVEDATE)>=0  and DATEDIFF(DAY,isnull(CLM_CLAIM_INFO.LOSS_DATE,'0'),@PROCESSCOMPLETED_DATETIME)<=@THREEYEARDAYS    
								and DATEDIFF(DAY,isnull(CLM_CLAIM_INFO.LOSS_DATE,'0'),@PROCESSCOMPLETED_DATETIME)>=0       
							AND	(ISNULL(PAID_LOSS,'0')+ ISNULL(PAID_EXPENSE,'0') > 75)  
						END
						IF (@PAIDLOSS>0)    
							BEGIN    
								SET @PREMIERDRIVER = 'FALSE'            
							END    
				END
			ELSE IF(@STATEID = '14')
				BEGIN
					SELECT @PAIDLOSS=count(CUSTOMER_ID)  FROM CLM_CLAIM_INFO with (NOLOCK) WHERE CUSTOMER_ID=@CUSTOMERID AND LOB_ID=2 and DATEDIFF(DAY,isnull(CLM_CLAIM_INFO.LOSS_DATE,'0'),@QUOTEEFFECTIVEDATE)<=@THREEYEARDAYS    
					AND  DATEDIFF(DAY,isnull(CLM_CLAIM_INFO.LOSS_DATE,'0'),@QUOTEEFFECTIVEDATE)>=0 AND  DATEDIFF(DAY,isnull(CLM_CLAIM_INFO.LOSS_DATE,'0'),@PROCESSCOMPLETED_DATETIME)<=@THREEYEARDAYS    
					AND  DATEDIFF(DAY,isnull(CLM_CLAIM_INFO.LOSS_DATE,'0'),@PROCESSCOMPLETED_DATETIME)>=0
					AND (ISNULL(PAID_LOSS,'0')+ ISNULL(PAID_EXPENSE,'0') > 750)  
						IF (@PAIDLOSS>0)    
							BEGIN    
								SET @PREMIERDRIVER = 'FALSE'            
							END   
				END
		END      
	  
		/*IF(@LOSSCOUNT >0 AND  @PREMIERDRIVER = 'TRUE')    
			BEGIN    
				SET @PREMIERDRIVER = 'FALSE'      
			END   
	  */
		IF(@LOSSCOUNTDRIVERFREE >0 AND  @PREMIERDRIVER = 'TRUE')    
			BEGIN    
				SET @PREMIERDRIVER = 'FALSE'      
			END    
	     
	END    
ELSE    
	BEGIN    
		SET @PREMIERDRIVER = 'FALSE'      
	END    

 
 
--SET @SAFEDRIVER ='TRUE'    
---- SAFE DRIVER DISCOUNT    
--    
--IF EXISTS (SELECT DRIVER_ID FROM POL_DRIVER_DETAILS with (NOLOCK) WHERE CUSTOMER_ID= @CUSTOMERID AND POLICY_ID= @POLICYID AND POLICY_VERSION_ID=@POLICYVERSIONID AND DRIVER_ID = @RATEDDRIVER AND SAFE_DRIVER_RENEWAL_DISCOUNT=0)    
-- BEGIN    
--  SET  @SAFEDRIVER = 'FALSE'     
-- END    

	IF(@SAFEDRIVER = 'TRUE')    
		BEGIN    

			IF(@YEARSCONTINSUREDWITHWOLVERINE > 2)            
				BEGIN 
					SET @PREMIERDRIVER = 'FALSE'                      
					SET @SAFEDRIVER = 'TRUE'                      
				END   
			ELSE IF(@YEARSCONTINSUREDWITHWOLVERINE<=2)
				BEGIN
					SET @SAFEDRIVER = 'FALSE'   
				END

			IF(@PREMIER_SAFE_SUM_MVR_POINTS > 0 and @SAFEDRIVER = 'TRUE')    
				BEGIN     
					SET  @SAFEDRIVER = 'FALSE'     
				END        

	-- IF TWO CLAIMS ARE MORE THAN $75 THEN REFUSE SAFE DISCOUNT    
	DECLARE @PAIDCLAIM INT   
	set @PAIDCLAIM=0  
	IF(@STATEID = '22')    
		BEGIN    
			IF (DATEDIFF(day,'07/01/2009',@QUOTEEFFECTIVEDATE)>=0)     
				BEGIN  
					SELECT @PAIDCLAIM=COUNT(CUSTOMER_ID)   FROM CLM_CLAIM_INFO with (NOLOCK) WHERE CUSTOMER_ID=@CUSTOMERID AND LOB_ID=2 and DATEDIFF(DAY,isnull(CLM_CLAIM_INFO.LOSS_DATE,'0'),@QUOTEEFFECTIVEDATE)<=@THREEYEARDAYS    
					AND  DATEDIFF(DAY,isnull(CLM_CLAIM_INFO.LOSS_DATE,'0'),@QUOTEEFFECTIVEDATE)>=0 AND  DATEDIFF(DAY,isnull(CLM_CLAIM_INFO.LOSS_DATE,'0'),@PROCESSCOMPLETED_DATETIME)<=@THREEYEARDAYS    
					AND  DATEDIFF(DAY,isnull(CLM_CLAIM_INFO.LOSS_DATE,'0'),@PROCESSCOMPLETED_DATETIME)>=0
					and  (ISNULL(PAID_LOSS,'0')+ ISNULL(PAID_EXPENSE,'0') > 750) 
				END
			ELSE
				BEGIN
					SELECT @PAIDCLAIM=COUNT(CUSTOMER_ID)   FROM CLM_CLAIM_INFO with (NOLOCK) WHERE CUSTOMER_ID=@CUSTOMERID AND LOB_ID=2 and DATEDIFF(DAY,isnull(CLM_CLAIM_INFO.LOSS_DATE,'0'),@QUOTEEFFECTIVEDATE)<=@THREEYEARDAYS    
					AND  DATEDIFF(DAY,isnull(CLM_CLAIM_INFO.LOSS_DATE,'0'),@QUOTEEFFECTIVEDATE)>=0 AND  DATEDIFF(DAY,isnull(CLM_CLAIM_INFO.LOSS_DATE,'0'),@PROCESSCOMPLETED_DATETIME)<=@THREEYEARDAYS    
					AND  DATEDIFF(DAY,isnull(CLM_CLAIM_INFO.LOSS_DATE,'0'),@PROCESSCOMPLETED_DATETIME)>=0
					and  (ISNULL(PAID_LOSS,'0')+ ISNULL(PAID_EXPENSE,'0') > 75) 
				END  
		END    
	ELSE IF(@STATEID ='14')    
		BEGIN    
			SELECT @PAIDCLAIM=COUNT(CUSTOMER_ID)   FROM CLM_CLAIM_INFO with (NOLOCK) WHERE CUSTOMER_ID=@CUSTOMERID AND LOB_ID=2 and DATEDIFF(DAY,isnull(CLM_CLAIM_INFO.LOSS_DATE,'0'),@QUOTEEFFECTIVEDATE)<=@THREEYEARDAYS    
			AND  DATEDIFF(DAY,isnull(CLM_CLAIM_INFO.LOSS_DATE,'0'),@QUOTEEFFECTIVEDATE)>=0 
			AND  DATEDIFF(DAY,isnull(CLM_CLAIM_INFO.LOSS_DATE,'0'),@PROCESSCOMPLETED_DATETIME)<=@THREEYEARDAYS    
			AND  DATEDIFF(DAY,isnull(CLM_CLAIM_INFO.LOSS_DATE,'0'),@PROCESSCOMPLETED_DATETIME)>=0
			and (ISNULL(PAID_LOSS,'0')+ ISNULL(PAID_EXPENSE,'0') > 750)  
		END   

		SET @LOSSCOUNT = @PAIDCLAIM + @LOSSCOUNTDRIVERFREE

	if(@YEARSCONTINSUREDWITHWOLVERINE >= 2 and  @SAFEDRIVER = 'TRUE')            
		BEGIN    
			IF (DATEDIFF(day,'02/01/2008',@QUOTEEFFECTIVEDATE)>0)     
				BEGIN    
					IF(@LOSSCOUNT >= 2)    
						BEGIN    
							SET @PREMIERDRIVER = 'FALSE'            
							SET @SAFEDRIVER = 'FALSE'     
				END    
		END    
	ELSE    
		BEGIN    
			IF(@LOSSCOUNT >= 3)    
				BEGIN    
					SET @PREMIERDRIVER = 'FALSE'            
					SET @SAFEDRIVER = 'FALSE'     
				END        
		END    
		END    
	ELSE    
		BEGIN    
			SET @SAFEDRIVER = 'FALSE'    
		END  
	END       

  --  TRAILBLAIZER   LOSS CHECKS (RENEWAL)
IF(@QUALIFIESTRAIBLAZERPROGRAM='Y')
	BEGIN 
		-- IF TWO CLAIMS ARE MORE THAN $750 THEN REFUSE TRAILBLAZER IN RENEWAL     
		DECLARE @PAIDCLAIMTRAIL INT   
		SET @PAIDCLAIMTRAIL=0  
			IF(@STATEID ='14')    
				BEGIN    
					SELECT @PAIDCLAIMTRAIL=COUNT(CUSTOMER_ID)   FROM CLM_CLAIM_INFO with (NOLOCK) WHERE CUSTOMER_ID=@CUSTOMERID AND LOB_ID=2 and DATEDIFF(DAY,isnull(CLM_CLAIM_INFO.LOSS_DATE,'0'),@QUOTEEFFECTIVEDATE)<=@THREEYEARDAYS    
					and (ISNULL(PAID_LOSS,'0')+ ISNULL(PAID_EXPENSE,'0') > 750)  
				END   
				
			IF(@PAIDCLAIMTRAIL IS NULL)
				BEGIN
					SET @PAIDCLAIMTRAIL=0
				END
				SET @LOSSCOUNT = @PAIDCLAIMTRAIL + @LOSSCOUNTDRIVERFREE

			IF(@HAVE_RENEWED='Y' AND @QUALIFIESTRAIBLAZERPROGRAM='Y' AND @LOSSCOUNT >= 2)
				BEGIN
					SET @QUALIFIESTRAIBLAZERPROGRAM='N'
				END 

	END

IF(@PREMIERDRIVER = 'TRUE' and @QUALIFIESTRAIBLAZERPROGRAM='Y')  
  BEGIN     
    SET  @PREMIERDRIVER = 'FALSE'  
END  
            
                                                              
                                                       
/* FINAL SELECT */                                     
 SELECT        
                        
  @VEHICLETYPEUSE as VEHICLETYPEUSE,            
  @VEHICLECLASS as VEHICLECLASS,                  
 @VEHICLECLASSCOMPONENT1  as VEHICLECLASSCOMPONENT1,                           
  @VEHICLECLASSCOMPONENT2 as VEHICLECLASSCOMPONENT2,                                                          
 isnull(@VEHICLECLASS_DESC,'') as VEHICLECLASS_DESC, 
 @VEHICLERATINGCODE AS VEHICLERATINGCODE,                       
  @VEHICLETYPE   AS VEHICLETYPE,                                                         
  @VEHICLETYPEDESC  AS  VEHICLETYPEDESC,                      
 --@SUMOFACCIDENTPOINTS   AS SUMOFACCIDENTPOINTS,                
 --@SUMOFVIOLATIONPOINTS   AS SUMOFVIOLATIONPOINTS,                                  
                        
  @YEAR   AS YEAR,            
  @MAKE   AS MAKE,                                        
  @AGE   AS AGE,     
  @MODEL   AS MODEL,                                 
  @VIN   AS VIN,      
  @SYMBOL   AS SYMBOL,                                  
  @COST   AS COST,                                                            
  @ANNUALMILES   AS ANNUALMILES,                                                            
  @VEHICLEUSE   AS VEHICLEUSE,                   
  @CARPOOL  AS CARPOOL,        
  @USE as USE1,  -- ths is not required in rater ..rater uses VehicleUse                                                          
  @VEHICLEUSEDESC as VEHICLEUSEDESC,     
  @ENOCNI AS ENOCNI,                                                         
  @MILESEACHWAY   AS MILESEACHWAY,                                                             
  @ISANTILOCKBRAKESDISCOUNTS   AS ISANTILOCKBRAKESDISCOUNTS,                                                            
  @AIRBAGDISCOUNT   AS AIRBAGDISCOUNT,                                             
  @MULTICARDISCOUNT   AS MULTICARDISCOUNT,         
  --@INSURANCEAMOUNT   AS INSURANCEAMOUNT,                       
  @ZIPCODEGARAGEDLOCATION   AS ZIPCODEGARAGEDLOCATION,                                                            
  @TERRCODEGARAGEDLOCATION   AS TERRCODEGARAGEDLOCATION,                                                            
  @GARAGEDLOCATION AS GARAGEDLOCATION,                                                              
-- @TYPE   AS TYPE,                                                            
  @WEARINGSEATBELT   AS WEARINGSEATBELT   ,                                                          
  @QUALIFIESTRAIBLAZERPROGRAM as QUALIFIESTRAIBLAZERPROGRAM ,                    
  @DRIVERINCOME  AS DRIVERINCOME    ,                                                        
  @NODEPENDENT   AS DEPENDENTS,                                                 
  @WAIVEWORKLOSS AS WAIVEWORKLOSS,                  
  @VEHICLECLASS_COM AS VEHICLECLASS_COMM ,            
  @GOODSTUDENT AS GOODSTUDENT,            
  @PREMIERDRIVER as PREMIERDRIVER,            
  @SAFEDRIVER AS SAFEDRIVER,  
  @VEHICLETYPE_SCO AS VEHICLETYPE_SCO,                                           
  @WCEXCCOVS AS WCEXCCOVS               
END    


GO

