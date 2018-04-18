IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_UPDATE_POL_WATERCRAFT_COVERAGES]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_UPDATE_POL_WATERCRAFT_COVERAGES]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


--DROP PROC PROC_UPDATE_POL_WATERCRAFT_COVERAGES                                  
                                  
/*----------------------------------------------------------                                                                  
PROC NAME   : DBO.PROC_UPDATE_POL_WATERCRAFT_COVERAGES                                                                 
CREATED BY  : SHAFI                                                                  
DATE        : 14/02/06                                                               
PURPOSE     :  DELETES/INSERTS  DEFAULT COVERAGES WHICH DONOT HAVE ANY BUISNESS LOGIC                                               
               WHEN A WATERCRAFT IS UPDATED                                                      
REVISON HISTORY  :                                                                        
------------------------------------------------------------                                                                              
DATE     REVIEW BY          COMMENTS                                                                            
-----------------------------------------------------------*/           
                                                         
CREATE   PROCEDURE PROC_UPDATE_POL_WATERCRAFT_COVERAGES                                                          
(                                                           
 @CUSTOMER_ID INT,                                                          
 @POL_ID INT,                                                          
 @POL_VERSION_ID SMALLINT,                                                           
 @BOAT_ID SMALLINT                                                        
                                                           
)                                                          
                                                          
AS                                                          
                                                           
                                       
DECLARE @COV_ID INT                                 
     
                                                    
      
      
                             
                                          
                                        
  EXEC @COV_ID = PROC_GET_POL_WATERCRAFT_COVERAGE_ID @CUSTOMER_ID,                                              
           @POL_ID,                                              
       @POL_VERSION_ID,                                          
       'EBIUE'                                              
                                           
  IF ( @COV_ID = 0 )                                          
 BEGIN                                          
  RAISERROR ('COV_ID NOT FOUND FOR  INCREASE IN "UNATTACHED EQUIPMENT" AND PERSONAL EFFECTS COVERAGE',                                          
       16, 1)                                          
                                           
 END  
   IF NOT EXISTS              
    (             
	  SELECT * FROM  POL_WATERCRAFT_COVERAGE_INFO              
	  WHERE CUSTOMER_ID = @CUSTOMER_ID AND              
	  POLICY_ID = @POL_ID AND              
	  POLICY_VERSION_ID = @POL_VERSION_ID AND              
	  BOAT_ID = @BOAT_ID AND              
	  COVERAGE_CODE_ID = @COV_ID              
    )                          
      BEGIN                                         
		EXEC PROC_SAVE_POLICYWATERCRAFT_COVERAGES                                               
		@CUSTOMER_ID,--@CUSTOMER_ID     INT,                                       
		@POL_ID,--@POL_ID     INT,                                                      
		@POL_VERSION_ID,-- @POL_VERSION_ID     SMALLINT,                                                      
		@BOAT_ID,--@VEHICLE_ID SMALLINT,                                                      
		-1,--@COVERAGE_ID INT,                                                      
		@COV_ID,--@COVERAGE_CODE_ID INT,                                                      
		1500,--@LIMIT_1 DECIMAL(18,2),                                                            
		NULL,--@LIMIT_2 DECIMAL(18,2),                                                          
		NULL,--@LIMIT1_AMOUNT_TEXT NVARCHAR(100),                                                            
		NULL,--@LIMIT2_AMOUNT_TEXT NVARCHAR(100),                                                            
		NULL,--@DEDUCTIBLE1_AMOUNT_TEXT NVARCHAR(100),                                            
		NULL,--@DEDUCTIBLE2_AMOUNT_TEXT NVARCHAR(100),                                                
		NULL,--@LIMIT_1_TYPE NVARCHAR(5),                                                            
		NULL,--@LIMIT_2_TYPE NVARCHAR(5),                                                            
		100,--@DEDUCTIBLE_1 DECIMAL(18,2),                                                            
		NULL,--@DEDUCTIBLE_2 DECIMAL(18,2),                                                            
		NULL,--@DEDUCTIBLE_1_TYPE NVARCHAR(5),                                                            
		NULL,--@DEDUCTIBLE_2_TYPE NVARCHAR(5),                                                            
		NULL,--@WRITTEN_PREMIUM DECIMAL(18,2),                                                            
		NULL--@FULL_TERM_PREMIUM DECIMAL(18,2)                 
		IF @@ERROR <> 0                                                  
		BEGIN                                                   
			RETURN                                          
		END   
      END

EXEC PROC_UPDATE_POL_WATERCRAFT_COVERAGES_ON_RULE  @CUSTOMER_ID , @POL_ID , @POL_VERSION_ID , @BOAT_ID                              
                                
           
--************************************************************************                                                
                                      
                                            
RETURN 1                                                          
                                                          
                                                          
                                                          
                                                        
                                          
                                                    
                                                  
                                                
                                              
                                            
                                          
                                        
                          
                                    
                                  
                                
                              
                            
                          
                        
                      
                    
                  
                  
                
              
            
          
        
      
    
    
    
  



GO

