IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_PaymentClaim]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_PaymentClaim]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

   
/*----------------------------------------------------------                      
Proc Name       : dbo.[Proc_PaymentClaim]                      
Created by      : Vijay Arora                      
Date            : 5/24/2006                      
Purpose     : To get a record in table named CLM_PAYMENT_TEXT                     
Revison History :                      
Used In  :                   
------------------------------------------------------------                      
Date     Review By          Comments                      
------   ------------       -------------------------*/         
-- DROP PROC dbo.[Proc_PaymentClaim]                  
CREATE PROC [dbo].[Proc_PaymentClaim]                                             
AS                      
BEGIN
	SELECT 
		TEXT_ID,
		[DESCRIPTION],
		[COUNT]
	FROM CLM_PAYMENT_TEXT WITH(NOLOCK)  
END
GO

