IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetAutoIDCardDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetAutoIDCardDetails]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
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
CREATE PROC Dbo.Proc_GetAutoIDCardDetails
(
@CUSTOMER_ID	int,
@APP_ID		int,
@APP_VERSION_ID	int,
@VEHICLE_ID		int
)
AS
BEGIN

SELECT
AUTO_CARD_ID,
CUSTOMER_ID,
APP_ID	,
APP_VERSION_ID,
VEHICLE_ID,
convert(varchar,ID_EFFECTIVE_DATE,101) AS ID_EFFECTIVE_DATE,
convert(varchar,ID_EXPITATION_DATE,101) AS ID_EXPITATION_DATE,
NAME_TYPE,
NAME_ID,
A_NAME AS NAME,
ADDRESS1,
ADDRESS2,
CITY,
STATE,
ZIP,
EMAIL,
NAME_PRINT,
STATE_TYPE,
IS_ACTIVE,
isnull(SPECIAL_WORD_FRONT,'') as SPECIAL_WORD_FRONT,
isnull(SPECIAL_WORD_BACK,'') as SPECIAL_WORD_BACK,
CREATED_BY,
CREATED_DATETIME,
MODIFIED_BY,
LAST_UPDATED_DATETIME


FROM  APP_AUTO_ID_CARD_INFO
WHERE 
CUSTOMER_ID 	=	@CUSTOMER_ID and
APP_ID			=	@APP_ID and
APP_VERSION_ID	=	@APP_VERSION_ID and

VEHICLE_ID		=	@VEHICLE_ID 
ORDER BY  CUSTOMER_ID ASC

END








GO

