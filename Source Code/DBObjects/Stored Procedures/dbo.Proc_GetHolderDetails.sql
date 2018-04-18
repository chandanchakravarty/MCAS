IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetHolderDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetHolderDetails]
GO

SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER OFF
GO



/*----------------------------------------------------------
Proc Name          : Dbo.Proc_GetAccountExecutive
Created by           : Shrikant Bhatt
Date                    : 20/04/2005
Purpose               : To get Producer  from MNT_User_LIST table
Revison History :
Used In                :   Wolverine
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
CREATE PROC Dbo.Proc_GetHolderDetails
(
@HolderID 	varchar(4)
)
AS
BEGIN

SELECT HOLDER_ID,HOLDER_NAME,HOLDER_CODE,HOLDER_ADD1,HOLDER_ADD2,HOLDER_CITY,HOLDER_COUNTRY,HOLDER_STATE,HOLDER_ZIP,
HOLDER_MAIN_PHONE_NO,HOLDER_FAX FROM  MNT_HOLDER_INTEREST_LIST WHERE HOLDER_ID =  @HolderID ORDER BY  HOLDER_ID ASC

END


GO

