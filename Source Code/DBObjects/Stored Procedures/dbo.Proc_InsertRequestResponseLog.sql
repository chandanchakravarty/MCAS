IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertRequestResponseLog]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertRequestResponseLog]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------        
Proc Name            	: Dbo.Proc_InsertRequestResponseLog  
Created by             	: Vijay Arora      
Date                    : 21-03-2006      
Purpose                 :         
Revison History   :        
Used In                 :   Wolverine          
------------------------------------------------------------        
Date     Review By          Comments        
------   ------------       -------------------------*/        
-- DROP PROC Proc_InsertRequestResponseLog
CREATE PROCEDURE Proc_InsertRequestResponseLog        
(  
 @CUSTOMER_ID INT = NULL,  
 @POLICY_ID INT = NULL,  
 @POLICY_VERSION_ID SMALLINT = NULL,  
 @CATEGORY_ID INT = NULL,  
 @SERVICE_VENDOR VARCHAR(50) = NULL,  
 @CREATED_BY INT  = NULL,  
 @REQUEST_DATETIME DATETIME = NULL,    
 @RESPONSE_DATETIME DATETIME  = NULL,
 @IIX_REQUEST text = NULL,
 @IIX_RESPONSE text = NULL,
 @ROW_ID INT OUTPUT  
)  
AS        
BEGIN        
  
INSERT INTO MNT_REQUEST_RESPONSE_LOG   
(  
 CUSTOMER_ID,  
 POLICY_ID,  
 POLICY_VERSION_ID,  
 CATEGORY_ID,  
 SERVICE_VENDOR,  
 CREATED_BY,  
 REQUEST_DATETIME,    
 RESPONSE_DATETIME,
 IIX_REQUEST,
 IIX_RESPONSE   
)  
VALUES  
(  
 @CUSTOMER_ID,  
 @POLICY_ID,  
 @POLICY_VERSION_ID,  
 @CATEGORY_ID,  
 @SERVICE_VENDOR,  
 @CREATED_BY,  
 @REQUEST_DATETIME,    
 @RESPONSE_DATETIME,
 @IIX_REQUEST,
 @IIX_RESPONSE   
)  
 
SET  @ROW_ID = @@IDENTITY 
      
END      
      
      
      
    
  
  
  
  
  
  
  
  
  
  
  
  





GO

