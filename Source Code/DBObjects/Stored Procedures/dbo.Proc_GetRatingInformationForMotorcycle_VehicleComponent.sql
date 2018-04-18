IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetRatingInformationForMotorcycle_VehicleComponent]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetRatingInformationForMotorcycle_VehicleComponent]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                                                            
Proc Name           : Proc_GetRatingInformationForMotorcycle_VehicleComponent                                                            
Created by            : Shrikant Bhatt.                                                            
Date                     : 19/12/2005                                                            
Purpose                : To get the information for creating the input xml                                                              
Revison History    :                                                            
Used In                 :   Creating InputXML for vehicle                                                           
                                                    
Modified By               :Shafi                                                                 
Date                      : 13 March 2006                                                      
Purpose                   : To get the information Sum Of Accident Points and Vialation Points  Of assigned Driver                                                
                                              
Modified By               : Ashwani                                                                 
Date                      : 27 March 2006                                                      
Purpose                   : Added a node  <VEHICLETYPEDESC> which will send text like "Sports Bike(..)" etc.                                               
                                                      
------------------------------------------------------------                                                            
Date     Review By          Comments                                                            
------   ------------       -------------------------*/                                                            
               
CREATE  PROC  dbo.Proc_GetRatingInformationForMotorcycle_VehicleComponent                
                                                             
(                                                            
@CUSTOMERID      INT,                                                            
@APPID      INT,                                                            
@APPVERSIONID    INT,                                                            
@VEHICLEID      INT                                                             
)                                                            
AS                                                            
                                                            
BEGIN                                                            
 SET QUOTED_IDENTIFIER OFF                                                            
                                                            
                                                            
                                                           
DECLARE   @VEHICLETYPE        nvarchar(100)                                                            
DECLARE   @YEAR1          nvarchar(100)                                                            
DECLARE   @MAKE          nvarchar(100)                                                            
DECLARE   @MODEL          nvarchar(100)                                                            
DECLARE   @VIN           nvarchar(100)                                                            
DECLARE   @SYMBOL         nvarchar(100)                                                            
DECLARE   @CC           nvarchar(100)                                                            
DECLARE   @CLASS          nvarchar(100)                                                            
DECLARE   @MULTICYCLE         nvarchar(100)    
DECLARE   @ZIPCODEGARAGEDLOCATION     nvarchar(100)             
DECLARE   @TERRCODEGARAGEDLOCATION     nvarchar(100)                               
DECLARE   @SUMOFMISCPOINTS     nvarchar(100)                                         
DECLARE   @GARAGEDLOCATION     nvarchar(100)                                               
DECLARE   @NOCYCLENDM     nvarchar(100)                                                            
DECLARE   @MATUREOPERATORCREDIT   nvarchar(100)                                                            
DECLARE   @AGE          nvarchar(100)                                                            
DECLARE   @ASSIGNEDDRIVERLICENCE3YEAR          nvarchar(100)   
DECLARE   @QUOTEEFFDATE          nvarchar(100)                                                            
DECLARE   @ASSIGNEDDRIVEREXPERIENCE          nvarchar(100)    
DECLARE   @COMPONLY          nvarchar(30) 
DECLARE    @ACCIDENT_NUM_YEAR   INT    
DECLARE    @VIOLATION_NUM_YEAR  INT    
DECLARE    @VIOLATION_NUM_YEAR_REFER  INT    
DECLARE    @ACCIDENT_PAID_AMOUNT  INT
DECLARE	  @TRANSFERRENEWAL NVARCHAR(20)
SET  @ACCIDENT_NUM_YEAR=3    
SET @VIOLATION_NUM_YEAR=2    
SET  @VIOLATION_NUM_YEAR_REFER=3    
SET @ACCIDENT_PAID_AMOUNT=500                        
                                 
