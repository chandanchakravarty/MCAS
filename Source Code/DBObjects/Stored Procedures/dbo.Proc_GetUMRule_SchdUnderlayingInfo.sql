IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetUMRule_SchdUnderlayingInfo]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetUMRule_SchdUnderlayingInfo]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/* ======================================================================================================                    
Proc Name                : Dbo.Proc_GetUMRule_SchdUnderlayingInfo                      
Created by               : Ashwani                                                                                                                
Date                     : 17 Oct,2006                                                                
Purpose                  : To get the Schedule of Underlaying Info for UM rules                                                                
Revison History          :                                                                                                                
Used In                  : Wolverine                                                                                                                
======================================================================================================                    
Date     Review By          Comments                                                                                                                
=====  ==============   =============================================================================*/                    
-- drop proc Proc_GetUMRule_SchdUnderlayingInfo                                                            
CREATE proc dbo.Proc_GetUMRule_SchdUnderlayingInfo                         
(                                                                                                                
 @CUSTOMERID    int,                                                                                                                
 @APPID         int,                                                                                                                
 @APPVERSIONID   int                                                                 
)                                                                                                                
AS                                                                                                                    
BEGIN                                                                   
                    
 --Mandatory Data                                                                
DECLARE @POLICY_LOB nvarchar(25)                    
DECLARE @POLICY_COMPANY nvarchar(150)                    
DECLARE @POLICY_NUMBER nvarchar(75)                    
DECLARE @POLICY_START_DATE varchar(20) --datetime                    
DECLARE @POLICY_EXPIRATION_DATE varchar(20) -- datetime                   
DECLARE @IS_POLICY  bit                 
DECLARE @UNDERINSURED_MOTORIST varchar(100)            
DECLARE @UNDERINSURED_MOTORISTCSL varchar(100)  
DECLARE @PERSONAL_LIABILITY VARCHAR(100)    
DECLARE @INJURY_LIABILITY VARCHAR(100) 
DECLARE @INJURY_LIABILITY_ACCIDENT VARCHAR(100)    
DECLARE @DAMAGE_LIABILITY VARCHAR(100)    
DECLARE @SLIMIT_LIABILITY VARCHAR(100)  
DECLARE @UNINSURED_MOTORISTCSL VARCHAR(100)
DECLARE @UNINSURED_MOTORIST VARCHAR(100)
--DECLARE @WATERCRAFT_LIABILITY VARCHAR(100)
DECLARE @LEN INT
DECLARE @IS_RECORD_EXISTS CHAR                                                                     
IF EXISTS (SELECT CUSTOMER_ID FROM APP_UMBRELLA_UNDERLYING_POLICIES                                                    
WHERE CUSTOMER_ID=@CUSTOMERID AND APP_ID= @APPID AND APP_VERSION_ID = @APPVERSIONID)                                                                
	BEGIN 
	   SET @IS_RECORD_EXISTS='N'                    
	   SELECT @POLICY_LOB=ISNULL(POLICY_LOB,''),@POLICY_COMPANY=ISNULL(POLICY_COMPANY,''),                    
	   @POLICY_NUMBER=ISNULL(POLICY_NUMBER,''),                    
	   @POLICY_START_DATE=ISNULL(CONVERT(VARCHAR(20),POLICY_START_DATE),''),                    
	   @POLICY_EXPIRATION_DATE=ISNULL(CONVERT(VARCHAR(20),POLICY_EXPIRATION_DATE),''),                
	   @IS_POLICY=ISNULL(IS_POLICY,'')                    
	   FROM  APP_UMBRELLA_UNDERLYING_POLICIES    
	   WHERE CUSTOMER_ID=@CUSTOMERID AND APP_ID= @APPID AND APP_VERSION_ID = @APPVERSIONID               
	END                    
ELSE
	BEGIN
	SET @POLICY_LOB =''
	SET @POLICY_NUMBER=''
	SET @POLICY_START_DATE=''
	SET @POLICY_EXPIRATION_DATE=''
        SET @IS_RECORD_EXISTS='Y'
	END 
                    
DECLARE @STATE_ID INT
DECLARE @APP_EFFECTIVE_DATE VARCHAR(20)              
SELECT @STATE_ID=STATE_ID,@APP_EFFECTIVE_DATE=APP_EFFECTIVE_DATE
FROM APP_LIST WHERE CUSTOMER_ID=@CUSTOMERID AND APP_ID= @APPID AND APP_VERSION_ID = @APPVERSIONID                  
          
-- Underlying Schedule of Insurance If the State is Indiana Look at all the Underlying policies with the Line                     
-- of Business Motorcycle or Automobile If the limit for Uninsured Motorist or Uninsured Motorist CSL are not the                     
-- same for all vehicles Refer to underwriters                  
                    
-- Homeowners / Rental Dwelling Policies Schedule of Underlying If the Comprehensive Personal Liability                     
-- Limit is under $300,000 then refer to underwriters                     
                    
-- Homeowners / Rental Dwelling Policies Schedule of Underlying If the Comprehensive Personal Liability                     
-- Personal Injury coverage endorsement HO-82 is No then refer to Underwriters                      
                    
-- Underlying Schedule of Insurance If the State is Indiana Look at all the Underlying policies with the Line of                    
-- Business Motorcycle or Automobile If the limit for Underinsured Motorist or Underinsured Motorist CSL are not the                     
-- same for all vehicles Refer to underwriters                                                                                                     
                    
--Automobile/Motor - atleast 1 Vehicle and 1 driver       
DECLARE @AUTO_VEHICLE_DRIVER_RULE varchar(2)               
DECLARE @MOTOR_MOTORINFO_DRIVER_RULE varchar(2)       
DECLARE @HOME_DWELLINGINFO_RULE varchar(2)               
DECLARE @WATERCRAFT_WATERCRAFTINFO_RULE varchar(2)      
DECLARE @UNDERINSURED_UNDERINSUREDCSL_MOTORIST VARCHAR(20) 
SET @UNDERINSURED_UNDERINSUREDCSL_MOTORIST = 'N'                                                                
              
                
CREATE TABLE #APP_UMBRELLA_UNDERLYING_POLICIES                 
(                
 [IDENT_COL] [Int] IDENTITY(1,1) NOT NULL ,                
 [POLICY_NUMBER] [nvarchar] (75) ,                
 [POLICY_LOB] [nvarchar] (25) ,                
 [POLICY_COMPANY] [nvarchar](100),                
 [IS_POLICY] [bit] NULL ,                
 [STATE_ID] [smallint] NULL                 
)                
                
