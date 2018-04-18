IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetOperators]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetOperators]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------      
Proc Name           : Dbo.Proc_GetDrivers      
Created by          : Praveen     
Date                : 13/10/2005      
Purpose             : To get the operators for the PPA rule implementation    
Revison History     :      
Used In             :   Wolverine        
------------------------------------------------------------      
Date     Review By          Comments      
------   ------------       -------------------------*/      
    
CREATE proc Dbo.Proc_GetOperators        
@CUSTOMERID  int,      
@APPID  int,      
@APPVERSIONID int     
as      
begin      
 select DRIVER_ID from APP_WATERCRAFT_DRIVER_DETAILS      
 where    CUSTOMER_ID = @CUSTOMERID   and APP_ID=@APPID and APP_VERSION_ID=@APPVERSIONID   
 and IS_ACTIVE='Y'  
order by DRIVER_ID  
   
end    



GO

