IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertPrimaryApplicantInsured]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertPrimaryApplicantInsured]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------
Proc Name          : Dbo.Proc_InsertPrimaryApplicantInsured
Created by           : Mohit Gupta
Date                    : 19/05/2005
Purpose               : 
Revison History :
MODIFIED BY			:pRAVESH K CHANDEL
DATE				: 28 JULY 09
PURPOSE				: UPDATE HOME COVERAGES IF CO -APPLIACANT CHANGES
Used In                :   Wolverine  
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
-- drop proc Proc_InsertPrimaryApplicantInsured
CREATE   PROCEDURE Proc_InsertPrimaryApplicantInsured
(
	@APPLICANT_ID int,
	@CUSTOMER_ID int,
	@APP_ID int,
	@APP_VERSION_ID int,
	@CREATED_BY int,	
	@IS_PRIMARY_APPLICANT int
	
)
AS
BEGIN

DECLARE @LOBID int
select @LOBID = APP_LOB from APP_LIST where  CUSTOMER_ID = @CUSTOMER_ID and APP_ID =  @APP_ID AND APP_VERSION_ID =  @APP_VERSION_ID


INSERT INTO APP_APPLICANT_LIST
(
APPLICANT_ID,
CUSTOMER_ID,
APP_ID,
APP_VERSION_ID,
CREATED_BY,
CREATED_DATETIME,
IS_PRIMARY_APPLICANT
)
VALUES
(
@APPLICANT_ID,
@CUSTOMER_ID,
@APP_ID,
@APP_VERSION_ID,
@CREATED_BY,
GETDATE(),
@IS_PRIMARY_APPLICANT
)

-- ADDED BY PRAVESH TO UPDATE HOME COVGS itrack  6179
IF (@LOBID=1 ) -- AND @IS_PRIMARY_APPLICANT=1)
BEGIN
	EXEC PROC_UPDATE_HOME_COVERAGE_BY_APPLICANT @CUSTOMER_ID,@APPLICANT_ID,@APP_ID,@APP_VERSION_ID
END
-- END HERE


END


GO

