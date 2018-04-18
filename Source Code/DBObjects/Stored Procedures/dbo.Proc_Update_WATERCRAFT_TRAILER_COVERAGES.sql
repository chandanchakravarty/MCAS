IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_Update_WATERCRAFT_TRAILER_COVERAGES]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_Update_WATERCRAFT_TRAILER_COVERAGES]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


--drop proc Proc_Update_WATERCRAFT_TRAILER_COVERAGES            
            
/*----------------------------------------------------------                                            
Proc Name   : dbo.Proc_Update_WATERCRAFT_COVERAGES                                           
Created by  : Praveen K                                            
Date        : 20 feb 2006                                         
Purpose     :  Deletes/Inserts  relevant coverages                         
  when a watercraft Trailer is updated                                
Revison History  :                                                  
------------------------------------------------------------                                                        
Date     Review By          Comments                                                      
-----------------------------------------------------------*/                                      
CREATE   PROCEDURE Proc_Update_WATERCRAFT_TRAILER_COVERAGES                                    
(                                     
 @CUSTOMER_ID int,                                    
 @APP_ID int,                                    
 @APP_VERSION_ID smallint,                                     
 @TRAILER_ID smallint                                  
                                     
)                                    
                                    
As                                    
                                     
DECLARE @TYPE  Char(10)                                  
DECLARE @AGE Int                                  
DECLARE @INSURED_VALUE Decimal(10,2)                        
DECLARE @DATE_PURCHASED DateTime                        
DECLARE @LENGTH NVarChar(10)                         
DECLARE @LENGTHINT Int                      
DECLARE @YEAR Int                        
DECLARE @BOAT_TOWING DECIMAL(10,2)                      
DECLARE @COV_ID Int              
                              
SELECT   @TYPE = TRAILER_TYPE,                                  
 @INSURED_VALUE = INSURED_VALUE ,                              
 --@DATE_PURCHASED = DATE_PURCHASED ,                        
 --@LENGTH = LENGTH ,                      
 @YEAR = YEAR                           
FROM APP_WATERCRAFT_TRAILER_INFO                               
WHERE TRAILER_ID = @TRAILER_ID AND                                  
 CUSTOMER_ID = @CUSTOMER_ID AND                                  
 APP_ID = @APP_ID AND                                  
 APP_VERSION_ID = @APP_VERSION_ID                                  
                            
--Get Age of watercraft                        
SET @AGE = DATEPART(yy,GetDate()) - @YEAR                      
        
print(@AGE)        
--SET @AGE = DATEDIFF(yy,@DATE_PURCHASED, GetDate())                        
                        
/*IF ( @LENGTH <> '' )                        
BEGIN                        
 SET @LENGTHINT = CONVERT(Int,@LENGTH)                        
END*/                        
                        
DECLARE @STATE_ID Int                                    
 DECLARE @LOB_ID int                                    
                                       
 SELECT @STATE_ID = STATE_ID,                                    
  @LOB_ID = APP_LOB                                    
 FROM APP_LIST                                    
 WHERE CUSTOMER_ID = @CUSTOMER_ID AND                                    
  APP_ID = @APP_ID AND                                    
  APP_VERSION_ID =  @APP_VERSION_ID                                  
                            
            
