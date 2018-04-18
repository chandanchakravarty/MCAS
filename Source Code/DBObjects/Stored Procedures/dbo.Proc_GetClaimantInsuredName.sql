IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetClaimantInsuredName]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetClaimantInsuredName]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                                    
Proc Name       : dbo.Proc_GetClaimantInsuredName
Created by      : Amar Singh                         
Date            : 10/05/2006                                    
Purpose         : Procedure to get Claimant/Insured name
		  Attached with a Claim/Policy
Revison History :                                    
Used In        : Wolverine                                    
------------------------------------------------------------                                    
Date     Review By          Comments                                    
------   ------------       -------------------------*/      
create PROCEDURE [dbo].[Proc_GetClaimantInsuredName](
@CLAIM_ID INT
)
As
Begin
	DECLARE @CUSTOMER_NAME VARCHAR(100), 
		@ADDRESS1 VARCHAR(150), 
		@ADDRESS2 VARCHAR(150), 
		@CITY VARCHAR(70),
		@STATE VARCHAR(10), 
		@COUNTRY VARCHAR(10), 
		@ZIPCODE VARCHAR(12), 
		@PHONE VARCHAR(20), 
		@EMAIL VARCHAR(50), 
		@CLAIMANT_NAME VARCHAR(100);


	SELECT @CUSTOMER_NAME = ISNULL(c.FIRST_NAME ,'')+ CASE ISNULL(C.MIDDLE_NAME,'') WHEN '' THEN '' ELSE + ' '+ C.MIDDLE_NAME  END +' '+ISNULL(c.LAST_NAME,''),
		@ADDRESS1=c.ADDRESS1,@ADDRESS2=c.ADDRESS2, @CITY=c.CITY, @STATE=c.STATE,
		@COUNTRY = c.COUNTRY,
		@ZIPCODE = c.ZIP_CODE,
		@PHONE = c.PHONE,
		@EMAIL = c.EMAIL
	FROM
					POL_APPLICANT_LIST a INNER JOIN
					Clm_Claim_Info b on (a.CUSTOMER_ID = B.CUSTOMER_ID AND A.POLICY_ID = B.POLICY_ID
					AND A.POLICY_VERSION_ID = B.POLICY_VERSION_ID)
					inner join CLT_APPLICANT_LIST c on a.CUSTOMER_ID = c.CUSTOMER_ID AND
					a.APPLICANT_ID = c.APPLICANT_ID
					Where B.Claim_ID = @CLAIM_ID AND A.IS_PRIMARY_APPLICANT=1;


	SELECT @CLAIMANT_NAME = CLAIMANT_NAME FROM CLM_CLAIM_INFO WHERE CLAIM_ID = @CLAIM_ID;

	SELECT 
	ISNULL(@CUSTOMER_NAME,'') 	As CUSTOMER_NAME, 
	ISNULL(@ADDRESS1,'') 		AS CUSTOMER_ADDRESS1,
	ISNULL(@ADDRESS2,'')	 AS CUSTOMER_ADDRESS2, 
	ISNULL(@CITY,'') 	AS CUSTOMER_CITY, 
	ISNULL(@STATE,'') 	AS CUSTOMER_STATE,
	ISNULL(@COUNTRY,'') 	AS CUSTOMER_COUNTRY,
	ISNULL(@ZIPCODE,'') 	AS ZIPCODE,
	ISNULL(@PHONE,'') 	AS CUSTOMER_PHONE,
	ISNULL(@EMAIL,'') 	AS CUSTOMER_EMAIL,
	ISNULL(@CLAIMANT_NAME,'') 	AS CLAIMANT_NAME;
End




GO

