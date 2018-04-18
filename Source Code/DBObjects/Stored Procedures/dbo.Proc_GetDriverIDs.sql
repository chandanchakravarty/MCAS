IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetDriverIDs]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetDriverIDs]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                
Proc Name   : dbo.Proc_GetDriverIDs      
Created by  : nidhi      
Date        : 07 October,2005      
Purpose     : Get the Driver IDs  for private passenger               
Revison History  :                      
 ------------------------------------------------------------                            
Date     Review By          Comments                          
                 
------   ------------       -------------------------*/  
-- drop PROC dbo.Proc_GetDriverIDs              
CREATE PROCEDURE dbo.Proc_GetDriverIDs      
(      
 @CUSTOMER_ID int,      
 @APP_ID int,      
 @APP_VERSION_ID int      
)          
AS               
BEGIN                
      
SELECT   DRIVER_ID      
FROM       APP_DRIVER_DETAILS   WITH (NOLOCK)    
   WHERE CUSTOMER_ID = @CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID     
 AND ISNULL(IS_ACTIVE,'Y')='Y'       
   ORDER BY   DRIVER_ID                  
End    
    
  



GO

