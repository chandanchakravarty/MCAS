IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertCLM_ACTIVITY_EXPENSE]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertCLM_ACTIVITY_EXPENSE]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                                    
                                    
Proc Name       : Proc_InsertCLM_ACTIVITY_EXPENSE                    
Created by      : Sumit Chhabra                    
Date            : 02/06/2006                                    
Purpose         : Insert of Claim Payment  data in CLM_ACTIVITY_EXPENSE                         
Revison History :                                    
Used In                   : Wolverine                                    
------------------------------------------------------------                                    
Date     Review By          Comments                                    
------   ------------       -------------------------*/                                    
-- drop PROC dbo.Proc_InsertCLM_ACTIVITY_EXPENSE   
CREATE PROC dbo.Proc_InsertCLM_ACTIVITY_EXPENSE                                    
(                                    
 @CLAIM_ID int,                    
 @RESERVE_ID int,              
 @ACTIVITY_ID int,                    
 @PAYMENT_AMOUNT decimal(20,2),                    
 @ACTION_ON_PAYMENT int,                    
 @CREATED_BY int,                    
 @CREATED_DATETIME datetime,            
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
                  
DECLARE @EXPENSE_ID int            
DECLARE @PAID_LOSS_PARTIAL int        
DECLARE @PAID_LOSS_FINAL int        
  
 /*Generating the new payment id*/                                    
 SELECT @EXPENSE_ID=ISNULL(MAX(EXPENSE_ID),0)+1 FROM CLM_ACTIVITY_EXPENSE where CLAIM_ID=@CLAIM_ID AND ACTIVITY_ID=@ACTIVITY_ID                                
          
SET @PAID_LOSS_PARTIAL = 180    
SET @PAID_LOSS_FINAL = 181    
       
                  
 INSERT INTO CLM_ACTIVITY_EXPENSE                         
 (                     
  CLAIM_ID,              
  RESERVE_ID,              
  ACTIVITY_ID,              
  EXPENSE_ID,              
  PAYMENT_AMOUNT,              
  ACTION_ON_PAYMENT,              
  CREATED_BY,              
  CREATED_DATETIME,              
  IS_ACTIVE,            
  VEHICLE_ID,    
  ADDITIONAL_EXPENSE ,  
  DRACCTS,  
  CRACCTS,  
  PAYMENT_METHOD,
  CHECK_NUMBER,
  ACTUAL_RISK_ID,
  ACTUAL_RISK_TYPE                  
 )                    
 VALUES                                    
 (                    
  @CLAIM_ID,              
  @RESERVE_ID,              
  @ACTIVITY_ID,               
  @EXPENSE_ID,              
  @PAYMENT_AMOUNT,              
  @ACTION_ON_PAYMENT,              
  @CREATED_BY,              
  @CREATED_DATETIME,              
  'Y',            
  @VEHICLE_ID,    
  @ADDITIONAL_EXPENSE,  
  @DRACCTS,  
  @CRACCTS,  
  @PAYMENT_METHOD,
  @CHECK_NUMBER,
  @ACTUAL_RISK_ID,
  @ACTUAL_RISK_TYPE                    
 )                  
          
 if(@ACTION_ON_PAYMENT=@REDUCE_RESERVES_BY_PAYMENT_AMOUNT)              
  UPDATE CLM_ACTIVITY SET CLAIM_RESERVE_AMOUNT=ISNULL(ISNULL(CLAIM_RESERVE_AMOUNT,0)-ISNULL(@PAYMENT_AMOUNT,0),0),EXPENSES = @PAYMENT_AMOUNT WHERE CLAIM_ID=@CLAIM_ID AND ACTIVITY_ID=@ACTIVITY_ID              
 else if(@ACTION_ON_PAYMENT=@CLOSE_TO_ZERO)             
 BEGIN           
  UPDATE CLM_ACTIVITY_RESERVE SET OUTSTANDING=0 WHERE  CLAIM_ID=@CLAIM_ID          
   UPDATE CLM_ACTIVITY SET CLAIM_RESERVE_AMOUNT=0,EXPENSES = @PAYMENT_AMOUNT WHERE CLAIM_ID=@CLAIM_ID AND ACTIVITY_ID=@ACTIVITY_ID              
 END         
      
END

GO

