IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_DeleteGeneralAdditionalInterest]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_DeleteGeneralAdditionalInterest]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------              
Proc Name    : dbo.Proc_DeleteGeneralAdditionalInterest            
Created by   : Sumit Chhabra        
Date         : 20 October.,2005             
Purpose      : Delete the record from  APP_GENERAL_HOLDER_INTEREST  Table          
Created by   : Sumit Chhabra        
Date         : 09 November,2005             
Purpose      : Delete the record from  APP_GENERAL_HOLDER_INTEREST  Table          
Revison History :          
Used In  :   Wolverine                 
 ------------------------------------------------------------                          
Date     Review By          Comments                        
               
------   ------------       -------------------------*/             
        
        
        
CREATE PROC Proc_DeleteGeneralAdditionalInterest          
(          
 @CUSTOMER_ID int,  
 @APP_ID int,  
 @APP_VERSION_ID smallint,  
 @HOLDER_ID int,        
 @ADD_INT_ID INT                    
)          
AS          
           
          
BEGIN          
  DELETE FROM APP_GENERAL_HOLDER_INTEREST WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND         
   APP_VERSION_ID=@APP_VERSION_ID AND ADD_INT_ID=@ADD_INT_ID      
           
END        
      
    
  



GO

