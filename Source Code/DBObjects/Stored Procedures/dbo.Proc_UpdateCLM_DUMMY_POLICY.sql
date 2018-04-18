IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdateCLM_DUMMY_POLICY]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdateCLM_DUMMY_POLICY]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*Proc Name     : dbo.Proc_UpdateCLM_DUMMY_POLICY                                        
Created by      : Mohit Agarwal                                        
Date            : 10-Dec-07                                        
Purpose       :Insert                                        
Revison History :                                        
Used In        : Wolverine                                        
                    
Modified By :            
Modified On :            
Purpose     :            
                         
------------------------------------------------------------                                        
Date     Review By          Comments                                        
------   ------------       -------------------------*/                                        
--drop PROC dbo.Proc_UpdateCLM_DUMMY_POLICY                                 
create PROC dbo.Proc_UpdateCLM_DUMMY_POLICY                                 
(                                        
@CLAIM_ID int,            
@DUMMY_POLICY_ID int                                         
)                                  
AS                                        
BEGIN                                        
  
 UPDATE CLM_DUMMY_POLICY            
    SET CLAIM_ID= @CLAIM_ID
  WHERE DUMMY_POLICY_ID = @DUMMY_POLICY_ID           

--update corresponding data in CLM_CLAIM_INFO table correspoding to claim number and claim id        
 UPDATE           
  CLM_CLAIM_INFO           
 SET           
  DUMMY_POLICY_ID = @DUMMY_POLICY_ID,
  POLICY_ID=0 
 WHERE           
  --CLAIM_NUMBER=@POLICY_NUMBER AND          
  CLAIM_ID = @CLAIM_ID          
END                                
                                        
                                        
                                      
                                      
                                      
                                      
                                 
  





GO

