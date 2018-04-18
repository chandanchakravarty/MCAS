IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetUMRule_SchdUnderlayingInfo_Pol]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetUMRule_SchdUnderlayingInfo_Pol]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/* ======================================================================================================                      
Proc Name                : Dbo.Proc_GetUMRule_SchdUnderlayingInfo_Pol                        
Created by               : Manoj Rathore                                                                                                                  
Date                     : 10 May,2007                                                                  
Purpose                  : To get the Schedule of Underlaying Info for UM rules                                                                  
Revison History          :                                                                                                                  
Used In                  : Wolverine                                                                                        

Reviewed By	:	Anurag verma
Reviewed On	:	25-06-2007                          
======================================================================================================                      
Date     Review By          Comments                                                                                                                  
=====  ==============   =============================================================================*/                      
-- drop proc Proc_GetUMRule_SchdUnderlayingInfo_Pol                                                              
CREATE proc dbo.Proc_GetUMRule_SchdUnderlayingInfo_Pol                                 
(                                                                                                                  
 @CUSTOMERID    int,                                                                                                                  
 @POLICYID         int,                                                                                                                  
 @POLICYVERSIONID   int                                                                   
)                                                                                                                  
as                                                                                                                      
begin                                                                     
                      
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
declare @INJURY_LIABILITY VARCHAR(100)   
DECLARE @INJURY_LIABILITY_ACCIDENT VARCHAR(100)      
DECLARE @DAMAGE_LIABILITY VARCHAR(100)      
DECLARE @SLIMIT_LIABILITY VARCHAR(100)    
DECLARE @UNINSURED_MOTORISTCSL VARCHAR(100)  
DECLARE @UNINSURED_MOTORIST VARCHAR(100)  
DECLARE @IS_RECORD_EXISTS CHAR  


                                                                     
IF EXISTS (SELECT CUSTOMER_ID FROM POL_UMBRELLA_UNDERLYING_POLICIES                                                      
WHERE CUSTOMER_ID=@CUSTOMERID AND POLICY_ID= @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID)                                                                  
 BEGIN   
    SET @IS_RECORD_EXISTS='N'                      
    SELECT @POLICY_LOB=ISNULL(POLICY_LOB,''),@POLICY_COMPANY=ISNULL(POLICY_COMPANY,''),                      
    @POLICY_NUMBER=ISNULL(POLICY_NUMBER,''),                      
    @POLICY_START_DATE=ISNULL(CONVERT(VARCHAR(20),POLICY_START_DATE),''),                      
    @POLICY_EXPIRATION_DATE=ISNULL(CONVERT(VARCHAR(20),POLICY_EXPIRATION_DATE),''),                  
  @IS_POLICY=ISNULL(IS_POLICY,'')                      
    FROM  POL_UMBRELLA_UNDERLYING_POLICIES                                          
    WHERE CUSTOMER_ID=@CUSTOMERID AND POLICY_ID= @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID                                                                  
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
SELECT @STATE_ID=STATE_ID FROM POL_CUSTOMER_POLICY_LIST                       
WHERE CUSTOMER_ID=@CUSTOMERID AND POLICY_ID= @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID                                                                  
   
----------    
DECLARE @UNDERINSURED_UNDERINSUREDCSL_MOTORIST VARCHAR    
SET @UNDERINSURED_UNDERINSUREDCSL_MOTORIST = 'N'  
/* Policy Level Rules*/

--Automobile/Motor - atleast 1 Vehicle and 1 driver           
DECLARE @AUTO_VEHICLE_DRIVER_RULE varchar(2) 
DECLARE @MOTOR_MOTORINFO_DRIVER_RULE varchar(2)           
DECLARE @HOME_DWELLINGINFO_RULE varchar(2)                   
DECLARE @WATERCRAFT_WATERCRAFTINFO_RULE varchar(2)


