IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_CheckCLM_ACTIVITY_RESERVE]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_CheckCLM_ACTIVITY_RESERVE]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                                                                                                                                                                                        
Proc Name       : dbo.Proc_CheckCLM_ACTIVITY_RESERVE                                                                                                                                                                            
Created by      : Sibin Thomas Philip                                                                                                                                                                                      
Date            : 30/05/2006                                                                                                                                                                                        
Purpose         : Checking CLM_ACTIVITY_RESERVE table for claim reserve existence against a claim                                                                                                                                                              
  
    
Created by      : Sibin Thomas Philip                                                                                                                                                                                       
Revison History :                                                                                                                                                                                        
Used In        : Wolverine                                                                                                                                                                                        
               
------------------------------------------------------------                        
Date     Review By          Comments                                                                                                                                                                                        
------   ------------       -------------------------*/                                                                                                                                                                                        
--DROP PROC dbo.Proc_CheckCLM_ACTIVITY_RESERVE                                                                                                                                                            
CREATE   PROC dbo.Proc_CheckCLM_ACTIVITY_RESERVE                                                                                                                                                                    
@CLAIM_ID int,       
@INSUREDVEHICLE_LOCATION_BOAT_ID int,  
@CALLED_FROM varchar(20),                                                          
@RESULT int OUT            
AS                                                                                                                                                                                    
BEGIN   
  
 IF(@CALLED_FROM='AUTO_MOTOR')  
 BEGIN                                                                                                               IF EXISTS(SELECT RESERVE_ID FROM CLM_INSURED_VEHICLE CLV INNER JOIN CLM_ACTIVITY_RESERVE CAR ON       
      CLV.CLAIM_ID=CAR.CLAIM_ID AND CLV.POLICY_VEHICLE_ID = CAR.VEHICLE_ID WHERE CLV.CLAIM_ID=@CLAIM_ID AND    
   INSURED_VEHICLE_ID = @INSUREDVEHICLE_LOCATION_BOAT_ID)                     
   BEGIN      
    SET @RESULT = 1      
   END      
       
   ELSE      
    SET @RESULT = 2  
 END   
  
 ELSE IF(@CALLED_FROM='RENTAL_HOME')  
 BEGIN   
   IF EXISTS(SELECT RESERVE_ID FROM CLM_INSURED_LOCATION CIL INNER JOIN CLM_ACTIVITY_RESERVE CAR ON       
      CIL.CLAIM_ID=CAR.CLAIM_ID AND CIL.INSURED_LOCATION_ID = CAR.VEHICLE_ID WHERE CAR.CLAIM_ID=@CLAIM_ID AND    
   INSURED_LOCATION_ID = @INSUREDVEHICLE_LOCATION_BOAT_ID)                     
   BEGIN      
    SET @RESULT = 1      
   END      
       
   ELSE      
    SET @RESULT = 2  
 END  
  
 ELSE IF(@CALLED_FROM='WATERCRAFT')   
 BEGIN  
   IF EXISTS(SELECT RESERVE_ID FROM CLM_INSURED_BOAT CIB INNER JOIN CLM_ACTIVITY_RESERVE CAR ON       
      CIB.CLAIM_ID=CAR.CLAIM_ID AND CIB.BOAT_ID = CAR.VEHICLE_ID WHERE CIB.CLAIM_ID=@CLAIM_ID AND    
   BOAT_ID = @INSUREDVEHICLE_LOCATION_BOAT_ID)                     
   BEGIN      
    SET @RESULT = 1      
   END      
       
   ELSE      
    SET @RESULT = 2  
 END  
END  
GO

