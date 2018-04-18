IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_DeletePOL_VEHICLE_COVERAGES_ALL]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_DeletePOL_VEHICLE_COVERAGES_ALL]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------            
Proc Name   : dbo.Proc_DeletePOL_VEHICLE_COVERAGES_ALL           
Created by  : Pradeep            
Date        : 16 June,2005          
Purpose     :  Deletes linked endorsements from POL_VEHICLE_ENDORSEMENTS and  
  then deletes all coveages for the current vehicle           
Revison History  :                  
------------------------------------------------------------                        
Date     Review By          Comments                      
-----------------------------------------------------------*/      
CREATE   PROCEDURE Proc_DeletePOL_VEHICLE_COVERAGES_ALL    
(     
 @CUSTOMER_ID int,    
 @POLICY_ID int,    
 @POLICY_VERSION_ID smallint,     
 @VEHICLE_ID smallint    
)    
    
As    
  
DECLARE @STATEID Int  
DECLARE @LOBID Int  
  
SELECT @STATEID = STATE_ID,            
 @LOBID = POLICY_LOB            
FROM POL_CUSTOMER_POLICY_LIST            
WHERE CUSTOMER_ID = @CUSTOMER_ID AND            
 POLICY_ID = @POLICY_ID AND            
 POLICY_VERSION_ID = @POLICY_VERSION_ID            
  
--Delete linked endorsements  
DELETE POL_VEHICLE_ENDORSEMENTS  
FROM POL_VEHICLE_ENDORSEMENTS  
INNER JOIN MNT_ENDORSMENT_DETAILS ON  
 POL_VEHICLE_ENDORSEMENTS.ENDORSEMENT_ID = MNT_ENDORSMENT_DETAILS.ENDORSMENT_ID  
WHERE MNT_ENDORSMENT_DETAILS.STATE_ID = @STATEID AND 
 MNT_ENDORSMENT_DETAILS.LOB_ID = @LOBID AND  
MNT_ENDORSMENT_DETAILS.ENDORS_ASSOC_COVERAGE = 'Y' AND  
 POL_VEHICLE_ENDORSEMENTS.CUSTOMER_ID = @CUSTOMER_ID AND  
 POL_VEHICLE_ENDORSEMENTS.POLICY_ID = @POLICY_ID AND  
 POL_VEHICLE_ENDORSEMENTS.POLICY_VERSION_ID = @POLICY_VERSION_ID AND  
 POL_VEHICLE_ENDORSEMENTS.VEHICLE_ID = @VEHICLE_ID  
  
  
--Delete all coverages  
DELETE FROM POL_VEHICLE_COVERAGES  
WHERE CUSTOMER_ID = @CUSTOMER_ID AND  
 POLICY_ID = @POLICY_ID AND  
 POLICY_VERSION_ID = @POLICY_VERSION_ID AND  
 VEHICLE_ID = @VEHICLE_ID  
  
RETURN 1    
  
  
    
    
  



GO