-- Select All Underling Policies/App in temp table                
INSERT INTO #APP_UMBRELLA_UNDERLYING_POLICIES                 
(                
 POLICY_NUMBER,POLICY_LOB,POLICY_COMPANY,                
 IS_POLICY,STATE_ID                
)                
SELECT  POLICY_NUMBER,POLICY_LOB,POLICY_COMPANY,                
 IS_POLICY,STATE_ID                
FROM APP_UMBRELLA_UNDERLYING_POLICIES                                                               
WHERE CUSTOMER_ID=@CUSTOMERID                 
 AND APP_ID= @APPID                 
 AND APP_VERSION_ID = @APPVERSIONID                                                                
                
DECLARE @IDENT_COL INt                 
DECLARE @UNDERLYING_POLICY_LOB Varchar(10),                
 @UNDERLYING_POLICY_COMPANY Varchar(20),                
 @STR Varchar(75),                
 @IndexOfP SmallInt ,              
 @IndexOfHypen SmallInt               
              
DECLARE @UNDERLYING_POLICY_NUMBER nvarchar(75)       
                 
SET @IDENT_COL = 1                
          
WHILE (@IDENT_COL=1)                
BEGIN                
  IF NOT EXISTS (SELECT IDENT_COL FROM #APP_UMBRELLA_UNDERLYING_POLICIES                
  WHERE IDENT_COL = @IDENT_COL)                
  BEGIN                 
  BREAK        
  END                
           
  SELECT  @UNDERLYING_POLICY_LOB = POLICY_LOB,                
  @UNDERLYING_POLICY_COMPANY = POLICY_COMPANY ,                
  @STR = POLICY_NUMBER ,               
  @IS_POLICY = isnull(IS_POLICY,'')                
  FROM #APP_UMBRELLA_UNDERLYING_POLICIES                
  WHERE IDENT_COL = @IDENT_COL                
          
  SET @IndexOfHypen = CHARINDEX ('-', @STR)              
   
  IF (@IndexOfHypen  <> 0)              
  SET @UNDERLYING_POLICY_NUMBER = SUBSTRING (@STR,1,@IndexOfHypen - 2)              
  ELSE              
  SET @UNDERLYING_POLICY_NUMBER = @STR 
 --SET @IndexOfP = CHARINDEX ('P', @UNDERLYING_POLICY_NUMBER)              
 --IF @IndexOfP <> 0               
  SET @UNDERLYING_POLICY_NUMBER = SUBSTRING (@UNDERLYING_POLICY_NUMBER,1,11)
---- Auto Rule Driver/Vehicle              
 /*IF(@POLICY_LOB='2'  and @POLICY_COMPANY='WOLVERINE')                  
 BEGIN                 
  -- Check  In APP  Vehicle Table                 
	  IF (@IS_POLICY = 0)                
	  BEGIN                 
		IF EXISTS (SELECT VEH.CUSTOMER_ID FROM  APP_VEHICLES VEH                
		INNER JOIN APP_LIST APP ON VEH.CUSTOMER_ID = APP.CUSTOMER_ID                 
		AND VEH.APP_ID = APP.APP_ID  AND VEH.APP_VERSION_ID = APP.APP_VERSION_ID                
		WHERE APP.APP_NUMBER = @UNDERLYING_POLICY_NUMBER AND ISNULL(VEH.IS_ACTIVE,'N') ='Y'              
		)                
		BEGIN               
			    IF EXISTS (SELECT DRV.CUSTOMER_ID FROM  APP_DRIVER_DETAILS DRV               
			    INNER JOIN APP_LIST APP ON DRV.CUSTOMER_ID = APP.CUSTOMER_ID                 
			    AND DRV.APP_ID = APP.APP_ID AND DRV.APP_VERSION_ID = APP.APP_VERSION_ID                
			    WHERE APP.APP_NUMBER = @UNDERLYING_POLICY_NUMBER               
			    AND ISNULL(DRV.IS_ACTIVE,'N') ='Y')               
			    BEGIN                 
					SET @AUTO_VEHICLE_DRIVER_RULE = 'N'                
			    END                
			    ELSE                
			    BEGIN                 
			    	SET @AUTO_VEHICLE_DRIVER_RULE = 'Y'                
		     	    BREAK                
		    	    END              
	        END              
	        ELSE                
		BEGIN                 
			SET @AUTO_VEHICLE_DRIVER_RULE = 'Y'                
		BREAK                
		END                 
	END                
  -- IF POLICY CHECK  IN POL  VEHICLE TABLE                 
  -- ADD CODE TO CHECK IN DRIVER INFO TABLE ALSO                
	    ELSE IF(@IS_POLICY = 1)                
	    BEGIN                 
		IF EXISTS (SELECT VEH.CUSTOMER_ID FROM  POL_VEHICLES VEH                
		INNER JOIN POL_CUSTOMER_POLICY_LIST POL  ON VEH.CUSTOMER_ID = POL.CUSTOMER_ID                 
		AND VEH.POLICY_ID = POL.POLICY_ID AND VEH.POLICY_VERSION_ID = POL.POLICY_VERSION_ID                
		WHERE POL.POLICY_NUMBER =   @UNDERLYING_POLICY_NUMBER              
		AND ISNULL(VEH.IS_ACTIVE,'N') ='Y')              
	        BEGIN              
			IF EXISTS (SELECT DRV.CUSTOMER_ID FROM  POL_DRIVER_DETAILS DRV               
			INNER JOIN POL_CUSTOMER_POLICY_LIST POL                 
			ON DRV.CUSTOMER_ID = POL.CUSTOMER_ID                 
			AND DRV.POLICY_ID = POL.POLICY_ID                 
			AND DRV.POLICY_VERSION_ID = POL.POLICY_VERSION_ID                
			WHERE POL.POLICY_NUMBER = @UNDERLYING_POLICY_NUMBER               
			AND ISNULL(DRV.IS_ACTIVE,'N') ='Y')              
			BEGIN                 
				SET @AUTO_VEHICLE_DRIVER_RULE = 'N'                
			END                
	 		ELSE              
			BEGIN               
				SET @AUTO_VEHICLE_DRIVER_RULE = 'Y'  
			BREAK              
			END              
	  	 END              
		 ELSE                
		 BEGIN                 
			 SET @AUTO_VEHICLE_DRIVER_RULE = 'Y'                
		 BREAK              
		 END                 
	    END                
  END      
  ELSE */