SELECT                                                   
   @YEAR1     = ISNULL(VEHICLE_YEAR,'') ,                                            
   @MAKE     = ISNULL(MAKE,''),                                                   
   @MODEL     = ISNULL(MODEL,''),                                          
   @VIN      = ISNULL(VIN,''),                                                            
   @SYMBOL     = ISNULL(SYMBOL,'0'),                                        
   @AGE      = ISNULL(VEHICLE_AGE,'0'),      
   @ZIPCODEGARAGEDLOCATION     = ISNULL(GRG_ZIP ,''),                                                            
   @TERRCODEGARAGEDLOCATION  = ISNULL(TERRITORY ,''),                                                            
   @CC        = ISNULL(VEHICLE_CC,''),                                                        
   @VEHICLETYPE   =ISNULL(VEHICLE_TYPE,''), 
   --@CLASS= isnull(CLASS_PER,''),
   @COMPONLY=CASE COMPRH_ONLY
				WHEN 10963
					THEN 'YES'
			 ELSE
					'NO'
			 END
 FROM  APP_VEHICLES WITH (NOLOCK)                                                            
 WHERE CUSTOMER_ID=@CUSTOMERID AND APP_ID=@APPID AND APP_VERSION_ID=@APPVERSIONID AND VEHICLE_ID=@VEHICLEID                                                                 
                                                            
-- App effective Date  
SELECT  @QUOTEEFFDATE = CONVERT(VARCHAR(10),APP_EFFECTIVE_DATE,101)                                           
FROM    APP_LIST WITH (NOLOCK)   
WHERE   CUSTOMER_ID=@CUSTOMERID AND APP_ID=@APPID AND APP_VERSION_ID=@APPVERSIONID  
  
  
                                       
--FOR MULTICYCLE IF MORE THAN ONE MOTOR IS PRESENT                                                      
 ------------------------                                                       
DECLARE @VCOUNT INT                       
SELECT @VCOUNT = COUNT(VEHICLE_ID)                         
FROM APP_VEHICLES  WITH (NOLOCK) 
WHERE  CUSTOMER_ID=@CUSTOMERID AND APP_ID=@APPID AND APP_VERSION_ID=@APPVERSIONID AND ISNULL(IS_ACTIVE,'N')='Y' --AND MOTORCYCLE_TYPE !=11426
                                                      
IF @VCOUNT > 1                                                       
 SET @MULTICYCLE = 'Y'                     
ELSE                                                      
 SET @MULTICYCLE = 'N'         
    
                       
--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------








      
 SELECT  @CLASS = ISNULL(LOOKUP_VALUE_CODE,'A') FROM   APP_VEHICLES  WITH (NOLOCK) INNER JOIN MNT_LOOKUP_VALUES WITH (NOLOCK) ON APP_VEHICLE_CLASS = LOOKUP_UNIQUE_ID                                                
 WHERE                                                 
 CUSTOMER_ID=@CUSTOMERID AND APP_ID=@APPID AND APP_VERSION_ID=@APPVERSIONID AND VEHICLE_ID=@VEHICLEID                         
                                               
--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
 








                                     
DECLARE @VEHICLETYPEDESC NVARCHAR(150)                                                        
                                                   
SELECT                                                
 @VEHICLETYPE = ISNULL(LOOKUP_VALUE_CODE,''),@VEHICLETYPEDESC= isnull(LOOKUP_VALUE_DESC,'')                                                    
 FROM                                                             
 APP_VEHICLES WITH (NOLOCK) INNER JOIN MNT_LOOKUP_VALUES WITH (NOLOCK) ON MOTORCYCLE_TYPE = LOOKUP_UNIQUE_ID                                                             
WHERE                                         
  CUSTOMER_ID=@CUSTOMERID AND APP_ID=@APPID AND APP_VERSION_ID=@APPVERSIONID AND VEHICLE_ID=@VEHICLEID                                                                 
                                                            
                                                            
IF(@VEHICLETYPE IS NULL)                                                            
 BEGIN                                                            
  SET @VEHICLETYPE = 'A'                                                            
 END                                                            
                                                            
                              
