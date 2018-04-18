IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetPPARule_DRIVER_VEHICLE_ASSIGN]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetPPARule_DRIVER_VEHICLE_ASSIGN]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO





/*----------------------------------------------------------                                              
Proc Name                : Dbo.Proc_GetPPARule_DRIVER_VEHICLE_ASSIGN                              
Created by               : Ashwani                                              
Date                     : 24 Nov.,2005                                            
Purpose                  : To get the deactivate vehicle assigned info            
Revison History          :                                              
Used In                  : Wolverine                                              
------------------------------------------------------------                                              
Date     Review By          Comments                                              
------   ------------       -------------------------*/                                              
CREATE  proc Dbo.Proc_GetPPARule_DRIVER_VEHICLE_ASSIGN
(                                              
 @CUSTOMERID    int,                                              
 @APPID    int,                                              
 @APPVERSIONID   int 

        
)                                              
as                               
begin                                       
     
	select DRV.DRIVER_ID,DRV.DRIVER_FNAME,VEH.MODEL,VEH.MAKE,VEH.VEHICLE_ID,VEH.IS_ACTIVE 
	from APP_VEHICLES VEH, APP_DRIVER_DETAILS DRV 
	where 
	VEH.VEHICLE_ID=DRV.VEHICLE_ID AND
	DRV.CUSTOMER_ID=@CUSTOMERID 
	AND DRV.APP_ID=@APPID 
	AND DRV.APP_VERSION_ID=@APPVERSIONID
	AND VEH.IS_ACTIVE = 'N'
	AND VEH.VEHICLE_ID
	in 
	(
	 select VEHICLE_ID 
	 from APP_DRIVER_DETAILS                    
	 where CUSTOMER_ID=@CUSTOMERID and  APP_ID=@APPID  and  APP_VERSION_ID=@APPVERSIONID
	)         
end              
              
            
            
    
          
          
    
    
    
  





GO

