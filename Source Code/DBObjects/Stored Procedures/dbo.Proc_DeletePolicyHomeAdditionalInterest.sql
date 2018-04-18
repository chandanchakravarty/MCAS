IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_DeletePolicyHomeAdditionalInterest]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_DeletePolicyHomeAdditionalInterest]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                
Proc Name     : dbo.Proc_DeletePolicyHomeAdditionalInterest            
Created by    : Vijay Arora      
Date          : 17-11-2005      
Purpose       : Delete the record from  POL_HOME_OWNER_ADD_INT  Table            
Revison History :            
Used In   :   Wolverine                   
 ------------------------------------------------------------                            
Date     Review By          Comments                          
drop proc  dbo.Proc_DeletePolicyHomeAdditionalInterest                           
------   ------------       -------------------------*/               
CREATE     PROC dbo.Proc_DeletePolicyHomeAdditionalInterest            
(            
 @CUSTOMER_ID   INT,            
 @POLICY_ID    INT,            
 @POLICY_VERSION_ID  SMALLINT,            
 @HOLDER_ID   INT,            
 @DWELLING_ID  SMALLINT,          
 @ADD_INT_ID INT           
                 
)            
AS            
          
            
BEGIN       



DECLARE @AGENCY_BILL_MORTAGAGEE SMALLINT,@INSURED_BILL_MORTAGAGEE SMALLINT,@INSURED_BILL_MORTAGAGEE_INCEPTION smallint  
 SET @AGENCY_BILL_MORTAGAGEE = 11277  
 SET @INSURED_BILL_MORTAGAGEE = 11278 
 SET @INSURED_BILL_MORTAGAGEE_INCEPTION = 11276   
  
--When the record is being deleted and the current record is the selected mortagagee for the application,  
--set the mortagagee value to 0 for the application  
IF exists(SELECT CUSTOMER_ID FROM POL_CUSTOMER_POLICY_LIST WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID   
  AND POLICY_VERSION_ID=@POLICY_VERSION_ID   
  AND ADD_INT_ID = @ADD_INT_ID AND DWELLING_ID=@DWELLING_ID   
  AND ISNULL(IS_ACTIVE,'N')='Y'  
  --AND BILL_TYPE_ID IN (@AGENCY_BILL_MORTAGAGEE,@INSURED_BILL_MORTAGAGEE))  
AND BILL_TYPE_ID IN (@INSURED_BILL_MORTAGAGEE_INCEPTION))  
 UPDATE POL_CUSTOMER_POLICY_LIST SET DWELLING_ID = 0,ADD_INT_ID = 0 WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND             
    POLICY_VERSION_ID=@POLICY_VERSION_ID        
      
      
    DELETE FROM POL_HOME_OWNER_ADD_INT WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND           
     POLICY_VERSION_ID=@POLICY_VERSION_ID AND DWELLING_ID=@DWELLING_ID   AND ADD_INT_ID=@ADD_INT_ID      
            
END          
        
      
    
  




GO

