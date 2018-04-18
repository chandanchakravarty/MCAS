IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_DeletePolicyWATERCRAFT_COVERAGES]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_DeletePolicyWATERCRAFT_COVERAGES]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------              
Proc Name   : dbo.Proc_DeletePolicyWATERCRAFT_COVERAGES             
Created by  : shafi              
Date        : 24 Feb,2005            
Purpose     :               
Revison History  :                    
 ------------------------------------------------------------                          
Date     Review By          Comments                        
               
------   ------------       -------------------------*/        
--  drop proc Proc_DeletePolicyWATERCRAFT_COVERAGES
CREATE      PROCEDURE Proc_DeletePolicyWATERCRAFT_COVERAGES      
(       
 @CUSTOMER_ID int,      
 @POL_ID int,      
 @POL_VERSION_ID smallint,       
 @BOAT_ID smallint,      
 @COVERAGE_ID smallint      
)      
      
As      
    
DECLARE @COV_ID Int      
DECLARE @END_ID smallint       
    
SELECT @COV_ID = COVERAGE_CODE_ID      
FROM POL_WATERCRAFT_COVERAGE_INFO      
WHERE CUSTOMER_ID = @CUSTOMER_ID AND      
      POLICY_ID =  @POL_ID AND      
      POLICY_VERSION_ID =  @POL_VERSION_ID AND        
      COVERAGE_ID =  @COVERAGE_ID AND      
 BOAT_ID =  @BOAT_ID      
    
DELETE FROM POL_WATERCRAFT_COVERAGE_INFO      
WHERE CUSTOMER_ID = @CUSTOMER_ID AND      
      POLICY_ID =  @POL_ID AND      
      POLICY_VERSION_ID =  @POL_VERSION_ID AND      
      BOAT_ID = @BOAT_ID AND         
      COVERAGE_ID =  @COVERAGE_ID       
    
--Delete dependent endorsements from Vehicle endorsements      
      
 SELECT @END_ID = VE.VEHICLE_ENDORSEMENT_ID       
 FROM MNT_ENDORSMENT_DETAILS ED      
 INNER JOIN POL_WATERCRAFT_ENDORSEMENTS VE ON      
  ED.ENDORSMENT_ID = VE.ENDORSEMENT_ID      
 WHERE VE.CUSTOMER_ID = @CUSTOMER_ID AND      
       VE.POLICY_ID =  @POL_ID AND      
       VE.POLICY_VERSION_ID =  @POL_VERSION_ID AND        
       ED.SELECT_COVERAGE =  @COV_ID AND      
      VE.BOAT_ID =  @BOAT_ID      
      
      
IF ( @END_ID IS NOT NULL )      
BEGIN      
DELETE FROM POL_WATERCRAFT_ENDORSEMENTS       
WHERE CUSTOMER_ID = @CUSTOMER_ID AND      
       POLICY_ID =  @POL_ID AND      
       POLICY_VERSION_ID =  @POL_VERSION_ID AND        
       BOAT_ID =  @BOAT_ID AND      
  VEHICLE_ENDORSEMENT_ID = @END_ID      
END      
      
RETURN 1         
      
      
      
      
      
      
      
      
      
      
      
    
  



GO

