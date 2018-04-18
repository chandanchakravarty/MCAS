IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_DELETEINSUREDVEHICLE]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_DELETEINSUREDVEHICLE]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                                                                                                                                                                                          
Proc Name       : dbo.PROC_DELETEINSUREDVEHICLE                                                                                                                                                                              
Created by      : Sibin Thomas Philip                                                                                                                                                                                        
Date            : 30/05/2009                                                                                                                                                                                          
Purpose         : DELETING CLM_ACTIVITY_RESERVE table for chosen insured vehicle id in the selected claim                                                                                                                                                      
  
    
      
              
Created by      : Sibin Thomas Philip                                                                                                                                                                                         
Revison History :                                                                                                                                                                                          
Used In        : Wolverine                                                                                                                                                                                          
                 
------------------------------------------------------------                          
Date     Review By          Comments                                                                                                                                                                                          
------   ------------       -------------------------*/                                                                                                                                                                                          
--DROP PROC dbo.PROC_DELETEINSUREDVEHICLE                                                                                                                                                              
CREATE   PROC dbo.PROC_DELETEINSUREDVEHICLE                                                                                                                                                                      
@CLAIM_ID int,                                                             
@INSUREDVEHICLE_LOCATION_BOAT_ID int,    
@CALLED_FROM varchar(20)              
AS                                                                                                                            
DECLARE @RESULT INT
DECLARE @INSURED_VEHICLEID INT                                                          
BEGIN     
    
 IF(@CALLED_FROM='AUTO_MOTOR')    
 BEGIN                                                                                                              
  SELECT @INSURED_VEHICLEID= INSURED_VEHICLE_ID FROM CLM_INSURED_VEHICLE 
  WHERE CLAIM_ID=@CLAIM_ID AND INSURED_VEHICLE_ID=@INSUREDVEHICLE_LOCATION_BOAT_ID

  IF NOT EXISTS(SELECT VEHICLE_ID FROM CLM_DRIVER_INFORMATION 
				WHERE CLAIM_ID=@CLAIM_ID AND VEHICLE_ID=@INSURED_VEHICLEID)
  BEGIN
   IF EXISTS(SELECT 1 FROM CLM_INSURED_VEHICLE WHERE CLAIM_ID=@CLAIM_ID AND     
   INSURED_VEHICLE_ID=@INSUREDVEHICLE_LOCATION_BOAT_ID)          
   BEGIN          
    DELETE CLM_INSURED_VEHICLE WHERE CLAIM_ID=@CLAIM_ID AND INSURED_VEHICLE_ID=@INSUREDVEHICLE_LOCATION_BOAT_ID       SET @RESULT = 1        
   END        
   ELSE        
    SET @RESULT = 0 
  END
  ELSE
  BEGIN
    SET @RESULT = -1 
  END      
         
  RETURN @RESULT      
 END     
     
 IF(@CALLED_FROM='RENTAL_HOME')    
 BEGIN                                                                                                              IF EXISTS(SELECT 1 FROM CLM_INSURED_LOCATION WHERE CLAIM_ID=@CLAIM_ID AND     
   INSURED_LOCATION_ID=@INSUREDVEHICLE_LOCATION_BOAT_ID)     
  BEGIN        
   DELETE CLM_INSURED_LOCATION WHERE CLAIM_ID=@CLAIM_ID AND INSURED_LOCATION_ID=@INSUREDVEHICLE_LOCATION_BOAT_ID     SET @RESULT = 1        
  END        
  ELSE        
   SET @RESULT = 0        
         
  RETURN @RESULT      
 END    
    
 IF(@CALLED_FROM='WATERCRAFT')    
 BEGIN                                                                                                              IF EXISTS(SELECT 1 FROM CLM_INSURED_BOAT WHERE CLAIM_ID=@CLAIM_ID AND BOAT_ID =@INSUREDVEHICLE_LOCATION_BOAT_ID)  BEGIN          
   DELETE CLM_INSURED_BOAT WHERE CLAIM_ID=@CLAIM_ID AND BOAT_ID = @INSUREDVEHICLE_LOCATION_BOAT_ID         
   SET @RESULT = 1        
  END        
  ELSE        
   SET @RESULT = 0        
         
  RETURN @RESULT      
 END    
     
END
GO

