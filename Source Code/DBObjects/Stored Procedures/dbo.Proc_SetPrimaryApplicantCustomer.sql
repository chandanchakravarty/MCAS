IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_SetPrimaryApplicantCustomer]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_SetPrimaryApplicantCustomer]
GO

SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER OFF
GO


/*----------------------------------------------------------
Proc Name          : Dbo.Proc_SetPrimaryApplicantCustomer
Created by           : Mohit Gupta
Date                    : 06/10/2005
Purpose               : 
Revison History :
Used In                :   Wolverine  
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
CREATE   PROCEDURE Proc_SetPrimaryApplicantCustomer
(
	@CUSTOMER_ID int,
	@APPLICANT_ID int	
)
AS
BEGIN

             -- Setting IS_PRIMARY_APPLICANT to 0 for  all applicants of customer. 
	UPDATE CLT_APPLICANT_LIST 
                         SET IS_PRIMARY_APPLICANT=0 
                         WHERE CUSTOMER_ID=@CUSTOMER_ID

	 -- Setting IS_PRIMARY_APPLICANT to 1 for the applicant of customer. 
             UPDATE CLT_APPLICANT_LIST
                        SET IS_PRIMARY_APPLICANT=1 
                        WHERE  CUSTOMER_ID=@CUSTOMER_ID AND APPLICANT_ID=@APPLICANT_ID

END

GO

