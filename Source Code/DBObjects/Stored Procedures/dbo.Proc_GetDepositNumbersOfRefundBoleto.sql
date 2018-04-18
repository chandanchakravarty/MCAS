IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetDepositNumbersOfRefundBoleto]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetDepositNumbersOfRefundBoleto]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


--BEGIN TRAN               
--drop proc dbo.Proc_GetDepositNumbersOfRefundBoleto              
--go       
--=============================================      
-- Author:  <Pradeep Kushwah >      
-- Create date: <04-July-2011>      
-- Description: <To get deposit # of refund Boletos>      
-- DROP PROC Proc_GetDepositNumbersOfRefundBoleto   9654   
-- =============================================      
CREATE PROCEDURE [dbo].[Proc_GetDepositNumbersOfRefundBoleto]      
 -- Add the parameters for the stored procedure here      
  @BOLETO_NO INT     
      
AS      
BEGIN      
 -- SET NOCOUNT ON added to prevent extra result sets from      
 -- interfering with SELECT statements.      
DECLARE @DEPOSIT_NUMBER varchar(1000) 

SELECT @DEPOSIT_NUMBER = ISNULL(@DEPOSIT_NUMBER + ', ', '') + CAST(
DEPOSIT_NUMBER AS varchar(20)) 
FROM 
(SELECT DISTINCT DEPOSIT_NUMBER 
FROM	ACT_CURRENT_DEPOSIT_LINE_ITEMS LINE_ITEMS  WITH(NOLOCK)  
			 INNER JOIN  ACT_CURRENT_DEPOSITS ITEMS  WITH(NOLOCK)  
			 ON LINE_ITEMS.DEPOSIT_ID=ITEMS.DEPOSIT_ID  
			-- AND ISNULL(ITEMS.IS_COMMITED, 'N')<>'Y' 
			 AND ISNULL(LINE_ITEMS.IS_ACTIVE,'N')='Y' -- NOT FOR THE INACTIVE BOLETOS
			 --AND  LINE_ITEMS.IS_APPROVE='R' AND LINE_ITEMS.IS_EXCEPTION='Y' AND LINE_ITEMS.EXCEPTION_REASON=294
			 WHERE LINE_ITEMS.BOLETO_NO=@BOLETO_NO 
)LINE_ITEM

SELECT @DEPOSIT_NUMBER  as DEPOSIT_NUMBER

END

GO

