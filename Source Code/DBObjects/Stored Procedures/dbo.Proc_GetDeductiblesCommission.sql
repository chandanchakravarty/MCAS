IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetDeductiblesCommission]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetDeductiblesCommission]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER OFF
GO



/*----------------------------------------------------------
Proc Name          : Dbo.Proc_GetDeductiblesCommission
Created by         : Priya
Date               : 22/05/2005
Purpose            : To get details from APP_GENERAL_DEDUCTIBLES_COMMISSION
Revison History :
Used In                :   Wolverine
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
CREATE  PROC Dbo.Proc_GetDeductiblesCommission
(
	@CUSTOMER_ID int,
	@APP_ID int,
	@APP_VERSION_ID	smallint
      
)

AS
BEGIN
	SELECT CUSTOMER_ID, APP_ID, APP_VERSION_ID,DEDUCTIBLES_ID,
               BODILY_INJURY_DEDUCTIBLE_AMOUNT,PREMISES_PREMIUM,TOTAL_ACCOUNT_PREMIUM,
		COMMISSION_PERCENT,COMMISSION_AMOUNT
	              
		
	FROM APP_GENERAL_DEDUCTIBLES_COMMISSION
	WHERE CUSTOMER_ID = @CUSTOMER_ID AND
		APP_ID = @APP_ID AND
		APP_VERSION_ID = @APP_VERSION_ID

END


GO

