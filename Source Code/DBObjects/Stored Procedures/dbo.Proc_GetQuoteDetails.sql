IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetQuoteDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetQuoteDetails]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------  
Proc Name      : dbo.Proc_GetQuoteDetails
Created by     : Vijay Arora
Date           : 27-02-2006
Purpose        : Get the Quote Details from QOT_CUSTOMER_QUOTE_LIST
Revison History :  
Modified by   :   
Description  :   
Used In        : Wolverine  
------------------------------------------------------------  
Date     Review By          Comments  
------   ------------       -------------------------*/  
CREATE  PROC Dbo.Proc_GetQuoteDetails  
@CUSTOMER_ID INT,
@APP_ID INT,  
@APP_VERSION_ID SMALLINT
AS  
BEGIN  
	SELECT  * FROM QOT_CUSTOMER_QUOTE_LIST  
 	WHERE CUSTOMER_ID = @CUSTOMER_ID 
	AND APP_ID = @APP_ID 
	AND APP_VERSION_ID = @APP_VERSION_ID
END  



GO

