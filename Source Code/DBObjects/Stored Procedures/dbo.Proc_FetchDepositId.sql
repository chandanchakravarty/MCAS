IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_FetchDepositId]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_FetchDepositId]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------    
Proc Name       : Proc_FetchDepositId    
Created by      : kranti  
Date            : 17 May 2007  
Purpose       : Get deposit id for Agency Reconciliation Status  
Revison History :    
  
------------------------------------------------------------    
Date     Review By          Comments    
------   ------------       --------------------------------*/    
--drop proc dbo.Proc_FetchDepositId    
-- DROP PROC dbo.Proc_FetchDepositId    
CREATE PROCEDURE dbo.Proc_FetchDepositId    
(    
 @CD_LINE_ITEM_ID  INT
)  
AS  
 SELECT
CUR.DEPOSIT_NUMBER AS DEPOSIT_NUMBER,
CUR.DEPOSIT_ID AS DEPOSIT_ID,
ITM.DEPOSIT_TYPE AS DEPOSIT_TYPE,
ITM.RECEIPT_AMOUNT AS RECEIPT_AMOUNT,
ITM.CUSTOMER_ID AS CUSTOMER_ID,
ITM.POLICY_ID AS POLICY_ID,
ITM.POLICY_VERSION_ID AS POLICY_VERSION_ID
FROM ACT_CURRENT_DEPOSIT_LINE_ITEMS ITM  with(nolock)
 INNER JOIN ACT_CURRENT_DEPOSITS  CUR with(nolock)
 ON ITM.DEPOSIT_ID = CUR.DEPOSIT_ID
 WHERE ITM.CD_LINE_ITEM_ID = @CD_LINE_ITEM_ID








GO

