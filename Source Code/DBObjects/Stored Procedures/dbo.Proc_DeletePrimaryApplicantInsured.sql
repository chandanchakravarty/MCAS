IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_DeletePrimaryApplicantInsured]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_DeletePrimaryApplicantInsured]
GO

SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER OFF
GO



/*----------------------------------------------------------
Proc Name          : Dbo.Proc_DeletePrimaryApplicantInsured
Created by           : Mohit Gupta
Date                    : 02/09/2005
Purpose               : 
Revison History :
Used In                :   Wolverine  
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
CREATE   PROCEDURE Proc_DeletePrimaryApplicantInsured
(
	--@APPLICANT_ID int,
	@CUSTOMER_ID int,
	@APP_ID int,
	@APP_VERSION_ID int
)
AS
BEGIN
DELETE FROM  APP_APPLICANT_LIST
WHERE 
	-- APPLICANT_ID=@APPLICANT_ID     AND
	  CUSTOMER_ID=@CUSTOMER_ID    AND 
               APP_ID=@APP_ID                              AND
               APP_VERSION_ID =@APP_VERSION_ID

END


GO

