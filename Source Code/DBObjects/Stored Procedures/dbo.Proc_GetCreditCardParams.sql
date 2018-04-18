IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetCreditCardParams]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetCreditCardParams]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------    
Proc Name        : dbo.Proc_GetCreditCardParams  
Created by       : Ravinda Gupta   
Date             :   
Purpose        :   
Revison History :    
Used In   :Wolverine    

Reviewed By	:	Anurag Verma
Reviewed On	:	25-06-2007
------------------------------------------------------------    
Date     Review By          Comments    
------------------------------------------------------------*/    
-- drop proc dbo.Proc_GetCreditCardParams  
CREATE PROC dbo.Proc_GetCreditCardParams  
AS  
BEGIN   
  
 -- Select Bank Account ID To Be used fot EFT   
  
	DECLARE @EFT_BANK_ACCOUNT Int ,  
			@POSTING_DATE DateTime

	SET @POSTING_DATE  = DATEADD(dd,1, GETDATE())  

	SELECT BNK_CUST_DEP_EFT_CARD AS BANK_ACCOUNT, FISCAL_ID  
	FROM ACT_GENERAL_LEDGER  WITH(NOLOCK) 
	WHERE FISCAL_BEGIN_DATE <= CAST(CONVERT(VARCHAR,@POSTING_DATE,101) AS DATETIME) 
	AND FISCAL_END_DATE >= CAST(CONVERT(VARCHAR,@POSTING_DATE,101) AS DATETIME)
END  
  
  


  



GO

