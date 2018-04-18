IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetPolicyQuoteDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetPolicyQuoteDetails]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------  
Proc Name      : dbo.Proc_GetPolicyQuoteDetails
Created by     : Vijay Arora
Date           : 27-02-2006
Purpose        : Get the Quote Details from POL_CUSTOMER_POLICY_LIST
Revison History :  
Modified by   :   
Description  :   
Used In        : Wolverine  
------------------------------------------------------------  
Date     Review By          Comments  
------   ------------       -------------------------*/  
CREATE  PROC Dbo.Proc_GetPolicyQuoteDetails  
@CUSTOMER_ID INT,
@POLICY_ID INT,  
@POLICY_VERSION_ID SMALLINT
AS  
BEGIN  
	SELECT  POLICY_PREMIUM_XML FROM POL_CUSTOMER_POLICY_LIST  
 	WHERE CUSTOMER_ID = @CUSTOMER_ID 
	AND POLICY_ID = @POLICY_ID 
	AND POLICY_VERSION_ID = @POLICY_VERSION_ID
END  



GO

