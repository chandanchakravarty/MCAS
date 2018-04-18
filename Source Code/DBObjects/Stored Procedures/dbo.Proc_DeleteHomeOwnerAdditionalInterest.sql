IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_DeleteHomeOwnerAdditionalInterest]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_DeleteHomeOwnerAdditionalInterest]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

  
/*----------------------------------------------------------                  
Proc Name    : dbo.Proc_DeleteHomeOwnerAdditionalInterest                
Created by   : Sumit Chhabra            
Date         : 20 October.,2005                 
Purpose      : Delete the record from  APP_HOME_OWNER_ADD_INT  Table              
Revison History :              
Used In  :   Wolverine                     
 ------------------------------------------------------------                              
Date     Review By          Comments                            
   drop proc dbo.Proc_DeleteHomeOwnerAdditionalInterest                   
------   ------------       -------------------------*/                 
            
            
CREATE     PROC dbo.Proc_DeleteHomeOwnerAdditionalInterest              
(              
 @CUSTOMER_ID int,        
 @APP_ID smallint,        
 @APP_VERSION_ID smallint,        
 @DWELLING_ID smallint,      
 @HOLDER_ID int,            
 @ADD_INT_ID INT             
                   
)              
AS              
            
              
BEGIN     
  
 DECLARE @AGENCY_BILL_MORTAGAGEE SMALLINT,@INSURED_BILL_MORTAGAGEE SMALLINT,@MORTAGAGEE_INCEPTION SMALLINT  
 SET @AGENCY_BILL_MORTAGAGEE = 11277  
 SET @INSURED_BILL_MORTAGAGEE = 11278  
 SET @MORTAGAGEE_INCEPTION = 11276  
  
--When the record is being deleted and the current record is the selected mortagagee for the application,  
--set the mortagagee value to 0 for the application  
IF exists(SELECT CUSTOMER_ID FROM APP_LIST WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID   
  AND APP_VERSION_ID=@APP_VERSION_ID   
  AND ADD_INT_ID = @ADD_INT_ID AND DWELLING_ID=@DWELLING_ID   
  AND ISNULL(IS_ACTIVE,'N')='Y'  
  --AND BILL_TYPE_ID IN (@AGENCY_BILL_MORTAGAGEE,@INSURED_BILL_MORTAGAGEE,@MORTAGAGEE_INCEPTION))  
AND BILL_TYPE_ID IN (@MORTAGAGEE_INCEPTION))  
 UPDATE APP_LIST SET DWELLING_ID = 0,ADD_INT_ID = 0 WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND             
    APP_VERSION_ID=@APP_VERSION_ID   
  
      
  DELETE FROM APP_HOME_OWNER_ADD_INT WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND             
   APP_VERSION_ID=@APP_VERSION_ID AND DWELLING_ID=@DWELLING_ID AND ADD_INT_ID=@ADD_INT_ID           
               
END            
          
        
      
    
  
  
  




GO

