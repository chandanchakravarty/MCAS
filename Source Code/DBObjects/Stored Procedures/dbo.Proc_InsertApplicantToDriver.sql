IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertApplicantToDriver]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertApplicantToDriver]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                                    
Proc Name          : Dbo.Proc_InsertApplicantToDriver                                    
Created by           : Sumit Chhabra                                    
Date                    : Nov 25,2005                                
Purpose               :  Insert Applicants into driver table                                
Revison History :                                    
Used In                :   Wolverine                                      
                            
------------------------------------------------------------                                    
Date     Review By          Comments                                    
------   ------------       -------------------------*/                                    
--DROP PROCEDURE Proc_InsertApplicantToDriver                          
CREATE  PROCEDURE Proc_InsertApplicantToDriver                                    
(                                    
 @CUSTOMER_ID INT,                                    
 @APP_ID INT,                                    
 @APP_VERSION_ID INT,                                    
 @APPLICANT_ID INT,                                    
 @CREATED_BY_USER_ID  INT,                                
 @CALLED_FROM VARCHAR(10),                                      
 @CALLED_FOR VARCHAR(10),              
 @DRIVER_CODE VARCHAR(30) OUTPUT                              
)                                    
AS                                    
BEGIN                                    
DECLARE @NEW_DRIVER_ID INT                                        
--DECLARE @CODE AS VARCHAR(20)                                
DECLARE @FIRSTCHAR VARCHAR(20)                                
DECLARE @LASTCHAR VARCHAR(20)                                
                                
                                
SET @DRIVER_CODE=CONVERT(INT,RAND()*50)                                
                                
SELECT @FIRSTCHAR=FIRST_NAME,    
-- @LASTCHAR=LAST_NAME - Done by Sibin For Itrack Issue 5044 on 12 Nov 08    
 @LASTCHAR=ISNULL(LAST_NAME,'')     
 FROM CLT_APPLICANT_LIST WHERE APPLICANT_ID=@APPLICANT_ID                                
SET @DRIVER_CODE=(SUBSTRING(@FIRSTCHAR,1,3) + SUBSTRING(@LASTCHAR,1,4) + @DRIVER_CODE)                                
                                    
          
                              
IF (UPPER(@CALLED_FROM)='UMB' and UPPER(@CALLED_FOR)='WAT')--umbrella's operators                              
BEGIN                                
 SELECT  @NEW_DRIVER_ID = ISNULL(MAX(DRIVER_ID),0) + 1   FROM    APP_UMBRELLA_OPERATOR_INFO                                      
  WHERE  CUSTOMER_ID=@CUSTOMER_ID    AND         APP_ID=@APP_ID                                    
  AND APP_VERSION_ID=@APP_VERSION_ID                                       
                                
                                
                              
 INSERT INTO APP_UMBRELLA_OPERATOR_INFO                                     
 (                                    
  CUSTOMER_ID,APP_ID,APP_VERSION_ID,DRIVER_ID,DRIVER_FNAME,DRIVER_MNAME,DRIVER_LNAME,DRIVER_CODE,                                
  DRIVER_SUFFIX,DRIVER_ADD1,DRIVER_ADD2,DRIVER_CITY,DRIVER_STATE,DRIVER_ZIP,                                   
  IS_ACTIVE,CREATED_BY,CREATED_DATETIME,DRIVER_DOB,DRIVER_SSN,DRIVER_SEX,DRIVER_LIC_STATE                                
 )                                    
 SELECT                                    
  @CUSTOMER_ID,@APP_ID,@APP_VERSION_ID,@NEW_DRIVER_ID,FIRST_NAME,MIDDLE_NAME,LAST_NAME,@DRIVER_CODE,                                    
  SUFFIX,ADDRESS1,ADDRESS2,CITY,STATE,ZIP_CODE,'Y', @CREATED_BY_USER_ID,GETDATE(),                                
     CO_APPL_DOB,CO_APPL_SSN_NO,'',STATE                
 FROM    CLT_APPLICANT_LIST                                    
 WHERE   CUSTOMER_ID=@CUSTOMER_ID                                    
   AND         APPLICANT_ID=@APPLICANT_ID                                
   AND         IS_ACTIVE='Y'                                
                                