IF(@POLICY_LOB='2' and upper(ltrim(rtrim(@POLICY_COMPANY)))!='WOLVERINE')            
  BEGIN                 
  --Check IN APP_UMBRELLA_VEHICLE_INFO                
		  IF EXISTS ( SELECT CUSTOMER_ID FROM  APP_UMBRELLA_VEHICLE_INFO               
		  WHERE ISNULL(IS_ACTIVE,'N') ='Y'              
		  AND CUSTOMER_ID = @CUSTOMERID              
		  AND APP_ID = @APPID              
		  AND APP_VERSION_ID = @APPVERSIONID)              
	  	  BEGIN                 
			IF EXISTS (SELECT CUSTOMER_ID FROM  APP_UMBRELLA_DRIVER_DETAILS              
			WHERE ISNULL(IS_ACTIVE,'N') ='Y'              
			AND CUSTOMER_ID = @CUSTOMERID              
			AND APP_ID = @APPID              
			AND APP_VERSION_ID = @APPVERSIONID )              
			BEGIN               
				SET @AUTO_VEHICLE_DRIVER_RULE = 'N'                
			END              
	   		ELSE              
			BEGIN               
				SET @AUTO_VEHICLE_DRIVER_RULE = 'Y'                
			BREAK                
			END              
		 END                
		 ELSE                
		 BEGIN                 
			 SET @AUTO_VEHICLE_DRIVER_RULE = 'Y'                
		 BREAK                
		 END                 
  END                 
---- MOTOR Rule Driver/Motorinfo              
/* IF(@POLICY_LOB='3'  and @POLICY_COMPANY='WOLVERINE')                  
 BEGIN                 
  -- Check  In APP  Vehicle Table                 
  IF (@IS_POLICY = 0)                
  BEGIN                 
		IF EXISTS (SELECT VEH.CUSTOMER_ID FROM  APP_VEHICLES VEH                
		INNER JOIN APP_LIST APP ON VEH.CUSTOMER_ID = APP.CUSTOMER_ID                 
		AND VEH.APP_ID = APP.APP_ID  AND VEH.APP_VERSION_ID = APP.APP_VERSION_ID                
		WHERE APP.APP_NUMBER = @UNDERLYING_POLICY_NUMBER AND ISNULL(VEH.IS_ACTIVE,'N') ='Y'  )                
		BEGIN               
				IF EXISTS (SELECT DRV.CUSTOMER_ID FROM  APP_DRIVER_DETAILS DRV               
				INNER JOIN APP_LIST APP ON DRV.CUSTOMER_ID = APP.CUSTOMER_ID                 
				AND DRV.APP_ID = APP.APP_ID AND DRV.APP_VERSION_ID = APP.APP_VERSION_ID                
				WHERE APP.APP_NUMBER = @UNDERLYING_POLICY_NUMBER               
				AND ISNULL(DRV.IS_ACTIVE,'N') ='Y')               
				BEGIN                 
					SET @MOTOR_MOTORINFO_DRIVER_RULE = 'N'                
				END                
				ELSE                
				BEGIN                 
					SET @MOTOR_MOTORINFO_DRIVER_RULE = 'Y'                
				BREAK                
				END              
		END              
		ELSE                
		BEGIN                 
			SET @MOTOR_MOTORINFO_DRIVER_RULE = 'Y'                
		BREAK                
		END                 
    END                
  -- If Policy Check  In POL  Vehicle Table                 
  -- Add code to check in Driver Info table also                
    ELSE IF(@IS_POLICY = 1)                
    BEGIN                 
		IF EXISTS (SELECT VEH.CUSTOMER_ID FROM  POL_VEHICLES VEH                
		INNER JOIN POL_CUSTOMER_POLICY_LIST POL  ON VEH.CUSTOMER_ID = POL.CUSTOMER_ID                 
		AND VEH.POLICY_ID = POL.POLICY_ID AND VEH.POLICY_VERSION_ID = POL.POLICY_VERSION_ID                
		WHERE POL.POLICY_NUMBER =   @UNDERLYING_POLICY_NUMBER              
		AND ISNULL(VEH.IS_ACTIVE,'N') ='Y')              
		BEGIN              
				IF EXISTS (SELECT DRV.CUSTOMER_ID FROM  POL_DRIVER_DETAILS DRV               
				INNER JOIN POL_CUSTOMER_POLICY_LIST POL                 
				ON DRV.CUSTOMER_ID = POL.CUSTOMER_ID                 
				AND DRV.POLICY_ID = POL.POLICY_ID                 
				AND DRV.POLICY_VERSION_ID = POL.POLICY_VERSION_ID                
				WHERE POL.POLICY_NUMBER = @UNDERLYING_POLICY_NUMBER               
				AND ISNULL(DRV.IS_ACTIVE,'N') ='Y')      
				BEGIN                 
					SET @MOTOR_MOTORINFO_DRIVER_RULE = 'N'                
				END                
				ELSE              
				BEGIN             
					SET @MOTOR_MOTORINFO_DRIVER_RULE = 'Y'   
				BREAK              
				END        
		END              
		ELSE                
		BEGIN                 
			SET @MOTOR_MOTORINFO_DRIVER_RULE = 'Y'                
		BREAK              
		END                 
    END                
  END                 
  ELSE */ 
