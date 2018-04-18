IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UPDATE_DRIVER_ENDORSEMENTS_POLICY]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UPDATE_DRIVER_ENDORSEMENTS_POLICY]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


--drop proc Proc_UPDATE_DRIVER_ENDORSEMENTS_POLICY    
    
/*----------------------------------------------------------          
Proc Name       : dbo.Proc_UPDATE_DRIVER_ENDORSEMENTS_POLICY          
Created by      : Pradeep          
Date            : 2/22/2006          
Purpose      : Updates relevant endorsements when driver is updated         
Revison History :          
Used In  : Wolverine          
------------------------------------------------------------          
Date     Review By          Comments          
------   ------------       -------------------------*/          
CREATE PROC Proc_UPDATE_DRIVER_ENDORSEMENTS_POLICY                   
(                      
 @CUSTOMER_ID int,                      
 @POLICY_ID  int,                      
 @POLICY_VERSION_ID int        
        
)                      
AS        
                      
BEGIN                      
        
DECLARE @EXCLUDED_COUNT Int        
DECLARE @WAIVER_COUNT Int        
DECLARE @STATE_ID Int        
DECLARE @A95 Int        
DECLARE @A94 Int        
        
 --39 22 2 B M Driver Exclusion (A-95)        
 --43 22 2 B M PIP Waiver / Waive Work Loss Benefits (A-94) NULL N 46 Y 70 2005-10-11 13:11:51.000 NULL NULL N aiimsquarters.txt PIPWR NULL        
         
 --SET @A95 = 39        
 SET @A94 = 43        
         
 SELECT @STATE_ID = STATE_ID        
 FROM POL_CUSTOMER_POLICY_LIST        
 WHERE CUSTOMER_ID = @CUSTOMER_ID AND        
  POLICY_ID = @POLICY_ID AND        
  POLICY_VERSION_ID = @POLICY_VERSION_ID        
    
 --Michigan       
 IF @STATE_ID = 22    
 BEGIN    
  SET @A95 = 39     
 END    
  
   
 --If State is Indiana return--------------------    
 IF @STATE_ID = 14    
 BEGIN   
  SET @A95 = 21      
  --RETURN    
 END    
 --------    
  
 --If exists select drivers with Excluded        
 SELECT @EXCLUDED_COUNT = COUNT(*)        
 FROM POL_DRIVER_DETAILS        
 WHERE CUSTOMER_ID = @CUSTOMER_ID AND        
  POLICY_ID = @POLICY_ID AND        
  POLICY_VERSION_ID = @POLICY_VERSION_ID AND        
  IS_ACTIVE = 'Y' AND        
  DRIVER_DRIV_TYPE = '3477'          
         
 --Select drivers with Waiver workloss        
 SELECT @WAIVER_COUNT = COUNT(*)        
 FROM POL_DRIVER_DETAILS        
 WHERE CUSTOMER_ID = @CUSTOMER_ID AND        
  POLICY_ID = @POLICY_ID AND        
  POLICY_VERSION_ID = @POLICY_VERSION_ID AND        
  IS_ACTIVE = 'Y' AND        
  WAIVER_WORK_LOSS_BENEFITS = '1'        
         
 --Driver Exclusion (A-95)**********************************************        
  
 IF ( @EXCLUDED_COUNT = 0 )        
 BEGIN        
  --Remove A-95 from all vehicles if it exists        
  IF EXISTS        
  (        
   SELECT * FROM POL_VEHICLE_ENDORSEMENTS AVC        
   INNER JOIN POL_VEHICLES V ON        
    AVC.VEHICLE_ID = V.VEHICLE_ID        
   WHERE V.CUSTOMER_ID = @CUSTOMER_ID AND         
    V.POLICY_ID = @POLICY_ID AND        
    V.POLICY_VERSION_ID = @POLICY_VERSION_ID AND        
    AVC.ENDORSEMENT_ID = @A95         
  )        
  BEGIN        
   DELETE POL_VEHICLE_ENDORSEMENTS        
   FROM POL_VEHICLE_ENDORSEMENTS AVC        
   INNER JOIN POL_VEHICLES V ON        
    AVC.VEHICLE_ID = V.VEHICLE_ID        
   WHERE V.CUSTOMER_ID = @CUSTOMER_ID AND         
    V.POLICY_ID = @POLICY_ID AND        
    V.POLICY_VERSION_ID = @POLICY_VERSION_ID AND        
    AVC.ENDORSEMENT_ID = @A95         
  END         
        
 END        
 ELSE        
 BEGIN        
  IF NOT EXISTS        
  (        
   SELECT * FROM POL_VEHICLE_ENDORSEMENTS        
   WHERE CUSTOMER_ID = @CUSTOMER_ID AND         
    POLICY_ID = @POLICY_ID AND        
    POLICY_VERSION_ID = @POLICY_VERSION_ID AND        
    ENDORSEMENT_ID = @A95 AND        
    VEHICLE_ID IN         
    (        
     SELECT VEHICLE_ID        
     FROM POL_VEHICLES        
     WHERE CUSTOMER_ID = @CUSTOMER_ID AND         
      POLICY_ID = @POLICY_ID AND         
      POLICY_VERSION_ID = @POLICY_VERSION_ID         
    )         
  )        
  	BEGIN        
       
 	--Add A-95 to all vehicles for this application        
	INSERT INTO POL_VEHICLE_ENDORSEMENTS 
	(
		POLICY_ID,
		POLICY_VERSION_ID,
		CUSTOMER_ID,
		VEHICLE_ID,
		ENDORSEMENT_ID,
		REMARKS,
		VEHICLE_ENDORSEMENT_ID
	)
        
	SELECT        
		@POLICY_ID,        
		@POLICY_VERSION_ID,        
		@CUSTOMER_ID,    
		VEHICLE_ID,         
		@A95,        
		NULL,         
		(        
			SELECT ISNULL(MAX(VEHICLE_ENDORSEMENT_ID),0) + 1 as VEHICLE_ENDORSEMENT_ID       
			FROM POL_VEHICLE_ENDORSEMENTS        
			WHERE CUSTOMER_ID = @CUSTOMER_ID AND         
			POLICY_ID = @POLICY_ID AND         
			POLICY_VERSION_ID = @POLICY_VERSION_ID         
			AND VEHICLE_ID = V.VEHICLE_ID        
		)
		        
		FROM POL_VEHICLES V        
		WHERE CUSTOMER_ID = @CUSTOMER_ID AND         
		POLICY_ID = @POLICY_ID AND        
		POLICY_VERSION_ID = @POLICY_VERSION_ID        
	
	END         
 END     
     
  
 --End of Driver Exclusion******************************************        
        
 --PIP Waiver / Waive Work Loss Benefits (A-94)******************   