-----------------------------                                                            
--GARAGED LOCATION  -- START                                                            
-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- 









              
                                                            
  IF(@ZIPCODEGARAGEDLOCATION != '')                           
 BEGIN                                                            
                                                             
     SELECT                                                            
 @GARAGEDLOCATION =  ISNULL(COUNTY,'') +'  COUNTY,'+' ' + ISNULL(CITY,'')+' ' +'('+ ZIP+'), ' + 'TERRITORY : '+ CONVERT(NVARCHAR(5),TERR),@TERRCODEGARAGEDLOCATION = isnull(TERR    ,'0')                                                        
      FROM                                                             
      MNT_TERRITORY_CODES  WITH (NOLOCK)                                 
     WHERE                                                             
  ZIP=(SUBSTRING(LTRIM(RTRIM(ISNULL(@ZIPCODEGARAGEDLOCATION,''))),1,5))                                                         
  AND LOBID = (SELECT APP_LOB from APP_LIST WITH (NOLOCK)  WHERE CUSTOMER_ID=@CUSTOMERID AND APP_ID=@APPID AND APP_VERSION_ID=@APPVERSIONID) --For Motorcycle   
  AND @QUOTEEFFDATE BETWEEN EFFECTIVE_FROM_DATE  AND ISNULL(EFFECTIVE_TO_DATE,'3000-03-16 16:59:06.630')  
END                                                            
  ELSE      
  BEGIN                           
    SET @GARAGEDLOCATION   = ''                                   
 END    
                           
                    
--GARAGED LOCATION  -- END                                                            
  ------------------------------------------------------------     
--				three years previous date(end)
	----------------------------------------------------------------------------------------------------  
DECLARE @FOURTYFIVEYEARLESSDATE DATETIME
DECLARE @FOURTYFIVEYEARDAYS INT
DECLARE @THREEYEARLESSDATE DATETIME
DECLARE @THREEFIVEYEARDAYS INT
DECLARE @EIGHTEENYEARLESSDATE DATETIME
DECLARE @EIGHTEENYEARDAYS INT
DECLARE @ONEYEARLESSDATE DATETIME
DECLARE @ONEYEARDAYS INT
SET @FOURTYFIVEYEARDAYS=0
SET @THREEFIVEYEARDAYS=0
SET @EIGHTEENYEARDAYS=0
SET @ONEYEARDAYS=0
SET @FOURTYFIVEYEARLESSDATE = DATEADD(YEAR,-45,@QUOTEEFFDATE)
SET @FOURTYFIVEYEARDAYS = DATEDIFF(DAY,@FOURTYFIVEYEARLESSDATE,@QUOTEEFFDATE)
SET @THREEYEARLESSDATE = DATEADD(YEAR,-3,@QUOTEEFFDATE)
SET @THREEFIVEYEARDAYS = DATEDIFF(DAY,@THREEYEARLESSDATE,@QUOTEEFFDATE)
SET @EIGHTEENYEARLESSDATE = DATEADD(YEAR,-18,@QUOTEEFFDATE)
SET @EIGHTEENYEARDAYS = DATEDIFF(DAY,@EIGHTEENYEARLESSDATE,@QUOTEEFFDATE)
SET @ONEYEARLESSDATE = DATEADD(YEAR,-1,@QUOTEEFFDATE)
SET @ONEYEARDAYS = DATEDIFF(DAY,@ONEYEARLESSDATE,@QUOTEEFFDATE)
---------------------------------------------------------------------------------------------------
--				three years previous date(end)
----------------------------------------------------------------------------------------------------                           
 --------------------------------------------------------------------
--    aSSIGNED dRIVER ACCIDENT AND VIOLATION POINT(START)
--------------------------------------------------------------------  
DECLARE @ASSIGNDRIVERIDS INT
DECLARE @IDENT_COLMN INT
DECLARE @ASSIGNPREMSAFEDRIVERIDS INT
DECLARE @ASSIGNDRIVS_SUM_MVR_POINTS INT
DECLARE @ASSIGNDRIVS_ACCIDENT_POINTS INT
DECLARE @ASSIGNDRIVS_SUMOFMISCPOINTS INT
SET @ASSIGNDRIVS_SUM_MVR_POINTS=0
SET @ASSIGNDRIVS_ACCIDENT_POINTS=0
SET @ASSIGNDRIVS_SUMOFMISCPOINTS=0



