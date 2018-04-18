IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetCustomerInfoforAccounts]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetCustomerInfoforAccounts]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------
Proc Name       : dbo.Proc_GetCustomerInfoforAccounts
Created by      : praveen kasana
Date            : 11/12/2006
Purpose    	  :To get customer records for accounts information.
Revison History :
Used In 	      : Wolverine

------   ------------       -------------------------*/
CREATE PROC dbo.Proc_GetCustomerInfoforAccounts

AS
BEGIN
	SELECT ISNULL(CUSTOMER_ID,0) AS CUSTOMER_ID,ISNULL(CUSTOMER_FIRST_NAME,'') +  ' ' + ISNULL(CUSTOMER_LAST_NAME,'') AS CUSTOMER_NAME 
	FROM CLT_CUSTOMER_LIST WITH(NOLOCK) WHERE IS_ACTIVE='Y' ORDER BY CUSTOMER_FIRST_NAME
END







GO

