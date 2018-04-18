IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_ACTIVATEDEACTIVATEINSUREDVEHICLE]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_ACTIVATEDEACTIVATEINSUREDVEHICLE]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                                                                                                                                                                                                  
Proc Name       : dbo.PROC_ACTIVATEDEACTIVATEINSUREDVEHICLE                                                                                                                                                                                      
Created by      : Sibin Thomas Philip                                                                                                                                                                                                
Date            : 30/05/2009                                                                                                                                                                                                  
Purpose         : Activating or Deactivating for chosen insured vehicle id in the selected claim                                                                                                                                                               
  
   
              
                      
Created by      : Sibin Thomas Philip                                                                                                                                                                                                 
Revison History :                                                                                                                                                                                                  
Used In        : Wolverine                                                                                                                                                                                                  
                         
------------------------------------------------------------                                  
Date     Review By          Comments                                                                                                                                                                                                  
------   ------------       -------------------------*/                                                                                                                                                                                                  
--DROP PROC dbo.PROC_ACTIVATEDEACTIVATEINSUREDVEHICLE                                                                                                                                                                      
CREATE   PROC dbo.PROC_ACTIVATEDEACTIVATEINSUREDVEHICLE                                                                                                                                                                              
@CLAIM_ID int,                                                                     
@INSUREDVEHICLE_LOCATION_BOAT_ID int,            
@ACTIVATEDEACTIVATE varchar(1),          
@CALLED_FROM varchar(20)            
                     
AS                                                                                                                      
                                                                            
DECLARE @RESULT INT      
DECLARE @INSURED_VEHICLEID INT       
      
BEGIN                                                                                                                    
 SELECT @INSURED_VEHICLEID = INSURED_VEHICLE_ID FROM CLM_INSURED_VEHICLE       
 WHERE CLAIM_ID=@CLAIM_ID AND INSURED_VEHICLE_ID=@INSUREDVEHICLE_LOCATION_BOAT_ID      
         
 IF(@CALLED_FROM='AUTO_MOTOR')          
  BEGIN          
    IF EXISTS(SELECT 1 FROM CLM_INSURED_VEHICLE       
    WHERE CLAIM_ID=@CLAIM_ID AND INSURED_VEHICLE_ID= @INSUREDVEHICLE_LOCATION_BOAT_ID)                  
    BEGIN            
    IF(@ACTIVATEDEACTIVATE = 'Y')            
    BEGIN                  
    UPDATE CLM_INSURED_VEHICLE SET IS_ACTIVE = 'Y' WHERE CLAIM_ID=@CLAIM_ID AND           
    INSURED_VEHICLE_ID=@INSUREDVEHICLE_LOCATION_BOAT_ID               
    END            
    ELSE IF(@ACTIVATEDEACTIVATE = 'N')            
    BEGIN         
     IF NOT EXISTS(SELECT VEHICLE_ID FROM CLM_DRIVER_INFORMATION       
     WHERE CLAIM_ID=@CLAIM_ID AND VEHICLE_ID=@INSURED_VEHICLEID AND IS_ACTIVE='Y')      
     BEGIN           
      UPDATE CLM_INSURED_VEHICLE SET IS_ACTIVE = 'N' WHERE CLAIM_ID=@CLAIM_ID AND          
      INSURED_VEHICLE_ID=@INSUREDVEHICLE_LOCATION_BOAT_ID      
         
      SET @RESULT = 1    
    END      
    ELSE 
        SET @RESULT = -1            
    END          
   END          
   RETURN @RESULT       
  END          
            
  ELSE IF(@CALLED_FROM='RENTAL_HOME')          
  BEGIN          
    IF EXISTS(SELECT * FROM CLM_INSURED_LOCATION       
    WHERE CLAIM_ID=@CLAIM_ID AND INSURED_LOCATION_ID= @INSUREDVEHICLE_LOCATION_BOAT_ID)                  
    BEGIN            
    IF(@ACTIVATEDEACTIVATE = 'Y')            
    BEGIN                  
    UPDATE CLM_INSURED_LOCATION SET IS_ACTIVE = 'Y' WHERE CLAIM_ID=@CLAIM_ID AND           
    INSURED_LOCATION_ID=@INSUREDVEHICLE_LOCATION_BOAT_ID               
    END            
    ELSE IF(@ACTIVATEDEACTIVATE = 'N')            
    BEGIN          
    UPDATE CLM_INSURED_LOCATION SET IS_ACTIVE = 'N' WHERE CLAIM_ID=@CLAIM_ID AND          
    INSURED_LOCATION_ID=@INSUREDVEHICLE_LOCATION_BOAT_ID            
    END             
    END          
  END             
            
  ELSE IF(@CALLED_FROM='WATERCRAFT')          
  BEGIN          
    IF EXISTS(SELECT 1 FROM CLM_INSURED_BOAT       
    WHERE CLAIM_ID=@CLAIM_ID AND BOAT_ID= @INSUREDVEHICLE_LOCATION_BOAT_ID)                  
    BEGIN            
    IF(@ACTIVATEDEACTIVATE = 'Y')            
    BEGIN                  
   UPDATE CLM_INSURED_BOAT SET IS_ACTIVE = 'Y' WHERE CLAIM_ID=@CLAIM_ID AND           
    BOAT_ID=@INSUREDVEHICLE_LOCATION_BOAT_ID               
    END            
    ELSE IF(@ACTIVATEDEACTIVATE = 'N')            
    BEGIN           
   UPDATE CLM_INSURED_BOAT SET IS_ACTIVE = 'N' WHERE CLAIM_ID=@CLAIM_ID AND          
    BOAT_ID=@INSUREDVEHICLE_LOCATION_BOAT_ID            
    END             
    END          
  END             
END 
GO

