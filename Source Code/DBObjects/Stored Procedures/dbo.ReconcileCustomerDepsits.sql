IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ReconcileCustomerDepsits]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[ReconcileCustomerDepsits]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

 

/*----------------------------------------------------------------------  
Created By  : Ravindra Gupta  
Created Date  : June 04 2007  
Purpose   :   
  
Modified By   	: Ravindra   
Modified On  	: 11/13/2007  
Purpose   	: To resolve overpayment issue.
	     		Bug details: System is llloking for all committed deposits against this policy and 
			reconciling all line items because of this line items for other policies are also getting
			reconciled again. This nees to be done for Deposit Line items of current policies only.
  
****************************************************************************************/  
-- drop proc dbo.ReconcileCustomerDepsits  
  
CREATE proc  dbo.ReconcileCustomerDepsits  
 (  
 @CUSTOMER_ID   int,  		-- Id of customer whose premium policy will be posted  
 @POLICY_ID  int,   		-- Policy identification number  
 @POLICY_VERSION_ID int   	-- Policy version identification number  
)  
AS  
BEGIN  
DECLARE @FISCAL_ID SMALLINT  

SELECT @FISCAL_ID = FISCAL_ID
FROM ACT_GENERAL_LEDGER 
WHERE CAST(CONVERT(VARCHAR,FISCAL_BEGIN_DATE ,101 ) AS DATETIME) <= 
		CAST(CONVERT(VARCHAR,GETDATE() ,101 ) AS DATETIME) 
	AND CAST(CONVERT(VARCHAR,FISCAL_END_DATE ,101 ) AS DATETIME) >=
		CAST(CONVERT(VARCHAR,GETDATE() ,101 ) AS DATETIME) 


--Changed By Ravinda to handle multiple Deposits  
--If there are some payments against this policy  
--then reconciling the payments against the open items  
IF EXISTS ( 
		SELECT CD.DEPOSIT_ID FROM ACT_CURRENT_DEPOSIT_LINE_ITEMS CDLI  
		INNER JOIN ACT_CURRENT_DEPOSITS CD ON CD.DEPOSIT_ID = CDLI.DEPOSIT_ID  
		WHERE CDLI.CUSTOMER_ID = @CUSTOMER_ID  
		AND CDLI.POLICY_ID = @POLICY_ID   
		AND ISNULL(CD.IS_COMMITED,'N') = 'Y' 
	)  
BEGIN  
	DECLARE @DEPOSIT_ID 		INT, 
		@COMMITTED_BY 		INT, 
		@COMMITED_BY_CODE 	VARCHAR(30), 
		@COMMITED_BY_NAME 	VARCHAR(50)  ,
		@CD_LINE_ITEM_ID	Int
	
	DECLARE @TEMP_DEPOSIT_LIST TABLE                
	(                
		IDENT_COL 	INT IDENTITY (1,1),                
		DEPOSIT_ID 	INT,           
		CD_LINE_ITEM_ID	INt,
		COMMITTED_BY 	INT                
	)                
  
	--Extracting the deposit information  
	INSERT INTO @TEMP_DEPOSIT_LIST(DEPOSIT_ID, CD_LINE_ITEM_ID, COMMITTED_BY)  
	SELECT   CDLI.DEPOSIT_ID, CDLI.CD_LINE_ITEM_ID , CDLI.MODIFIED_BY  
	FROM ACT_CURRENT_DEPOSIT_LINE_ITEMS CDLI 
	INNER JOIN ACT_CURRENT_DEPOSITS CD 
	ON CD.DEPOSIT_ID = CDLI.DEPOSIT_ID  
	INNER JOIN ACT_CUSTOMER_OPEN_ITEMS OI
	ON OI.CUSTOMER_ID = CDLI.CUSTOMER_ID 
	AND OI.POLICY_ID = CDLI.POLICY_ID 
	AND OI.UPDATED_FROM = 'D'
	AND OI.SOURCE_ROW_ID = CDLI.CD_LINE_ITEM_ID 
	AND ISNULL(OI.TOTAL_DUE , 0 )  <>  ISNULL(OI.TOTAL_PAID , 0) 
	WHERE CDLI.CUSTOMER_ID = @CUSTOMER_ID  
	AND CDLI.POLICY_ID = @POLICY_ID   
	AND ISNULL(CD.IS_COMMITED , 'N') = 'Y'
	ORDER BY CDLI.DEPOSIT_ID  

	DECLARE @IDENT_COL INT                
	SET  @IDENT_COL = 1      
	WHILE(1=1)  
	BEGIN  
		IF NOT EXISTS                
		(                
		SELECT IDENT_COL FROM @TEMP_DEPOSIT_LIST               
		WHERE IDENT_COL = @IDENT_COL                
		)                
		BEGIN                
			BREAK                
		END  
   
  
		--Extracting the deposit information  
		SELECT   
		@DEPOSIT_ID = DEPOSIT_ID,  
		@COMMITTED_BY = COMMITTED_BY  ,
		@CD_LINE_ITEM_ID = CD_LINE_ITEM_ID
		FROM @TEMP_DEPOSIT_LIST  
		WHERE IDENT_COL = @IDENT_COL  

		--Extracting the user information  
		SELECT @COMMITED_BY_CODE = USER_LOGIN_ID, @COMMITED_BY_NAME = IsNull(USER_FNAME,'') + ' ' + IsNull(USER_LNAME,'')  
		FROM MNT_USER_LIST   
		WHERE [USER_ID] = @COMMITTED_BY    
  
		--Reconciling and posting the deposit  
		EXEC Proc_PostAndReconcileCustomerDeposit @DEPOSIT_ID, @FISCAL_ID, @COMMITTED_BY,   
			@COMMITED_BY_CODE, @COMMITED_BY_NAME,2 , -- 2 For Policy Process 
			@CD_LINE_ITEM_ID 	 	
    
 		SET @IDENT_COL = @IDENT_COL +1  
	END  
END 
 
END  
  
  

GO

