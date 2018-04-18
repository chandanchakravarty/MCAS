IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_DeleteCLM_OCCURRENCE_DETAIL]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_DeleteCLM_OCCURRENCE_DETAIL]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------
Proc Name       : dbo.Proc_DeleteCLM_OCCURRENCE_DETAIL
Created by      : Vijay Arora
Date            : 5/3/2006
Purpose    	: To Delete the record from table named CLM_OCCURRENCE_DETAIL
Revison History :
Used In 	      : Wolverine
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
CREATE PROC Dbo.Proc_DeleteCLM_OCCURRENCE_DETAIL
(
@OCCURRENCE_DETAIL_ID int,
@CLAIM_ID int)
As
Begin
DELETE FROM CLM_OCCURRENCE_DETAIL WHERE OCCURRENCE_DETAIL_ID=@OCCURRENCE_DETAIL_ID AND CLAIM_ID=@CLAIM_ID 
END



GO