IF(@POLICY_LOB='2' and upper(ltrim(rtrim(@POLICY_COMPANY)))!='WOLVERINE')                
  BEGIN                     
    --Check IN APP_UMBRELLA_VEHICLE_INFO                    
    IF EXISTS ( SELECT CUSTOMER_ID FROM  POL_UMBRELLA_VEHICLE_INFO
                  
    WHERE ISNULL(IS_ACTIVE,'N') ='Y'                  
    AND CUSTOMER_ID = @CUSTOMERID                  
    AND POLICY_ID = @POLICYID                  
    AND POLICY_VERSION_ID = @POLICYVERSIONID)                  
    BEGIN                     
   		IF EXISTS (SELECT CUSTOMER_ID FROM  POL_UMBRELLA_DRIVER_DETAILS                  
   		WHERE ISNULL(IS_ACTIVE,'N') ='Y'                  
   		AND CUSTOMER_ID = @CUSTOMERID                  
    		AND POLICY_ID = @POLICYID                  
    		AND POLICY_VERSION_ID = @POLICYVERSIONID)                  
   		BEGIN                   
    			SET @AUTO_VEHICLE_DRIVER_RULE = 'N'                    
   		END                  
      		ELSE                  
   		BEGIN                   
    			SET @AUTO_VEHICLE_DRIVER_RULE = 'Y'                    
   			--BREAK                    
   		END                  
   END                    
   ELSE                    
   BEGIN                     
    	SET @AUTO_VEHICLE_DRIVER_RULE = 'Y'                    
   	--BREAK                    
   END                     
END     

IF(@POLICY_LOB='3' and upper(ltrim(rtrim(@POLICY_COMPANY)))!='WOLVERINE')                      
  BEGIN                     
  --Check IN APP_UMBRELLA_VEHICLE_INFO                    
  IF EXISTS ( SELECT CUSTOMER_ID FROM  POL_UMBRELLA_VEHICLE_INFO                   
  WHERE ISNULL(IS_ACTIVE,'N') ='Y'                  
  AND CUSTOMER_ID = @CUSTOMERID                  
    AND POLICY_ID = @POLICYID                  
    AND POLICY_VERSION_ID = @POLICYVERSIONID)       
  BEGIN                     
    IF EXISTS (SELECT CUSTOMER_ID FROM  POL_UMBRELLA_DRIVER_DETAILS                  
    WHERE ISNULL(IS_ACTIVE,'N') ='Y'                  
    AND CUSTOMER_ID = @CUSTOMERID                  
    AND POLICY_ID = @POLICYID                  
    AND POLICY_VERSION_ID = @POLICYVERSIONID )                  
    BEGIN                   
     SET @MOTOR_MOTORINFO_DRIVER_RULE = 'N'                    
    END                  
    ELSE                  
    BEGIN                   
     SET @MOTOR_MOTORINFO_DRIVER_RULE = 'Y'                    
    --BREAK                    
    END                  
  END                    
  ELSE                    
  BEGIN                     
   SET @MOTOR_MOTORINFO_DRIVER_RULE = 'Y'                    
  --BREAK                    
  END                     
  END                    

--DECLARE @STATE_ID INT    
DECLARE @POL_EFFECTIVE_DATE VARCHAR(20)                  
SELECT @STATE_ID=STATE_ID,@POL_EFFECTIVE_DATE=POLICY_EFFECTIVE_DATE    
FROM POL_CUSTOMER_POLICY_LIST WHERE CUSTOMER_ID = @CUSTOMERID                  
    AND POLICY_ID = @POLICYID                  
    AND POLICY_VERSION_ID = @POLICYVERSIONID         
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
FROM POL_UMBRELLA_DRIVER_DETAILS WHERE CUSTOMER_ID = @CUSTOMERID                  
    AND POLICY_ID = @POLICYID                  
    AND POLICY_VERSION_ID = @POLICYVERSIONID    
 SET @INT_DIFFERENCE = datediff(yy,@DRIVER_DOB,@POL_EFFECTIVE_DATE)    
    
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


