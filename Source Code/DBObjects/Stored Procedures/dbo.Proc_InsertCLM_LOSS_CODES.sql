IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertCLM_LOSS_CODES]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertCLM_LOSS_CODES]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                              
                              
Proc Name       : Proc_InsertCLM_LOSS_CODES              
Created by      : Sumit Chhabra              
Date            : 24/04/2006                              
Purpose         : Insert of Loss Codes Type in CLM_LOSS_CODES      
Revison History :                              
Used In                   : Wolverine                              
------------------------------------------------------------                              
Date     Review By          Comments                              
------   ------------       -------------------------*/    
--drop proc    Dbo.Proc_InsertCLM_LOSS_CODES                            
CREATE PROC Dbo.Proc_InsertCLM_LOSS_CODES                             
(                              
 @LOB_ID smallint,      
 @LOSS_CODE_TYPES varchar(2000),      
 @CREATED_BY int,      
 @CREATED_DATETIME datetime      
)                              
AS                              
BEGIN                         
      
declare @LOSS_CODE_ID int      
declare @DataExistedFlag int  
  
--Delete all data related to that particular LOB before inserting/updating new data      
 DELETE FROM CLM_LOSS_CODES WHERE LOB_ID=@LOB_ID      
      
      
SELECT @LOSS_CODE_ID=ISNULL(MAX(LOSS_CODE_ID),0) + 1 FROM CLM_LOSS_CODES      
      
--if there is no data for insertion, return from the procedure      
if (@LOSS_CODE_TYPES is null) or (@LOSS_CODE_TYPES='')  
 return -1     
      
DECLARE @CURRENT_LOSS_CODE_TYPE VARCHAR(10)      
DECLARE @COUNT INT      
SET @COUNT=2      
    
 SET @CURRENT_LOSS_CODE_TYPE = DBO.PIECE(@LOSS_CODE_TYPES,',',1)                     
           
 --Run a loop to go through the list of comma-separated values for insertion      
while @CURRENT_LOSS_CODE_TYPE is not null            
 BEGIN                    
  --Insert LossCodesType data    
  IF NOT EXISTS(SELECT LOSS_CODE_ID FROM CLM_LOSS_CODES WHERE LOB_ID=@LOB_ID AND LOSS_CODE_TYPE=@CURRENT_LOSS_CODE_TYPE)  
    INSERT INTO CLM_LOSS_CODES       
    (      
     LOSS_CODE_ID,      
     LOB_ID,      
     LOSS_CODE_TYPE,      
     IS_ACTIVE,      
     CREATED_BY,      
     CREATED_DATETIME      
    )      
    values      
    (      
     @LOSS_CODE_ID,      
     @LOB_ID,      
     @CURRENT_LOSS_CODE_TYPE,      
     'Y',      
     @CREATED_BY,      
     @CREATED_DATETIME      
    )    
    --increment the loss_code_id         
      SET @CURRENT_LOSS_CODE_TYPE=DBO.PIECE(@LOSS_CODE_TYPES,',',@COUNT)              
   SET @COUNT=@COUNT+1     
     SET @LOSS_CODE_ID=@LOSS_CODE_ID+1      
 END          
END            
    

  
  


GO

