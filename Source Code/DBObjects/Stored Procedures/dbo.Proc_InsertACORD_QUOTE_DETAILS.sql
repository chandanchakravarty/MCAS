IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertACORD_QUOTE_DETAILS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertACORD_QUOTE_DETAILS]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
   
/*=================================================================================        
 Proc Name       : dbo.Proc_InsertACORD_QUOTE_DETAILS                
 Created by      : Ashwani              
 Date            : 8 Dec. 06          
 Purpose         :To insert records in ACORD_QUOTE_DETAILS table.                
 Revison History :                      
 Used In         : Wolverine                
======== =============  ===========================================================        
 Date     Review By          Comments                
================================================================================*/        
--drop proc dbo.Proc_InsertACORD_QUOTE_DETAILS                
CREATE PROC dbo.Proc_InsertACORD_QUOTE_DETAILS        
(         
              
 @INSURANCE_SVC_RQ varchar(100) ,        
 @AGENCY_ID INT ,  
 @ACORD_XML TEXT,        
 @QQ_XML TEXT,       
 @PREMIUM_XML TEXT =NULL ,        
 @CREATED_DATETIME  DATETIME =NULL                
)                
AS                
BEGIN                
 IF EXISTS (SELECT INSURANCE_SVC_RQ FROM ACORD_QUOTE_DETAILS WHERE INSURANCE_SVC_RQ=@INSURANCE_SVC_RQ)        
 BEGIN         
  UPDATE ACORD_QUOTE_DETAILS        
   SET ACORD_XML =@ACORD_XML ,QQ_XML =@QQ_XML ,PREMIUM_XML = @PREMIUM_XML ,CREATED_DATETIME =@CREATED_DATETIME          
  WHERE INSURANCE_SVC_RQ=@INSURANCE_SVC_RQ        
 END         
 ELSE        
 BEGIN         
  INSERT INTO ACORD_QUOTE_DETAILS                
  (         
   INSURANCE_SVC_RQ,        
   AGENCY_ID,ACORD_XML,QQ_XML,PREMIUM_XML,CREATED_DATETIME         
  )                
  VALUES                
  (         
   @INSURANCE_SVC_RQ,        
   @AGENCY_ID,@ACORD_XML,@QQ_XML,@PREMIUM_XML,@CREATED_DATETIME               
  )             
 END            
END           
        
      
    
  
  
  



GO

