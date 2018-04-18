IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_SUSEP_APPLICANT_INSURED_BENEFICIARY_INFO_CADASTRO]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_SUSEP_APPLICANT_INSURED_BENEFICIARY_INFO_CADASTRO]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                                                                    
Proc Name       : dbo.PROC_APPLICANT_INSURED_BENEFICIARY_INFO_CADASTRO                                                              
Created by      : ANKIT KUMAR GOEL                                                                
Date            : 25/03/2011                                                                    
Purpose         : The purpose is to GENEARATE REPORT 
				  APPLICANT INSURED BENEFICIARY INFO CADASTRO
Revison History :                                             
------------------------------------------------------------                                                                    
Modified By  :           
Date   :          
Purpose  :          
------------------------------------------------------------                                                                          
Date     Review By          Comments                                                                    
------   ------------       -------------------------*/ 

 
--DROP PROC PROC_SUSEP_APPLICANT_INSURED_BENEFICIARY_INFO_CADASTRO
CREATE PROC PROC_SUSEP_APPLICANT_INSURED_BENEFICIARY_INFO_CADASTRO --'03-FEB-2011'
@REF_DATE DATETIME=NULL,  
@POLICY_NUMBER VARCHAR(22)=NULL 
AS
BEGIN
	SELECT 
	---------RECORD_SEQUENCE-----------
	 --CAL.APPLICANT_ID AS  SEQUENCE,
	 RIGHT('0000000000'+ CAST((ROW_NUMBER() OVER (ORDER BY CAL.APPLICANT_ID)) AS VARCHAR),10) AS  SEQUENCE,
	---------SUSEP_INSURER_CODE-------
	'05045' AS COD_CIA,
	
	-------BASE_MONTH------------------
	CAST(YEAR(CAL.CREATED_DATETIME) AS VARCHAR)+ RIGHT('00'+CAST(MONTH(CAL.CREATED_DATETIME) AS VARCHAR),2) AS DT_BASE,
	
	---------ROLE_TYPE-----------------	
	CASE WHEN PAL.IS_PRIMARY_APPLICANT = 1 THEN '2' --INSURED
	     ELSE '1' END AS TIPO_PES,	    --APPLICANT
	
	-----------PERSON_NAME-------------
	RTRIM(CAL.FIRST_NAME+' '+
	 CASE WHEN  len(ISNULL(CAL.MIDDLE_NAME,''))<1  THEN ''						
			ELSE CAL.MIDDLE_NAME +' '   
			END  +  ISNULL(CAL.LAST_NAME,'')) AS NOME_SUBSC,
	
	SUBSTRING((RTRIM(CAL.FIRST_NAME+' '+
			CASE WHEN  len(ISNULL(CAL.MIDDLE_NAME,''))<1  THEN ''						
			ELSE CAL.MIDDLE_NAME +' '   
			END  +  ISNULL(CAL.LAST_NAME,''))),1,30) AS  NOME_SUBSC,
	
	--------------INCOME_TAX_ID--------
	CAST(CAL.CPF_CNPJ AS VARCHAR(14)) AS CPF_CNPJ,
	
	-------------[ADDRESS]-------------
	SUBSTRING(CAL.ADDRESS1 +''+
	CAL.ADDRESS2,1,30) AS ENDERECO,
	
	-------------NEIGHBORHOOD----------
	SUBSTRING(CAL.DISTRICT,1,30) AS BAIRRO,
	
	-----------CITY-----------	
	SUBSTRING(CAL.CITY,1,30) AS CIDADE,
	
	-----------STATE ABBREVIATION------
	CAST(MCSL.STATE_CODE AS VARCHAR(2)) AS UF,
	
	---------ZIP CODE----------------
	CAST(CAL.ZIP_CODE AS VARCHAR(8)) AS ZIP_CODE,
	
	------COUNTRY--------------
	CAST(MCL.COUNTRY_NAME AS VARCHAR(20)) AS COUNTRY	
	  FROM CLT_APPLICANT_LIST CAL WITH(NOLOCK)
	INNER JOIN POL_APPLICANT_LIST PAL WITH(NOLOCK)
		ON CAL.CUSTOMER_ID = PAL.CUSTOMER_ID
		AND CAL.APPLICANT_ID = PAL.APPLICANT_ID   
	INNER JOIN POL_CUSTOMER_POLICY_LIST PCPL
		ON PCPL.CUSTOMER_ID = PAL.CUSTOMER_ID
		AND PCPL.POLICY_ID = PAL.POLICY_ID
		AND PCPL.POLICY_VERSION_ID = PAL.POLICY_VERSION_ID 
	INNER JOIN MNT_COUNTRY_STATE_LIST MCSL WITH(NOLOCK) --MNT_COUNTRY_LIST MCL	
		ON CAL.[STATE] = MCSL.STATE_ID	 
	INNER JOIN MNT_COUNTRY_LIST MCL WITH(NOLOCK) 	 
		ON MCL.COUNTRY_ID = CAL.COUNTRY	 
	 WHERE CAST(YEAR(CAL.CREATED_DATETIME) AS VARCHAR)+ CAST(MONTH(CAL.CREATED_DATETIME) AS VARCHAR)
	  = CAST(YEAR(@REF_DATE) AS VARCHAR)+ CAST(MONTH(@REF_DATE) AS VARCHAR)
		AND (PCPL.POLICY_NUMBER=@POLICY_NUMBER OR @POLICY_NUMBER IS NULL)
	  
END
 

GO

