IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_DeleteCLM_INSURED_VEHICLE]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_DeleteCLM_INSURED_VEHICLE]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



/*----------------------------------------------------------
Proc Name       : Proc_DeleteCLM_INSURED_VEHICLE
Created by      : Amar
Date            : 5/1/2006
Purpose    	  :Evaluation
Revison History :
Used In 	      : Wolverine
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
CREATE PROC Proc_DeleteCLM_INSURED_VEHICLE
(
@INSURED_VEHICLE_ID int,
@CLAIM_ID int)
As
Begin
DELETE FROM CLM_INSURED_VEHICLE WHERE INSURED_VEHICLE_ID=@INSURED_VEHICLE_ID AND CLAIM_ID=@CLAIM_ID 
END






GO

