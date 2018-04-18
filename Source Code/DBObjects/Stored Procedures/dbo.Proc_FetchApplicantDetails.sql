IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_FetchApplicantDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_FetchApplicantDetails]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                
Proc Name       : dbo.Proc_FetchApplicantDetails                
Created by      : Chetna Agarwal      
Date            : 19/04/2010                
Purpose   :To FETCH applicants Details    
Revison History :                
Used In   : Ebix Advantage                
------------------------------------------------------------                
Date     Review By          Comments                
------   ------------       -------------------------*/                
--DROP PROC dbo.Proc_FetchApplicantDetails  1098, 28498,1,1  
    
CREATE PROC [dbo].[Proc_FetchApplicantDetails]    
(    
@APPLICANT_ID INT,   
@CUSTOMER_ID INT,  
@POLICY_VERSION_ID INT,  
@POLICY_ID INT  
)    
AS    
BEGIN  
IF EXISTS(SELECT APPLICANT_ID FROM POL_PERSONAL_ACCIDENT_INFO WHERE APPLICANT_ID=@APPLICANT_ID and CUSTOMER_ID=@CUSTOMER_ID and POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID)  
BEGIN  
select    
PERSONAL_INFO_ID,    
POLICY_ID,     
POLICY_VERSION_ID,     
CUSTOMER_ID,    
INDIVIDUAL_NAME,    
CODE,    
POSITION_ID,    
CPF_NUM,    
STATE_ID,    
--CONVERT(varchar(10),DATE_OF_BIRTH,101) AS DATE_OF_BIRTH,  
DATE_OF_BIRTH,  
GENDER,    
REG_IDEN,    
--CONVERT(varchar(10),REG_ID_ISSUES,101) AS REG_ID_ISSUES,   
REG_ID_ISSUES, 
REMARKS,    
IS_ACTIVE,       
COUNTRY_ID,    
REG_ID_ORG ,
CITY_OF_BIRTH ,
MARITAL_STATUS
   
FROM     
POL_PERSONAL_ACCIDENT_INFO WITH(NOLOCK)  
WHERE     
APPLICANT_ID=@APPLICANT_ID and CUSTOMER_ID=@CUSTOMER_ID and POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID  
END   
ELSE  
 BEGIN   
 Declare @Gen nvarchar(10),
  @REG_ID_ORG NVARCHAR(50)=NULL,
  @PERSONAL_INFO_ID INT=0  ,
  @TPYE int=0,
  @CITY_OF_BIRTH nvarchar(100)
 Select @Gen= CO_APPL_GENDER ,@REG_ID_ORG=ORIGINAL_ISSUE,@TPYE= APPLICANT_TYPE  from CLT_APPLICANT_LIST where APPLICANT_ID=@APPLICANT_ID  
  --if (@Gen is not null  and @Gen<>'')
  -- BEGIN  
  -- if(@Gen='M')  
  -- BEGIN  
  -- set @Gen='6615'  
  -- END  
  --else if(@Gen='F')  
  -- BEGIN  
  -- set @Gen='6614'  
  -- END  
  --END  
 SELECT 
 @PERSONAL_INFO_ID AS PERSONAL_INFO_ID,
 @POLICY_ID AS POLICY_ID,     
 @POLICY_VERSION_ID AS POLICY_VERSION_ID,     
 @CUSTOMER_ID AS CUSTOMER_ID, 
 ISNULL(APPLICANT_ID,0),  
 ISNULL(FIRST_NAME+''+MIDDLE_NAME+''+LAST_NAME,0) AS INDIVIDUAL_NAME,  
 ISNULL(CONTACT_CODE,0) AS CODE, 
 ISNULL(CPF_CNPJ,0) AS CPF_NUM,  
 isnull(STATE,'') AS STATE_ID,    
 isnull(convert(nvarchar(20),CO_APPL_DOB),'') AS DATE_OF_BIRTH,  
( case when isnull(@Gen,'') ='M' then 6615 
 when isnull(@Gen,'') ='F' then 6614 end ) GENDER,  
 REGIONAL_IDENTIFICATION AS REG_IDEN,  
  isnull(convert(nvarchar(20),REG_ID_ISSUE),'') AS REG_ID_ISSUES,  
 ISNULL(NOTE,0) AS REMARKS,
 IS_ACTIVE, 
 ISNULL(COUNTRY,0) as COUNTRY_ID,
 @REG_ID_ORG AS REG_ID_ORG,
 @TPYE as TYPE,
ISNULL( @CITY_OF_BIRTH,'') as CITY_OF_BIRTH,
 TITLE AS POSITION_ID,
 ISNULL(CO_APPL_MARITAL_STATUS,'') AS MARITAL_STATUS
 
 from CLT_APPLICANT_LIST WITH(NOLOCK)  
 where APPLICANT_ID=@APPLICANT_ID and CUSTOMER_ID=@CUSTOMER_ID  
 END  
END
GO

