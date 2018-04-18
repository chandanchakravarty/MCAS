IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_Update_MOTORCYCLE_COVERAGES]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_Update_MOTORCYCLE_COVERAGES]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


--drop proc Proc_Update_MOTORCYCLE_COVERAGES    
    
/*----------------------------------------------------------                            
Proc Name   : dbo.Proc_Update_MOTORCYCLE_COVERAGES                           
Created by  : Pradeep                            
Date        : 11/25/2005                          
Purpose     :  Deletes relevant coverages when a vehicle is updated                
Revison History  :                                  
------------------------------------------------------------                                        
Date     Review By          Comments                                      
-----------------------------------------------------------*/                      
CREATE   PROCEDURE Proc_Update_MOTORCYCLE_COVERAGES                    
(                     
 @CUSTOMER_ID int,                    
 @APP_ID int,                    
 @APP_VERSION_ID smallint,                     
 @VEHICLE_ID smallint                         
)                    
                    
As                  
        
        
BEGIN        
      
 DECLARE @COV_ID Int        
 DECLARE @STATE_ID Int      
 DECLARE @LOB_ID Int      
      
SELECT  @STATE_ID = STATE_ID ,      
 @LOB_ID = APP_LOB      
FROM APP_LIST      
WHERE CUSTOMER_ID = @CUSTOMER_ID AND      
 APP_ID = @APP_ID AND      
 APP_VERSIOn_ID = @APP_VERSION_ID       
       
IF ( @STATE_ID = 22 )      
BEGIN      
      
 
IF NOT EXISTS
(             
  SELECT * FROM  APP_VEHICLE_COVERAGES  
  
   WHERE  CUSTOMER_ID = @CUSTOMER_ID AND  
    APP_ID = @APP_ID AND  
    APP_VERSION_ID = @APP_VERSION_ID AND  
    VEHICLE_ID = @VEHICLE_ID AND  
    COVERAGE_CODE_ID = 769  
 )       
  EXEC Proc_SAVE_VEHICLE_COVERAGES        
     @CUSTOMER_ID,--@CUSTOMER_ID     int,                            
     @APP_ID,--@APP_ID     int,                            
     @APP_VERSION_ID,--@APP_VERSION_ID     smallint,                            
     @VEHICLE_ID,--@VEHICLE_ID smallint,                            
     -1,--@COVERAGE_ID int,                            
     769,--@COVERAGE_CODE_ID int,                            
     1000,--@LIMIT_1 Decimal(18,2),                            
     NULL,--@LIMIT_2 Decimal(18,2),                          
     '$1000 Medical',--@LIMIT1_AMOUNT_TEXT NVarChar(100),                            
     NULL,--@LIMIT2_AMOUNT_TEXT NVarChar(100),                            
     NULL,--@DEDUCTIBLE1_AMOUNT_TEXT NVarChar(100),                            
     NULL,--@DEDUCTIBLE2_AMOUNT_TEXT NVarChar(100),                            
     NULL,--@LIMIT_1_TYPE NVarChar(5),                            
     NULL,--@LIMIT_2_TYPE NVarChar(5),                            
     NULL,--@DEDUCTIBLE_1 DECIMAL(18,2),                            
     NULL,--@DEDUCTIBLE_2 DECIMAL(18,2),                            
     NULL,--@DEDUCTIBLE_1_TYPE NVarChar(5),                            
     NULL,--@DEDUCTIBLE_2_TYPE NVarChar(5),                            
     NULL,--@WRITTEN_PREMIUM DECIMAL(18,2),                            
     NULL,--@FULL_TERM_PREMIUM DECIMAL(18,2) ,             
     'N',--@SIGNATURE_OBTAINED NChar(1)     
     843,    
     NULL                     
        
             
 END               
   
 --Update coverages based on Motorcycle CC*************************************  
DECLARE @CC Int   
DECLARE @OTC_ID Int  
DECLARE @COLL_ID Int   
DECLARE @COLL_AMOUNT Decimal(18,0)  
DECLARE @OTC_AMOUNT Decimal(18,0)  
DECLARE @COLL_MIN_AMOUNT Decimal(18,0)  
DECLARE @OTC_MIN_AMOUNT Decimal(18,0)  
DECLARE @COLL_DED_ID Int   
DECLARE @OTC_DED_ID Int   
  
