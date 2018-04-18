IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetEmailInfo]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetEmailInfo]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------          
Proc Name       : dbo.Proc_GetEmailInfo  
Created by      : Gaurav          
Date            : 07/19/2005          
Purpose       : Return the Query         
Revison History :          
Used In   : Wolverine          
------------------------------------------------------------          
Date     Review By          Comments          
------   ------------       -------------------------*/      
-- drop proc Proc_GetEmailInfo      
CREATE PROC Dbo.Proc_GetEmailInfo   
(          
   @Customer_ID  int  ,  
   @RowId int    
)          
AS          
BEGIN          
Select t1.EMAIL_FROM as FROM_EMAIL ,t1.EMAIL_TO as [TO],t1.EMAIL_ROW_ID,t1.CUSTOMER_ID,  
t1.EMAIL_FROM_NAME as  FROM_NAME,t1.EMAIL_RECIPIENTS as RECIPIENTS,  
t1.EMAIL_ATTACH_PATH,  
t1.DIARY_ITEM_REQ as DIARY_ITEM_REQ,  
t1.EMAIL_SUBJECT as EMAIL_SUBJECT,  
t1.EMAIL_MESSAGE as EMAIL_MESSAGE,  
t1.POLICY_NUMBER as POLICY_NUMBER,  
t1.APP_NUMBER as APP_NUMBER,  
t1.QUOTE as QUOTE,  
CONVERT(VARCHAR(20),T1.FOLLOW_UP_DATE,101) as FOLLOW_UP_DATE,   
DIARY_ITEM_TO
From CLT_CUSTOMER_EMAIL t1   
  where t1.CUSTOMER_ID=@Customer_ID and t1.EMAIL_ROW_ID=@RowId  
END  
  
  
  
  
  
  
  
  
  
  
  



GO