IF(@POLICY_LOB='3' and upper(ltrim(rtrim(@POLICY_COMPANY)))!='WOLVERINE')                  
  BEGIN                 
  --Check IN APP_UMBRELLA_VEHICLE_INFO                
		IF EXISTS ( SELECT CUSTOMER_ID FROM  APP_UMBRELLA_VEHICLE_INFO               
		WHERE ISNULL(IS_ACTIVE,'N') ='Y'              
		AND CUSTOMER_ID = @CUSTOMERID              
		AND APP_ID = @APPID              
		AND APP_VERSION_ID = @APPVERSIONID)   
		BEGIN                 
				IF EXISTS (SELECT CUSTOMER_ID FROM  APP_UMBRELLA_DRIVER_DETAILS              
				WHERE ISNULL(IS_ACTIVE,'N') ='Y'              
				AND CUSTOMER_ID = @CUSTOMERID              
				AND APP_ID = @APPID              
				AND APP_VERSION_ID = @APPVERSIONID )              
				BEGIN               
					SET @MOTOR_MOTORINFO_DRIVER_RULE = 'N'                
				END              
				ELSE              
				BEGIN               
					SET @MOTOR_MOTORINFO_DRIVER_RULE = 'Y'                
				BREAK                
				END              
		END                
		ELSE                
		BEGIN                 
			SET @MOTOR_MOTORINFO_DRIVER_RULE = 'Y'                
		BREAK                
		END                 
  END                
                
 ---- For HOMEOWNER/RENTAL LOB---------------------------------------            
        
                 
 /* IF((@POLICY_LOB='1' or @POLICY_LOB='6') and @POLICY_COMPANY='WOLVERINE')                  
  BEGIN                 
        -- Check  In APP  dwelling Table                 
     IF (@IS_POLICY = 0)                
     BEGIN                 
			IF EXISTS ( SELECT APP_DWELLING.CUSTOMER_ID FROM  APP_DWELLINGS_INFO APP_DWELLING                
			INNER JOIN APP_LIST APP                 
			ON APP_DWELLING.CUSTOMER_ID = APP.CUSTOMER_ID           
			AND APP_DWELLING.APP_ID = APP.APP_ID                 
			AND APP_DWELLING.APP_VERSION_ID = APP.APP_VERSION_ID              
			WHERE APP.APP_NUMBER = @UNDERLYING_POLICY_NUMBER AND ISNULL(APP_DWELLING.IS_ACTIVE,'N') ='Y')                
			BEGIN                 
				SET @HOME_DWELLINGINFO_RULE = 'N'                
			END                
			ELSE                
			BEGIN                 
				SET @HOME_DWELLINGINFO_RULE = 'Y'                
			BREAK                
			END                 
     END                
     -- If Policy Check  In POL  Dwelling Table                 
     ELSE IF(@IS_POLICY = 1)                
     BEGIN                 
			IF EXISTS ( SELECT POL_DWELLING.CUSTOMER_ID FROM  POL_DWELLINGS_INFO POL_DWELLING                
			INNER JOIN POL_CUSTOMER_POLICY_LIST POL                 
			ON POL_DWELLING.CUSTOMER_ID = POL.CUSTOMER_ID                 
			AND POL_DWELLING.POLICY_ID  = POL.POLICY_ID                 
			AND POL_DWELLING.POLICY_VERSION_ID = POL.POLICY_VERSION_ID              
			WHERE POL.POLICY_NUMBER =   @UNDERLYING_POLICY_NUMBER              
			AND ISNULL(POL_DWELLING .IS_ACTIVE,'N') ='Y')                
			BEGIN                 
				SET @HOME_DWELLINGINFO_RULE = 'N'                
			END                
			ELSE                
			BEGIN                 
				SET @HOME_DWELLINGINFO_RULE = 'Y'                
			BREAK                
			END                 
     END                
 END*/
DECLARE @PER_INJURY_82 CHAR
SET     @PER_INJURY_82='N' 
/*Itrack : 1794  THIS IS COMMENTED BECAUSE WE HAVE NO NEED TO CHECK ENDORSEMENT # HO-82  IN CASE OF WOLVERINE POLICY */     
/* Itrack : 1794
IF WOLVERINE HAS THE PRIMARY HOMEOWNERS MAKE SURE THAT THE POLICY HAS THE ENDORSEMENT # HO-82 
IF NOT THEN IN THE VERIFY COMMENTS - PLEASE INDICATE THAT UNDERLYING POLICY NEEDS TO BE ENDORSED WITH THE 
FOR PERSONAL INJURY COVERAGE - HO-82. 
IF ON THE LOCATIONS - OTHER CARRIERS TAB 
THE FIELD DO YOU CARRY PERSONAL INJURY COVERAGE - HO-82 SHOULD ONLY APPEAR IF 
OCCUPIED BY IS OWNER . 
IF NO TO THIS FIELD THEN IN THE VERIFY COMMENTS - PLEASE INDICATE THAT UNDERLYING POLICY NEEDS TO BE ENDORSED WITH THE FOR PERSONAL INJURY COVERAGE - HO-82.
*/
/*
DECLARE @PER_INJURY_82 CHAR
DECLARE @LOCATION_TYPE CHAR
DECLARE @POL_LOCATION_TYPE CHAR
--If  Location is primary (At App Level )
IF EXISTS(SELECT APP_LOC.CUSTOMER_ID FROM  APP_LOCATIONS APP_LOC                
	INNER JOIN APP_LIST APP ON APP_LOC.CUSTOMER_ID = APP.CUSTOMER_ID  AND APP_LOC.APP_ID = APP.APP_ID AND
	APP_LOC.APP_VERSION_ID = APP.APP_VERSION_ID WHERE APP.APP_NUMBER = @UNDERLYING_POLICY_NUMBER AND 
	ISNULL(APP_LOC.IS_ACTIVE,'N') ='Y' AND APP_LOC.LOCATION_TYPE = 11812)
	BEGIN 
		SET @LOCATION_TYPE = 'Y'
	END
	ELSE 
	BEGIN 
		SET @LOCATION_TYPE = 'N'
	END
-- AT Policy Level
IF EXISTS(SELECT POL_LOC.CUSTOMER_ID FROM  POL_LOCATIONS POL_LOC                
	INNER JOIN POL_CUSTOMER_POLICY_LIST POL ON POL_LOC.CUSTOMER_ID = POL.CUSTOMER_ID  AND POL_LOC.POLICY_ID = POL.POLICY_ID AND
	POL_LOC.POLICY_VERSION_ID = POL.POLICY_VERSION_ID WHERE POL.POLICY_NUMBER = @UNDERLYING_POLICY_NUMBER AND 
	ISNULL(POL_LOC.IS_ACTIVE,'N') ='Y' AND POL_LOC.LOCATION_TYPE = 11812)
	BEGIN 
		SET @POL_LOCATION_TYPE = 'Y'
	END
	ELSE 
	BEGIN 
		SET @POL_LOCATION_TYPE = 'N'
	END
-- Personal Injury HO-82
--DECLARE @PER_INJURY_82 CHAR (at app level)
DECLARE @PERSONAL_INJURY_82 CHAR
IF EXISTS (SELECT COV_CODE FROM APP_DWELLING_SECTION_COVERAGES APP_COVERAGE
	INNER JOIN MNT_COVERAGE MNT ON MNT.COV_ID = APP_COVERAGE.COVERAGE_CODE_ID
	INNER JOIN APP_LIST APP ON APP_COVERAGE.CUSTOMER_ID = APP.CUSTOMER_ID AND APP_COVERAGE.APP_ID = APP.APP_ID                 
	AND APP_COVERAGE.APP_VERSION_ID = APP.APP_VERSION_ID 
	WHERE APP.APP_NUMBER = @UNDERLYING_POLICY_NUMBER AND ISNULL(APP.IS_ACTIVE,'N') ='Y' AND COV_CODE = 'PERIJ')              	
	BEGIN
		SET @PERSONAL_INJURY_82 = 'Y'
	END 
	ELSE 
	BEGIN
		SET @PERSONAL_INJURY_82 = 'N'
	END 
-- AT Policy Level
DECLARE @POL_PERSONAL_INJURY_82 CHAR
IF EXISTS (SELECT COV_CODE FROM POL_DWELLING_SECTION_COVERAGES POL_COVERAGE
	INNER JOIN MNT_COVERAGE MNT ON MNT.COV_ID = POL_COVERAGE.COVERAGE_CODE_ID
	INNER JOIN POL_CUSTOMER_POLICY_LIST POL ON POL_COVERAGE.CUSTOMER_ID = POL.CUSTOMER_ID AND POL_COVERAGE.POLICY_ID = POL.POLICY_ID                 
	AND POL_COVERAGE.POLICY_VERSION_ID = POL.POLICY_VERSION_ID 
	WHERE POL.POLICY_NUMBER = @UNDERLYING_POLICY_NUMBER AND ISNULL(POL.IS_ACTIVE,'N') ='Y' AND COV_CODE = 'PERIJ')              	
	BEGIN
		SET @POL_PERSONAL_INJURY_82 = 'Y'
	END 
	ELSE 
	BEGIN
		SET @POL_PERSONAL_INJURY_82 = 'N'
	END 
-----
IF(@POLICY_LOB='1' and @POLICY_COMPANY='WOLVERINE')                  
  BEGIN                 
                    
     IF (@IS_POLICY = 0)                
     BEGIN                 
		IF (@LOCATION_TYPE = 'Y' AND ( @PERSONAL_INJURY_82 = 'N' OR @PERSONAL_INJURY_82 IS NULL ))	
		BEGIN
			SET  @PER_INJURY_82 ='Y'
		END 
		ELSE
		BEGIN
			SET  @PER_INJURY_82 ='N'
		END 
     END                
                 
     ELSE IF(@IS_POLICY = 1)                
     BEGIN                 
			            
		IF (@POL_LOCATION_TYPE = 'Y' AND ( @POL_PERSONAL_INJURY_82 = 'N' OR @POL_PERSONAL_INJURY_82 IS NULL ))	
		BEGIN
			SET  @PER_INJURY_82 ='Y'
		END 
		ELSE
		BEGIN
			SET  @PER_INJURY_82 ='N'
		END              
     END                
 END  
---- End Itrack 1794  */      
-----------------------------------------------------------
--ELSE 


