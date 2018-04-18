IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertFaxDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertFaxDetails]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


---------------------  
  
/*----------------------------------------------------------    
Proc Name          : Dbo.Proc_InsertFaxDetails    
Created by           : Ashish     
Date                    : 08/19/2005    
Purpose               :     
Revison History :    
Used In                :   Wolverine      
------------------------------------------------------------    
Date     Review By          Comments    
------   ------------       -------------------------*/  
--drop PROC dbo.Proc_InsertFaxDetails  
CREATE  PROCEDURE dbo.Proc_InsertFaxDetails    
(    
 @CUSTOMER_ID int,   
 @FAX_NUMBER nvarchar(40),  
 @FAX_FROM_NAME nvarchar(100),    
 @FAX_FROM nvarchar(100),    
 @FAX_TO nvarchar(100),    
 @FAX_RECIPIENTS nvarchar(2000),    
 @FAX_SUBJECT nvarchar(255),    
 @FAX_REFERENCE nvarchar(200),  
 @FAX_BODY nvarchar(4000),    
 @FAX_ATTACH_PATH nvarchar(255),    
 @FAX_SEND_DATE datetime,  
 @FAX_RETURN_CODE nvarchar(8),
 @DIARY_ITEM_REQ char,
 @FOLLOW_UP_DATE datetime,
 @DIARY_ITEM_TO int,  
 @FAX_Row_Id int output  
)    
AS    
BEGIN    
    
INSERT INTO CLT_CUSTOMER_FAX    
(    
CUSTOMER_ID,  
FAX_NUMBER,  
FAX_FROM_NAME,  
FAX_FROM,  
FAX_TO,  
FAX_RECIPIENTS,  
FAX_SUBJECT,  
FAX_REFERENCE,  
FAX_BODY,  
FAX_ATTACH_PATH,  
FAX_SEND_DATE,  
FAX_RETURN_CODE,
DIARY_ITEM_REQ,
FOLLOW_UP_DATE,
DIARY_ITEM_TO
 
)    
VALUES    
(    
 @CUSTOMER_ID,   
 @FAX_NUMBER,  
 @FAX_FROM_NAME,    
 @FAX_FROM,    
 @FAX_TO,    
 @FAX_RECIPIENTS,    
 @FAX_SUBJECT,    
 @FAX_REFERENCE,  
 @FAX_BODY,    
 @FAX_ATTACH_PATH,    
 @FAX_SEND_DATE,  
 @FAX_RETURN_CODE,
 @DIARY_ITEM_REQ,
 @FOLLOW_UP_DATE,
 @DIARY_ITEM_TO
   
)  
  
SELECT @FAX_Row_Id= @@IDENTITY    
END    
  



GO

