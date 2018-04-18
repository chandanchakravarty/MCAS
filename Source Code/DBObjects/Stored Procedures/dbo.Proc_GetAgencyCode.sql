IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetAgencyCode]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetAgencyCode]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



/*----------------------------------------------------------
Proc Name          : Dbo.Proc_GetAgencyCode
Created by           : Gaurav
Date                    : 07/15/2005
Purpose               : To get Account Information  from MNT_AGENCY_LIST table
Revison History :
Used In                :   Wolverine
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
CREATE  PROC Dbo.Proc_GetAgencyCode
(

	@AGENCY_ID int
)

AS
BEGIN

SELECT isnull(AGENCY_CODE,'') as AGENCY_CODE
 FROM MNT_AGENCY_LIST 
WHERE AGENCY_ID =@AGENCY_ID

END





GO

