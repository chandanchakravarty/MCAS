IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetUnderwriterForApplication]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetUnderwriterForApplication]
GO

SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER OFF
GO



/*----------------------------------------------------------
Proc Name          : Proc_GetUnderwriterForApplication
Created by           : Nidhi
Date                    : 29/06/2005
Purpose               : To get the underwriter for application
Revison History :
Used In                :   Wolverine
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
CREATE PROC Proc_GetUnderwriterForApplication
AS
BEGIN
SELECT  top 1   MNT_USER_LIST.USER_ID
FROM         MNT_USER_TYPES INNER JOIN
                      MNT_USER_LIST ON MNT_USER_TYPES.USER_TYPE_ID = MNT_USER_LIST.USER_TYPE_ID
WHERE     (MNT_USER_TYPES.USER_TYPE_CODE = 'UWT')
END


GO

