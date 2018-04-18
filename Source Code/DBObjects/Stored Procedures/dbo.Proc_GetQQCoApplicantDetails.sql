IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetQQCoApplicantDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetQQCoApplicantDetails]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--begin tran
--drop proc [dbo].[Proc_GetQQCoApplicantDetails]  
--
--go

/*----------------------------------------------------------  
Proc Name       : Proc_GetQQCoApplicantDetails  901
Created by      : Deepak  
Date            : 10/10/2005  
Purpose     : Evaluation  
Revison History :  
Modified by      : Praveen Kasana  
Date            : 18/April/2008  
Purpose			: At QQ this proc is used to set the Operator Details at QQ.If Name Contains some Invalid Char..then 
				  Error reported while Loading the QQ.hence removing the Invalid Chars(<,>,&)  

Used In  : Wolverine  
------------------------------------------------------------  
Date     Review By          Comments  
------   ------------       -------------------------*/  
--drop proc Proc_GetQQCoApplicantDetails 1400
create PROC [dbo].[Proc_GetQQCoApplicantDetails]  
(  
 @CUSTOMER_ID      int  
)  
AS  
DECLARE @STRSQLWHERECLAUSE NUMERIC  
DECLARE @COUNT NUMERIC  
BEGIN  
 IF EXISTS (SELECT 1 FROM CLT_CUSTOMER_LIST WHERE CUSTOMER_ID = @CUSTOMER_ID AND CUSTOMER_TYPE='11109')  
  SELECT @STRSQLWHERECLAUSE = 0  
 ELSE  
  SELECT @STRSQLWHERECLAUSE = 1  
  
/*  
 SELECT @COUNT=COUNT(*) FROM CLT_APPLICANT_LIST WHERE IS_ACTIVE='Y' AND CUSTOMER_ID=@CUSTOMER_ID  AND ISNULL(IS_PRIMARY_APPLICANT,0) = @STRSQLWHERECLAUSE  
 IF (@COUNT>0)  
 BEGIN  
  SELECT TOP 1 FIRST_NAME FNAME,MIDDLE_NAME MNAME,LAST_NAME LNAME,CONVERT(VARCHAR,CO_APPL_DOB,101) DOB,CASE CO_APPL_MARITAL_STATUS WHEN 'M' THEN 'Married' ELSE 'Single' END MARITALSTATUS FROM CLT_APPLICANT_LIST WHERE IS_ACTIVE='Y' AND CUSTOMER_ID=@CUSTOME



R_ID AND ISNULL(IS_PRIMARY_APPLICANT,0) = @STRSQLWHERECLAUSE  
 END  
 ELSE  
 BEGIN  
  IF (@STRSQLWHERECLAUSE=0)  
  BEGIN  
   SELECT CUSTOMER_FIRST_NAME FNAME,'' MNAME,CUSTOMER_FIRST_NAME LNAME,CONVERT(VARCHAR,DATE_OF_BIRTH,101) DOB,CASE MARITAL_STATUS WHEN 'M' THEN 'Married' ELSE 'Single' END MARITALSTATUS FROM CLT_CUSTOMER_LIST WHERE CUSTOMER_ID=@CUSTOMER_ID  
  END  
  ELSE  
  BEGIN  
   SELECT CUSTOMER_FIRST_NAME FNAME,CUSTOMER_MIDDLE_NAME MNAME,CUSTOMER_LAST_NAME LNAME,CONVERT(VARCHAR,DATE_OF_BIRTH,101) DOB,CASE MARITAL_STATUS WHEN 'M' THEN 'Married' ELSE 'Single' END MARITALSTATUS FROM CLT_CUSTOMER_LIST WHERE CUSTOMER_ID=@CUSTOMER_I



D  
  
  END  
 END  
*/  
 SELECT @COUNT=COUNT(*) FROM CLT_APPLICANT_LIST WHERE IS_ACTIVE='Y' AND CUSTOMER_ID=@CUSTOMER_ID   
 IF (@COUNT>0)  
 BEGIN  
 SELECT 
replace(replace(replace(replace(ISNULL(FIRST_NAME,''),'<',''),'>',''),'&',''),'"','D673GSUYD7G3J73UDD') FNAME,
replace(replace(replace(ISNULL(MIDDLE_NAME,''),'<',''),'>',''),'&','') MNAME,
replace(replace(replace(ISNULL(LAST_NAME,''),'<',''),'>',''),'&','') LNAME,
CONVERT(VARCHAR,CO_APPL_DOB,101) DOB,
CASE CO_APPL_MARITAL_STATUS 
WHEN 'M' THEN 'Married' 
WHEN 'D' THEN 'Divorced' 
WHEN 'P' THEN 'Separated' 
WHEN 'W' THEN 'Widowed' 
ELSE 'Single' END MARITALSTATUS ,
CASE CO_APPL_GENDER WHEN 'M' THEN 'Male' ELSE 'Female' END GENDER
FROM CLT_APPLICANT_LIST 
WHERE IS_ACTIVE='Y' AND CUSTOMER_ID=@CUSTOMER_ID ORDER BY ISNULL(IS_PRIMARY_APPLICANT,0) desc,FIRST_NAME,MIDDLE_NAME,LAST_NAME asc--AND ISNULL(IS_PRIMARY_APPLICANT,0) = @STRSQLWHERECLAUSE  
 END  
 ELSE  
 BEGIN  
  IF (@STRSQLWHERECLAUSE=0)  
  BEGIN  
   SELECT 
replace(replace(replace(ISNULL(CUSTOMER_FIRST_NAME,''),'<',''),'>',''),'&','') FNAME,
'' MNAME,
replace(replace(replace(ISNULL(CUSTOMER_LAST_NAME,''),'<',''),'>',''),'&','') LNAME,
CONVERT(VARCHAR,DATE_OF_BIRTH,101) DOB,CASE MARITAL_STATUS 
WHEN 'M' THEN 'Married' 
WHEN 'D' THEN 'Divorced' 
WHEN 'P' THEN 'Separated' 
WHEN 'W' THEN 'Widowed' 
ELSE 'Single'END MARITALSTATUS,
CASE GENDER WHEN 'M' THEN 'Male' ELSE 'Female' END GENDER
FROM CLT_CUSTOMER_LIST WHERE CUSTOMER_ID=@CUSTOMER_ID  
  END  
  ELSE  
  BEGIN  
   SELECT 
replace(replace(replace(ISNULL(CUSTOMER_FIRST_NAME,''),'<',''),'>',''),'&','') FNAME,
replace(replace(replace(ISNULL(CUSTOMER_MIDDLE_NAME,''),'<',''),'>',''),'&','') MNAME,
replace(replace(replace(ISNULL(CUSTOMER_LAST_NAME,''),'<',''),'>',''),'&','') LNAME,
CONVERT(VARCHAR,DATE_OF_BIRTH,101) DOB,CASE MARITAL_STATUS
WHEN 'M' THEN 'Married' 
WHEN 'D' THEN 'Divorced' 
WHEN 'P' THEN 'Separated' 
WHEN 'W' THEN 'Widowed' 
ELSE 'Single' END MARITALSTATUS ,
CASE GENDER WHEN 'M' THEN 'Male' ELSE 'Female' END GENDER
FROM CLT_CUSTOMER_LIST WHERE CUSTOMER_ID=@CUSTOMER_ID  
  
  END  
 END  
  
   
END  


--go
--
--exec Proc_GetQQCoApplicantDetails 1400
--
--rollback tran  
  
  
  
  







GO

