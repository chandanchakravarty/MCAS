IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetDepositAdjustmentDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetDepositAdjustmentDetails]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- begin tran 
-- drop proc dbo.Proc_GetDepositAdjustmentDetails  
-- go 
/*----------------------------------------------------------                
 Proc Name       : Dbo.Proc_GetDepositAdjustmentDetails
 Created by      : Ravindra 
 Date            : 9-24-2007
 Purpose         : Fetch Adjustment details against other policies for a deposit
 Revison History :               
 Used In		 : Wolverine        
exec Proc_GetDepositAdjustmentDetails 12872 , 8 
------------------------------------------------------------                
Date     Review By          Comments                
------   ------------       -------------------------*/                
-- drop proc dbo.Proc_GetDepositAdjustmentDetails  
CREATE PROC [dbo].[Proc_GetDepositAdjustmentDetails]
(                
       @OPEN_ITEM_ID INT,      -- groupID of reconcilliation
       @POLICY_ID INT     , 
	   @CALLED_FROM		Char(1) = 'A'
)                
AS               
BEGIN 

IF(@CALLED_FROM = 'D')
BEGIN 

	CREATE TABLE #RECON_IDS (IDEN_COL Int Identity(1,1) , RECON_ID Int)

	-- Find Reconciliation groups against this Deposit

	INSERT INTO #RECON_IDS ( RECON_ID ) 
	SELECT DISTINCT GROUP_ID
	FROM ACT_CUSTOMER_RECON_GROUP_DETAILS RD  with(nolock)
	INNER JOIN ACT_CUSTOMER_OPEN_ITEMS OI 
	ON OI.IDEN_ROW_ID  = RD.ITEM_REFERENCE_ID
	WHERE RD.ITEM_REFERENCE_ID = @OPEN_ITEM_ID


	-- Find ammount applied against each policy in each reconciliation 
	-- other than reconciliation created when deposited is committed
	SELECT CPL.POLICY_NUMBER , 
	SUM(ISNULL(RD.RECON_AMOUNT ,0)) AS AMOUNT_APPLIED 
	FROM ACT_CUSTOMER_OPEN_ITEMS OI with(nolock)
	INNER JOIN ACT_CUSTOMER_RECON_GROUP_DETAILS RD  with(nolock)
	ON OI.IDEN_ROW_ID  = RD.ITEM_REFERENCE_ID
	AND OI.IDEN_ROW_ID <> @OPEN_ITEM_ID
	AND OI.POLICY_ID  <> @POLICY_ID 
	INNER JOIN #RECON_IDS TMP
	ON RD.GROUP_ID = TMP.RECON_ID 
	INNER JOIN POL_CUSTOMER_POLICY_LIST CPL  with(nolock)
	ON OI.CUSTOMER_ID = CPL.CUSTOMER_ID 
	AND OI.POLICY_ID = CPL.POLICY_ID 
	AND OI.POLICY_VERSION_ID = CPL.POLICY_VERSION_ID 
	GROUP BY CPL.POLICY_NUMBER
	ORDER BY CPL.POLICY_NUMBER

	DROP TABLE #RECON_IDS
END
ELSE
BEGIN 
	SELECT CPL.POLICY_NUMBER , 
	SUM(ISNULL(RD.RECON_AMOUNT ,0)) AS AMOUNT_APPLIED 
	FROM ACT_CUSTOMER_OPEN_ITEMS OI with(nolock)
	INNER JOIN ACT_CUSTOMER_RECON_GROUP_DETAILS RD  with(nolock)
	ON OI.IDEN_ROW_ID  = RD.ITEM_REFERENCE_ID
	AND OI.POLICY_ID  <> @POLICY_ID 
	AND RD.GROUP_ID = @OPEN_ITEM_ID
	INNER JOIN POL_CUSTOMER_POLICY_LIST CPL  with(nolock)
	ON OI.CUSTOMER_ID = CPL.CUSTOMER_ID 
	AND OI.POLICY_ID = CPL.POLICY_ID 
	AND OI.POLICY_VERSION_ID = CPL.POLICY_VERSION_ID 
	GROUP BY CPL.POLICY_NUMBER
	ORDER BY CPL.POLICY_NUMBER
END

END


 
-- 
--go 
--exec Proc_GetDepositAdjustmentDetails  47484, 20
--rollback tran 
-- 
--
--







GO

