IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdateReinsuarnceActivityReserve]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdateReinsuarnceActivityReserve]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--begin tran 
-- drop PROC dbo.Proc_UpdateReinsuarnceActivityReserve  
--go  
/*----------------------------------------------------------                                                                                          
Proc Name       : dbo.Proc_UpdateReinsuarnceActivityReserve                                                                              
Created by      : Raghav Gupta  
Date            :   
Purpose         : Update  Reinsurance Reserve  data at CLM_ACTIVITY table                          
Created by      :  Itrack Issue #6144.  
Revison History :                                                                                          
Used In        : Wolverine                                                                                          
------------------------------------------------------------                                                                                          
Date     Review By          Comments                                                                                          
------   ------------       -------------------------*/                                                                                          
-- drop PROC dbo.Proc_UpdateReinsuarnceActivityReserve     
CREATE PROC [dbo].[Proc_UpdateReinsuarnceActivityReserve]                                                                                
@CLAIM_ID int,                             
@ACTIVITY_ID int,                                 
@RESERVE_AMOUNT decimal(20,2),                          
@EXPENSES decimal(20,2),                         
@RECOVERY decimal(20,2),                         
@PAYMENT_AMOUNT decimal(20,2),                         
@RI_RESERVE decimal(20,2),                         
@MODIFIED_BY int,                          
@LAST_UPDATED_DATETIME datetime                              
                                                                      
AS                                                                                          
BEGIN                                 
                    
  declare @ACTIVITY_REASON int                    
  declare @ACTION_ON_PAYMENT int            
  declare @ACTIVITY_RESERVE DECIMAL(20,2)            
  declare @OLD_CLAIM_RESERVE_AMOUNT DECIMAL(20,2)  
  declare @CURRENT_CLAIM_RESERVE_AMOUNT DECIMAL(20,2)         
  declare @OLD_CLAIM_RI_RESERVE DECIMAL(20,2)   
  declare @RECOVERIES DECIMAL(20,2)           
  declare @CLOSE_RESERVE INT  
  DECLARE @NEW_RESERVE INT     
  declare @REINSURANCE_RECOVERY DECIMAL (20,2)   
            
  SET @ACTIVITY_RESERVE = 11773            
  SET @OLD_CLAIM_RESERVE_AMOUNT = 0             
  SET @OLD_CLAIM_RI_RESERVE = 0       
  SET @NEW_RESERVE = 169   
  SET @CLOSE_RESERVE = 171         
              
  SELECT @RECOVERIES = SUM(ISNULL([RECOVERY],0)) FROM CLM_ACTIVITY WHERE CLAIM_ID=@CLAIM_ID  
  SELECT @REINSURANCE_RECOVERY = SUM(ISNULL(LOSS_REINSURANCE_RECOVERED,0)) + SUM(ISNULL(EXPENSE_REINSURANCE_RECOVERED,0)) FROM CLM_ACTIVITY WHERE CLAIM_ID=@CLAIM_ID   
  SELECT @ACTIVITY_REASON = ACTIVITY_REASON,@ACTION_ON_PAYMENT=ACTION_ON_PAYMENT FROM CLM_ACTIVITY WHERE CLAIM_ID=@CLAIM_ID AND ACTIVITY_ID=@ACTIVITY_ID                    
  
  if(@ACTIVITY_ID=1)  
    BEGIN  
 SET @OLD_CLAIM_RESERVE_AMOUNT = 0  
  
 SELECT @CURRENT_CLAIM_RESERVE_AMOUNT = SUM(ISNULL(RI_RESERVE,0)) FROM CLM_ACTIVITY_RESERVE   
     WHERE CLAIM_ID=@CLAIM_ID AND ACTIVITY_ID=0  
    END  
  ELSE  
    BEGIN  
 SELECT @OLD_CLAIM_RI_RESERVE = SUM(ISNULL(RI_RESERVE,0)) FROM CLM_ACTIVITY_RESERVE   
 WHERE CLAIM_ID=@CLAIM_ID AND ACTIVITY_ID=(SELECT TOP 1 ACTIVITY_ID FROM CLM_ACTIVITY_RESERVE WHERE CLAIM_ID=@CLAIM_ID AND ACTIVITY_ID<@ACTIVITY_ID AND IS_ACTIVE='Y' ORDER BY ACTIVITY_ID DESC)  
 
 SELECT @CURRENT_CLAIM_RESERVE_AMOUNT = SUM(ISNULL(RI_RESERVE,0)) FROM CLM_ACTIVITY_RESERVE   
     WHERE CLAIM_ID=@CLAIM_ID AND ACTIVITY_ID=@ACTIVITY_ID  
    END  
  
  IF(@ACTION_ON_PAYMENT <> @CLOSE_RESERVE)  
    BEGIN  
 SET @RESERVE_AMOUNT = @CURRENT_CLAIM_RESERVE_AMOUNT - @OLD_CLAIM_RI_RESERVE  
    END  
  
            
  if(@ACTIVITY_REASON = @ACTIVITY_RESERVE)   
    begin         
     UPDATE CLM_ACTIVITY SET                             
     RI_RESERVE = @RESERVE_AMOUNT,                     
     PAYMENT_AMOUNT = @PAYMENT_AMOUNT,   
     LAST_UPDATED_DATETIME = @LAST_UPDATED_DATETIME,      
    --@RECOVERIES Added For Itrack Issue #6372/6274.      
     CLAIM_RI_RESERVE = ISNULL(@OLD_CLAIM_RI_RESERVE,0) + isnull(@RESERVE_AMOUNT,0) +  ISNULL(@RECOVERIES,0)   
     WHERE                             
     CLAIM_ID=@CLAIM_ID AND ACTIVITY_ID = @ACTIVITY_ID                  
       
     UPDATE CLM_ACTIVITY SET   
     ESTIMATION_EXPENSES = @EXPENSES,                              
     ESTIMATION_RECOVERY = @RECOVERY  
     WHERE                             
     CLAIM_ID=@CLAIM_ID AND ACTION_ON_PAYMENT=@NEW_RESERVE  
   
  
   end        
END            
  
--go  
--exec Proc_UpdateReinsuarnceActivityReserve  
--rollback tran   
  
  
  



GO

