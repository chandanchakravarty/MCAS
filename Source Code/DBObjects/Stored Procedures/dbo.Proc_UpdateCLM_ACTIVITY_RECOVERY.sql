IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdateCLM_ACTIVITY_RECOVERY]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdateCLM_ACTIVITY_RECOVERY]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                                    
                                    
Proc Name       : Proc_UpdateCLM_ACTIVITY_RECOVERY                    
Created by      : Sumit Chhabra                    
Date            : 02/06/2006                                    
Purpose         : Update of Claim Payment  data in CLM_ACTIVITY_RECOVERY                         
Revison History :                                    
Used In                   : Wolverine                                    
  
Reviewed By : Anurag verma  
Reviewed On : 16-07-2007  
------------------------------------------------------------                                    
Date     Review By          Comments                                    
------   ------------       -------------------------*/                                    
--DROP PROC dbo.Proc_UpdateCLM_ACTIVITY_RECOVERY  
CREATE PROC [dbo].[Proc_UpdateCLM_ACTIVITY_RECOVERY]                                    
(                                    
 @CLAIM_ID int,                    
 @RESERVE_ID int,              
 @ACTIVITY_ID int,              
 @RECOVERY_ID int,              
 @AMOUNT decimal(18,2),                    
 @ACTION_ON_RECOVERY int,                    
 @MODIFIED_BY int,                    
 @LAST_UPDATED_DATETIME datetime,            
 @VEHICLE_ID int     ,  
 @DRACCTS int=null,    
 @CRACCTS int=null,  
 @PAYMENT_METHOD SMALLINT = NULL,      
 @CHECK_NUMBER NVARCHAR(40)=NULL,
 @ACTUAL_RISK_ID INT = NULL,
 @ACTUAL_RISK_TYPE VARCHAR(10) = NULL          
)                                    
AS                                    
BEGIN                
    
DECLARE @LOSS_REINSURANCE_RECOVERED int            
--SET @LOSS_REINSURANCE_RECOVERED = 86          
SET @LOSS_REINSURANCE_RECOVERED = 182       
       

IF @CHECK_NUMBER=''
BEGIN
	SET @CHECK_NUMBER=NULL
END 
       
                  
UPDATE CLM_ACTIVITY_RECOVERY                         
SET  RESERVE_ID=@RESERVE_ID,              
ACTIVITY_ID=@ACTIVITY_ID,                
AMOUNT=@AMOUNT,              
ACTION_ON_RECOVERY=@ACTION_ON_RECOVERY,              
MODIFIED_BY=@MODIFIED_BY,              
LAST_UPDATED_DATETIME=@LAST_UPDATED_DATETIME,            
VEHICLE_ID = @VEHICLE_ID        ,  
DRACCTS = @DRACCTS,  
CRACCTS = @CRACCTS,  
PAYMENT_METHOD = @PAYMENT_METHOD,    
CHECK_NUMBER = @CHECK_NUMBER,
ACTUAL_RISK_ID = @ACTUAL_RISK_ID,
ACTUAL_RISK_TYPE = @ACTUAL_RISK_TYPE             
WHERE              
CLAIM_ID=@CLAIM_ID AND              
RECOVERY_ID = @RECOVERY_ID and VEHICLE_ID = @VEHICLE_ID AND
  ACTUAL_RISK_ID = @ACTUAL_RISK_ID AND ACTUAL_RISK_TYPE = @ACTUAL_RISK_TYPE            

--Ravindra(08-06-2008); Reducing Reinsurance Reserve here will result in Reserve reduced by double amount.
-- because system also creates a Reserve Update entry at activity completion which in turn reduces reserve by activity 
--amount          
-- if(@ACTION_ON_RECOVERY=@LOSS_REINSURANCE_RECOVERED)          
-- 	UPDATE CLM_ACTIVITY_RESERVE SET RI_RESERVE=ISNULL(ISNULL(RI_RESERVE,0)-ISNULL(@AMOUNT,0),0) 
-- 	WHERE CLAIM_ID=@CLAIM_ID AND RESERVE_ID=@RESERVE_ID          
         
END

GO

