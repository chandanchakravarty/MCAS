IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_GETPOLPERSONALACCIDENTINFO]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_GETPOLPERSONALACCIDENTINFO]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                          
Proc Name       : dbo.[Proc_GetPolPersonalAccidentInfo]                          
Created by      : Chetna Agarwal                        
Date            : 15-04-2010                          
Purpose			: retrieving data from POL_PERSONAL_ACCIDENT_INFO                          
Revison History :                 
Modify by       : PRADEEP KUSHWAHA
Date            : 25-05-2010                         
Purpose			:         
                       
Used In        : Ebix Advantage                      
------------------------------------------------------------                          
Date     Review By          Comments                          
------   ------------       -------------------------*/                          
--drop proc dbo.PROC_GETPOLPERSONALACCIDENTINFO 
 
CREATE PROC [dbo].[PROC_GETPOLPERSONALACCIDENTINFO]    
	@CUSTOMER_ID INT,
	@POLICY_ID INT,
	@POLICY_VERSION_ID SMALLINT,
	@PERSONAL_INFO_ID INT                           
AS 
                                            
BEGIN     
SELECT
	PERSONAL_INFO_ID,
	POLICY_ID, 
	POLICY_VERSION_ID, 
	PPAI.CUSTOMER_ID AS CUSTOMER_ID,
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
	PPAI.IS_ACTIVE AS IS_ACTIVE,
	COUNTRY_ID,
	REG_ID_ORG,
	PPAI.APPLICANT_ID AS APPLICANT_ID ,
	IS_SPOUSE_OR_CHILD,
	MAIN_INSURED,
	APPLICANT_TYPE AS TYPE,
	CITY_OF_BIRTH ,
	ORIGINAL_VERSION_ID,
	MARITAL_STATUS,
	EXCEEDED_PREMIUM
	 
FROM 
	POL_PERSONAL_ACCIDENT_INFO PPAI WITH(NOLOCK) LEFT OUTER JOIN CLT_APPLICANT_LIST CAL WITH(NOLOCK)
	 ON CAL.APPLICANT_ID=PPAI.APPLICANT_ID
WHERE
	PPAI.CUSTOMER_ID= @CUSTOMER_ID AND
	POLICY_ID=@POLICY_ID AND
	POLICY_VERSION_ID=@POLICY_VERSION_ID AND
	PERSONAL_INFO_ID=@PERSONAL_INFO_ID
END
GO