END                                
                                
ELSE IF (UPPER(@CALLED_FROM)='UMB' and UPPER(@CALLED_FOR)='PPA')---umbrella's drivers                              
BEGIN                                
 SELECT  @NEW_DRIVER_ID = ISNULL(MAX(DRIVER_ID),0) + 1   FROM    APP_UMBRELLA_DRIVER_DETAILS                                     
  WHERE  CUSTOMER_ID=@CUSTOMER_ID    AND         APP_ID=@APP_ID                                    
  AND APP_VERSION_ID=@APP_VERSION_ID                      
                                
                        
                                   
 INSERT INTO APP_UMBRELLA_DRIVER_DETAILS                                    
 (                                    
  CUSTOMER_ID,APP_ID,APP_VERSION_ID,DRIVER_ID,DRIVER_FNAME,DRIVER_MNAME,DRIVER_LNAME,DRIVER_CODE,                                
  DRIVER_SUFFIX,DRIVER_ADD1,DRIVER_ADD2,DRIVER_CITY,DRIVER_STATE,DRIVER_ZIP,DRIVER_HOME_PHONE,                                   
  IS_ACTIVE,CREATED_BY,CREATED_DATETIME,DRIVER_MART_STAT,DRIVER_DOB,DRIVER_SSN,DRIVER_SEX,                        
 DRIVER_DRIVERLOYER_NAME,DRIVER_DRIVERLOYER_ADD,DRIVER_US_CITIZEN,DRIVER_LIC_STATE                     
                               
 )                                    
 SELECT                                    
  @CUSTOMER_ID,@APP_ID,@APP_VERSION_ID,@NEW_DRIVER_ID,FIRST_NAME,MIDDLE_NAME,LAST_NAME,@DRIVER_CODE,                                    
  SUFFIX,ADDRESS1,ADDRESS2,CITY,STATE,ZIP_CODE,PHONE,'Y', @CREATED_BY_USER_ID,GETDATE(),                                
     CO_APPL_MARITAL_STATUS,CO_APPL_DOB,CO_APPL_SSN_NO,'',CO_APPLI_EMPL_NAME,CO_APPLI_EMPL_ADDRESS,1,STATE                                  
 FROM CLT_APPLICANT_LIST                                    
 WHERE   CUSTOMER_ID=@CUSTOMER_ID                                    
   AND         APPLICANT_ID=@APPLICANT_ID                                
   AND         IS_ACTIVE='Y'                                
                                
END                                
                                
