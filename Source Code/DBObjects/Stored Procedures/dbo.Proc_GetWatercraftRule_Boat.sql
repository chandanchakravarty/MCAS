IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetWatercraftRule_Boat]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetWatercraftRule_Boat]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                                                                                                                                                                              
Proc Name                : Dbo.Proc_GetWatercraftRule_Boat 1141,1,1,1,'s','w001','<RULES></RULES>'         
Created by               : Praveen Kasana                                                                                                                                                                             
Date                     : 7 Dec.,2005                                                                                                                                                
Purpose                  : To get the Watercraft Details info for Rules                                                                                                                                                                  
Revison History          :                                                                                                                                                                              
Used In                  : Wolverine                                                                                                                                                                              
------------------------------------------------------------                                                                                                                                                                              
Date     Review By          Comments                                                                                                                                                                              
------   ------------       -------------------------*/         
--drop proc dbo.Proc_GetWatercraftRule_Boat                                                                                                                    
CREATE proc [dbo].[Proc_GetWatercraftRule_Boat]                                                                                                                                                                                    
(                                                                                                                                                                                        
@CUSTOMERID    int,                                                                                                                                                                                        
@APPID    int,                                                                                                                                                                                        
@APPVERSIONID   int,                                                                                                                                                                              
@BOATID int ,                                                                                                                                               
@DESC varchar(10),                                                                                                                                                  
@USER varchar(50),   ---Added to Check the Wolv User           
@DEFAULT_DOC TEXT                                                                                                                                                                             
)                               
                                                                                                                                                           
AS                 
BEGIN 
	-- BOAT INFO                                
	--DECLARE @BOAT_NO INT      
	DECLARE @BOAT_NO VARCHAR(20)  
	--DECLARE @BOAT_YEAR INT                                                    
	DECLARE @BOAT_YEAR VARCHAR(15)                                                                
	DECLARE @MODEL NVARCHAR(75)                                                       
	DECLARE @TYPE_OF_WATERCRAFT NVARCHAR(75)                                                               
	DECLARE @MAKE NVARCHAR(75)                                                                          
	DECLARE @HULL_ID_NO NVARCHAR(75)                                                                                            
	DECLARE @STATE_REG NVARCHAR(5)                                                                                             
	--DECLARE @HULL_MATERIAL INT                                                                                                   
	DECLARE @HULL_MATERIAL VARCHAR(30)                                                                                                                                        
	DECLARE @INSURING_VALUE  CHAR                                                                                                                                      
	DECLARE @MAX_INSURING_VALUE INT                                                                                                                       
	DECLARE @WATERS_NAVIGATED NVARCHAR(75)                                                                                                 
	DECLARE @LENGTH NVARCHAR(75)                                                                                                                                          
	DECLARE @MAX_LENGTH CHAR                                                                                          
	DECLARE @INT_MAX_LENGTH DECIMAL                                                                                                                    
	DECLARE @INT_INCH INT                                                                                                                      
	DECLARE @AGE CHAR                                                                                                                           
	DECLARE @INT_AGE INT                                                                                                                                                
	DECLARE @MAX_SPEED CHAR                                                                                                                                                
	DECLARE @INT_MAXSPEED INT                                                                                                                        
	DECLARE @INTSTATE_REG INT                                                                                                                                       
	DECLARE @MAX_INSURING_WMOTOR CHAR                                                                                                               
	DECLARE @TERRITORY NVARCHAR(25)                             
	--COV TYPE                                                            
	DECLARE @COV_TYPE_BASIS VARCHAR(100)                                      
	--WATER SAFETY                                                                         
	DECLARE @INT_WAT_SAFETY_COURSE INT                                                                                 
	DECLARE @WAT_SAFETY_COURSE CHAR 
	--EFFECTIVE DATE STATUS                           
	DECLARE @IS_EFFECTIVE_DATE  CHAR                                                        
	DECLARE @IS_EFFECTIVE_DATE_OTHER CHAR                                    
	--OPERATOR CHECK                                                          
	DECLARE @IS_OP_UNDER_21 CHAR                                    
	DECLARE @IS_OP_UNDER_16 CHAR                                                                          
	DECLARE @EFFECTIVE_YEAR INT                                    
	---TYPE OF WATERCRAFT ATTACH WITH HOME POLICY                                                            
	DECLARE @WOLVERINE_INSURE CHAR                       
	--LOCATION USED / STORED                                                       
	DECLARE @LOCATION_ADDRESS VARCHAR(200)                                                      
	DECLARE @LOCATION_CITY VARCHAR(50)                                                      
	DECLARE @LOCATION_STATE VARCHAR(50)                                                      
	DECLARE @LOCATION_ZIP VARCHAR(20)                                                      
                                                                               
IF EXISTS (SELECT CUSTOMER_ID FROM APP_WATERCRAFT_INFO 			WHERE CUSTOMER_ID=@CUSTOMERID and  APP_ID=@APPID  and  APP_VERSION_ID=@APPVERSIONID   and BOAT_ID=@BOATID		  )                                                                                             BEGIN                                                                          
	SELECT   
			@BOAT_NO=ISNULL(CONVERT(VARCHAR(20),BOAT_NO),''),                                                    
			@BOAT_YEAR=ISNULL(CONVERT(VARCHAR(15),YEAR),''),@MAKE=ISNULL(UPPER(MAKE),''),                                                                                                                  
			@MODEL=ISNULL(UPPER(MODEL),''),                                                                                                                                 
			@LENGTH=ISNULL(LENGTH,''),@TYPE_OF_WATERCRAFT=ISNULL(TYPE_OF_WATERCRAFT,''),                                                                                                                           
			--@HULL_ID_NO=ISNULL(HULL_ID_NO,''),
			@STATE_REG=ISNULL(STATE_REG,''),                                                                                                                                          
			@HULL_MATERIAL=ISNULL(CONVERT(VARCHAR(30),HULL_MATERIAL),''),                                                                                                                                      
			@MAX_INSURING_VALUE=ISNULL(INSURING_VALUE,0),@WATERS_NAVIGATED=ISNULL(WATERS_NAVIGATED,''),                                                                                                                                           
			--@INT_MAX_LENGTH=ISNULL(CONVERT(DECIMAL,LENGTH),0),                                                                                              
			@INT_MAX_LENGTH= CASE WHEN ISNUMERIC(LENGTH)=0 THEN 0 ELSE                                                                                 
			ISNULL(CONVERT(DECIMAL,LENGTH),0) END,                                                                                              
			@INT_INCH=ISNULL(INCHES,0),@INT_AGE=[YEAR], --@INT_AGE=(YEAR(GETDATE()) - YEAR),                                                                                                                                     
			@INT_MAXSPEED=ISNULL(MAX_SPEED,0),@INTSTATE_REG=ISNULL(STATE_REG,0),@TERRITORY=ISNULL(TERRITORY,''),        
			@COV_TYPE_BASIS=ISNULL(CONVERT(VARCHAR(100),COV_TYPE_BASIS),''),          
			@LOCATION_ADDRESS=ISNULL(CONVERT(VARCHAR(100),LOCATION_ADDRESS),''),                                         
			@LOCATION_CITY=ISNULL(LOCATION_CITY,''),                  
			@LOCATION_STATE=ISNULL(LOCATION_STATE,''),                     
			@LOCATION_ZIP=ISNULL(LOCATION_ZIP,'')                        
	FROM APP_WATERCRAFT_INFO  WITH(NOLOCK)                                                                                                                                                         
	WHERE CUSTOMER_ID=@CUSTOMERID and  APP_ID=@APPID  and APP_VERSION_ID=@APPVERSIONID   and BOAT_ID=@BOATID                                                                                                                           
