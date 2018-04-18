IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_COPY_POLICY_LEVEL_COVERAGES_APP]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_COPY_POLICY_LEVEL_COVERAGES_APP]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/**************************************
Created By	: Pravesh k chandel
Date		: 13 April 2009
Purpose     : Copy Policy level coverages from other vehicle   

modified by	: Pravesh K Chandel
Date		: 24 aPRIL 09
Purpose		: Copy policy Level Covg of Same type Of Vehile Only
------------------------------------------------------------                                
Date     Review By          Comments                                
------   ------------       -------------------------
-- DROP proc dbo.Proc_COPY_POLICY_LEVEL_COVERAGES_APP
*********************************************/                               
CREATE PROC dbo.Proc_COPY_POLICY_LEVEL_COVERAGES_APP                                
(                                
 @CUSTOMER_ID     int,                                
 @APP_ID     int ,                                
 @APP_VERSION_ID     smallint,                                
 @VEHICLE_ID     smallint    
)
as 
BEGIN
--Copy Policy level vehicles from any other vehicle if it exists--            
declare @VEHICLE_TYPE_PER nvarchar(10),@VEHICLE_TYPE_COM nvarchar(10),@IS_SUSPENDED int,@USE_VEHICLE int


 SELECT @VEHICLE_TYPE_PER=VEHICLE_TYPE_PER,@VEHICLE_TYPE_COM=VEHICLE_TYPE_COM,@IS_SUSPENDED=IS_SUSPENDED ,
	@USE_VEHICLE =USE_VEHICLE 
	FROM    APP_VEHICLES    WITH(NOLOCK)        
    WHERE CUSTOMER_ID = @CUSTOMER_ID AND            
     APP_ID = @APP_ID AND            
     APP_VERSION_ID = @APP_VERSION_ID AND            
     VEHICLE_ID = @VEHICLE_ID

if (ISNULL(@VEHICLE_TYPE_PER,'0') NOT IN ('11870','11337','11618') 
	AND ISNULL(@VEHICLE_TYPE_COM,'0')  NOT IN ('11341')
	AND isnull(@IS_SUSPENDED,0)!=10963    
	
  )
BEGIN
  DECLARE @TEMP_ERROR Int            
  SET @TEMP_ERROR = 0            
           
   SELECT  AVC.* INTO #APP_VEHICLE_COVERAGES             
  FROM APP_VEHICLE_COVERAGES AVC   WITH(NOLOCK)         
  INNER JOIN MNT_COVERAGE C ON            
   AVC.COVERAGE_CODE_ID = C.COV_ID             
  WHERE CUSTOMER_ID = @CUSTOMER_ID AND            
   APP_ID = @APP_ID AND            
   APP_VERSION_ID = @APP_VERSION_ID AND            
   C.COVERAGE_TYPE = 'PL' AND            
   VEHICLE_ID =             
   (            
    SELECT TOP 1 VEHICLE_ID FROM             
    APP_VEHICLES    WITH(NOLOCK)        
    WHERE CUSTOMER_ID = @CUSTOMER_ID AND            
     APP_ID = @APP_ID AND            
     APP_VERSION_ID = @APP_VERSION_ID AND            
     VEHICLE_ID <> @VEHICLE_ID
	AND ISNULL(VEHICLE_TYPE_PER,'0') NOT IN ('11870','11337','11618') 
	AND ISNULL(VEHICLE_TYPE_COM,'0')  NOT IN ('11341')
	AND isnull(IS_SUSPENDED,0)!=10963    
	AND isnull(USE_VEHICLE,'0') =@USE_VEHICLE  -- Itrack 5731
   )  
    SET @TEMP_ERROR = @@ERROR            
      UPDATE #APP_VEHICLE_COVERAGES             
     SET VEHICLE_ID = @VEHICLE_ID            
        SET @TEMP_ERROR = @@ERROR            
              
  --     SELECT * FROM #APP_VEHICLE_COVERAGES            
      print(@@rowcount)            
