IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_DeleteAPP_MVR_INFORMATION]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_DeleteAPP_MVR_INFORMATION]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                    
                    
Proc Name       : Proc_DeleteAPP_MVR_INFORMATION                    
Created by      : Sumit Chhabra                    
Date            : 10/03/2005                    
Purpose         : Delete of Driver MVR Information                    
Revison History :                    
Used In         : Wolverine                    
            
------------------------------------------------------------                    
Modified by  : Ashwani <001>            
Date  : 09 Feb. 2006            
Purpose  : Updates the class of Assigned Vehicle.            
------------------------------------------------------------                    
Date     Review By          Comments                    
------   ------------       -------------------------*/    
-- Drop PROC dbo.Proc_DeleteAPP_MVR_INFORMATION                     
CREATE PROC dbo.Proc_DeleteAPP_MVR_INFORMATION                    
(                    
 @CUSTOMER_ID INT,              
 @APP_ID INT,              
 @APP_VERSION_ID INT,              
 @DRIVER_ID INT,              
 @APP_MVR_ID INT ,            
 @CALLED_FROM varchar(10)=null               
)                    
AS                    
BEGIN                    
 DELETE FROM APP_MVR_INFORMATION             
 WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID AND               
         DRIVER_ID=@DRIVER_ID AND APP_MVR_ID=@APP_MVR_ID                
 --<001> <Start>             
/* IF(UPPER(@CALLED_FROM)='PPA')                  
 BEGIN            
  EXEC PROC_SETVEHICLECLASSRULE @CUSTOMER_ID,@APP_ID,@APP_VERSION_ID,@DRIVER_ID                
 END            
      
*/      
 --<001> <End>             
  IF (UPPER(@CALLED_FROM)='MOT')                
   EXEC Proc_SetPreferredRiskDiscount @CUSTOMER_ID,@APP_ID,@APP_VERSION_ID,@DRIVER_ID      
  
Update APP_DRIVER_DETAILS set DATE_ORDERED = NULL ,MVR_ORDERED=''   --yes    
  WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID AND               
         DRIVER_ID=@DRIVER_ID
          
END                   
                
  



GO