IF((@POLICY_LOB='1' or @POLICY_LOB='6') and UPPER(LTRIM(RTRIM(@POLICY_COMPANY)))!='WOLVERINE')           
  BEGIN     
SET @HOME_DWELLINGINFO_RULE = 'Y'                    
   --Check IN APP_UMBRELLA_VEHICLE_INFO                    
    IF (@IS_POLICY = 0)                    
    BEGIN                     
     IF EXISTS ( SELECT CUSTOMER_ID FROM  POL_UMBRELLA_REAL_ESTATE_LOCATION         
   WHERE ISNULL(IS_ACTIVE,'N') ='Y'        
     AND CUSTOMER_ID = @CUSTOMERID                  
    AND POLICY_ID = @POLICYID                  
    AND POLICY_VERSION_ID = @POLICYVERSIONID )                     
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
    --BREAK                    
   END    
                   
    END     
 END 

IF(@POLICY_LOB='4' and UPPER(LTRIM(RTRIM(@POLICY_COMPANY)))!='WOLVERINE')                      
BEGIN                     
--Check IN APP_UMBRELLA_VEHICLE_INFO                    
	IF (@IS_POLICY = 0)                    
	BEGIN                     
		IF EXISTS ( SELECT CUSTOMER_ID FROM  POL_UMBRELLA_WATERCRAFT_INFO                     
		WHERE ISNULL(IS_ACTIVE,'N') ='Y'                   
		AND CUSTOMER_ID = @CUSTOMERID                  
		AND POLICY_ID = @POLICYID                  
		AND POLICY_VERSION_ID = @POLICYVERSIONID)     
		BEGIN         
			IF EXISTS (SELECT CUSTOMER_ID FROM  POL_UMBRELLA_DRIVER_DETAILS                  
			WHERE ISNULL(IS_ACTIVE,'N') ='Y'                  
			AND CUSTOMER_ID = @CUSTOMERID                  
			AND POLICY_ID = @POLICYID                  
			AND POLICY_VERSION_ID = @POLICYVERSIONID  AND    
			(OP_VEHICLE_ID IS NOT NULL OR OP_APP_VEHICLE_PRIN_OCC_ID IS NOT NULL ))     
			BEGIN     
				SET @WATERCRAFT_WATERCRAFTINFO_RULE = 'N'    
			END    
			ELSE     
			BEGIN    
				SET @WATERCRAFT_WATERCRAFTINFO_RULE = 'Y'    
			END 
		END                    
		ELSE                    
		BEGIN                     
			SET @WATERCRAFT_WATERCRAFTINFO_RULE = 'Y'                    
		END     
        
	END   
END           
   
/*End Rule*/
   
 /*SELECT              
 @UNDERINSURED_MOTORIST = COVERAGE_AMOUNT              
 FROM POL_UMBRELLA_UNDERLYING_POLICIES_COVERAGES WHERE CUSTOMER_ID=@CUSTOMERID and POLICY_ID= @POLICYID and POLICY_VERSION_ID = @POLICYVERSIONID              
 AND  COV_CODE='UNDSP'    
 declare @UNINS_SLASH INT     
 SET @UNINS_SLASH = CHARINDEX('/',@UNDERINSURED_MOTORIST)    
 IF @UNINS_SLASH > 1    
 SET @UNDERINSURED_MOTORIST = SUBSTRING(@UNDERINSURED_MOTORIST,0,@UNINS_SLASH)    
    
 SELECT              
 @UNDERINSURED_MOTORISTCSL = COVERAGE_AMOUNT              
 FROM POL_UMBRELLA_UNDERLYING_POLICIES_COVERAGES  WHERE CUSTOMER_ID=@CUSTOMERID and POLICY_ID= @POLICYID and POLICY_VERSION_ID = @POLICYVERSIONID              
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
 END */  
  
      
