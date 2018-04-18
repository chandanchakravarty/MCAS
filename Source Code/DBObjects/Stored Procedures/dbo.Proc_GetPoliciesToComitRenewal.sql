IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetPoliciesToComitRenewal]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetPoliciesToComitRenewal]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/******************************************************************************************************************
Proc  		: dbo.Proc_GetPoliciesToComitRenewal
Created By 	: Ravindra Gupta
Date 		: 
Purpose		: 
Used in 	: Wolvorine 's EOD process

*********************************************************************************************/
-- drop proc dbo.Proc_GetPoliciesToComitRenewal
CREATE PROC dbo.Proc_GetPoliciesToComitRenewal
AS
BEGIN

DECLARE @POLICY_URENEW nvarchar(15),
	@POLICY_RSUSPENSE nvarchar(15)

SET @POLICY_URENEW='URENEW'
SET @POLICY_RSUSPENSE='RSUSPENSE'


SELECT  CPL.CUSTOMER_ID,CPL.POLICY_ID,CPL.POLICY_VERSION_ID,CPL.POLICY_STATUS,CPL.POLICY_LOB AS LOB_ID
FROM POL_CUSTOMER_POLICY_LIST CPL
INNER JOIN POL_POLICY_PROCESS PPP
ON CPL.CUSTOMER_ID = PPP.CUSTOMER_ID 
AND CPL.POLICY_ID = PPP.POLICY_ID 
AND CPL.POLICY_VERSION_ID = PPP.NEW_POLICY_VERSION_ID
WHERE PPP.PROCESS_ID = 5
AND PPP.PROCESS_STATUS ='PENDING'
AND CPL.POLICY_STATUS IN(@POLICY_URENEW , @POLICY_RSUSPENSE)

ORDER BY  CPL.CUSTOMER_ID,CPL.POLICY_ID,CPL.POLICY_VERSION_ID 
END
















GO

