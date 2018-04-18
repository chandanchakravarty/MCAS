IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetNameInsuredPolicies]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetNameInsuredPolicies]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------            
Proc Name          : Dbo.Proc_GetNameInsuredPolicies         
Created by         : Sibin Philip          
Date               : 16-2-2009          
Purpose            :             
Revison History :            
Used In            :   Wolverine              
------------------------------------------------------------            
Date     Review By          Comments        
drop Proc Proc_GetNameInsuredPolicies        
------   ------------       -------------------------*/            
CREATE   PROCEDURE dbo.Proc_GetNameInsuredPolicies            
(            
 @CUSTOMER_ID  int,      
 @APPLICANT_ID int                 
)            
AS   
          
BEGIN            
  IF EXISTS(SELECT POLICY_NUMBER FROM POL_CUSTOMER_POLICY_LIST PCL INNER JOIN POL_APPLICANT_LIST  PL ON    
   PCL.POLICY_ID=PL.POLICY_ID AND PCL.CUSTOMER_ID=PL.CUSTOMER_ID                    
   WHERE PL.CUSTOMER_ID = @CUSTOMER_ID AND APPLICANT_ID=@APPLICANT_ID  
   )  
  BEGIN  
   SELECT DISTINCT(POLICY_NUMBER),APP_STATUS     
   FROM POL_CUSTOMER_POLICY_LIST PCL INNER JOIN POL_APPLICANT_LIST  PL ON    
   PCL.POLICY_ID=PL.POLICY_ID AND PCL.CUSTOMER_ID=PL.CUSTOMER_ID                    
   WHERE PL.CUSTOMER_ID = @CUSTOMER_ID AND APPLICANT_ID=@APPLICANT_ID   
  END   
  
  ELSE   
  BEGIN   
    IF EXISTS(SELECT APP_NUMBER FROM APP_LIST AL INNER JOIN APP_APPLICANT_LIST CL ON    
     AL.APP_ID=CL.APP_ID AND AL.CUSTOMER_ID=CL.CUSTOMER_ID                    
     WHERE CL.CUSTOMER_ID =@CUSTOMER_ID AND APPLICANT_ID=@APPLICANT_ID  
    )  
    BEGIN  
   SELECT DISTINCT(APP_NUMBER),APP_STATUS FROM APP_LIST AL INNER JOIN APP_APPLICANT_LIST CL ON    
   AL.APP_ID=CL.APP_ID AND AL.CUSTOMER_ID=CL.CUSTOMER_ID                    
   WHERE CL.CUSTOMER_ID =@CUSTOMER_ID AND APPLICANT_ID=@APPLICANT_ID  
 END  
  END  
END
GO

