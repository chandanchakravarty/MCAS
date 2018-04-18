IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_DeleteCLM_INSURED_LOCATION]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_DeleteCLM_INSURED_LOCATION]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------
Proc Name       : dbo.Proc_DeleteCLM_INSURED_LOCATION
Created by      : Vijay Arora
Date            : 5/1/2006
Purpose    	: To delete the record in table named CLM_INSURED_LOCATION
Revison History :
Used In 	: Wolverine
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
CREATE PROC Dbo.Proc_DeleteCLM_INSURED_LOCATION
(
@INSURED_LOCATION_ID int,
@CLAIM_ID int
)
As
BEGIN
IF EXISTS (SELECT INSURED_LOCATION_ID FROM CLM_INSURED_LOCATION WHERE INSURED_LOCATION_ID = @INSURED_LOCATION_ID AND CLAIM_ID = @CLAIM_ID)
	DELETE FROM CLM_INSURED_LOCATION WHERE INSURED_LOCATION_ID=@INSURED_LOCATION_ID AND CLAIM_ID=@CLAIM_ID 
END



GO

