IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_Get_MOTORCYCLE_CC_RANGES]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_Get_MOTORCYCLE_CC_RANGES]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


--drop proc Proc_Get_MOTORCYCLE_CC_RANGES                            
/*                                                    
----------------------------------------------------------                                                        
Proc Name       : dbo.Proc_Get_APP_MOTORCYCLE_CC_RANGES                                                    
Created by      : Pradeep                                                      
Date            : 26 May,2005                                                        
Purpose         : Selects a single record from UMBRELLA_LIMITS                                                        
Revison History :                                                        
Used In         : Wolverine                                                        
------------------------------------------------------------                                                        
Date     Review By          Comments                                                        
------   ------------       -------------------------                                                       
*/                                                    
                                                    
CREATE PROCEDURE Proc_Get_MOTORCYCLE_CC_RANGES
(                                                    
 @STATE_ID int                                                  

                                              
)                                                    
                                                    
As                                                    
                                                    
BEGIN                
                                                  
	--Get Motorcycle CC master table  
	SELECT * FROM dbo.VIW_MOTOR_MIN_DED
	WHERE STATE_ID = @STATE_ID 
                                                 
END                                                    
                                                  
                                                     
                          
                                                    
                                                    
                                                  
                                                  
                                                  
                                                
                                                
                                              
                                            
                                          
                                        
                                      
                                    
                                  
                                
                              
             
                          
                        
                      
                    
                  
                
              
            
          
        
      
    
  



GO

