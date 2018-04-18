IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_FetchContactList]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_FetchContactList]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Charles Gomes
-- Create date: 17-Mar-10
-- Description:	Fetch Contact List From MNT_CONTACT_LIST

--drop proc Proc_FetchContactList 28232
-- =============================================
CREATE PROCEDURE [dbo].[Proc_FetchContactList]  
@CUSTOMER_ID INT = NULL
AS
BEGIN	
	SET NOCOUNT ON	    
	
	SELECT CONTACT_ID, CONTACT_FNAME + ' ' + CONTACT_MNAME + ' ' + CONTACT_LNAME AS CONTACT_NAME,INDIVIDUAL_CONTACT_ID AS CUSTOMER_ID
	FROM MNT_CONTACT_LIST WITH(NOLOCK)
	WHERE ISNULL(IS_ACTIVE,'N') = 'Y' AND INDIVIDUAL_CONTACT_ID = ISNULL(@CUSTOMER_ID,INDIVIDUAL_CONTACT_ID)
	ORDER BY CONTACT_NAME
END
GO

