IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_Delete_HOME_ENDORSEMENT_BY_ID]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_Delete_HOME_ENDORSEMENT_BY_ID]
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
CREATE   PROCEDURE Proc_Delete_HOME_ENDORSEMENT_BY_ID            
(             
  @CUSTOMER_ID int,            
  @APP_ID int,            
  @APP_VERSION_ID smallint,             
  @ENDORSEMENT_ID smallint,        
  @DWELLING_ID smallint            
)            
            
As            
begin             
 IF EXISTS        
 (        
   SELECT * FROM APP_DWELLING_ENDORSEMENTS        
   WHERE CUSTOMER_ID = @CUSTOMER_ID AND        
    APP_ID = @APP_ID AND        
    APP_VERSION_ID = @APP_VERSION_ID AND        
    DWELLING_ID = @DWELLING_ID  AND      
    ENDORSEMENT_ID = @ENDORSEMENT_ID       
 )    
    
	DELETE FROM APP_DWELLING_ENDORSEMENTS 
	   WHERE CUSTOMER_ID = @CUSTOMER_ID AND        
    APP_ID = @APP_ID AND        
    APP_VERSION_ID = @APP_VERSION_ID AND        
    DWELLING_ID = @DWELLING_ID  AND      
    ENDORSEMENT_ID = @ENDORSEMENT_ID       
END            
            

            
            
            
          
        
      
    
  



GO