--Mandatory for all-----            
--Mandatory coverage: Increase in "Unattached Equipment" And Personal Effects Coverage EBIUE                       
                  
  EXEC @COV_ID = Proc_Get_WATERCRAFT_COVERAGE_ID @CUSTOMER_ID,                        
           @APP_ID,                        
       @APP_VERSION_ID,                    
       'EBIUE'                        
                     
  IF ( @COV_ID = 0 )                    
 BEGIN                    
  RAISERROR ('COV_ID not found for  Increase in "Unattached Equipment" And Personal Effects Coverage',                    
       16, 1)                    
                     
 END                    
                            
  EXEC Proc_Save_WATERCRAFT_COVERAGES                         
    @CUSTOMER_ID,--@CUSTOMER_ID     int,                 
     @APP_ID,--@APP_ID     int,                                
    @APP_VERSION_ID,-- @APP_VERSION_ID     smallint,                                
     @TRAILER_ID,--@VEHICLE_ID smallint,                                
     -1,--@COVERAGE_ID int,                                
     @COV_ID,--@COVERAGE_CODE_ID int,                                
     1500,--@LIMIT_1 Decimal(18,2),                                      
     NULL,--@LIMIT_2 Decimal(18,2),                                    
     NULL,--@LIMIT1_AMOUNT_TEXT NVarChar(100),                                      
     NULL,--@LIMIT2_AMOUNT_TEXT NVarChar(100),                                      
     NULL,--@DEDUCTIBLE1_AMOUNT_TEXT NVarChar(100),                      
     NULL,--@DEDUCTIBLE2_AMOUNT_TEXT NVarChar(100),                          
     NULL,--@LIMIT_1_TYPE NVarChar(5),                                      
     NULL,--@LIMIT_2_TYPE NVarChar(5),                                      
     100,--@DEDUCTIBLE_1 DECIMAL(18,2),                                      
     NULL,--@DEDUCTIBLE_2 DECIMAL(18,2),                                      
     NULL,--@DEDUCTIBLE_1_TYPE NVarChar(5),                                      
     NULL,--@DEDUCTIBLE_2_TYPE NVarChar(5),                                      
     NULL,--@WRITTEN_PREMIUM DECIMAL(18,2),                                      
     NULL--@FULL_TERM_PREMIUM DECIMAL(18,2)                       
                         
 IF @@ERROR <> 0                            
  BEGIN                             
     RETURN                    
     END          
          
--Mandatory coverage: Boat Towing and Emergency Service Coverage                   
--Boat Towing and Emergency Service Coverage = 5% of Insuring value****                      
IF ( @INSURED_VALUE IS NOT NULL )               
BEGIN                      
 SET @BOAT_TOWING = @INSURED_VALUE *  0.05                      
END          
ELSE          
BEGIN                      
 SET @BOAT_TOWING = 0          
END          
          
           
                      
 EXEC @COV_ID = Proc_Get_WATERCRAFT_COVERAGE_ID @CUSTOMER_ID,                        
          @APP_ID,                        
      @APP_VERSION_ID,                        
      'BTESC'                        
                      
 IF ( @COV_ID = 0 )                    
BEGIN                    
 RAISERROR ('COV_ID not found for Boat Towing and Emergency Service Coverage = 5% of Insuring value   ',                    
      16, 1)                    
                    
END                    
                          
 /*                      
 print (@INSURING_VALUE)                      
 print (@BOAT_TOWING)                      
 */                      
                      
 EXEC Proc_Save_WATERCRAFT_COVERAGES                         
   @CUSTOMER_ID,--@CUSTOMER_ID     int,                                
    @APP_ID,--@APP_ID     int,                                
   @APP_VERSION_ID,-- @APP_VERSION_ID     smallint,                                
    @TRAILER_ID,--@VEHICLE_ID smallint,                                
    -1,--@COVERAGE_ID int,                                
    @COV_ID,--@COVERAGE_CODE_ID int,                                
    @BOAT_TOWING,--@LIMIT_1 Decimal(18,2),                                      
    NULL,--@LIMIT_2 Decimal(18,2),                                    
    NULL,--@LIMIT1_AMOUNT_TEXT NVarChar(100),                                      
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
    NULL--@FULL_TERM_PREMIUM DECIMAL(18,2)                      
       
IF @@ERROR <> 0                            
 BEGIN                    
                             
   RETURN                    
    END                            
                          
            
-----------------------            
        
                    
             
                             
                      
                      
RETURN 1                                    
                                    
                                    
                                    
                                  
                    
                              
                            
                          
                        
                      
                    
                  
                
              
            
          
        
      
    
  



GO