END                                                                                              
ELSE 
BEGIN                                                                                        
	SET @BOAT_NO =''                                                                                                                                             
	SET @BOAT_YEAR=''                                                                                        
	SET @MODEL =''                                                                                                                                    
	SET @LENGTH=''                                                                                                                   
	SET @TYPE_OF_WATERCRAFT=''                                                                  
	SET @MAKE =''                                                         
	SET @HULL_ID_NO=''                                  
	SET @HULL_MATERIAL =''                                                                                                               
	SET @INSURING_VALUE =''                                                                                                                                                           
	SET @WATERS_NAVIGATED =''                                                                                                             
	--                                                                                    
	SET @MAX_LENGTH=''                                                                                                                                        
	SET @INT_MAX_LENGTH=0                                                                                
	SET @INT_INCH = 0                                                                                                                          
	SET @AGE=''                                                                         
	SET @INT_AGE=0                                                                                                                                        
	SET @MAX_SPEED=''                    
	SET @INT_MAXSPEED=0                                                                                   
	SET @STATE_REG=''                                                      
	SET @INTSTATE_REG=0                                                          
	SET @INSURING_VALUE=''                                                                               
	SET @MAX_INSURING_VALUE=0           
	SET @MAX_INSURING_WMOTOR=''                                                       
	SET @TERRITORY=''                  
	SET @COV_TYPE_BASIS=''                               
	SET @LOCATION_ADDRESS=''                                           
	SET @LOCATION_CITY=''                                                      
	SET @LOCATION_STATE=''                              
	SET @LOCATION_ZIP=''                                                                                                                             
END                                                                                                 
                        
--=====================================                    
DECLARE @IDOC INT                     
--CREATE AN INTERNAL REPRESENTATION OF THE XML DOCUMENT.                    
EXEC SP_XML_PREPAREDOCUMENT @IDOC OUTPUT, @DEFAULT_DOC                    
-- EXECUTE A SELECT STATEMENT THAT USES THE OPENXML ROWSET PROVIDER.                    
DECLARE @DEFAULT_MAX_LENGTH VARCHAR(20)                    
DECLARE @DEFAULT_AGREED_MAX_LENGTH VARCHAR(20) 
DECLARE @DEFAULT_AGE VARCHAR(20)                    
DECLARE @DEFAULT_MAX_SPEED VARCHAR(20)                    
                    
SELECT   @DEFAULT_MAX_LENGTH=MAX_LENGTH,                    
         @DEFAULT_AGREED_MAX_LENGTH = AGREED_BOAT_LENGTH, 
 	 @DEFAULT_AGE=AGE,         
 	 @DEFAULT_MAX_SPEED=MAX_SPEED                    
                    
FROM     OPENXML (@IDOC, '/RULES/DATE[@LOB="BOAT"]/VALUE',1)                    
         WITH(                    
  MAX_LENGTH  VARCHAR(10),                     
  AGREED_BOAT_LENGTH VARCHAR(10),                    
  AGE  VARCHAR(10),                    
  MAX_SPEED VARCHAR(10)                    
  )                    
           

EXEC SP_XML_REMOVEDOCUMENT @IDOC         
--=====================================                                                                                                                                  
---Check Rules--                                                                                                      
--1                                                                                                               
/*if(@INT_MAX_LENGTH>26 or (@INT_MAX_LENGTH=26 and @INT_INCH>0))                                                                                                                                                
begin                                                                                                                                             
set @MAX_LENGTH='Y'                                                                                                                                                
end else if(@INT_MAX_LENGTH<>0)                                           
begin                                                                                                                    
 set @MAX_LENGTH='N'                                                                     
end */                                                               
   
                                                                                                                                      
--2                                                                             
/*if(@INT_AGE>20)                                           
begin                                                                                                              
set @AGE='Y'                                    
end                                
else if(@INT_AGE<>0)                                                                       
begin                           
set @AGE='N'                                                   
end */                                                            
--3             
IF(@INT_MAX_LENGTH>@DEFAULT_MAX_LENGTH  OR (@INT_MAX_LENGTH=@DEFAULT_MAX_LENGTH AND @INT_INCH>0))  --40                                                                               
                                                                           
BEGIN                                                                 
SET @LENGTH='Y'                                                                                                                                                
END                                                                                                  
ELSE IF(@INT_MAX_LENGTH<>0)                                   
BEGIN                                                                       
SET @LENGTH='N'                                                                                                    
END                                                                                                                                                
--4 
IF(@INT_MAXSPEED>@DEFAULT_MAX_SPEED)   --65                                  
	BEGIN                            
	SET @MAX_SPEED='Y'                                                                            
	END                                                                                                 
ELSE IF(@INT_MAXSPEED<>0)       
	BEGIN                                                                  
	SET @MAX_SPEED='N'                                                                                                                                  
	END 
ELSE IF(@INT_MAXSPEED=0)                                                                                                                                               
BEGIN                                        
SET @MAX_SPEED=''                                                                                                               
END                  
--******************************                
--if Insuring value is zero and coverage type is                 
 --if (@MAX_INSURING_VALUE=0 and (@COV_TYPE_BASIS=11758 or @COV_TYPE_BASIS=11759) )                
--begin                
--set @INSURING_VALUE=''                 
--end                                                
                                                                                                              
--if type of watercraft is selected as Trailer types then speed is not mandatory                                                                                       
 IF(@TYPE_OF_WATERCRAFT='11445' OR @TYPE_OF_WATERCRAFT='11490' OR @TYPE_OF_WATERCRAFT='11497' OR @TYPE_OF_WATERCRAFT='11496')                                                                                                              
 BEGIN                                                                                                               
	 IF(@MAX_SPEED='')                                                                                                            
		BEGIN                                                                          
		SET @MAX_SPEED='N'           
		END                
 END                                                            
--5                                                    
if(@intSTATE_REG=0)                                                                                                
begin                                                                                                                                         
 set @STATE_REG =''                                                                                                                                                
end                                                                                       
else if(@intSTATE_REG=14 or  @intSTATE_REG=22 or @intSTATE_REG=49)                                                                                  
begin                                                                                                                                                 
 set @STATE_REG='N'                                          
end                                                                  
else                                                                                                                     
begin                                                                       
 set @STATE_REG='Y'                                                               
end                                                                       
--If  INSURING_VALUE is 0 and Coverage type is selected as  Actual Cash Value-11758  or  Agreed Value-11759               
              
IF(@MAX_INSURING_VALUE=0 AND ( @COV_TYPE_BASIS=11758 OR  @COV_TYPE_BASIS=11759))              
	BEGIN  
	SET @INSURING_VALUE ='N'            
	END         
ELSE -- INSURING VALUE CAN BE LESS THAN =0 IF TYPE BASIS='NOT APPLICABLE'            
	BEGIN                
	SET @INSURING_VALUE ='Y'                
	END                 
                
