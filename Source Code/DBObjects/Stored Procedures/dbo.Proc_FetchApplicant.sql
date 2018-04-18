IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_FetchApplicant]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_FetchApplicant]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------            
Proc Name       : dbo.Proc_FetchApplicant            
Created by      : Chetna Agarwal  
Date            : 15/04/2010            
Purpose			:To FETCH applicants
Revison History :            
Used In			: Ebix Advantage            
------------------------------------------------------------            
Date     Review By          Comments            
------   ------------       -------------------------*/            
--DROP PROC dbo.Proc_FetchApplicant 531,1,2126 

CREATE PROC [dbo].[Proc_FetchApplicant]
(
@POLICY_ID INT,
@POLICY_VERSION_ID INT,
@CUSTOMER_ID INT
)
AS

declare @lob_id int=null
select @lob_id=POLICY_LOB from POL_CUSTOMER_POLICY_LIST where CUSTOMER_ID = @CUSTOMER_ID 
 and policy_id =@POLICY_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID 

if(@lob_id IN (15))--(,21,33,34)) by praveer panghal - itrack 1408 only for Individual Personal Accident 
BEGIN


SELECT POL_APPLICANT_LIST.APPLICANT_ID,
ISNULL(FIRST_NAME,'')+' '+ISNULL(MIDDLE_NAME,'')+' '+ISNULL(LAST_NAME,'') As APPLICANT_NAME,
POL_APPLICANT_LIST.IS_PRIMARY_APPLICANT as IS_PRIMARY_APPLICANT -- changes by praveer for itrack no 900
FROM CLT_APPLICANT_LIST  WITH (NOLOCK) 
inner JOIN POL_APPLICANT_LIST WITH (NOLOCK)
ON
CLT_APPLICANT_LIST.APPLICANT_ID=POL_APPLICANT_LIST.APPLICANT_ID
WHERE 
POL_APPLICANT_LIST.CUSTOMER_ID = @CUSTOMER_ID AND APPLICANT_TYPE='11110'
 and POL_APPLICANT_LIST.policy_id =@POLICY_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID 
END

else 

BEGIN


SELECT POL_APPLICANT_LIST.APPLICANT_ID,
ISNULL(FIRST_NAME,'')+' '+ISNULL(MIDDLE_NAME,'')+' '+ISNULL(LAST_NAME,'') As APPLICANT_NAME,
POL_APPLICANT_LIST.IS_PRIMARY_APPLICANT as IS_PRIMARY_APPLICANT --changes by praveer for itrack no 900
FROM CLT_APPLICANT_LIST  WITH (NOLOCK) 
inner JOIN POL_APPLICANT_LIST WITH (NOLOCK)
ON
CLT_APPLICANT_LIST.APPLICANT_ID=POL_APPLICANT_LIST.APPLICANT_ID
WHERE 
POL_APPLICANT_LIST.CUSTOMER_ID = @CUSTOMER_ID 
 and POL_APPLICANT_LIST.policy_id =@POLICY_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID 
END








GO

