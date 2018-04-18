IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertPolicyApplicantToDriver]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertPolicyApplicantToDriver]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

 -------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- 
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
  
   
/*----------------------------------------------------------                          
Proc Name          : Dbo.Proc_ InsertPolicyApplicantToDriver                          
Created by         : Swastika Gaur                          
Date               : 24th Mar'06                      
Purpose            :  Insert Policy applicants into driver table                      
Revison History    :                          
Used In            :   Wolverine                            
                  
------------------------------------------------------------                          
Date     Review By          Comments                          
------   ------------       -------------------------*/                          
-- drop proc dbo.Proc_InsertPolicyApplicantToDriver                                       
CREATE  PROCEDURE dbo.Proc_InsertPolicyApplicantToDriver                          
(                          
 @CUSTOMER_ID INT,                          
 @POLICY_ID INT,                          
 @POLICY_VERSION_ID INT,                          
 @APPLICANT_ID INT,                          
 @CREATED_BY_USER_ID  INT,                      
 @CALLED_FROM VARCHAR(10),                            
 @CALLED_FOR VARCHAR(10),    
 @DRIVER_CODE VARCHAR(30) OUTPUT                    
)                          
AS                          
BEGIN                          
 DECLARE @NEW_DRIVER_ID INT                              
 DECLARE @FIRSTCHAR VARCHAR(20)                      
 DECLARE @LASTCHAR VARCHAR(20)                      
                       
 SET @DRIVER_CODE=CONVERT(INT,RAND()*50)                      
  
 SELECT @FIRSTCHAR=FIRST_NAME, 
-- @LASTCHAR=LAST_NAME - Done by Sibin For Itrack Issue 5044 on 12 Nov 08    
 @LASTCHAR=ISNULL(LAST_NAME,'') 
