IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ActivateDeactivateRecVehAdditionalInterest]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[ActivateDeactivateRecVehAdditionalInterest]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                
Proc Name    : dbo.ActivateDeactivateRecVehAdditionalInterest              
Created by   : Swastika Gaur          
Date         : 15th Jun'06               
Purpose      : Delete the record from  APP_HOMEOWNER_REC_VEH_ADD_INT  Table            
Revison History :            
Used In  :   Wolverine                   
 ------------------------------------------------------------                            
Date     Review By          Comments                          
                 
------   ------------       -------------------------*/               
          
-- drop proc dbo.ActivateDeactivateRecVehAdditionalInterest            
CREATE    PROC dbo.ActivateDeactivateRecVehAdditionalInterest            
(            
 @CUSTOMER_ID int,    
 @APP_ID smallint,    
 @APP_VERSION_ID smallint,    
 @REC_VEH_ID smallint,    
 @HOLDER_ID int,          
 @ADD_INT_ID INT,      
 @IS_ACTIVE NCHAR(2)       
           
)            
AS            
          
            
BEGIN            
    
  UPDATE APP_HOMEOWNER_REC_VEH_ADD_INT     
  SET IS_ACTIVE=@IS_ACTIVE WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND           
  APP_VERSION_ID=@APP_VERSION_ID  and REC_VEH_ID=@REC_VEH_ID and ADD_INT_ID=@ADD_INT_ID         
             
END          
        
      
    
  



GO

