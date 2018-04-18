IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertCLM_LINKED_CLAIMS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertCLM_LINKED_CLAIMS]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



/*----------------------------------------------------------                  
Proc Name       : dbo.Proc_InsertCLM_LINKED_CLAIMS                  
Created by      : Sumit Chhabra                  
Date            : 09/11/2006                  
Purpose         : Insert data in CLM_LINKED_CLAIMS      
Created by      : Sumit Chhabra                  
Revison History :                  
Used In        : Wolverine                  
------------------------------------------------------------                  
Date     Review By          Comments                  
------   ------------       -------------------------*/                  
-- DROP PROC dbo.Proc_InsertCLM_LINKED_CLAIMS                  
CREATE PROC dbo.Proc_InsertCLM_LINKED_CLAIMS                         
@CLAIM_ID int,    
@LINKED_CLAIM_ID_LIST varchar(500)    
AS    
begin    
    
--find applications assigned to these underwriters                    
declare @CURRENT_CLAIM_ID varchar(10)                    
declare @COUNT INT                         
set @COUNT=2                    
                 
if @LINKED_CLAIM_ID_LIST is null or @LINKED_CLAIM_ID_LIST=''    
 return 1    
     
 --delete all the records for the current claim_id  
 DELETE FROM CLM_LINKED_CLAIMS WHERE CLAIM_ID=@CLAIM_ID  
  
 --delete all the records where the current claim is being linked  
 DELETE FROM CLM_LINKED_CLAIMS WHERE LINKED_CLAIM_ID=@CLAIM_ID  
        
 set @CURRENT_CLAIM_ID = dbo.piece(@LINKED_CLAIM_ID_LIST,'^',1)                                  
    
 WHILE @CURRENT_CLAIM_ID is not null                                  
 BEGIN                          
 if not exists(select claim_id from CLM_LINKED_CLAIMS where claim_id=@claim_id and LINKED_CLAIM_ID = @CURRENT_CLAIM_ID)    
  insert into CLM_LINKED_CLAIMS values(@claim_id,@CURRENT_CLAIM_ID)    
  
 if not exists(select claim_id from CLM_LINKED_CLAIMS where claim_id=@CURRENT_CLAIM_ID and LINKED_CLAIM_ID = @claim_id)    
  insert into CLM_LINKED_CLAIMS values(@CURRENT_CLAIM_ID,@claim_id)    
  
      SET @CURRENT_CLAIM_ID=DBO.PIECE(@LINKED_CLAIM_ID_LIST,'^',@COUNT)                    
      SET @COUNT=@COUNT+1                                                    
 END                       
end    
    
    
    
  




GO

