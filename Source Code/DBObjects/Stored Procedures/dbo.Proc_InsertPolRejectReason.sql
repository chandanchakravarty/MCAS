IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertPolRejectReason]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertPolRejectReason]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

  /*----------------------------------------------------------            
Proc Name       : dbo.POL_POLICY_REJECTION            
Created by      : Pradeep kushwaha   
Date            : 07/07/2010            
Purpose			: Insert Policy reject reason record
Revison History :            
Used In			: Ebix Advantage web   (Brazil)        
------------------------------------------------------------            
Date     Review By          Comments            
------   ------------       -------------------------*/            
--DROP PROC dbo.Proc_InsertPolRejectReason     
  
CREATE PROC [dbo].[Proc_InsertPolRejectReason]   
(        
 @CUSTOMER_ID INT,  
 @POLICY_ID INT ,  
 @POLICY_VERSION_ID SMALLINT,  
 @REJECT_REASON_ID SMALLINT OUT,  
 @REASON_TYPE_ID INT,
 @REASON_DESC NVARCHAR(4000)=NULL,
 @IS_ACTIVE NCHAR(1)=NULL,     
 @CREATED_BY INT=NULL,  
 @CREATED_DATETIME DATETIME=NULL 
)            
AS            
BEGIN  

SELECT  @REJECT_REASON_ID=ISNULL(MAX(REJECT_REASON_ID),0)+1 FROM POL_POLICY_REJECTION  WITH (NOLOCK) 
WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID       
      
INSERT INTO POL_POLICY_REJECTION            
(            
	CUSTOMER_ID,POLICY_ID,POLICY_VERSION_ID,
	REJECT_REASON_ID,REASON_TYPE_ID,REASON_DESC,
	IS_ACTIVE,CREATED_BY,CREATED_DATETIME
)            
VALUES            
(            
	@CUSTOMER_ID,@POLICY_ID,@POLICY_VERSION_ID,
	@REJECT_REASON_ID,@REASON_TYPE_ID,@REASON_DESC,
	@IS_ACTIVE,@CREATED_BY,@CREATED_DATETIME 
)           
  
END  
GO

