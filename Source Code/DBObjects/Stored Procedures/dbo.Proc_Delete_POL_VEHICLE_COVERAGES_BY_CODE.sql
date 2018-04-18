IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_Delete_POL_VEHICLE_COVERAGES_BY_CODE]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_Delete_POL_VEHICLE_COVERAGES_BY_CODE]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
 
/*----------------------------------------------------------          
Proc Name   : dbo.Proc_Delete_POL_VEHICLE_COVERAGES_BY_CODE         
Created by  : Pradeep          
Date        : 16 June,2005        
Purpose     :  Deletes coverage from POL_VEHICLE_COVERAGE and
		dependent endorsements from MNT_ENDORSEMENT         
Revison History  :                
------------------------------------------------------------                      
Date     Review By          Comments                    
-----------------------------------------------------------*/    
CREATE   PROCEDURE dbo.Proc_Delete_POL_VEHICLE_COVERAGES_BY_CODE  
(   
 @CUSTOMER_ID int,  
 @POLICY_ID int,  
 @POLICY_VERSION_ID smallint,   
 @COVERAGE_CODE VarChar(10),  
 @VEHICLE_ID smallint  
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
FROM POL_VEHICLE_COVERAGES  AVC
INNER JOIN MNT_COVERAGE C ON
	AVC.COVERAGE_CODE_ID = C.COV_ID
WHERE AVC.CUSTOMER_ID = @CUSTOMER_ID AND  
      AVC.POLICY_ID =  @POLICY_ID AND  
      AVC.POLICY_VERSION_ID =  @POLICY_VERSION_ID AND    
 	AVC.VEHICLE_ID =  @VEHICLE_ID  AND
	C.STATE_ID = @STATE_ID AND
	C.LOB_ID = @LOB_ID AND
	C.COV_CODE = @COVERAGE_CODE

IF ( @COV_ID IS NULL )
BEGIN
	RETURN

END

DELETE FROM POL_VEHICLE_COVERAGES  
WHERE CUSTOMER_ID = @CUSTOMER_ID AND  
      POLICY_ID =  @POLICY_ID AND  
      POLICY_VERSION_ID =  @POLICY_VERSION_ID AND    
      COVERAGE_ID =  @COVERAGE_ID AND  
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

