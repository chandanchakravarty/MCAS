IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[FetchPoliciesForUT]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[FetchPoliciesForUT]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author	   : Praveen Kasana
-- Create date : 31-Dec-09
-- Description : FETCHING POLICIES UNDERWRITING_TIER --PROCESS UT FROM PRIOR POLICY / PRIOR LOSS
---This proc will be called from Prior Loss / Prior Policy
-- =============================================
--drop proc FetchPoliciesForUT
CREATE PROC dbo.FetchPoliciesForUT
(
@CUSTOMER_ID INT
)
AS
BEGIN
----------------------------
DECLARE @UREWRITE	INT  
SET @UREWRITE = 31

DECLARE @URENEW INT
SET @URENEW = 5

DECLARE @UISSUE INT
SET @UISSUE = 24

---------------UNDER PROCESS POLICIES
 SELECT 
POL.CUSTOMER_ID AS CUSTOMER_ID
,POL.POLICY_ID AS ID
,POL.POLICY_VERSION_ID AS VERSION_ID 
,'POLICY' AS [LEVEL] 
				FROM POL_CUSTOMER_POLICY_LIST POL WITH(NOLOCK)
				INNER JOIN POL_POLICY_PROCESS PPP WITH(NOLOCK)
				ON PPP.CUSTOMER_ID=POL.CUSTOMER_ID AND 
				PPP.POLICY_ID=POL.POLICY_ID AND PPP.NEW_POLICY_VERSION_ID=POL.POLICY_VERSION_ID 
				AND PPP.PROCESS_STATUS!='ROLLBACK'
				AND PROCESS_ID IN (@UREWRITE,@URENEW,@UISSUE)
				WHERE 
				POL.CUSTOMER_ID = @CUSTOMER_ID
				AND STATE_ID = 14 AND POLICY_LOB='2' AND ISNULL(IS_ACTIVE,'N') = 'Y'

UNION------------SUSPENDED POLICIES

SELECT 
POL.CUSTOMER_ID AS CUSTOMER_ID
,POL.POLICY_ID AS ID
,POL.POLICY_VERSION_ID AS VERSION_ID 
,'POLICY' AS [LEVEL] 
				FROM POL_CUSTOMER_POLICY_LIST POL WITH(NOLOCK)
				WHERE ISNULL(POL.POLICY_STATUS,'')= 'SUSPENDED'
				AND POL.CUSTOMER_ID = @CUSTOMER_ID
				AND STATE_ID = 14 AND POLICY_LOB='2' AND ISNULL(IS_ACTIVE,'N') = 'Y'

UNION ----------INCOMPLETE POLICIES

SELECT 
CUSTOMER_ID AS CUSTOMER_ID,
APP_ID AS ID
,APP_VERSION_ID AS VERSION_ID
,'APP' AS [LEVEL] 
FROM APP_LIST WITH(NOLOCK) WHERE 
CUSTOMER_ID = @CUSTOMER_ID AND UPPER(APP_STATUS) IN ('INCOMPLETE')
AND STATE_ID = 14 AND APP_LOB='2' AND ISNULL(IS_ACTIVE,'N') = 'Y'

END 




--select policy_status,* from POL_CUSTOMER_POLICY_LIST where policy_number='AR100089'
--select * from pol_policy_process where customer_id = 2030 and policy_id=1 
--select * from pol_process_master where process_id = 5 --URENEW
--select * from pol_process_master where process_id = 31 --URENEW --SCANCEL
--
--
--
--select distinct policy_current_status,* from POL_POLICY_PROCESS where 
--policy_current_status is not null
--and policy_current_status <> ''
--and PROCESS_STATUS!='ROLLBACK' and 
--policy_current_status IN ('SUSPENDED','URENEW','UREWRITE','UISSUE')
--
--select * from pol_process_master where process_id in (24,25)
			

GO

