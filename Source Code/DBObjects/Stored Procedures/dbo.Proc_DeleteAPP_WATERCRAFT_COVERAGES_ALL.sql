IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_DeleteAPP_WATERCRAFT_COVERAGES_ALL]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_DeleteAPP_WATERCRAFT_COVERAGES_ALL]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                      
Proc Name   : dbo.Proc_DeleteAPP_WATERCRAFT_COVERAGES_ALL                     
Created by  : Pradeep                      
Date        : Oct 24, 2005                    
Purpose     :  Deletes linked endorsements from APP_WATERCRAFT_ENDORSEMENTS and            
  then deletes all coveages for the current vehicle                     
Revison History  :                            
------------------------------------------------------------                                  
Date     Review By          Comments                                
-----------------------------------------------------------*/        
--drop proc Proc_DeleteAPP_WATERCRAFT_COVERAGES_ALL            
CREATE   PROCEDURE Proc_DeleteAPP_WATERCRAFT_COVERAGES_ALL              
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
            
--Delete linked endorsements            
DELETE APP_WATERCRAFT_ENDORSEMENTS            
FROM APP_WATERCRAFT_ENDORSEMENTS            
INNER JOIN MNT_ENDORSMENT_DETAILS ON            
 APP_WATERCRAFT_ENDORSEMENTS.ENDORSEMENT_ID = MNT_ENDORSMENT_DETAILS.ENDORSMENT_ID            
WHERE MNT_ENDORSMENT_DETAILS.STATE_ID = @STATEID AND  MNT_ENDORSMENT_DETAILS.LOB_ID = 4 AND  MNT_ENDORSMENT_DETAILS.ENDORS_ASSOC_COVERAGE = 'Y' AND            
 APP_WATERCRAFT_ENDORSEMENTS.CUSTOMER_ID = @CUSTOMER_ID AND            
 APP_WATERCRAFT_ENDORSEMENTS.APP_ID = @APP_ID AND            
 APP_WATERCRAFT_ENDORSEMENTS.APP_VERSION_ID = @APP_VERSION_ID AND            
 APP_WATERCRAFT_ENDORSEMENTS.BOAT_ID = @VEHICLE_ID            
            
            
--Delete all coverages            
DELETE APP_WATERCRAFT_COVERAGE_INFO FROM APP_WATERCRAFT_COVERAGE_INFO          
INNER JOIN MNT_COVERAGE ON          
 APP_WATERCRAFT_COVERAGE_INFO.coverage_code_id = MNT_COVERAGE.COV_ID        
WHERE CUSTOMER_ID = @CUSTOMER_ID AND            
 APP_ID = @APP_ID AND            
 APP_VERSION_ID = @APP_VERSION_ID AND            
 BOAT_ID = @VEHICLE_ID          
        
            
RETURN 1              
            
            
              
              
            
          
          
        
      
    
  



GO

