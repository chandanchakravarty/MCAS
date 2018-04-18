IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetValuesCLM_ACTIVITY_EXPENSE]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetValuesCLM_ACTIVITY_EXPENSE]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------    
Proc Name       : dbo.Proc_GetValuesCLM_ACTIVITY_EXPENSE    
Created by      : Vijay Arora    
Date            : 5/31/2006    
Purpose     : To get the values from table named CLM_ACTIVITY_EXPENSE    
Revison History :    
Used In        : Wolverine    
------------------------------------------------------------    
Date     Review By          Comments    
------   ------------       -------------------------*/    
CREATE PROC dbo.Proc_GetValuesCLM_ACTIVITY_EXPENSE    
(    
 @CLAIM_ID     int,   
 @ACTIVITY_ID int,  
 @EXPENSE_ID    int    
)    
AS    
BEGIN    
 SELECT ACTIVITY_ID, /*INVOICED_BY, INVOICE_NUMBER, Convert(varchar(10),INVOICE_DATE,101) AS INVOICE_DATE,     
 SERVICE_TYPE, SERVICE_DESCRIPTION,*/ PAYMENT_AMOUNT, ACTION_ON_PAYMENT     
 FROM CLM_ACTIVITY_EXPENSE WHERE CLAIM_ID = @CLAIM_ID AND EXPENSE_ID = @EXPENSE_ID  AND ACTIVITY_ID=@ACTIVITY_ID  
END    
    
    
    
    
  



GO