IF ( @STATE_ID = 22 )  
BEGIN  
  IF ( @WAIVER_COUNT = 0 )        
  BEGIN        
   --Remove A-94 from all vehicles if it exists        
   IF EXISTS        
   (        
    SELECT * FROM POL_VEHICLE_ENDORSEMENTS AVC        
    INNER JOIN POL_VEHICLES V ON        
     AVC.VEHICLE_ID = V.VEHICLE_ID        
    WHERE V.CUSTOMER_ID = @CUSTOMER_ID AND         
     V.POLICY_ID = @POLICY_ID AND        
     V.POLICY_VERSION_ID = @POLICY_VERSION_ID AND        
     AVC.ENDORSEMENT_ID = @A94         
   )        
   BEGIN        
    DELETE POL_VEHICLE_ENDORSEMENTS        
    FROM POL_VEHICLE_ENDORSEMENTS AVC        
    INNER JOIN POL_VEHICLES V ON        
     AVC.VEHICLE_ID = V.VEHICLE_ID        
    WHERE V.CUSTOMER_ID = @CUSTOMER_ID AND         
     V.POLICY_ID = @POLICY_ID AND        
     V.POLICY_VERSION_ID = @POLICY_VERSION_ID AND        
     AVC.ENDORSEMENT_ID = @A94         
   END         
         
  END        
  ELSE        
  BEGIN        
   IF NOT EXISTS        
   (        
    SELECT * FROM POL_VEHICLE_ENDORSEMENTS        
    WHERE CUSTOMER_ID = @CUSTOMER_ID AND         
     POLICY_ID = @POLICY_ID AND        
     POLICY_VERSION_ID = @POLICY_VERSION_ID AND        
     ENDORSEMENT_ID = @A94 AND        
     VEHICLE_ID IN         
     (        
      SELECT VEHICLE_ID        
      FROM POL_VEHICLES        
      WHERE CUSTOMER_ID = @CUSTOMER_ID AND         
       POLICY_ID = @POLICY_ID AND         
       POLICY_VERSION_ID = @POLICY_VERSION_ID         
     )         
   )        
   BEGIN        
    --Add A-95 to all vehicles for this application        
	INSERT INTO POL_VEHICLE_ENDORSEMENTS  
	(
		POLICY_ID,
		POLICY_VERSION_ID,
		CUSTOMER_ID,
		VEHICLE_ID,
		ENDORSEMENT_ID,
		REMARKS,
		VEHICLE_ENDORSEMENT_ID
	)       
	SELECT       
	@POLICY_ID,        
	@POLICY_VERSION_ID,        
	@CUSTOMER_ID,      
	VEHICLE_ID,         
	@A94,        
	NULL,         
	(        
		SELECT ISNULL(MAX(VEHICLE_ENDORSEMENT_ID),0) + 1         
		FROM POL_VEHICLE_ENDORSEMENTS        
		WHERE CUSTOMER_ID = @CUSTOMER_ID AND         
		POLICY_ID = @POLICY_ID AND         
		POLICY_VERSION_ID = @POLICY_VERSION_ID         
		AND VEHICLE_ID = V.VEHICLE_ID        
	)        
	FROM POL_VEHICLES V        
	WHERE CUSTOMER_ID = @CUSTOMER_ID AND         
	POLICY_ID = @POLICY_ID AND        
	POLICY_VERSION_ID = @POLICY_VERSION_ID        
   END        
        
 END     
END  
     
 --End of PIP Waiver / Waive Work Loss Benefits (A-94)******************        
        
END                    


GO

