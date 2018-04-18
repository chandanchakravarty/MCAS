IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertCLM_ACTIVITY_PAYMENT]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertCLM_ACTIVITY_PAYMENT]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                                  
                                  
Proc Name       : Proc_InsertCLM_ACTIVITY_PAYMENT                  
Created by      : Sumit Chhabra                  
Date            : 02/06/2006                                  
Purpose         : Insert of Claim Payment  data in CLM_ACTIVITY_PAYMENT                       
Revison History :                                  
Used In                   : Wolverine                                  
------------------------------------------------------------                                  
MODIFIED BY  : Asfa Praveen    
Date  : 13-Sept-2007    
Purpose  : Modify Activities result    
------------------------------------------------------------          
Date     Review By          Comments                                  
------   ------------       -------------------------*/                                  
-- DROP PROC dbo.Proc_InsertCLM_ACTIVITY_PAYMENT                                  
CREATE PROC [dbo].[Proc_InsertCLM_ACTIVITY_PAYMENT]                                  
(                                  
 @CLAIM_ID int,                  
 @RESERVE_ID int,            
 @ACTIVITY_ID int,                  
 @PAYMENT_AMOUNT decimal(18,2),                  
 @ACTION_ON_PAYMENT int,                  
 @CREATED_BY int,                  
 @CREATED_DATETIME datetime,          
 @VEHICLE_ID int    ,      
 @DRACCTS int=null,      
 @CRACCTS int=null,      
 @PAYMENT_METHOD SMALLINT = NULL,      
 @CHECK_NUMBER NVARCHAR(40)=NULL,
 @ACTUAL_RISK_ID INT = NULL,
 @ACTUAL_RISK_TYPE VARCHAR(10) = NULL                             
)                                  
AS                                  
BEGIN               
                
DECLARE @PAYMENT_ID int          
DECLARE @PAID_LOSS_PARTIAL int      
DECLARE @PAID_LOSS_FINAL int 

IF @CHECK_NUMBER=''
BEGIN
  SET @CHECK_NUMBER=NULL
END     
      
 /*Generating the new payment id*/                                  
 SELECT @PAYMENT_ID=ISNULL(MAX(PAYMENT_ID),0)+1 FROM CLM_ACTIVITY_PAYMENT where CLAIM_ID=@CLAIM_ID                               
        
SET @PAID_LOSS_PARTIAL = 180        
SET @PAID_LOSS_FINAL = 181    

IF EXISTS(SELECT PAYMENT_ID FROM CLM_ACTIVITY_PAYMENT WHERE CLAIM_ID=@CLAIM_ID AND RESERVE_ID=@RESERVE_ID AND ACTION_ON_PAYMENT=@ACTION_ON_PAYMENT AND ACTIVITY_ID=@ACTIVITY_ID AND VEHICLE_ID=@VEHICLE_ID AND ACTUAL_RISK_ID = @ACTUAL_RISK_ID 
          AND ACTUAL_RISK_TYPE = @ACTUAL_RISK_TYPE)    
BEGIN
  DELETE FROM CLM_ACTIVITY_PAYMENT WHERE CLAIM_ID=@CLAIM_ID AND RESERVE_ID=@RESERVE_ID AND ACTION_ON_PAYMENT=@ACTION_ON_PAYMENT AND ACTIVITY_ID=@ACTIVITY_ID AND VEHICLE_ID=@VEHICLE_ID AND ACTUAL_RISK_ID = @ACTUAL_RISK_ID 
  AND ACTUAL_RISK_TYPE = @ACTUAL_RISK_TYPE
END
                
 INSERT INTO CLM_ACTIVITY_PAYMENT                       
 (                   
  CLAIM_ID,            
  RESERVE_ID,            
  ACTIVITY_ID,            
  PAYMENT_ID,            
  PAYMENT_AMOUNT,            
  ACTION_ON_PAYMENT,            
  CREATED_BY,            
  CREATED_DATETIME,            
  IS_ACTIVE,          
  VEHICLE_ID ,      
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
  @PAYMENT_ID,            
  @PAYMENT_AMOUNT,            
  @ACTION_ON_PAYMENT,            
  @CREATED_BY,            
  @CREATED_DATETIME,            
  'Y',          
  @VEHICLE_ID,      
  @DRACCTS,      
  @CRACCTS,      
  @PAYMENT_METHOD,
  @CHECK_NUMBER,
  @ACTUAL_RISK_ID,
  @ACTUAL_RISK_TYPE
 )                
-- Commented by Asfa (13-Sept-2007) in order to correct Activities as per email sent by Gagan.    
/* if(@ACTION_ON_PAYMENT=@PAID_LOSS_PARTIAL)        
  UPDATE CLM_ACTIVITY_RESERVE SET OUTSTANDING=ISNULL(ISNULL(OUTSTANDING,0)-ISNULL(@PAYMENT_AMOUNT,0),0) WHERE CLAIM_ID=@CLAIM_ID AND RESERVE_ID=@RESERVE_ID        
 else if(@ACTION_ON_PAYMENT=@PAID_LOSS_FINAL)        
  UPDATE CLM_ACTIVITY_RESERVE SET OUTSTANDING=0 WHERE CLAIM_ID=@CLAIM_ID AND RESERVE_ID=@RESERVE_ID */       
  
if(@ACTION_ON_PAYMENT=@PAID_LOSS_PARTIAL)        
  UPDATE CLM_ACTIVITY_RESERVE SET OUTSTANDING=ISNULL(OUTSTANDING,0) WHERE CLAIM_ID=@CLAIM_ID AND RESERVE_ID=@RESERVE_ID 
/* else if(@ACTION_ON_PAYMENT=@PAID_LOSS_FINAL)        
  UPDATE CLM_ACTIVITY_RESERVE SET OUTSTANDING=0 WHERE CLAIM_ID=@CLAIM_ID AND RESERVE_ID=@RESERVE_ID  
*/
END

GO

