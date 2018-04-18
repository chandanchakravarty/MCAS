IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_Delete_APP_WATERCRAFT_COVERAGES_BY_CODE]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_Delete_APP_WATERCRAFT_COVERAGES_BY_CODE]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


--drop proc Proc_Delete_APP_WATERCRAFT_COVERAGES_BY_CODE

/*----------------------------------------------------------              
Proc Name   : dbo.Proc_Delete_APP_WATERCRAFT_COVERAGES_BY_CODE             
Created by  : Pradeep              
Date        : 12/12/2005         
Purpose     :  Deletes coverage from APP_WATERCRAFT_COVERAGE_INFO and    
    dependent endorsements from APP_WATERCRAFT_ENDORSEMENTS             
Revison History  :                    
------------------------------------------------------------                          
Date     Review By          Comments                        
-----------------------------------------------------------*/        
CREATE   PROCEDURE Proc_Delete_APP_WATERCRAFT_COVERAGES_BY_CODE      
(       
 @CUSTOMER_ID int,      
 @APP_ID int,      
 @APP_VERSION_ID smallint,       
 @COVERAGE_CODE VarChar(10),      
 @BOAT_ID smallint      
)      
      
As      
       
DECLARE @COV_ID Int      
DECLARE @COVERAGE_ID Int      
DECLARE @END_ID smallint       
DECLARE @STATE_ID Int    
DECLARE @LOB_ID int    
      
SELECT @STATE_ID = STATE_ID,    
 @LOB_ID = APP_LOB    
FROM APP_LIST    
WHERE CUSTOMER_ID = @CUSTOMER_ID AND    
 APP_ID = @APP_ID AND    
 APP_VERSION_ID =  @APP_VERSION_ID    
    
     
SELECT @COV_ID = COVERAGE_CODE_ID , @COVERAGE_ID = COVERAGE_ID     
FROM APP_WATERCRAFT_COVERAGE_INFO  AVC    
INNER JOIN MNT_COVERAGE C ON    
 AVC.COVERAGE_CODE_ID = C.COV_ID    
WHERE AVC.CUSTOMER_ID = @CUSTOMER_ID AND      
      AVC.APP_ID =  @APP_ID AND      
      AVC.APP_VERSION_ID =  @APP_VERSION_ID AND        
  AVC.BOAT_ID =  @BOAT_ID  AND    
 C.STATE_ID = @STATE_ID AND    
 C.LOB_ID = 4 AND    
 C.COV_CODE = @COVERAGE_CODE    
    
IF ( @COV_ID IS NULL )    
BEGIN    
 RETURN    
    
END    
    
DELETE FROM APP_WATERCRAFT_COVERAGE_INFO      
WHERE CUSTOMER_ID = @CUSTOMER_ID AND      
      APP_ID =  @APP_ID AND      
      APP_VERSION_ID =  @APP_VERSION_ID AND        
      COVERAGE_ID =  @COVERAGE_ID AND      
 BOAT_ID =  @BOAT_ID      
       
--Delete dependent endorsements from Vehicle endorsements      
 SELECT @END_ID = VE.VEHICLE_ENDORSEMENT_ID       
 FROM MNT_ENDORSMENT_DETAILS ED      
 INNER JOIN APP_WATERCRAFT_ENDORSEMENTS VE ON      
  ED.ENDORSMENT_ID = VE.ENDORSEMENT_ID      
 WHERE VE.CUSTOMER_ID = @CUSTOMER_ID AND      
       VE.APP_ID =  @APP_ID AND      
       VE.APP_VERSION_ID =  @APP_VERSION_ID AND        
       ED.SELECT_COVERAGE =  @COV_ID AND      
  VE.BOAT_ID =  @BOAT_ID   
      
      
IF ( @END_ID IS NOT NULL )      
BEGIN      
DELETE FROM APP_WATERCRAFT_ENDORSEMENTS       
WHERE CUSTOMER_ID = @CUSTOMER_ID AND      
       APP_ID =  @APP_ID AND      
       APP_VERSION_ID =  @APP_VERSION_ID AND        
  BOAT_ID =  @BOAT_ID AND      
  VEHICLE_ENDORSEMENT_ID = @END_ID      
END      
      
      
RETURN 1      
      
      
      
    
  



GO