CREATE TABLE #ASSIGNDRIVSPRMSAFE_DRIVER_VIOLATION_ACCIDENT    
(    
 [SUM_MVR_POINTS]  INT,                                                
 [ACCIDENT_POINTS]  INT,                    
 [COUNT_ACCIDENTS]  INT,    
 [MVR_POINTS]  INT,
 [SUMOFMISCPOINTS] INT   
                   
)     
CREATE TABLE #ASSIGNDRIVSPRMSAFE                  
(         
  [IDEN_ID] INT IDENTITY(1,1) NOT NULL,                    
  [ASSIGNPRESAFEDRIVERIDS] INT                   
)    
INSERT INTO #ASSIGNDRIVSPRMSAFE   
SELECT ADDS.DRIVER_ID FROM  APP_DRIVER_ASSIGNED_VEHICLE ADDS WITH (NOLOCK) INNER JOIN APP_VEHICLES AV WITH (NOLOCK)                      
ON AV.CUSTOMER_ID=ADDS.CUSTOMER_ID AND                      
   AV.APP_ID=ADDS.APP_ID AND                      
   AV.APP_VERSION_ID = ADDS.APP_VERSION_ID AND                   
   AV.VEHICLE_ID=ADDS.VEHICLE_ID         
WHERE AV.CUSTOMER_ID=@CUSTOMERID AND AV.APP_ID=@APPID AND AV.APP_VERSION_ID=@APPVERSIONID AND AV.VEHICLE_ID=@VEHICLEID AND     
ADDS.APP_VEHICLE_PRIN_OCC_ID IN (11398,11925)    
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
 INSERT INTO #ASSIGNDRIVSPRMSAFE_DRIVER_VIOLATION_ACCIDENT EXEC GetMotorMVRViolationPoints @CUSTOMERID,@APPID,@APPVERSIONID ,@ASSIGNDRIVERIDS,@ACCIDENT_NUM_YEAR,@VIOLATION_NUM_YEAR,@VIOLATION_NUM_YEAR_REFER,@ACCIDENT_PAID_AMOUNT               
 
	SET @IDENT_COLMN = @IDENT_COLMN + 1    
END      
SELECT @ASSIGNDRIVS_SUM_MVR_POINTS = SUM(ISNULL(SUM_MVR_POINTS,0)), @ASSIGNDRIVS_ACCIDENT_POINTS = SUM(ISNULL(ACCIDENT_POINTS,0)), @ASSIGNDRIVS_SUMOFMISCPOINTS =SUM(ISNULL(SUMOFMISCPOINTS,0)) FROM  #ASSIGNDRIVSPRMSAFE_DRIVER_VIOLATION_ACCIDENT
   SET @ASSIGNDRIVS_SUM_MVR_POINTS = @ASSIGNDRIVS_SUM_MVR_POINTS + @ASSIGNDRIVS_ACCIDENT_POINTS + @ASSIGNDRIVS_SUMOFMISCPOINTS

