IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetWCViolationIDs]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetWCViolationIDs]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
   
  
/*----------------------------------------------------------            
Proc Name   : dbo.Proc_GetWCViolationIDs  
Created by  : Ashwini  
Date        : 04 Jan.,2006
Purpose     : Get the WC driver's IDs 
Revison History  :                  
 ------------------------------------------------------------                        
Date     Review By          Comments                      
             
------   ------------       -------------------------*/            
CREATE PROCEDURE dbo.Proc_GetWCViolationIDs  
(  
 @CUSTOMER_ID int,  
 @APP_ID int,  
 @APP_VERSION_ID int,  
 @DRIVER_ID int  
)      
AS           
BEGIN            
  
	SELECT APP_WATER_MVR_ID  
	FROM APP_WATER_MVR_INFORMATION   
	WHERE CUSTOMER_ID = @CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID  and DRIVER_ID=@DRIVER_ID  
	ORDER BY   APP_WATER_MVR_ID              
End



GO

