IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_CheckCustomerIsActive]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_CheckCustomerIsActive]
GO

SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER OFF
GO


/*----------------------------------------------------------
Proc Name          : Dbo.Proc_CheckCustomerIsActive
Created by           : Mohit Gupta
Date                    : 13/10/2005
Purpose               : 
Revison History :
Used In                :   Wolverine  
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
CREATE   PROCEDURE Proc_CheckCustomerIsActive
(
	@Cust_Id int,
	@isActiveCust varchar(2) output
)
AS
BEGIN
SELECT @isActiveCust=IS_ACTIVE FROM CLT_CUSTOMER_LIST WHERE CUSTOMER_ID=@Cust_Id
END

GO

