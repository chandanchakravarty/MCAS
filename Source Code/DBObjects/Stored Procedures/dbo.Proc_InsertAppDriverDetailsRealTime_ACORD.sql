IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertAppDriverDetailsRealTime_ACORD]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertAppDriverDetailsRealTime_ACORD]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------            
Proc Name       : Dbo.Proc_InsertAppDriverDetails_ACORD            
Created by      :             
Date            :            
Purpose       :Inserts Driver Detail from Quick quote           
Revison History :            
Used In        : Wolverine            
------------------------------------------------------------            
Date     Review By          Comments            
------   ------------       -------------------------*/       
--drop PROC dbo.Proc_InsertAppDriverDetails_ACORD         
CREATE PROC dbo.Proc_InsertAppDriverDetailsRealTime_ACORD                                              
(                                              
 @CUSTOMER_ID      int,                                              
 @APP_ID   int,                                              
 @APP_VERSION_ID  int,                                              
 @DRIVER_ID       smallint,                                              
 @DRIVER_FNAME      nvarchar(150),                                              
 @DRIVER_MNAME      nvarchar(50),                                              
 @DRIVER_LNAME      nvarchar(150),                                              
 @DRIVER_CODE      nvarchar(20),                                              
 @DRIVER_SUFFIX      nvarchar(20),                                              
 @DRIVER_ADD1      nvarchar(140),                                              
 @DRIVER_ADD2      nvarchar(140),                                              
 @DRIVER_CITY      nvarchar(80),                                              
 @DRIVER_STATE      nvarchar(10),                                              
 @DRIVER_ZIP      nvarchar(20),                                              
 @DRIVER_COUNTRY      nchar(10),                                              
 @DRIVER_HOME_PHONE      nvarchar(40),                                              
 @DRIVER_BUSINESS_PHONE  nvarchar(40),                                              
 @DRIVER_EXT      nvarchar(10),                                              
 @DRIVER_MOBILE      nvarchar(40),                
 @VIOLATIONS int,                                           
 @DRIVER_DOB      datetime,                                              
 @DRIVER_SSN      nvarchar(88),                                              
 @DRIVER_MART_STAT      nchar(2),                                              
 @DRIVER_SEX      nchar(2),                                              
 @DRIVER_DRIV_LIC      nvarchar(60),                                              
 @DRIVER_LIC_STATE      nvarchar(10),                                              
 @DRIVER_LIC_CLASS      nvarchar(10),                                              
 @DATE_LICENSED   datetime,                                               
 @DRIVER_REL      nvarchar(10),                                              
 @DRIVER_DRIV_TYPE      nvarchar(10)=null,                                              
 @DRIVER_OCC_CODE      nvarchar(12),                                              
 @DRIVER_OCC_CLASS      nvarchar(10),                                              
 @DRIVER_DRIVERLOYER_NAME nvarchar(150),                                              
 @DRIVER_DRIVERLOYER_ADD nvarchar(510),                                              
 @DRIVER_INCOME      Int,                                              
 @DRIVER_BROADEND_NOFAULT    smallint,                                              
 @DRIVER_PHYS_MED_IMPAIRE    nchar(2),                                              
 @DRIVER_DRINK_VIOLATION     nchar(2),                      
 @DRIVER_PREF_RISK      nchar(2),                                              
 @DRIVER_GOOD_STUDENT    nchar(2),                                              
 @DRIVER_STUD_DIST_OVER_HUNDRED    nchar(2),                
 @DRIVER_LIC_SUSPENDED  nchar(2), 
 @DRIVER_VOLUNTEER_POLICE_FIRE     nchar(2),                                    
 @DRIVER_US_CITIZEN      nchar(2) =null,     
 @CREATED_BY      int,                                       
 @CREATED_DATETIME      datetime,                         
@MODIFIED_BY      int,                                     
 @LAST_UPDATED_DATETIME  datetime,                                              
 @INSERTUPDATE  varchar(1),                               
 @DRIVER_FAX varchar(40)= null,                                              
 @RELATIONSHIP  VarChar(10) = null,        
 @RELATIONSHIP_CODE VarChar(10) = null,                           
 @SAFE_DRIVER BIT,                                              
 @Safe_Driver_Renewal_Discount decimal(9,2)=null ,                                          
 @VEHICLE_ID Smallint,                                        
 @APP_VEHICLE_PRIN_OCC_ID int,                   
 @NEW_DRIVER smallint output ,                        
 @NO_DEPENDENTS smallint = null ,                      
 @WAIVER_WORK_LOSS_BENEFITS char(1)=null,                 
 @NO_CYCLE_ENDMT nchar(1)    ,              
 @COLL_STUD_AWAY_HOME  smallint=null ,                  
 @CYCL_WITH_YOU  smallint=null                                          
)                                              
AS                                              
BEGIN                                      
                                              
