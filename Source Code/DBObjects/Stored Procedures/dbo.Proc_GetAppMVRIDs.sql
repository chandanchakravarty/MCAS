IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetAppMVRIDs]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetAppMVRIDs]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
  
     
    
/*----------------------------------------------------------              
Proc Name   : dbo.Proc_GetAppMVRIDs    
Created by  : Ashwaini    
Date        : 14 Nov.,2005    
Purpose     : Get the APP_MVR_IDs               
Revison History  :                    
 ------------------------------------------------------------                          
Date     Review By          Comments                        
               
------   ------------       -------------------------*/              
CREATE procedure dbo.Proc_GetAppMVRIDs    
(    
 @CUSTOMER_ID int,    
 @APP_ID int,    
 @APP_VERSION_ID int,    
 @DRIVER_ID int    
)        
as             
begin        
select   APP_MVR_ID    
from  APP_MVR_INFORMATION     
where CUSTOMER_ID = @CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID AND  DRIVER_ID=@DRIVER_ID    
	and IS_ACTIVE='Y'
order by   APP_MVR_ID                
end    
  



GO