DROP TABLE #ASSIGNDRIVSPRMSAFE_DRIVER_VIOLATION_ACCIDENT     
DROP TABLE #ASSIGNDRIVSPRMSAFE    
--------------------------------------------------------------------
--    ASSIGNED dRIVER ACCIDENT AND VIOLATION POINT(END)
--------------------------------------------------------------------                                 
	-----------------------------                                                            
	--MATURE OPERATOR   CREDIT   (Start)
	-----------------------------                                                     
		--APPLICABLE TO EACH BIKE THAT THE PRINCIPAL OPERATOR IS AGE 45 OR OLDER AND THERE ARE NO OTHER                                                        
		--OPERATORS UNDER AGE 45.                                                        
		-- (WE CHECK IF ANY DRIVER BELOW 45 YRS EXISTS. IF SUCH DRIVER EXISTS THEN WE SET THE CREDIT AS 'N' ELSE CREDIT IS GRANTED.)                                                        
	SET @MATUREOPERATORCREDIT  = 'Y'                                                            
	IF EXISTS ( SELECT * FROM APP_DRIVER_ASSIGNED_VEHICLE ADAV  WITH (NOLOCK) 
			INNER JOIN APP_DRIVER_DETAILS ADDS  WITH (NOLOCK)        
			ON ADDS.CUSTOMER_ID = ADAV.CUSTOMER_ID        
			AND ADDS.APP_ID = ADAV.APP_ID        
			AND ADDS.APP_VERSION_ID = ADAV.APP_VERSION_ID        
			AND ADDS.DRIVER_ID = ADAV.DRIVER_ID        
			WHERE ADAV.CUSTOMER_ID=@CUSTOMERID AND ADAV.APP_ID=@APPID AND ADAV.APP_VERSION_ID=@APPVERSIONID AND ADAV.VEHICLE_ID=@VEHICLEID         
			AND ISNULL(ADDS.IS_ACTIVE,'Y') ='Y' AND ISNULL(DATEDIFF(DAY,ADDS.DRIVER_DOB, @QUOTEEFFDATE),0) < @FOURTYFIVEYEARDAYS and ADAV.APP_VEHICLE_PRIN_OCC_ID IN (11398,11399,11925,11926) and DRIVER_DRIV_TYPE =11941)        
				BEGIN     
					 SET @MATUREOPERATORCREDIT  = 'N'                                                            
				END                    
        
   	-----------------------------                                                            
	--MATURE OPERATOR   CREDIT   (end)
	----------------------------- 
	-------------------------------------------                                      
	-----------------------Preferred Risk Credit (Start)--------------    
	-------------------------------------------      
  			SET @ASSIGNEDDRIVERLICENCE3YEAR ='TRUE'                          
			IF EXISTS (SELECT  * FROM	APP_DRIVER_DETAILS ADDS WITH (NOLOCK)                                                           
								INNER JOIN APP_DRIVER_ASSIGNED_VEHICLE ADAVS WITH (NOLOCK) on                       
								ADDS.APP_ID = ADAVS.APP_ID AND                                                         
								ADDS.APP_VERSION_ID = ADAVS.APP_VERSION_ID AND                                                         
								ADDS.CUSTOMER_ID = ADAVS.CUSTOMER_ID AND
								ADDS.DRIVER_ID = ADAVS.DRIVER_ID                                                      
						WHERE ISNULL(ADDS.IS_ACTIVE,'Y') ='Y'                                            
						AND ADDS.CUSTOMER_ID=@CUSTOMERID AND ADDS.APP_ID=@APPID AND ADDS.APP_VERSION_ID=@APPVERSIONID   AND ADAVS.VEHICLE_ID=@VEHICLEID                       
						AND ISNULL(DATEDIFF(DAY,ADDS.DATE_LICENSED, @QUOTEEFFDATE),0) < @THREEFIVEYEARDAYS AND ADAVS.APP_VEHICLE_PRIN_OCC_ID IN (11398,11399,11925,11926) and DRIVER_DRIV_TYPE =11941)
							BEGIN       
								SET @ASSIGNEDDRIVERLICENCE3YEAR  = 'FALSE'                                                        
							END   
			IF(@ASSIGNEDDRIVERLICENCE3YEAR='TRUE' AND @ASSIGNDRIVS_SUM_MVR_POINTS >2)
				BEGIN
					SET @ASSIGNEDDRIVERLICENCE3YEAR = 'FALSE'
				END

  	-------------------------------------------                                      
	-----------------------Preferred Risk Credit (End)--------------    
	-------------------------------------------          
  -----------------------------------------------------------------  
	--    assigned Driver experince(start)  
  -----------------------------------------------------------------     
