IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_DeleteTrailerAdditionalInterest]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_DeleteTrailerAdditionalInterest]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------            
Proc Name    : dbo.DeleteTrailerAdditionalInterest          
Created by   : Sumit Chhabra      
Date         : 20 October.,2005           
Purpose      : Delete the record from  APP_WATERCRAFT_TRAILER_ADD_INT  Table        
Revison History :        
Used In  :   Wolverine               
 ------------------------------------------------------------                        
Date     Review By          Comments                      
             
------   ------------       -------------------------*/           
      
      
      
CREATE    PROC dbo.Proc_DeleteTrailerAdditionalInterest        
(        
 @CUSTOMER_ID int,  
 @APP_ID smallint,  
 @APP_VERSION_ID smallint,  
 @TRAILER_ID smallint,  
 @HOLDER_ID int,      
 @ADD_INT_ID INT       
             
)        
AS        
      
        
BEGIN        
  DELETE FROM APP_WATERCRAFT_TRAILER_ADD_INT WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND       
   APP_VERSION_ID=@APP_VERSION_ID AND  TRAILER_ID=@TRAILER_ID and ADD_INT_ID=@ADD_INT_ID
         
END      
    
  


GO

