IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetViolationIDs]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetViolationIDs]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------            
Proc Name   : dbo.Proc_GetViolationIDs  
Created by  : nidhi  
Date        : 07 October,2005  
Purpose     : Get the Driver IDs  for private passenger           
Revison History  :                  
 ------------------------------------------------------------                        
Date     Review By          Comments                      
             
------   ------------       -------------------------*/            
CREATE PROCEDURE dbo.Proc_GetViolationIDs  
(  
 @CUSTOMER_ID int,  
 @APP_ID int,  
 @APP_VERSION_ID int,  
 @DRIVER_ID int  
)      
AS           
BEGIN            
  
SELECT   APP_MVR_ID  
FROM       APP_MVR_INFORMATION WITH (NOLOCK)   
   WHERE CUSTOMER_ID = @CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID  and DRIVER_ID=@DRIVER_ID  
			and ISNULL(IS_ACTIVE,'Y')='Y'
   ORDER BY   APP_MVR_ID              
End





GO