IF((@POLICY_LOB='1' or @POLICY_LOB='6') and UPPER(LTRIM(RTRIM(@POLICY_COMPANY)))!='WOLVERINE')       
  BEGIN 
SET @HOME_DWELLINGINFO_RULE = 'Y'                
   --Check IN APP_UMBRELLA_VEHICLE_INFO                
    IF (@IS_POLICY = 0)                
    BEGIN                 
    	IF EXISTS ( SELECT CUSTOMER_ID FROM  APP_UMBRELLA_REAL_ESTATE_LOCATION     
  	WHERE ISNULL(IS_ACTIVE,'N') ='Y'    
    	AND CUSTOMER_ID = @CUSTOMERID              
    	AND APP_ID = @APPID              
    	AND APP_VERSION_ID = @APPVERSIONID)                 
    --INNER JOIN APP_LIST APP                 
    --ON APP_DWELLING.CUSTOMER_ID = APP.CUSTOMER_ID                 
    --AND APP_DWELLING.APP_ID = APP.APP_ID                 
    --AND APP_DWELLING.APP_VERSION_ID = APP.APP_VERSION_ID              
    	             
  	BEGIN                 
  		SET @HOME_DWELLINGINFO_RULE = 'N'                
  	END                
 	ELSE                
  	BEGIN                 
  		SET @HOME_DWELLINGINFO_RULE = 'Y'                
  		BREAK                
  	END
               
    END 
 END      
         
 -- FOR WATERCRAFT LOB RULE               
               
 /* IF(@POLICY_LOB='4' and @POLICY_COMPANY='WOLVERINE')                  
  BEGIN                 
       -- Check In APP  dwelling Table                 
     IF (@IS_POLICY = 0)                
     BEGIN                 
		IF EXISTS ( SELECT APP_WATERCRAFT.CUSTOMER_ID FROM  APP_WATERCRAFT_INFO APP_WATERCRAFT                
		INNER JOIN APP_LIST APP                 
		ON APP_WATERCRAFT.CUSTOMER_ID = APP.CUSTOMER_ID                 
		AND APP_WATERCRAFT.APP_ID = APP.APP_ID                 
		AND APP_WATERCRAFT.APP_VERSION_ID = APP.APP_VERSION_ID              
		WHERE APP.APP_NUMBER = @UNDERLYING_POLICY_NUMBER AND ISNULL(APP_WATERCRAFT .IS_ACTIVE,'N') ='Y')                
		BEGIN  
			    IF EXISTS (SELECT DRV.CUSTOMER_ID FROM  APP_DRIVER_DETAILS DRV               
			    INNER JOIN APP_LIST APP ON DRV.CUSTOMER_ID = APP.CUSTOMER_ID                 
			    AND DRV.APP_ID = APP.APP_ID AND DRV.APP_VERSION_ID = APP.APP_VERSION_ID                
			    WHERE APP.APP_NUMBER = @UNDERLYING_POLICY_NUMBER               
			    AND ISNULL(DRV.IS_ACTIVE,'N') ='Y' AND 
			    (VEHICLE_ID IS NOT NULL OR APP_VEHICLE_PRIN_OCC_ID IS NOT NULL ))               
			    BEGIN                 
					SET @WATERCRAFT_WATERCRAFTINFO_RULE = 'N'                
			    END                
			    ELSE                
			    BEGIN                 
			    		SET @WATERCRAFT_WATERCRAFTINFO_RULE = 'Y'                
		     	    BREAK                
		    	    END              
		END                
		ELSE                
		BEGIN                 
			SET @WATERCRAFT_WATERCRAFTINFO_RULE = 'Y'                
		BREAK                
		END       
     END                
    -- If Policy Check  In POL  Dwelling Table                 
     ELSE IF(@IS_POLICY = 1)                
     BEGIN                 
		IF EXISTS ( SELECT POL_WATERCRAFT.CUSTOMER_ID FROM  POL_WATERCRAFT_INFO  POL_WATERCRAFT               
		INNER JOIN POL_CUSTOMER_POLICY_LIST POL                 
		ON POL_WATERCRAFT.CUSTOMER_ID = POL.CUSTOMER_ID                 
		AND POL_WATERCRAFT.POLICY_ID  = POL.POLICY_ID                 
		AND POL_WATERCRAFT.POLICY_VERSION_ID = POL.POLICY_VERSION_ID              
		WHERE POL.POLICY_NUMBER =   @UNDERLYING_POLICY_NUMBER              
		AND ISNULL(POL_WATERCRAFT.IS_ACTIVE,'N') ='Y') 
		BEGIN                 
			IF EXISTS (SELECT DRV.CUSTOMER_ID FROM  POL_DRIVER_DETAILS DRV               
			INNER JOIN POL_CUSTOMER_POLICY_LIST POL                 
			ON DRV.CUSTOMER_ID = POL.CUSTOMER_ID                 
			AND DRV.POLICY_ID = POL.POLICY_ID                 
			AND DRV.POLICY_VERSION_ID = POL.POLICY_VERSION_ID                
			WHERE POL.POLICY_NUMBER = @UNDERLYING_POLICY_NUMBER               
			AND ISNULL(DRV.IS_ACTIVE,'N') ='Y' AND 
			(VEHICLE_ID IS NOT NULL OR APP_VEHICLE_PRIN_OCC_ID IS NOT NULL ))              
			BEGIN                 
				SET @WATERCRAFT_WATERCRAFTINFO_RULE = 'N'                
			END                
	 		ELSE              
			BEGIN               
				SET @WATERCRAFT_WATERCRAFTINFO_RULE = 'Y'                
			BREAK              
			END                 
		END                
		ELSE                
		BEGIN                 
			SET @WATERCRAFT_WATERCRAFTINFO_RULE = 'Y'                
		BREAK                
		END                 
    END                
  END           
  ELSE */
