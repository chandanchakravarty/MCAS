IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetExistingDriver]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetExistingDriver]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

  
/*----------------------------------------------------------      
Proc Name          : Dbo.Proc_GetExistingDriver      
Created by           : Mohit Gupta      
Date                    : 03/06/2005      
Purpose               :       
Revison History :      
Used In                :   Wolverine        
------------------------------------------------------------      
Date     Review By          Comments      
Proc_GetExistingDriver 1162,12,1    
drop procedure dbo.Proc_GetExistingDriver     
------   ------------       -------------------------*/      
CREATE PROCEDURE Proc_GetExistingDriver      
(      
 @CUSTOMERID int,      
 @APPID int,      
 @APPVERSIONID int      
       
)      
AS      
BEGIN      
      
DECLARE @LOBID int      
select @LOBID = APP_LOB from APP_LIST where  CUSTOMER_ID = @CUSTOMERID and APP_ID =  @APPID AND APP_VERSION_ID =  @APPVERSIONID    
      
      
SELECT APP_DRIVER_DETAILS.CUSTOMER_ID AS CUSTOMERID ,     
 APP_DRIVER_DETAILS. APP_ID,      
 APP_LIST.APP_VERSION_ID,      
 APP_LIST.APP_NUMBER,      
 APP_LIST.APP_VERSION,      
 APP_DRIVER_DETAILS.DRIVER_ID,      
 APP_DRIVER_DETAILS.DRIVER_CODE,      
 DRIVER_FNAME+' '+DRIVER_MNAME+' '+DRIVER_LNAME AS DRIVERNAME,      
 CUSTOMER_FIRST_NAME+' '+CUSTOMER_MIDDLE_NAME+' '+CUSTOMER_LAST_NAME  AS CUSTOMERNAME      
FROM  APP_DRIVER_DETAILS       
INNER JOIN CLT_CUSTOMER_LIST ON  APP_DRIVER_DETAILS.CUSTOMER_ID=CLT_CUSTOMER_LIST.CUSTOMER_ID      
INNER JOIN APP_LIST  ON APP_DRIVER_DETAILS.CUSTOMER_ID=APP_LIST.CUSTOMER_ID AND      
   APP_DRIVER_DETAILS.APP_ID=APP_LIST.APP_ID AND      
  APP_DRIVER_DETAILS.APP_VERSION_ID=APP_LIST.APP_VERSION_ID   
-- AND APP_LIST.APP_LOB = @LOBID        
WHERE APP_DRIVER_DETAILS.CUSTOMER_ID=@CUSTOMERID      
 and  ISNULL(APP_DRIVER_DETAILS.IS_ACTIVE,'') <> 'N'      
  
--Added By Sibin on 13 Nov 08 for Itrack Issue 4905 to Copy drivers & household member of all LOB in Watercraft,Automobile and Homeowner  
UNION  
SELECT APP_WATERCRAFT_DRIVER_DETAILS.CUSTOMER_ID AS CUSTOMERID ,     
 APP_WATERCRAFT_DRIVER_DETAILS. APP_ID,      
 APP_LIST.APP_VERSION_ID,      
 APP_LIST.APP_NUMBER,      
 APP_LIST.APP_VERSION,      
 APP_WATERCRAFT_DRIVER_DETAILS.DRIVER_ID,      
 APP_WATERCRAFT_DRIVER_DETAILS.DRIVER_CODE,      
 DRIVER_FNAME+' '+DRIVER_MNAME+' '+DRIVER_LNAME AS DRIVERNAME,      
 CUSTOMER_FIRST_NAME+' '+CUSTOMER_MIDDLE_NAME+' '+CUSTOMER_LAST_NAME  AS CUSTOMERNAME      
FROM  APP_WATERCRAFT_DRIVER_DETAILS       
INNER JOIN CLT_CUSTOMER_LIST ON  APP_WATERCRAFT_DRIVER_DETAILS.CUSTOMER_ID=CLT_CUSTOMER_LIST.CUSTOMER_ID      
INNER JOIN APP_LIST  ON APP_WATERCRAFT_DRIVER_DETAILS.CUSTOMER_ID=APP_LIST.CUSTOMER_ID AND      
   APP_WATERCRAFT_DRIVER_DETAILS.APP_ID=APP_LIST.APP_ID AND      
  APP_WATERCRAFT_DRIVER_DETAILS.APP_VERSION_ID=APP_LIST.APP_VERSION_ID   
 --AND APP_LIST.APP_LOB = @LOBID        
WHERE APP_WATERCRAFT_DRIVER_DETAILS.CUSTOMER_ID=@CUSTOMERID      
 and  ISNULL(APP_WATERCRAFT_DRIVER_DETAILS.IS_ACTIVE,'') <> 'N'      
      
END      
  
  
GO

