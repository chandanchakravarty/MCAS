IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetRatingInformationForWatercraft_BoatComponent]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetRatingInformationForWatercraft_BoatComponent]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER OFF
GO









/* ----------------------------------------------------------                                                                
Proc Name               : Proc_GetRatingInformationForWatercraft_BoatComponent   936,148,1,1         
Created by              : Ashwini                                                                
Date                    : 3 Jan.,2006                                                                
Purpose                 : To get the information for creating the input xml            
Revison History         :                                                                
Used In                 :   Creating InputXML for Watercraft                                                                
------------------------------------------------------------                                                                
Date     Review By          Comments                                                                
------   ------------       -------------------------*/                       
--DROP PROC  Proc_GetRatingInformationForWatercraft_BoatComponent                                                                                       
create PROC Proc_GetRatingInformationForWatercraft_BoatComponent                                                                                  
(                                                                                  
 @CUSTOMERID    int,                                                                            
 @APPID     int,                                                                                  
 @APPVERSIONID  int,                                                                                  
 @BOATID    int                                                                                   
)                                                                                  
AS                                                                                  
                                                                                  
BEGIN                                                                                  
set quoted_identifier off                                                                                  
                                                                                  
DECLARE     @BOATROWID         NVARCHAR(100)                                                                                  
DECLARE     @BOATTYPE          NVARCHAR(100)                                                                                  
DECLARE     @BOATTYPECODE      NVARCHAR(100)                                                                                  
DECLARE     @YEAR              NVARCHAR(12)                                                                                  
DECLARE     @MODEL             NVARCHAR(150)                                                                                  
DECLARE     @SERIALNUMBER      NVARCHAR(150)                                                                                   
DECLARE     @LENGTH            NVARCHAR(25) 
DECLARE     @INCHES            NVARCHAR(25)                                                                                 
DECLARE     @HORSEPOWER        NVARCHAR(20)                                                                                  
DECLARE     @CAPABLESPEED      NVARCHAR(20)                                                                                  
DECLARE     @WATERS            NVARCHAR(150)                                                                                  
DECLARE     @WATERSCODE        NVARCHAR(150)                                                                                  
DECLARE     @COVERAGEBASIS     NVARCHAR(20)    
DECLARE     @MARKETVALUE       NVARCHAR(20)                                                                                  
DECLARE     @CONSTRUCTION      NVARCHAR(150)        
DECLARE     @CONSTRUCTIONCODE  NVARCHAR(150)                                                                                  
DECLARE     @AGE            NVARCHAR(20)                                       
--              
DECLARE     @COUNTYOFOPERATION NVARCHAR(200)                       
DECLARE     @DIESELENGINE      NVARCHAR(20)                             
DECLARE     @SHORESTATION      NVARCHAR(20)                                         
DECLARE     @HALONFIRE         NVARCHAR(20)                                                                                  
DECLARE     @LORANNAVIGATIONSYSTEM  NVARCHAR(20)                                          
DECLARE     @DUALOWNERSHIP     NVARCHAR(20)                                                  
DECLARE     @REMOVESAILBOAT    NVARCHAR(20)                                              
DECLARE     @MULTIBOATCREDIT   NVARCHAR(20)                                              
DECLARE     @ISGRAND NVARCHAR(2)                                                                                  
DECLARE     @BOATSTYLE         NVARCHAR(2)               
DECLARE @MANUFACTURER NVARCHAR(150)                                                   
                                       
--DECLARE @DEDUCTIBLE NVARCHAR(20)                                              
-------FOR POLICY LEVEL NODES AT BOAT LEVEL-----23 May 06                                          
DECLARE  @PERSONALLIABILITY          NVARCHAR(20)                                                            
DECLARE  @MEDICALPAYMENT          NVARCHAR(20)                                   
DECLARE  @MEDICALPAYMENTSOTHER             NVARCHAR(20)                                          
DECLARE  @MEDICALPAYMENTSOTHERLIMIT        NVARCHAR(20)                                          
DECLARE  @UNINSUREDBOATERS                 NVARCHAR(20)                                             
DECLARE  @UNATTACHEDEQUIPMENT              NVARCHAR(20)                                            
DECLARE  @UNATTACHEDEQUIPMENT_DEDUCTIBLE   VARCHAR(20)                                    
DECLARE  @DEDUCTIBLE         VARCHAR(20)                                       
DECLARE  @OP900_LIMIT        NVARCHAR(20)               
DECLARE @COUNTYCODE VARCHAR(100)                               
                      