--Get CC  
 SELECT @CC = VEHICLE_CC  
 FROM APP_VEHICLES  
 WHERE CUSTOMER_ID = @CUSTOMER_ID AND  
  APP_ID = @APP_ID AND  
  APP_VERSION_ID = @APP_VERSION_ID AND  
  VEHICLE_ID = @VEHICLE_ID  
   
 --Collision and Other than collision  
 IF (  @STATE_ID = 14 )  
 BEGIN  
  SET @OTC_ID = 201  
  SET @COLL_ID = 200  
 END   
   
 IF (  @STATE_ID = 22 )  
 BEGIN  
  SET @OTC_ID = 217  
  SET @COLL_ID = 216  
 END   
   
 --Get existing deductibles for Collsiion and Other than collsion  
 SELECT  @COLL_AMOUNT = R.LIMIT_DEDUC_AMOUNT  
 FROM APP_VEHICLE_COVERAGES AVC  
 INNER JOIN MNT_COVERAGE_RANGES R ON  
  AVC.DEDUC_ID = R.LIMIT_DEDUC_ID   
 WHERE  AVC.CUSTOMER_ID = @CUSTOMER_ID AND  
  AVC.APP_ID = @APP_ID AND  
  AVC.APP_VERSION_ID = @APP_VERSION_ID AND  
  AVC.VEHICLE_ID = @VEHICLE_ID AND  
  AVC.COVERAGE_CODE_ID = @COLL_ID  
   
 --Other than collision  
 SELECT  @OTC_AMOUNT = R.LIMIT_DEDUC_AMOUNT  
 FROM APP_VEHICLE_COVERAGES AVC  
 INNER JOIN MNT_COVERAGE_RANGES R ON  
  AVC.DEDUC_ID = R.LIMIT_DEDUC_ID   
 WHERE  AVC.CUSTOMER_ID = @CUSTOMER_ID AND  
  AVC.APP_ID = @APP_ID AND  
  AVC.APP_VERSION_ID = @APP_VERSION_ID AND  
  AVC.VEHICLE_ID = @VEHICLE_ID AND  
  AVC.COVERAGE_CODE_ID = @OTC_ID  
 -----------  
   
 --Get min values for Collision and other than collision based on CC  
 SELECT  @COLL_MIN_AMOUNT = LIMIT_DEDUC_AMOUNT,  
  @COLL_DED_ID = LIMIT_DEDUC_ID  
 FROM VIW_MOTOR_MIN_DED  
 WHERE COV_ID = @COLL_ID AND  
  @CC BETWEEN CC_RANGE1 AND CC_RANGE2  
   
 SELECT  @OTC_MIN_AMOUNT = LIMIT_DEDUC_AMOUNT,  
  @OTC_DED_ID = LIMIT_DEDUC_ID  
 FROM VIW_MOTOR_MIN_DED  
 WHERE COV_ID = @OTC_ID AND  
  @CC BETWEEN CC_RANGE1 AND CC_RANGE2  
 ------------------  
   
 --If existing amount, is less than min amount, then update  
 IF ( @COLL_AMOUNT IS NOT NULL )  
 BEGIN  
  IF ( @COLL_AMOUNT < @COLL_MIN_AMOUNT )  
  BEGIN  
   UPDATE APP_VEHICLE_COVERAGES  
   SET DEDUCTIBLE_1 = @COLL_MIN_AMOUNT,  
    DEDUC_ID = @COLL_DED_ID  
   WHERE  CUSTOMER_ID = @CUSTOMER_ID AND  
    APP_ID = @APP_ID AND  
    APP_VERSION_ID = @APP_VERSION_ID AND  
    VEHICLE_ID = @VEHICLE_ID AND  
    COVERAGE_CODE_ID = @COLL_ID  
   
  END  
 END  
   
   
 IF ( @OTC_AMOUNT IS NOT NULL )  
 BEGIN  
  IF ( @OTC_AMOUNT < @OTC_MIN_AMOUNT )  
  BEGIN  
   UPDATE APP_VEHICLE_COVERAGES  
   SET DEDUCTIBLE_1 = @OTC_MIN_AMOUNT,  
    DEDUC_ID = @OTC_DED_ID  
   WHERE  CUSTOMER_ID = @CUSTOMER_ID AND  
    APP_ID = @APP_ID AND  
    APP_VERSION_ID = @APP_VERSION_ID AND  
    VEHICLE_ID = @VEHICLE_ID AND  
    COVERAGE_CODE_ID = @OTC_ID  
   
  END  
 END  
  
--******************************************************************************          
END          
      
    
  



GO

