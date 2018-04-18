IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetDriversDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetDriversDetails]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                                                        
Proc Name                : Dbo.Proc_GetDriversDetails                              
Created by               :Ashish kumar                                                      
Date                    : 11 jan 2006                                                     
Purpose                  : To all drivers of the application      
Revison History          :                                                        
Modified by				 : Pravesh k chandel
Modified Date			 :25 June 09
Purpose					: fetching Customerid, appid and ap versionid	
Used In                  : Wolverine                                                        
------------------------------------------------------------                                                        
Date     Review By          Comments                                                       
drop proc dbo.Proc_GetDriversDetails 1081,136,1                                                     
------   ------------       -------------------------*/                                                        
CREATE proc dbo.Proc_GetDriversDetails                                                     
(                                                        
 @CUSTOMER_ID    int,                                                        
 @APP_ID    int,                                                        
 @APP_VERSION_ID   int         
)                                                        
as                                         
      
SELECT     dbo.APP_DRIVER_DETAILS.CUSTOMER_ID, dbo.APP_DRIVER_DETAILS.APP_ID, dbo.APP_DRIVER_DETAILS.APP_VERSION_ID,
		   dbo.APP_DRIVER_DETAILS.DRIVER_ID, dbo.APP_DRIVER_DETAILS.DRIVER_FNAME, dbo.APP_DRIVER_DETAILS.DRIVER_MNAME,       
           dbo.APP_DRIVER_DETAILS.DRIVER_LNAME, dbo.APP_DRIVER_DETAILS.DRIVER_CITY, dbo.APP_DRIVER_DETAILS.DRIVER_ZIP, dbo.APP_DRIVER_DETAILS.DRIVER_CODE, dbo.APP_DRIVER_DETAILS.DRIVER_SUFFIX,       
           dbo.APP_DRIVER_DETAILS.DRIVER_ADD1,dbo.APP_DRIVER_DETAILS.DRIVER_ADD2, dbo.APP_DRIVER_DETAILS.DRIVER_STATE, dbo.APP_DRIVER_DETAILS.DRIVER_DOB,       
           dbo.APP_DRIVER_DETAILS.DRIVER_SSN, dbo.APP_DRIVER_DETAILS.DRIVER_SEX, dbo.APP_DRIVER_DETAILS.DRIVER_DRIV_LIC,       
           dbo.MNT_COUNTRY_STATE_LIST.STATE_CODE,      
   (SELECT STATE_ID FROM APP_LIST WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID) STATE_ID,
	   dbo.MNT_COUNTRY_STATE_LIST.STATE_NAME    
FROM       dbo.APP_DRIVER_DETAILS LEFT JOIN      
           dbo.MNT_COUNTRY_STATE_LIST ON dbo.APP_DRIVER_DETAILS.DRIVER_LIC_STATE = dbo.MNT_COUNTRY_STATE_LIST.STATE_ID       
     AND APP_DRIVER_DETAILS.DRIVER_COUNTRY = MNT_COUNTRY_STATE_LIST.COUNTRY_ID    
where ( CUSTOMER_ID=@CUSTOMER_ID and APP_ID=@APP_ID and APP_VERSION_ID=@APP_VERSION_ID )    
    
    
    
    
    
    
  




GO