IF(@POLICY_LOB='4' and UPPER(LTRIM(RTRIM(@POLICY_COMPANY)))!='WOLVERINE')                  
  BEGIN                 
   --Check IN APP_UMBRELLA_VEHICLE_INFO                
	IF (@IS_POLICY = 0)                
	BEGIN                 
		IF EXISTS ( SELECT CUSTOMER_ID FROM  APP_UMBRELLA_WATERCRAFT_INFO                 
		WHERE ISNULL(IS_ACTIVE,'N') ='Y'               
		AND CUSTOMER_ID = @CUSTOMERID              
		AND APP_ID = @APPID              
		AND APP_VERSION_ID = @APPVERSIONID           
		        
		)	
			BEGIN     
				IF EXISTS (SELECT CUSTOMER_ID FROM  APP_UMBRELLA_DRIVER_DETAILS              
				WHERE ISNULL(IS_ACTIVE,'N') ='Y'              
				AND CUSTOMER_ID = @CUSTOMERID              
				AND APP_ID = @APPID              
				AND APP_VERSION_ID = @APPVERSIONID AND
				(OP_VEHICLE_ID IS NOT NULL OR OP_APP_VEHICLE_PRIN_OCC_ID IS NOT NULL )) 
				BEGIN 
					SET @WATERCRAFT_WATERCRAFTINFO_RULE = 'N'
				END
				ELSE 
				BEGIN
					SET @WATERCRAFT_WATERCRAFTINFO_RULE = 'Y'
				BREAK
				END
				              
			END                
		ELSE                
		BEGIN                 
			SET @WATERCRAFT_WATERCRAFTINFO_RULE = 'Y'                
		BREAK                
		END 
		          
	END 
	    
   -- IF(@IS_POLICY = 1)                
   -- BEGIN                 
   --IF EXISTS ( SELECT POL_DWELLING.CUSTOMER_ID FROM  POL_UMBRELLA_DWELLINGS_INFO POL_DWELLING                
   -- INNER JOIN POL_CUSTOMER_POLICY_LIST POL_LIST                 
   -- ON POL_DWELLING.CUSTOMER_ID = POL_LIST.CUSTOMER_ID   
   -- AND POL_DWELLING.POLICY_ID  = POL_LIST.POLICY_ID                  
   --AND POL_DWELLING.POLICY_VERSION_ID = POL_LIST.POLICY_VERSION_ID              
  -- )                
   --BEGIN                 
   -- SET @DWELLING_HOME = 'Y'                
   --END                
   --ELSE                
   --BEGIN                 
   -- SET @DWELLING_HOME = 'N'                
   --BREAK                
   --END                 
    --END                
  END                
 ----------------------------------------                
 SET @IDENT_COL = @IDENT_COL + 1                 
END                
----------   
/* SELECT            
 @UNDERINSURED_MOTORIST = COVERAGE_AMOUNT            
 FROM APP_UMBRELLA_UNDERLYING_POLICIES_COVERAGES WHERE CUSTOMER_ID=@CUSTOMERID and APP_ID= @APPID and APP_VERSION_ID = @APPVERSIONID            
 AND  COV_CODE='UNDSP'  
 declare @UNINS_SLASH INT   
 SET @UNINS_SLASH = CHARINDEX('/',@UNDERINSURED_MOTORIST)  
 IF @UNINS_SLASH > 1  
 SET @UNDERINSURED_MOTORIST = SUBSTRING(@UNDERINSURED_MOTORIST,0,@UNINS_SLASH)  
  
 SELECT            
 @UNDERINSURED_MOTORISTCSL = COVERAGE_AMOUNT            
 FROM APP_UMBRELLA_UNDERLYING_POLICIES_COVERAGES  WHERE CUSTOMER_ID=@CUSTOMERID and APP_ID= @APPID and APP_VERSION_ID = @APPVERSIONID            
 AND COV_CODE='UNCSL'  
   
 IF(@STATE_ID=14)      
 BEGIN   
---IF THE LIMIT FOR Underinsured Motorist or Underinsured Motorist CSL are not the same for all vehicles Refer to Underwriters            
	  IF((@UNDERINSURED_MOTORIST!=@UNDERINSURED_MOTORISTCSL) AND(@POLICY_LOB=2 OR @POLICY_LOB=3) )            
		BEGIN            
			SET @UNDERINSURED_UNDERINSUREDCSL_MOTORIST='Y'            
		END            
	  ELSE            
		BEGIN            
			SET @UNDERINSURED_UNDERINSUREDCSL_MOTORIST='N'            
		END     
 END   */ 
 
