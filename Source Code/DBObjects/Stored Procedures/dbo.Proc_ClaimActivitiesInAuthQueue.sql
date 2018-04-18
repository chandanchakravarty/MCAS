IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_ClaimActivitiesInAuthQueue]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_ClaimActivitiesInAuthQueue]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                                        
Proc Name       : dbo.Proc_ClaimActivitiesInAuthQueue                                        
Created by      : Sumit Chhabra                                  
Date            : 06/20/2006                                        
Purpose         : Checks for the existence of activities in authorisation queue for current user      
Revison History :                                        
Used In  : Wolverine                                        
------------------------------------------------------------                                        
Date     Review By          Comments                                        
------   ------------       -------------------------*/                                        
--DROP PROC dbo.Proc_ClaimActivitiesInAuthQueue      
CREATE PROC dbo.Proc_ClaimActivitiesInAuthQueue                                        
(                                        
 @CLAIM_ID     int,      
 @USER_ID int                           
)                                        
AS                                        
BEGIN              
      
DECLARE @AWAITING_AUTHORIZATION INT     
DECLARE @LOB_ID INT     
declare @PAYMENT_LIMIT DECIMAL(20,2)  
DECLARE @RESERVE_LIMIT DECIMAL(20,2)     
declare @ADJUSTER_CODE varchar(10)  
set @AWAITING_AUTHORIZATION = 11803 --lookup unique id for Awaiting Authorization Activity Status  
      
SELECT @LOB_ID=POLICY_LOB FROM POL_CUSTOMER_POLICY_LIST PCPL JOIN CLM_CLAIM_INFO CCI     
 ON PCPL.CUSTOMER_ID = CCI.CUSTOMER_ID AND    
   PCPL.POLICY_ID = CCI.POLICY_ID AND    
   PCPL.POLICY_VERSION_ID = CCI.POLICY_VERSION_ID     
 WHERE    
   CCI.CLAIM_ID=@CLAIM_ID    
  
--SELECT   
--@ADJUSTER_CODE = ADJUSTER_CODE   
--FROM   
--MNT_USER_LIST   
--WHERE   
--USER_ID=@USER_ID  

SELECT @RESERVE_LIMIT=ISNULL(RESERVE_LIMIT,0),@PAYMENT_LIMIT=ISNULL(PAYMENT_LIMIT,0)   
FROM CLM_ADJUSTER_AUTHORITY CAA JOIN CLM_AUTHORITY_LIMIT CAL                                              
ON CAA.LIMIT_ID = CAL.LIMIT_ID LEFT JOIN CLM_ADJUSTER CA ON CA.ADJUSTER_ID = CAA.ADJUSTER_ID                                                                    
WHERE --CA.ADJUSTER_CODE=@ADJUSTER_CODE AND 
CA.USER_ID = @USER_ID AND
CAA.LOB_ID=@LOB_ID AND CAA.IS_ACTIVE='Y'   


DECLARE @RET_VAL Int

SET @RET_VAL = 0   

IF EXISTS(  
		SELECT ACTIVITY_ID FROM CLM_ACTIVITY WHERE   
		CLAIM_ID = @CLAIM_ID AND ACTIVITY_STATUS=@AWAITING_AUTHORIZATION AND   
		ACTIVITY_REASON IN (11773 )  AND   ISNULL(CLAIM_RESERVE_AMOUNT,0) <= ISNULL(@RESERVE_LIMIT,0) 
	)
BEGIN 
	SET @RET_VAL = 1  
END

IF EXISTS(  
		SELECT ACTIVITY_ID FROM CLM_ACTIVITY WHERE   
		CLAIM_ID = @CLAIM_ID AND ACTIVITY_STATUS=@AWAITING_AUTHORIZATION AND   
		ACTIVITY_REASON IN (11775 )  AND   ISNULL(PAYMENT_AMOUNT,0)<=ISNULL(@PAYMENT_LIMIT,0)
	)
BEGIN 
	SET @RET_VAL = 1  
END

IF EXISTS(  
		SELECT ACTIVITY_ID FROM CLM_ACTIVITY WHERE   
		CLAIM_ID = @CLAIM_ID AND ACTIVITY_STATUS=@AWAITING_AUTHORIZATION AND   
		ACTIVITY_REASON IN (11774 )  AND   ISNULL(EXPENSES,0)<=ISNULL(@PAYMENT_LIMIT,0)
	)
BEGIN 
	SET @RET_VAL = 1  
END

RETURN @RET_VAL 
         
END               
    
  
GO

