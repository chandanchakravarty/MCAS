IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_DeleteAPP_UMBRELLA_VEHICLE_COVERAGES_ALL]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_DeleteAPP_UMBRELLA_VEHICLE_COVERAGES_ALL]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------              
Proc Name   : dbo.Proc_DeleteAPP_UMBRELLA_VEHICLE_COVERAGES_ALL             
Created by  : Pradeep              
Date        : 20 Oct,2005            
Purpose     :  Deletes linked endorsements from APP_VEHICLE_ENDORSEMENTS and    
  then deletes all coveages for the current vehicle             
Revison History  :                    
------------------------------------------------------------                          
Date     Review By          Comments                        
-----------------------------------------------------------*/        
CREATE   PROCEDURE Proc_DeleteAPP_UMBRELLA_VEHICLE_COVERAGES_ALL      
(       
 @CUSTOMER_ID int,      
 @APP_ID int,      
 @APP_VERSION_ID smallint,       
 @VEHICLE_ID smallint      
)      
      
As      
    
DECLARE @STATEID Int    
DECLARE @LOBID Int    
    
SELECT @STATEID = STATE_ID,              
 @LOBID = APP_LOB              
FROM APP_LIST              
WHERE CUSTOMER_ID = @CUSTOMER_ID AND              
 APP_ID = @APP_ID AND              
 APP_VERSION_ID = @APP_VERSION_ID              
    
SET @LOBID = 2  
  
--Delete linked endorsements    
DELETE APP_UMBRELLA_VEHICLE_ENDORSEMENTS    
FROM APP_UMBRELLA_VEHICLE_ENDORSEMENTS    
INNER JOIN MNT_ENDORSMENT_DETAILS ON    
 APP_UMBRELLA_VEHICLE_ENDORSEMENTS.ENDORSEMENT_ID = MNT_ENDORSMENT_DETAILS.ENDORSMENT_ID    
WHERE MNT_ENDORSMENT_DETAILS.STATE_ID = @STATEID AND  MNT_ENDORSMENT_DETAILS.LOB_ID = @LOBID AND  MNT_ENDORSMENT_DETAILS.ENDORS_ASSOC_COVERAGE = 'Y' AND    
 APP_UMBRELLA_VEHICLE_ENDORSEMENTS.CUSTOMER_ID = @CUSTOMER_ID AND    
 APP_UMBRELLA_VEHICLE_ENDORSEMENTS.APP_ID = @APP_ID AND    
 APP_UMBRELLA_VEHICLE_ENDORSEMENTS.APP_VERSION_ID = @APP_VERSION_ID AND    
 APP_UMBRELLA_VEHICLE_ENDORSEMENTS.VEHICLE_ID = @VEHICLE_ID    
    
    
--Delete all coverages    
DELETE FROM APP_UMBRELLA_VEHICLE_COV_IFNO
WHERE CUSTOMER_ID = @CUSTOMER_ID AND    
 APP_ID = @APP_ID AND    
 APP_VERSION_ID = @APP_VERSION_ID AND    
 VEHICLE_ID = @VEHICLE_ID    
    
RETURN 1      
    
    
      
      
    
  



GO

