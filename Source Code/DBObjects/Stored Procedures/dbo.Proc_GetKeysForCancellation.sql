IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetKeysForCancellation]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetKeysForCancellation]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------  
Proc Name       : dbo.Proc_GetKeysForCancellation  
Created by      : Ravindra  
Date            : 12-30-2006  
Purpose     :   
Revison History :  
Used In  : Wolverine  

modified by 	:Pravesh k Chandel
modified Date	:6 sep 2007
Purpose		: change process status condition for process id while fetching  @CANC_TYPE and @CANC_OPT
  
exec dbo.Proc_GetKeysForCancellation 1448,1,1
  
------------------------------------------------------------  
Date     Review By          Comments  
------   ------------       -------------------------*/  
-- drop proc dbo.Proc_GetKeysForCancellation  
CREATE PROC [dbo].[Proc_GetKeysForCancellation]  
(  
	@CUSTOMER_ID     Int,  
	@POLICY_ID    INT,  
	@POLICY_VERSION_ID     Int   
)   
AS  
BEGIN  


DECLARE @TODAY Datetime 
SET @TODAY = GETDATE() 
exec Proc_ReconCustomerOpenItem @TODAY , 0 , @CUSTOMER_ID ,@POLICY_ID


-- Cancellation Type & Cancellation type Need to be fetched from DB after implementation of   
-- cancellation process  
DECLARE @BASE_POLICY_VERSION  Int,  
		@BASE_PROCESS_ID  Int,  
		@PROCESS_PAYMENT_CODE Varchar(10),  
		@PYMT_RECD Smallint,
		@CANC_TYPE  INT,
		@CANC_OPT INT,
		@CURRENT_TERM Int 


SELECT @CURRENT_TERM =  CURRENT_TERM FROM POL_CUSTOMER_POLICY_LIST WHERE CUSTOMER_ID= @CUSTOMER_ID  
AND POLICY_ID=@POLICY_ID   and policy_version_id = @POLICY_VERSION_ID
  
SELECT  @BASE_POLICY_VERSION   = NEW_POLICY_VERSION_ID ,  
@BASE_PROCESS_ID  = PROCESS_ID ,  
@PROCESS_PAYMENT_CODE =   
	CASE PROCESS_ID WHEN 25 THEN 'NBS'  
	WHEN 18 THEN 'REN'  
	END  
FROM POL_POLICY_PROCESS  (NOLOCK) 
WHERE   
CUSTOMER_ID= @CUSTOMER_ID   
AND POLICY_ID=@POLICY_ID   
AND PROCESS_ID IN (25,18)   -- New Business Commit & Renewal Commit  
AND PROCESS_STATUS ='COMPLETE'  
AND ISNULL(REVERT_BACK,'N') <> 'Y'
AND NEW_POLICY_VERSION_ID IN   
(  
	SELECT MAX(NEW_POLICY_VERSION_ID) FROM POL_POLICY_PROCESS ( NOLOCK) 
	WHERE  
	CUSTOMER_ID= @CUSTOMER_ID  
	AND POLICY_ID=@POLICY_ID  
	AND PROCESS_ID IN (25,18)  
	AND PROCESS_STATUS ='COMPLETE'  
	AND ISNULL(REVERT_BACK,'N') <> 'Y'
)  

-- Ravindra(10-11-2007) : Due to change in implementation now, discount is applied to installment 
-- as soon as process is committed need to exclude discount transactions while checking payment recieved
-- If premium paid is greater than discount applied 
-- Ravindra(06-20-2008): Write not be considered as Payments
IF(
	ISNULL(
		(SELECT ISNULL(SUM(ISNULL(TOTAL_PAID,0) - ISNULL(WRITE_OFF_AMOUNT,0)  ),0) 
		FROM ACT_CUSTOMER_OPEN_ITEMS 
		WHERE CUSTOMER_ID = @CUSTOMER_ID 
		AND POLICY_ID = @POLICY_ID
		AND POLICY_VERSION_ID IN (SELECT POLICY_VERSION_ID FROM POL_CUSTOMER_POLICY_LIST WHERE 
						CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID 
						AND CURRENT_TERM = @CURRENT_TERM )
		AND ITEM_TRAN_CODE <> 'DISC'
		AND ITEM_TRAN_CODE_TYPE = 'PREM' 
		AND UPDATED_FROM 	= 'P') 
		, 0
		)
	>
	ISNULL(
		(SELECT ISNULL(SUM(ISNULL(TOTAL_PAID,0)),0)  * -1
		FROM ACT_CUSTOMER_OPEN_ITEMS 
		WHERE CUSTOMER_ID = @CUSTOMER_ID 
		AND POLICY_ID = @POLICY_ID
		AND POLICY_VERSION_ID IN (SELECT POLICY_VERSION_ID FROM POL_CUSTOMER_POLICY_LIST WHERE 
						CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID 
						AND CURRENT_TERM = @CURRENT_TERM )
		AND ITEM_TRAN_CODE = 'DISC') 
		, 0
		) 
	)
--
--IF EXISTS (SELECT IDEN_ROW_ID  FROM ACT_CUSTOMER_OPEN_ITEMS   ( NOLOCK)
--	WHERE  CUSTOMER_ID = @CUSTOMER_ID   
--	AND POLICY_ID = @POLICY_ID   
--	AND POLICY_VERSION_ID = @POLICY_VERSION_ID   
--	AND ISNULL(TOTAL_PAID ,0) <> 0  
--	AND ITEM_TRAN_CODE <> 'DISC'  )
BEGIN   
	SET @PYMT_RECD = 1  
	SET @PROCESS_PAYMENT_CODE =  @PROCESS_PAYMENT_CODE  + '-PTRCD'  
END  
ELSE   
BEGIN   
	SET @PYMT_RECD = 0  
	SET @PROCESS_PAYMENT_CODE =  @PROCESS_PAYMENT_CODE  + '-NPT'  
END  

  

SELECT @CANC_TYPE=ISNULL(CANCELLATION_TYPE,0),@CANC_OPT=ISNULL(CANCELLATION_OPTION,0) 
FROM POL_POLICY_PROCESS (NOLOCK)
WHERE  CUSTOMER_ID = @CUSTOMER_ID   
AND POLICY_ID = @POLICY_ID   
--AND NEW_POLICY_VERSION_ID = @POLICY_VERSION_ID   
AND POLICY_VERSION_ID = @POLICY_VERSION_ID   
AND PROCESS_ID  IN(2,28) ---IN(12,29)
  
SELECT  @CANC_TYPE AS CANC_TYPE , 
 @CANC_OPT AS CANC_OPT ,  
 @PROCESS_PAYMENT_CODE AS PROCESS_PAYMENT_CODE,   
 @PYMT_RECD As PMT_RCD,  
 @BASE_PROCESS_ID As BASE_PROCESS_ID,  
 APP_EFFECTIVE_DATE AS APP_EFFECTIVE_DATE,  
 STATE_ID AS STATE_ID,  
 BILL_TYPE  As BILL_TYPE  
FROM POL_CUSTOMER_POLICY_LIST   (NOLOCK)
WHERE  CUSTOMER_ID = @CUSTOMER_ID   
 AND POLICY_ID = @POLICY_ID   
 AND POLICY_VERSION_ID = @POLICY_VERSION_ID   
  
  
END


GO

