IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetAddInterestIDs]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetAddInterestIDs]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
   
  
/*----------------------------------------------------------            
Proc Name   : dbo.Proc_GetAddInterestIDs  
Created by  : Ashwaini  
Date        : 14 Nov.,2005  
Purpose     : Get the Additional Interest IDs             
Revison History  :                  
 ------------------------------------------------------------                        
Date     Review By          Comments                      
             
------   ------------       -------------------------*/            
CREATE PROCEDURE dbo.Proc_GetAddInterestIDs  
(  
 @CUSTOMER_ID int,  
 @APP_ID int,  
 @APP_VERSION_ID int,  
 @VEHICLE_ID int  
)      
AS           
BEGIN      
SELECT   ADD_INT_ID  
FROM       APP_ADD_OTHER_INT   
WHERE CUSTOMER_ID = @CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID AND  VEHICLE_ID=@VEHICLE_ID  
	AND IS_ACTIVE='Y'
ORDER BY   ADD_INT_ID              
End  



GO

