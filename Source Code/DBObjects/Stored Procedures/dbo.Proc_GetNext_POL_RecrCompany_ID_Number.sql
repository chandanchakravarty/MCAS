IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetNext_POL_RecrCompany_ID_Number]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetNext_POL_RecrCompany_ID_Number]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                      
Proc Name   : dbo.Proc_GetNext_POL_RecrCompany_ID_Number                     
Created by  :Pradeep                      
Date        :10 Nov,2005                    
Purpose     : Returns the next COMPANY_ID_NUMBER for the               
          CUSTOMERID, POLICYID and PolicyVersionID                       
 ------------------------------------------------------------                                  
Date     Review By          Comments                                
                       
------   ------------       -------------------------*/                
              
CREATE     PROCEDURE dbo.Proc_GetNext_POL_RecrCompany_ID_Number              
(              
               
 @CUSTOMER_ID Int,              
 @POLICY_ID Int,              
 @POLiCY_VERSION_ID SmallInt              
)              
              
As              
begin              
 DECLARE @MAX int  
 DECLARE @APP_INCEPTION_DATE datetime  
 set @APP_INCEPTION_DATE = null  
  
 select @APP_INCEPTION_DATE = convert(char,APP_INCEPTION_DATE,101) from POL_CUSTOMER_POLICY_LIST   
  WHERE CUSTOMER_ID = @CUSTOMER_ID AND              
    POLICY_ID = @POLICY_ID AND               
    POLICY_VERSION_ID = @POLICY_VERSION_ID and   
    POLICY_STATUS IN ('UISSUE','SUSPENDED')  

               
 SELECT @MAX = ISNULL(MAX(COMPANY_ID_NUMBER),0)              
 FROM POL_HOME_OWNER_RECREATIONAL_VEHICLES              
 WHERE CUSTOMER_ID = @CUSTOMER_ID AND              
  POLICY_ID = @POLICY_ID AND               
  POLICY_VERSION_ID = @POLICY_VERSION_ID               
           
 IF @MAX = 9999          
 BEGIN              
  SELECT -2              
  RETURN                
 END              
          
 set @MAX=@MAX+1          
 SELECT @MAX AS NEXT_COMPANY_ID, @APP_INCEPTION_DATE AS APP_INCEPTION_DATE  
 return 1          
  
end  


GO

