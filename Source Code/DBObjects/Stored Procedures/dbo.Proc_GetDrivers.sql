IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetDrivers]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetDrivers]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------            
Proc Name            : Dbo.Proc_GetDrivers            
Created by           : Ashwani           
Date                    : 13/10/2005            
Purpose                : To get the drivers for the PPA rule implementation          
Revison History  :            
Used In                 :   Wolverine              
------------------------------------------------------------            
Date     Review By          Comments            
------   ------------       -------------------------*/            
         
create proc dbo.Proc_GetDrivers              
@CUSTOMERID  int,            
@APPID  int,            
@APPVERSIONID int           
as            
begin            
 select DRIVER_ID from APP_DRIVER_DETAILS            
 where    CUSTOMER_ID = @CUSTOMERID   and APP_ID=@APPID and APP_VERSION_ID=@APPVERSIONID         
 and IS_ACTIVE='Y'  and isnull(DRIVER_DRIV_TYPE,11603)in (11603,3477,11941,11942)  
 order by DRIVER_ID        
  -- Driver Type is Licensed & value="11603"         
end          
      
  



GO

