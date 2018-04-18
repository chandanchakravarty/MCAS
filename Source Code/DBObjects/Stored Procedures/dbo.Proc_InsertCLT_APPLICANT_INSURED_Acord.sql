IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertCLT_APPLICANT_INSURED_Acord]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertCLT_APPLICANT_INSURED_Acord]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- drop  PROCEDURE [dbo].[Proc_InsertCLT_APPLICANT_INSURED] 
CREATE  PROCEDURE [dbo].[Proc_InsertCLT_APPLICANT_INSURED_Acord]          
(          
 @APPLICANT_ID    int output,          
 @CUSTOMER_ID   int,          
 @TITLE     varchar(10),          
 @SUFFIX     varchar(5),          
 @FIRST_NAME     varchar(100),          
 @MIDDLE_NAME     varchar(10),          
 @LAST_NAME     varchar(25),          
 @ADDRESS1     varchar(150),          
 @ADDRESS2     varchar(150),          
 @CITY     varchar(70),          
 @COUNTRY     varchar(10),          
 @STATE     varchar(10),    
          
 @ZIP_CODE     varchar(20),          
 @PHONE     varchar(20),    
-- Mobile, Business_ph, ext included by swastika on 21st Mar'06 for Gen Iss# 2367    
 @MOBILE nvarchar(20),    
 @BUSINESS_PHONE nvarchar(20),    
 @EXT nvarchar(5),          
 @EMAIL     varchar(50),          
 @IS_ACTIVE nchar(1),          
 @CREATED_BY int,          
 @CO_APPLI_OCCU nvarchar(25),          
 @CO_APPLI_EMPL_NAME nvarchar(75),          
 @CO_APPLI_EMPL_ADDRESS nvarchar(75),    
 @CO_APPLI_EMPL_ADDRESS1 nvarchar(75),    
 -- <START> Fields included by swastika on 10th Apr'06 for Gen Iss# 2367    
 @CO_APPLI_EMPL_CITY     varchar(70),          
 @CO_APPLI_EMPL_COUNTRY     varchar(10),          
 @CO_APPLI_EMPL_STATE     varchar(10),    
 @CO_APPLI_EMPL_ZIP_CODE     varchar(12),          
 @CO_APPLI_EMPL_PHONE     varchar(20),     
 @CO_APPLI_EMPL_EMAIL     varchar(50),   
  
 -- <START> Fields included by Neeraj Singh on 10th Apr'06 for Gen Iss# 09   
 @CO_APPL_RELATIONSHIP varchar(25),   
 @CO_APPL_GENDER varchar(20),   
--<END>    
 @CO_APPLI_YEARS_WITH_CURR_EMPL real,          
 @CO_APPL_YEAR_CURR_OCCU real,          
 @CO_APPL_MARITAL_STATUS nchar(1),          
 @CO_APPL_DOB datetime,          
 @CO_APPL_SSN_NO nvarchar(44),        
 @DESC_CO_APPLI_OCCU varchar(200),    
 @APPLICATION_FLAG int=0           
)          
AS          
BEGIN          
select @APPLICANT_ID=isnull(Max(APPLICANT_ID),0)+1 from CLT_APPLICANT_LIST     
     
CREATE TABLE #TEMPAPPOCC
(
	[LOOKUP_VALUE_DESC] NVARCHAR(100),
	[LOOKUP_UNIQUE_ID] INT
)  
INSERT INTO #TEMPAPPOCC
EXECUTE Proc_GetLookupDescFromAcordCodes '%OCC',@CO_APPLI_OCCU 
IF ((SELECT COUNT(*) FROM #TEMPAPPOCC) > 0)
BEGIN
	SELECT @CO_APPLI_OCCU= CONVERT(NVARCHAR(100),LOOKUP_UNIQUE_ID) FROM #TEMPAPPOCC
END
ELSE
BEGIN
	SET @CO_APPLI_OCCU=NULL
END
DROP TABLE #TEMPAPPOCC    
               
INSERT INTO CLT_APPLICANT_LIST          
(          
APPLICANT_ID,CUSTOMER_ID,TITLE,SUFFIX,FIRST_NAME,MIDDLE_NAME,          
LAST_NAME,ADDRESS1,ADDRESS2,CITY,COUNTRY,STATE,ZIP_CODE,PHONE,MOBILE,BUSINESS_PHONE,EXT,EMAIL,IS_ACTIVE,CREATED_BY,CREATED_DATETIME,          
CO_APPLI_OCCU, CO_APPLI_EMPL_NAME ,CO_APPLI_EMPL_ADDRESS,CO_APPLI_EMPL_ADDRESS1,CO_APPLI_EMPL_CITY,CO_APPLI_EMPL_COUNTRY,CO_APPLI_EMPL_STATE,CO_APPLI_EMPL_ZIP_CODE,    
CO_APPLI_EMPL_PHONE,CO_APPLI_EMPL_EMAIL,CO_APPLI_YEARS_WITH_CURR_EMPL,CO_APPL_YEAR_CURR_OCCU,          
CO_APPL_MARITAL_STATUS,CO_APPL_DOB,CO_APPL_SSN_NO,IS_PRIMARY_APPLICANT,DESC_CO_APPLI_OCCU,CO_APPL_RELATIONSHIP,CO_APPL_GENDER          
)          
VALUES          
(          
@APPLICANT_ID,@CUSTOMER_ID,@TITLE,@SUFFIX,@FIRST_NAME,@MIDDLE_NAME,@LAST_NAME,@ADDRESS1,@ADDRESS2,@CITY,@COUNTRY,@STATE,@ZIP_CODE,          
@PHONE,@MOBILE,@BUSINESS_PHONE,@EXT,@EMAIL,@IS_ACTIVE,@CREATED_BY,GETDATE(),@CO_APPLI_OCCU, @CO_APPLI_EMPL_NAME ,@CO_APPLI_EMPL_ADDRESS ,@CO_APPLI_EMPL_ADDRESS1,@CO_APPLI_EMPL_CITY,    
@CO_APPLI_EMPL_COUNTRY,@CO_APPLI_EMPL_STATE,@CO_APPLI_EMPL_ZIP_CODE,@CO_APPLI_EMPL_PHONE,@CO_APPLI_EMPL_EMAIL,@CO_APPLI_YEARS_WITH_CURR_EMPL,          
@CO_APPL_YEAR_CURR_OCCU,@CO_APPL_MARITAL_STATUS,@CO_APPL_DOB,@CO_APPL_SSN_NO,0,@DESC_CO_APPLI_OCCU,@CO_APPL_RELATIONSHIP,@CO_APPL_GENDER          
)          
END          
    
    
    

GO

