IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetAllProducers]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetAllProducers]
GO

SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER OFF
GO



/*----------------------------------------------------------
Proc Name          : Dbo.Proc_GetAllProducers
Created by           : Anurag Verma
Date                    : 28/04/2005
Purpose               : To get Producers
Revison History :
Used In                :   Wolverine

------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
CREATE PROC Dbo.Proc_GetAllProducers
AS

BEGIN
	SELECT     UL.USER_ID, UL.USER_FNAME +'  '+  UL.USER_LNAME as Username
	FROM       MNT_USER_TYPES UT INNER JOIN
		   MNT_USER_LIST UL ON UT.USER_TYPE_ID = UL.USER_TYPE_ID
	WHERE     (UT.USER_TYPE_CODE='PRO') AND UL.IS_ACTIVE='Y'
	ORDER BY Username
END


GO