--****************                                   
--6                                                                                                          
--If they select Agreed Value check the following on the Watercraft rating info Tab  
--The Insurance Value incl Motor field limit can not exceed $75,000                                                    
--The length fields can not exceed 26 ft                                                     
--The age of the boat can not exceed 20 years :Take the effective date of the policy and subtract the Year of the boat if the result is greater than 20                                                                                         
--EBPPDAV  (Agreed Value)                                                                                          
IF EXISTS(SELECT COVERAGE_CODE_ID FROM APP_WATERCRAFT_COVERAGE_INFO A             
LEFT JOIN MNT_COVERAGE B ON A.COVERAGE_CODE_ID = B.COV_ID                                                                                                 
WHERE  CUSTOMER_ID=@CUSTOMERID AND  APP_ID=@APPID  AND  APP_VERSION_ID=@APPVERSIONID   AND BOAT_ID=@BOATID AND B.COV_CODE ='EBPPDAV')                                                                                                      
BEGIN                                                                                                       
		IF(@MAX_INSURING_VALUE>75000)                                                               
			BEGIN                                       
				SET @INSURING_VALUE='Y'                                       
			END                                                                                     
		ELSE IF(@MAX_INSURING_VALUE<>0)                                                                                                   
			BEGIN                              
				SET  @INSURING_VALUE='N'                                         
			END                                                                                                              
		ELSE IF(@MAX_INSURING_VALUE=0 AND (@COV_TYPE_BASIS=11758 OR @COV_TYPE_BASIS=11759))                                                                                                                  
			BEGIN                                                                                                                                       
			SET  @INSURING_VALUE=''                                                        
			END                                                                                                                           
		ELSE  -- INSURING VALUE CAN BE LESS THAN =0 IF TYPE BASIS='NOT APPLICABLE'            
		BEGIN                
		SET @INSURING_VALUE ='N'                
		END             
--ADDED ON 8 MAY 2006                                                                              
--CHECK THE 26 FT FOR AGREED VALUE BOATS :                                                                               
	IF(@INT_MAX_LENGTH>@DEFAULT_AGREED_MAX_LENGTH OR (@INT_MAX_LENGTH=26 AND @INT_INCH>0))                                      
		BEGIN                                 
		SET @MAX_LENGTH='Y'   
		END
	ELSE IF(@INT_MAX_LENGTH<>0)                                                                                                                                 
		BEGIN                                                               
		SET @MAX_LENGTH='N'                 
		END          
	 SET @LENGTH='N' --if Agree Value Boat then set Lenght to N for boats above 40ft       
--End Added the Lenght check                  
----Check for BOAT AGE                                                     
	DECLARE @EFF_DATE int                                                    
	SELECT @EFF_DATE = YEAR(APP_EFFECTIVE_DATE)                                                 
	FROM APP_LIST WHERE CUSTOMER_ID=@CUSTOMERID and APP_ID=@APPID and APP_VERSION_ID=@APPVERSIONID                                          
	                                                    
	IF((@EFF_DATE-@INT_AGE)>20)                       
		BEGIN                                                            
			SET @AGE='Y'                            
		END                
	ELSE IF(@INT_AGE<>0)                                                                                                                                                
		BEGIN                                                                                  
			SET @AGE='N'                                                                                                     
		END                         
----END Check for BOAT AGE                                                                      
END                                                                                   
ELSE                           
BEGIN                                                                                                 
	IF(@MAX_INSURING_VALUE=0 AND (@COV_TYPE_BASIS=11758 OR @COV_TYPE_BASIS=11759))                                                                                     
		BEGIN                                                      
			SET  @INSURING_VALUE=''                                               
		END                                                                                      
	ELSE                                         
		BEGIN                                                                                                       
			SET @INSURING_VALUE='N'                                                                                                      
		END                                                                    
END                                                        
----------------------------------------------------------------------------------------------------------------------                                    
                                                                                                  
--EBPPDACV (Actual Cash Value)                                                                                                         
IF EXISTS(SELECT COVERAGE_CODE_ID FROM APP_WATERCRAFT_COVERAGE_INFO A   LEFT JOIN MNT_COVERAGE B ON A.COVERAGE_CODE_ID = B.COV_ID                                                                              
 WHERE  CUSTOMER_ID=@CUSTOMERID AND  APP_ID=@APPID AND  APP_VERSION_ID=@APPVERSIONID  AND BOAT_ID=@BOATID AND (B.COV_CODE ='EBPPDACV' or B.COV_CODE='EBPPDJ'))                                                                              
 BEGIN                                        
                                                               
