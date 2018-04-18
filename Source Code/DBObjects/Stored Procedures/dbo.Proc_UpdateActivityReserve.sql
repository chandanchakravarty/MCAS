IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdateActivityReserve]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdateActivityReserve]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                                                                                        
Proc Name       : dbo.Proc_UpdateActivityReserve                                                                            
Created by      : Sumit Chhabra                                                                                      
Date            : 02/06/2006                                                                                        
Purpose         : Update Reserve Outstanding data at CLM_ACTIVITY table                        
Created by      : Sumit Chhabra                                                                                       
Revison History :                                                                                        
Used In        : Wolverine                                                                                        
------------------------------------------------------------                                                                                        
Date     Review By          Comments                                                                                        
------   ------------       -------------------------*/                                                                                        
-- drop PROC dbo.Proc_UpdateActivityReserve   
CREATE PROC dbo.Proc_UpdateActivityReserve                                                                              
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
          
  SET @ACTIVITY_RESERVE = 11773          
  SET @OLD_CLAIM_RESERVE_AMOUNT = 0           
  SET @OLD_CLAIM_RI_RESERVE = 0     
  SET @NEW_RESERVE = 165 
  SET @CLOSE_RESERVE = 167       
            
  SELECT @RECOVERIES = SUM(ISNULL([RECOVERY],0)) FROM CLM_ACTIVITY WHERE CLAIM_ID=@CLAIM_ID
  SELECT @ACTIVITY_REASON = ACTIVITY_REASON,@ACTION_ON_PAYMENT=ACTION_ON_PAYMENT FROM CLM_ACTIVITY WHERE CLAIM_ID=@CLAIM_ID AND ACTIVITY_ID=@ACTIVITY_ID                  
          
--  SELECT TOP 1 @OLD_CLAIM_RESERVE_AMOUNT = CLAIM_RESERVE_AMOUNT,@OLD_CLAIM_RI_RESERVE=CLAIM_RI_RESERVE FROM CLM_ACTIVITY WHERE CLAIM_ID=@CLAIM_ID ORDER BY ACTIVITY_ID DESC          

