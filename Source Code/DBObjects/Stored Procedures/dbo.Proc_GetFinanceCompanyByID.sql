IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetFinanceCompanyByID]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetFinanceCompanyByID]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




/*----------------------------------------------------------
Proc Name       : dbo.Proc_GetFinanceCompanyByID
Created by      : Pradeep
Date            : 02/05/2005
Purpose         : To retrive a single record 
		  from Finance company
Revison History :
Used In         :   Wolverine
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/

CREATE   PROCEDURE Proc_GetFinanceCompanyByID
(
	@COMPANY_ID Int
)

As

SELECT 
	COMPANY_ID, 		
	COMPANY_NAME, 		
	COMPANY_CODE, 		
	COMPANY_ADD1, 		
	COMPANY_ADD2 ,		
	COMPANY_CITY ,		
	COMPANY_COUNTRY ,	
	COMPANY_STATE 	,
	COMPANY_ZIP 		,
	COMPANY_MAIN_PHONE_NO,
	COMPANY_TOLL_FREE_NO, 
	COMPANY_EXT, 		
	COMPANY_FAX ,		
	COMPANY_EMAIL, 	
	COMPANY_WEBSITE, 	
	COMPANY_MOBILE, 	
	COMPANY_TERMS, 	
	COMPANY_TERMS_DESC, 	
	COMPANY_NOTE ,		
	IS_ACTIVE, 		
	CREATED_BY, 		
	CREATED_DATETIME, 	
	MODIFIED_BY , 	
	LAST_UPDATED_DATETIME
FROM MNT_FINANCE_COMPANY_LIST
WHERE COMPANY_ID = @COMPANY_ID	
 	 






GO

