IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_Reverse_WriteOffAmount]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_Reverse_WriteOffAmount]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------            
Proc Name       	: dbo.Proc_Reverse_WriteOffAmount
Created by      	: Ravindra
Date            	: 
Purpose         	: Reverse Write Off  
Revison History 	:            
------------------------------------------------------------            
Date     Review By          Comments            
------   ------------       -------------------------*/     


Create proc [dbo].[Proc_Reverse_WriteOffAmount]
(
	@WRITE_OFF_ITEM_ID int
)
AS
BEGIN
--VARIBALES
DECLARE @CUSTOMER_ID		INT,
		@POLICY_ID			INT,
		@POLICY_VERSION_ID	INT,
		@WRITE_OFF_AMOUNT	Decimal(18,2),
		@CUR_DATE			Datetime

SET		@CUR_DATE	= GETDATE() 

SELECT  @CUSTOMER_ID 		= CUSTOMER_ID,
		@POLICY_ID 			= POLICY_ID,
		@POLICY_VERSION_ID 	= POLICY_VERSION_ID ,
		@WRITE_OFF_AMOUNT	= TOTAL_DUE 
FROM ACT_CUSTOMER_OPEN_ITEMS  WITH(NOLOCK)
WHERE IDEN_ROW_ID 	= @WRITE_OFF_ITEM_ID 


INSERT INTO ACT_CUSTOMER_OPEN_ITEMS(
	UPDATED_FROM,  SOURCE_TRAN_DATE, SOURCE_EFF_DATE, POSTING_DATE, TOTAL_DUE, TOTAL_PAID, AGENCY_ID,
	CUSTOMER_ID, POLICY_ID, POLICY_VERSION_ID,  DIV_ID, DEPT_ID, PC_ID, BILL_CODE, 
	ITEM_TRAN_CODE,ITEM_TRAN_CODE_TYPE, LOB_ID, SUB_LOB_ID, COUNTRY_ID, STATE_ID, 
	TRANS_DESC, DUE_DATE , PROCESSED_AMOUNT_FOR_AGENCY ,WRITE_OFF_AMOUNT
)	
SELECT 
'W' , @CUR_DATE, @CUR_DATE ,@CUR_DATE ,@WRITE_OFF_AMOUNT *- 1 , @WRITE_OFF_AMOUNT *- 1, AGENCY_ID , 
@CUSTOMER_ID, @POLICY_ID, @POLICY_VERSION_ID,PCL.DIV_ID, PCL.DEPT_ID, PCL.PC_ID, PCL.BILL_TYPE,  
'SBWF','PREM',PCL.POLICY_LOB , PCL.POLICY_SUBLOB, PCL.COUNTRY_ID, PCL.STATE_ID, 
'Write off Reversed',	@CUR_DATE ,  @WRITE_OFF_AMOUNT *- 1 ,  @WRITE_OFF_AMOUNT *- 1
FROM POL_CUSTOMER_POLICY_LIST PCL WITH (NOLOCK)
WHERE PCL.CUSTOMER_ID = @CUSTOMER_ID AND  PCL.POLICY_ID = @POLICY_ID 
AND PCL.POLICY_VERSION_ID  = @POLICY_VERSION_ID



END 


GO

