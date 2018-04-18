IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_Delete_APP_WATERCRAFT_ENDORSEMENT_BY_ID]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_Delete_APP_WATERCRAFT_ENDORSEMENT_BY_ID]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                  
Proc Name   : dbo.Proc_Delete_APP_WATERCRAFT_ENDORSEMENT_BY_ID                 
Created by  : Ravindra                  
Date        : 07-31-2006
Purpose     :  Delete an appropriate endorsemnt in       
		  APP_WATERCRAFT_ENDORSEMENTS      
Revison History  :                        
------------------------------------------------------------                              
Date     Review By          Comments                            
-----------------------------------------------------------*/            
CREATE   PROCEDURE Proc_Delete_APP_WATERCRAFT_ENDORSEMENT_BY_ID          
(           
 @CUSTOMER_ID int,          
 @APP_ID int,          
 @APP_VERSION_ID smallint,           
 @ENDORSEMENT_ID smallint,      
 @BOAT_ID smallint          
)          
          
As          
           
 IF  EXISTS      
 (      
  SELECT CUSTOMER_ID FROM APP_WATERCRAFT_ENDORSEMENTS      
  WHERE CUSTOMER_ID = @CUSTOMER_ID AND      
   APP_ID = @APP_ID AND      
   APP_VERSION_ID = @APP_VERSION_ID AND      
   BOAT_ID = @BOAT_ID  AND    
   ENDORSEMENT_ID = @ENDORSEMENT_ID     
 )  
 BEGIN  
	DELETE FROM APP_WATERCRAFT_ENDORSEMENTS  
	WHERE CUSTOMER_ID = CUSTOMER_ID AND  
	 APP_ID = @APP_ID AND  
	 APP_VERSION_ID = @APP_VERSION_ID AND  
	 BOAT_ID = @BOAT_ID  
	 AND ENDORSEMENT_ID = @ENDORSEMENT_ID     
END          
          
RETURN 1          
          
          
          
        
      
    
  





GO

