IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdateCLM_ACTIVITY_PAYMENT]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdateCLM_ACTIVITY_PAYMENT]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                                
                                
Proc Name       : Proc_UpdateCLM_ACTIVITY_PAYMENT                
Created by      : Sumit Chhabra                
Date            : 02/06/2006                                
Purpose         : Update of Claim Payment  data in CLM_ACTIVITY_PAYMENT                     
Revison History :                                
Used In                   : Wolverine                                
------------------------------------------------------------                                
MODIFIED BY  : Asfa Praveen  
Date  : 13-Sept-2007  
Purpose  : Modify Activities result.  
------------------------------------------------------------        
Date     Review By          Comments                                
------   ------------       -------------------------*/                                
-- DROP PROC dbo.Proc_UpdateCLM_ACTIVITY_PAYMENT                                
CREATE PROC [dbo].[Proc_UpdateCLM_ACTIVITY_PAYMENT]                                
(                                
 @CLAIM_ID int,                
 @RESERVE_ID int,          
 @ACTIVITY_ID int,          
 @PAYMENT_ID int,          
 @PAYMENT_AMOUNT decimal(18,2),                
 @ACTION_ON_PAYMENT int,                
 @MODIFIED_BY int,                
 @LAST_UPDATED_DATETIME datetime,        
 @VEHICLE_ID int  ,    
 @DRACCTS int=null,      
 @CRACCTS int=null,    
 @PAYMENT_METHOD SMALLINT = NULL,      
 @CHECK_NUMBER NVARCHAR(40)=NULL,
 @ACTUAL_RISK_ID INT = NULL,
 @ACTUAL_RISK_TYPE VARCHAR(10) = NULL                        
)                                
AS                                
BEGIN            
      
DECLARE @PAID_LOSS_PARTIAL int    
DECLARE @PAID_LOSS_FINAL int    
    
    
SET @PAID_LOSS_PARTIAL = 180      
SET @PAID_LOSS_FINAL = 181      

IF @CHECK_NUMBER=''
BEGIN
  SET @CHECK_NUMBER=NULL
END    
              
 UPDATE CLM_ACTIVITY_PAYMENT                     
 SET          
  RESERVE_ID=@RESERVE_ID,          
  ACTIVITY_ID=@ACTIVITY_ID,            
  PAYMENT_AMOUNT=@PAYMENT_AMOUNT,          
  ACTION_ON_PAYMENT=@ACTION_ON_PAYMENT,          
  MODIFIED_BY=@MODIFIED_BY,          
  LAST_UPDATED_DATETIME=@LAST_UPDATED_DATETIME,        
 VEHICLE_ID = @VEHICLE_ID  ,    
 DRACCTS = @DRACCTS,    
 CRACCTS = @CRACCTS,  PAYMENT_METHOD = @PAYMENT_METHOD , CHECK_NUMBER= @CHECK_NUMBER,
 ACTUAL_RISK_ID = @ACTUAL_RISK_ID,
 ACTUAL_RISK_TYPE = @ACTUAL_RISK_TYPE   
 WHERE          
  CLAIM_ID=@CLAIM_ID AND          
  PAYMENT_ID = @PAYMENT_ID  AND VEHICLE_ID =@VEHICLE_ID AND 
  ACTUAL_RISK_ID = @ACTUAL_RISK_ID AND ACTUAL_RISK_TYPE = @ACTUAL_RISK_TYPE        
  
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

