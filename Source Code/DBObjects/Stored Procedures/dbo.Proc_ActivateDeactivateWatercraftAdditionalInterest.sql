IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_ActivateDeactivateWatercraftAdditionalInterest]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_ActivateDeactivateWatercraftAdditionalInterest]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------              
Proc Name    : dbo.Proc_ActivateDeactivateWatercraftAdditionalInterest          
Created by   : Sumit Chhabra        
Date         : 20 October.,2005             
Purpose      : Delete the record from  APP_WATERCRAFT_COV_ADD_INT  Table          
Revison History :          
Used In  :   Wolverine                 
 ------------------------------------------------------------                          
Date     Review By          Comments                        
               
------   ------------       -------------------------*/             
        
        
        
CREATE PROC dbo.Proc_ActivateDeactivateWatercraftAdditionalInterest          
(          
 @CUSTOMER_ID int,
 @APP_ID smallint,
 @APP_VERSION_ID smallint,
 @BOAT_ID smallint,
 @HOLDER_ID int,        
 @ADD_INT_ID INT,  
 @IS_ACTIVE NCHAR(2)         
               
)          
AS          
  
  
          
BEGIN          
  UPDATE APP_WATERCRAFT_COV_ADD_INT SET IS_ACTIVE=@IS_ACTIVE   
 WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND         
    APP_VERSION_ID=@APP_VERSION_ID AND ADD_INT_ID = @ADD_INT_ID and BOAT_ID=@BOAT_ID        
           
END        
      
    
  
  
  



GO

