IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DeleteRecVehAdditionalInterest]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[DeleteRecVehAdditionalInterest]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------              
Proc Name    : dbo.DeleteRecVehAdditionalInterest            
Created by   : Swastika Gaur        
Date         : 15th Jun'06             
Purpose      : Delete the record from  APP_HOMEOWNER_REC_VEH_ADD_INT  Table          
Revison History :          
Used In  :   Wolverine                 
 ------------------------------------------------------------                          
Date     Review By          Comments                        
               
------   ------------       -------------------------*/             
         
CREATE    PROC dbo.DeleteRecVehAdditionalInterest          
(          
 @CUSTOMER_ID int,    
 @APP_ID smallint,    
 @APP_VERSION_ID smallint,    
 @REC_VEH_ID smallint,    
 @HOLDER_ID int,        
 @ADD_INT_ID INT         
               
)          
AS          
        
          
BEGIN          
  DELETE FROM APP_HOMEOWNER_REC_VEH_ADD_INT WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND         
   APP_VERSION_ID=@APP_VERSION_ID AND  REC_VEH_ID=@REC_VEH_ID and ADD_INT_ID=@ADD_INT_ID  
           
END        
      
    



GO

