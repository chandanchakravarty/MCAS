    
/*----------------------------------------------------------                                
Proc Name      : dbo.[Proc_DeleteInteresrRates]                                
Created by     : Aditya Goel                         
Date           : 03/11/2011          
Modify by      :   
Date           :                      
Purpose        : Delete data from MNT_INTEREST_RATES                                                      
Used In        : Ebix Advantage                            
------------------------------------------------------------                                
Date     Review By          Comments                                
------   ------------       -------------------------*/                                
--drop proc dbo.[Proc_DeleteInteresrRates] 
      
CREATE PROC [dbo].[Proc_DeleteInteresrRates]        
(         
@INTEREST_RATE_ID INT                        
)                    
AS                    
BEGIN                    
  DELETE FROM MNT_INTEREST_RATES  WHERE 
  INTEREST_RATE_ID = @INTEREST_RATE_ID
END    
        
        
     
          