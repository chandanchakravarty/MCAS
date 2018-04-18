IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetWatercraftAdditionalInterestDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetWatercraftAdditionalInterestDetails]
GO

SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER OFF
GO




/*----------------------------------------------------------
Proc Name          : Dbo.Proc_GetWatercraftAdditionalInterestDetails
Created by           : Anurag Verma
Date                    :  20/05/2005
Purpose               :  Retrieve additional interest from APP_WATERCRAFT_COV_ADD_INT table
Revison History :
Used In                :   Wolverine
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
CREATE  PROC Dbo.Proc_GetWatercraftAdditionalInterestDetails
(
	@CUSTOMER_ID	int,
	@APP_ID		int,
	@APP_VERSION_ID	int,
	@BOAT_ID		int,
	@ADD_INT_ID int	
)
AS

DECLARE @HOLDER_ID1 int

SELECT 	@HOLDER_ID1 = HOLDER_ID
FROM  APP_WATERCRAFT_COV_ADD_INT
WHERE 
	CUSTOMER_ID 	=	@CUSTOMER_ID and
	APP_ID			=	@APP_ID and
	APP_VERSION_ID	=	@APP_VERSION_ID and
	BOAT_ID		=	@BOAT_ID AND
	ADD_INT_ID = @ADD_INT_ID
 
BEGIN
	
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
		FROM  APP_WATERCRAFT_COV_ADD_INT
		WHERE 
			CUSTOMER_ID 	=	@CUSTOMER_ID and
			APP_ID			=	@APP_ID and
			APP_VERSION_ID	=	@APP_VERSION_ID and
			ADD_INT_ID	=	@ADD_INT_ID	 AND	
			BOAT_ID		=	@BOAT_ID 
		
		ORDER BY  CUSTOMER_ID ASC
	END
	ELSE
	BEGIN
		SELECT
			MEMO,
			NATURE_OF_INTEREST,
			RANK,
			LOAN_REF_NUMBER,
			I.IS_ACTIVE,
			L.HOLDER_ID,
			L.HOLDER_NAME,
			L.HOLDER_ADD1,
			L.HOLDER_ADD2,
			L.HOLDER_CITY,
			L.HOLDER_COUNTRY,
			L.HOLDER_STATE,
			L.HOLDER_ZIP
		FROM  APP_WATERCRAFT_COV_ADD_INT I
		INNER JOIN MNT_HOLDER_INTEREST_LIST L ON
			I.HOLDER_ID = L.HOLDER_ID 
		WHERE 
			I.CUSTOMER_ID 	=	@CUSTOMER_ID and
			I.APP_ID			=	@APP_ID and
			I.APP_VERSION_ID	=	@APP_VERSION_ID and
			I.ADD_INT_ID	=	@ADD_INT_ID	 AND	
			I.BOAT_ID		=	@BOAT_ID 	
	END
END



GO

