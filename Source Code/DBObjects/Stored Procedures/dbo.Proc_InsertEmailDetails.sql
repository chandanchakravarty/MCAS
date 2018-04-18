IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertEmailDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertEmailDetails]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------      
Proc Name          : Dbo.Proc_InsertEmailDetails      
Created by           : Mohit Gupta      
Date                    : 29/06/2005      
Purpose               :       
Revison History :      
Modify by           : Pravesh Chandel      
Date                    : 07/11/2006      
Purpose               :     Add More parameters  @DIARY_ITEM_REQ,@FOLLOW_UP_DATE ,@POLICY_ID,@APP_ID,@POLICY_VERSION_ID,@CREATED_BY  
  
Used In                :   Wolverine        
drop proc Proc_InsertEmailDetails  
------------------------------------------------------------      
Date     Review By          Comments      
------   ------------       -------------------------*/    
--DROP PROC  dbo.Proc_InsertEmailDetails    
CREATE  PROCEDURE dbo.Proc_InsertEmailDetails      
(      
 @CUSTOMER_ID int,      
 @EMAIL_FROM_NAME nvarchar(100),      
 @EMAIL_FROM nvarchar(100),      
 @EMAIL_TO nvarchar(100),      
 @EMAIL_RECIPIENTS nvarchar(2000),      
 @EMAIL_SUBJECT nvarchar(255),      
 @EMAIL_MESSAGE nvarchar(4000),      
 @EMAIL_ATTACH_PATH nvarchar(255),      
 @Email_Row_Id int output,    
 @Email_Send_Date datetime,    
 @DIARY_ITEM_REQ char(1)=null,  
 @FOLLOW_UP_DATE  datetime=null,  
 @POLICY_ID   int=null,  
 @APP_ID      int=null,  
 @POLICY_VERSION_ID INT = NULL,  
 @CREATED_BY   INT=NULL,  
 @POLICY_NUMBER NVARCHAR(100)=null,  
 @CLAIM_NUMBER NVARCHAR(100)=null,  
 @APP_NUMBER NVARCHAR(100)=null,  
 @QUOTE NVARCHAR(100)=null,
 @DIARY_ITEM_TO INT = NULL     
)      
AS      
BEGIN      
  
INSERT INTO CLT_CUSTOMER_EMAIL      
(      
CUSTOMER_ID,      
EMAIL_FROM_NAME,      
EMAIL_FROM,      
EMAIL_TO,      
EMAIL_RECIPIENTS,      
EMAIL_SUBJECT,      
EMAIL_MESSAGE,      
EMAIL_ATTACH_PATH,    
Email_Send_Date,     
DIARY_ITEM_REQ,  
FOLLOW_UP_DATE,  
POLICY_NUMBER,  
CLAIM_NUMBER,  
APP_NUMBER ,  
QUOTE,
DIARY_ITEM_TO  
)      
VALUES      
(      
@CUSTOMER_ID,      
@EMAIL_FROM_NAME,      
@EMAIL_FROM,      
@EMAIL_TO,      
@EMAIL_RECIPIENTS,      
@EMAIL_SUBJECT,      
@EMAIL_MESSAGE,      
@EMAIL_ATTACH_PATH,    
@Email_Send_Date,  
@DIARY_ITEM_REQ,     
@FOLLOW_UP_DATE,  
@POLICY_NUMBER,  
@CLAIM_NUMBER,  
@APP_NUMBER ,  
@QUOTE,
@DIARY_ITEM_TO  
)      
SELECT @Email_Row_Id= @@IDENTITY      
  
--select * from CLT_CUSTOMER_NOTES where customer_id=1008  
  
DECLARE @TEMP_POLICY_NUMBER char(10)          
 DECLARE @TEMP_UNDERWRITER int          
      
 SELECT @TEMP_POLICY_NUMBER = POLICY_NUMBER, @TEMP_UNDERWRITER = UNDERWRITER          
 FROM POL_CUSTOMER_POLICY_LIST WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID       
        AND POLICY_VERSION_ID=@POLICY_VERSION_ID      
  
  
END    
  
  
  
  
  



GO

