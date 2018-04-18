IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetUserLastVisitedPages]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetUserLastVisitedPages]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

 /*----------------------------------------------------------    
Proc Name       : dbo.Proc_GetUserLastVisitedPages    
Created by      : Sibin   
Date            : 3/18/2009    
Purpose         : To get User Last Visited Pages    
Revison History :    
Used In         :   Wolverine    
------------------------------------------------------------    
Date     Review By          Comments    
------   ------------       -------------------------*/    
 -- drop proc Proc_GetUserLastVisitedPages    
CREATE PROC dbo.Proc_GetUserLastVisitedPages    
(    
  @USER_ID int,  
  @USER_SYSTEM_ID varchar(8) = null   
)  
AS  
BEGIN    
  
SELECT   
ISNULL(ITEM.LAST_VISITED_CUSTOMER,'') AS LAST_VISITED_CUSTOMER,  
ISNULL(ITEM.LAST_VISITED_APPLICATION,'') AS LAST_VISITED_APPLICATION,  
ISNULL(ITEM.LAST_VISITED_POLICY,'') AS LAST_VISITED_POLICY,  
ISNULL(ITEM.LAST_VISITED_CLAIM,'') AS LAST_VISITED_CLAIM,    
ISNULL(ITEM.LAST_VISITED_QUOTE,'') AS LAST_VISITED_QUOTE  
FROM LAST_VISITED_ITEMS ITEM   
WHERE ITEM.[USER_ID] = @USER_ID  
and ITEM.USER_SYSTEM_ID = @USER_SYSTEM_ID  
    
END  
  
  
  
  
  
  
  


GO

