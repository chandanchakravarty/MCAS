IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_CLM_ACTIVATEDEACTIVATEDRIVERDETAILS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_CLM_ACTIVATEDEACTIVATEDRIVERDETAILS]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                                                                                                                                                                                            
Proc Name       : dbo.PROC_ACTIVATEDEACTIVATEINSUREDVEHICLE                                                                                                                                                                                
Created by      : Sibin Thomas Philip                                                                                                                                                                                          
Date            : 8 Sept 2009                                                                                                                                                                                            
Purpose         : Activating or Deactivating chosen DRIVER id in the selected claim             
                                                                                                                  Created by      : Sibin Thomas Philip                                                                                                                                                                                           
Revison History :                                                                                                                                                                                            
Used In        : Wolverine                                                                                                                                                                                            
                   
------------------------------------------------------------                            
Date     Review By          Comments                                                                                                                                                                                            
------   ------------       -------------------------*/                                                                                                                                                                                            
--DROP PROC dbo.PROC_CLM_ACTIVATEDEACTIVATEDRIVERDETAILS                                                                                                                                                                
CREATE   PROC dbo.PROC_CLM_ACTIVATEDEACTIVATEDRIVERDETAILS                                                                                                                                                                        
@CLAIM_ID int,                                                               
@DRIVER_ID int,      
@ACTIVATEDEACTIVATE varchar(1)
               
AS                                                                                                                                                                                      
BEGIN                                                                                                                  
   IF EXISTS(SELECT 1 FROM CLM_DRIVER_INFORMATION WHERE CLAIM_ID=@CLAIM_ID AND DRIVER_ID=@DRIVER_ID)            
   BEGIN      
    IF(@ACTIVATEDEACTIVATE = 'Y')      
    BEGIN            
     UPDATE CLM_DRIVER_INFORMATION SET IS_ACTIVE = 'Y' WHERE CLAIM_ID=@CLAIM_ID AND DRIVER_ID=@DRIVER_ID         
    END      
    ELSE IF(@ACTIVATEDEACTIVATE = 'N')      
    BEGIN            
     UPDATE CLM_DRIVER_INFORMATION SET IS_ACTIVE = 'N' WHERE CLAIM_ID=@CLAIM_ID AND DRIVER_ID=@DRIVER_ID      
    END    
  END    
   
END 
GO

