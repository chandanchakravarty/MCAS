IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetExistingOperatorForUmbrella]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetExistingOperatorForUmbrella]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------    
Proc Name      : dbo.Proc_GetExistingDriverForUmbrella    
Created by       :Shafi     
Date             :12/12/2005    
Purpose       : retrieving data from APP_UMBRELLA_OPERATOR_INFO  
Revison History :    
Used In        : Wolverine    
------------------------------------------------------------    
Date     Review By          Comments    
------   ------------       -------------------------*/    
  
CREATE procedure Proc_GetExistingOperatorForUmbrella  
@CUSTOMERID int,    
@APPID int ,    
@APPVERSIONID int    
  
as  
begin  
  
  
SELECT    DISTINCT APP_UMBRELLA_OPERATOR_INFO.DRIVER_ID, APP_UMBRELLA_OPERATOR_INFO.CUSTOMER_ID AS CUSTOMERID,  APP_UMBRELLA_OPERATOR_INFO.DRIVER_CODE,     
                      APP_UMBRELLA_OPERATOR_INFO.DRIVER_FNAME + ' ' + APP_UMBRELLA_OPERATOR_INFO.DRIVER_MNAME + ' ' +    
   APP_UMBRELLA_OPERATOR_INFO.DRIVER_LNAME AS DRIVERNAME,    
                       CLT_CUSTOMER_LIST.CUSTOMER_FIRST_NAME + ' ' + CLT_CUSTOMER_LIST.CUSTOMER_MIDDLE_NAME + ' ' + CLT_CUSTOMER_LIST.CUSTOMER_LAST_NAME    
                       AS CUSTOMERNAME, APP_LIST.APP_VERSION,APP_UMBRELLA_OPERATOR_INFO. APP_ID,    
 APP_LIST.APP_VERSION_ID,    
 APP_LIST.APP_NUMBER,    
 APP_LIST.APP_VERSION    
FROM         APP_UMBRELLA_OPERATOR_INFO INNER JOIN    
                      APP_LIST ON APP_UMBRELLA_OPERATOR_INFO.CUSTOMER_ID = APP_LIST.CUSTOMER_ID  AND     
   APP_UMBRELLA_OPERATOR_INFO.APP_ID = APP_LIST.APP_ID AND    
   APP_UMBRELLA_OPERATOR_INFO.APP_VERSION_ID = APP_LIST.APP_VERSION_ID    
 LEFT OUTER JOIN    
                      CLT_CUSTOMER_LIST ON APP_UMBRELLA_OPERATOR_INFO.CUSTOMER_ID = CLT_CUSTOMER_LIST.CUSTOMER_ID    
WHERE     (APP_UMBRELLA_OPERATOR_INFO.CUSTOMER_ID = @CUSTOMERID) AND (APP_LIST.APP_LOB = 5)  AND     
 IsNull(APP_UMBRELLA_OPERATOR_INFO.IS_ACTIVE,'') <> 'N'     
end   
  
  



GO

