IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetCustomerNotes]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetCustomerNotes]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

 /*  
modify by: Pravesh  
Date     :  6 Nov 2006  
Purpose  : To Get More Fieds i.e  DIARY_ITEM_REQ,FOLLOW_UP_DATE  
  
*/  
--drop proc Proc_GetCustomerNotes          
CREATE PROC [dbo].[Proc_GetCustomerNotes]          
(          
  @Note_Id       int,      
  @Customer_Id  int      
)          
AS          
BEGIN          
 SELECT  NOTES_ID, CUSTOMER_ID,  NOTES_SUBJECT,  NOTES_TYPE,   POLICY_ID policy_id1,              
        CLAIMS_ID, NOTES_DESC, VISIBLE_TO_AGENCY, CREATED_BY, CREATED_DATETIME  ,MODIFIED_BY,LAST_UPDATED_DATETIME      
 ,convert(varchar(10),POLICY_ID) + '-' + convert(varchar(10),POLICY_VER_TRACKING_ID)  + '-'  + QQ_APP_POL POLICY_ID,      
 DIARY_ITEM_REQ,FOLLOW_UP_DATE        
   FROM CLT_CUSTOMER_NOTES    with (NoLock)    
 WHERE        
 NOTES_ID=@Note_Id  AND CUSTOMER_ID = @Customer_Id      
END        
        
         
      
      
      
      
    
  
  
GO

