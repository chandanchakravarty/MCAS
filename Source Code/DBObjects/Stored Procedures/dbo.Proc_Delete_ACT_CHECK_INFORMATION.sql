IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_Delete_ACT_CHECK_INFORMATION]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_Delete_ACT_CHECK_INFORMATION]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--begin tran 
--DROP PROC Proc_Delete_ACT_CHECK_INFORMATION      
--go 

/*----------------------------------------------------------      
Proc Name       : dbo.ACT_CHECK_INFORMATION      
Created by      : Ajit Singh Chahal      
Date            : 6/30/2005      
Purpose         :To insert records in check information.      
Revison History :      
Used In        : Wolverine      
------------------------------------------------------------      
Date     Review By          Comments      
------   ------------       -------------------------*/      
--DROP PROC Proc_Delete_ACT_CHECK_INFORMATION      
CREATE PROC dbo.Proc_Delete_ACT_CHECK_INFORMATION      
(      
@CHECK_ID INT,      
@CHECK_TYPE int = NULL,      
@OPEN_ITEM_ID int = NULL      
)      
AS      
BEGIN      

DECLARE @ACC_Check Int  
DECLARE @RPC_Check Int  
DECLARE @ROP_Check Int  
DECLARE @RSC_Check Int  
DECLARE @CC_Check Int  
DECLARE @VC_Check Int  
DECLARE @MOC_Check Int  
DECLARE @REC_Check Int  
DECLARE @ID INT
  
SET @ACC_Check = 2472  
SET @RPC_Check = 2474  
SET @ROP_Check = 9935  
SET @RSC_Check = 9936  
SET @CC_Check = 9937  
SET @VC_Check = 9938  
SET @MOC_Check = 9940  
SET @REC_Check = 9945  

SELECT @CHECK_TYPE = CHECK_TYPE
FROM  ACT_CHECK_INFORMATION
WHERE CHECK_ID  = @CHECK_ID

/*customer checks:      
   Premium Refund Checks for Return Premium Payment      
   Premium Refund Checks for Over Payment      
   Premium Refund Checks for Suspense Amount         
*/      
IF @CHECK_TYPE=@RPC_Check or @CHECK_TYPE=@ROP_Check or @CHECK_TYPE=@RSC_Check       
BEGIN      
	/*SELECT @ID = OPEN_ITEM_ROW_ID FROM ACT_CHECK_INFORMATION WHERE CHECK_ID = @CHECK_ID
	UPDATE ACT_CUSTOMER_OPEN_ITEMS set IS_CHECK_CREATED='N' where IDEN_ROW_ID=@ID*/
	SELECT @ID = IDEN_ROW_ID FROM ACT_CHECK_OPEN_ITEMS WHERE CHECK_ID = @CHECK_ID
	UPDATE ACT_CUSTOMER_OPEN_ITEMS set IS_CHECK_CREATED='N' where IDEN_ROW_ID=@ID
END      
      
IF @CHECK_TYPE = @ACC_Check
BEGIN 

	DECLARE @COMM_TYPE Varchar(10),
		@MONTH Int,
		@YEAR  Int,
		@AGENCY_ID		Int
	SELECT  @COMM_TYPE = COMM_TYPE ,
		@MONTH = [MONTH],
		@YEAR  = [YEAR] , 
		@AGENCY_ID = CASE COMM_TYPE WHEN  'CAC' THEN AGENCY_ID ELSE PAYEE_ENTITY_ID END
	FROM  ACT_CHECK_INFORMATION
	WHERE CHECK_ID  = @CHECK_ID

	IF(@COMM_TYPE <> 'OP')  
	BEGIN   
		UPDATE ACT_AGENCY_STATEMENT SET IS_CHECK_CREATED = 'N'  
		WHERE MONTH_NUMBER = @MONTH   
		AND MONTH_YEAR =  @YEAR  
		AND COMM_TYPE =  @COMM_TYPE   
		AND AGENCY_ID = @AGENCY_ID   
	END  
	ELSE  
	BEGIN   
		UPDATE ACT_AGENCY_STATEMENT SET IS_CHECK_CREATED = 'N'  
		WHERE MONTH_NUMBER = @MONTH   
		AND MONTH_YEAR  =  @YEAR  
		AND ITEM_STATUS =  @COMM_TYPE   
		AND AGENCY_ID  = @AGENCY_ID   
	END  
END


--To be done      
/*Claims Checks      
 @CHECK_TYPE=9937       
      
Vendor Checks      
      
9938      
      
Miscellaneous (Other) Checks      
9940      
      
Reinsurance Premium Checks      
9945*/      
DELETE FROM ACT_DISTRIBUTION_DETAILS WHERE GROUP_TYPE = 'CHQ' AND GROUP_ID = @CHECK_ID 
DELETE  FROM TEMP_ACT_CHECK_INFORMATION WHERE CHECK_ID = @CHECK_ID    
DELETE FROM ACT_AGENCY_CHECK_DISTRIBUTION WHERE CHECK_ID = @CHECK_ID    
DELETE FROM ACT_CHECK_INFORMATION WHERE CHECK_ID = @CHECK_ID     

Return 1 
END    



--go 
--SELECT is_check_created,* FROM ACT_CUSTOMER_OPEN_ITEMS WHERE CUSTOMER_ID = 490 AND POLICY_ID = 2
--exec Proc_Delete_ACT_CHECK_INFORMATION '2904'
--SELECT is_check_created,* FROM ACT_CUSTOMER_OPEN_ITEMS WHERE CUSTOMER_ID = 490 AND POLICY_ID = 2
----select is_check_created ,* from  act_agency_statement where comm_type = 'CAC' and month_number = 9 
--rollback tran 


GO

