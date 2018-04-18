IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetAgencyIDFromInfo]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetAgencyIDFromInfo]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



/*----------------------------------------------------------  
Proc Name          : Dbo.Proc_GetAgencyIDFromInfo  
Created by           : Pradeep  
Date                    : 05/05/2005  
Purpose               : To get Account Information  from MNT_AGENCY_LIST table  
Revison History :  
Used In                :   Wolverine  
------------------------------------------------------------  
Date     Review By          Comments  
------   ------------       -------------------------*/  
CREATE  PROC Dbo.Proc_GetAgencyIDFromInfo  
(  
  
 @AGENCY_DISPLAY_NAME NVarChar(75),  
 @AGENCY_ID smallint output  
)  
  
AS  
BEGIN  
  
SELECT @AGENCY_ID = AGENCY_ID
 FROM MNT_AGENCY_LIST   
WHERE AGENCY_DISPLAY_NAME = @AGENCY_DISPLAY_NAME  
  
END  
  




GO

