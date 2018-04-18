IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_FetchWriteoffAmount]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_FetchWriteoffAmount]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--exec Proc_FetchWriteoffAmount 'R7000076'
--drop proc  dbo.Proc_FetchWriteoffAmount null
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

Create proc [dbo].[Proc_FetchWriteoffAmount]
(
	@POLICYNO varchar(25) = null
)
AS

-- ITrack # 6120 21 July 2009 -Manoj Rathore

DECLARE @CUSTOMER_ID INT,
	@POLICY_ID INT
SELECT @CUSTOMER_ID=CUSTOMER_ID,@POLICY_ID=POLICY_ID FROM POL_CUSTOMER_POLICY_LIST
WHERE POLICY_NUMBER=@POLICYNO

----

SELECT OI.IDEN_ROW_ID,
CONVERT(VARCHAR,OI.DUE_DATE ,101)	AS DUE_DATE,
ISNULL(OI.TOTAL_DUE,0)				AS AMOUNT_DUE , 
ISNULL(OI.TRANS_DESC,'')			AS PLAN_DESCRIPTION,
ISNULL(OI.TOTAL_PAID,0)				AS AMOUNT_PAID,
ISNULL(OI.TOTAL_DUE,0) - ISNULL(OI.TOTAL_PAID,0) AS BALANCE 
 FROM         
ACT_CUSTOMER_OPEN_ITEMS  OI with(nolock)
LEFT JOIN
	    ACT_POLICY_INSTALLMENT_DETAILS INSTALL_DETAILS  with(nolock)
		ON OI.CUSTOMER_ID = INSTALL_DETAILS.CUSTOMER_ID
		AND OI.POLICY_ID = INSTALL_DETAILS.POLICY_ID 
		AND OI.INSTALLMENT_ROW_ID = INSTALL_DETAILS.ROW_ID 
LEFT JOIN 
		POL_CUSTOMER_POLICY_LIST PCL with(nolock)
		ON PCL.CUSTOMER_ID = OI.CUSTOMER_ID 
		AND PCL.POLICY_ID = OI.POLICY_ID
		AND PCL.POLICY_VERSION_ID = OI.POLICY_VERSION_ID 

WHERE 		--(PCL.POLICY_NUMBER=@POLICYNO)-- ITrack # 6120 21 July 2009 -Manoj Rathore 
		OI.CUSTOMER_ID=@CUSTOMER_ID AND OI.POLICY_ID=@POLICY_ID
		AND 
		(
			( OI.ITEM_TRAN_CODE_TYPE in ('PREM','JE') AND OI.UPDATED_FROM  in ('P','J') )
			OR OI.UPDATED_FROM = 'F' 
		) 
		AND (ISNULL(OI.TOTAL_DUE,0) - ISNULL(OI.TOTAL_PAID,0) ) <> 0
		
ORDER BY INSTALL_DETAILS.CURRENT_TERM,INSTALLMENT_NO 










GO

