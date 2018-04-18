IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetProfitCenter]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetProfitCenter]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------  
Proc Name          : Dbo.Proc_GetProfitCenter  
Created by         : Priya  
Date               : 09/05/2005  
Purpose            : To get details from MNT_PROFIT_CENTER_LIST  
Revison History :  
Used In                :   Wolverine  
------------------------------------------------------------  
Date     Review By          Comments  
------   ------------       -------------------------*/  
--DROP PROC Dbo.Proc_GetProfitCenter  
CREATE PROC Dbo.Proc_GetProfitCenter  
(  
@PC_ID smallint  
)  
  
AS  
BEGIN  
SELECT PC_ID,PC_NAME,PC_CODE,PC_ADD1,PC_ADD2,PC_CITY,PC_STATE,PC_ZIP,PC_COUNTRY,PC_PHONE,PC_EXT,PC_FAX,PC_EMAIL,IS_ACTIVE--,CREATED_BY,CREATED_DATETIME,MODIFIED_BY,LAST_UPDATED_DATETIME   
FROM MNT_PROFIT_CENTER_LIST  
WHERE PC_ID = @PC_ID  
  
END  



GO

