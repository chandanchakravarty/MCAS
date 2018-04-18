IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetDriverCount]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetDriverCount]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
   
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
  
   
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
  
  
   
/*----------------------------------------------------------  
Proc Name          : Dbo.Proc_GetDriverCount  
Created by           : Anurag Verma  
Date                     : 21/09/2005  
Purpose               : To get the driver count for the customer and application  
Revison History :  
Used In                :   Wolverine  
  
Modified By : Anurag Verma  
Modified On : 23/09/2005  
Purpose : getting count from app_watercraft_driver_details  
   adding defualt @type parameter  
Modified By     : Mohit Gupta  
Modified On     : 15/10/2005.  
Purpose         : Added parameter @CALLEDFROM FOR checking the lob. 

Modified By     : PAwan Papreja
Modified On     : 11/11/2005.  
Purpose         : Complete checking of operator and driver   
------------------------------------------------------------  
Date     Review By          Comments  
------   ------------       -------------------------*/  
CREATE PROC Dbo.Proc_GetDriverCount  
  
@CUSTOMERID  int,  
@APPID  int,  
@APPVERSIONID int,  
@CALLEDFROM     varchar(5),  
@type char(1)=null ,
@CalledFor varchar(5)=null 
  
AS  
BEGIN  
--if @type=null  
if (UPPER(@CALLEDFROM) ='PPA')  
begin  
 SELECT COUNT(DRIVER_ID) DRIVER_ID FROM APP_DRIVER_DETAILS  
 WHERE    CUSTOMER_ID = @CUSTOMERID   and APP_ID=@APPID AND APP_VERSION_ID=@APPVERSIONID   
end  
else if (UPPER(@CALLEDFROM) ='UMB' AND UPPER(@CALLEDFOR) ='PPA')  
begin  
 SELECT COUNT(DRIVER_ID) DRIVER_ID FROM APP_UMBRELLA_DRIVER_DETAILS  
 WHERE    CUSTOMER_ID = @CUSTOMERID   and APP_ID=@APPID AND APP_VERSION_ID=@APPVERSIONID   
end  
else if (UPPER(@CALLEDFROM) ='WAT' OR UPPER(@CALLEDFROM) ='HOME' OR UPPER(@CALLEDFROM) ='RENT')  
begin  
 SELECT COUNT(DRIVER_ID) DRIVER_ID FROM APP_Watercraft_DRIVER_DETAILS  
 WHERE    CUSTOMER_ID = @CUSTOMERID   and APP_ID=@APPID AND APP_VERSION_ID=@APPVERSIONID   
end  
else if (UPPER(@CALLEDFROM) ='UMB' AND UPPER(@CALLEDFOR) ='WAT')  
begin  
 SELECT COUNT(DRIVER_ID) DRIVER_ID FROM APP_UMBRELLA_OPERATOR_INFO  
 WHERE    CUSTOMER_ID = @CUSTOMERID   and APP_ID=@APPID AND APP_VERSION_ID=@APPVERSIONID   
end  
END  
  
  
  

  
  



GO

