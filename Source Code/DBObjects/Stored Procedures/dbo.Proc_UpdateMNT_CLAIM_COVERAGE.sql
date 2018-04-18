IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdateMNT_CLAIM_COVERAGE]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdateMNT_CLAIM_COVERAGE]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                                        
Proc Name       : dbo.Proc_UpdateMNT_CLAIM_COVERAGE                                        
Created by      : Mohit Agarwal                                        
Date            : 5-Dec-2007                                        
Purpose         : Update data in MNT_CLAIM_COVERAGE table for claim notification screen                                        
Revison History :                                        
Used In        : Wolverine                                        
------------------------------------------------------------                                        
Modified By  :              
Date   :              
Purpose  :              
------------------------------------------------------------                                                                              
Date     Review By          Comments                                        
------   ------------       -------------------------*/                                        
--DROP PROC dbo.Proc_UpdateMNT_CLAIM_COVERAGE                                                                                 
CREATE PROC dbo.Proc_UpdateMNT_CLAIM_COVERAGE     
(    
@COV_ID int,                                                                                
@CLAIM_ID int,    
@COV_DES varchar(500),    
@STATE_ID varchar(10),    
@LOB_ID varchar(10),    
@LIMIT_1 decimal(18,2),    
@DEDUCTIBLE_1 decimal(18,2),    
@IS_ACTIVE char(1),    
@CREATED_BY int = null,    
@CREATED_DATETIME DateTime = null,    
@MODIFIED_BY int = null,    
@LAST_UPDATED_DATETIME DateTime = null    
)    
AS                                                              
BEGIN                 
    
IF ISNULL(@LOB_ID,'') = ''     
    SELECT @LOB_ID=LOB_ID FROM CLM_DUMMY_POLICY WHERE @CLAIM_ID=CLAIM_ID     
    
IF ISNULL(@STATE_ID,'') = ''     
    SELECT @STATE_ID=DUMMY_STATE FROM CLM_DUMMY_POLICY WHERE @CLAIM_ID=CLAIM_ID     
      
UPDATE MNT_CLAIM_COVERAGE       
SET      
  CLAIM_ID=@CLAIM_ID,    
  COV_DES=@COV_DES,    
  STATE_ID=@STATE_ID,    
  LOB_ID=@LOB_ID,    
  LIMIT_1=@LIMIT_1,    
  DEDUCTIBLE_1=@DEDUCTIBLE_1,    
  --IS_ACTIVE=@IS_ACTIVE,    
  MODIFIED_BY=@MODIFIED_BY,    
  LAST_UPDATED_DATETIME=@LAST_UPDATED_DATETIME    
WHERE      
  COV_ID=@COV_ID      
    
END                   
      
                                          
                                          
                              
              
            
            
          
        
        
      
      
      
      
      
      
      
      
      
      
      
      
      
    
    
  
  




GO

