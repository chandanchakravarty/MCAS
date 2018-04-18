IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_Delete_POL_UMBRELLA_COVERAGES_BY_CODE]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_Delete_POL_UMBRELLA_COVERAGES_BY_CODE]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------              
Proc Name   : dbo.Proc_Delete_POL_UMBRELLA_COVERAGES_BY_CODE             
Created by  : Pradeep              
Date        : 16 June,2005            
Purpose     :  Deletes coverage from APP_VEHICLE_COVERAGE and    
  dependent endorsements from MNT_ENDORSEMENT             
Revison History  :                    
------------------------------------------------------------                          
Date     Review By          Comments                        
-----------------------------------------------------------*/        
CREATE  PROCEDURE dbo.Proc_Delete_POL_UMBRELLA_COVERAGES_BY_CODE      
(       
 @CUSTOMER_ID int,      
 @POLICY_ID int,      
 @POLICY_VERSION_ID smallint,       
 @COVERAGE_CODE VarChar(10)    
)      
      
As      
       
DECLARE @COV_ID Int      
DECLARE @COVERAGE_ID Int      
DECLARE @END_ID smallint       
DECLARE @STATE_ID Int    
DECLARE @LOB_ID int    
      
SELECT @STATE_ID = STATE_ID,    
 @LOB_ID = POLICY_LOB    
FROM POL_CUSTOMER_POLICY_LIST    
WHERE CUSTOMER_ID = @CUSTOMER_ID AND    
 POLICY_ID = @POLICY_ID AND    
 POLICY_VERSION_ID =  @POLICY_VERSION_ID    
    
SELECT @COV_ID = COVERAGE_CODE_ID , @COVERAGE_ID = COVERAGE_ID     
FROM POL_UMBRELLA_COVERAGES  PUC    
INNER JOIN MNT_COVERAGE C ON    
 PUC.COVERAGE_CODE_ID = C.COV_ID    
WHERE PUC.CUSTOMER_ID = @CUSTOMER_ID AND      
      PUC.POL_ID =  @POLICY_ID AND      
      PUC.POL_VERSION_ID =  @POLICY_VERSION_ID AND        
  C.STATE_ID = @STATE_ID AND    
 C.LOB_ID = @LOB_ID AND    
 C.COV_CODE = @COVERAGE_CODE    
    
IF ( @COV_ID IS NULL )    
BEGIN    
 RETURN    
    
END    
    
DELETE FROM POL_UMBRELLA_COVERAGES      
WHERE CUSTOMER_ID = @CUSTOMER_ID AND      
      POL_ID =  @POLICY_ID AND      
      POL_VERSION_ID =  @POLICY_VERSION_ID AND        
      COVERAGE_ID =  @COVERAGE_ID     
RETURN 1      
      
      
      
    
    
    
  



GO

