IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdateClaimReceiptDocument]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdateClaimReceiptDocument]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                                                        
Proc Name             : Dbo.[Proc_UpdateClaimReceiptDocument]                                                        
Created by            : Santosh Kumar Gautam                                 
Date                  : 17 June 2011                             
Purpose               :          
Revison History       :                                                        
Used In               :           
------------------------------------------------------------                                                        
Date     Review By          Comments                           
      
drop Proc [Proc_UpdateClaimReceiptDocument]  27995,1,1                                       
------   ------------       -------------------------*/          
        
CREATE PROC [dbo].[Proc_UpdateClaimReceiptDocument]                
  (      
     @CLAIM_ID INT,      
	 @ACTIVITY_ID INT,      
	 @PAYEE_ID INT,      
	 @DOC_TEXT TEXT,      
	 @PROCESS_TYPE VARCHAR(20)      
   )              
AS                                                                                          
BEGIN       
  
  
  UPDATE  CLM_PROCESS_LOG
  SET     DOC_TEXT     = @DOC_TEXT
  WHERE   CLAIM_ID     = @CLAIM_ID    AND
          ACTIVITY_ID  = @ACTIVITY_ID AND
          PAYEE_ID     = @PAYEE_ID    AND
          PROCESS_TYPE = @PROCESS_TYPE
      
END      


GO