DECLARE @DRIVER_STATE_ID Int                                              
DECLARE @LIC_STATE_ID Int                                              
DECLARE @OCC_CLASS_ID Int                                              
DECLARE @MAR_ST_ID Int                                              
DECLARE @REL_ID Int                                              
DECLARE @DRIVER_TYPE_ID Int                                              
DECLARE @FAX NVarChar(15)                                    
                                              
EXECUTE @DRIVER_STATE_ID = Proc_GetSTATE_ID 1,@DRIVER_STATE                                       
                                              
EXECUTE @LIC_STATE_ID =  Proc_GetSTATE_ID 1,@DRIVER_LIC_STATE                                              
                                              
EXECUTE @OCC_CLASS_ID = Proc_GetLookupID 'OCCCL',@DRIVER_OCC_CLASS                                              
                                              
EXECUTE @MAR_ST_ID = Proc_GetLookupID 'Marst',@DRIVER_MART_STAT                                              
                                              
                                              
EXECUTE @REL_ID = Proc_GetLookupID 'DRACD',@RELATIONSHIP_CODE                                                
                                                
--EXECUTE @DRIVER_TYPE_ID =Proc_GetLookupID 'MTD',@DRIVER_DRIV_TYPE                        
if @DRIVER_TYPE_ID is null                   
 SET @DRIVER_TYPE_ID = 11603 --default to 'Licensed Type'--                
          
                                       
                              
SET @DRIVER_COUNTRY = '1'                             
          
if @DRIVER_US_CITIZEN is null                  
 SET @DRIVER_US_CITIZEN ='1' -- by default driver will be US citizen                    
     
