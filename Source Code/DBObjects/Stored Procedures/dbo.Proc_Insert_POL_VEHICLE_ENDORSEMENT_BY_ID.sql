IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_Insert_POL_VEHICLE_ENDORSEMENT_BY_ID]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_Insert_POL_VEHICLE_ENDORSEMENT_BY_ID]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


--drop proc Proc_Insert_POL_VEHICLE_ENDORSEMENT_BY_ID
/*----------------------------------------------------------                    
Proc Name   : dbo.Proc_Insert_POL_VEHICLE_ENDORSEMENT_BY_ID                   
Created by  : Pradeep                    
Date        : 22 Feb,2006                  
Purpose     :  Inserts an appropriate endorsemnt in         
    POL_VEHICLE_ENDORSEMENTS        
Revison History  :                          
------------------------------------------------------------                                
Date     Review By          Comments                              
-----------------------------------------------------------*/              
CREATE   PROCEDURE Proc_Insert_POL_VEHICLE_ENDORSEMENT_BY_ID            
(             
 @CUSTOMER_ID int,            
 @POLICY_ID int,            
 @POLICY_VERSION_ID smallint,             
 @ENDORSEMENT_ID smallint,        
 @VEHICLE_ID smallint            
)            
            
As            
             
 IF NOT EXISTS        
 (        
  SELECT * FROM POL_VEHICLE_ENDORSEMENTS        
  WHERE CUSTOMER_ID = @CUSTOMER_ID AND        
   POLICY_ID = @POLICY_ID AND        
   POLICY_VERSION_ID = @POLICY_VERSION_ID AND        
   VEHICLE_ID = @VEHICLE_ID  AND      
   ENDORSEMENT_ID = @ENDORSEMENT_ID       
 )    
 BEGIN    
     
 DECLARE @VEH_END_ID Int    
    
SELECT  @VEH_END_ID   = ISNULL(MAX ( VEHICLE_ENDORSEMENT_ID ),0) + 1    
FROM POL_VEHICLE_ENDORSEMENTS    
WHERE CUSTOMER_ID = @CUSTOMER_ID AND    
 POLICY_ID = @POLICY_ID AND    
 POLICY_VERSION_ID = @POLICY_VERSION_ID AND    
 VEHICLE_ID = @VEHICLE_ID    
  
 INSERT INTO POL_VEHICLE_ENDORSEMENTS        
 (        
  CUSTOMER_ID,        
  POLICY_ID,        
  POLICY_VERSION_ID,        
  VEHICLE_ID,        
  ENDORSEMENT_ID,    
  VEHICLE_ENDORSEMENT_ID         
 )        
 VALUES        
 (        
  @CUSTOMER_ID,        
  @POLICY_ID,        
  @POLICY_VERSION_ID,        
  @VEHICLE_ID,        
  @ENDORSEMENT_ID ,    
  @VEH_END_ID       
 )        
END            
            
RETURN 1            
            
            
            
          
        
      
    
  



GO

