IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetAuthorityAmounts]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetAuthorityAmounts]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------      
Proc Name       : dbo.Proc_GetAuthorityAmounts
Created by      : Sumit Chhabra    
Date            : 25/04/2006      
Purpose         : Fetch Authority Amounts based on Limit chosen
Revison History :      
Used In        : Wolverine      
------------------------------------------------------------      
Date     Review By          Comments      
------   ------------       -------------------------*/      
CREATE PROC Dbo.Proc_GetAuthorityAmounts
@LIMIT_ID int
AS      
BEGIN      
SELECT PAYMENT_LIMIT,RESERVE_LIMIT FROM CLM_AUTHORITY_LIMIT WHERE LIMIT_ID=@LIMIT_ID
END    
    
  



GO

