IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_Get_POLICY_STATE]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_Get_POLICY_STATE]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


--drop proc  Proc_Get_POLICY_STATE

/*                    
----------------------------------------------------------                            
Proc Name       : dbo.Proc_Get_POLICY_STATE                        
Created by      : Pradeep                          
Date            : 26 May,2005                            
Purpose         : Gets the State and Product for the LOB                            
Revison History :                            
Used In         : Wolverine                            
------------------------------------------------------------                            
Date     Review By          Comments                            
------   ------------       -------------------------                           
*/                        
                        
CREATE PROCEDURE Proc_Get_POLICY_STATE                        
(                        
  @CUSTOMER_ID int,                        
  @APP_ID int,                        
  @APP_VERSION_ID smallint
             
)                        
                        
As                        
                        
                      

SELECT MNT_LOOKUP_VALUES.LOOKUP_VALUE_CODE,          
 MNT_COUNTRY_STATE_LIST.STATE_ID,          
 APP_LIST.POLICY_TYPE          
 FROM APP_LIST                      
INNER JOIN MNT_LOOKUP_VALUES ON POLICY_TYPE=LOOKUP_UNIQUE_ID                    
INNER JOIN  MNT_COUNTRY_STATE_LIST ON MNT_COUNTRY_STATE_LIST.STATE_ID =APP_LIST.STATE_ID                    
WHERE CUSTOMER_ID= @CUSTOMER_ID AND 
	APP_ID=@APP_ID AND 
	APP_VERSION_ID= @APP_VERSION_ID                      
                      
                      
                    
                    
                    
                    
                       
                  
                
              
              
              
            
          
        
      
    
  



GO

