IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_Delete_HOME_ENDORSEMENT_BY_ID_FOR_POLICY]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_Delete_HOME_ENDORSEMENT_BY_ID_FOR_POLICY]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                      
Proc Name   : dbo.Proc_Delete_HOME_ENDORSEMENT_BY_ID                     
Created by  : Sumit Chhabra                      
Date        : 041/01/06                    
Purpose     :  Delete  an appropriate endorsemnt in           
  APP_VEHICLE_ENDORSEMENTS          
Revison History  :                            
------------------------------------------------------------                                  
Date     Review By          Comments                                
-----------------------------------------------------------*/                
CREATE   PROCEDURE Proc_Delete_HOME_ENDORSEMENT_BY_ID_FOR_POLICY             
(               
  @CUSTOMER_ID int,              
  @POL_ID int,              
  @POL_VERSION_ID smallint,               
  @ENDORSEMENT_ID smallint,          
  @DWELLING_ID smallint              
)              
              
As              
begin               
 IF EXISTS          
 (          
   SELECT * FROM POL_DWELLING_ENDORSEMENTS          
   WHERE CUSTOMER_ID = @CUSTOMER_ID AND          
    POLICY_ID = @POL_ID AND          
    POLICY_VERSION_ID = @POL_VERSION_ID AND          
    DWELLING_ID = @DWELLING_ID  AND        
    ENDORSEMENT_ID = @ENDORSEMENT_ID         
 )      
      
 DELETE FROM POL_DWELLING_ENDORSEMENTS   
    WHERE CUSTOMER_ID = @CUSTOMER_ID AND          
    POLICY_ID = @POL_ID AND          
    POLICY_VERSION_ID = @POL_VERSION_ID AND          
    DWELLING_ID = @DWELLING_ID  AND        
    ENDORSEMENT_ID = @ENDORSEMENT_ID         
END              
              
  
              
              
              
            
          
        
      
    
  



GO