--  SELECT @OLD_CLAIM_RESERVE_AMOUNT = SUM(ISNULL(OUTSTANDING,0)) FROM CLM_ACTIVITY_RESERVE 
--  WHERE CLAIM_ID=@CLAIM_ID AND ACTIVITY_ID=(SELECT TOP 1 ACTIVITY_ID FROM CLM_ACTIVITY_RESERVE WHERE CLAIM_ID=@CLAIM_ID AND ACTIVITY_ID<@ACTIVITY_ID AND IS_ACTIVE='Y' ORDER BY ACTIVITY_ID DESC)

  if(@ACTIVITY_ID=1)
    BEGIN
	SET @OLD_CLAIM_RESERVE_AMOUNT = 0

	SELECT @CURRENT_CLAIM_RESERVE_AMOUNT = SUM(ISNULL(OUTSTANDING,0)) FROM CLM_ACTIVITY_RESERVE 
    	WHERE CLAIM_ID=@CLAIM_ID AND ACTIVITY_ID=0
    END
  ELSE
    BEGIN
	SELECT @OLD_CLAIM_RESERVE_AMOUNT = SUM(ISNULL(OUTSTANDING,0)) FROM CLM_ACTIVITY_RESERVE 
	WHERE CLAIM_ID=@CLAIM_ID AND ACTIVITY_ID=(SELECT TOP 1 ACTIVITY_ID FROM CLM_ACTIVITY_RESERVE WHERE CLAIM_ID=@CLAIM_ID AND ACTIVITY_ID<@ACTIVITY_ID AND IS_ACTIVE='Y' ORDER BY ACTIVITY_ID DESC)

	SELECT @CURRENT_CLAIM_RESERVE_AMOUNT = SUM(ISNULL(OUTSTANDING,0)) FROM CLM_ACTIVITY_RESERVE 
    	WHERE CLAIM_ID=@CLAIM_ID AND ACTIVITY_ID=@ACTIVITY_ID
    END

  IF(@ACTION_ON_PAYMENT <> @CLOSE_RESERVE)
    BEGIN
	SET @RESERVE_AMOUNT = @CURRENT_CLAIM_RESERVE_AMOUNT - @OLD_CLAIM_RESERVE_AMOUNT
    END

  SELECT TOP 1 @OLD_CLAIM_RI_RESERVE=ISNULL(CLAIM_RI_RESERVE,0)
  FROM CLM_ACTIVITY WHERE CLAIM_ID= @CLAIM_ID AND ACTIVITY_ID< @ACTIVITY_ID AND IS_ACTIVE ='Y'
  ORDER BY ACTIVITY_ID DESC 
          
  if(@ACTIVITY_REASON = @ACTIVITY_RESERVE) --AND @ACTION_ON_PAYMENT=@NEW_RESERVE)                  
    begin       
     UPDATE CLM_ACTIVITY SET                           
     RESERVE_AMOUNT = @RESERVE_AMOUNT,                   
 --  CLAIM_RESERVE_AMOUNT = @RESERVE_AMOUNT,                           
     CLAIM_RESERVE_AMOUNT = ISNULL(@OLD_CLAIM_RESERVE_AMOUNT,0) +  ISNULL(@RESERVE_AMOUNT,0) + ISNULL(@RECOVERIES,0),
     PAYMENT_AMOUNT = @PAYMENT_AMOUNT, 
     LAST_UPDATED_DATETIME = @LAST_UPDATED_DATETIME,          
     RI_RESERVE = @RI_RESERVE,          
     CLAIM_RI_RESERVE = ISNULL(@OLD_CLAIM_RI_RESERVE,0) + ISNULL(@RI_RESERVE,0)          
     WHERE                           
     CLAIM_ID=@CLAIM_ID AND ACTIVITY_ID = @ACTIVITY_ID                
     
     UPDATE CLM_ACTIVITY SET 
     ESTIMATION_EXPENSES = @EXPENSES,                            
     ESTIMATION_RECOVERY = @RECOVERY
     WHERE                           
     CLAIM_ID=@CLAIM_ID AND ACTION_ON_PAYMENT=@NEW_RESERVE
 
--   EXEC Proc_CompleteClaimActivity @CLAIM_ID,@ACTIVITY_ID                  
   end      
/*       
else            
begin            
  UPDATE                           
  CLM_ACTIVITY                                             
 SET                           
  RESERVE_AMOUNT = @RESERVE_AMOUNT,                   
--  CLAIM_RESERVE_AMOUNT = @RESERVE_AMOUNT,                    
 CLAIM_RESERVE_AMOUNT = ISNULL(@OLD_CLAIM_RESERVE_AMOUNT,0) + @RESERVE_AMOUNT,                           
  CLAIM_RI_RESERVE = ISNULL(@OLD_CLAIM_RI_RESERVE,0) + ISNULL(@RI_RESERVE,0),          
  LAST_UPDATED_DATETIME = @LAST_UPDATED_DATETIME,          
 RI_RESERVE = @RI_RESERVE                           
 WHERE              
  CLAIM_ID=@CLAIM_ID  AND                        
  ACTIVITY_ID=@ACTIVITY_ID            
            
  UPDATE                           
  CLM_ACTIVITY                    
  SET                           
  ESTIMATION_EXPENSES = @EXPENSES,                            
  ESTIMATION_RECOVERY = @RECOVERY,                            
  PAYMENT_AMOUNT = @PAYMENT_AMOUNT            
 WHERE                           
  CLAIM_ID=@CLAIM_ID  AND                        
  ACTIVITY_REASON = @ACTIVITY_RESERVE and ACTION_ON_PAYMENT=@NEW_RESERVE          
            
end           
*/          
END          









GO

