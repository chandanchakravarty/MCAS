IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_ActivateDeactivateAdditionalInterest]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_ActivateDeactivateAdditionalInterest]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------            
Proc Name    : dbo.Proc_ActivateDeactivateAdditionalInterest        
Created by   : Sumit Chhabra      
Date         : 20 October.,2005           
Purpose      : Delete the record from  AdditionalInterest  Table        
Revison History :        
Used In  :   Wolverine               
 ------------------------------------------------------------                        
Date     Review By          Comments                      
             
------   ------------       -------------------------*/           
  
      
CREATE     PROC DBO.Proc_ActivateDeactivateAdditionalInterest        
(        
 @CUSTOMER_ID int,
 @APP_ID smallint,
 @APP_VERSION_ID smallint,
 @HOLDER_ID   INT,        
 @VEHICLE_ID int,
 @ADD_INT_ID INT,  
 @IS_ACTIVE nchar(2)    
             
)        
AS        
      
        
BEGIN        
 UPDATE APP_ADD_OTHER_INT SET IS_ACTIVE=@IS_ACTIVE WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND       
   APP_VERSION_ID=@APP_VERSION_ID AND ADD_INT_ID=@ADD_INT_ID AND VEHICLE_ID=@VEHICLE_ID      
         
END      
    
  
  



GO

