IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_Update_VEHICLE_ENDORSEMENTS_DRIVER]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_Update_VEHICLE_ENDORSEMENTS_DRIVER]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


--drop proc Proc_Update_VEHICLE_ENDORSEMENTS_DRIVER

/*----------------------------------------------------------                        
Proc Name   : dbo.Proc_Update_VEHICLE_ENDORSEMENTS_DRIVER                       
Created by  : Pradeep                        
Date        : 11/25/2005                      
Purpose     :  Deletes relevant coverages when a vehicle is updated            
Revison History  :                              
------------------------------------------------------------                                    
Date     Review By          Comments                                  
-----------------------------------------------------------*/                  
CREATE   PROCEDURE Proc_Update_VEHICLE_ENDORSEMENTS_DRIVER                
(                 
 @CUSTOMER_ID int,                
 @APP_ID int,                
 @APP_VERSION_ID smallint,                 
 @DRIVER_ID smallint              
)                
As    
  
BEGIN  
            
DECLARE @WAIVE NChar(1)  
DECLARE @VEHICLE_ID Int  
  
SELECT  @WAIVE =  Waiver_Work_loss_benefits,@VEHICLE_ID = VEHICLE_ID   
FROM APP_DRIVER_DETAILS  
WHERE CUSTOMER_ID = @CUSTOMER_ID AND  
 APP_ID = @APP_ID AND  
 APP_VERSION_ID = @APP_VERSION_ID AND  
 DRIVER_ID = @DRIVER_ID  
  
DECLARE @STATE_ID Int                
 DECLARE @LOB_ID int                
--DECLARE @VEHICLE_ID Int  
DECLARE @PIP Int  
                   
 SELECT @STATE_ID = STATE_ID,                
  @LOB_ID = APP_LOB                
 FROM APP_LIST                
 WHERE CUSTOMER_ID = @CUSTOMER_ID AND                
  APP_ID = @APP_ID AND                
  APP_VERSION_ID =  @APP_VERSION_ID           
  
IF ( @STATE_ID = 22 )        
 --MICHIGAN        
 BEGIN        
     
  SET  @PIP =  43    
       
  --If any one of the drives has Waiver work loss benefits as selected---------  
  IF  ( @PIP IS NOT NULL )  
  BEGIN  
    
   IF (@WAIVE IS NOT NULL AND ISNULL(@VEHICLE_ID,0) <> 0)  
   BEGIN  
      
     EXEC Proc_Insert_APP_VEHICLE_ENDORSEMENT_BY_ID @CUSTOMER_ID,        
           @APP_ID,        
          @APP_VERSION_ID,        
          @PIP,        
          @VEHICLE_ID        
      
    IF @@ERROR <> 0        
     BEGIN        
      RETURN        
     END        
   END  
      
     
  END  
   
   
 END        
END  



GO