DECLARE  @TERRITORYDOCKEDIN                VARCHAR(20)                              
DECLARE  @STATEDOCKEDIN                    VARCHAR(20)                       
                      
                                          
SET @UNATTACHEDEQUIPMENT_DEDUCTIBLE = 100  --Fixed Limit 100               
-------END POLICY COV -------------------------------------                                                                             
----            
--DECLARE @WAT_SAFETY_COURSE  VARCHAR(12)                      
--DECLARE @CERT_COAST_GUARD  VARCHAR(12)               
DECLARE  @HAS_5_YEARSOPERATOREXPERIENCE  VARCHAR(12)        
--Added Experience Discount      
DECLARE @POWERSQUADRONCOURSE  VARCHAR(12)         
DECLARE @COASTGUARDAUXILARYCOURSE  VARCHAR(12)         
---            
                                                            
                                          
                                                                                  
                                                                                
                                                                 
                                                                  
                                                              
                                                                             
 SELECT                                                                                
  @YEAR         = ISNULL(YEAR,0),                                                                                  
  @MODEL        = ISNULL(REPLACE(MODEL,'''',''),''),                                                                           
                         
                                                                                
  @SERIALNUMBER  = ISNULL(HULL_ID_NO,0),                                                                    
  @LENGTH       =  ISNULL(LENGTH,0),                 
  @INCHES 	= ISNULL(INCHES,0),                                                                       
  @HORSEPOWER    = ISNULL(WATERCRAFT_HORSE_POWER,0),              
  @CAPABLESPEED  = ISNULL(MAX_SPEED,0),          
  @COVERAGEBASIS = ISNULL(LOOKUP_VALUE_CODE,'ANA'),    
  @MARKETVALUE = case         
  when   COV_TYPE_BASIS = 11978 then 0      
  else CAST(isnull(INSURING_VALUE,0)AS INTEGER) END,      
           
 -- @MARKETVALUE =   CAST(isnull(INSURING_VALUE,0)AS INTEGER),                                                                       
  @SHORESTATION = ISNULL(SHORE_STATION,0)  ,                                  
  @HALONFIRE  = ISNULL(HALON_FIRE_EXT_SYSTEM,0)  ,                                              
  @DUALOWNERSHIP  = ISNULL(DUAL_OWNERSHIP,0) ,                                                       
  @LORANNAVIGATIONSYSTEM=ISNULL(LORAN_NAV_SYSTEM,0) ,                                                                     
  @REMOVESAILBOAT =       ISNULL(remove_sailboat,0),                                                        
  @MANUFACTURER = isnull(REPLACE(MAKE,'''',''),'')                                                                    
                                                                    
                                             
 FROM   APP_WATERCRAFT_INFO A  WITH (NOLOCK) INNER JOIN MNT_LOOKUP_VALUES   B    
 ON A.COV_TYPE_BASIS = B.LOOKUP_UNIQUE_ID                                                                                     
 WHERE  CUSTOMER_ID=@CUSTOMERID AND APP_ID=@APPID AND APP_VERSION_ID=@APPVERSIONID     
 AND A.BOAT_ID=@BOATID                                                                                       
    
 SET @LENGTH = (@LENGTH * 12)
 SET @LENGTH = convert(nvarchar(25),((convert(int,@LENGTH) + convert(int,@INCHES)))) 

 
 
 --                                                                                  
 IF(@MARKETVALUE IS NULL)                                                                                  
 BEGIN                                                                                  
   SET @MARKETVALUE = 0                                                                                 
 END                                                   
                                                                                
                                                              
DECLARE @APPEFFECTIVEDATE datetime                                                                     
                                                                      
 SELECT @APPEFFECTIVEDATE = convert(char(10),APP_EFFECTIVE_DATE,101)    FROM APP_LIST  WITH (NOLOCK)                                          
 WHERE CUSTOMER_ID=@CUSTOMERID AND APP_ID=@APPID AND APP_VERSION_ID=@APPVERSIONID                                               
                                              
--Added For Grand Father Implementation                                              
                                              
if @APPEFFECTIVEDATE < '03/1/2006'                                              
 SET @ISGRAND='Y'                                     
                                  
ELSE                                              
 SET @ISGRAND='N'                                              
                                                
                                                                                  
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------                                                           
  
    
      
      
        
          
             
             
                                                                   
IF(@YEAR IS NOT NULL)                                                    
 BEGIN                                                                                   
   SET @AGE = YEAR(@APPEFFECTIVEDATE) - @YEAR                                                                                  
 END                                                                            
ELSE                                                                  
 BEGIN                                                                                     
  SET @AGE=0                                                                                 
 END                                 
--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
  
     
      
      
        
          
             
              
                                                             
 SELECT  @DIESELENGINE=ISNULL(LOOKUP_VALUE_CODE,'')                                                            
 FROM   APP_WATERCRAFT_INFO WITH (NOLOCK)         
 INNER JOIN  MNT_LOOKUP_VALUES WITH (NOLOCK) ON APP_WATERCRAFT_INFO.DIESEL_ENGINE=MNT_LOOKUP_VALUES.LOOKUP_UNIQUE_ID                                                            
 WHERE  CUSTOMER_ID=@CUSTOMERID AND APP_ID=@APPID AND APP_VERSION_ID=@APPVERSIONID AND BOAT_ID=@BOATID                           
                                                         
 -- in mnt lookup table 10964='NO' and 10963 ='Yes'                                                                        
 IF(@DIESELENGINE='1')                                                        
 BEGIN                                  
  SET @DIESELENGINE='Y'                                                        
 END                                                         
 ELSE IF(@DIESELENGINE='0')                                                        
 BEGIN                                                         
  SET @DIESELENGINE='N'                                                        
 END                                                         
 ELSE IF(@DIESELENGINE='')                                                                
 BEGIN                                                        
    SET @DIESELENGINE='N'                                                             
 END                                                        
                                                                                          
                                                            
/*                                                      
"3725"=Diesel                                                            
"3726"=Gas                                                            
"3724"=None                                                            
                                                                
                                                                  
                                                                    
*/                                                                      
                                                                        
/*                                                                          
modification for thes 5 fields                                              
@DIESELENGINE  , @SHORESTATION ,@HALONFIRE ,@DUALOWNERSHIP  ,@LORANNAVIGATIONSYSTEM ,@REMOVESAILBOAT                                                                        
                                       
*/                                                                        
                                                                        
                                                                      
                                                                        
                                            
                                  
IF (@SHORESTATION = 10964 or @SHORESTATION = 0)                                                                
 SET  @SHORESTATION='N'                                                                        
ELSE                                                                       
 SET @SHORESTATION='Y'                                                                        
                 
IF (@HALONFIRE = 10964  or @HALONFIRE=0 )                                                                
 SET @HALONFIRE='N'                                                                   
ELSE                                                                        
 SET @HALONFIRE='Y'                                                                        
                                                                        
IF (@DUALOWNERSHIP = 10964 or @DUALOWNERSHIP = 0)                                          
 SET @DUALOWNERSHIP='N'                                                                        
ELSE                                                                        
 SET @DUALOWNERSHIP='Y'                                                                        
                                                                               
                                                 
IF (@LORANNAVIGATIONSYSTEM = 10964 or @LORANNAVIGATIONSYSTEM = 0)                                                      
 SET @LORANNAVIGATIONSYSTEM ='N'                                                                        
ELSE                                                                        
 SET @LORANNAVIGATIONSYSTEM ='Y'                                                                        
                                                                                
                                 
IF (@REMOVESAILBOAT = 10964 or @REMOVESAILBOAT = 0)                                                                               
 SET @REMOVESAILBOAT ='N'                                                                        
--IF @REMOVESAILBOAT = 10963                                                                        
else                           
 SET @REMOVESAILBOAT ='Y'                                                                        
                                                                    
 --IF @REMOVESAILBOAT = '0'                                                                                  
 --SET @REMOVESAILBOAT ='N'                                                                     
                               
-----------------------------                                                                                  
--Vehicle Related Fields  START                                                                                  
-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- 
  
   
       
       
        
          
             
              
                
                  
                    
                      
                      
                      
                       
                           
                           
                               
                               
                                  
                                     
                                     
                                         
                                          
                                            
-----------------------POLICY LEVEL COVERAGES AT BOAT LEVEL-----PRAVEEN KASANA-------------------------                                           

--MEDICAL PAYMENT should hold 'EFH' if 'Extended From Home' otherwise should hold Limit_1 Value - Asfa Praveen 22/May/2007
--MCPAY  - WATER CRAFT LIABLITY

IF EXISTS(SELECT CUSTOMER_ID FROM APP_WATERCRAFT_COVERAGE_INFO 
where  CUSTOMER_ID =@CUSTOMERID AND APP_ID=@APPID AND APP_VERSION_ID=@APPVERSIONID   AND BOAT_ID=@BOATID                                          
       AND  LIMIT_ID IN('1415','1416','1417'))
   BEGIN
      SET @MEDICALPAYMENT= 'EFH'   
   END
ELSE
   BEGIN
	SELECT @MEDICALPAYMENT=ISNULL(Limit_1,'0'),  @MEDICALPAYMENTSOTHERLIMIT=ISNULL(Limit_2,'0')                                
	FROM APP_WATERCRAFT_COVERAGE_INFO WITH (NOLOCK) 
	WHERE CUSTOMER_ID =@CUSTOMERID AND APP_ID=@APPID AND APP_VERSION_ID=@APPVERSIONID  AND BOAT_ID=@BOATID                                          
	      AND  COVERAGE_CODE_ID IN('21','68','821')                                               
        SET @MEDICALPAYMENTSOTHER= @MEDICALPAYMENT + '/' + @MEDICALPAYMENTSOTHERLIMIT   
   END


--PERSONAL LIABILITY should hold 'EFH' if 'Extended From Home' otherwise should hold Limit_1 Value - Asfa Praveen 22/May/2007
--LCCSL - WATER CRAFT LIABLITY

IF EXISTS(SELECT CUSTOMER_ID FROM APP_WATERCRAFT_COVERAGE_INFO 
where  CUSTOMER_ID =@CUSTOMERID AND APP_ID=@APPID AND APP_VERSION_ID=@APPVERSIONID   AND BOAT_ID=@BOATID                                          
       AND  LIMIT_ID IN('1412','1413','1414'))
   BEGIN
      SET @PERSONALLIABILITY= 'EFH'   
   END
ELSE
   BEGIN
	SELECT @PERSONALLIABILITY=ISNULL(Limit_1,'0') FROM APP_WATERCRAFT_COVERAGE_INFO WITH (NOLOCK) 
	WHERE  CUSTOMER_ID =@CUSTOMERID AND APP_ID=@APPID AND APP_VERSION_ID=@APPVERSIONID   AND BOAT_ID=@BOATID                                          
	       AND  COVERAGE_CODE_ID IN('19','65','820')  
   END

--------------------------------------------------------------------------------------------------------------

DECLARE @STATE_ID INT                                          
SELECT @STATE_ID=STATE_ID FROM APP_LIST WHERE CUSTOMER_ID =@CUSTOMERID AND APP_ID=@APPID AND APP_VERSION_ID=@APPVERSIONID                           
                                          
IF @STATE_ID = 14 --INDIANA                                                      
BEGIN                                             
---MEDICAL PAYMENT  CODE - MCPAY                                               
/* SELECT @MEDICALPAYMENT=ISNULL(Limit_1,'0'),  @MEDICALPAYMENTSOTHERLIMIT=ISNULL(Limit_2,'0')                                
 FROM APP_WATERCRAFT_COVERAGE_INFO WITH (NOLOCK) WHERE                                                           
 CUSTOMER_ID =@CUSTOMERID AND APP_ID=@APPID AND APP_VERSION_ID=@APPVERSIONID  AND BOAT_ID=@BOATID                                          
        AND  COVERAGE_CODE_ID='21'   -- 21 - MCPAY - SECTION II - MEDICAL                                                           
                                                  
        SET @MEDICALPAYMENTSOTHER= @MEDICALPAYMENT + '/' + @MEDICALPAYMENTSOTHERLIMIT   
*/

                             
---PERSONAL LIABILITY  
/*select                                   
 SELECT @PERSONALLIABILITY=ISNULL(Limit_1,'0') FROM APP_WATERCRAFT_COVERAGE_INFO WITH (NOLOCK) WHERE                   
 CUSTOMER_ID =@CUSTOMERID AND APP_ID=@APPID AND APP_VERSION_ID=@APPVERSIONID   AND BOAT_ID=@BOATID                                          
        AND  COVERAGE_CODE_ID='19'   -- 19 -LCCSL - WATER CRAFT LIABLITY                                                           

IF @PERSONALLIABILITY is null                                                     
 SET @PERSONALLIABILITY='0'
*/

---UNINSURED BOATERS                                          
        SELECT @UNINSUREDBOATERS=ISNULL(Limit_1,'0') FROM APP_WATERCRAFT_COVERAGE_INFO WITH (NOLOCK) WHERE                                                           
 CUSTOMER_ID =@CUSTOMERID AND APP_ID=@APPID AND APP_VERSION_ID=@APPVERSIONID   AND BOAT_ID=@BOATID                                          
        AND  COVERAGE_CODE_ID='24'   -- 24  UMBCS UNINSURED BOATERS  14                                                       
                                                    
 IF @UNINSUREDBOATERS = ''                                                   
 SET @UNINSUREDBOATERS='0'                    
---UNATTACHEDEQUIPMENT                                  
        SELECT @UNATTACHEDEQUIPMENT=ISNULL(Limit_1,'0'),@UNATTACHEDEQUIPMENT_DEDUCTIBLE=ISNULL(Deductible_1,0) FROM APP_WATERCRAFT_COVERAGE_INFO WITH (NOLOCK) WHERE                                                           
 CUSTOMER_ID =@CUSTOMERID AND APP_ID=@APPID AND APP_VERSION_ID=@APPVERSIONID  AND BOAT_ID=@BOATID                                           
 AND COVERAGE_CODE_ID='26'   -- "26 -EBIUE  - Increase in "Unattached Equipment" And Personal Effects Coverage "                                                          
                                                          
 IF @UNATTACHEDEQUIPMENT is not null                                                           
 BEGIN                                                                   
 SET @UNATTACHEDEQUIPMENT='$' + @UNATTACHEDEQUIPMENT                                                                  
 END                               
                          
---------------OP900_LIMIT-----------------                          
 SELECT @OP900_LIMIT = ISNULL(Limit_1,'0') FROM APP_WATERCRAFT_COVERAGE_INFO WITH (NOLOCK) WHERE                                                           
 CUSTOMER_ID =@CUSTOMERID AND APP_ID=@APPID AND APP_VERSION_ID=@APPVERSIONID  AND BOAT_ID=@BOATID                                          
 AND  COVERAGE_CODE_ID='41' --41  EBSMWL Watercraft Liability Pollution Coverage (OP 900)                           
                          
---------------END OP900_LIMIT -------------                          
                                          
                                          
END                                          
             
IF @STATE_ID=22 --MICHIGAN                                                       
BEGIN                                             

---MEDICAL PAYMENT  CODE - MACPAY                                               
/*        SELECT @MEDICALPAYMENT=ISNULL(Limit_1,'0'),  @MEDICALPAYMENTSOTHERLIMIT=ISNULL(Limit_2,'0')                                          
 FROM APP_WATERCRAFT_COVERAGE_INFO WITH (NOLOCK) WHERE                                                           
 CUSTOMER_ID =@CUSTOMERID AND APP_ID=@APPID AND APP_VERSION_ID=@APPVERSIONID   AND BOAT_ID=@BOATID                                           
 AND  COVERAGE_CODE_ID='68'   -- "21 - MCPAY - Section II - Medical  "                                                          
                                                         
 SET @MEDICALPAYMENTSOTHER= @MEDICALPAYMENT + '/' + @MEDICALPAYMENTSOTHERLIMIT                                               
---PERSONAL LIABILITY                                          
 SELECT @PERSONALLIABILITY=ISNULL(Limit_1,'0') FROM APP_WATERCRAFT_COVERAGE_INFO WITH (NOLOCK) WHERE                                    
 CUSTOMER_ID =@CUSTOMERID AND APP_ID=@APPID AND APP_VERSION_ID=@APPVERSIONID AND BOAT_ID=@BOATID                                            
        AND  COVERAGE_CODE_ID='65'   -- "19 -LCCSL - water craft liablity "                                                          
                                                    
 IF @PERSONALLIABILITY is null                                                     
 SET @PERSONALLIABILITY='0'                                            
*/

---UNINSURED BOATERS                                          
        SELECT @UNINSUREDBOATERS=ISNULL(Limit_1,'0') FROM APP_WATERCRAFT_COVERAGE_INFO WITH (NOLOCK) WHERE                                                           
 CUSTOMER_ID =@CUSTOMERID AND APP_ID=@APPID AND APP_VERSION_ID=@APPVERSIONID AND BOAT_ID=@BOATID                                            
        AND  COVERAGE_CODE_ID='70'  -- 70 UMBCS Uninsured Boaters 22                                                        
                                                    
 IF @UNINSUREDBOATERS =''                                                
 SET @UNINSUREDBOATERS='0'                                     
---UNATTACHEDEQUIPMENT                                          
        SELECT @UNATTACHEDEQUIPMENT=ISNULL(Limit_1,'0'),@UNATTACHEDEQUIPMENT_DEDUCTIBLE=ISNULL(Deductible_1,0) FROM APP_WATERCRAFT_COVERAGE_INFO WITH (NOLOCK) WHERE                                                           
 CUSTOMER_ID =@CUSTOMERID AND APP_ID=@APPID AND APP_VERSION_ID=@APPVERSIONID AND BOAT_ID=@BOATID                                          
 AND COVERAGE_CODE_ID='71'   -- "71 -EBIUE  - Increase in "Unattached Equipment" And Personal Effects Coverage "                                                          
                                                          
 IF @UNATTACHEDEQUIPMENT is not null                                                           
 BEGIN                                                                   
 SET @UNATTACHEDEQUIPMENT='$' + @UNATTACHEDEQUIPMENT                     
 END                             
                          
---------------OP900_LIMIT-----------------                          
 SELECT @OP900_LIMIT = ISNULL(Limit_1,'0') FROM APP_WATERCRAFT_COVERAGE_INFO WITH (NOLOCK) WHERE                                                           
 CUSTOMER_ID =@CUSTOMERID AND APP_ID=@APPID AND APP_VERSION_ID=@APPVERSIONID  AND BOAT_ID=@BOATID                                          
 AND  COVERAGE_CODE_ID='83' --83  EBSMWL Watercraft Liability Pollution Coverage (OP 900)                           
                          
---------------END OP900_LIMIT -------------                              
                                              
END                                          
                                          
IF @STATE_ID=49 --WISCONSIN                                 
BEGIN                                                        
/*
---MEDICAL PAYMENT  CODE - MACPAY                                     
 SELECT @MEDICALPAYMENT=ISNULL(Limit_1,'0'),  @MEDICALPAYMENTSOTHERLIMIT=ISNULL(Limit_2,'0')                                           
 FROM APP_WATERCRAFT_COVERAGE_INFO WITH (NOLOCK) WHERE                                                           
 CUSTOMER_ID =@CUSTOMERID AND APP_ID=@APPID AND APP_VERSION_ID=@APPVERSIONID AND BOAT_ID=@BOATID                                              
 AND  COVERAGE_CODE_ID='821'   -- " MCPAY - Section II - Medical  "                                                          
                           
 SET @MEDICALPAYMENTSOTHER= @MEDICALPAYMENT + '/' + @MEDICALPAYMENTSOTHERLIMIT                                                         
                                          
--PERSONAL LIABILITY                                          
 SELECT @PERSONALLIABILITY=ISNULL(Limit_1,'0') FROM APP_WATERCRAFT_COVERAGE_INFO WITH (NOLOCK) WHERE                                                           
 CUSTOMER_ID =@CUSTOMERID AND APP_ID=@APPID AND APP_VERSION_ID=@APPVERSIONID  AND BOAT_ID=@BOATID                                           
        AND  COVERAGE_CODE_ID='820'   -- "19 -LCCSL - water craft liablity "                                                          
                                                    
 IF @PERSONALLIABILITY is null                         
 SET @PERSONALLIABILITY='0'                                            
*/
                
---UNINSURED BOATERS                                          
        SELECT @UNINSUREDBOATERS=ISNULL(Limit_1,'0') FROM APP_WATERCRAFT_COVERAGE_INFO WITH (NOLOCK) WHERE                                                           
 CUSTOMER_ID =@CUSTOMERID AND APP_ID=@APPID AND APP_VERSION_ID=@APPVERSIONID  AND BOAT_ID=@BOATID                                           
        AND  COVERAGE_CODE_ID='822'   --822 UMBCS Uninsured Boaters 49                                          
                                                    
 IF @UNINSUREDBOATERS =''                                                 
 SET @UNINSUREDBOATERS='0'             
---UNATTACHEDEQUIPMENT                                          
        SELECT @UNATTACHEDEQUIPMENT=ISNULL(Limit_1,'0'),@UNATTACHEDEQUIPMENT_DEDUCTIBLE=ISNULL(Deductible_1,0) FROM APP_WATERCRAFT_COVERAGE_INFO WITH (NOLOCK) WHERE                                                           
 CUSTOMER_ID =@CUSTOMERID AND APP_ID=@APPID AND APP_VERSION_ID=@APPVERSIONID  AND BOAT_ID=@BOATID                                           
 AND COVERAGE_CODE_ID='823'   -- "823 -EBIUE  - Increase in "Unattached Equipment" And Personal Effects Coverage "                                                          
                                                          
 IF @UNATTACHEDEQUIPMENT is not null                                                           
 BEGIN                                                                   
 SET @UNATTACHEDEQUIPMENT='$' + @UNATTACHEDEQUIPMENT                                                                  
 END                                            
                                                    
---------------OP900_LIMIT-----------------                          
 SELECT @OP900_LIMIT = ISNULL(Limit_1,'0') FROM APP_WATERCRAFT_COVERAGE_INFO WITH (NOLOCK) WHERE                                                           
 CUSTOMER_ID =@CUSTOMERID AND APP_ID=@APPID AND APP_VERSION_ID=@APPVERSIONID AND BOAT_ID=@BOATID                                          
 AND  COVERAGE_CODE_ID='829' --829  EBSMWL Watercraft Liability Pollution Coverage (OP 900)                           
                          
---------------END OP900_LIMIT -------------                                            
                                          
                                          
END                                          
                                       
------------------------END POLICY LEVEL-------------------------------------------------------------------------                                              
                                  
---------------COUNTY----------------                      
DECLARE @TERRITORY NVARCHAR(25)                      
DECLARE @TYPE NVARCHAR(20)                      
                      
DECLARE @TERRCODE varchar(20)                      
DECLARE @STATECODE varchar(20)                      
                      
                      
SELECT @TERRITORY = TERRITORY FROM APP_WATERCRAFT_INFO  WHERE                       
CUSTOMER_ID =@CUSTOMERID AND APP_ID=@APPID AND APP_VERSION_ID=@APPVERSIONID  AND BOAT_ID=@BOATID                        
/*TYPE contains the Territory Code and State ID*/                      
SELECT @TYPE = TYPE FROM MNT_LOOKUP_VALUES WHERE LOOKUP_UNIQUE_ID=@TERRITORY AND  IS_ACTIVE='Y'                       
                      
SET @TERRCODE = dbo.PIECE(@TYPE,'^',1)                        
SET @STATECODE = dbo.PIECE(@TYPE,'^',2)                        
                      
SET @TERRITORYDOCKEDIN = @TERRCODE                      
                      
SELECT @STATEDOCKEDIN = STATE_NAME FROM MNT_COUNTRY_STATE_LIST WHERE STATE_ID = @STATECODE                      
                   
---------------COUNTY----------------                      
                                             
-----------------------------                                                                                  
                                                                                  
SELECT  @BOATTYPE  = ISNULL(LOOKUP_VALUE_DESC,''),@BOATTYPECODE=ISNULL(LOOKUP_VALUE_CODE,''),                                            
        @BOATSTYLE = ISNULL(TYPE,'')                                                                      
                                                                    
 FROM   APP_WATERCRAFT_INFO WITH (NOLOCK) INNER JOIN MNT_LOOKUP_VALUES WITH (NOLOCK) ON TYPE_OF_WATERCRAFT = LOOKUP_UNIQUE_ID                                                                        
                                                                                 
 WHERE  CUSTOMER_ID=@CUSTOMERID AND APP_ID=@APPID AND APP_VERSION_ID=@APPVERSIONID AND BOAT_ID=@BOATID                                                                         
                                                                                  
--------------------------------------------------------------------------------------------------------------------------                                                                                  
                                                                                   
SELECT                                                                        
 @WATERS = ISNULL(LOOKUP_VALUE_DESC,'') ,@WATERSCODE = ISNULL(LOOKUP_VALUE_CODE,'')                                                                 
FROM                                      
 APP_WATERCRAFT_INFO WITH (NOLOCK) INNER JOIN MNT_LOOKUP_VALUES WITH (NOLOCK) ON WATERS_NAVIGATED = LOOKUP_UNIQUE_ID                                                                                   
WHERE                                                        
 CUSTOMER_ID=@CUSTOMERID AND APP_ID=@APPID AND APP_VERSION_ID=@APPVERSIONID AND BOAT_ID=@BOATID                                                               
--------------------------------------------------------------------------------------------------------------------------                                                                     
                                              
                                                                                 
---------------------------------------------------------------------------------------------------------------------------                                                                                  
 SELECT                                 
 @CONSTRUCTION = ISNULL(LOOKUP_VALUE_DESC,'') ,@CONSTRUCTIONCODE = ISNULL(LOOKUP_VALUE_CODE,'')                                                                                     
 FROM                                                                                   
 APP_WATERCRAFT_INFO WITH (NOLOCK) INNER JOIN MNT_LOOKUP_VALUES WITH (NOLOCK) ON HULL_MATERIAL = LOOKUP_UNIQUE_ID                                                                                   
 WHERE                                                                                  
 CUSTOMER_ID=@CUSTOMERID AND APP_ID=@APPID AND APP_VERSION_ID=@APPVERSIONID AND BOAT_ID=@BOATID                                                                                     
                                                                      
                                              
--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
   
   
      
      
        
          
            
              
                                          
                                                            
 SELECT @COUNTYOFOPERATION =  ISNULL(MNT.LOOKUP_VALUE_DESC,''),                      
 @COUNTYCODE        =  ISNULL(LOOKUP_VALUE_CODE,'') --NOT REQUIRED                      
 FROM MNT_LOOKUP_VALUES MNT  WITH (NOLOCK) inner join APP_WATERCRAFT_INFO AWI  WITH (NOLOCK)                                                                              
 ON MNT.LOOKUP_UNIQUE_ID=AWI.TERRITORY                      
 WHERE  AWI.CUSTOMER_ID=@CUSTOMERID AND AWI.APP_ID=@APPID AND AWI.APP_VERSION_ID=@APPVERSIONID and AWI.BOAT_ID=@BOATID                  
                      
-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- 
  
    
     
       
        
          
            
              
                                                                       
                                                                         
/*                                              
                                                          
Discount is valid for:                                                            
Outboard (All types)                                                            
Pontoon                                                            
Inboard (All types)                                                            
Sailboat                                                            
Jetski                                                            
Waverunner                                                            
Skiboat                                                            
Bass Boat                                                            
Not available in Trailer(all types) and Mini jetboat                                       
         select TYPE_OF_WATERCRAFT from APP_WATERCRAFT_INFO where                                                   
select * from mnt_lookup_tables where lookup_id='1201'                                         
select * from mnt_lookup_values where lookup_id='1201'                                          
*/                                                                   
--11390 JS Jet Ski                                                           
--11490 TRAI Trailer                                           
--11445 JT JetSki Trailer                                           
--11446 WRT WaveRunner Trailer                                           
--11373 MJB Mini Jet Boat                                           
--11385 PWT Class 131 Mini-Jetboats                                    
--11386 Waverunner              
--11387 Jetski (w/Lift Bar)                                                       
                                                            
DECLARE @BCOUNT INT                                                            
SET @BCOUNT=0                       
                                                           
SELECT @BCOUNT=COUNT(BOAT_ID) FROM  APP_WATERCRAFT_INFO  WITH (NOLOCK)                                                                   
WHERE                                                                     
 CUSTOMER_ID=@CUSTOMERID AND APP_ID=@APPID AND APP_VERSION_ID=@APPVERSIONID AND                               
 (TYPE_OF_WATERCRAFT <> 11390 AND TYPE_OF_WATERCRAFT <> 11490 AND                              
TYPE_OF_WATERCRAFT <> 11445 AND TYPE_OF_WATERCRAFT <> 11446 AND                              
TYPE_OF_WATERCRAFT <> 11373 AND TYPE_OF_WATERCRAFT <> 11385                              
AND TYPE_OF_WATERCRAFT <> 11386 AND TYPE_OF_WATERCRAFT<>11387)  AND ISNULL(IS_ACTIVE,'Y')='Y'                                                              
                                                           
IF(@BCOUNT > 1 )                                                                                  
  BEGIN                                                                                 
    SET @MULTIBOATCREDIT = 'Y'                                                                                  
  END                                                                                  
ELSE                                                                    
  BEGIN                             
    SET @MULTIBOATCREDIT = 'N'                                                                  
  END                               
                                     
                                                                   
                                                                    
--                                                                                
IF(@REMOVESAILBOAT IS NULL)                    
 BEGIN                                                             
   SET @REMOVESAILBOAT = ''                                                                                  
 END                                                                                 
---                                                                                
IF(@DUALOWNERSHIP IS NULL)                                                                                  
 BEGIN                                                        
   SET @DUALOWNERSHIP = ''                                                                                  
 END                                                                                  
--                                                                                
                                        
---                                                                                
IF(@HALONFIRE IS NULL)                                                                                  
 BEGIN                                                                             
   SET @HALONFIRE = ''                           
 END                                     
--                                                                                
IF(@SHORESTATION IS NULL)                                                                                  
 BEGIN                                                                                  
   SET @SHORESTATION = ''                        
 END                                                                
---                                                            
IF(@DIESELENGINE IS NULL)                                                                                  
 BEGIN                                                                                  
   SET @DIESELENGINE = ''                                                                                  
 END                                                                                  
--                           
IF(@COUNTYOFOPERATION IS NULL)                                                                                  
 BEGIN                                                                                  
   SET @COUNTYOFOPERATION = ''                                                                        
 END                                                                                 
                                                                        
                                                                      
IF (@MANUFACTURER is NULL)                                                                      
 BEGIN                          
   SET @MANUFACTURER = ''                                                                                  
 END                                                                                 
                                                                      
                                        
-----------------------------                                                                                  
-- FINAL SELECT [TAGS] --START                                                                                  
-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- 
  
   
---START===================POWERSQUADRONCOURSE/COASTGUARDAUXILARYCOURSE/HAS_5_YEARSOPERATOREXPERIENCE========            
 
 SET @COASTGUARDAUXILARYCOURSE='N'  
 IF EXISTS( 
		SELECT AWDD.CUSTOMER_ID FROM APP_WATERCRAFT_DRIVER_DETAILS AWDD 
		INNER JOIN 
			APP_OPERATOR_ASSIGNED_BOAT AOAB ON 
			AWDD.CUSTOMER_ID = AOAB.CUSTOMER_ID
			AND AWDD.APP_ID = AOAB.APP_ID
			AND AWDD.APP_VERSION_ID = AOAB.APP_VERSION_ID
			AND AWDD.DRIVER_ID = AOAB.DRIVER_ID
		INNER JOIN 
			APP_WATERCRAFT_INFO AWI ON
			AWI.CUSTOMER_ID = AOAB.CUSTOMER_ID
			AND AWI.APP_ID = AOAB.APP_ID
			AND AWI.APP_VERSION_ID = AOAB.APP_VERSION_ID
			AND AWI.BOAT_ID = AOAB.BOAT_ID
			WHERE AWDD.CUSTOMER_ID=@CUSTOMERID AND AWDD.APP_ID=@APPID AND AWDD.APP_VERSION_ID=@APPVERSIONID
			AND AWDD.DRIVER_ID IN (SELECT DRIVER_ID FROM APP_OPERATOR_ASSIGNED_BOAT     
  			WHERE CUSTOMER_ID=@CUSTOMERID AND APP_ID=@APPID AND APP_VERSION_ID=@APPVERSIONID AND BOAT_ID=@BOATID AND APP_VEHICLE_PRIN_OCC_ID=11936)
 			AND AWI.BOAT_ID=@BOATID 
 			AND AWDD.WAT_SAFETY_COURSE=10963)    
  				BEGIN    
   					SET @COASTGUARDAUXILARYCOURSE='Y'                 
  				END    
  			
------------------------------------------------------------------------------------    
---@POWERSQUADRONCOURSE     
SET @POWERSQUADRONCOURSE='N' 
--If a boat has More than 1 Assigned driver then chk the Principal Assigned Driver Info only    
 IF EXISTS( 
		SELECT AWDD.CUSTOMER_ID FROM APP_WATERCRAFT_DRIVER_DETAILS AWDD
		INNER JOIN 
			APP_OPERATOR_ASSIGNED_BOAT AOAB ON 
			AWDD.CUSTOMER_ID = AOAB.CUSTOMER_ID
			AND AWDD.APP_ID = AOAB.APP_ID
			AND AWDD.APP_VERSION_ID = AOAB.APP_VERSION_ID
			AND AWDD.DRIVER_ID = AOAB.DRIVER_ID
		INNER JOIN 
			APP_WATERCRAFT_INFO AWI ON
			AWI.CUSTOMER_ID = AOAB.CUSTOMER_ID
			AND AWI.APP_ID = AOAB.APP_ID
			AND AWI.APP_VERSION_ID = AOAB.APP_VERSION_ID
			AND AWI.BOAT_ID = AOAB.BOAT_ID
			WHERE AWDD.CUSTOMER_ID=@CUSTOMERID AND AWDD.APP_ID=@APPID AND AWDD.APP_VERSION_ID=@APPVERSIONID
			AND AWDD.DRIVER_ID IN (SELECT DRIVER_ID FROM APP_OPERATOR_ASSIGNED_BOAT     
	  		WHERE CUSTOMER_ID=@CUSTOMERID AND APP_ID=@APPID AND APP_VERSION_ID=@APPVERSIONID AND BOAT_ID=@BOATID AND APP_VEHICLE_PRIN_OCC_ID=11936)
	 		AND AWI.BOAT_ID=@BOATID 
	 		AND AWDD.CERT_COAST_GUARD=10963)    
	  			BEGIN    
	   				SET @POWERSQUADRONCOURSE='Y'                 
	  			END    
	  		    
------------------------------------------------------------------------------------    
---@HAS_5_YEARSOPERATOREXPERIENCE     
---------Boating Experience Since (YYYY) Field ..                         
DECLARE @EFFECTIVE_DATE VARCHAR(20)                        
SELECT @EFFECTIVE_DATE = CONVERT(VARCHAR(20),APP_EFFECTIVE_DATE,109) FROM APP_LIST                         
WHERE CUSTOMER_ID =@CUSTOMERID AND APP_ID=@APPID AND APP_VERSION_ID=@APPVERSIONID                         
SET @HAS_5_YEARSOPERATOREXPERIENCE='N'    
 --If a boat has More than 1 Assigned driver then chk the Principle Assigned Driver Info only    
 IF EXISTS 
	(
		SELECT AWDD.CUSTOMER_ID FROM APP_WATERCRAFT_DRIVER_DETAILS AWDD 
		INNER JOIN 
			APP_OPERATOR_ASSIGNED_BOAT AOAB ON 
			AWDD.CUSTOMER_ID = AOAB.CUSTOMER_ID
			AND AWDD.APP_ID = AOAB.APP_ID
			AND AWDD.APP_VERSION_ID = AOAB.APP_VERSION_ID
			AND AWDD.DRIVER_ID = AOAB.DRIVER_ID
			AND     
		  	(    
		  		isnull(AWDD.DRIVER_COST_GAURAD_AUX,0) > 0  AND YEAR(@EFFECTIVE_DATE) - AWDD.DRIVER_COST_GAURAD_AUX>=5    
		  	) 
		INNER JOIN 
			APP_WATERCRAFT_INFO AWI ON
			AWI.CUSTOMER_ID = AOAB.CUSTOMER_ID
			AND AWI.APP_ID = AOAB.APP_ID
			AND AWI.APP_VERSION_ID = AOAB.APP_VERSION_ID
			AND AWI.BOAT_ID = AOAB.BOAT_ID
			WHERE AWDD.CUSTOMER_ID=@CUSTOMERID AND AWDD.APP_ID=@APPID AND AWDD.APP_VERSION_ID=@APPVERSIONID
			AND AWDD.DRIVER_ID IN (SELECT DRIVER_ID FROM APP_OPERATOR_ASSIGNED_BOAT     
		  	WHERE CUSTOMER_ID=@CUSTOMERID AND APP_ID=@APPID AND APP_VERSION_ID=@APPVERSIONID AND BOAT_ID=@BOATID AND APP_VEHICLE_PRIN_OCC_ID=11936)
		 	AND AWI.BOAT_ID=@BOATID)    
		  		BEGIN    
		   			SET @HAS_5_YEARSOPERATOREXPERIENCE='Y'                 
		 	 	END    
		  		
---END===================POWERSQUADRONCOURSE/COASTGUARDAUXILARYCOURSE/HAS_5_YEARSOPERATOREXPERIENCE========            
                                                                                
 SELECT                                                                                  
  @BOATTYPE   AS  BOATTYPE,                                                                                  
  @BOATTYPECODE    AS  BOATTYPECODE,                                                                                  
  @YEAR        AS  YEAR,                                                                              
  @AGE      AS AGE, -- 0                                                                                  
  @MODEL        AS  MODEL,                                                                        
  @MANUFACTURER  AS MANUFACTURER,                                                                                   
  @SERIALNUMBER   AS  SERIALNUMBER, --0                                                                        
  @LENGTH       AS  LENGTH, --0                                                                                  
  @HORSEPOWER      AS  HORSEPOWER, --0                                                                        
                            
  @CAPABLESPEED     AS  CAPABLESPEED, --0                                            
  @WATERS       AS  WATERS,                                          
  @WATERSCODE   AS  WATERSCODE,                                                                              
  @COVERAGEBASIS  AS COVERAGEBASIS, --ANA    
  @MARKETVALUE     AS  MARKETVALUE, --0                                                 
                                                        
  @CONSTRUCTION     AS  CONSTRUCTION,                                                                                  
  @CONSTRUCTIONCODE    AS  CONSTRUCTIONCODE,                                                                                  
  -- THESE FIELDS ARET TO BE MODIFIED                                                         
  @COUNTYOFOPERATION    AS  COUNTYOFOPERATION,                                                                                  
  @DIESELENGINE    AS  DIESELENGINE,                                                                                  
  @SHORESTATION      AS  SHORESTATION,                                                                       
  @HALONFIRE     AS  HALONFIRE,                                                                                  
  @LORANNAVIGATIONSYSTEM  AS  LORANNAVIGATIONSYSTEM,                                                                                  
  @DUALOWNERSHIP      AS  DUALOWNERSHIP,                                                                                  
  @REMOVESAILBOAT    AS  REMOVESAILBOAT,                                                                   
  @MULTIBOATCREDIT   AS  MULTIBOATCREDIT ,                                              
  @ISGRAND AS  ISGRAND,                                            
  ISNULL(@BOATSTYLE,'N')         AS  BOATSTYLECODE,                                          
--POLICY LEVEL NODES --                                          
  isnull(@PERSONALLIABILITY,'0') AS PERSONALLIABILITY,                                          
  @MEDICALPAYMENT  AS  MEDICALPAYMENT,                                          
  @MEDICALPAYMENTSOTHER AS  MEDICALPAYMENTSOTHER,                           
  @MEDICALPAYMENTSOTHERLIMIT  AS  MEDICALPAYMENTSOTHERLIMIT,                                          
  @UNINSUREDBOATERS    AS   UNINSUREDBOATERS,                                          
  @UNATTACHEDEQUIPMENT   AS  UNATTACHEDEQUIPMENT,                                          
  @OP900_LIMIT AS OP900_LIMIT,                          
  @UNATTACHEDEQUIPMENT_DEDUCTIBLE   AS   UNATTACHEDEQUIPMENT_DEDUCTIBLE   ,                      
  @TERRITORYDOCKEDIN      AS  TERRITORYDOCKEDIN,                      
  UPPER(@STATEDOCKEDIN)          AS  STATEDOCKEDIN,                      
  @COUNTYCODE AS    COUNTYCODE ,            
  --            
  --@WAT_SAFETY_COURSE as WAT_SAFETY_COURSE,            
  --@CERT_COAST_GUARD as CERT_COAST_GUARD,            
  @POWERSQUADRONCOURSE as POWERSQUADRONCOURSE,      
  @COASTGUARDAUXILARYCOURSE as COASTGUARDAUXILARYCOURSE,      
  @HAS_5_YEARSOPERATOREXPERIENCE as HAS_5_YEARSOPERATOREXPERIENCE            
  --                     
  --@MVR       AS  MVR,                                                                                  
  --@SUMOFVIOLATIONPOINTS AS  SUMOFVIOLATIONPOINTS,                                                                   
  --@SUMOFACCIDENTPOINTS  AS  SUMOFACCIDENTPOINTS                                                                                  
                 
-----------------------------                                                                                  
-- FINAL SELECT [TAGS] --END                                                                            
------------------------------                                                  
END                                                                                 
                                                 
                                                          
                                                        
                                                        
                                                        
                                                      
                                                    
                                                  
                                                
                                              
                                            
                                          
                                          
                                        
                                      
                                    
                                  
                                
                              
                            
                          
                        
                      
                      
                      
                      
                      
              
                    
              
                
              
              
            
          
          
        
        
      
      
      
      
      
    
    
    
    
  










GO