SET @ASSIGNEDDRIVEREXPERIENCE='TRUE'                    
 IF EXISTS (SELECT   * FROM  APP_DRIVER_DETAILS ADDS WITH (NOLOCK) 
						INNER JOIN APP_DRIVER_ASSIGNED_VEHICLE ADAV WITH (NOLOCK) on                       
						ADDS.APP_ID = ADAV.APP_ID AND                                                         
                        ADDS.APP_VERSION_ID = ADAV.APP_VERSION_ID AND                                                         
                        ADDS.CUSTOMER_ID = ADAV.CUSTOMER_ID AND
						ADDS.DRIVER_ID = ADAV.DRIVER_ID                                                      
				WHERE   ISNULL(ADDS.IS_ACTIVE,'Y') ='Y' 
						AND ADDS.CUSTOMER_ID=@CUSTOMERID AND ADDS.APP_ID=@APPID AND ADDS.APP_VERSION_ID=@APPVERSIONID   AND ADAV.VEHICLE_ID=@VEHICLEID  
						AND ISNULL(DATEDIFF(DAY,ADDS.DATE_LICENSED, @QUOTEEFFDATE),0) <= @ONEYEARDAYS AND ADAV.APP_VEHICLE_PRIN_OCC_ID IN (11398,11399,11925,11926) and DRIVER_DRIV_TYPE =11941)     
							BEGIN  
								SET @ASSIGNEDDRIVEREXPERIENCE='FALSE'  
							END                                                     
 SET @TRANSFERRENEWAL='TRUE'
/* As per discussed with Rajan commented on 07/06/2009
  IF EXISTS (SELECT   * FROM  APP_DRIVER_DETAILS ADDS WITH (NOLOCK) 
						INNER JOIN APP_DRIVER_ASSIGNED_VEHICLE ADAV WITH (NOLOCK) on                       
						ADDS.APP_ID = ADAV.APP_ID AND                                                         
                        ADDS.APP_VERSION_ID = ADAV.APP_VERSION_ID AND                                                         
                        ADDS.CUSTOMER_ID = ADAV.CUSTOMER_ID AND
						ADDS.DRIVER_ID = ADAV.DRIVER_ID                                                      
				WHERE   ISNULL(ADDS.IS_ACTIVE,'Y') ='Y' 
						AND ADDS.CUSTOMER_ID=@CUSTOMERID AND ADDS.APP_ID=@APPID AND ADDS.APP_VERSION_ID=@APPVERSIONID   AND ADAV.VEHICLE_ID=@VEHICLEID  
						AND ISNULL(TRANSFEREXPERIENCE_RENEWALCREDIT,'0')='0' AND ADAV.APP_VEHICLE_PRIN_OCC_ID IN (11398,11399,11925,11926) and DRIVER_DRIV_TYPE =11941)     
							BEGIN
								SET @TRANSFERRENEWAL='FALSE'
							END  */ 

-----------------------------------------------------------------  
--    assigned Driver experince(end)  
  -----------------------------------------------------------------           
/********************END OF DRIVER SECTION ***********************************/                                           
                                          
--------------------------------------------------------------------------------------------------------------------                               
-- CHECK IF AGE OF ASSIENGED DRIVERS IS GREATER THAN 18. SEND 'TRUE'                                           
-- ELSE SEND 'FALSE'.                                          
                                          
 DECLARE @ALLDRIVEROVER18 VARCHAR(10)   
 SET @ALLDRIVEROVER18='TRUE'
  IF EXISTS (SELECT ADDS.DRIVER_ID FROM APP_DRIVER_DETAILS ADDS  WITH (NOLOCK)  
						INNER JOIN APP_DRIVER_ASSIGNED_VEHICLE ADAVS  WITH (NOLOCK)
					ON  ADDS.CUSTOMER_ID = ADAVS.CUSTOMER_ID AND
						ADDS.APP_ID = ADAVS.APP_ID AND
                        ADDS.APP_VERSION_ID = ADAVS.APP_VERSION_ID AND
						ADDS.DRIVER_ID = ADAVS.DRIVER_ID
					WHERE ADDS.CUSTOMER_ID=@CUSTOMERID AND ADDS.APP_ID=@APPID AND ADDS.APP_VERSION_ID=@APPVERSIONID AND ADAVS.VEHICLE_ID=@VEHICLEID AND                                                                 
						(datediff(day, isnull(ADDS.DRIVER_DOB,0),isnull(@QUOTEEFFDATE,0)) <= @EIGHTEENYEARDAYS ) AND ISNULL(ADDS.IS_ACTIVE,'Y') ='Y' and DRIVER_DRIV_TYPE =11941)  
                                         
						  BEGIN                                           
						   SET @ALLDRIVEROVER18='FALSE'                                  
						  END                      
