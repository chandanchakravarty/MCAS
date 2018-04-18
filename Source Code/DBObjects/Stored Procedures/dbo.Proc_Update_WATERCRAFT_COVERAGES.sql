IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_Update_WATERCRAFT_COVERAGES]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_Update_WATERCRAFT_COVERAGES]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


--drop proc Proc_Update_WATERCRAFT_COVERAGES                                            
                                            
/*----------------------------------------------------------                                                                            
Proc Name   : dbo.Proc_Update_WATERCRAFT_COVERAGES                                                                           
Created by  : Pradeep                                                                            
Date        : 12/12/2005                                                                          
Purpose     :  Deletes/Inserts  relevant coverages                                                         
  when a watercraft is updated                                                                
Revison History  :                                                                                  
------------------------------------------------------------                                                                                        
Date     Review By          Comments                                                                                      
-----------------------------------------------------------*/                       
--DROP PROC Proc_Update_WATERCRAFT_COVERAGES                                                                   
CREATE   PROCEDURE Proc_Update_WATERCRAFT_COVERAGES                                                                    
(                                                                     
 @CUSTOMER_ID int,                                                                    
 @APP_ID int,                                                                    
 @APP_VERSION_ID smallint,                                                                     
 @BOAT_ID smallint                                                                  
                                                                     
)                                                                    
                                                                    
As                                                                    
                                                                     
        
                                                 
DECLARE @COV_ID Int                                              
            
                           
                                                            
                                                
                                                 
                                                                  
     
          
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
  IF NOT EXISTS                            
   (                            
		SELECT * FROM APP_WATERCRAFT_COVERAGE_INFO  
		WHERE  CUSTOMER_ID = @CUSTOMER_ID and                                             
		APP_ID=@APP_ID and                                             
		APP_VERSION_ID = @APP_VERSION_ID AND                                            
		BOAT_ID = @BOAT_ID AND                                    
		COVERAGE_CODE_ID =  @COV_ID       
   ) 
   
   BEGIN                                                  
		EXEC Proc_Save_WATERCRAFT_COVERAGES                                                         
		@CUSTOMER_ID,--@CUSTOMER_ID     int,                                                 
		@APP_ID,--@APP_ID     int,   
		@APP_VERSION_ID,-- @APP_VERSION_ID     smallint,                
		@BOAT_ID,--@VEHICLE_ID smallint,                                                                
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
   END                                         
		        
    --EXEC  Proc_Update_WATERCRAFT_COVERAGES_ON_RULE @CUSTOMER_ID ,@APP_ID,  @APP_VERSION_ID, @BOAT_ID                              
--************************************************************************                                       
                                                      
RETURN 1                                                                    
                                                                    
                                                                    
                                                                    
                                                                  
                                          
                                           
                                                            
                                                          
                                                        
                                                      
                           
                                                  
                                                
                                              
                                            
                                          
                                        
                                      
                                
                                  
                                
                              
                            
                          
                        
                      
                    
                  
                
              
            
          
        
      
    
  



GO