--If the Comprehensive Personal Liability Limit is under $300,000 then refer to underwriters.    
 DECLARE @UNDERLYING_PERSONAL_LIABILITY VARCHAR   
 SELECT            
 @PERSONAL_LIABILITY  = COVERAGE_AMOUNT            
 FROM APP_UMBRELLA_UNDERLYING_POLICIES_COVERAGES  WHERE CUSTOMER_ID=@CUSTOMERID and APP_ID= @APPID and APP_VERSION_ID = @APPVERSIONID            
 AND COV_CODE='PL'
 /*SELECT            
 @WATERCRAFT_LIABILITY  = COVERAGE_AMOUNT            
 FROM APP_UMBRELLA_UNDERLYING_POLICIES_COVERAGES  WHERE CUSTOMER_ID=@CUSTOMERID and APP_ID= @APPID and APP_VERSION_ID = @APPVERSIONID            
 AND COV_CODE='LCCSL'*/
 IF(@STATE_ID=14 OR @STATE_ID=22)
	BEGIN
		IF(convert(int,convert(money,@PERSONAL_LIABILITY)) < convert(int,'300000'))
			BEGIN   
				SET @UNDERLYING_PERSONAL_LIABILITY='Y'  
			END  
		ELSE  
			BEGIN   
				SET @UNDERLYING_PERSONAL_LIABILITY='N'  
			END 
	END  
-- END
--If Insured for Bodily Injury Split Limit and the limit is less than $250,000/$500,000 then Refer to Underwriters. 
DECLARE @COV_CODE VARCHAR(10)
CREATE TABLE #TEMP_POLICY
( 
 POLICY_NUMBER NVARCHAR(75),
 POLICY_COMPANY nvarchar(150),
 POLICY_LOB nvarchar(25)
)  
  
INSERT INTO #TEMP_POLICY  
SELECT POLICY_NUMBER , POLICY_COMPANY, POLICY_LOB
FROM APP_UMBRELLA_UNDERLYING_POLICIES   
WHERE CUSTOMER_ID=@CUSTOMERID and APP_ID= @APPID and APP_VERSION_ID = @APPVERSIONID           

DECLARE  CURPOLICY CURSOR  FOR     
SELECT POLICY_NUMBER FROM #TEMP_POLICY  

OPEN CURPOLICY

FETCH NEXT FROM CURPOLICY INTO @POLICY_NUMBER


WHILE @@FETCH_STATUS = 0     
BEGIN 

--DECLARE @COUNT INT  
--SELECT  @COUNT = COUNT(POLICY_NUMBER) FROM #TEMP_POLICY 
--SELECT  @POLICY_NUMBER = POLICY_NUMBER FROM #TEMP_POLICY 
--PRINT  @COUNT
--WHILE @COUNT <> 0  
--BEGIN   

 BEGIN     
		 DECLARE @BODILY_INJURY_LIABILITY VARCHAR   
		 SELECT            
		 @INJURY_LIABILITY  = COVERAGE_AMOUNT,
		 @LEN = LEN(COVERAGE_AMOUNT)          
		 FROM APP_UMBRELLA_UNDERLYING_POLICIES_COVERAGES  WHERE CUSTOMER_ID=@CUSTOMERID and APP_ID= @APPID and APP_VERSION_ID = @APPVERSIONID            
		 AND COV_CODE='BISPL'
	
		 DECLARE @PROPERTY_DAMAGE_LIABILITY VARCHAR  
		 SELECT            
		 @DAMAGE_LIABILITY  = COVERAGE_AMOUNT            
		 FROM APP_UMBRELLA_UNDERLYING_POLICIES_COVERAGES  WHERE CUSTOMER_ID=@CUSTOMERID and APP_ID= @APPID and APP_VERSION_ID = @APPVERSIONID            
		 AND COV_CODE='PD'  
		 DECLARE @SINGLE_LIMITS_LIABILITY VARCHAR  
	
		 SELECT            
		 @SLIMIT_LIABILITY  = COVERAGE_AMOUNT            
		 FROM APP_UMBRELLA_UNDERLYING_POLICIES_COVERAGES  WHERE CUSTOMER_ID=@CUSTOMERID and APP_ID= @APPID and APP_VERSION_ID = @APPVERSIONID            
		 AND COV_CODE='SLL'  
	
		 DECLARE @UNINS_SLASH INT   
		 SET @UNINS_SLASH = CHARINDEX('/',@INJURY_LIABILITY)  
		 IF @UNINS_SLASH > 1
			BEGIN
			SET @INJURY_LIABILITY_ACCIDENT = SUBSTRING(@INJURY_LIABILITY,@UNINS_SLASH + 1, @LEN )
			SET @INJURY_LIABILITY = SUBSTRING(@INJURY_LIABILITY,0,@UNINS_SLASH)  
			END
	
		--IF(@POLICY_LOB=2 )-- Automobile   
		-- BEGIN  
		IF (@POLICY_COMPANY ='Wolverine')
		BEGIN 
			SET @INJURY_LIABILITY = @INJURY_LIABILITY * 1000
		END 
			 IF(convert(int,convert(money,@INJURY_LIABILITY)) < convert(int,'250000') OR convert(int,convert(money,@INJURY_LIABILITY_ACCIDENT)) < convert(int,'500000') )  
			  BEGIN   
			 	  SET @BODILY_INJURY_LIABILITY='Y'  
			  END  
 --If Bodily Injury Split Limit is $250,000/$500,000 or higher then look at the Property Damage Limit   
 --If Less than $100,000 Refer to Underwriters   
			  ELSE IF(convert(int,convert(money,@DAMAGE_LIABILITY))< convert(int,'100000') )  
			  BEGIN   
				   SET @PROPERTY_DAMAGE_LIABILITY='Y'  
			  END 
 --If Insured for Combined Single Limit and the limit is less than $300,000 then Refer to Underwriters  
			IF(convert(int,convert(money,@SLIMIT_LIABILITY)) < convert(int,'300000'))  
				BEGIN   
					SET @SINGLE_LIMITS_LIABILITY='Y'  
				END   
			ELSE  
				BEGIN   
					SET @SINGLE_LIMITS_LIABILITY='N'  
				END  
 
