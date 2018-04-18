IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetAdditionalInterestDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetAdditionalInterestDetails]
GO

SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER OFF
GO




/*----------------------------------------------------------
Proc Name          : Dbo.Proc_GetAdditionalInterestDetails
Created by           : Shrikant Bhatt
Date                    : 28/04/2005
Purpose               : To get Producer  from MNT_User_LIST table
Revison History :
Used In                :   Wolverine
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
CREATE  PROC Dbo.Proc_GetAdditionalInterestDetails
(
	@CUSTOMER_ID	int,
	@APP_ID		int,
	@APP_VERSION_ID	int,
	@VEHICLE_ID		int,
	@ADD_INT_ID int	
)
AS

DECLARE @HOLDER_ID1 int

SELECT 	@HOLDER_ID1 = HOLDER_ID
FROM  APP_ADD_OTHER_INT
WHERE 
	CUSTOMER_ID 	=	@CUSTOMER_ID and
	APP_ID			=	@APP_ID and
	APP_VERSION_ID	=	@APP_VERSION_ID and
	VEHICLE_ID		=	@VEHICLE_ID AND
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
		FROM  APP_ADD_OTHER_INT
		WHERE 
			CUSTOMER_ID 	=	@CUSTOMER_ID and
			APP_ID			=	@APP_ID and
			APP_VERSION_ID	=	@APP_VERSION_ID and
			ADD_INT_ID	=	@ADD_INT_ID	 AND	
			VEHICLE_ID		=	@VEHICLE_ID 
		
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
		FROM  APP_ADD_OTHER_INT I
		INNER JOIN MNT_HOLDER_INTEREST_LIST L ON
			I.HOLDER_ID = L.HOLDER_ID 
		WHERE 
			I.CUSTOMER_ID 	=	@CUSTOMER_ID and
			I.APP_ID			=	@APP_ID and
			I.APP_VERSION_ID	=	@APP_VERSION_ID and
			I.ADD_INT_ID	=	@ADD_INT_ID	 AND	
			I.VEHICLE_ID		=	@VEHICLE_ID 	
	END
END



GO

