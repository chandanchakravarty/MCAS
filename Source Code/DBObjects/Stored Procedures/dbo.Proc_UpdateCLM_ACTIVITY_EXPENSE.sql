IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdateCLM_ACTIVITY_EXPENSE]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdateCLM_ACTIVITY_EXPENSE]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                                  
                                  
Proc Name       : Proc_UpdateCLM_ACTIVITY_EXPENSE                  
Created by      : Sumit Chhabra                  
Date            : 02/06/2006                                  
Purpose         : Update of Claim Payment  data in CLM_ACTIVITY_EXPENSE                       
Revison History :                                  
Used In                   : Wolverine                                  
------------------------------------------------------------                                  
Date     Review By          Comments                                  
------   ------------       -------------------------*/                                  
--DROP PROC Proc_UpdateCLM_ACTIVITY_EXPENSE  
CREATE PROC dbo.Proc_UpdateCLM_ACTIVITY_EXPENSE                                  
(                                  
 @CLAIM_ID int,                  
 @RESERVE_ID int,            
 @ACTIVITY_ID int,            
 @EXPENSE_ID int,            
 @PAYMENT_AMOUNT decimal(20,2),                  
 @ACTION_ON_PAYMENT int,                  
 @MODIFIED_BY int,                  
 @LAST_UPDATED_DATETIME datetime,          
 @VEHICLE_ID int,    
 @ADDITIONAL_EXPENSE decimal(20,2)  ,  
 @DRACCTS int=null,    
 @CRACCTS int=null,  
 @PAYMENT_METHOD SMALLINT = NULL,      
 @CHECK_NUMBER NVARCHAR(40)=NULL,
 @ACTUAL_RISK_ID INT = NULL,
 @ACTUAL_RISK_TYPE VARCHAR(10) = NULL   
)                                  
AS                                  
BEGIN              
        
DECLARE @REDUCE_RESERVES_BY_PAYMENT_AMOUNT int            
DECLARE @CLOSE_TO_ZERO int            

IF @CHECK_NUMBER=''
BEGIN
  SET @CHECK_NUMBER=NULL
END          
            
set @REDUCE_RESERVES_BY_PAYMENT_AMOUNT = 11785            
set @CLOSE_TO_ZERO = 11783     
      
                
 UPDATE CLM_ACTIVITY_EXPENSE                       
 SET            
  RESERVE_ID=@RESERVE_ID,            
  PAYMENT_AMOUNT=@PAYMENT_AMOUNT,            
  ACTION_ON_PAYMENT=@ACTION_ON_PAYMENT,            
  MODIFIED_BY=@MODIFIED_BY,            
  LAST_UPDATED_DATETIME=@LAST_UPDATED_DATETIME,          
 VEHICLE_ID = @VEHICLE_ID,    
ADDITIONAL_EXPENSE=@ADDITIONAL_EXPENSE  ,  
 DRACCTS = @DRACCTS,  
 CRACCTS = @CRACCTS,  
 PAYMENT_METHOD = @PAYMENT_METHOD,    
 CHECK_NUMBER = @CHECK_NUMBER,
 ACTUAL_RISK_ID = @ACTUAL_RISK_ID,
 ACTUAL_RISK_TYPE = @ACTUAL_RISK_TYPE   
 WHERE            
  CLAIM_ID=@CLAIM_ID AND ACTIVITY_ID=@ACTIVITY_ID AND             
  EXPENSE_ID = @EXPENSE_ID   AND VEHICLE_ID =@VEHICLE_ID AND 
  ACTUAL_RISK_ID = @ACTUAL_RISK_ID AND ACTUAL_RISK_TYPE = @ACTUAL_RISK_TYPE         
        
  if(@ACTION_ON_PAYMENT=@REDUCE_RESERVES_BY_PAYMENT_AMOUNT)            
  UPDATE CLM_ACTIVITY SET CLAIM_RESERVE_AMOUNT=ISNULL(ISNULL(CLAIM_RESERVE_AMOUNT,0)-ISNULL(@PAYMENT_AMOUNT,0),0),EXPENSES = @PAYMENT_AMOUNT WHERE CLAIM_ID=@CLAIM_ID AND ACTIVITY_ID=@ACTIVITY_ID            
 else if(@ACTION_ON_PAYMENT=@CLOSE_TO_ZERO)          
 BEGIN        
   UPDATE CLM_ACTIVITY_RESERVE SET OUTSTANDING=0 WHERE  CLAIM_ID=@CLAIM_ID           
    UPDATE CLM_ACTIVITY SET CLAIM_RESERVE_AMOUNT=0,EXPENSES = @PAYMENT_AMOUNT WHERE CLAIM_ID=@CLAIM_ID AND ACTIVITY_ID=@ACTIVITY_ID           
 END        
END

GO

