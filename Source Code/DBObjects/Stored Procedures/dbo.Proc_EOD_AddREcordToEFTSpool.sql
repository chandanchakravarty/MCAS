IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_EOD_AddREcordToEFTSpool]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_EOD_AddREcordToEFTSpool]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--begin tran
--drop proc dbo.Proc_EOD_AddREcordToEFTSpool
--go
/*----------------------------------------------------------  
Proc Name        : dbo.Proc_EOD_AddREcordToEFTSpool
Created by       : Ravinda Gupta 
Date             : 1-19-2007
Purpose      	 : Will add a record to EFT to EFT Spool Table
Revison History :  
Used In   :Wolverine  
-----------------------------------------------------------------------------------------------------------------------
Date		Review By          Comments  
06/30/2009	Shikha Dixit	   Chnaged Processed field from 'N' to null.
-----------------------------------------------------------------------------------------------------------------------*/  
-- drop proc dbo.Proc_EOD_AddREcordToEFTSpool
CREATE PROC [dbo].[Proc_EOD_AddREcordToEFTSpool]
(	
	@ENTITY_ID Int,
	@ENTITY_TYPE Varchar(10),
	@POLICY_NUMBER Varchar(15),
	@POLICY_ID Int,
	@POLICY_VERSION_ID SmallInt,
	@REF_CHECK_ID Int,
	@REF_DEPOSIT_ID Int,
	@REF_DEP_DETAIL_ID Int,
	@TRANSACTION_AMOUNT Decimal(18,2),
	@TRANSACTION_CODE Char (2),
	@ACC_TRAN_REQUIRED Char,
	@ACCOUNT_TO_USE SmallInt ,
	@BRICS_TRAN_TYPE	Int
)
AS
BEGIN 
	IF(@POLICY_NUMBER IS NULL )
	BEGIN 
--Upper Policy_Number For Itrack Issue 6371
		SELECT @POLICY_NUMBER = UPPER(POLICY_NUMBER)   FROM POL_CUSTOMER_POLICY_LIST PCPL 
		WHERE CUSTOMER_ID = @ENTITY_ID 
		AND POLICY_ID = @POLICY_ID 
		AND POLICY_VERSION_ID = @POLICY_VERSION_ID 
	END
	ELSE IF(@POLICY_ID IS NULL)
	BEGIN 
		SELECT TOP 1 @POLICY_ID = POLICY_ID ,@POLICY_VERSION_ID = POLICY_VERSION_ID
		FROM 
		POL_CUSTOMER_POLICY_LIST 
		WHERE CUSTOMER_ID = @ENTITY_ID 
		AND POLICY_NUMBER = @POLICY_NUMBER 
		ORDER BY POLICY_VERSION_ID DESC 
	END
		
	INSERT INTO EOD_EFT_SPOOL(
		ENTITY_ID,ENTITY_TYPE,POLICY_NUMBER,POLICY_ID,POLICY_VERSION_ID,REF_CHECK_ID,REF_DEPOSIT_ID,
		REF_DEP_DETAIL_ID,TRANSACTION_AMOUNT,TRANSACTION_CODE,ACC_TRAN_REQUIRED,ACCOUNT_TO_USE,
		CREATED_DATETIME,PROCESSED , BRICS_TRAN_TYPE
		)
	VALUES(
--Upper Policy_Number For Itrack Issue 6371
		@ENTITY_ID,@ENTITY_TYPE,UPPER(@POLICY_NUMBER),@POLICY_ID,@POLICY_VERSION_ID,@REF_CHECK_ID,@REF_DEPOSIT_ID,
		@REF_DEP_DETAIL_ID,@TRANSACTION_AMOUNT,@TRANSACTION_CODE,@ACC_TRAN_REQUIRED,@ACCOUNT_TO_USE,
		GETDATE(),NULL , @BRICS_TRAN_TYPE
		)		


	RETURN 1	
END

--go 
--exec Proc_EOD_AddREcordToEFTSpool 619,'cust','R1000184',1,2,null,null,null,58.25,12,'n',1,104
--rollback tran













GO

