IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UPDATE_POLICY_VEHICLE_ENDORSEMENTS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UPDATE_POLICY_VEHICLE_ENDORSEMENTS]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


--drop proc Proc_UPDATE_POLICY_VEHICLE_ENDORSEMENTS        
        
/*----------------------------------------------------------                              
Proc Name       : dbo.Proc_UPDATE_POLICY_VEHICLE_ENDORSEMENTS                              
Created by      : Ravindra                              
Date            : 04-25-2006	
Purpose      	: Inserts non - linked endorsemnts for this coverage                            
Revison History :                              
Used In  : Wolverine                              
------------------------------------------------------------                              
Date     Review By          Comments                              
------   ------------       -------------------------*/                              
CREATE    PROC Dbo.Proc_UPDATE_POLICY_VEHICLE_ENDORSEMENTS          
(                              
 @CUSTOMER_ID     int,                              
 @POLICY_ID     int,                              
 @POLICY_VERSION_ID     smallint,                              
 @VEHICLE_ID smallint             
)           
          
AS           
          
BEGIN          
     
     
  DECLARE @STATEID SmallInt                                  
  DECLARE @LOBID NVarCHar(5)               
          
  DECLARE @ENDORSEMENT_ID Int                                                   
  DECLARE @VEHICLE_ENDORSEMENT_ID int                                
                     
          
   SELECT @STATEID = STATE_ID,                                  
   @LOBID = POLICY_LOB
   FROM POL_CUSTOMER_POLICY_LIST
   WHERE CUSTOMER_ID = @CUSTOMER_ID AND                                  
    POLICY_ID = @POLICY_ID AND                                  
    POLICY_VERSION_ID = @POLICY_VERSION_ID             
          
   
  --Insert Endorsement  related to PIP--------------------------------------------------------          
  ------ For Indiana if Limit for PIP is takem as Excess Limit then endorsement
  ----Coordination of Benefits $300 Deductible (A-91)  will be given automatically        
  IF (@STATEID = 22 AND @LOBID = 2)          
 BEGIN          
  --Part A – Personal Injury Protection           
  IF EXISTS        
  (        
  SELECT * FROM POL_VEHICLE_COVERAGES        
  WHERE CUSTOMER_ID = @CUSTOMER_ID AND        
  POLICY_ID = @POLICY_ID AND        
  POLICY_VERSION_ID = @POLICY_VERSION_ID AND        
  VEHICLE_ID = @VEHICLE_ID AND        
  COVERAGE_CODE_ID = 116 AND        
  LIMIT1_AMOUNT_TEXT = 'Excess Medical'        
   
   )        
  BEGIN        
  --Coordination of Benefits $300 Deductible (A-91)          
     EXEC Proc_Insert_POL_VEHICLE_ENDORSEMENT_BY_ID
	     @CUSTOMER_ID,                
             @POLICY_ID,                
             @POLICY_VERSION_ID,                
             42,                
             @VEHICLE_ID             
        IF @@ERROR <> 0                
        BEGIN                
          RETURN                
        END        
  END        
  ELSE        
    BEGIN        
	  EXEC Proc_Delete_POL_VEHICLE_ENDORSEMENT_BY_ID        
	   @CUSTOMER_ID,--@CUSTOMER_ID int,                
	   @POLICY_ID,--@POLICY_ID int,                
	   @POLICY_VERSION_ID,--@POLICY_VERSION_ID smallint,                 
	   42,--@ENDORSEMENT_ID smallint,            
	   @VEHICLE_ID--@VEHICLE_ID smallint                
         
         
    END         
 
 END          
 -------------------------------------------------------------------------------------------------          
              
END                             
        
      
    
  



GO

