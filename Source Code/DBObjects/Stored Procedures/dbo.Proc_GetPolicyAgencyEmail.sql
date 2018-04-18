IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetPolicyAgencyEmail]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetPolicyAgencyEmail]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROC dbo.Proc_GetPolicyAgencyEmail
(
@CUSTOMER_ID INT,
@POLICY_ID INT,
@POLICY_VERSION_ID INT
)
AS
BEGIN
SELECT POL.AGENCY_ID,ISNULL(MNT.AGENCY_DISPLAY_NAME,'') AGENCY_DISPLAY_NAME
 ,isnull(MNT.AGENCY_FAX,'') AGENCY_FAX,isnull(MNT.AGENCY_EMAIL,'') as AGENCY_EMAIL
 FROM POL_CUSTOMER_POLICY_LIST POL
LEFT JOIN MNT_AGENCY_LIST MNT ON POL.AGENCY_ID = MNT.AGENCY_ID
WHERE POL.POLICY_ID =@POLICY_ID  AND POLICY_VERSION_ID =@POLICY_VERSION_ID AND CUSTOMER_ID = @CUSTOMER_ID
END

GO

