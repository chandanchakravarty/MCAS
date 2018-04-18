IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetCLM_AUTHORITY_LIMIT]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetCLM_AUTHORITY_LIMIT]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                
                
Proc Name       : Proc_GetCLM_AUTHORITY_LIMIT
Created by      : Sumit Chhabra
Date            : 20/04/2006                
Purpose         : Get Authority Limit data from CLM_AUTHORITY_LIMIT
Revison History :                
Used In                   : Wolverine  
Modified By		: Agniswar
Modified On		: 26 Sep 2011
Purpose			: Singapore Implementation              
------------------------------------------------------------                
Date     Review By          Comments                
------   ------------       -------------------------*/                
CREATE PROC Dbo.Proc_GetCLM_AUTHORITY_LIMIT                  
(                  
 @LIMIT_ID int              
)                  
AS                  
BEGIN                
  SELECT  
   LIMIT_ID,  
   AUTHORITY_LEVEL,  
   TITLE,  
   PAYMENT_LIMIT,  
   RESERVE_LIMIT,  
   REOPEN_CLAIM_LIMIT,
   GRATIA_CLAIM_AMOUNT,
   CLAIM_ON_DUMMY_POLICY,  
   IS_ACTIVE  
  FROM CLM_AUTHORITY_LIMIT  
  WHERE LIMIT_ID=@LIMIT_ID  

END


GO