CREATE TABLE #TEMPDRVOCC
(
	[LOOKUP_VALUE_DESC] NVARCHAR(100),
	[LOOKUP_UNIQUE_ID] INT
)  
INSERT INTO #TEMPDRVOCC
EXECUTE Proc_GetLookupDescFromAcordCodes '%OCC',@DRIVER_OCC_CODE 
IF ((SELECT COUNT(*) FROM #TEMPDRVOCC) > 0)
BEGIN
	SELECT @DRIVER_OCC_CODE= CONVERT(NVARCHAR(100),LOOKUP_UNIQUE_ID) FROM #TEMPDRVOCC
END
ELSE
BEGIN
	SET @DRIVER_OCC_CODE=NULL
END
DROP TABLE #TEMPDRVOCC         
           

                     
/*                                    
--Get Customer address as Garaging address---                                    
EXECUTE Proc_GetCUSTOMER_ADDRESS                             
     @CUSTOMER_ID,  --@CUSTOMER_ID                                  
     @DRIVER_ADD1 OUTPUT,  --@CUSTOMER_ADDRESS1 
     @DRIVER_ADD2 OUTPUT,   --@CUSTOMER_ADDRESS2                                 
     @DRIVER_CITY OUTPUT,    --@CUSTOMER_CITY                                
     @DRIVER_STATE OUTPUT,   --@CUSTOMER_STATE                                 
     @DRIVER_ZIP OUTPUT,       --@CUSTOMER_ZIP                   
     @DRIVER_COUNTRY OUTPUT   --@CUSTOMER_COUNTRY                            
*/                            
                   
--------------------                                    
DECLARE @APP_LOB INT      
SELECT @APP_LOB=APP_LOB FROM APP_LIST      
WHERE CUSTOMER_ID =@CUSTOMER_ID AND      
APP_ID=@APP_ID  AND APP_VERSION_ID = @APP_VERSION_ID   
--Set 11603 Licensed 
if(@APP_LOB = 2)
begin
	set @DRIVER_DRIV_TYPE = '11603' 
end
---                                   
--Get Customer contact details                                    
/*EXECUTE Proc_GetCUSTOMER_CONTACT_DETAILS @CUSTOMER_ID,                                    
     @DRIVER_BUSINESS_PHONE OUTPUT,                                    
     @DRIVER_EXT OUTPUT,                                    
     @DRIVER_HOME_PHONE OUTPUT,                                    
     @DRIVER_MOBILE OUTPUT,                                    
     @FAX OUTPUT*/                                    
--------------------                 
--Get Customer contact details : 24 May 2006                
SELECT       @DRIVER_BUSINESS_PHONE = CUSTOMER_BUSINESS_PHONE,                
 @DRIVER_EXT = CUSTOMER_EXT,                
 @DRIVER_HOME_PHONE = CUSTOMER_HOME_PHONE,       
--CHANGED BY SWARUP ON 31/01/2007      
 @DRIVER_MOBILE = (CASE WHEN ISNULL(CUSTOMER_MOBILE,'')='' THEN PER_CUST_MOBILE ELSE CUSTOMER_MOBILE END),           
-- @DRIVER_SSN=SSN_NO,      
 @DRIVER_STATE=CUSTOMER_STATE,               
 @FAX = CUSTOMER_FAX                
FROM CLT_CUSTOMER_LIST                
WHERE CUSTOMER_ID = @CUSTOMER_ID              
 
/*                                              
print(@DRIVER_STATE_ID)                                              
print(@LIC_STATE_ID)                                              
print(@OCC_CLASS_ID)            
print(@MAR_ST_ID)                                              
print(@REL_ID)                                              
print(@DRIVER_TYPE_ID)                                              
*/                                              
                                              
if @DRIVER_ID = -1                                              
BEGIN     




DECLARE @FULL_TIME_STUDENT  INT
SET @FULL_TIME_STUDENT = NULL

IF(@APP_LOB = 2)
BEGIN
	IF(@DRIVER_GOOD_STUDENT = '1')
	  SET @FULL_TIME_STUDENT = 1
END                                      
                                               
 /*Checking the duplicay of code*/                                              
 /*                                            
 IF Exists(SELECT DRIVER_CODE FROM APP_DRIVER_DETAILS WHERE DRIVER_CODE = @DRIVER_CODE)                                              
 BEGIN                                              
  SELECT @DRIVER_ID = -1                                              
  return 0                                              
 END*/                       
                                              
 /*Generating the new driver id*/                                              
 SELECT @DRIVER_ID = IsNull(Max(DRIVER_ID),0) + 1                                               
 FROM APP_DRIVER_DETAILS                                              
 WHERE CUSTOMER_ID = @CUSTOMER_ID AND       
  APP_ID = @APP_ID AND                                              
  APP_VERSION_ID = APP_VERSION_ID     

--SET  @DRIVER_SSN = NULL               
if(@DRIVER_SSN = NULL or @DRIVER_SSN = '')
	BEGIN
		SELECT @DRIVER_SSN=CO_APPL_SSN_NO FROM CLT_APPLICANT_LIST WHERE CUSTOMER_ID = @CUSTOMER_ID  
			AND ISNULL(LTRIM(RTRIM(FIRST_NAME)),'') = ISNULL(@DRIVER_FNAME,'') AND
				ISNULL(LTRIM(RTRIM(MIDDLE_NAME)),'') = ISNULL(@DRIVER_MNAME,'')  
			AND ISNULL(LTRIM(RTRIM(LAST_NAME)),'') = ISNULL(@DRIVER_LNAME,'')                                           
	END                                              
 INSERT INTO APP_DRIVER_DETAILS 
 (         
  CUSTOMER_ID,                       
  APP_ID,                                              
  APP_VERSION_ID,                                              
  DRIVER_ID,                                               
  DRIVER_FNAME,                                       
  DRIVER_MNAME,                                              
  DRIVER_LNAME,                                              
 DRIVER_CODE,                                              
  DRIVER_SUFFIX,                                              
  DRIVER_ADD1,                                              
  DRIVER_ADD2,                                              
  DRIVER_CITY,                                              
  DRIVER_STATE,                                              
  DRIVER_ZIP,                                
  DRIVER_COUNTRY,                                              
  DRIVER_HOME_PHONE,                                              
  DRIVER_BUSINESS_PHONE,                                              
  DRIVER_EXT,                                              
  DRIVER_MOBILE,             
  VIOLATIONS,                                              
  DRIVER_DOB,                                              
  DRIVER_SSN,                                              
  DRIVER_MART_STAT,                              
  DRIVER_SEX,                                              
  DRIVER_DRIV_LIC,                                              
  DRIVER_LIC_STATE,                                              
  DRIVER_LIC_CLASS,                                              
                              
  DATE_LICENSED,                                              
  DRIVER_REL,             
  DRIVER_DRIV_TYPE,                                              
  [DRIVER_OCC_CODE],                                              
  DRIVER_OCC_CLASS,                                              
  DRIVER_DRIVERLOYER_NAME,                                              
  DRIVER_DRIVERLOYER_ADD,                                   
  DRIVER_INCOME,                                              
  DRIVER_BROADEND_NOFAULT,                                              
  DRIVER_PHYS_MED_IMPAIRE,                                              
  DRIVER_DRINK_VIOLATION,                                              
  DRIVER_PREF_RISK,                                              
  DRIVER_GOOD_STUDENT,                                              
  DRIVER_STUD_DIST_OVER_HUNDRED,                                              
DRIVER_LIC_SUSPENDED,                                              
  DRIVER_VOLUNTEER_POLICE_FIRE,                                              
  DRIVER_US_CITIZEN,                                              
  IS_ACTIVE,                             
  CREATED_BY,                                              
  CREATED_DATETIME,                                              
  DRIVER_FAX,                                              
  RELATIONSHIP,                                              
  SAFE_DRIVER,      
  SAFE_DRIVER_RENEWAL_DISCOUNT,                                         
  VEHICLE_ID,                                        
  APP_VEHICLE_PRIN_OCC_ID,                        
  NO_DEPENDENTS,                      
WAIVER_WORK_LOSS_BENEFITS ,                 
NO_CYCLE_ENDMT,              
COLL_STUD_AWAY_HOME,              
CYCL_WITH_YOU ,
FULL_TIME_STUDENT                    
                                         
 )                                              
 VALUES 
 (                                              
  @CUSTOMER_ID,     
  @APP_ID,                                              
  @APP_VERSION_ID,                                              
  @DRIVER_ID,                         
 @DRIVER_FNAME,                                              
  @DRIVER_MNAME,  
  @DRIVER_LNAME,    
  @DRIVER_CODE,                                     
  @DRIVER_SUFFIX,                                              
  @DRIVER_ADD1,                                
  @DRIVER_ADD2,                                              
  @DRIVER_CITY,                                              
--  @DRIVER_STATE_ID,                                            
  @DRIVER_STATE,      
  @DRIVER_ZIP,                                 
  @DRIVER_COUNTRY,                                              
  @DRIVER_HOME_PHONE,                                              
  @DRIVER_BUSINESS_PHONE,                                              
  @DRIVER_EXT,                                              
 @DRIVER_MOBILE,             
  @VIOLATIONS,                                             
  @DRIVER_DOB,                                   
  @DRIVER_SSN,                                              
  @DRIVER_MART_STAT,                                              
  @DRIVER_SEX,                                              
  @DRIVER_DRIV_LIC,                                              
  @LIC_STATE_ID, --y default the drivers license state will be same as the state of driver                                            
  @DRIVER_LIC_CLASS,                                              
                                     
  @DATE_LICENSED,                                              
  @DRIVER_REL,          
  @DRIVER_DRIV_TYPE ,--@DRIVER_TYPE_ID,                                   
	@OCC_CLASS_ID,  
	@DRIVER_OCC_CODE,                                              
                                                
  @DRIVER_DRIVERLOYER_NAME,                                              
  @DRIVER_DRIVERLOYER_ADD,                                              
  @DRIVER_INCOME,                                              
  @DRIVER_BROADEND_NOFAULT,                                              
  @DRIVER_PHYS_MED_IMPAIRE,                                              
@DRIVER_DRINK_VIOLATION,                                          
  @DRIVER_PREF_RISK,                                              
  @DRIVER_GOOD_STUDENT,                                              
  @DRIVER_STUD_DIST_OVER_HUNDRED,                                              
  @DRIVER_LIC_SUSPENDED,                                              
  @DRIVER_VOLUNTEER_POLICE_FIRE,                                              
  @DRIVER_US_CITIZEN,                                              
  'Y',                                              
  @CREATED_BY,                                              
  @CREATED_DATETIME,                                              
  @DRIVER_FAX,                                              
  @REL_ID,                                              
  @SAFE_DRIVER ,      
  @SAFE_DRIVER_RENEWAL_DISCOUNT,                                         
  @VEHICLE_ID,                                        
  @APP_VEHICLE_PRIN_OCC_ID,                        
@NO_DEPENDENTS  ,                      
@WAIVER_WORK_LOSS_BENEFITS,                 
@NO_CYCLE_ENDMT,                     
@COLL_STUD_AWAY_HOME,              
@CYCL_WITH_YOU    ,
@FULL_TIME_STUDENT                                            
 )                                              
      
      
--Inserting Assign APP_DRIVER_ASSIGNED_VEHICLE (LOB:AUTO):      
--2 AUTOP      
--3 CYCL      
      
/*DECLARE @APP_LOB INT      
SELECT @APP_LOB=APP_LOB FROM APP_LIST      
WHERE CUSTOMER_ID =@CUSTOMER_ID AND      
APP_ID=@APP_ID  AND APP_VERSION_ID = @APP_VERSION_ID    */  
  
--Add data in assigned vehicle for both Automobile and Motorcycle    
IF (@APP_LOB = 2 OR @APP_LOB=3)      
BEGIN    
		if(@APP_VEHICLE_PRIN_OCC_ID <> 11931) --Condition for Utility trailer / CTT
		begin
			INSERT INTO APP_DRIVER_ASSIGNED_VEHICLE      
			(      
			 CUSTOMER_ID,      
			 APP_ID,      
			 APP_VERSION_ID,      
			 DRIVER_ID,    
			 VEHICLE_ID,      
			 APP_VEHICLE_PRIN_OCC_ID      
			)      
			VALUES      
			(      
			 @CUSTOMER_ID,      
			 @APP_ID,      
			 @APP_VERSION_ID,      
			 @DRIVER_ID,      
			 @VEHICLE_ID,      
			 @APP_VEHICLE_PRIN_OCC_ID      
			)      
        end 
		/*Start Not rated Drivers*/
		CREATE TABLE #TEMP_VEHICLES   
			(         
			   [IDENT_COL] [INT] IDENTITY(1,1) NOT NULL ,                  
			   [VEHICLE_ID]  INT  ,
			   Driver_id int 
			
			       
			)     

			INSERT INTO #TEMP_VEHICLES

			SELECT VEHICLE_ID ,@DRIVER_ID FROM APP_VEHICLES WITH(NOLOCK)
			WHERE CUSTOMER_ID = @CUSTOMER_ID AND                                              
			APP_ID = @APP_ID AND                                              
			APP_VERSION_ID = @APP_VERSION_ID   
			AND VEHICLE_ID NOT IN 
			(SELECT VEHICLE_ID FROM APP_DRIVER_ASSIGNED_VEHICLE WITH(NOLOCK) WHERE 
			CUSTOMER_ID =@CUSTOMER_ID AND APP_ID=@APP_ID
			AND DRIVER_ID=@DRIVER_ID AND APP_VEHICLE_PRIN_OCC_ID NOT IN (11931))
			         
				     INSERT INTO APP_DRIVER_ASSIGNED_VEHICLE      
						(      
						 CUSTOMER_ID,      
						 APP_ID,      
						 APP_VERSION_ID,      
						 DRIVER_ID,      
						 VEHICLE_ID,      
						 APP_VEHICLE_PRIN_OCC_ID      
						)      
						      
						SELECT 
						 @CUSTOMER_ID,      
						 @APP_ID,      
						 @APP_VERSION_ID,      
						 @DRIVER_ID,      
						 VEHICLE_ID,      
						 11931 ---NOT RATED     
						FROM #TEMP_VEHICLES 
						--where vehicle_id = vehicle_id
					 

						
		/*END*/

END      

DROP TABLE #TEMP_VEHICLES
--             
END                                          
ELSE                      
                           
BEGIN                                              
 /*                         
                                              
 IF Exists(SELECT DRIVER_CODE FROM APP_DRIVER_DETAILS WHERE DRIVER_CODE = @DRIVER_CODE AND DRIVER_ID <> @DRIVER_ID)                                              
 BEGIN       
  return 0                                              
 END*/                                              
                                                
                                             
 /*Update the values */                                              
 UPDATE APP_DRIVER_DETAILS                                             
 SET                                              
  DRIVER_FNAME   = @DRIVER_FNAME,                                              
  DRIVER_MNAME   = @DRIVER_MNAME,                                              
  DRIVER_LNAME   = @DRIVER_LNAME,                                              
  DRIVER_CODE    = @DRIVER_CODE,                                         
  DRIVER_SUFFIX   = @DRIVER_SUFFIX,                                              
  DRIVER_ADD1   = @DRIVER_ADD1,                                               
  DRIVER_ADD2   = @DRIVER_ADD2,                                              
  DRIVER_CITY   = @DRIVER_CITY,                               
  DRIVER_STATE   = @DRIVER_STATE,                                              
  DRIVER_ZIP   = @DRIVER_ZIP,                                               
  DRIVER_COUNTRY   = @DRIVER_COUNTRY,                    
  DRIVER_HOME_PHONE  = @DRIVER_HOME_PHONE,                                               
  DRIVER_BUSINESS_PHONE  = @DRIVER_BUSINESS_PHONE,                                               
  DRIVER_EXT   = @DRIVER_EXT,               
  DRIVER_MOBILE   = @DRIVER_MOBILE,             
  VIOLATIONS = @VIOLATIONS,                                   
  DRIVER_DOB   = @DRIVER_DOB,                                               
  DRIVER_SSN   = @DRIVER_SSN,                                               
  DRIVER_MART_STAT  = @DRIVER_MART_STAT,                           
  DRIVER_SEX   = @DRIVER_SEX,                                    
  DRIVER_DRIV_LIC  = @DRIVER_DRIV_LIC, 
  DRIVER_LIC_STATE  = @DRIVER_STATE_ID , --@LIC_STATE_ID,     (by default driver state id)                                          
  DRIVER_LIC_CLASS  = @DRIVER_LIC_CLASS,                                               
                              
  DATE_LICENSED = @DATE_LICENSED ,                                              
  DRIVER_REL   = @DRIVER_REL,                                               
  DRIVER_DRIV_TYPE  = @DRIVER_TYPE_ID,                                               
  [DRIVER_OCC_CODE]  = @DRIVER_OCC_CODE,                                   
  DRIVER_OCC_CLASS  = @OCC_CLASS_ID,                                               
  DRIVER_DRIVERLOYER_NAME = @DRIVER_DRIVERLOYER_NAME,                                               
  DRIVER_DRIVERLOYER_ADD  = @DRIVER_DRIVERLOYER_ADD,                                               
  DRIVER_INCOME    = @DRIVER_INCOME,                                               
  DRIVER_BROADEND_NOFAULT = @DRIVER_BROADEND_NOFAULT,                               
  DRIVER_PHYS_MED_IMPAIRE = @DRIVER_PHYS_MED_IMPAIRE,                                               
  DRIVER_DRINK_VIOLATION  = @DRIVER_DRINK_VIOLATION,                       
  DRIVER_PREF_RISK  = @DRIVER_PREF_RISK,                                               
  DRIVER_GOOD_STUDENT  =  @DRIVER_GOOD_STUDENT,                                               
  DRIVER_STUD_DIST_OVER_HUNDRED = @DRIVER_STUD_DIST_OVER_HUNDRED,                                               
 DRIVER_LIC_SUSPENDED  = @DRIVER_LIC_SUSPENDED,                                               
  DRIVER_VOLUNTEER_POLICE_FIRE = @DRIVER_VOLUNTEER_POLICE_FIRE,                                               
  DRIVER_US_CITIZEN  = @DRIVER_US_CITIZEN,                                               
  MODIFIED_BY   = @MODIFIED_BY,                                               
  LAST_UPDATED_DATETIME  = @LAST_UPDATED_DATETIME,                                              
  DRIVER_FAX=@DRIVER_FAX,                                        
  RELATIONSHIP=@REL_ID,                                              
  SAFE_DRIVER=@SAFE_DRIVER ,      
  SAFE_DRIVER_RENEWAL_DISCOUNT = @SAFE_DRIVER_RENEWAL_DISCOUNT,                                        
  VEHICLE_ID = @VEHICLE_ID,                  
  APP_VEHICLE_PRIN_OCC_ID = @APP_VEHICLE_PRIN_OCC_ID ,                        
NO_DEPENDENTS = @NO_DEPENDENTS  ,                      
WAIVER_WORK_LOSS_BENEFITS=@WAIVER_WORK_LOSS_BENEFITS,                
NO_CYCLE_ENDMT=@NO_CYCLE_ENDMT                      
                        
                                         
                                        
 WHERE                                              
  CUSTOMER_ID  = @CUSTOMER_ID AND                                              
 APP_ID   = @APP_ID AND                                              
  APP_VERSION_ID  = @APP_VERSION_ID AND                                              
  DRIVER_ID   = @DRIVER_ID                                              
                                        
          
                                        
                                         
 --IF @DRIVER_ID IS NOT NULL                       
                                               
 /*Updating values end*/                                               
END                                     
                                              
 SET @NEW_DRIVER = @DRIVER_ID                                             
                                            
END          
        
      

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
	IF(rtrim(ltrim(@CUSTOMER_ZIP)) = rtrim(ltrim(@DRIVER_ZIP)))
	BEGIN
		UPDATE APP_DRIVER_DETAILS SET DRIVER_ZIP = @CUSTOMER_ZIP + '-' + @EXTENSION 
			WHERE                                              
			CUSTOMER_ID		= @CUSTOMER_ID AND                                              
			APP_ID			= @APP_ID AND                                              
			APP_VERSION_ID  = @APP_VERSION_ID AND                                              
			DRIVER_ID		= @DRIVER_ID     
	END	
END

------------UPDATE ZIP EXTENSION END------------------          
      
      
      
    
    
    
  














GO

