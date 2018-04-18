IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_Get_POLICY_STATE_TYPE]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_Get_POLICY_STATE_TYPE]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


--drop proc  Proc_Get_POLICY_STATE_TYPE  
  
/*                      
----------------------------------------------------------                              
Proc Name       : dbo.Proc_Get_POLICY_STATE_TYPE                          
Created by      : shafi                            
Date            : 26 May,2005                              
Purpose         : Gets the State and Product for the LOB                              
Revison History :                              
Used In         : Wolverine                              
------------------------------------------------------------                              
Date     Review By          Comments                              
------   ------------       -------------------------                             
*/                          
                          
CREATE PROCEDURE Proc_Get_POLICY_STATE_TYPE                          
(                          
  @CUSTOMER_ID int,                          
  @POL_ID int,                          
  @POL_VERSION_ID smallint  
               
)                          
                          
As                          
                          
                        
  
SELECT MNT_LOOKUP_VALUES.LOOKUP_VALUE_CODE,            
 MNT_COUNTRY_STATE_LIST.STATE_ID,            
 POL_CUSTOMER_POLICY_LIST.POLICY_TYPE            
 FROM POL_CUSTOMER_POLICY_LIST                        
INNER JOIN MNT_LOOKUP_VALUES ON POLICY_TYPE=LOOKUP_UNIQUE_ID                      
INNER JOIN  MNT_COUNTRY_STATE_LIST ON MNT_COUNTRY_STATE_LIST.STATE_ID =POL_CUSTOMER_POLICY_LIST.STATE_ID                      
WHERE CUSTOMER_ID= @CUSTOMER_ID AND   
 POLICY_ID=@POL_ID AND   
 POLICY_VERSION_ID= @POL_VERSION_ID                        
                        
                        
                      
                      
                      
                      
                         
                    
                  
                
                
                
              
            
          
        
      
    
  



GO

