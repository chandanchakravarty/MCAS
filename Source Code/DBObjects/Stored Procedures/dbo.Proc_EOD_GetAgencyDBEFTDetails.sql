IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_EOD_GetAgencyDBEFTDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_EOD_GetAgencyDBEFTDetails]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------  
Proc Name        : dbo.Proc_EOD_GetAgencyDBEFTDetails
Created by       : Ravinda Gupta 
Date             : 2-27-2007
Purpose      	 : Will fetch 
Revison History :  
Used In   :Wolverine  
------------------------------------------------------------  
Date     Review By          Comments  
------------------------------------------------------------*/  
-- drop proc dbo.Proc_EOD_GetAgencyDBEFTDetails
CREATE PROC dbo.Proc_EOD_GetAgencyDBEFTDetails
(
	@EFT_SPOOL_ID Int
)
AS
BEGIN 
	SELECT IDEN_ROW_ID,POLICY_ID,POLICY_VERSION_ID,CUSTOMER_ID,
	POLICY_NUMBER,AGENCY_ID,AMOUNT,DATE_CREATED,DATE_COMMITTED,
	REF_DEPOSIT_ID,	REF_DEPOSIT_LINE_ITEM_ID
	FROM ACT_CUSTOMER_PAYMENTS_FROM_AGENCY
	WHERE REF_EFT_SPOOL_ID = @EFT_SPOOL_ID

END



GO

