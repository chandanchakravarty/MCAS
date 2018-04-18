IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_Insert_APP_WATERCRAFT_ENDORSEMENT_BY_ID]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_Insert_APP_WATERCRAFT_ENDORSEMENT_BY_ID]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                  
Proc Name   : dbo.Proc_Insert_APP_WATERCRAFT_ENDORSEMENT_BY_ID                 
Created by  : Pradeep                  
Date        : 13 Mar, 2006            
Purpose     :  Inserts an appropriate endorsemnt in       
  APP_WATERCRAFT_ENDORSEMENTS      
Revison History  :                        
------------------------------------------------------------                              
Date     Review By          Comments                            
-----------------------------------------------------------*/            
CREATE   PROCEDURE Proc_Insert_APP_WATERCRAFT_ENDORSEMENT_BY_ID          
(           
 @CUSTOMER_ID int,          
 @APP_ID int,          
 @APP_VERSION_ID smallint,           
 @ENDORSEMENT_ID smallint,      
 @BOAT_ID smallint          
)          
          
As          
           
 IF NOT EXISTS      
 (      
  SELECT * FROM APP_WATERCRAFT_ENDORSEMENTS      
  WHERE CUSTOMER_ID = @CUSTOMER_ID AND      
   APP_ID = @APP_ID AND      
   APP_VERSION_ID = @APP_VERSION_ID AND      
   BOAT_ID = @BOAT_ID  AND    
   ENDORSEMENT_ID = @ENDORSEMENT_ID     
 )  
 BEGIN  
   
 DECLARE @VEH_END_ID Int  
  
SELECT  @VEH_END_ID   = ISNULL(MAX ( VEHICLE_ENDORSEMENT_ID ),0) + 1  
FROM APP_WATERCRAFT_ENDORSEMENTS  
WHERE CUSTOMER_ID = CUSTOMER_ID AND  
 APP_ID = @APP_ID AND  
 APP_VERSION_ID = @APP_VERSION_ID AND  
 BOAT_ID = @BOAT_ID  
 INSERT INTO APP_WATERCRAFT_ENDORSEMENTS      
 (      
  CUSTOMER_ID,      
  APP_ID,      
  APP_VERSION_ID,      
  BOAT_ID,      
  ENDORSEMENT_ID,  
  VEHICLE_ENDORSEMENT_ID       
 )      
 VALUES      
 (      
  @CUSTOMER_ID,      
  @APP_ID,      
  @APP_VERSION_ID,      
  @BOAT_ID,      
  @ENDORSEMENT_ID ,  
  @VEH_END_ID     
 )      
END          
          
RETURN 1          
          
          
          
        
      
    
  



GO