--If the Comprehensive Personal Liability Limit is under $300,000 then refer to underwriters.      
 DECLARE @UNDERLYING_PERSONAL_LIABILITY VARCHAR     
 SELECT              
 @PERSONAL_LIABILITY  = COVERAGE_AMOUNT              
 FROM POL_UMBRELLA_UNDERLYING_POLICIES_COVERAGES  WHERE CUSTOMER_ID=@CUSTOMERID and POLICY_ID= @POLICYID and POLICY_VERSION_ID = @POLICYVERSIONID              
 AND COV_CODE='PL'    
 IF(convert(int,convert(money,@PERSONAL_LIABILITY)) < convert(int,'300000'))   
--IF((@STATE_ID=14 OR @STATE_ID=22)AND @PERSONAL_LIABILITY <'300000') -- AND( @POLICY_LOB=1 OR @POLICY_LOB=6))    
  BEGIN     
   SET @UNDERLYING_PERSONAL_LIABILITY='Y'    
  END    
  ELSE    
  BEGIN     
   SET @UNDERLYING_PERSONAL_LIABILITY='N'    
  END     
  
--If Insured for Bodily Injury Split Limit and the limit is less than $250,000/$500,000 then Refer to Underwriters.    
 DECLARE @BODILY_INJURY_LIABILITY VARCHAR    
 DECLARE @LEN INT    
 SELECT              
 @INJURY_LIABILITY  = COVERAGE_AMOUNT  , @LEN =LEN(COVERAGE_AMOUNT)            
 FROM POL_UMBRELLA_UNDERLYING_POLICIES_COVERAGES  WHERE CUSTOMER_ID=@CUSTOMERID and POLICY_ID= @POLICYID and POLICY_VERSION_ID = @POLICYVERSIONID              
 AND COV_CODE='BISPL'    
  
 DECLARE @PROPERTY_DAMAGE_LIABILITY VARCHAR    
 SELECT              
 @DAMAGE_LIABILITY  = COVERAGE_AMOUNT              
 FROM POL_UMBRELLA_UNDERLYING_POLICIES_COVERAGES  WHERE CUSTOMER_ID=@CUSTOMERID and POLICY_ID= @POLICYID and POLICY_VERSION_ID = @POLICYVERSIONID              
 AND COV_CODE='PD'    
  
 DECLARE @SINGLE_LIMITS_LIABILITY VARCHAR     
 SELECT              
 @SLIMIT_LIABILITY  = COVERAGE_AMOUNT              
 FROM POL_UMBRELLA_UNDERLYING_POLICIES_COVERAGES  WHERE CUSTOMER_ID=@CUSTOMERID and POLICY_ID= @POLICYID and POLICY_VERSION_ID = @POLICYVERSIONID              
 AND COV_CODE='SLL'   
   
 DECLARE @UNINS_SLASH INT     
 SET @UNINS_SLASH = CHARINDEX('/',@INJURY_LIABILITY)    
 IF @UNINS_SLASH > 1    
 BEGIN  
  SET @INJURY_LIABILITY_ACCIDENT = SUBSTRING(@INJURY_LIABILITY,@UNINS_SLASH + 1, @LEN )  
  SET @INJURY_LIABILITY = SUBSTRING(@INJURY_LIABILITY,0,@UNINS_SLASH)   
 END   
