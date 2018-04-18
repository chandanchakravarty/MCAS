IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_CheckCustomerExistence]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_CheckCustomerExistence]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------  
Proc Name          : Dbo.Proc_CheckCustomerExistence  
Created by         : Pradeep  
Date               : 05/05/2005  
Purpose            : To check whether this customer exists  
Revison History :  
Used In                :   Wolverine  
------------------------------------------------------------  
Date     Review By          Comments  
------   ------------       -------------------------*/  
CREATE    PROC dbo.Proc_CheckCustomerExistence  
(  
  
 @CUSTOMER_FIRST_NAME nVarChar(25),  
 @CUSTOMER_LAST_NAME nVarChar(25),  
 @CUSTOMER_ADDRESS1 NVarChar(150),  
 @CUSTOMER_CITY VarChar(70),  
 @CUSTOMER_STATE NVarChar(10),  
 @CUSTOMER_ZIP VarChar(11),  
 @CUSTOMER_AGENCY_ID smallint   
)  
  
AS  
BEGIN  
  
DECLARE @CUSTOMER_ID Int  
  
 SELECT @CUSTOMER_ID = CUSTOMER_ID  
 FROM CLT_CUSTOMER_LIST  with(nolock)
 WHERE CUSTOMER_FIRST_NAME = @CUSTOMER_FIRST_NAME AND  
  ISNULL(CUSTOMER_LAST_NAME,'') = ISNULL(@CUSTOMER_LAST_NAME,'') AND  
  ISNULL(CUSTOMER_ADDRESS1,'') = ISNULL(@CUSTOMER_ADDRESS1,'') AND  
  ISNULL(CUSTOMER_CITY,'') = ISNULL(@CUSTOMER_CITY,'') AND  
  ISNULL(CUSTOMER_ZIP,'') = ISNULL(@CUSTOMER_ZIP,'') AND  
  ISNULL(CUSTOMER_AGENCY_ID,0) = ISNULL(@CUSTOMER_AGENCY_ID,0)  
   
 IF ( @CUSTOMER_ID IS NULL )  
 BEGIN  
  RETURN -1  
 END  
 ELSE  
 BEGIN  
  RETURN @CUSTOMER_ID  
 END    
     
END  
--SELECT * FROM CLT_CUSTOMER_LIST WHERE CUSTOMER_FIRST_NAME = 'AAA-XXX' AND ISNULL(CUSTOMER_LAST_NAME,'') = ISNULL('','')  




GO

