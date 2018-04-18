IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertCLM_ACTIVITY_RECOVERY]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertCLM_ACTIVITY_RECOVERY]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                                      
                                      
Proc Name       : Proc_InsertCLM_ACTIVITY_RECOVERY                      
Created by      : Sumit Chhabra                      
Date            : 02/06/2006                                      
Purpose         : Insert of Claim Payment  data in CLM_ACTIVITY_RECOVERY                           
Revison History :                                      
Used In         : Wolverine                                      
  
Reviewed By : Anurag Verma  
Reviewed On : 16-07-2007  
------------------------------------------------------------                                      
Date     Review By          Comments                                      
------   ------------       -------------------------*/                                      
--DROP PROC Proc_InsertCLM_ACTIVITY_RECOVERY  
CREATE PROC [dbo].[Proc_InsertCLM_ACTIVITY_RECOVERY]                                      
(                                      
 @CLAIM_ID int,                      
 @RESERVE_ID int,                
 @ACTIVITY_ID int,                      
 @AMOUNT decimal(18,2),                      
 @ACTION_ON_RECOVERY int,                      
 @CREATED_BY int,                      
 @CREATED_DATETIME datetime,              
 @VEHICLE_ID int   ,  
 @DRACCTS int=null,  
 @CRACCTS int=null,  
 @PAYMENT_METHOD SMALLINT = NULL,      
 @CHECK_NUMBER NVARCHAR(40)=NULL,
 @ACTUAL_RISK_ID INT = NULL,
 @ACTUAL_RISK_TYPE VARCHAR(10) = NULL  
)                                      
AS                                      
BEGIN                   
                    
DECLARE @RECOVERY_ID int              
DECLARE @LOSS_REINSURANCE_RECOVERED int          

 /*Generating the new payment id*/                                      
 SELECT @RECOVERY_ID=ISNULL(MAX(RECOVERY_ID),0)+1 FROM CLM_ACTIVITY_RECOVERY where CLAIM_ID=@CLAIM_ID                                   
            
--SET @LOSS_REINSURANCE_RECOVERED = 86          
SET @LOSS_REINSURANCE_RECOVERED = 182

IF @CHECK_NUMBER=''
BEGIN
  SET @CHECK_NUMBER=NULL
END     
        
                    
 INSERT INTO CLM_ACTIVITY_RECOVERY                           
 (                       
  CLAIM_ID,                
  RESERVE_ID,                
  ACTIVITY_ID,                
  RECOVERY_ID,                
  AMOUNT,                
  ACTION_ON_RECOVERY,                
  CREATED_BY,                
  CREATED_DATETIME,                
  IS_ACTIVE,              
  VEHICLE_ID   ,  
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
  @RECOVERY_ID,                
  @AMOUNT,                
  @ACTION_ON_RECOVERY,                
  @CREATED_BY,                
  @CREATED_DATETIME,                
  'Y',              
  @VEHICLE_ID    ,  
  @DRACCTS,  
  @CRACCTS,   
  @PAYMENT_METHOD,
  @CHECK_NUMBER,
  @ACTUAL_RISK_ID,
  @ACTUAL_RISK_TYPE
 )                    
            
--Praveen(08-06-2008); 
--Reducing Reinsurance Reserve here will result in Reserve reduced by double amount.
--because system also creates a Reserve Update entry at activity completion which in turn reduces reserve by activity 
--amount         
--  if(@ACTION_ON_RECOVERY=@LOSS_REINSURANCE_RECOVERED)            
--   UPDATE CLM_ACTIVITY_RESERVE SET RI_RESERVE=ISNULL(ISNULL(RI_RESERVE,0)-ISNULL(@AMOUNT,0),0) WHERE CLAIM_ID=@CLAIM_ID AND RESERVE_ID=@RESERVE_ID            
END

GO

