IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetWCDrivers]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetWCDrivers]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------    
Proc Name          	 : Dbo.Proc_GetWCDrivers    
Created by          	 : Ashwani   
Date                     : 20 Oct. 2005   
Purpose                  : To get the drivers for the Watercraft rule implementation  
Revison History 	 :    
Used In                  :   Wolverine      
------------------------------------------------------------    
Date     Review By          Comments    
------   ------------       -------------------------*/  
create proc Dbo.Proc_GetWCDrivers      
@CUSTOMER_ID  int,    
@APP_ID  int,    
@APPVERSION_ID int   
as    
begin    
 select DRIVER_ID from APP_WATERCRAFT_DRIVER_DETAILS    
 where    CUSTOMER_ID = @CUSTOMER_ID   and APP_ID=@APP_ID AND APP_VERSION_ID=@APPVERSION_ID     
end


GO

