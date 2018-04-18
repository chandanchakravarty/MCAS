IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_FetchDepositNumberOnId]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_FetchDepositNumberOnId]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

 /*----------------------------------------------------------        
Proc Name       : Proc_FetchDepositNumberOnId        
Created by      : kranti      
Date            : 8th june 2007      
Purpose        : Fetch Deposit Number based On deposit Id     
Revison History :        
      
------------------------------------------------------------        
Date     Review By          Comments        
Modified by Praveen kasana
25 March 2008
------   ------------       --------------------------------*/        
-- DROP PROC dbo.Proc_FetchDepositNumberOnId        
CREATE PROCEDURE dbo.Proc_FetchDepositNumberOnId        
(        
 @DEPOSIT_ID  INT  ,   
 @DEPOSIT_TYPE varchar(20) = null,
 @POLICY_NUMBER varchar(20) = null 
)      
AS      
BEGIN   

DECLARE @DEPOSIT_NUMBER varchar(20)
DECLARE @CUSTOMER_ID INT 
DECLARE @POLICY_ID INT
DECLARE @POLICY_VERSION_ID INT 

SET @CUSTOMER_ID = 0
SET @POLICY_ID = 0
SET @POLICY_VERSION_ID = 0

SELECT @DEPOSIT_NUMBER = DEPOSIT_NUMBER FROM ACT_CURRENT_DEPOSITS  with(nolock)
WHERE DEPOSIT_ID = @DEPOSIT_ID  

IF(@DEPOSIT_TYPE = 'CUST')
BEGIN
	---Ravindra(07-09-09): Fetch version which is not in progress
--	SELECT       
--			@POLICY_ID = IsNull(POLICY_ID,0), @POLICY_VERSION_ID = MAX(POLICY_VERSION_ID),      
--			@CUSTOMER_ID = CUSTOMER_ID      
--			FROM POL_CUSTOMER_POLICY_LIST  with(nolock)   
--			WHERE POLICY_NUMBER = @POLICY_NUMBER AND BILL_TYPE = 'DB'      
--			GROUP BY IsNull(POLICY_ID,0), IsNull(POLICY_ACCOUNT_STATUS,0), CUSTOMER_ID    

	
	SELECT TOP 1 @POLICY_ID = POL.POLICY_ID, @POLICY_VERSION_ID = POL.POLICY_VERSION_ID, 
	@CUSTOMER_ID = POL.CUSTOMER_ID      
	FROM POL_CUSTOMER_POLICY_LIST POL (NOLOCK)      
	LEFT JOIN POL_POLICY_PROCESS PPP (NOLOCK) ON
	PPP.CUSTOMER_ID = POL.CUSTOMER_ID AND PPP.POLICY_ID = POL.POLICY_ID AND PPP.NEW_POLICY_VERSION_ID =
	POL.POLICY_VERSION_ID   
	WHERE POL.POLICY_NUMBER = @POLICY_NUMBER AND POL.BILL_TYPE = 'DB'
	AND  
	(	
		(ISNULL(PPP.PROCESS_STATUS,'') = 'COMPLETE' 	AND ISNULL(PPP.REVERT_BACK,'N') = 'N' )
		OR 
		POL.POLICY_STATUS IN ( 'SUSPENDED','UISSUE')
	)
	ORDER BY POL.POLICY_VERSION_ID DESC 

END

SELECT @DEPOSIT_NUMBER as DEPOSIT_NUMBER, @CUSTOMER_ID as CUSTOMER_ID , @POLICY_ID as POLICY_ID,@POLICY_VERSION_ID as POLICY_VERSION_ID

END  
  
  
  


GO

