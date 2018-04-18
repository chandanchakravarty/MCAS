IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetExistingDriverForWatercraft]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetExistingDriverForWatercraft]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------        
Proc Name      : dbo.Proc_GetExistingDriverForWatercraft        
Created by       :Nidhi         
Date             :6/27/2005        
Purpose       : retrieving data from APP_WATERCRAFT_DRIVER_DETAILS        
Revison History :        
Used In        : Wolverine        
------------------------------------------------------------        
Date     Review By          Comments        
------   ------------       -------------------------*/        
-- DROP PROC Dbo.Proc_GetExistingDriverForWatercraft        
CREATE PROC Dbo.Proc_GetExistingDriverForWatercraft        
@CUSTOMERID int,        
@APPID int ,        
@APPVERSIONID int        
        
AS        
BEGIN        
        
declare @LOBID int        
select @LOBID = APP_LOB from APP_LIST where  CUSTOMER_ID=@CUSTOMERID AND APP_ID=@APPID AND APP_VERSION_ID=@APPVERSIONID        
--Added By Sibin on 21 Dec 09 for Itrack Issue 4905 to Copy drivers & household member of all LOB in Watercraft,Automobile and Homeowner 

SELECT    DISTINCT APP_DRIVER_DETAILS.DRIVER_ID, APP_DRIVER_DETAILS.CUSTOMER_ID AS CUSTOMERID,
APP_DRIVER_DETAILS.DRIVER_CODE,APP_DRIVER_DETAILS.DRIVER_FNAME + ' ' + APP_DRIVER_DETAILS.DRIVER_MNAME + ' ' +        APP_DRIVER_DETAILS.DRIVER_LNAME AS DRIVERNAME,        
                       ISNULL(CLT_CUSTOMER_LIST.CUSTOMER_FIRST_NAME,'') + ' ' + ISNULL(CLT_CUSTOMER_LIST.CUSTOMER_MIDDLE_NAME,'') + ' ' + ISNULL(CLT_CUSTOMER_LIST.CUSTOMER_LAST_NAME,'')AS CUSTOMERNAME,APP_LIST.APP_VERSION,APP_DRIVER_DETAILS.APP_ID,APP_LIST.APP_VERSION_ID,APP_LIST.APP_NUMBER,APP_LIST.APP_VERSION        
FROM         APP_DRIVER_DETAILS INNER JOIN        
                      APP_LIST ON APP_DRIVER_DETAILS.CUSTOMER_ID = APP_LIST.CUSTOMER_ID       
        AND APP_LIST.APP_ID=APP_DRIVER_DETAILS.APP_ID    --Added by mohit       
        AND APP_LIST.APP_VERSION_ID=APP_DRIVER_DETAILS.APP_VERSION_ID -- Added by mohit         
                     LEFT OUTER JOIN        
                      CLT_CUSTOMER_LIST ON APP_DRIVER_DETAILS.CUSTOMER_ID = CLT_CUSTOMER_LIST.CUSTOMER_ID        
WHERE     (APP_DRIVER_DETAILS.CUSTOMER_ID = @CUSTOMERID) --AND (APP_LIST.APP_LOB = @LOBID)    
AND         
  IsNull(APP_DRIVER_DETAILS.IS_ACTIVE,'') <> 'N'         
    
UNION  
  --Added till here
SELECT    DISTINCT APP_WATERCRAFT_DRIVER_DETAILS.DRIVER_ID, APP_WATERCRAFT_DRIVER_DETAILS.CUSTOMER_ID AS CUSTOMERID,  APP_WATERCRAFT_DRIVER_DETAILS.DRIVER_CODE,         
                      APP_WATERCRAFT_DRIVER_DETAILS.DRIVER_FNAME + ' ' + APP_WATERCRAFT_DRIVER_DETAILS.DRIVER_MNAME + ' ' +        
   APP_WATERCRAFT_DRIVER_DETAILS.DRIVER_LNAME AS DRIVERNAME,        
                       ISNULL(CLT_CUSTOMER_LIST.CUSTOMER_FIRST_NAME,'') + ' ' + ISNULL(CLT_CUSTOMER_LIST.CUSTOMER_MIDDLE_NAME,'') + ' ' + ISNULL(CLT_CUSTOMER_LIST.CUSTOMER_LAST_NAME,'')        
                       AS CUSTOMERNAME, APP_LIST.APP_VERSION,APP_WATERCRAFT_DRIVER_DETAILS. APP_ID,        
 APP_LIST.APP_VERSION_ID,        
 APP_LIST.APP_NUMBER,        
 APP_LIST.APP_VERSION        
FROM         APP_WATERCRAFT_DRIVER_DETAILS INNER JOIN        
                      APP_LIST ON APP_WATERCRAFT_DRIVER_DETAILS.CUSTOMER_ID = APP_LIST.CUSTOMER_ID       
        AND APP_LIST.APP_ID=APP_WATERCRAFT_DRIVER_DETAILS.APP_ID    --Added by mohit       
        AND APP_LIST.APP_VERSION_ID=APP_WATERCRAFT_DRIVER_DETAILS.APP_VERSION_ID -- Added by mohit         
                     LEFT OUTER JOIN        
                      CLT_CUSTOMER_LIST ON APP_WATERCRAFT_DRIVER_DETAILS.CUSTOMER_ID = CLT_CUSTOMER_LIST.CUSTOMER_ID        
WHERE     (APP_WATERCRAFT_DRIVER_DETAILS.CUSTOMER_ID = @CUSTOMERID) --AND (APP_LIST.APP_LOB = @LOBID)    
AND         
  IsNull(APP_WATERCRAFT_DRIVER_DETAILS.IS_ACTIVE,'') <> 'N'        
        
 --commented by mohit on 14/07/2005        
  --  AND        
                --       (APP_WATERCRAFT_DRIVER_DETAILS.DRIVER_CODE NOT IN        
                  --         (SELECT     DRIVER_CODE        
                    --         FROM          APP_WATERCRAFT_DRIVER_DETAILS        
                      --       WHERE      APP_ID =@APPID  AND APP_VERSION_ID = @APPVERSIONID AND CUSTOMER_ID = @CUSTOMERID))        
END        
      
    
    
    
    
    
GO