--7 Check for Boats(w/motor and Insuring values over 200,000...                                                      
-- if((@TYPE_OF_WATERCRAFT='11487' or @TYPE_OF_WATERCRAFT='11489' or @TYPE_OF_WATERCRAFT='11374') and @MAX_INSURING_VALUE>200000) 
-- changed for all boats                                     
	 IF(@MAX_INSURING_VALUE>200000)                  
		BEGIN                     
		SET @MAX_INSURING_WMOTOR='Y'                          
		END                          
	 ELSE IF(@MAX_INSURING_VALUE<>0)                                                                                                                            
		BEGIN                                                   
		SET @MAX_INSURING_WMOTOR='N'           
		END 
END                                                       
ELSE                                                                                                      
BEGIN                          
SET @MAX_INSURING_WMOTOR='N'                                                 
END                                                      
                                                       
----------------------------------------------------------                                               
--8-- Any racing perfo Model other than Sailboat less than 26 feet...          
/*declare @IS_PARTICIPATE_RACE char                                                                                                                           
declare @INT_PARTICIPATE_RACE int                                       
if exists(select APP_ID  from APP_WATERCRAFT_GEN_INFO                                                              
where  CUSTOMER_ID=@CUSTOMERID and APP_ID= @APPID and APP_VERSION_ID = @APPVERSIONID)                                                                                                                                              
begin                                                                                                                         
select @INT_PARTICIPATE_RACE=isnull(PARTICIPATE_RACE,-1)                                                           
from APP_WATERCRAFT_GEN_INFO  where  CUSTOMER_ID=@CUSTOMERID and APP_ID= @APPID and APP_VERSION_ID = @APPVERSIONID                                                                                                                                            

end                                                                     
else                                                                                                              
begin                                                                                             
set @INT_PARTICIPATE_RACE =0                                            
 end                                                                                                                                                                                 
--Type of watercraft is Sailboat (11372)                                                                                                                      
if(@TYPE_OF_WATERCRAFT<>'11372' and @INT_MAX_LENGTH<26 and @INT_PARTICIPATE_RACE=1)                                 
 begin                                                                              
 set @IS_PARTICIPATE_RACE='Y'                                                      
 end                                                        
else                                              
 begin                                                                                                               
 set  @IS_PARTICIPATE_RACE='N'                      
 end  */       
-----------------------          
/*If Watercraft Type Field is Sailboat ,
Length field is under 26 ft and Remove Sailboat Racing Exclusion is yes Submit*/                                                                 

/*Grandfathered Check on Sailboat 04 May 2007*/
/*Grandfather rule: have polices prior to 03/01/06 
that will be longer than 26ft and the exclusion is in place
do not have to be submitted on renewal */


DECLARE @INT_PARTICIPATE_RACE INT    
declare @WATERCRAFT_REMOVE_SAILBOAT char            
DECLARE @REMOVE_SAILBOAT SMALLINT          
--Grandfather date check for sailboat
DECLARE @APP_INCEPTION_DATE datetime                                                
DECLARE @POLICY_PRIOR_DATE datetime
SET @POLICY_PRIOR_DATE = '01/03/2006'
SELECT @APP_INCEPTION_DATE = APP_INCEPTION_DATE
FROM APP_LIST WHERE CUSTOMER_ID=@CUSTOMERID and APP_ID=@APPID and APP_VERSION_ID=@APPVERSIONID   
--
SELECT  @REMOVE_SAILBOAT = ISNULL(REMOVE_SAILBOAT,'') FROM APP_WATERCRAFT_INFO          
WHERE  CUSTOMER_ID=@CUSTOMERID AND APP_ID= @APPID AND APP_VERSION_ID = @APPVERSIONID AND BOAT_ID=@BOATID   
      
--If Watercraft Type Field is Sailboat or Sailboat with Outboard 
--If  Length field is under 26 ft and yes to remove sailboat racing exclusion and no change in rates. 
--Add this condition to refer list; no grandfathered requirements. Confirmed by Bill 09/17/2007

-- IF(@APP_INCEPTION_DATE > @POLICY_PRIOR_DATE)
-- BEGIN
--                                               
	IF EXISTS(SELECT APP_ID  FROM APP_WATERCRAFT_GEN_INFO                                   
	WHERE  CUSTOMER_ID=@CUSTOMERID AND APP_ID= @APPID AND APP_VERSION_ID = @APPVERSIONID)                                                                                      
	BEGIN                                                                                                                         
		SELECT @INT_PARTICIPATE_RACE=ISNULL(PARTICIPATE_RACE,-1)        
		FROM APP_WATERCRAFT_GEN_INFO WHERE  CUSTOMER_ID=@CUSTOMERID AND APP_ID= @APPID AND APP_VERSION_ID = @APPVERSIONID                                                                                                                                           	END           
                                                     
	--IF((@TYPE_OF_WATERCRAFT = 11372 OR @TYPE_OF_WATERCRAFT = 11672)  AND @INT_MAX_LENGTH < 26 AND @REMOVE_SAILBOAT = 10963)  COMMNETED BY PRAVESH AS PER iTRACK 
	IF((@TYPE_OF_WATERCRAFT = 11372 OR @TYPE_OF_WATERCRAFT = 11672) AND @REMOVE_SAILBOAT = 10963)                                                                                            
	BEGIN                                                     
		SET @WATERCRAFT_REMOVE_SAILBOAT='Y'                                                      
	END                                                        
	ELSE                                                               
	BEGIN                                                                                                               
		SET  @WATERCRAFT_REMOVE_SAILBOAT='N'                                                                                                   
	END
--
-- END
-- ELSE
-- BEGIN
-- 	SET  @WATERCRAFT_REMOVE_SAILBOAT='N'
-- END      
--    
        
---Must be insured for 100% of market value 
-- THIS RULE HAS BEEN BEEN REMOVED                                                                                       
declare @INT_COVERAGE int                         
declare @COVERAGE char                                             
if exists(select count(W.APP_ID) from APP_WATERCRAFT_COVERAGE_INFO W                                                                                                                            
INNER JOIN MNT_COVERAGE C ON C.COV_ID=W.COVERAGE_ID                                                                                         
where  W.CUSTOMER_ID=@CUSTOMERID and W.APP_ID= @APPID and W.APP_VERSION_ID = @APPVERSIONID and W.IS_ACTIVE='Y'                 
and (C.COV_CODE = 'EBPPDACV' OR C.COV_CODE = 'EBPPDAV') and C.LOB_ID = 4 )                                                                                                                                              
begin                                                                        
SET @COVERAGE='N'   -- SET @COVERAGE='Y'  this rule has been been removed                                                                      
end            
else               
begin                                           
                              
set @COVERAGE ='N'                                                 
end                                  
  
---------------NO HULL TYPE FOR PERSONAL BOATS-------------------------------------                                                                              
if(@HULL_MATERIAL=0)                                                                    
begin                   
 if(@TYPE_OF_WATERCRAFT <> '11387' and @TYPE_OF_WATERCRAFT <> '11390' and @TYPE_OF_WATERCRAFT <> '11386' and @TYPE_OF_WATERCRAFT <> '11373')                                          
 begin                                     
 set @HULL_MATERIAL=''                        
 end                   
 else                        
 begin          
  set @HULL_MATERIAL='N'                        
 end                        
end                               
------------------------------------------------------------------------------------------                                                            
                                                                             
--- FOR ALL BOATS OTHER THAN OUTBOARDS AND JET SKI, MINI JET BOAT OR WAVE RUNNER                                                                                   
--- TAKE THE POLICY EFFECTIVE YEAR FORM THE APPLICATION/POLICY DETAILS  AND SUBTRACT FROM THE RATING INFO TAB THE YEAR MANUFACTURED  FROM THE YEAR FIELD  IF GREATER THAN 15 REFER                                                                            --- OUTBOARDS W/M 11487 | MINIJET 11373 | WAVERUNNER 11386 | JETSKI 11390 | JETSKI (W/LIFT BAR) 11387                                                          
--- Outboard include the following type of boats::                                                        
--- Outboard w/Motor : 11487-                                                        
--- Row Boat : 11377-                                                        
--- Canoe : 11376 -                       
--- Pontoon Outboard : 11755-                                       
--- Paddle Boat : 11756                                                        
--- Jet drive Outboards : 11757                                                                              
DECLARE @EFFEDATE_APP DATETIME                
DECLARE @EFFEDATE_RATINGINFO DATETIME                                                                                  
                                                                                  
SELECT @EFFEDATE_APP = convert(char(10),APP_EFFECTIVE_DATE,101)                                                                 
FROM APP_LIST WHERE CUSTOMER_ID=@CUSTOMERID and APP_ID=@APPID and APP_VERSION_ID=@APPVERSIONID                                                                                  
                                                                                  
----------NEW CHECK----------                                         
 
-- if(@TYPE_OF_WATERCRAFT <> '11487' and @TYPE_OF_WATERCRAFT <> '11373' and @TYPE_OF_WATERCRAFT <> '11386'                                              
-- and @TYPE_OF_WATERCRAFT <> '11390' and @TYPE_OF_WATERCRAFT <> '11387' and @TYPE_OF_WATERCRAFT <> '11487'                                               
-- and @TYPE_OF_WATERCRAFT <> '11377' and @TYPE_OF_WATERCRAFT <> '11376' and @TYPE_OF_WATERCRAFT <> '11755'                                              
-- and @TYPE_OF_WATERCRAFT <> '11756' and @TYPE_OF_WATERCRAFT <> '11757') 
-- BEGIN                   
-- 	IF EXISTS(SELECT APP_ID  FROM APP_WATERCRAFT_INFO                        
-- 	WHERE (YEAR(@EFFEDATE_APP)-[YEAR]) > 15 AND                                               
-- 	CUSTOMER_ID=@CUSTOMERID AND APP_ID= @APPID AND APP_VERSION_ID = @APPVERSIONID AND BOAT_ID=@BOATID)                                               
-- 	             
-- 	BEGIN  
-- 		SET @IS_EFFECTIVE_DATE='Y'                                                                  
-- 	END           
-- 	ELSE            
-- 	BEGIN                           
-- 		SET @IS_EFFECTIVE_DATE='N'                                                                               
-- 	END                                           
-- END
-- ELSE
-- BEGIN                                                                                           
-- 		SET @IS_EFFECTIVE_DATE='N'                                                                      
-- END 
SET @IS_EFFECTIVE_DATE='N' 
IF NOT 
(@TYPE_OF_WATERCRAFT = '11373' OR @TYPE_OF_WATERCRAFT = '11386'
OR @TYPE_OF_WATERCRAFT = '11390' OR @TYPE_OF_WATERCRAFT = '11387'
--OR @TYPE_OF_WATERCRAFT = '11370' 
--OR @TYPE_OF_WATERCRAFT = '11757' 
--OR @TYPE_OF_WATERCRAFT = '11375'
OR @TYPE_OF_WATERCRAFT = '11755' 
OR @TYPE_OF_WATERCRAFT = '11756'
OR @TYPE_OF_WATERCRAFT = '11672'  
OR @TYPE_OF_WATERCRAFT = '11487') 
BEGIN 
IF EXISTS(SELECT APP_ID  FROM APP_WATERCRAFT_INFO                        
	WHERE (YEAR(@EFFEDATE_APP)-[YEAR]) > 15 AND 
	(MARINE_SURVEY = 10964 or MARINE_SURVEY = 0 )AND (PHOTO_ATTACHED = 10964 or PHOTO_ATTACHED = 0)AND
	CUSTOMER_ID=@CUSTOMERID AND APP_ID= @APPID AND APP_VERSION_ID = @APPVERSIONID AND BOAT_ID=@BOATID)                                               
	                                     
	BEGIN                                                                  
		SET @IS_EFFECTIVE_DATE='Y'                                                                  
	END                                                      
	ELSE                                                                                                                          
	BEGIN                                                                                           
		SET @IS_EFFECTIVE_DATE='N'                                                                                                   
	END 
END 
                            
-----------------------                                                                      
                                                                    
                                                                     
--END CALCULATING THE EFFECTIVE YEAR                                                                         
                                                                                  
--FOR JET SKI, MINI JET BOAT OR WAVE RUNNER                                                                                   
--TAKE THE POLICY EFFECTIVE YEAR FORM THE APPLICATION/POLICY DETAILS                                                                           
--AND SUBTRACT FROM THE RATING INFO TAB THE YEAR MANUFACTURED  FROM THE YEAR FIELD                                                                                  
--IF ANSWER IS OVER 10 THEN REFER :                                                            
--FOLLOWING BOAT TYPES :                                                         
-- JET SKI : 11390                                            
-- JETSKI (W/LIFT BAR) : 11387                                                        
-- MINI JET BOAT : 11373                                                        
-- WAVERUNNER : 11386                
IF(@TYPE_OF_WATERCRAFT = '11390' or @TYPE_OF_WATERCRAFT = '11386'                                           
or @TYPE_OF_WATERCRAFT = '11373' or @TYPE_OF_WATERCRAFT = '11387')                                             
IF EXISTS(SELECT APP_ID  FROM APP_WATERCRAFT_INFO WITH(NOLOCK) WHERE                                                    
(YEAR(@EFFEDATE_APP)-[YEAR]) > 10    AND 
	(MARINE_SURVEY = 10964 or MARINE_SURVEY=0 )AND (PHOTO_ATTACHED = 10964 or MARINE_SURVEY=0 ) AND                   
CUSTOMER_ID=@CUSTOMERID and APP_ID= @APPID and APP_VERSION_ID = @APPVERSIONID and BOAT_ID=@BOATID)                  
                 
BEGIN                                                        
	SET @IS_EFFECTIVE_DATE_OTHER='Y'                                          
END                                                              
ELSE                                          
BEGIN                                                                                           
	SET @IS_EFFECTIVE_DATE_OTHER='N'                                                                                                   
END                         
                            
--END for Jetski,Mini Bot and Waverunner --       
---                     
                                                                          
---*****Start 10 May 2006                                                                          
                                                       
--TYPE OF WATERCRAFT FIELD                         
--IF TYPE IS JET SKI, MINI JET BOAT OR WAVE RUNNER THEN LOOK AT THE OPERATORS MEMBERS FIELD                                                                      
--LOOK FOR THE PRINCIPLE OPERATOR OF THIS WATERCRAFT                                                                           
--TAKE THE EFFECTIVE YEAR OF THE POLICY AND SUBTRACT THE YEAR FROM THE DATE OF BIRTH                                                            
--FIELD IF THE ANSWER IS UNDER 21 THEN REFER THIS RISK -*/                                                          
--FOLLOWING BOAT TYPES :                    
-- Jet Ski : 11390                                                        
-- Jetski (w/Lift Bar) : 11387                                                        
-- Mini Jet Boat : 11373        
-- Waverunner : 11386                                                                      
                            
--------------------------------------------------------                                         
                                         
SELECT @EFFECTIVE_YEAR = year(APP_EFFECTIVE_DATE)                                                                             
FROM APP_LIST WHERE CUSTOMER_ID=@CUSTOMERID and APP_ID=@APPID and APP_VERSION_ID=@APPVERSIONID                                                                           
                                                                 
IF(@TYPE_OF_WATERCRAFT = '11390' OR @TYPE_OF_WATERCRAFT = '11386' OR @TYPE_OF_WATERCRAFT = '11373' OR @TYPE_OF_WATERCRAFT = '11387')                                                                          
BEGIN
	--IF EXISTS(SELECT APP_ID FROM APP_WATERCRAFT_DRIVER_DETAILS WHERE 
	 --CUSTOMER_ID=@CUSTOMERID AND APP_ID=@APPID AND APP_VERSION_ID=APP_VERSION_ID  AND (@EFFECTIVE_YEAR-YEAR(DRIVER_DOB))  < 21 AND  VEHICLE_ID=@BOATID                                                                  
	--AND APP_VEHICLE_PRIN_OCC_ID='11936'        
	-- AND APP_VEHICLE_PRIN_OCC_ID='11398'
      IF EXISTS(SELECT APP.CUSTOMER_ID  FROM APP_WATERCRAFT_DRIVER_DETAILS APP  INNER JOIN APP_OPERATOR_ASSIGNED_BOAT OPR 
      ON APP.CUSTOMER_ID=OPR.CUSTOMER_ID  AND APP.APP_ID=OPR.APP_ID AND APP.APP_VERSION_ID=OPR.APP_VERSION_ID 
AND APP.DRIVER_ID=OPR.DRIVER_ID
      AND (@EFFECTIVE_YEAR-YEAR(APP.DRIVER_DOB))  < 21 
      WHERE APP.CUSTOMER_ID=@CUSTOMERID AND APP.APP_ID=@APPID AND APP.APP_VERSION_ID=@APPVERSIONID AND   OPR.APP_VEHICLE_PRIN_OCC_ID='11936' AND OPR.BOAT_ID=@BOATID)        
	                                                                        
	BEGIN                                                         
		SET @IS_OP_UNDER_21='Y'  --REFER THE CASE                                                                                         
	END                             
	ELSE                           
	BEGIN              
		SET @IS_OP_UNDER_21='N'                       
	END                            
END 
--if the principal operator is over 21 then look for any occasional drivers on this watercraft.
IF(@IS_OP_UNDER_21='N') 
BEGIN                           
	if(@TYPE_OF_WATERCRAFT = '11390' or @TYPE_OF_WATERCRAFT = '11386' or @TYPE_OF_WATERCRAFT = '11373' or @TYPE_OF_WATERCRAFT = '11387')                                 
	BEGIN
		--IF EXISTS(SELECT APP_ID FROM APP_WATERCRAFT_DRIVER_DETAILS WHERE                                                              
		 --CUSTOMER_ID=@CUSTOMERID AND APP_ID=@APPID AND APP_VERSION_ID=APP_VERSION_ID  AND (@EFFECTIVE_YEAR-YEAR(DRIVER_DOB))  < 16 AND  VEHICLE_ID=@BOATID             
		-- AND APP_VEHICLE_PRIN_OCC_ID='11937' AND (ISNULL(WAT_SAFETY_COURSE,10964)=10964 OR WAT_SAFETY_COURSE=0))                                                                   
		--IF(@WAT_SAFETY_COURSE='N') 
		IF EXISTS(SELECT APP.CUSTOMER_ID  FROM APP_WATERCRAFT_DRIVER_DETAILS APP  INNER JOIN APP_OPERATOR_ASSIGNED_BOAT OPR 
	        ON APP.CUSTOMER_ID=OPR.CUSTOMER_ID  AND APP.APP_ID=OPR.APP_ID AND APP.APP_VERSION_ID=OPR.APP_VERSION_ID 
	        AND (@EFFECTIVE_YEAR-YEAR(APP.DRIVER_DOB))  < 16 
	        WHERE APP.CUSTOMER_ID=@CUSTOMERID AND APP.APP_ID=@APPID AND APP.APP_VERSION_ID=@APPVERSIONID AND   OPR.APP_VEHICLE_PRIN_OCC_ID='11937' AND OPR.BOAT_ID=@BOATID 
		AND (ISNULL(APP.WAT_SAFETY_COURSE,10964)=10964 OR APP.WAT_SAFETY_COURSE=0))                                                                   
		BEGIN                 
			SET @IS_OP_UNDER_16='Y'  --REFER THE CASE                                                                                         
		END                                                                          
		ELSE      
		BEGIN                                                                          
			SET @IS_OP_UNDER_16='N'                                                                           
		END                         
	END 
END
                     
---*****End                             
                                                    
-----------Start---Rule 11 May 2006                                            
--IF THE TYPE OF WATERCRAFT FIELD IS JET SKI, WAVE RUNNER  THEN CHECK THE FOLLOWING FIELD                                      
	--WATERCRAFT UNDERWRITING QUESTION - DOES WOLVERINE INSURE THE HOME - IF YES - OK                                                                 
	--IF NO THEN SEE IF  WE ARE INSURING ANOTHER TYPE OF WATERCRAFT OTHER THAN JET SKI, WAVE RUNNER                              
	--IF NO THEN REFER THE APPLICATION                                                                  
	--IF YES - THEN OK                                                                  
Declare @INT_MULTI_POLICY_DISC_APPLIED int                                                                  
Declare @MULTI_POLICY_DISC_APPLIED char                                   
                                                                  
SELECT @INT_MULTI_POLICY_DISC_APPLIED = MULTI_POLICY_DISC_APPLIED                                                                
FROM APP_WATERCRAFT_GEN_INFO                                       
WHERE  CUSTOMER_ID=@CUSTOMERID and APP_ID= @APPID and APP_VERSION_ID = @APPVERSIONID                                                                     
	                                                                  
	IF(@INT_MULTI_POLICY_DISC_APPLIED=1)                                                                                        
		BEGIN    
			SET @MULTI_POLICY_DISC_APPLIED ='Y'                                                                         
		END   
	ELSE IF(@INT_MULTI_POLICY_DISC_APPLIED=0)                                                                              
		BEGIN          
			SET @MULTI_POLICY_DISC_APPLIED='N'         
		END                                                                 
--conversion into CASE                               
------------------
/*If the Type of Watercraft Field is Jet Ski, Wave runner  then check the following field 
 - Watercraft Underwriting Question - Does Wolverine Insure the Home - if yes - ok 
 - If No then see if  we are insuring another type of watercraft other than Jet Ski, Wave runner 
 - If No then refer the application
 - If Yes - then OK
Jet Ski with left bar : 11387
Jet Ski : 11386,
Wave runner : 11390*/ 
                                                               
IF(@TYPE_OF_WATERCRAFT = '11386' OR @TYPE_OF_WATERCRAFT = '11390' or @TYPE_OF_WATERCRAFT='11387')     
BEGIN      
	IF (@MULTI_POLICY_DISC_APPLIED = 'N') 
	BEGIN
		 IF EXISTS(SELECT CUSTOMER_ID FROM APP_WATERCRAFT_INFO WHERE CUSTOMER_ID=@CUSTOMERID                  
		 AND APP_ID=@APPID AND APP_VERSION_ID=@APPVERSIONID AND TYPE_OF_WATERCRAFT <> '11386' AND TYPE_OF_WATERCRAFT <> '11390' AND TYPE_OF_WATERCRAFT <> '11387' AND IS_ACTIVE='Y' )                                    
		 BEGIN                                                               
		    SET @WOLVERINE_INSURE = 'N'                                                                  
		 END                           
		 ELSE                                                                  
		 BEGIN            
		    SET @WOLVERINE_INSURE = 'Y'                                                                  
		 END
	END
END                                                            
-----------END RULE----                                                                  
---------------------------------------GRANDFATHER COVERAGES---------------------------------------------                                                
DECLARE @APP_EFF_DATE datetime                                                
SELECT @APP_EFF_DATE = APP_EFFECTIVE_DATE                                                                                   
FROM APP_LIST WHERE CUSTOMER_ID=@CUSTOMERID and APP_ID=@APPID and APP_VERSION_ID=@APPVERSIONID                                                 
--declare @Cov_XML varchar(400)                                                
                         
SELECT MNTC.COV_DES as COVERAGE_DES                        
from APP_WATERCRAFT_COVERAGE_INFO AVC                                                
INNER JOIN MNT_COVERAGE MNTC on MNTC.COV_ID = AVC.COVERAGE_CODE_ID                                                
WHERE CUSTOMER_ID=@CUSTOMERID and APP_ID= @APPID and APP_VERSION_ID = @APPVERSIONID  AND BOAT_ID=@BOATID                                                                   
AND NOT( @APP_EFF_DATE  BETWEEN MNTC.EFFECTIVE_FROM_DATE                                                
AND ISNULL(MNTC.EFFECTIVE_TO_DATE,'3000-03-16 16:59:06.630'))                 
                        
--Check for WOL and AGENCY USER                        
--W001 -CARRIERSYSTEMID --W001 TO BE CHECKED WITH XML(DYNAMICALLY)                          
DECLARE @WOLVERINE_USER VARCHAR(50)                        
IF(@USER='w001')                         
BEGIN                        
 SET @WOLVERINE_USER='Y'  --WOL USER                        
END                        
ELSE                        
BEGIN                        
 SET @WOLVERINE_USER='N'  --AGENCY USER                        
END       
-----------------------------------------GRANDFATHER LIMIT -------------------------------------------------------------    
 SELECT MNT.COV_DES AS LIMIT_DESCRIPTION,    
 CASE MNT.LIMIT_TYPE    
 WHEN 2 THEN ISNULL(CONVERT(VARCHAR,MNTC.LIMIT_DEDUC_AMOUNT),'')  + ISNULL(MNTC.LIMIT_DEDUC_AMOUNT_TEXT,'')     
 + '/' + ISNULL(CONVERT(VARCHAR,MNTC.LIMIT_DEDUC_AMOUNT1),'')   + ISNULL(MNTC.LIMIT_DEDUC_AMOUNT1_TEXT,'')     
 ELSE    
 ISNULL(CONVERT(VARCHAR,MNTC.LIMIT_DEDUC_AMOUNT),'')+ ISNULL(MNTC.LIMIT_DEDUC_AMOUNT_TEXT,'')     
 + ISNULL(CONVERT(VARCHAR,MNTC.LIMIT_DEDUC_AMOUNT1),'') + ISNULL(MNTC.LIMIT_DEDUC_AMOUNT1_TEXT,'')     
 END AS LIMIT, AVC.COVERAGE_CODE_ID   
 FROM APP_WATERCRAFT_COVERAGE_INFO AVC                                    
 INNER JOIN MNT_COVERAGE_RANGES MNTC     
 ON MNTC.COV_ID = AVC.COVERAGE_CODE_ID AND  AVC.LIMIT_ID= MNTC.LIMIT_DEDUC_ID       
 INNER JOIN MNT_COVERAGE MNT     
 ON AVC.COVERAGE_CODE_ID = MNT.COV_ID     
 WHERE CUSTOMER_ID=@CUSTOMERID and APP_ID=@APPID and APP_VERSION_ID =@APPVERSIONID     
 AND BOAT_ID = @BOATID AND LIMIT_DEDUC_TYPE='LIMIT'    
 AND NOT(    
 @APP_EFF_DATE  BETWEEN ISNULL(MNTC.EFFECTIVE_FROM_DATE,'1950-1-1')    
 AND ISNULL(MNTC.EFFECTIVE_TO_DATE,'3000-12-31')    
)      
----------------------------------------- END GRANFATHER LIMIT ---------------------------------------------------------                     
  
--========================================= GRANDFATHER DEDUCTIBLE ====================================================  
  
SELECT MNT.COV_DES AS DEDUCTIBLE_DESCRIPTION,  
 CASE MNT.DEDUCTIBLE_TYPE  
 WHEN 2 THEN   
  ISNULL(CONVERT(VARCHAR,MNTC.LIMIT_DEDUC_AMOUNT),'')  
+ ISNULL(MNTC.LIMIT_DEDUC_AMOUNT_TEXT,'')   
  + '/'  
  + ISNULL(CONVERT(VARCHAR,MNTC.LIMIT_DEDUC_AMOUNT1),'')   
  + ISNULL(MNTC.LIMIT_DEDUC_AMOUNT1_TEXT,'')   
 ELSE  
  ISNULL(CONVERT(VARCHAR,MNTC.LIMIT_DEDUC_AMOUNT),'')  
   + ISNULL(MNTC.LIMIT_DEDUC_AMOUNT_TEXT,'')   
  + ISNULL(CONVERT(VARCHAR,MNTC.LIMIT_DEDUC_AMOUNT1),'')   
  + ISNULL(MNTC.LIMIT_DEDUC_AMOUNT1_TEXT,'')   
 END AS DEDUCTIBLE,  
 AVC.COVERAGE_CODE_ID    
FROM APP_WATERCRAFT_COVERAGE_INFO AVC                                  
INNER JOIN MNT_COVERAGE_RANGES MNTC   
ON MNTC.COV_ID = AVC.COVERAGE_CODE_ID    
AND  AVC.DEDUC_ID= MNTC.LIMIT_DEDUC_ID              
INNER JOIN MNT_COVERAGE MNT   
ON AVC.COVERAGE_CODE_ID = MNT.COV_ID   
WHERE CUSTOMER_ID=@CUSTOMERID and APP_ID=@APPID and APP_VERSION_ID =@APPVERSIONID     
 AND BOAT_ID = @BOATID AND LIMIT_DEDUC_TYPE='DEDUCT'  
AND NOT(  
 @APP_EFF_DATE  BETWEEN ISNULL(MNTC.EFFECTIVE_FROM_DATE,'1950-1-1')  
 AND ISNULL(MNTC.EFFECTIVE_TO_DATE,'3000-12-31')  
 )  

AND LIMIT_DEDUC_TYPE='NOT REQUIRED NOW'   -- ADDED BY PRAVESH AS THIS RULE NOT REQUIRED NOW

--========================================= END GRANDFATHER DEDUCTIBLE ===============================================  
-----BOAT TYPE--------                                  
DECLARE @BOATTYPE VARCHAR(100)                                  
SELECT @BOATTYPE = UPPER(LOOKUP_VALUE_DESC) FROM MNT_LOOKUP_VALUES M  WHERE M.LOOKUP_UNIQUE_ID =@TYPE_OF_WATERCRAFT                                  
---------------------  
/*=============================================================
 No boat can be associated with more than one principal operator.
 If assocaited with more than one principal, reject it 
 11936 = Principal
==============================================================*/ 

DECLARE @PRINCIPLE_OPERATOR CHAR
DECLARE @INTCOUNT INT 
SET @PRINCIPLE_OPERATOR='N'
DECLARE @IS_ACTIVE CHAR

SELECT @INTCOUNT=COUNT(AOPB.APP_VEHICLE_PRIN_OCC_ID) FROM APP_OPERATOR_ASSIGNED_BOAT AOPB
INNER JOIN APP_WATERCRAFT_DRIVER_DETAILS AWDD
ON AOPB.CUSTOMER_ID=AWDD.CUSTOMER_ID AND AOPB.APP_ID=AWDD.APP_ID AND AOPB.APP_VERSION_ID=AWDD.APP_VERSION_ID
and AWDD.DRIVER_ID = AOPB.DRIVER_ID
WHERE AOPB.CUSTOMER_ID=@CUSTOMERID AND AOPB.APP_ID=@APPID 
AND AOPB.APP_VERSION_ID =@APPVERSIONID AND BOAT_ID =@BOATID AND AOPB.APP_VEHICLE_PRIN_OCC_ID=11936 AND IS_ACTIVE='Y'

-- SELECT @INTCOUNT=COUNT(APP_VEHICLE_PRIN_OCC_ID) FROM APP_OPERATOR_ASSIGNED_BOAT with(nolock)
-- WHERE CUSTOMER_ID=@CUSTOMERID AND APP_ID=@APPID 
-- AND APP_VERSION_ID = @APPVERSIONID  AND BOAT_ID = @BOATID AND APP_VEHICLE_PRIN_OCC_ID=11936

	IF(@INTCOUNT > 1 OR @INTCOUNT= 0)
	BEGIN
		SET @PRINCIPLE_OPERATOR='Y'
	END

--========TRAILER RULE===============                  
--11761 --Ski Jet Trailer                  
/*DECLARE @JETSKI_TYPE_TRAILER CHAR 
IF EXISTS(SELECT CUSTOMER_ID FROM APP_WATERCRAFT_INFO                   
WHERE CUSTOMER_ID=@CUSTOMERID AND  APP_ID=@APPID  AND  APP_VERSION_ID=@APPVERSIONID AND      
TYPE_OF_WATERCRAFT<>11390 and TYPE_OF_WATERCRAFT<>11387 and BOAT_ID=@BOATID)     
BEGIN             
	IF EXISTS(SELECT CUSTOMER_ID FROM  APP_WATERCRAFT_TRAILER_INFO                                                    
	WHERE CUSTOMER_ID=@CUSTOMERID AND  APP_ID=@APPID  AND  APP_VERSION_ID=@APPVERSIONID AND TRAILER_TYPE=11761                  
	AND ASSOCIATED_BOAT = @BOATID and IS_ACTIVE='Y')    
		BEGIN                   
			SET @JETSKI_TYPE_TRAILER ='Y'                  
		END           
	ELSE                  
		BEGIN                  
			SET @JETSKI_TYPE_TRAILER ='N'                  
		END                  
   
END                  
ELSE                  
BEGIN                  
	SET @JETSKI_TYPE_TRAILER ='N'   
END  */ 
/*DECLARE @JETTYPEBOAT CHAR
SET @JETTYPEBOAT='N'
	IF EXISTS(SELECT CUSTOMER_ID,TYPE_OF_WATERCRAFT FROM APP_WATERCRAFT_INFO                   
	WHERE CUSTOMER_ID=@CUSTOMERID AND  APP_ID=@APPID  AND  APP_VERSION_ID=@APPVERSIONID AND 
	(TYPE_OF_WATERCRAFT=11390 OR TYPE_OF_WATERCRAFT=11387) and BOAT_ID=@BOATID) 
	BEGIN 
		SET @JETTYPEBOAT ='Y'
	END

DECLARE @JETSKI_TYPE_TRAILER CHAR 
SET @JETSKI_TYPE_TRAILER='N'
	IF EXISTS(SELECT CUSTOMER_ID,TRAILER_TYPE FROM  APP_WATERCRAFT_TRAILER_INFO                                                    
	WHERE CUSTOMER_ID=@CUSTOMERID AND  APP_ID=@APPID  AND  APP_VERSION_ID=@APPVERSIONID AND TRAILER_TYPE=11761                  
	AND ASSOCIATED_BOAT = @BOATID and IS_ACTIVE='Y' ) 
	BEGIN                   
		SET @JETSKI_TYPE_TRAILER ='Y'  
	              
	END 

DECLARE @OTHER_TYPE_TRAILER CHAR
SET @OTHER_TYPE_TRAILER='N'
IF EXISTS(SELECT CUSTOMER_ID,TRAILER_TYPE FROM  APP_WATERCRAFT_TRAILER_INFO                                                    
	WHERE CUSTOMER_ID=@CUSTOMERID AND  APP_ID=@APPID  AND  APP_VERSION_ID=@APPVERSIONID AND TRAILER_TYPE=11760                  
	AND ASSOCIATED_BOAT = @BOATID and IS_ACTIVE='Y' ) 
	BEGIN                   
		SET @OTHER_TYPE_TRAILER ='Y'  
	              
	END 


DECLARE @JETSKI_TYPE_TRAILERINFO CHAR
IF ((@JETTYPEBOAT='Y' AND @JETSKI_TYPE_TRAILER='N') OR (@JETTYPEBOAT='N' AND @JETSKI_TYPE_TRAILER='Y') OR ( @OTHER_TYPE_TRAILER='Y' AND @JETTYPEBOAT='Y'))
	BEGIN 
		SET @JETSKI_TYPE_TRAILERINFO='Y'
	END
ELSE
	BEGIN 
		SET @JETSKI_TYPE_TRAILERINFO='N'
	END*/
        
--===================================                  
                                                                                        
SELECT                                    
	@BOAT_NO as BOAT_NO,                                                                                                                                 
	@BOAT_YEAR as BOAT_YEAR,                                                                                                                                          
	--@MODEL as MODEL,                                                                                                                                          
	@LENGTH as LENGTH,      
	@TYPE_OF_WATERCRAFT as TYPE_OF_WATERCRAFT,                                                                                                
	@MAKE as MAKE ,   
	@HULL_ID_NO as HULL_ID_NO,     
	@STATE_REG as STATE_REG ,                                                 
	@HULL_MATERIAL as HULL_MATERIAL ,                                                                                                                           
	@INSURING_VALUE as INSURING_VALUE,                                             
	@WATERS_NAVIGATED as WATER_NAVIGATED,                                                                                                                                        
	--    
	@MAX_LENGTH as MAX_LENGTH,                                                                      
	@AGE as AGE,                                                                                           
	@MAX_SPEED as MAX_SPEED,                
	@MAX_INSURING_WMOTOR as MAX_INSURING_WMOTOR  ,                
	--@PARTICIPATE_RACE as PARTICIPATE_RACE,                                                   
	@COVERAGE  as COVERAGE,                                                                                                              
	@TERRITORY as TERRITORY ,                                                             
	@COV_TYPE_BASIS as COV_TYPE_BASIS,                                                           
	--Loaction                                 
	@LOCATION_ADDRESS as LOCATION_ADDRESS,                                     
	@LOCATION_CITY as LOCATION_CITY,                                               
	@LOCATION_STATE as LOCATION_STATE,                                                      
	@LOCATION_ZIP as LOCATION_ZIP,                                                      
	                                                                            
	--EFFECTIVE DATE                                   
	@IS_EFFECTIVE_DATE as IS_EFFECTIVE_DATE,                                                                               
	@IS_EFFECTIVE_DATE_OTHER as IS_EFFECTIVE_DATE_OTHER ,                                                                            
	                                                           
	--Operator check                                                                          
	@IS_OP_UNDER_21 as IS_OP_UNDER_21,                                                                          
	@IS_OP_UNDER_16 as IS_OP_UNDER_16,                                               
	--Type of watercraft                                                                         
	@WOLVERINE_INSURE as WOLVERINE_INSURE,                       
	@BOATTYPE as BOATTYPE ,                               
	@INT_MAX_LENGTH AS INT_MAX_LENGTH,                   
	@INT_INCH AS INT_INCH ,          
	--                                                
	@WOLVERINE_USER AS WOLVERINE_USER ,         
	@DEFAULT_MAX_SPEED AS DEFAULT_MAX_SPEED ,                  
	--@JETSKI_TYPE_TRAILERINFO AS JETSKI_TYPE_TRAILERINFO ,          
	@WATERCRAFT_REMOVE_SAILBOAT AS WATERCRAFT_REMOVE_SAILBOAT ,
	@PRINCIPLE_OPERATOR as PRINCIPLE_OPERATOR                         
END










GO

