IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetAgencyCustomerDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetAgencyCustomerDetails]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



/*----------------------------------------------------------
Proc Name          : Dbo.Proc_GetCustomerDetails
Created by           : Shrikant Bhatt
Date                    : 19/05/2005
Purpose               : To all customer related to the any Agency 
Revison History :
Used In                :   Wolverine*/

CREATE PROC Dbo.Proc_GetAgencyCustomerDetails
(
@AGENCY_ID 	int
)
AS
BEGIN

SELECT   CUSTOMER_ID,
ISNULL(CUSTOMER_FIRST_NAME,'') + ' ' +
ISNULL(CUSTOMER_MIDDLE_NAME,'')+ ' ' +
ISNULL(CUSTOMER_LAST_NAME,'') as CustName

FROM  CLT_CUSTOMER_LIST 

WHERE customer_AGENCY_ID  = @AGENCY_ID   
ORDER BY  2 ASC   -- second column is CustName



END





GO