ELSE IF (UPPER(@CALLED_FROM)='MOT' OR UPPER(@CALLED_FROM)='PPA') ---motorcycle and automobile drivers                               
 BEGIN                                
   SELECT  @NEW_DRIVER_ID = ISNULL(MAX(DRIVER_ID),0) + 1   FROM     APP_DRIVER_DETAILS                                     
   WHERE  CUSTOMER_ID=@CUSTOMER_ID                                    
    AND         APP_ID=@APP_ID                                    
    AND         APP_VERSION_ID=@APP_VERSION_ID                                       
                                    
                                   
   INSERT INTO APP_DRIVER_DETAILS                                    
   (                                    
    CUSTOMER_ID,APP_ID,APP_VERSION_ID,DRIVER_ID,DRIVER_FNAME,DRIVER_MNAME,DRIVER_LNAME,DRIVER_CODE,                                
    DRIVER_SUFFIX,DRIVER_ADD1,DRIVER_ADD2,DRIVER_CITY,DRIVER_STATE,DRIVER_ZIP,DRIVER_MOBILE,DRIVER_HOME_PHONE,                                   
    DRIVER_BUSINESS_PHONE,DRIVER_EXT,IS_ACTIVE,CREATED_BY,CREATED_DATETIME,DRIVER_MART_STAT,DRIVER_DOB,        
    DRIVER_SSN,DRIVER_SEX,DRIVER_DRIVERLOYER_NAME,DRIVER_DRIVERLOYER_ADD,DRIVER_US_CITIZEN,DRIVER_LIC_STATE,        
    NO_DEPENDENTS,
	DRIVER_OCC_CLASS --Done by Sibin For Itrack Issue 5061                              
                                
                                
   )                                    
   SELECT                                    
     @CUSTOMER_ID,@APP_ID,@APP_VERSION_ID,@NEW_DRIVER_ID,FIRST_NAME,MIDDLE_NAME,LAST_NAME,@DRIVER_CODE,                                    
     SUFFIX,ADDRESS1,ADDRESS2,CITY,STATE,ZIP_CODE,PER_CUST_MOBILE,PHONE,BUSINESS_PHONE,EXT,'Y', @CREATED_BY_USER_ID,GETDATE(),                                     
     CO_APPL_MARITAL_STATUS,CO_APPL_DOB,CO_APPL_SSN_NO,CONVERT(nchar(1),CO_APPL_GENDER),CO_APPLI_EMPL_NAME,CO_APPLI_EMPL_ADDRESS,1,STATE,11589,
	CO_APPLI_OCCU --Done by Sibin For Itrack Issue 5061                                
                                    
   FROM    CLT_APPLICANT_LIST                                    
   WHERE    CUSTOMER_ID=@CUSTOMER_ID                                    
    AND      APPLICANT_ID=@APPLICANT_ID                                
    AND         IS_ACTIVE='Y'                                
           
END                                
ELSE IF (UPPER(@CALLED_FROM)='WAT' or UPPER(@CALLED_FROM)='HOME')---home and watercraft operators                              
 BEGIN                                
SELECT  @NEW_DRIVER_ID = ISNULL(MAX(DRIVER_ID),0) + 1   FROM   APP_WATERCRAFT_DRIVER_DETAILS                                     
   WHERE  CUSTOMER_ID=@CUSTOMER_ID                                    
    AND         APP_ID=@APP_ID                                    
    AND         APP_VERSION_ID=@APP_VERSION_ID                                       
                                    
                                   
   INSERT INTO APP_WATERCRAFT_DRIVER_DETAILS                                    
   (                                    
    CUSTOMER_ID,APP_ID,APP_VERSION_ID,DRIVER_ID,DRIVER_FNAME,DRIVER_MNAME,DRIVER_LNAME,DRIVER_CODE,                                
    DRIVER_SUFFIX,DRIVER_ADD1,DRIVER_ADD2,DRIVER_CITY,DRIVER_STATE,DRIVER_ZIP,                              
    IS_ACTIVE,CREATED_BY,CREATED_DATETIME,DRIVER_DOB,DRIVER_SSN,DRIVER_SEX,DRIVER_LIC_STATE, MARITAL_STATUS                                
                                
            
   )                                    
   SELECT                                    
     @CUSTOMER_ID,@APP_ID,@APP_VERSION_ID,@NEW_DRIVER_ID,FIRST_NAME,MIDDLE_NAME,LAST_NAME,@DRIVER_CODE,                                    
     SUFFIX,ADDRESS1,ADDRESS2,CITY,STATE,ZIP_CODE,'Y', @CREATED_BY_USER_ID,GETDATE(),                             
     CO_APPL_DOB,CO_APPL_SSN_NO,CONVERT(nchar(1),CO_APPL_GENDER),STATE, CO_APPL_MARITAL_STATUS                               
                                    
   FROM    CLT_APPLICANT_LIST                                    
   WHERE    CUSTOMER_ID=@CUSTOMER_ID                                    
    AND         APPLICANT_ID=@APPLICANT_ID                                
    AND         IS_ACTIVE='Y'                                
                             
END                       
ELSE                                
RETURN -1                                    
 END 
GO

