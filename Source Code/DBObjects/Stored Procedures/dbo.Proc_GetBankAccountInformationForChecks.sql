IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetBankAccountInformationForChecks]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetBankAccountInformationForChecks]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



/*----------------------------------------------------------
Proc Name       : dbo.Proc_GetBankAccountInformationForChecks
Created by      : Ajit Singh Chahal
Date            : 7/14/2005
Purpose    	  :to print pdf checks
Revison History :
Used In 	      : Wolverine
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
CREATE PROC Dbo.Proc_GetBankAccountInformationForChecks
(
@ACCOUNT_ID int
)
AS
BEGIN

declare @START_CHECK_NUMBER int,
@END_CHECK_NUMBER int


	select @END_CHECK_NUMBER=END_CHECK_NUMBER from ACT_BANK_INFORMATION where ACCOUNT_ID = @ACCOUNT_ID
			
	select @START_CHECK_NUMBER=max(CHECK_NUMBER)+1 from  ACT_CHECK_INFORMATION where 	ACCOUNT_ID = @ACCOUNT_ID  
	if(@START_CHECK_NUMBER is null)
		select @START_CHECK_NUMBER=START_CHECK_NUMBER from ACT_BANK_INFORMATION where ACCOUNT_ID = @ACCOUNT_ID
	
	select @START_CHECK_NUMBER as START_CHECK_NUMBER, @END_CHECK_NUMBER as END_CHECK_NUMBER

END













GO

