IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdateCustomerNotes]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdateCustomerNotes]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/********************************************************************  
Modify By : Pravesh Chandel  
Date      : 6 Nov 2006  
Purpose   :  Add Two more fields 1.@DIARY_ITEM_REQ  2.@FOLLOW_UP_DATE  
drop proc  Proc_UpdateCustomerNotes                        
*********************************************************************/  
  
CREATE  PROC Dbo.Proc_UpdateCustomerNotes                        
(  
   @TO_FOLLOWUP_ID int=null,                      
   @CUSTOMER_ID  int,                
   @NOTES_SUBJECT  nvarchar(255),                     
   @NOTES_TYPE       int,            
   @POLICY_ID  smallint,      
   @POLICY_VER_TRACKING_ID smallint,            
   @CLAIMS_ID  int,            
   @NOTES_DESC  text,            
   @VISIBLE_TO_AGENCY nvarchar(1),            
   @MODIFIED_BY  int,            
   @LAST_UPDATED_DATETIME datetime,           
   @NOTEUPID   int       ,    
   @QQ_APP_POL NVARCHAR(10),  
   @DIARY_ITEM_REQ  char(1),  
   @FOLLOW_UP_DATE  datetime=null              
)                        
AS                        
BEGIN       
    UPDATE CLT_CUSTOMER_NOTES 
	 SET 
	  TO_FOLLOWUP_ID =@TO_FOLLOWUP_ID,      
	  CUSTOMER_ID   = @CUSTOMER_ID,        
	  NOTES_SUBJECT =  @NOTES_SUBJECT ,         
	  NOTES_TYPE =   @NOTES_TYPE ,        
	  POLICY_ID = @POLICY_ID,      
	  POLICY_VER_TRACKING_ID =@POLICY_VER_TRACKING_ID,           
	  CLAIMS_ID = @CLAIMS_ID,              
	  NOTES_DESC = @NOTES_DESC,            
	  VISIBLE_TO_AGENCY = @VISIBLE_TO_AGENCY,            
	  MODIFIED_BY = @MODIFIED_BY,          
	  LAST_UPDATED_DATETIME = @LAST_UPDATED_DATETIME  ,    
	  QQ_APP_POL = @QQ_APP_POL,  
	  DIARY_ITEM_REQ = @DIARY_ITEM_REQ ,  
	  FOLLOW_UP_DATE = @FOLLOW_UP_DATE              
   
   WHERE NOTES_ID = @NOTEUPID        
END      
    
  






GO

