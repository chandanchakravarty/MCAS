IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_CheckCloseReserveDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_CheckCloseReserveDetails]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--begin tran
--DROP PROC dbo.Proc_CheckCloseReserveDetails
--go
/*----------------------------------------------------------                          
Proc Name       : dbo.Proc_CheckCloseReserveDetails                          
Created by      : Sibin Thomas Philip                          
Date            : 9 July 2009                          
Purpose   : To Check whether there is any existing reserve or not                          
Revison History :                          
Used In  : Wolverine                          
------------------------------------------------------------                          
    
Date     Review By          Comments                          
------   ------------       -------------------------*/                          
--DROP PROC dbo.Proc_CheckCloseReserveDetails                          
CREATE PROC [dbo].[Proc_CheckCloseReserveDetails]                          
(                          
 @CLAIM_ID     int ,
---New Parameter Adedd For Itrack Issue #6372/6274. 
@ACTION_ON_PAYMENT int                               
)                          
AS       
    


DECLARE @OUTSTANDING DECIMAL(25,2) 
DECLARE @RI_RESERVE DECIMAL(25,2)                     
BEGIN                          

 IF(@ACTION_ON_PAYMENT =167)
 BEGIN                     
	SELECT @OUTSTANDING = SUM(ISNULL(OUTSTANDING,0.00)) FROM CLM_ACTIVITY_RESERVE     
	WHERE CLAIM_ID=@CLAIM_ID AND     
	TRANSACTION_ID =(  
    SELECT ISNULL(MAX(CAR.TRANSACTION_ID),0) FROM CLM_ACTIVITY CA      
    INNER JOIN CLM_ACTIVITY_RESERVE CAR       
    ON CA.ACTIVITY_ID = CASE CAR.ACTIVITY_ID  WHEN 0 THEN 1 ELSE CAR.ACTIVITY_ID  END     
    AND CA.CLAIM_ID=CAR.CLAIM_ID     
    WHERE CA.CLAIM_ID=@CLAIM_ID AND CA.ACTIVITY_STATUS =11801  
    )   


IF(@OUTSTANDING > 0.00)    
 BEGIN    
	 SELECT 1    
 END    
 ELSE    
 BEGIN    
	SELECT 0    
 END  

END

---New Parameter Adedd For Itrack Issue #6372/6274.
ELSE IF(@ACTION_ON_PAYMENT =171)
BEGIN
	SELECT @RI_RESERVE = SUM(ISNULL(RI_RESERVE,0.00)) FROM CLM_ACTIVITY_RESERVE     
	WHERE CLAIM_ID=@CLAIM_ID AND     
	TRANSACTION_ID =(  
     SELECT ISNULL(MAX(CAR.TRANSACTION_ID),0) FROM CLM_ACTIVITY CA      
     INNER JOIN CLM_ACTIVITY_RESERVE CAR       
     ON CA.ACTIVITY_ID = CASE CAR.ACTIVITY_ID  WHEN 0 THEN 1 ELSE CAR.ACTIVITY_ID  END     
     AND CA.CLAIM_ID=CAR.CLAIM_ID     
     WHERE CA.CLAIM_ID=@CLAIM_ID AND CA.ACTIVITY_STATUS =11801  
     )
    
 IF(@RI_RESERVE > 0.00)    
 BEGIN    
	SELECT 1    
 END    
 ELSE    
 BEGIN    
	SELECT 0    
 END    
     
 END              
END
--
--go
--exec Proc_CheckCloseReserveDetails 1722
--rollback tran


GO

