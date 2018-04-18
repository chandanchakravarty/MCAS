IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UPDATE_DRIVER_ENDORSEMENTS_VEHICLE]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UPDATE_DRIVER_ENDORSEMENTS_VEHICLE]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


--drop proc Proc_UPDATE_DRIVER_ENDORSEMENTS_VEHICLE  
  
/*  
Proc Name       : dbo.Proc_UPDATE_DRIVER_ENDORSEMENTS_VEHICLE                    
Created By     :    Pradeep                      
Created Date   : 10-02-2006                        
Purpose    : Updates the driver dependent endorsements for a vehicle      
                                                     
==========================================================================                                            
Date     Review By          Comments                                                      
========================================================================== */              
CREATE PROC Proc_UPDATE_DRIVER_ENDORSEMENTS_VEHICLE                 
(                    
 @CUSTOMER_ID int,                    
 @APP_ID  int,                    
 @APP_VERSION_ID int,    
 @VEHICLE_ID Int      
      
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
       
 SET @A95 = 39      
 SET @A94 = 43      
       
 SELECT @STATE_ID = STATE_ID      
 FROM APP_LIST      
 WHERE CUSTOMER_ID = @CUSTOMER_ID AND      
  APP_ID = @APP_ID AND      
  APP_VERSION_ID = @APP_VERSION_ID      
       
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
 FROM APP_DRIVER_DETAILS      
 WHERE CUSTOMER_ID = @CUSTOMER_ID AND      
  APP_ID = @APP_ID AND      
  APP_VERSION_ID = @APP_VERSION_ID AND      
  IS_ACTIVE = 'Y' AND      
  DRIVER_DRIV_TYPE = '3477'        
       
 --Select drivers with Waiver workloss      
 SELECT @WAIVER_COUNT = COUNT(*)      
 FROM APP_DRIVER_DETAILS      
 WHERE CUSTOMER_ID = @CUSTOMER_ID AND      
  APP_ID = @APP_ID AND      
  APP_VERSION_ID = @APP_VERSION_ID AND      
  IS_ACTIVE = 'Y' AND      
  WAIVER_WORK_LOSS_BENEFITS = '1'      
       
 --Driver Exclusion (A-95)**********************************************      
 IF ( @EXCLUDED_COUNT = 0 )      
 BEGIN      
  --Remove A-95 from this vehicles if it exists      
  IF EXISTS      
  (      
   SELECT * FROM APP_VEHICLE_ENDORSEMENTS AVC       
   WHERE AVC.CUSTOMER_ID = @CUSTOMER_ID AND       
    AVC.APP_ID = @APP_ID AND      
    AVC.APP_VERSION_ID = @APP_VERSION_ID AND      
    AVC.ENDORSEMENT_ID = @A95 AND    
    AVC.VEHICLE_ID = @VEHICLE_ID      
  )      
  BEGIN      
   DELETE FROM APP_VEHICLE_ENDORSEMENTS      
 WHERE       
    APP_ID = @APP_ID AND      
    APP_VERSION_ID = @APP_VERSION_ID AND      
    ENDORSEMENT_ID = @A95   AND    
    VEHICLE_ID = @VEHICLE_ID    
  END       
      
 END      
 ELSE      
 BEGIN      
  IF NOT EXISTS      
  (      
   SELECT * FROM APP_VEHICLE_ENDORSEMENTS      
   WHERE CUSTOMER_ID = @CUSTOMER_ID AND       
    APP_ID = @APP_ID AND      
    APP_VERSION_ID = @APP_VERSION_ID AND      
    ENDORSEMENT_ID = @A95 AND      
    VEHICLE_ID = @VEHICLE_ID       
    
  )      
  BEGIN      
   --Add A-95 to all vehicles for this application      
   INSERT INTO APP_VEHICLE_ENDORSEMENTS 
	(
		CUSTOMER_ID,
		APP_ID,
		APP_VERSION_ID,
		VEHICLE_ID,
		ENDORSEMENT_ID,
		REMARKS,
		VEHICLE_ENDORSEMENT_ID
	)      
   SELECT @CUSTOMER_ID,      
    @APP_ID,      
    @APP_VERSION_ID,      
    @VEHICLE_ID,       
    @A95,      
    NULL,       
   (      
    SELECT ISNULL(MAX(VEHICLE_ENDORSEMENT_ID),0) + 1      
    FROM APP_VEHICLE_ENDORSEMENTS      
    WHERE CUSTOMER_ID = @CUSTOMER_ID AND       
     APP_ID = @APP_ID AND       
     APP_VERSION_ID = @APP_VERSION_ID       
     AND VEHICLE_ID = @VEHICLE_ID      
   )      
      
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
    SELECT * FROM APP_VEHICLE_ENDORSEMENTS AVC      
    WHERE AVC.CUSTOMER_ID = @CUSTOMER_ID AND       
     AVC.APP_ID = @APP_ID AND      
     AVC.APP_VERSION_ID = @APP_VERSION_ID AND      
     AVC.ENDORSEMENT_ID = @A94   AND    
     AVC.VEHICLE_ID = @VEHICLE_ID     
   )      
  BEGIN      
    DELETE     
    FROM APP_VEHICLE_ENDORSEMENTS       
    WHERE CUSTOMER_ID = @CUSTOMER_ID AND       
     APP_ID = @APP_ID AND      
     APP_VERSION_ID = @APP_VERSION_ID AND      
     ENDORSEMENT_ID = @A94   AND    
     VEHICLE_ID = @VEHICLE_ID     
   END       
       
  END      
  ELSE      
  BEGIN      
   IF NOT EXISTS      
   (      
    SELECT * FROM APP_VEHICLE_ENDORSEMENTS      
    WHERE CUSTOMER_ID = @CUSTOMER_ID AND       
     APP_ID = @APP_ID AND      
     APP_VERSION_ID = @APP_VERSION_ID AND      
     ENDORSEMENT_ID = @A94 AND      
     VEHICLE_ID = @VEHICLE_ID    
     
   )      
   BEGIN      
    --Add A-95 to all vehicles for this application      
    INSERT INTO APP_VEHICLE_ENDORSEMENTS 
	(
		CUSTOMER_ID,
		APP_ID,
		APP_VERSION_ID,
		VEHICLE_ID,
		ENDORSEMENT_ID,
		REMARKS,
		VEHICLE_ENDORSEMENT_ID
	)      
      
    SELECT @CUSTOMER_ID,      
     @APP_ID,      
     @APP_VERSION_ID,      
     @VEHICLE_ID,       
     @A94,      
     NULL,       
    (      
     SELECT ISNULL(MAX(VEHICLE_ENDORSEMENT_ID),0) + 1      
     FROM APP_VEHICLE_ENDORSEMENTS      
     WHERE CUSTOMER_ID = @CUSTOMER_ID AND       
      APP_ID = @APP_ID AND       
      APP_VERSION_ID = @APP_VERSION_ID       
      AND VEHICLE_ID = @VEHICLE_ID    
    )      
        
   END      
       
  END     
END   
 --End of PIP Waiver / Waive Work Loss Benefits (A-94)******************      
      
END         


GO

