IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetTrailerAdditionalInterestDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetTrailerAdditionalInterestDetails]
GO

SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER OFF
GO




/*----------------------------------------------------------
Proc Name          : Dbo.Proc_GetTrailerAdditionalInterestDetails
Created by           : Anurag Verma
Date                    :  20/05/2005
Purpose               :  Retrieve additional interest from APP_TRAILER_ADD_INT table
Revison History :
Used In                :   Wolverine
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
CREATE  PROC Dbo.Proc_GetTrailerAdditionalInterestDetails
(
	@CUSTOMER_ID	int,
	@APP_ID		int,
	@APP_VERSION_ID	int,
	@TRAILER_ID		int,
	@ADD_INT_ID		INT
)
AS
BEGIN

	DECLARE @HOLDER_ID1 int

	SELECT 	@HOLDER_ID1 = HOLDER_ID
	FROM  APP_WATERCRAFT_TRAILER_ADD_INT
	WHERE 
		CUSTOMER_ID 	=	@CUSTOMER_ID and
		APP_ID			=	@APP_ID and
		APP_VERSION_ID	=	@APP_VERSION_ID and
		TRAILER_ID		=	@TRAILER_ID and
		ADD_INT_ID = @ADD_INT_ID
	
	IF ( @HOLDER_ID1 IS NULL )
	BEGIN
		SELECT
		MEMO,
		NATURE_OF_INTEREST,
		RANK,
		LOAN_REF_NUMBER,
		IS_ACTIVE,
		HOLDER_ID,
		HOLDER_NAME,
		HOLDER_ADD1,
		HOLDER_ADD2,
		HOLDER_CITY,
		HOLDER_COUNTRY,
		HOLDER_STATE,
		HOLDER_ZIP
		
		FROM  APP_WATERCRAFT_TRAILER_ADD_INT
		WHERE 
		CUSTOMER_ID 	=	@CUSTOMER_ID and
		APP_ID			=	@APP_ID and
		APP_VERSION_ID	=	@APP_VERSION_ID and
		ADD_INT_ID	=	@ADD_INT_ID	 AND	
		TRAILER_ID		=	@TRAILER_ID 
		
	END
	ELSE
	BEGIN
		SELECT
			I.MEMO,
			I.NATURE_OF_INTEREST,
			I.RANK,
			I.LOAN_REF_NUMBER,
			I.IS_ACTIVE,
			L.HOLDER_ID,
			L.HOLDER_NAME,
			L.HOLDER_ADD1,
			L.HOLDER_ADD2,
			L.HOLDER_CITY,
			L.HOLDER_COUNTRY,
			L.HOLDER_STATE,
			L.HOLDER_ZIP
		FROM  APP_WATERCRAFT_TRAILER_ADD_INT I
		INNER JOIN MNT_HOLDER_INTEREST_LIST L ON
			I.HOLDER_ID = L.HOLDER_ID 
		WHERE 
			I.CUSTOMER_ID 	=	@CUSTOMER_ID and
			I.APP_ID			=	@APP_ID and
			I.APP_VERSION_ID	=	@APP_VERSION_ID and
			I.ADD_INT_ID	=	@ADD_INT_ID	 AND	
			I.TRAILER_ID		=	@TRAILER_ID 	
	END

END



GO

