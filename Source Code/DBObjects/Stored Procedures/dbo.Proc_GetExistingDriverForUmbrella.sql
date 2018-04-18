IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetExistingDriverForUmbrella]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetExistingDriverForUmbrella]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------  
Proc Name      : dbo.Proc_GetExistingDriverForUmbrella  
Created by       :Nidhi   
Date             :6/21/2005  
Purpose       : retrieving data from APP_UMBRELLA_DRIVER_DETAILS  
Revison History :  
Used In        : Wolverine  
------------------------------------------------------------  
Date     Review By          Comments  
------   ------------       -------------------------*/  
--DROP PROC Proc_GetExistingDriverForUmbrella  

CREATE PROC Proc_GetExistingDriverForUmbrella  
@CUSTOMERID int,  
@APPID int ,  
@APPVERSIONID int  
AS  
BEGIN  
declare @UMRELLA_LOBID int
set @UMRELLA_LOBID = 5
SELECT    DISTINCT APP_UMBRELLA_DRIVER_DETAILS.DRIVER_ID, APP_UMBRELLA_DRIVER_DETAILS.CUSTOMER_ID AS CUSTOMERID,  APP_UMBRELLA_DRIVER_DETAILS.DRIVER_CODE,   
                      APP_UMBRELLA_DRIVER_DETAILS.DRIVER_FNAME + ' ' + APP_UMBRELLA_DRIVER_DETAILS.DRIVER_MNAME + ' ' +  
   APP_UMBRELLA_DRIVER_DETAILS.DRIVER_LNAME AS DRIVERNAME,  
                       CLT_CUSTOMER_LIST.CUSTOMER_FIRST_NAME + ' ' + CLT_CUSTOMER_LIST.CUSTOMER_MIDDLE_NAME + ' ' + CLT_CUSTOMER_LIST.CUSTOMER_LAST_NAME  
                       AS CUSTOMERNAME, APP_LIST.APP_VERSION,APP_UMBRELLA_DRIVER_DETAILS. APP_ID,  
 APP_LIST.APP_VERSION_ID,  
 APP_LIST.APP_NUMBER,  
 APP_LIST.APP_VERSION  
FROM         APP_UMBRELLA_DRIVER_DETAILS INNER JOIN  
                      APP_LIST ON APP_UMBRELLA_DRIVER_DETAILS.CUSTOMER_ID = APP_LIST.CUSTOMER_ID  AND   
   APP_UMBRELLA_DRIVER_DETAILS.APP_ID = APP_LIST.APP_ID AND  
   APP_UMBRELLA_DRIVER_DETAILS.APP_VERSION_ID = APP_LIST.APP_VERSION_ID  
 LEFT OUTER JOIN  
                      CLT_CUSTOMER_LIST ON APP_UMBRELLA_DRIVER_DETAILS.CUSTOMER_ID = CLT_CUSTOMER_LIST.CUSTOMER_ID  
WHERE   (APP_UMBRELLA_DRIVER_DETAILS.CUSTOMER_ID = @CUSTOMERID) 
	--	AND (APP_LIST.APP_LOB = 5)  AND   
	AND (APP_LIST.APP_LOB = @UMRELLA_LOBID)  AND   
	IsNull(APP_UMBRELLA_DRIVER_DETAILS.IS_ACTIVE,'') <> 'N'   
                         
 --Commented by mohit on 14/07/2005  
 -- (APP_UMBRELLA_DRIVER_DETAILS.DRIVER_CODE NOT IN  
               --           (SELECT     DRIVER_CODE  
                --            FROM          APP_UMBRELLA_DRIVER_DETAILS  
                 --           WHERE      APP_ID =@APPID  AND APP_VERSION_ID = @APPVERSIONID AND CUSTOMER_ID = @CUSTOMERID))  
END  
  
  
  



GO