----deleting exiting PL Covg if existing
--	DELETE FROM  APP_VEHICLE_COVERAGES      
--		WHERE CUSTOMER_ID = @CUSTOMER_ID 
--		AND APP_ID = @APP_ID 
--		AND APP_VERSION_ID = @APP_VERSION_ID 
--		AND  VEHICLE_ID =   @VEHICLE_ID
--		AND COVERAGE_CODE_ID IN
--					(
--					SELECT COVERAGE_CODE_ID FROM #APP_VEHICLE_COVERAGES
--					)   
---- END DELETING EXISTING PL cOVERAGES

      INSERT INTO APP_VEHICLE_COVERAGES             
      SELECT * FROM #APP_VEHICLE_COVERAGES                                  
       SET @TEMP_ERROR = @@ERROR      
--INSERT ASSOCIATED ENDORSEMENT 
 SELECT PVE.* INTO  #APP_VEHICLE_ENDORSEMENTS 
	 FROM APP_VEHICLE_ENDORSEMENTS  PVE
	 INNER JOIN  MNT_ENDORSMENT_DETAILS MED ON MED.ENDORSMENT_ID=PVE.ENDORSEMENT_ID
	 WHERE CUSTOMER_ID = @CUSTOMER_ID AND        
	  APP_ID = @APP_ID AND        
	  APP_VERSION_ID = @APP_VERSION_ID AND        
	  VEHICLE_ID <> @VEHICLE_ID   
	AND SELECT_COVERAGE IN (SELECT COVERAGE_CODE_ID FROM #APP_VEHICLE_COVERAGES)
	AND ISNULL(MED.ENDORS_ASSOC_COVERAGE,'')='Y'
	AND VEHICLE_ID =         
	(        
		SELECT TOP 1 VEHICLE_ID FROM         
		APP_VEHICLES        
		WHERE CUSTOMER_ID = @CUSTOMER_ID AND        
        APP_ID = @APP_ID AND        
	    APP_VERSION_ID = @APP_VERSION_ID AND        
		VEHICLE_ID <> @VEHICLE_ID  
		AND ISNULL(VEHICLE_TYPE_PER,'0') NOT IN ('11870','11337','11618') 
		AND ISNULL(VEHICLE_TYPE_COM,'0')  NOT IN ('11341') 
		AND isnull(IS_SUSPENDED,0)!=10963  
		AND isnull(USE_VEHICLE,'0') =@USE_VEHICLE     -- Itrack 5731
	)         
	UPDATE #APP_VEHICLE_ENDORSEMENTS SET VEHICLE_ID = @VEHICLE_ID  
----deleting exiting PL Covg if existing
--	DELETE FROM APP_VEHICLE_ENDORSEMENTS
--		 WHERE CUSTOMER_ID = @CUSTOMER_ID AND        
--		  APP_ID = @APP_ID AND        
--		  APP_VERSION_ID = @APP_VERSION_ID AND        
--		VEHICLE_ID = @VEHICLE_ID   
--		AND ENDORSEMENT_ID IN
--		(SELECT ENDORSMENT_ID FROM MNT_ENDORSMENT_DETAILS 
--			WHERE SELECT_COVERAGE IN (SELECT COVERAGE_CODE_ID FROM #APP_VEHICLE_COVERAGES)
--		)
----END DELETING

	INSERT INTO APP_VEHICLE_ENDORSEMENTS
	 SELECT * FROM #APP_VEHICLE_ENDORSEMENTS 
    SET @TEMP_ERROR = @@ERROR 
	DROP TABLE #APP_VEHICLE_ENDORSEMENTS                             
--END HERE 
     SET @TEMP_ERROR = @@ERROR            
      DROP TABLE #APP_VEHICLE_COVERAGES  
     SET @TEMP_ERROR = @@ERROR            
             
  IF (   @TEMP_ERROR  <> 0 )            
  BEGIN            
    RAISERROR ('Unable to copy Policy Level Coverages', 16, 1)            
    RETURN            
  END            
END

END 









GO

