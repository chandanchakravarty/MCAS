IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdateCLM_ACTIVITY_RESERVE_BREAKDOWN]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdateCLM_ACTIVITY_RESERVE_BREAKDOWN]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                                                                      
Proc Name       : dbo.Proc_UpdateCLM_ACTIVITY_RESERVE_BREAKDOWN                                                          
Created by      : Sumit Chhabra                                                                    
Date            : 01/06/2006                                                                      
Purpose         : Update data at CLM_ACTIVITY_RESERVE_BREAKDOWN table for claim reserve breakdown screen                                                  
Created by      : Sumit Chhabra                                                                     
Revison History :                                                                      
Used In        : Wolverine                                                                      
------------------------------------------------------------                                                                      
Date     Review By          Comments                                                                      
------   ------------       -------------------------*/                                                                      
-- DROP PROC dbo.Proc_UpdateCLM_ACTIVITY_RESERVE_BREAKDOWN                                                            
CREATE PROC dbo.Proc_UpdateCLM_ACTIVITY_RESERVE_BREAKDOWN                                                            
@CLAIM_ID int,                
@ACTIVITY_ID int,                
@RESERVE_BREAKDOWN_ID int,      
@TRANSACTION_CODE int,                
@BASIS int,      
@VALUE decimal(20,2),                
@AMOUNT decimal(20,2),                 
@MODIFIED_BY int,                
@LAST_UPDATED_DATETIME datetime      
                                                  
AS                                                                      
BEGIN                   
    
--CHECK WHETHER THE AMOUNT ENTERED IS WITHIN THE LIMITS OF TOTAL OUTSTANDING FOR THE CURRENT ACTIVITY    
declare @RESERVE_RECORDED decimal(20,2)    
declare @RESERVE_LIMIT decimal(20,2)    
--fetch the total reserved amount limit and amount recorded    
select @RESERVE_LIMIT=isnull(reserve_amount,0) From clm_activity where claim_id=@CLAIM_ID and activity_id=@ACTIVITY_ID                     
select @RESERVE_RECORDED=isnull(sum(amount),0)  From CLM_ACTIVITY_RESERVE_BREAKDOWN   where claim_id=@CLAIM_ID and activity_id=@ACTIVITY_ID and RESERVE_BREAKDOWN_ID<>@RESERVE_BREAKDOWN_ID
   
set @RESERVE_RECORDED = isnull(@RESERVE_RECORDED,0) + isnull(@AMOUNT,0)
--If no reserve limit record exists for the current activity or is set to 0, the return  
if @RESERVE_LIMIT < 1  
 return -2  
  
--if the difference in reserve limit and recorded reserve amount is less than amount being entered, then return    
--we cannot allow to record reserve in excess of the limit defined..    
if (@RESERVE_LIMIT - @RESERVE_RECORDED) < @AMOUNT    
 return -1     
      
  
 UPDATE       
  CLM_ACTIVITY_RESERVE_BREAKDOWN                                       
 SET      
   TRANSACTION_CODE=@TRANSACTION_CODE,                
   BASIS=@BASIS,                
   VALUE=@VALUE,                
   AMOUNT=@AMOUNT,                      
   MODIFIED_BY=@MODIFIED_BY,                
   LAST_UPDATED_DATETIME=@LAST_UPDATED_DATETIME                
 WHERE                                         
   CLAIM_ID=@CLAIM_ID AND                 
   ACTIVITY_ID=@ACTIVITY_ID AND                 
   RESERVE_BREAKDOWN_ID=@RESERVE_BREAKDOWN_ID                
END       
    
  





GO

