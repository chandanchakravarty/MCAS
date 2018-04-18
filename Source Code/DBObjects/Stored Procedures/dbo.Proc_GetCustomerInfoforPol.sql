IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetCustomerInfoforPol]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetCustomerInfoforPol]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------  
Proc Name       : dbo.Proc_GetCustomerInfoforPol 
Created by      : Swarup
Date            : 05/01/2007  
Purpose       :To get customer records for Policy information.  
Revison History :  
Used In        : Wolverine  
  
------   ------------       -------------------------*/  
--drop PROC dbo.Proc_GetCustomerInfoforPol 
CREATE PROC dbo.Proc_GetCustomerInfoforPol 
  
AS  
BEGIN  
 SELECT ISNULL(CUSTOMER_ID,0) AS CUSTOMER_ID,ISNULL(CUSTOMER_FIRST_NAME,'') +  ' ' + ISNULL(CUSTOMER_LAST_NAME,'') AS CUSTOMER_NAME   
 FROM CLT_CUSTOMER_LIST WITH(NOLOCK) WHERE IS_ACTIVE='Y' order by CUSTOMER_FIRST_NAME   
END  
  
  



GO

