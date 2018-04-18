IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetQuickQuoteStatus]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetQuickQuoteStatus]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



/*----------------------------------------------------------  
Proc Name       : Proc_GetQuickQuoteStatus  
Created by      : Praveen Kasana
Date            : 9/18/2006  
Purpose         :Get the Status of QQ (Activate/Deactivate)
Revison History :  
Used In        : Wolverine  
------------------------------------------------------------  
Date     Review By          Comments  
------   ------------       -------------------------*/  
CREATE PROC Proc_GetQuickQuoteStatus  
(  
 @CUSTOMER_ID     int,  
 @QQ_ID     int  
)  
AS  
BEGIN  
 SELECT IS_ACTIVE FROM  CLT_QUICKQUOTE_LIST  
 WHERE  CUSTOMER_ID = @CUSTOMER_ID  
  AND QQ_ID=@QQ_ID  
END  
  
  
  
  
  




GO

