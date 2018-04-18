IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_DelACT_BANK_RECONCILIATION]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_DelACT_BANK_RECONCILIATION]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------
Proc Name       : dbo.ACT_BANK_RECONCILIATION
Created by      : Ajit Singh Chahal
Date            : 7/13/2005
Purpose    	  :Evaluation
Revison History :
Used In 	      : Wolverine
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
-- drop proc dbo.Proc_DelACT_BANK_RECONCILIATION
CREATE PROC [dbo].[Proc_DelACT_BANK_RECONCILIATION]
(
	@AC_RECONCILIATION_ID int)
As
BEGIN
	
	IF  EXISTS ( SELECT AC_RECONCILIATION_ID 
		FROM ACT_BANK_RECONCILIATION 
		WHERE AC_RECONCILIATION_ID = @AC_RECONCILIATION_ID AND ISNULL(IS_COMMITED,'N') = 'N' )

    BEGIN
		DELETE FROM ACT_BANK_RECON_CHECK_FILE WHERE RECON_GROUP_ID=@AC_RECONCILIATION_ID
		DELETE FROM ACT_BANK_RECON_UPLOAD_FILE WHERE AC_RECONCILIATION_ID=@AC_RECONCILIATION_ID
		DELETE FROM ACT_BANK_RECONCILIATION_ITEMS_DETAILS WHERE AC_RECONCILIATION_ID=@AC_RECONCILIATION_ID
		DELETE FROM ACT_BANK_RECONCILIATION WHERE AC_RECONCILIATION_ID=@AC_RECONCILIATION_ID and isnull(is_commited,'N')='N'

		--Ravindra(07-04-2008): Delete distribution details too
		DELETE FROM ACT_DISTRIBUTION_DETAILS WHERE GROUP_TYPE = 'BRN' AND GROUP_ID = @AC_RECONCILIATION_ID
    END
 

END














GO

