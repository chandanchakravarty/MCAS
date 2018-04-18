IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_COPY_CYCLE_POLICY_LEVEL_COVERAGES]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_COPY_CYCLE_POLICY_LEVEL_COVERAGES]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                  
Proc Name       : dbo.Proc_COPY_CYCLE_POLICY_LEVEL_COVERAGES                  
Created by      : Pravesh K Chandel
Date            : 14 April 2009
Purpose  : Copied policy level coverages for MotorCycle

Revison History :                  
Used In        : Wolverine              

------------------------------------------------------------                  
Date     Review By          Comments                  
------   ------------       -------------------------*/                  
--drop proc Proc_COPY_CYCLE_POLICY_LEVEL_COVERAGES
CREATE PROC dbo.Proc_COPY_CYCLE_POLICY_LEVEL_COVERAGES
(                  
@CUSTOMER_ID     int,                  
@APP_ID     int,                  
@APP_VERSION_ID     smallint,                  
@VEHICLE_ID     smallint 
)
as
BEGIN
declare @COMPRH_ONLY int

 SELECT @COMPRH_ONLY=isnull(COMPRH_ONLY,0)
	FROM    APP_VEHICLES    WITH(NOLOCK)        
    WHERE CUSTOMER_ID = @CUSTOMER_ID AND            
     APP_ID = @APP_ID AND        
     APP_VERSION_ID = @APP_VERSION_ID AND        
     VEHICLE_ID = @VEHICLE_ID 

if (isnull(@COMPRH_ONLY,0)!=10963)
BEGIN
 --Copy Policy level vehicles from any other vehicle if it exists--            
 IF EXISTS             
 (            
  SELECT TOP 1 * FROM             
  APP_VEHICLE_COVERAGES            
  WHERE CUSTOMER_ID = @CUSTOMER_ID AND            
   APP_ID = @APP_ID AND            
   APP_VERSION_ID = @APP_VERSION_ID AND            
   VEHICLE_ID <> @VEHICLE_ID 
 )            
 BEGIN            
   DECLARE @TEMP_ERROR Int            
   SET @TEMP_ERROR = 0            
              
    SELECT  AVC.* INTO #APP_VEHICLE_COVERAGES             
   FROM APP_VEHICLE_COVERAGES AVC            
   INNER JOIN MNT_COVERAGE C ON            
    AVC.COVERAGE_CODE_ID = C.COV_ID             
   WHERE CUSTOMER_ID = @CUSTOMER_ID AND            
    APP_ID = @APP_ID AND            
    APP_VERSION_ID = @APP_VERSION_ID AND            
    C.COVERAGE_TYPE = 'PL' AND            
    VEHICLE_ID =             
    (            
     SELECT TOP 1 VEHICLE_ID FROM             
     APP_VEHICLES            
     WHERE CUSTOMER_ID = @CUSTOMER_ID AND            
      APP_ID = @APP_ID AND            
      APP_VERSION_ID = @APP_VERSION_ID AND            
      VEHICLE_ID <> @VEHICLE_ID 
	AND isnull(COMPRH_ONLY,0)!=10963          
    )             
               
     SET @TEMP_ERROR = @@ERROR            
                                  
       UPDATE #APP_VEHICLE_COVERAGES             
      SET VEHICLE_ID = @VEHICLE_ID            
                             
         SET @TEMP_ERROR = @@ERROR            
                   
               
        SELECT * FROM #APP_VEHICLE_COVERAGES            
              
   print(@@rowcount)            
                          
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
		AND isnull(COMPRH_ONLY,0)!=10963 
	)         
	UPDATE #APP_VEHICLE_ENDORSEMENTS SET VEHICLE_ID = @VEHICLE_ID  

	INSERT INTO APP_VEHICLE_ENDORSEMENTS
	 SELECT * FROM #APP_VEHICLE_ENDORSEMENTS 
    SET @TEMP_ERROR = @@ERROR 
	DROP TABLE #APP_VEHICLE_ENDORSEMENTS                             
--END HERE 
	SET @TEMP_ERROR = @@ERROR          
       DROP TABLE #APP_VEHICLE_COVERAGES                                  
      SET @TEMP_ERROR = @@ERROR            
              
   IF (  @TEMP_ERROR  <> 0 )            
   BEGIN            
     RAISERROR ('Unable to copy Policy Level Coverages', 16, 1)            
     RETURN            
   END            
 END           
END

END





GO

