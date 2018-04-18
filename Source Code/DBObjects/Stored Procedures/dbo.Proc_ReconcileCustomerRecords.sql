IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_ReconcileCustomerRecords]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_ReconcileCustomerRecords]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*                                                     
----------------------------------------------------------                                                                  
Proc Name       : dbo.Proc_ReconcileCustomerRecords 
Created by      : Ravindra                                                                
Date            : July - 17 -2006
Purpose         : 
Revison History :                                                                  
Used In         : Wolverine              
------------------------------------------------------------                                                                  
Date     Review By          Comments                                                                  
------   ------------       -------------------------                                                                 
*/                                                        
 
-- drop proc  dbo.Proc_ReconcileCustomerRecords
CREATE PROCEDURE dbo.Proc_ReconcileCustomerRecords 
(
	@TRAN_EFFECTIVE_DATE  	DATETIME =NULL ,
	@USER_ID	      	INTEGER  =NULL,
	@CUST_ID 		INTEGER  =NULL,
	@POL_ID 		INTEGER  =NULL

)
AS
BEGIN
	
	IF(@TRAN_EFFECTIVE_DATE  IS NULL)
	BEGIN 
		SET @TRAN_EFFECTIVE_DATE  =GETDATE()
	END 

	CREATE TABLE #ITEMS_TO_RECON
	(
		[IDENT_COL] [int] IDENTITY(1,1) NOT NULL ,     
		IDEN_ROW_ID int,
		TOTAL_PAID DECIMAL(18,2),
		TOTAL_DUE DECIMAL(18,2)
	)

	CREATE TABLE #POLICY_TO_RECON
	(
		[IDENT_COL] [int] IDENTITY(1,1) NOT NULL ,     
		IDEN_ROW_ID INTEGER,
		CUSTOMER_ID INTEGER,			
		POLICY_ID INTEGER,
		POLICY_VERSION_ID INTEGER,
		TOTAL_PAID DECIMAL(18,2),
		TOTAL_DUE DECIMAL(18,2)
	)
	
	if(@CUST_ID is null)
	begin
		--- Select All Policies which have Negative Open items 
		INSERT INTO #POLICY_TO_RECON 	 
		(
			IDEN_ROW_ID ,
			CUSTOMER_ID ,			
			POLICY_ID ,
			POLICY_VERSION_ID ,
			TOTAL_PAID ,
			TOTAL_DUE 
		)
		SELECT 	IDEN_ROW_ID ,CUSTOMER_ID,POLICY_ID ,POLICY_VERSION_ID ,TOTAL_PAID,TOTAL_DUE
		FROM ACT_CUSTOMER_OPEN_ITEMS 
		WHERE 
		SOURCE_EFF_DATE <= @TRAN_EFFECTIVE_DATE 
		--AND  	TOTAL_DUE < 0
		AND   ISNULL(TOTAL_DUE,0)  <> ISNULL(TOTAL_PAID,0)
		ORDER BY SOURCE_EFF_DATE, IDEN_ROW_ID
	end
	else
	begin
		--- Select All Policies which have Negative Open items 
		INSERT INTO #POLICY_TO_RECON 	 
		(
			IDEN_ROW_ID ,
			CUSTOMER_ID ,			
			POLICY_ID ,
			POLICY_VERSION_ID ,
			TOTAL_PAID ,
			TOTAL_DUE 
		)
		SELECT 	IDEN_ROW_ID ,CUSTOMER_ID,POLICY_ID ,POLICY_VERSION_ID ,isnull(TOTAL_PAID,0),isnull(TOTAL_DUE,0)
		FROM ACT_CUSTOMER_OPEN_ITEMS 
		WHERE 
		SOURCE_EFF_DATE <= @TRAN_EFFECTIVE_DATE 
