IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertApplicationGenInfo]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertApplicationGenInfo]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

 --drop proc dbo.Proc_InsertApplicationGenInfo   
CREATE PROC dbo.Proc_InsertApplicationGenInfo      
(      
 @CUSTOMER_ID     int,      
 @APP_ID     int   output  ,      
 @APP_VERSION_ID     smallint,      
 @PARENT_APP_VERSION_ID     smallint,      
 @APP_STATUS     nvarchar(10),      
 @APP_NUMBER     nvarchar(150),      
 @APP_VERSION     nvarchar(6),      
 @APP_TERMS     nvarchar(10),      
 @APP_INCEPTION_DATE     datetime=null,      
 @APP_EFFECTIVE_DATE     datetime,      
 @APP_EXPIRATION_DATE     datetime,      
 @APP_LOB     nvarchar(10),      
 @APP_SUBLOB     nvarchar(10),      
 @CSR     int,      
 @UNDERWRITER     int,      
 @IS_UNDER_REVIEW     nchar(2),      
 @APP_AGENCY_ID     smallint,      
 @IS_ACTIVE     nchar(2),      
 @CREATED_BY     int,      
 @CREATED_DATETIME     datetime,      
 @MODIFIED_BY     int,      
 @LAST_UPDATED_DATETIME     datetime,      
 @COUNTRY_ID int,      
 @STATE_ID int,      
 @DIV_ID     smallint,      
 @DEPT_ID     smallint,      
 @PC_ID     smallint,      
 @BILL_TYPE_ID     Integer = null,      
 @COMPLETE_APP     char(1),      
 @PROPRTY_INSP_CREDIT int,      
 @INSTALL_PLAN_ID int,      
 @CHARGE_OFF_PRMIUM varchar(5),      
 @RECEIVED_PRMIUM decimal(18,2),      
 @PROXY_SIGN_OBTAINED int,      
 @POLICY_TYPE int,      
 @YEAR_AT_CURR_RESI int = null,      
 @YEARS_AT_PREV_ADD varchar(250) = null,  
 @PIC_OF_LOC int,   
 @IS_HOME_EMP   bit = null,  
 @PRODUCER int,   
--added  by pravesh  
 @DOWN_PAY_MODE int =null     
)      
AS      
BEGIN      
DECLARE @BILL_TYPE varchar(2)  
DECLARE @APPLY_INSURANCE_SCORE numeric  

--By praveen
DECLARE @CUSTOMER_REASON_CODE nvarchar(10)  
DECLARE @CUSTOMER_REASON_CODE2 nvarchar(10)  
DECLARE @CUSTOMER_REASON_CODE3 nvarchar(10)  
DECLARE @CUSTOMER_REASON_CODE4 nvarchar(10)  
--end 


  
SELECT 

@APPLY_INSURANCE_SCORE= CUSTOMER_INSURANCE_SCORE ,
@CUSTOMER_REASON_CODE = ISNULL(CUSTOMER_REASON_CODE,''),
@CUSTOMER_REASON_CODE2 = ISNULL(CUSTOMER_REASON_CODE2,''),
@CUSTOMER_REASON_CODE3 = ISNULL(CUSTOMER_REASON_CODE3,''),
@CUSTOMER_REASON_CODE4 = ISNULL(CUSTOMER_REASON_CODE4,'')

FROM CLT_CUSTOMER_LIST WHERE CUSTOMER_ID=@CUSTOMER_ID  
  
