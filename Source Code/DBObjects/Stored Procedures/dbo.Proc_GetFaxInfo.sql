IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetFaxInfo]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetFaxInfo]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



/*----------------------------------------------------------          
Proc Name       : dbo.Proc_GetFaxInfo  
Created by      : Kumar          
Date            : 08/19/2005          
Purpose       : Return the Query         
Revison History :          
Used In   : Wolverine          
------------------------------------------------------------          
Date     Review By          Comments          
------   ------------       -------------------------*/          
 --drop PROC Dbo.Proc_GetFaxInfo 1009,20   
CREATE PROC Dbo.Proc_GetFaxInfo   
(          
   @Customer_ID  int  ,  
   @RowId int    
)          
AS          
BEGIN    
        
Select t1.FAX_ROW_ID,  
 t1.CUSTOMER_ID,  
 t1.FAX_NUMBER as TO_NUMBER,  
 t1.FAX_FROM_NAME as FROM_NAME,  
 t1.FAX_FROM as FROM_FAX,  
 t1.FAX_TO as [TO],  
 t1.FAX_RECIPIENTS as RECIPIENTS,  
 t1.FAX_SUBJECT as SUBJECT,  
 t1.FAX_BODY as MESSAGE,  
 t1.FAX_ATTACH_PATH as ATTACHMENT,  
 t1.FAX_SEND_DATE,
 t1.DIARY_ITEM_REQ,
 convert(varchar(10),t1.FOLLOW_UP_DATE,101) FOLLOW_UP_DATE,
 t1.DIARY_ITEM_TO
   
From CLT_CUSTOMER_FAX t1   
   where t1.CUSTOMER_ID = @Customer_ID   
 and t1.FAX_ROW_ID=@RowId  
END  
  
  



GO

