/*----------------------------------------------------------                                                    
Proc Name             : Dbo.[Proc_GetInterestRates]                                                    
Created by            : Aditya Goel                                                   
Date                  : 03/11/2011                                                   
Purpose               : To get Interest Rate details          
Revison History       :                                                    
Used In               : Maintenance module          
------------------------------------------------------------                                                    
Date     Review By          Comments                       
              
drop Proc [Proc_GetInterestRates]  6                                         
------   ------------       -------------------------*/            
          
CREATE PROC [dbo].[Proc_GetInterestRates]         
          
@INTEREST_RATE_ID   int            
          
AS                                                                                      
BEGIN               
            
    SELECT INTEREST_RATE_ID,
    RATE_EFFECTIVE_DATE,
    NO_OF_INSTALLMENTS,
    INTEREST_TYPE,
    INTEREST_RATE        
  FROM [dbo].[MNT_INTEREST_RATES] WITH(NOLOCK)        
  WHERE (INTEREST_RATE_ID=@INTEREST_RATE_ID AND IS_ACTIVE='Y')          
        
            
END            
          
          