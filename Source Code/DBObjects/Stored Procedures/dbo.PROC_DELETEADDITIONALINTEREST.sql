IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_DELETEADDITIONALINTEREST]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_DELETEADDITIONALINTEREST]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------            
Proc Name    : dbo.Proc_DeleteAdditionalInterest        
Created by   : Sumit Chhabra      
Date         : 20 October.,2005           
Purpose      : Delete the record from  AdditionalInterest  Table        
Revison History :        
Used In  :   Wolverine               
 ------------------------------------------------------------                        
Date     Review By          Comments                      
             
------   ------------       -------------------------*/           
      
      
CREATE     PROC DBO.PROC_DELETEADDITIONALINTEREST        
(        
 @CUSTOMER_ID int,
 @APP_ID smallint,
 @APP_VERSION_ID smallint,
 @HOLDER_ID   INT,        
 @VEHICLE_ID int,
 @ADD_INT_ID INT       
             
)        
AS        
      
        
BEGIN        

  DELETE FROM APP_ADD_OTHER_INT WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND       
   APP_VERSION_ID=@APP_VERSION_ID AND ADD_INT_ID=@ADD_INT_ID and VEHICLE_ID=@VEHICLE_ID      
         
END      
    
  



GO