--If Automobile 
--Look for all driver with a License type - other than Excluded 
--Take the effective date of the policy minus the Date of Birth Field on the Driver/Operator Details 
--is any driver under 25 years age 
--Look to make sure that this line of Business has Coverage Description of Combined Single Limit with $500,000 Coverage 
--If not then refer to underwriters  
DECLARE @DRIVER_DRIV_TYPE NVARCHAR(5)
DECLARE @DRIVER_DOB VARCHAR(20) 
DECLARE @DRIVERTYPE_DOB_SLIABILITY VARCHAR
DECLARE @INT_DIFFERENCE INT
SELECT @DRIVER_DRIV_TYPE=DRIVER_DRIV_TYPE,@DRIVER_DOB=DRIVER_DOB
FROM APP_UMBRELLA_DRIVER_DETAILS WHERE CUSTOMER_ID=@CUSTOMERID and APP_ID= @APPID and APP_VERSION_ID = @APPVERSIONID
 SET @INT_DIFFERENCE = datediff(yy,@DRIVER_DOB,@APP_EFFECTIVE_DATE)

IF(@DRIVER_DRIV_TYPE!=3477)
BEGIN
	IF(@INT_DIFFERENCE < 25)
	BEGIN	
		IF(convert(int,convert(money,@SLIMIT_LIABILITY)) < convert(int,'500000'))
		BEGIN 
			SET @DRIVERTYPE_DOB_SLIABILITY='Y'
		END
		ELSE	
		BEGIN 
			SET @DRIVERTYPE_DOB_SLIABILITY='N'
		END
	END
END


--END  
--Only Indiana State ,Only Automobile and Motorcycle  LOB
--If the limit for Uninsured Motorist or Uninsured Motorist CSL are not
--the same for all vehicles Refer to underwriters 
DECLARE @UNINSURED_UNINSUREDCSL_MOTORIST VARCHAR
SET @UNINSURED_UNINSUREDCSL_MOTORIST ='N'
/*
IF(@STATE_ID=14) --(@POLICY_LOB=2 OR @POLICY_LOB=3)
BEGIN
	SELECT            
	@UNINSURED_MOTORISTCSL  = COVERAGE_AMOUNT            
	FROM APP_UMBRELLA_UNDERLYING_POLICIES_COVERAGES  WHERE CUSTOMER_ID=@CUSTOMERID and APP_ID= @APPID and APP_VERSION_ID = @APPVERSIONID            
	AND COV_CODE='PUNCS' 
	SELECT            
	@UNINSURED_MOTORIST  = COVERAGE_AMOUNT            
	FROM APP_UMBRELLA_UNDERLYING_POLICIES_COVERAGES  WHERE CUSTOMER_ID=@CUSTOMERID and APP_ID= @APPID and APP_VERSION_ID = @APPVERSIONID            
	AND COV_CODE='PUMSP'
	SET @UNINS_SLASH = CHARINDEX('/',@UNINSURED_MOTORIST)  
	IF @UNINS_SLASH > 1  
	SET @UNINSURED_MOTORIST = SUBSTRING(@UNINSURED_MOTORIST,0,@UNINS_SLASH)

	IF(@UNINSURED_MOTORIST!=@UNINSURED_MOTORISTCSL)            
	BEGIN            
		SET @UNINSURED_UNINSUREDCSL_MOTORIST='Y'            
	END            
	ELSE            
	BEGIN            
		SET @UNINSURED_UNINSUREDCSL_MOTORIST='N'            
	END    

	  
END */

--SET @COUNT = @COUNT - 1
--print @COUNT  
END

FETCH NEXT FROM CURPOLICY INTO @POLICY_NUMBER

 
END 
 CLOSE CURPOLICY    
 DEALLOCATE CURPOLICY    

DROP TABLE #TEMP_POLICY  
-------       
SELECT                        
 @POLICY_LOB as POLICY_LOB,                    
 @POLICY_COMPANY as POLICY_COMPANY,          
 @POLICY_NUMBER as POLICY_NUMBER,                    
 @POLICY_START_DATE as POLICY_START_DATE,                    
 @POLICY_EXPIRATION_DATE as POLICY_EXPIRATION_DATE,
 @IS_RECORD_EXISTS AS IS_RECORD_EXISTS ,                  
 @AUTO_VEHICLE_DRIVER_RULE as AUTO_VEHICLE_DRIVER_RULE ,              
 @MOTOR_MOTORINFO_DRIVER_RULE as MOTOR_MOTORINFO_DRIVER_RULE,                
 @HOME_DWELLINGINFO_RULE as HOME_DWELLINGINFO_RULE ,              
 @WATERCRAFT_WATERCRAFTINFO_RULE as WATERCRAFT_WATERCRAFTINFO_RULE ,            
 @UNDERINSURED_UNDERINSUREDCSL_MOTORIST AS UNDERINSURED_UNDERINSUREDCSL_MOTORIST ,  
 @UNDERLYING_PERSONAL_LIABILITY AS UNDERLYING_PERSONAL_LIABILITY   ,  
 @BODILY_INJURY_LIABILITY AS BODILY_INJURY_LIABILITY ,  
 @PROPERTY_DAMAGE_LIABILITY AS PROPERTY_DAMAGE_LIABILITY ,  
 @SINGLE_LIMITS_LIABILITY AS SINGLE_LIMITS_LIABILITY ,
 @UNINSURED_UNINSUREDCSL_MOTORIST AS UNINSURED_UNINSUREDCSL_MOTORIST ,
 @DRIVERTYPE_DOB_SLIABILITY AS DRIVERTYPE_DOB_SLIABILITY ,
 @PER_INJURY_82 AS PER_INJURY_82 
END



GO

