IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertCLM_ACTIVITY_RESERVE_BREAKDOWN]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertCLM_ACTIVITY_RESERVE_BREAKDOWN]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                                                                            
Proc Name       : dbo.Proc_InsertCLM_ACTIVITY_RESERVE_BREAKDOWN                                                                
Created by      : Sumit Chhabra                                                                          
Date            : 01/06/2006                                                                            
Purpose         : Insert data in CLM_ACTIVITY_RESERVE_BREAKDOWN table for claim reserve breakdown screen                                                        
Created by      : Sumit Chhabra                                                                           
Revison History :                                                                            
Used In        : Wolverine                                                                            
------------------------------------------------------------                                                                            
Date     Review By          Comments                                                                            
------   ------------       -------------------------*/                                                                            
-- DROP PROC dbo.Proc_InsertCLM_ACTIVITY_RESERVE_BREAKDOWN                                                                  
CREATE PROC dbo.Proc_InsertCLM_ACTIVITY_RESERVE_BREAKDOWN                                                                  
@CLAIM_ID int,                      
@ACTIVITY_ID int,                      
@RESERVE_BREAKDOWN_ID int output,            
@TRANSACTION_CODE int,                      
@BASIS int,            
@VALUE decimal(20,2),                      
@AMOUNT decimal(20,2),                       
@CREATED_BY int,                      
@CREATED_DATETIME datetime            
                                                        
AS                                                                            
BEGIN         
      
--CHECK WHETHER THE AMOUNT ENTERED IS WITHIN THE LIMITS OF TOTAL OUTSTANDING FOR THE CURRENT ACTIVITY      
declare @RESERVE_RECORDED decimal(20,2)      
declare @RESERVE_LIMIT decimal(20,2)      
  
--If no reserve limit record exists for the current activity or is set to 0, the return  
if not exists(select claim_id from CLM_ACTIVITY WHERE CLAIM_ID=@CLAIM_ID AND ACTIVITY_ID=@ACTIVITY_ID)  
 return -2  

  
--fetch the total reserved amount limit and amount recorded      
SELECT @RESERVE_LIMIT=isnull(RESERVE_AMOUNT,0) FROM CLM_ACTIVITY WHERE CLAIM_ID=@CLAIM_ID AND ACTIVITY_ID=@ACTIVITY_ID                       
SELECT @RESERVE_RECORDED=ISNULL(SUM(AMOUNT),0)  FROM CLM_ACTIVITY_RESERVE_BREAKDOWN   WHERE CLAIM_ID=@CLAIM_ID AND ACTIVITY_ID=@ACTIVITY_ID      
      
--if the difference in reserve limit and recorded reserve amount is less than amount being entered, then return      
--we cannot allow to record reserve in excess of the limit defined..      
if (@RESERVE_LIMIT - @RESERVE_RECORDED) < @AMOUNT      
 return -1      

 SELECT                                                         
  @RESERVE_BREAKDOWN_ID = ISNULL(MAX(RESERVE_BREAKDOWN_ID),0)+1                                                         
 FROM                                                         
  CLM_ACTIVITY_RESERVE_BREAKDOWN                               
 WHERE                      
  CLAIM_ID=@CLAIM_ID AND        
 ACTIVITY_ID=@ACTIVITY_ID                                          
                                                      
                         
                                                        
 INSERT INTO CLM_ACTIVITY_RESERVE_BREAKDOWN                                             
 (                                                  
  CLAIM_ID,                      
  ACTIVITY_ID,                   
  RESERVE_BREAKDOWN_ID,                      
  TRANSACTION_CODE,                      
  BASIS,                      
  VALUE,                      
  AMOUNT,                            
  CREATED_BY,                      
  CREATED_DATETIME,                      
  IS_ACTIVE            
 )                
 VALUES                                                        
 (                                                        
  @CLAIM_ID,                      
  @ACTIVITY_ID,                      
  @RESERVE_BREAKDOWN_ID,                      
 @TRANSACTION_CODE,                      
  @BASIS,                      
  @VALUE,                      
  @AMOUNT,                            
  @CREATED_BY,                      
  @CREATED_DATETIME,                      
  'Y'            
 )                                                                       
END             
          
        
      
    
  
  
  





GO

