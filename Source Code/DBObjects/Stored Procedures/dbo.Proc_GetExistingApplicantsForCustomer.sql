IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetExistingApplicantsForCustomer]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetExistingApplicantsForCustomer]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------        
Proc Name      : dbo.Proc_GetExistingApplicantsForCustomer        
Created by       :Sumit Chhabra         
Date             :11/25/2005        
Purpose       : retrieving data from CLT_APPLICANT_LIST        
Revison History :        
Used In        : Wolverine        
------------------------------------------------------------        
Date     Review By          Comments        
------   ------------       -------------------------*/        
-- drop PROC dbo.Proc_GetExistingApplicantsForCustomer      
CREATE PROC [dbo].[Proc_GetExistingApplicantsForCustomer]      
(      
@CUSTOMERID int      
)      
AS        
BEGIN        
	SELECT 
		APPLICANT_ID,CUSTOMER_ID,rtrim(ltrim(isnull(FIRST_NAME,'') + ' ' + isnull(MIDDLE_NAME,'') + ' ' +  isnull(LAST_NAME,''))) AS APPLICANT_NAME,     
		(ADDRESS1 + ' ' + ADDRESS2) AS APPLICANT_ADDRESS,CITY,ISNULL(MNT_COUNTRY_STATE_LIST.STATE_NAME,'') AS  STATE,ZIP_CODE,  
		CASE  IS_PRIMARY_APPLICANT WHEN 1 THEN 'Yes' WHEN 0 THEN 'No' ELSE '' END AS IS_PRIMARY_APPLICANT 
	FROM 
		CLT_APPLICANT_LIST 
	LEFT OUTER JOIN
		MNT_COUNTRY_STATE_LIST
	ON
		CLT_APPLICANT_LIST.STATE = MNT_COUNTRY_STATE_LIST.STATE_ID
	WHERE CUSTOMER_ID=@CUSTOMERID AND CLT_APPLICANT_LIST.IS_ACTIVE='Y'      
END       





GO