--IF(@POLICY_LOB=2 )-- Automobile     
--BEGIN    
    
 IF(convert(int,convert(money,@INJURY_LIABILITY)) < convert(int,'250000') OR convert(int,convert(money,@INJURY_LIABILITY_ACCIDENT)) < convert(int,'500000'))    
 BEGIN     
  SET @BODILY_INJURY_LIABILITY='Y'    
 END    
 --If Bodily Injury Split Limit is $250,000/$500,000 or higher then look at the Property Damage Limit     
 --If Less than $100,000 Refer to Underwriters     
 ELSE IF(convert(int,convert(money,@DAMAGE_LIABILITY))< convert(int,'100000') )    
 BEGIN     
  SET @PROPERTY_DAMAGE_LIABILITY='Y'    
 END     
  --IF INSURED FOR COMBINED SINGLE LIMIT AND THE LIMIT IS LESS THAN $300,000 THEN REFER TO UNDERWRITERS    
  IF(convert(int,convert(money,@SLIMIT_LIABILITY)) < convert(int,'300000'))    
    BEGIN     
     SET @SINGLE_LIMITS_LIABILITY='Y'    
    END     
  ELSE    
    BEGIN     
     SET @SINGLE_LIMITS_LIABILITY='N'    
    END     
--END    
--Only Indiana State ,Only Automobile and Motorcycle  LOB  
--If the limit for Uninsured Motorist or Uninsured Motorist CSL are not  
--the same for all vehicles Refer to underwriters   
DECLARE @UNINSURED_UNINSUREDCSL_MOTORIST VARCHAR  
SET @UNINSURED_UNINSUREDCSL_MOTORIST = 'N'  
  
/*  
IF((@POLICY_LOB=2 OR @POLICY_LOB=3) and @STATE_ID=14)  
BEGIN  
 SELECT              
 @UNINSURED_MOTORISTCSL  = COVERAGE_AMOUNT              
 FROM POL_UMBRELLA_UNDERLYING_POLICIES_COVERAGES  WHERE CUSTOMER_ID=@CUSTOMERID and POLICY_ID= @POLICYID and POLICY_VERSION_ID = @POLICYVERSIONID              
 AND COV_CODE='PUNCS'   
 SELECT              
 @UNINSURED_MOTORIST  = COVERAGE_AMOUNT              
 FROM POL_UMBRELLA_UNDERLYING_POLICIES_COVERAGES  WHERE CUSTOMER_ID=@CUSTOMERID and POLICY_ID= @POLICYID and POLICY_VERSION_ID = @POLICYVERSIONID              
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
-------         
SELECT                          
 @POLICY_LOB as POLICY_LOB,                      
 @POLICY_COMPANY as POLICY_COMPANY,                      
 @POLICY_NUMBER as POLICY_NUMBER,                      
 @POLICY_START_DATE as POLICY_START_DATE,                      
 @POLICY_EXPIRATION_DATE as POLICY_EXPIRATION_DATE,  
 @IS_RECORD_EXISTS as IS_RECORD_EXISTS,                    
 @UNDERINSURED_UNDERINSUREDCSL_MOTORIST AS UNDERINSURED_UNDERINSUREDCSL_MOTORIST ,    
 @UNDERLYING_PERSONAL_LIABILITY AS UNDERLYING_PERSONAL_LIABILITY   ,    
 @BODILY_INJURY_LIABILITY AS BODILY_INJURY_LIABILITY  ,    
 @PROPERTY_DAMAGE_LIABILITY AS PROPERTY_DAMAGE_LIABILITY ,    
 @SINGLE_LIMITS_LIABILITY AS SINGLE_LIMITS_LIABILITY ,  
 @UNINSURED_UNINSUREDCSL_MOTORIST AS UNINSURED_UNINSUREDCSL_MOTORIST,
 @AUTO_VEHICLE_DRIVER_RULE as AUTO_VEHICLE_DRIVER_RULE ,
 @MOTOR_MOTORINFO_DRIVER_RULE as MOTOR_MOTORINFO_DRIVER_RULE,                    
 @HOME_DWELLINGINFO_RULE as HOME_DWELLINGINFO_RULE ,                  
 @WATERCRAFT_WATERCRAFTINFO_RULE as WATERCRAFT_WATERCRAFTINFO_RULE ,  
 @DRIVERTYPE_DOB_SLIABILITY AS DRIVERTYPE_DOB_SLIABILITY 
END  


GO

