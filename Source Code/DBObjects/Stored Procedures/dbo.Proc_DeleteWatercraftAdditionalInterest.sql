IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_DeleteWatercraftAdditionalInterest]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_DeleteWatercraftAdditionalInterest]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                
Proc Name    : dbo.Proc_DeleteWatercraftAdditionalInterest            
Created by   : Sumit Chhabra          
Date         : 20 October.,2005               
Purpose      : Delete the record from  APP_WATERCRAFT_COV_ADD_INT  Table            
Revison History :            
Used In  :   Wolverine                   
 ------------------------------------------------------------                            
Date     Review By          Comments                          
                 
------   ------------       -------------------------*/               
          
          
          
CREATE    PROC dbo.Proc_DeleteWatercraftAdditionalInterest            
(            
 @CUSTOMER_ID int,
 @APP_ID smallint,
 @APP_VERSION_ID smallint,
 @BOAT_ID smallint,
 @HOLDER_ID int,          
 @ADD_INT_ID INT           
                 
)            
AS            
          
            
BEGIN            

  DELETE FROM APP_WATERCRAFT_COV_ADD_INT WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND           
   APP_VERSION_ID=@APP_VERSION_ID  and BOAT_ID=@BOAT_ID and ADD_INT_ID=@ADD_INT_ID  
             
END          
        
      
    
  



GO

