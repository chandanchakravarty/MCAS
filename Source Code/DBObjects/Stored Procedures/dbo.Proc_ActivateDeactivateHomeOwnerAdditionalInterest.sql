IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_ActivateDeactivateHomeOwnerAdditionalInterest]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_ActivateDeactivateHomeOwnerAdditionalInterest]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                  
Proc Name    : dbo.Proc_ActivateDeactivateHomeOwnerAdditionalInterest                
Created by   : Sumit Chhabra            
Date         : 20 October.,2005                 
Purpose      : Delete the record from  APP_HOME_OWNER_ADD_INT  Table              
Revison History :              
Used In  :   Wolverine                     
 ------------------------------------------------------------                              
Date     Review By          Comments                            
    drop proc Proc_ActivateDeactivateHomeOwnerAdditionalInterest                   
------   ------------       -------------------------*/                 
            
            
create PROC dbo.Proc_ActivateDeactivateHomeOwnerAdditionalInterest      
(              
 @CUSTOMER_ID int,      
 @APP_ID smallint,      
 @APP_VERSION_ID smallint,      
 @DWELLING_ID smallint,      
 @HOLDER_ID int,            
 @ADD_INT_ID int,      
 @IS_ACTIVE NCHAR(2)            
                   
)              
AS              
            
              
BEGIN            
  
 DECLARE @AGENCY_BILL_MORTAGAGEE SMALLINT,@INSURED_BILL_MORTAGAGEE SMALLINT,@MORTAGAGEE_INCEPTION SMALLINT ,@YES_LOOKUP_ID SMALLINT 
 SET @AGENCY_BILL_MORTAGAGEE = 11277  
 SET @INSURED_BILL_MORTAGAGEE = 11278  
 SET @MORTAGAGEE_INCEPTION = 11276  
  declare @BILL_MORTAGAGEE  SMALLINT
  
  set @YES_LOOKUP_ID = 10963   
  
  
--When the record is being deactivated and the current record is the selected mortagagee for the application,  
--set the mortagagee value to 0 for the application  
if(UPPER(@IS_ACTIVE)='N')  
BEGIN   
IF exists(SELECT CUSTOMER_ID FROM APP_LIST WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID   
  AND APP_VERSION_ID=@APP_VERSION_ID   
  AND ADD_INT_ID = @ADD_INT_ID AND DWELLING_ID=@DWELLING_ID   
  AND ISNULL(IS_ACTIVE,'N')='Y'  
 -- AND BILL_TYPE_ID IN (@AGENCY_BILL_MORTAGAGEE,@INSURED_BILL_MORTAGAGEE,@MORTAGAGEE_INCEPTION))  
 AND BILL_TYPE_ID IN (@MORTAGAGEE_INCEPTION))  
 UPDATE APP_LIST SET DWELLING_ID = 0,ADD_INT_ID = 0 WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND             
    APP_VERSION_ID=@APP_VERSION_ID   
END  
  
    
 UPDATE APP_HOME_OWNER_ADD_INT SET IS_ACTIVE=@IS_ACTIVE         
 WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND             
    APP_VERSION_ID=@APP_VERSION_ID AND ADD_INT_ID = @ADD_INT_ID AND DWELLING_ID=@DWELLING_ID   
   
IF (UPPER(@IS_ACTIVE)='Y') 	
	BEGIN
		SELECT @BILL_MORTAGAGEE=ISNULL(BILL_MORTAGAGEE,0) FROM APP_HOME_OWNER_ADD_INT WHERE CUSTOMER_ID=@CUSTOMER_ID AND  APP_ID=@APP_ID AND             
									   APP_VERSION_ID=@APP_VERSION_ID AND ADD_INT_ID = @ADD_INT_ID AND DWELLING_ID=@DWELLING_ID   
		IF ( @BILL_MORTAGAGEE   = @YES_LOOKUP_ID AND UPPER(@IS_ACTIVE)='Y' )
			UPDATE APP_LIST SET DWELLING_ID =@DWELLING_ID,ADD_INT_ID = @ADD_INT_ID WHERE CUSTOMER_ID=@CUSTOMER_ID  AND APP_ID=@APP_ID AND             
					 APP_VERSION_ID=@APP_VERSION_ID   
	END
         
END            
          
      
  
    
  
  
  
  





GO

