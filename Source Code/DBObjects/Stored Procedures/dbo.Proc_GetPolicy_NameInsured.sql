IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetPolicy_NameInsured]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetPolicy_NameInsured]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------            
Proc Name          : Dbo.Proc_GetPolicy_NameInsured         
Created by         : Sibin Philip          
Date               : 16-2-2009          
Purpose            :             
Revison History :            
Used In            :   Wolverine              
------------------------------------------------------------            
Date     Review By          Comments        
drop Proc Proc_GetPolicy_NameInsured        
------   ------------       -------------------------*/            
CREATE   PROCEDURE [dbo].[Proc_GetPolicy_NameInsured]          
(            
 @CUSTOMER_ID  int,      
 @APPLICANT_ID int,     
 @RESULT int OUTPUT                
)            
AS            
BEGIN   
DECLARE @COUNT_CLT INT  
DECLARE @COUNT_POL INT               
   
SET @COUNT_CLT =(SELECT COUNT(APP_NUMBER) FROM APP_LIST AL INNER JOIN POL_APPLICANT_LIST CL ON    
     AL.APP_ID=CL.POLICY_ID AND AL.CUSTOMER_ID=CL.CUSTOMER_ID                    
     WHERE CL.CUSTOMER_ID =@CUSTOMER_ID AND APPLICANT_ID=@APPLICANT_ID    
    )      
SET @COUNT_POL =(SELECT COUNT(POLICY_NUMBER) FROM POL_CUSTOMER_POLICY_LIST PCL INNER JOIN POL_APPLICANT_LIST PL ON     PCL.POLICY_ID=PL.POLICY_ID AND PCL.CUSTOMER_ID=PL.CUSTOMER_ID                    
        WHERE PL.CUSTOMER_ID = @CUSTOMER_ID AND APPLICANT_ID=@APPLICANT_ID  
    )  
  
IF((@COUNT_CLT + @COUNT_POL)>0)  
 BEGIN    
   SET @RESULT = 1    
 END    
  
ELSE    
 BEGIN    
  SET @RESULT =0    
 END    
   
END
GO

