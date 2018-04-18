IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetPayeeNameReference]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetPayeeNameReference]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------  
Proc Name       : dbo.Proc_GetPayeeNameReference
Created by      : Vijay Arora  
Date            : 6/1/2006  
Purpose         : To get the values of PAYEE from table named CLM_PARTIES  
Revison History :  
Used In  : Wolverine  
------------------------------------------------------------  
Date     Review By          Comments  
------   ------------       -------------------------*/  

-- DROP PROC dbo.Proc_GetPayeeNameReference  
CREATE PROC dbo.Proc_GetPayeeNameReference  
(  
 @CLAIM_ID int,  
 @ACTIVITY_ID int,  
 @EXPENSE_ID int
)  
AS  
BEGIN  
 SELECT  CP.[NAME],CP.REFERENCE, CP.ADDRESS1, CP.ADDRESS2, CP.CITY, CP.COUNTRY, CP.STATE, CP.ZIP, -- E.INVOICED_BY AS PARTY_ID,
 CP.BANK_NAME,CP.ACCOUNT_NAME,CP.ACCOUNT_NUMBER
 FROM CLM_ACTIVITY_EXPENSE E  
 LEFT JOIN CLM_PARTIES CP ON CP.CLAIM_ID = E.CLAIM_ID -- AND CP.PARTY_ID = E.INVOICED_BY 
 WHERE E.CLAIM_ID = @CLAIM_ID AND E.ACTIVITY_ID = @ACTIVITY_ID AND E.EXPENSE_ID = @EXPENSE_ID
END








GO