--------------------------------------------------------------------------------------------------------------------                                          
-- FOR EACH DRIVER AGAINST EACH VEHICLE ,                                     
-- CHECK  'NO CYCLE ENDORSEMENT ON LICENSE' . IF IT IS 'Y' FOR ANY DRIVER THEN SEND 'Y' ELSE 'N"                                          
-- This surcharge applies to the operator on the bike they drive most.   
--Itrack 5974 -@NOCYCLENDMT
 DECLARE @RATEDDRIVER INT                      
 DECLARE @NOCYCLENDMT NVARCHAR(2)  
 SELECT @RATEDDRIVER = CLASS_DRIVERID FROM APP_VEHICLES WHERE CUSTOMER_ID=@CUSTOMERID AND APP_ID=@APPID AND APP_VERSION_ID=@APPVERSIONID AND VEHICLE_ID=@VEHICLEID            
 SET @NOCYCLENDMT=''                                      
 IF EXISTS(select ADDS.DRIVER_ID from APP_DRIVER_DETAILS ADDS WITH (NOLOCK)                    
 inner join app_DRIVER_ASSIGNED_VEHICLE ADAV WITH (NOLOCK) on                 
  ADDS.CUSTOMER_ID = ADAV.CUSTOMER_ID                    
 AND  ADDS.APP_ID = ADAV.APP_ID                    
 AND  ADDS.APP_VERSION_ID = ADAV.APP_VERSION_ID 
 AND  ADDS.DRIVER_ID = ADAV.DRIVER_ID
 where
 ADAV.APP_VEHICLE_PRIN_OCC_ID<>11931
 AND
 ADDS.CUSTOMER_ID=@CUSTOMERID and ADDS.APP_ID=@APPID and adds.APP_VERSION_ID=@APPVERSIONID and NO_CYCLE_ENDMT ='1' and ADAV.VEHICLE_ID=@VEHICLEID AND ADDS.DRIVER_ID = @RATEDDRIVER and DRIVER_DRIV_TYPE =11941)                    
  BEGIN
		--SET @NOCYCLENDMT='Y'
		SET @NOCYCLENDMT='N'
  END
  ELSE
  BEGIN
		--SET @NOCYCLENDMT ='N'
		SET @NOCYCLENDMT ='Y'
  END

                       
                    
--------------------------------------------------------------------------------------------------------------------                                
---------------------------RETURN VALUES---------                                  
                                                            
 SELECT                                                           
  @VEHICLETYPE       AS  VEHICLETYPE,                                               
  @VEHICLETYPEDESC    AS  VEHICLETYPEDESC,                                                           
  @YEAR1         AS  YEAR,                                
  @MAKE          AS  MAKE,                                                            
  @MODEL         AS  MODEL,                                                            
  @VIN           AS  VIN,                                                            
  @SYMBOL         AS  SYMBOL,                   
  @CC        AS  CC,                                                            
  @CLASS       AS  CLASS,             
  @MULTICYCLE      AS  MULTICYCLE,                                                                
                                  
  @ZIPCODEGARAGEDLOCATION      AS  ZIPCODEGARAGEDLOCATION,                                                            
  @TERRCODEGARAGEDLOCATION      AS  TERRCODEGARAGEDLOCATION,                                         
                                                     
  --@SUMOFMISCPOINTS     AS SUMOFMISCPOINTS,                                                            
  @GARAGEDLOCATION     AS GARAGEDLOCATION,                                                              
  @MATUREOPERATORCREDIT AS MATUREOPERATORCREDIT,                           
  @ASSIGNEDDRIVERLICENCE3YEAR AS ASSIGNEDDRIVERLICENCE3YEAR,                          
  @AGE          AS AGE,                                          
  @ALLDRIVEROVER18  AS ALLDRIVERSOVER18,                                          
  @NOCYCLENDMT   AS NOCYCLENDMT,  
  @ASSIGNEDDRIVEREXPERIENCE AS DRIVEREXPERINCE,  
  @COMPONLY AS COMPONLY,
  @TRANSFERRENEWAL AS TRANSFERRENEWAL                                                         
--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------


                                    
END  



GO