SELECT @BILL_TYPE  = TYPE FROM MNT_LOOKUP_VALUES WHERE LOOKUP_UNIQUE_ID= @BILL_TYPE_ID  
select @APP_ID=ISNULL(MAX(APP_ID),0)+1 from APP_LIST where CUSTOMER_ID=@CUSTOMER_ID;      
INSERT INTO APP_LIST      
(      
 CUSTOMER_ID,      
 APP_ID,      
 APP_VERSION_ID,      
 PARENT_APP_VERSION_ID,      
 APP_STATUS,      
 APP_NUMBER,      
 APP_VERSION,      
 APP_TERMS,      
 APP_INCEPTION_DATE,      
 APP_EFFECTIVE_DATE,      
 APP_EXPIRATION_DATE,      
 APP_LOB,      
 APP_SUBLOB,      
 CSR,      
 UNDERWRITER,      
 IS_UNDER_REVIEW,      
 APP_AGENCY_ID,      
 IS_ACTIVE,      
 CREATED_BY,      
 CREATED_DATETIME,      
 MODIFIED_BY,      
 LAST_UPDATED_DATETIME,      
 COUNTRY_ID ,      
 STATE_ID ,      
 DIV_ID,      
 DEPT_ID,      
 PC_ID,    
 BILL_TYPE_ID,    
 BILL_TYPE,      
 COMPLETE_APP,      
 PROPRTY_INSP_CREDIT,      
 INSTALL_PLAN_ID,      
 CHARGE_OFF_PRMIUM,      
 RECEIVED_PRMIUM,      
 PROXY_SIGN_OBTAINED,      
 POLICY_TYPE,      
 YEAR_AT_CURR_RESI,      
 YEARS_AT_PREV_ADD,  
 PIC_OF_LOC,  
 IS_HOME_EMP,  
 PRODUCER,  
--added  by pravesh  
 DOWN_PAY_MODE,  
 APPLY_INSURANCE_SCORE ,
--added by praveen
CUSTOMER_REASON_CODE,
CUSTOMER_REASON_CODE2,
CUSTOMER_REASON_CODE3,
CUSTOMER_REASON_CODE4      
)      
VALUES      
(      
 @CUSTOMER_ID,      
 @APP_ID,      
 @APP_VERSION_ID,      
 @PARENT_APP_VERSION_ID,      
 @APP_STATUS,      
 @APP_NUMBER,      
 @APP_VERSION,      
 @APP_TERMS,      
 @APP_INCEPTION_DATE,      
 @APP_EFFECTIVE_DATE,      
 @APP_EXPIRATION_DATE,      
 @APP_LOB,      
 @APP_SUBLOB,      
 @CSR,      
 @UNDERWRITER,      
 @IS_UNDER_REVIEW,      
 @APP_AGENCY_ID,      
 @IS_ACTIVE,      
 @CREATED_BY,      
 @CREATED_DATETIME,      
 @MODIFIED_BY,      
 @LAST_UPDATED_DATETIME,      
 @COUNTRY_ID,      
 @STATE_ID,      
 @DIV_ID,      
 @DEPT_ID,      
 @PC_ID,      
 @BILL_TYPE_ID,  
 @BILL_TYPE,      
 @COMPLETE_APP,      
 @PROPRTY_INSP_CREDIT,      
 @INSTALL_PLAN_ID,      
 @CHARGE_OFF_PRMIUM,      
 @RECEIVED_PRMIUM,      
 @PROXY_SIGN_OBTAINED,      
 @POLICY_TYPE,      
 @YEAR_AT_CURR_RESI,      
 @YEARS_AT_PREV_ADD,  
 @PIC_OF_LOC ,  
 @IS_HOME_EMP,  
 @PRODUCER,  
--added  by pravesh  
@DOWN_PAY_MODE,  
@APPLY_INSURANCE_SCORE ,
--added by praveen
@CUSTOMER_REASON_CODE,
@CUSTOMER_REASON_CODE2,
@CUSTOMER_REASON_CODE3,
@CUSTOMER_REASON_CODE4       
)     
      
--Added by Mohit.      
--for inserting customer as primary applicant while creating applicantion       
      
DECLARE @APPLICANT_ID int      
SET @APPLICANT_ID=0      
  
DECLARE @IS_PRIMARY_APPLICANT INT  
SET @IS_PRIMARY_APPLICANT=0  
      
SELECT @APPLICANT_ID = APPLICANT_ID       
FROM CLT_APPLICANT_LIST      
WHERE CUSTOMER_ID = @CUSTOMER_ID AND IS_PRIMARY_APPLICANT=1      
      
      
DECLARE @APPCOUNT int      
SET @APPCOUNT= 0      
      
IF (@APPLICANT_ID > 0)      
BEGIN      
SELECT @APPCOUNT =  COUNT(*) FROM APP_APPLICANT_LIST      
WHERE CUSTOMER_ID = @CUSTOMER_ID AND APP_ID= @APP_ID AND APP_VERSION_ID = @APP_VERSION_ID AND APPLICANT_ID=@APPLICANT_ID     
  
SET @IS_PRIMARY_APPLICANT=1  
END      
      
IF (@APPCOUNT = 0)      
BEGIN      
INSERT INTO APP_APPLICANT_LIST      
(      
APPLICANT_ID,CUSTOMER_ID,APP_ID,APP_VERSION_ID,CREATED_BY,CREATED_DATETIME,IS_PRIMARY_APPLICANT      
)      
VALUES      
(      
@APPLICANT_ID,@CUSTOMER_ID,@APP_ID,@APP_VERSION_ID,@CREATED_BY,GETDATE(),@IS_PRIMARY_APPLICANT  
)      
END      
      
      
END      
      
      
      
      
      
    
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
GO