--		AND ISNULL(TOTAL_DUE,0)  <> ISNULL(TOTAL_PAID,0)
		AND  	
		(
			(ISNULL(TOTAL_DUE,0) < 0 AND  ISNULL(TOTAL_DUE,0)  <> ISNULL(TOTAL_PAID,0))
				OR
			(ISNULL(TOTAL_DUE,0) - ISNULL(TOTAL_PAID,0) < 0)
		
		)
	 	AND CUSTOMER_ID = @CUST_ID
	 	--AND POLICY_ID = @POL_ID
		ORDER BY SOURCE_EFF_DATE, IDEN_ROW_ID
	end
 



	DECLARE @CUSTOMER_ID INTEGER, @POLICY_ID INT, @POLICY_VERSION_ID INT

	DECLARE @IDENT_COL INTEGER
	DECLARE @IDENT_COL_INNER INTEGER
	DECLARE @RECON_AMOUNT DECIMAL(18,2), @INNER_RECON_AMOUNT DECIMAL(18,2)
	DECLARE @IDEN_ROW_ID INT,@IDEN_ROW_ID_INNER INT
	declare @GROUP_ID int, @TOTAL_PAID_TO_UPDATE DECIMAL(18,2), @RECONCILE_TO_UPDATE DECIMAL(18,2)

	set @IDENT_COL = 1
	WHILE EXISTS 
		(                
	    	SELECT IDENT_COL FROM #POLICY_TO_RECON               
	    	WHERE IDENT_COL = @IDENT_COL                
	    	)               
	BEGIN     
		select @GROUP_ID = ISNULL(MAX(GROUP_ID), 0) + 1 FROM ACT_RECONCILIATION_GROUPS



		SELECT 
			@CUSTOMER_ID = CUSTOMER_ID, @POLICY_ID = POLICY_ID,
			@RECON_AMOUNT = ISNULL(TOTAL_DUE,0) - ISNULL(TOTAL_PAID,0),
			@IDEN_ROW_ID = IDEN_ROW_ID
		FROM #POLICY_TO_RECON
		WHERE IDENT_COL = @IDENT_COL

		

		TRUNCATE TABLE #ITEMS_TO_RECON

		INSERT INTO #ITEMS_TO_RECON
		(
			IDEN_ROW_ID,
			TOTAL_PAID ,
			TOTAL_DUE 
		)
		SELECT IDEN_ROW_ID, TOTAL_PAID, TOTAL_DUE 
		FROM ACT_CUSTOMER_OPEN_ITEMS 
		WHERE 
		ISNULL(TOTAL_DUE,0) > 0 AND
		ISNULL(TOTAL_DUE,0) <> ISNULL(TOTAL_PAID,0)
		AND CUSTOMER_ID = @CUSTOMER_ID 
		--AND POLICY_ID = @POLICY_ID
		ORDER BY SOURCE_EFF_DATE, IDEN_ROW_ID

		
		IF NOT EXISTS( SELECT IDEN_ROW_ID FROM #ITEMS_TO_RECON )
		BEGIN
			SET @IDENT_COL = @IDENT_COL + 1  
			Continue
		END 

		INSERT INTO ACT_RECONCILIATION_GROUPS
		(
			GROUP_ID, RECON_ENTITY_ID, RECON_ENTITY_TYPE, IS_COMMITTED,
			DATE_COMMITTED, COMMITTED_BY, IS_ACTIVE, CREATED_BY, CREATED_DATETIME,
			MODIFIED_BY, LAST_UPDATED_DATETIME, CD_LINE_ITEM_ID

		)
		VALUES
		(
			@GROUP_ID, @CUSTOMER_ID, 'CUST', 'Y', 
			GETDATE(), @USER_ID, 'Y', @USER_ID, GETDATE(),
			NULL, NULL, NULL
		)
		--print @GROUP_ID

		SET @TOTAL_PAID_TO_UPDATE =0 
		SET @IDENT_COL_INNER =1
		
		WHILE EXISTS ( SELECT IDEN_ROW_ID FROM #ITEMS_TO_RECON 
				WHERE IDENT_COL = @IDENT_COL_INNER   )
		BEGIN
			if (@RECON_AMOUNT = 0)
				break
			
			SELECT 
				@INNER_RECON_AMOUNT = ISNULL(TOTAL_DUE,0) - ISNULL(TOTAL_PAID,0),
				@IDEN_ROW_ID_INNER = IDEN_ROW_ID
			FROM #ITEMS_TO_RECON
			WHERE IDENT_COL = @IDENT_COL_INNER 


			IF isnull(@RECON_AMOUNT,0) * -1 <= isnull(@INNER_RECON_AMOUNT,0)
			BEGIN
				
				SELECT 
					@TOTAL_PAID_TO_UPDATE = ISNULL(TOTAL_PAID,0) + (isnull(@RECON_AMOUNT,0) * -1),
					@RECONCILE_TO_UPDATE = isnull(@RECON_AMOUNT,0) * -1
				FROM ACT_CUSTOMER_OPEN_ITEMS
				WHERE IDEN_ROW_ID = @IDEN_ROW_ID_INNER

				
				SET @RECON_AMOUNT = 0
				
			END
			ELSE
			BEGIN


				SELECT 
					@TOTAL_PAID_TO_UPDATE = ISNULL(TOTAL_DUE,0),
					@RECONCILE_TO_UPDATE = ISNULL(TOTAL_DUE,0) - ISNULL(TOTAL_PAID,0)
				FROM ACT_CUSTOMER_OPEN_ITEMS
				WHERE IDEN_ROW_ID = @IDEN_ROW_ID_INNER
				
				SET @RECON_AMOUNT = (isnull(@RECON_AMOUNT,0) + isnull(@INNER_RECON_AMOUNT,0)) 
			END
			

			INSERT INTO ACT_CUSTOMER_RECON_GROUP_DETAILS
			(
				GROUP_ID, ITEM_TYPE, ITEM_REFERENCE_ID, SUB_LEDGER_TYPE, RECON_AMOUNT,
				NOTE, DIV_ID, DEPT_ID, PC_ID
			)
			VALUES
			(
				@GROUP_ID, 'CUST', @IDEN_ROW_ID_INNER, 'CUST', @RECONCILE_TO_UPDATE,
				NULL, NULL, NULL, NULL
			)

			UPDATE ACT_CUSTOMER_OPEN_ITEMS
			SET TOTAL_PAID = @TOTAL_PAID_TO_UPDATE
			WHERE IDEN_ROW_ID = @IDEN_ROW_ID_INNER
	
			--@RECON_AMOUNT 
			SET @IDENT_COL_INNER = @IDENT_COL_INNER + 1
		END


		SELECT @TOTAL_PAID_TO_UPDATE = (ISNULL(TOTAL_DUE,0) - isnull(@RECON_AMOUNT,0))
		FROM ACT_CUSTOMER_OPEN_ITEMS
		WHERE IDEN_ROW_ID = @IDEN_ROW_ID

		INSERT INTO ACT_CUSTOMER_RECON_GROUP_DETAILS
		(
			GROUP_ID, ITEM_TYPE, ITEM_REFERENCE_ID, SUB_LEDGER_TYPE, RECON_AMOUNT,
			NOTE, DIV_ID, DEPT_ID, PC_ID
		)
		VALUES
		(
			@GROUP_ID, 'CUST', @IDEN_ROW_ID, 'CUST', @TOTAL_PAID_TO_UPDATE,
			NULL, NULL, NULL, NULL
		)

		UPDATE ACT_CUSTOMER_OPEN_ITEMS
		SET TOTAL_PAID =  @TOTAL_PAID_TO_UPDATE
		WHERE IDEN_ROW_ID = @IDEN_ROW_ID
		
		--SET @RECON_AMOUNT = (@RECON_AMOUNT * -1) - @INNER_RECON_AMOUNT			
		SET @IDENT_COL = @IDENT_COL + 1                
	END  --- End While Loop               



	DROP TABLE #POLICY_TO_RECON
	DROP TABLE #ITEMS_TO_RECON

END 









GO

