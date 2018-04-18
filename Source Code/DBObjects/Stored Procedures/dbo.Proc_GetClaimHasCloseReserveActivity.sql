IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetClaimHasCloseReserveActivity]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetClaimHasCloseReserveActivity]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--BEGIN TRAN  
--DROP PROC dbo.Proc_GetClaimHasCloseReserveActivity  
--GO  
/*----------------------------------------------------------                    
Proc Name       : dbo.Proc_GetClaimHasCloseReserveActivity                    
Created by      : Santosh Kumar Gautam                 
Date            : 29 Dec 2010              
Purpose         : To check whether last completed activity is closed reserve
Revison History :                    
Used In         : CLAIM                    
------------------------------------------------------------                    
Date     Review By          Comments                    
------   ------------       -------------------------*/       
-- DROP PROC dbo.Proc_GetClaimHasCloseReserveActivity                
CREATE PROC [dbo].[Proc_GetClaimHasCloseReserveActivity]            
(                    
  @CLAIM_ID     int,
  @HAS_CLOSED_ACTIVITY CHAR(1) ='N' OUT
)                    
AS                    
BEGIN  
            
	DECLARE @ACTION_ON_PAYMENT INT       
	DECLARE @ACTIVITY_REASON   INT 
	      
	      

	SELECT TOP 1 @ACTION_ON_PAYMENT = ACTION_ON_PAYMENT ,@ACTIVITY_REASON=ACTIVITY_REASON
	FROM   CLM_ACTIVITY  
	WHERE  CLAIM_ID = @CLAIM_ID AND IS_ACTIVE='Y' 
	ORDER  BY CREATED_DATETIME DESC

    --  CHECK LAST ACTIVITY IS THE CLOSE RESERVE
    IF(@ACTIVITY_REASON ='11773' AND @ACTION_ON_PAYMENT='167')
        SET @HAS_CLOSED_ACTIVITY='Y'
        


END                    
                  
--GO  
--EXEC Proc_GetClaimHasCloseReserveActivity 2668,2  
--ROLLBACK TRAN  
GO

