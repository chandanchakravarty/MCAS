IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertInstallmentExpireDate]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertInstallmentExpireDate]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--begin tran
--drop proc Proc_InsertInstallmentExpireDate
--go
-- =============================================
-- Author:		<Pradeep Kushwaha>
-- Create date: <17-Jan-2011>
-- Description:	<Add Installment expire date (date calculated based on the short term table) >
-- Proc_InsertInstallmentExpireDate   2043,564,1 
-- drop proc Proc_InsertInstallmentExpireDate
-- =============================================
                              
CREATE PROC  [dbo].[Proc_InsertInstallmentExpireDate]                 
(
	-- Add the parameters for the stored procedure here
	@CUSTOMER_ID INT,  
	@POLICY_ID INT,  
	@POLICY_VERSION_ID INT
)
AS
BEGIN
	DECLARE @CANC_DATE DATETIME 
	DECLARE @INSTALLMENT_NUMBER INT = 0  
	DECLARE @MAX_NO_OF_INSTALLMENT INT
	DECLARE @MIN_NO_OF_INSTALLMENT INT
	
  
	SELECT @MAX_NO_OF_INSTALLMENT=MAX(INSTALLMENT_NO),@MIN_NO_OF_INSTALLMENT=MIN(INSTALLMENT_NO) FROM ACT_POLICY_INSTALLMENT_DETAILS WITH(NOLOCK)  WHERE CUSTOMER_ID =@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID
	
	SELECT @INSTALLMENT_NUMBER=@MIN_NO_OF_INSTALLMENT
  
    WHILE(@MAX_NO_OF_INSTALLMENT >= @INSTALLMENT_NUMBER)  
	   BEGIN
			
			EXEC Proc_GetNonPayCancellationDate @CUSTOMER_ID,@POLICY_ID,@POLICY_VERSION_ID,@CANC_DATE OUT ,'BOLETO',@INSTALLMENT_NUMBER
			--I-Track-835 ADD BY Pradeep Kushwaha on 29-March-2011
			DECLARE @INSTALLMENT_DUE_DATE DATETIME
			SELECT @INSTALLMENT_DUE_DATE = INSTALLMENT_EFFECTIVE_DATE FROM ACT_POLICY_INSTALLMENT_DETAILS  WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID =@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID AND INSTALLMENT_NO=@INSTALLMENT_NUMBER 
			
			IF(@CANC_DATE < @INSTALLMENT_DUE_DATE)
		    BEGIN
				SELECT @CANC_DATE=@INSTALLMENT_DUE_DATE
		    END
			--Till here 
			UPDATE ACT_POLICY_INSTALLMENT_DETAILS SET INSTALLMENT_EXPIRE_DATE=@CANC_DATE WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID =@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID AND INSTALLMENT_NO=@INSTALLMENT_NUMBER 
			
		    SET @INSTALLMENT_NUMBER= @INSTALLMENT_NUMBER+1 		
		    
	   END
END

--go
--exec Proc_InsertInstallmentExpireDate 28241,34,1 
--rollback tran
	
 
  
GO

