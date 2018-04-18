IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_COPY_CYCLE_POLICY_LEVEL_COVERAGES_POL]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_COPY_CYCLE_POLICY_LEVEL_COVERAGES_POL]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--drop proc dbo.Proc_COPY_CYCLE_POLICY_LEVEL_COVERAGES_POL  
/*----------------------------------------------------------            
Proc Name       : dbo.Proc_COPY_CYCLE_POLICY_LEVEL_COVERAGES_POL  
Created by      : Pravesh K Chandel
Date            : 14 April 2009
Purpose        :Copy Policy Level Coverages

Revison History :            
Used In         : Wolverine      
    
------------------------------------------------------------            
Date     Review By          Comments            
------   ------------       -------------------------*/        
CREATE PROC dbo.Proc_COPY_CYCLE_POLICY_LEVEL_COVERAGES_POL  
(       
 @CUSTOMER_ID     int,            
 @POLICY_ID     int,            
 @POLICY_VERSION_ID     smallint,            
 @VEHICLE_ID     smallint        
)  
AS  
BEGIN  
  
--Copy Policy level vehicles from any other vehicle if it exists--    
declare @COMPRH_ONLY int

 SELECT @COMPRH_ONLY=isnull(COMPRH_ONLY,0)
	FROM    POL_VEHICLES    WITH(NOLOCK)        
    WHERE CUSTOMER_ID = @CUSTOMER_ID AND            
     POLICY_ID = @POLICY_ID AND        
     POLICY_VERSION_ID = @POLICY_VERSION_ID AND        
     VEHICLE_ID = @VEHICLE_ID 

if (isnull(@COMPRH_ONLY,0)!=10963)
BEGIN
    
IF EXISTS         
(        
 SELECT TOP 1 * FROM         
 POL_VEHICLE_COVERAGES        
 WHERE CUSTOMER_ID = @CUSTOMER_ID AND        
  POLICY_ID = @POLICY_ID AND        
  POLICY_VERSION_ID = @POLICY_VERSION_ID AND        
  VEHICLE_ID <> @VEHICLE_ID        
)        
BEGIN        
  DECLARE @TEMP_ERROR Int        
  SET @TEMP_ERROR = 0        
         
   SELECT  AVC.* INTO #POL_VEHICLE_COVERAGES         
  FROM POL_VEHICLE_COVERAGES AVC        
  INNER JOIN MNT_COVERAGE C ON        
   AVC.COVERAGE_CODE_ID = C.COV_ID         
  WHERE CUSTOMER_ID = @CUSTOMER_ID AND        
   POLICY_ID = @POLICY_ID AND        
   POLICY_VERSION_ID = @POLICY_VERSION_ID AND        
   C.COVERAGE_TYPE = 'PL' AND        
   VEHICLE_ID =         
   (        
    SELECT TOP 1 VEHICLE_ID FROM         
    POL_VEHICLES        
    WHERE CUSTOMER_ID = @CUSTOMER_ID AND        
     POLICY_ID = @POLICY_ID AND        
     POLICY_VERSION_ID = @POLICY_VERSION_ID AND        
     VEHICLE_ID <> @VEHICLE_ID 
	AND  ISNULL(COMPRH_ONLY,0)!=10963      
   )         
          
    SET @TEMP_ERROR = @@ERROR        
      UPDATE #POL_VEHICLE_COVERAGES         
     SET VEHICLE_ID = @VEHICLE_ID        
                        
        SET @TEMP_ERROR = @@ERROR        
              
       IF NOT EXISTS
       (	   
       		SELECT * FROM #POL_VEHICLE_COVERAGES        
        )
	BEGIN
		DROP TABLE #POL_VEHICLE_COVERAGES
		RETURN
	END 
    
                     
      INSERT INTO POL_VEHICLE_COVERAGES         
      SELECT * FROM #POL_VEHICLE_COVERAGES                              
              
       SET @TEMP_ERROR = @@ERROR   
--INSERT ASSOCIATED ENDORSEMENT --ADDED BY pravesh on 26 aug 2008
 SELECT PVE.* INTO  #POL_VEHICLE_ENDORSEMENTS 
	 FROM POL_VEHICLE_ENDORSEMENTS PVE
	 INNER JOIN  MNT_ENDORSMENT_DETAILS MED ON MED.ENDORSMENT_ID=PVE.ENDORSEMENT_ID
	 WHERE CUSTOMER_ID = @CUSTOMER_ID AND        
	  POLICY_ID = @POLICY_ID AND        
	  POLICY_VERSION_ID = @POLICY_VERSION_ID AND        
	  VEHICLE_ID <> @VEHICLE_ID   
	AND SELECT_COVERAGE IN (SELECT COVERAGE_CODE_ID FROM #POL_VEHICLE_COVERAGES)
	AND ISNULL(MED.ENDORS_ASSOC_COVERAGE,'')='Y'
    AND VEHICLE_ID =         
    (        
		SELECT TOP 1 VEHICLE_ID FROM         
		POL_VEHICLES        
		WHERE CUSTOMER_ID = @CUSTOMER_ID AND        
		POLICY_ID = @POLICY_ID AND        
		POLICY_VERSION_ID = @POLICY_VERSION_ID AND        
		VEHICLE_ID <> @VEHICLE_ID 
		AND ISNULL(COMPRH_ONLY,0)!=10963          
	)         
      
	UPDATE #POL_VEHICLE_ENDORSEMENTS SET VEHICLE_ID = @VEHICLE_ID  
	
	INSERT INTO POL_VEHICLE_ENDORSEMENTS
	SELECT * FROM #POL_VEHICLE_ENDORSEMENTS 
      SET @TEMP_ERROR = @@ERROR 
	DROP TABLE #POL_VEHICLE_ENDORSEMENTS                             
--END HERE 
  SET @TEMP_ERROR = @@ERROR 
      DROP TABLE #POL_VEHICLE_COVERAGES                              
          
     SET @TEMP_ERROR = @@ERROR        
         
  IF (   @TEMP_ERROR  <> 0 )        
  BEGIN        
    RAISERROR ('Unable to copy Policy Level Coverages', 16, 1)        
    RETURN        
  END        
END        
------------------   End of policy level              
END

END  













GO

