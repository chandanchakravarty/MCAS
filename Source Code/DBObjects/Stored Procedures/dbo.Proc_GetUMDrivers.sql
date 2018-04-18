IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetUMDrivers]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetUMDrivers]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*===========================================================================
Proc Name            : Dbo.Proc_GetUMDrivers          
Created by           : Ashwani         
Date                 : 18 Oct. 2006  
Purpose              : To get the driver's count for the UM rule implementation        
Revison History      :          
Used In              :   Wolverine            
===========================================================================
Date     Review By          Comments          
=====  ============    ====================================================*/
        
create proc dbo.Proc_GetUMDrivers            
(	
	@CUSTOMERID  int,          
	@APPID  int,          
	@APPVERSIONID int         
)
as          
begin          
 select DRIVER_ID from APP_UMBRELLA_DRIVER_DETAILS          
	 where    CUSTOMER_ID = @CUSTOMERID   and APP_ID=@APPID and APP_VERSION_ID=@APPVERSIONID       
		 and IS_ACTIVE='Y'
		 order by DRIVER_ID      
end        
    







GO

