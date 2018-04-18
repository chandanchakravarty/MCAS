IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_Delete_POL_VEHICLE_COVERAGES_BY_ID]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_Delete_POL_VEHICLE_COVERAGES_BY_ID]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------            
Proc Name   : dbo.Proc_DeleteAPP_VEHICLE_COVERAGES_BY_ID           
Created by  : Pradeep            
Date        : Feb 03, 2006  
Purpose     :             
Revison History  :                  
------------------------------------------------------------                        
Date     Review By          Comments                      
-----------------------------------------------------------*/      
CREATE   PROCEDURE Proc_Delete_POL_VEHICLE_COVERAGES_BY_ID    
(     
 @CUSTOMER_ID int,    
 @POLICY_ID int,    
 @POLICY_VERSION_ID smallint,     
 @COVERAGE_CODE_ID smallint,    
 @VEHICLE_ID smallint    
)    
    
As    
    
DECLARE @COV_ID Int    
DECLARE @END_ID smallint     
    
SELECT @COV_ID = COVERAGE_CODE_ID    
FROM POL_VEHICLE_COVERAGES    
WHERE CUSTOMER_ID = @CUSTOMER_ID AND    
      POLICY_ID =  @POLICY_ID AND    
      POLICY_VERSION_ID =  @POLICY_VERSION_ID AND      
      COVERAGE_CODE_ID =  @COVERAGE_CODE_ID AND    
 VEHICLE_ID =  @VEHICLE_ID    
    
DELETE FROM POL_VEHICLE_COVERAGES    
WHERE CUSTOMER_ID = @CUSTOMER_ID AND    
      POLICY_ID =  @POLICY_ID AND    
      POLICY_VERSION_ID =  @POLICY_VERSION_ID AND      
      COVERAGE_CODE_ID =  @COVERAGE_CODE_ID AND    
 VEHICLE_ID =  @VEHICLE_ID    
    
    
    
--Delete dependent endorsements from Vehicle endorsements    
    
 SELECT @END_ID = VE.VEHICLE_ENDORSEMENT_ID     
 FROM MNT_ENDORSMENT_DETAILS ED    
 INNER JOIN POL_VEHICLE_ENDORSEMENTS VE ON    
  ED.ENDORSMENT_ID = VE.ENDORSEMENT_ID    
 WHERE VE.CUSTOMER_ID = @CUSTOMER_ID AND    
       VE.POLICY_ID =  @POLICY_ID AND    
       VE.POLICY_VERSION_ID =  @POLICY_VERSION_ID AND      
       ED.SELECT_COVERAGE =  @COV_ID AND    
  VE.VEHICLE_ID =  @VEHICLE_ID    
    
    
IF ( @END_ID IS NOT NULL )    
BEGIN    
DELETE FROM POL_VEHICLE_ENDORSEMENTS     
WHERE CUSTOMER_ID = @CUSTOMER_ID AND    
       POLICY_ID =  @POLICY_ID AND    
       POLICY_VERSION_ID =  @POLICY_VERSION_ID AND      
  VEHICLE_ID =  @VEHICLE_ID AND    
  VEHICLE_ENDORSEMENT_ID = @END_ID    
END    
    
    
RETURN 1    
    
    
    
  



GO

