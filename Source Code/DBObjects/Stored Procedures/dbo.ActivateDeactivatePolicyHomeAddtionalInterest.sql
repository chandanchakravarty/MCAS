IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ActivateDeactivatePolicyHomeAddtionalInterest]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[ActivateDeactivatePolicyHomeAddtionalInterest]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                
Proc Name    : dbo.ActivateDeactivatePolicyHomeAddtionalInterest              
Created by   : Vijay Arora    
Date         : 17-11-2005    
Purpose      : Activate Deactivate the record in Policy Home Owner Additional Interest Table.    
Revison History :            
Used In  :   Wolverine                   
 ------------------------------------------------------------                            
Date     Review By          Comments                          
drop proc ActivateDeactivatePolicyHomeAddtionalInterest                 
------   ------------       -------------------------*/               
CREATE PROC dbo.ActivateDeactivatePolicyHomeAddtionalInterest            
(            
 @CUSTOMER_ID      int,            
 @POLICY_ID  int,            
 @POLICY_VERSION_ID  smallint,            
 @DWELLING_ID   smallint,            
 @HOLDER_ID int,          
 @ADD_INT_ID INT,      
 @IS_ACTIVE char(1)          
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
IF exists(SELECT CUSTOMER_ID FROM POL_CUSTOMER_POLICY_LIST WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID   
  AND POLICY_VERSION_ID=@POLICY_VERSION_ID   
  AND ADD_INT_ID = @ADD_INT_ID AND DWELLING_ID=@DWELLING_ID   
  AND ISNULL(IS_ACTIVE,'N')='Y'  
  --AND BILL_TYPE_ID IN (@AGENCY_BILL_MORTAGAGEE,@INSURED_BILL_MORTAGAGEE,@MORTAGAGEE_INCEPTION))  
	AND BILL_TYPE_ID IN (@MORTAGAGEE_INCEPTION))  
 UPDATE POL_CUSTOMER_POLICY_LIST SET DWELLING_ID = 0,ADD_INT_ID = 0 WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND             
    POLICY_VERSION_ID=@POLICY_VERSION_ID   
END  
        
 UPDATE POL_HOME_OWNER_ADD_INT SET IS_ACTIVE=@IS_ACTIVE       
 WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND           
    POLICY_VERSION_ID=@POLICY_VERSION_ID  AND DWELLING_ID=@DWELLING_ID  AND ADD_INT_ID = @ADD_INT_ID        
IF (UPPER(@IS_ACTIVE)='Y') 	
	BEGIN
	SELECT @BILL_MORTAGAGEE=ISNULL(BILL_MORTAGAGEE,0) FROM POL_HOME_OWNER_ADD_INT WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND           
		POLICY_VERSION_ID=@POLICY_VERSION_ID  AND DWELLING_ID=@DWELLING_ID  AND ADD_INT_ID = @ADD_INT_ID 
	IF ( @BILL_MORTAGAGEE   = @YES_LOOKUP_ID AND UPPER(@IS_ACTIVE)='Y' )
		UPDATE POL_CUSTOMER_POLICY_LIST SET DWELLING_ID =@DWELLING_ID,ADD_INT_ID = @ADD_INT_ID WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND             
								 POLICY_VERSION_ID=@POLICY_VERSION_ID   
	END
END          
        
      

    
  







GO

