IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetAttentionDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetAttentionDetails]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------  
Proc Name          : Dbo.Proc_GetAttentionDetails  
Created by           : Shrikant Bhatt  
Date                    : 20/04/2005  
Purpose               : To get Account Information  from CLT_CUSTOMER_LIST table  
Revison History :  
Used In                :   Wolverine  
------------------------------------------------------------  
Date     Review By          Comments  
------   ------------       -------------------------*/  
--drop PROC Dbo.Proc_GetAttentionDetails
CREATE PROC Dbo.Proc_GetAttentionDetails  
(  
@CustomerID  int  
)  
AS  
BEGIN  
  
SELECT  CUSTOMER_ATTENTION_NOTE ,  
 ATTENTION_NOTE_UPDATED,CUSTOMER_ID,CUSTOMER_CODE,CUSTOMER_FIRST_NAME,CUSTOMER_MIDDLE_NAME,CUSTOMER_LAST_NAME  
FROM  CLT_CUSTOMER_LIST WHERE CUSTOMER_ID  =  @CustomerID ORDER BY  CUSTOMER_ID ASC  
  
END  
  



GO

