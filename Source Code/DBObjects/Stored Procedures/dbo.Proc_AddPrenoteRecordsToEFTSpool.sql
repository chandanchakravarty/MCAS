IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_AddPrenoteRecordsToEFTSpool]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_AddPrenoteRecordsToEFTSpool]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--BEGIN TRAN 
--drop proc dbo.Proc_AddPrenoteRecordsToEFTSpool 
--go 

/*----------------------------------------------------------  
Proc Name        : dbo.Proc_AddPrenoteRecordsToEFTSpool
Created by       : Ravinda Gupta 
Date             : 5-22-2008
Purpose      	 : 
Revison History  :  
Used In   :Wolverine  
------------------------------------------------------------  
Date     Review By          Comments  
------------------------------------------------------------*/  
-- drop proc dbo.Proc_AddPrenoteRecordsToEFTSpool 
CREATE PROC [dbo].[Proc_AddPrenoteRecordsToEFTSpool]
AS
BEGIN 

	DECLARE @NO Int , @YES	Int, @PRENOTE_TRAN Int 
		
	SET @NO = 10964
	SET @YES = 10963
	SET @PRENOTE_TRAN = 22

	INSERT INTO EOD_EFT_SPOOL 
		( 
		ENTITY_ID , ENTITY_TYPE , POLICY_ID , POLICY_VERSION_ID , TRANSACTION_AMOUNT ,
		TRANSACTION_CODE ,ACC_TRAN_REQUIRED,ACCOUNT_TO_USE , CREATED_DATETIME 
		)
	SELECT AGN.AGENCY_ID ,'AGN' , NULL , NULL , 0 , 
	@PRENOTE_TRAN, 'N' , 1 , GETDATE()
	FROM MNT_AGENCY_LIST AGN
	WHERE ISNULL(AGN.ALLOWS_EFT,@NO) = @YES 
	--AND ISNULL(ACCOUNT_ISVERIFIED1, @NO ) = @NO
	AND ISNULL(AGN.REVERIFIED_AC1 , @NO ) = @YES  
	AND NOT EXISTS ( 
					SELECT SPOOL.IDEN_ROW_ID FROM EOD_EFT_SPOOL SPOOL WHERE 
					SPOOL.ENTITY_ID = AGN.AGENCY_ID AND  SPOOL.ENTITY_TYPE = 'AGN' AND SPOOL.ACCOUNT_TO_USE = 1 
					AND ISNULL(SPOOL.PROCESSED ,'N' ) <> 'Y' AND SPOOL.TRANSACTION_CODE = 22
					)

	INSERT INTO EOD_EFT_SPOOL 
	( 
	ENTITY_ID , ENTITY_TYPE , POLICY_ID , POLICY_VERSION_ID , TRANSACTION_AMOUNT ,
	TRANSACTION_CODE ,ACC_TRAN_REQUIRED,ACCOUNT_TO_USE , CREATED_DATETIME 
	)
	SELECT AGN.AGENCY_ID ,'AGN' , NULL , NULL , 0 , 
	@PRENOTE_TRAN, 'N' , 2 , GETDATE()
	FROM MNT_AGENCY_LIST AGN
	WHERE ISNULL(ALLOWS_CUSTOMER_SWEEP,@NO) = @YES 
	--AND ISNULL(ACCOUNT_ISVERIFIED2,@NO) = @NO
	AND ISNULL(REVERIFIED_AC2 , @NO ) = @YES  
	AND NOT EXISTS ( 
					SELECT SPOOL.IDEN_ROW_ID FROM EOD_EFT_SPOOL SPOOL WHERE 
					SPOOL.ENTITY_ID = AGN.AGENCY_ID AND  SPOOL.ENTITY_TYPE = 'AGN' AND SPOOL.ACCOUNT_TO_USE = 2 
					AND ISNULL(SPOOL.PROCESSED ,'N' ) <> 'Y' AND SPOOL.TRANSACTION_CODE = 22
					)

	INSERT INTO EOD_EFT_SPOOL 
	( 
	ENTITY_ID , ENTITY_TYPE , POLICY_ID , POLICY_VERSION_ID , TRANSACTION_AMOUNT ,
	TRANSACTION_CODE ,ACC_TRAN_REQUIRED,ACCOUNT_TO_USE , CREATED_DATETIME 
	)
	SELECT VEN.VENDOR_ID ,'VEN' , NULL , NULL , 0 , 
	@PRENOTE_TRAN, 'N' , 1 , GETDATE()
	FROM MNT_VENDOR_LIST VEN
	WHERE ISNULL(ALLOWS_EFT,@NO) = @YES 
	--AND ISNULL(ACCOUNT_ISVERIFIED,@NO) = @NO
	AND ISNULL(REVERIFIED_AC,@NO) = @YES
	AND NOT EXISTS ( 
					SELECT SPOOL.IDEN_ROW_ID FROM EOD_EFT_SPOOL SPOOL WHERE 
					SPOOL.ENTITY_ID = VEN.VENDOR_ID AND  SPOOL.ENTITY_TYPE = 'VEN' AND ISNULL(SPOOL.PROCESSED ,'N' ) <> 'Y'
					AND SPOOL.TRANSACTION_CODE = 22
					)

	INSERT INTO EOD_EFT_SPOOL 
	( 
	ENTITY_ID , ENTITY_TYPE , POLICY_ID , POLICY_VERSION_ID , TRANSACTION_AMOUNT ,
	TRANSACTION_CODE ,ACC_TRAN_REQUIRED,ACCOUNT_TO_USE , CREATED_DATETIME , POLICY_NUMBER
	)
	SELECT EFT.CUSTOMER_ID ,'CUST' , EFT.POLICY_ID , EFT.POLICY_VERSION_ID , 0 , 
	@PRENOTE_TRAN, 'N' , 1 , GETDATE() , CPL.POLICY_NUMBER
	FROM ACT_POL_EFT_CUST_INFO EFT
	INNER JOIN POL_CUSTOMER_POLICY_LIST CPL
	ON EFT.CUSTOMER_ID = CPL.CUSTOMER_ID 
	AND EFT.POLICY_ID = CPL.POLICY_ID 
	AND EFT.POLICY_VERSION_ID = CPL.POLICY_VERSION_ID
	WHERE 
	--ISNULL(IS_VERIFIED,@NO) =  @NO AND 
	ISNULL(EFT.REVERIFIED_AC , @NO ) = @YES
	AND NOT EXISTS ( 
					SELECT SPOOL.IDEN_ROW_ID FROM EOD_EFT_SPOOL SPOOL WHERE 
					SPOOL.ENTITY_ID = EFT.CUSTOMER_ID AND  SPOOL.ENTITY_TYPE = 'CUST' AND ISNULL(SPOOL.PROCESSED ,'N' ) <> 'Y'
					AND EFT.POLICY_ID  = SPOOL.POLICY_ID AND SPOOL.TRANSACTION_CODE = 22
					)
	AND  EFT.POLICY_VERSION_ID  = (SELECT MAX(EFT2.POLICY_VERSION_ID)  FROM ACT_POL_EFT_CUST_INFO EFT2 
								WHERE EFT.CUSTOMER_ID = EFT2.CUSTOMER_ID AND EFT.POLICY_ID = EFT2.POLICY_ID )
				
END




--go 
--
--exec Proc_AddPrenoteRecordsToEFTSpool 
--
--select * from eod_eft_spool where TRANSACTION_CODE = 22
--rollback tran 
--
--


GO