FROM CLT_APPLICANT_LIST (NOLOCK) WHERE APPLICANT_ID=@APPLICANT_ID                      
 SET @DRIVER_CODE=(SUBSTRING(@FIRSTCHAR,1,3) + SUBSTRING(@LASTCHAR,1,4) + @DRIVER_CODE)                      
   
 IF (UPPER(@CALLED_FROM)='UMB')  
  BEGIN                      
    SELECT  @NEW_DRIVER_ID = ISNULL(MAX(DRIVER_ID),0) + 1   FROM    POL_UMBRELLA_DRIVER_DETAILS  (NOLOCK)                         
     WHERE  CUSTOMER_ID=@CUSTOMER_ID    AND         POLICY_ID=@POLICY_ID                          
     AND POLICY_VERSION_ID=@POLICY_VERSION_ID                             
                         
                 
    INSERT INTO POL_UMBRELLA_DRIVER_DETAILS                          
    (                          
     CUSTOMER_ID,POLICY_ID,POLICY_VERSION_ID,DRIVER_ID,DRIVER_FNAME,DRIVER_MNAME,DRIVER_LNAME,DRIVER_CODE,                      
     DRIVER_SUFFIX,DRIVER_ADD1,DRIVER_ADD2,DRIVER_CITY,DRIVER_STATE,DRIVER_ZIP,DRIVER_HOME_PHONE,                         
     IS_ACTIVE,CREATED_BY,CREATED_DATETIME,DRIVER_MART_STAT,DRIVER_DOB,DRIVER_SSN,DRIVER_SEX,              
     DRIVER_DRIVERLOYER_NAME,DRIVER_DRIVERLOYER_ADD,DRIVER_US_CITIZEN,DRIVER_LIC_STATE           
    )                          
    SELECT                          
     @CUSTOMER_ID,@POLICY_ID,@POLICY_VERSION_ID,@NEW_DRIVER_ID,FIRST_NAME,MIDDLE_NAME,LAST_NAME,@DRIVER_CODE,                          
     SUFFIX,ADDRESS1,ADDRESS2,CITY,STATE,ZIP_CODE,PHONE,'Y', @CREATED_BY_USER_ID,GETDATE(),                      
    CO_APPL_MARITAL_STATUS,CO_APPL_DOB,CO_APPL_SSN_NO,'',CO_APPLI_EMPL_NAME,CO_APPLI_EMPL_ADDRESS,1,STATE                        
    FROM CLT_APPLICANT_LIST   (NOLOCK)                       
    WHERE   CUSTOMER_ID=@CUSTOMER_ID AND APPLICANT_ID=@APPLICANT_ID AND IS_ACTIVE='Y'                      
  END                      
 ELSE IF (UPPER(@CALLED_FROM)='MOT' OR UPPER(@CALLED_FROM)='PPA') ---motorcycle and automobile drivers  
     BEGIN                      
     SELECT  @NEW_DRIVER_ID = ISNULL(MAX(DRIVER_ID),0) + 1   FROM     POL_DRIVER_DETAILS (NOLOCK)                          
     WHERE  CUSTOMER_ID=@CUSTOMER_ID                          
      AND         POLICY_ID=@POLICY_ID                          
      AND         POLICY_VERSION_ID=@POLICY_VERSION_ID                             
     INSERT INTO POL_DRIVER_DETAILS   
     (            
      CUSTOMER_ID,POLICY_ID,POLICY_VERSION_ID,DRIVER_ID,DRIVER_FNAME,DRIVER_MNAME,DRIVER_LNAME,DRIVER_CODE,                      
      DRIVER_SUFFIX,DRIVER_ADD1,DRIVER_ADD2,DRIVER_CITY,DRIVER_STATE,DRIVER_ZIP,DRIVER_HOME_PHONE,                         
      IS_ACTIVE,CREATED_BY,CREATED_DATETIME,DRIVER_MART_STAT,DRIVER_DOB,DRIVER_SSN,DRIVER_SEX,              
      DRIVER_DRIVERLOYER_NAME,DRIVER_DRIVERLOYER_ADD,DRIVER_US_CITIZEN,DRIVER_LIC_STATE,NO_DEPENDENTS,  
   DRIVER_BUSINESS_PHONE,DRIVER_EXT,DRIVER_MOBILE,
	DRIVER_OCC_CLASS --Added by Sibin for Itrack Issue 5061 on 26 Nov 08                
      )                          
     SELECT                          
       @CUSTOMER_ID,@POLICY_ID,@POLICY_VERSION_ID,@NEW_DRIVER_ID,FIRST_NAME,MIDDLE_NAME,LAST_NAME,@DRIVER_CODE,                          
       SUFFIX,ADDRESS1,ADDRESS2,CITY,STATE,ZIP_CODE,PHONE,'Y', @CREATED_BY_USER_ID,GETDATE(),                           
       CO_APPL_MARITAL_STATUS,CO_APPL_DOB,CO_APPL_SSN_NO,CO_APPL_GENDER,CO_APPLI_EMPL_NAME,CO_APPLI_EMPL_ADDRESS,1,STATE,11589,  
    BUSINESS_PHONE,EXT,MOBILE,
	CO_APPLI_OCCU  --Added by Sibin for Itrack Issue 5061 on 26 Nov 08                     
     FROM    CLT_APPLICANT_LIST  (NOLOCK)                        
     WHERE    CUSTOMER_ID=@CUSTOMER_ID AND APPLICANT_ID=@APPLICANT_ID AND IS_ACTIVE='Y'                      
  
     
  END                      
 ELSE IF (UPPER(@CALLED_FROM)='WAT' or UPPER(@CALLED_FROM)='HOME')---home and watercraft operators                    
  BEGIN                      
     SELECT  @NEW_DRIVER_ID = ISNULL(MAX(DRIVER_ID),0) + 1   FROM   POL_WATERCRAFT_DRIVER_DETAILS                           
     WHERE  CUSTOMER_ID=@CUSTOMER_ID                          
      AND         POLICY_ID=@POLICY_ID                          
      AND         POLICY_VERSION_ID=@POLICY_VERSION_ID                             
      
     INSERT INTO POL_WATERCRAFT_DRIVER_DETAILS                          
     (                          
   CUSTOMER_ID,POLICY_ID,POLICY_VERSION_ID,DRIVER_ID,DRIVER_FNAME,DRIVER_MNAME,DRIVER_LNAME,DRIVER_CODE,                      
      DRIVER_SUFFIX,DRIVER_ADD1,DRIVER_ADD2,DRIVER_CITY,DRIVER_STATE,DRIVER_ZIP,                    
      IS_ACTIVE,CREATED_BY,CREATED_DATETIME,DRIVER_DOB,DRIVER_SSN,DRIVER_SEX,DRIVER_LIC_STATE,MARITAL_STATUS                      
     )                          
     SELECT                          
       @CUSTOMER_ID,@POLICY_ID,@POLICY_VERSION_ID,@NEW_DRIVER_ID,FIRST_NAME,MIDDLE_NAME,LAST_NAME,@DRIVER_CODE,                          
       SUFFIX,ADDRESS1,ADDRESS2,CITY,STATE,ZIP_CODE,'Y', @CREATED_BY_USER_ID,GETDATE(),                   
       CO_APPL_DOB,CO_APPL_SSN_NO,CO_APPL_GENDER,STATE,CO_APPL_MARITAL_STATUS                     
     FROM    CLT_APPLICANT_LIST (NOLOCK)                         
     WHERE    CUSTOMER_ID=@CUSTOMER_ID AND APPLICANT_ID=@APPLICANT_ID AND IS_ACTIVE='Y'                      
  END     
  ELSE       
  RETURN -1                          
END  
  
  
  
  
  
  
  
  
  
GO

