IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UPDATE_WATERCRAFT_COVERAGES_FROM_HOME]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UPDATE_WATERCRAFT_COVERAGES_FROM_HOME]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                                          
                    
Proc Name       : dbo.Proc_UPDATE_WATERAFT_COVERAGES_FROM_HOME                                          
Created by      : Pradeep                                          
Date            : 01/03/2006                                         
Purpose      :  Updates default coverages for watercrfat within home                                       
Revison History :                                          
Used In  : Wolverine                                          
------------------------------------------------------------                                          
Date     Review By          Comments                                          
------   ------------       -------------------------*/                                          
CREATE           PROC dbo.Proc_UPDATE_WATERCRAFT_COVERAGES_FROM_HOME              
(                                        
 @CUSTOMER_ID     int,                                        
 @APP_ID     int,                                        
 @APP_VERSION_ID     smallint              
)                     
                    
AS                     
                    
BEGIN                    
                     
  DECLARE @STATEID SmallInt                                            
  DECLARE @LOBID NVarCHar(5)                         
  DECLARE @DWELLING_ID Int                 
  DECLARE @LIABILITY_ID Int              
  DECLARE @MEDPM_ID Int               
  DECLARE @LIAB_AMOUNT DECIMAL(18,0)              
    DECLARE @MED_AMOUNT DECIMAL(18,0)               
  DECLARE @LIAB_LIMIT_ID Int                
   DECLARE @MED_LIMIT_ID Int                
   DECLARE @UMBCS INT
   DECLARE @UMBCS_LIMIT_ID INT
   
                       
    SELECT @STATEID = STATE_ID,                                            
    @LOBID = APP_LOB                                            
  FROM APP_LIST                                            
  WHERE CUSTOMER_ID = @CUSTOMER_ID AND                                            
    APP_ID = @APP_ID AND                                            
    APP_VERSION_ID = @APP_VERSION_ID                       
                  
  IF (  @STATEID = 14 )              
  BEGIN              
  SET @LIABILITY_ID = 19              
  SET @MEDPM_ID = 21              
  SET @UMBCS=24
    SET @UMBCS_LIMIT_ID=1037            


  END              
                
IF (  @STATEID = 22 )              
  BEGIN              
	  SET @LIABILITY_ID = 65              
	  SET @MEDPM_ID = 68   
	  SET @UMBCS=70    
    SET @UMBCS_LIMIT_ID=1040                  

              
  END              
   /*              
 10  PL Coverage E – Personal Liability Each Occurrence              
 170  PL Coverage E – Personal Liability Each Occurrence              
 171  MEDPM Coverage F – Medical Payment Each Person              
 13  MEDPM Coverage F – Medical Payment Each Person              
              
   */                  
  IF ( @LOBID = 1 )              
  BEGIN              
 --Get the first dwelling for tis LOB              
 SELECT @DWELLING_ID = MIN(DWELLING_ID)              
 FROM APP_DWELLINGS_INFO              
 WHERE CUSTOMER_ID = @CUSTOMER_ID AND              
  APP_ID = @APP_ID AND              
  APP_VERSION_ID = @APP_VERSION_ID               
               
 IF ( @DWELLING_ID IS NULL )              
 BEGIN              
  RETURN              
 END              
               
 IF NOT EXISTS              
 (               
  SELECT BOAT_ID              
  FROM APP_WATERCRAFT_INFO INNER JOIN MNT_LOOKUP_VALUES ON         
    APP_WATERCRAFT_INFO.TYPE_OF_WATERCRAFT =  MNT_LOOKUP_VALUES.LOOKUP_UNIQUE_ID        
            
  WHERE CUSTOMER_ID = @CUSTOMER_ID AND              
   APP_ID = @APP_ID AND              
   APP_VERSION_ID = @APP_VERSION_ID      
 )              
 RETURN              
              
 --Personal Liability limit              
 SELECT @LIAB_AMOUNT = PERSONAL_LIAB_LIMIT,              
  @MED_AMOUNT = MED_PAY_EACH_PERSON              
 FROM APP_DWELLING_COVERAGE              
 WHERE CUSTOMER_ID = @CUSTOMER_ID AND              
  APP_ID = @APP_ID AND              
  APP_VERSION_ID = @APP_VERSION_ID AND              
  DWELLING_ID = @DWELLING_ID               
               
               
 SELECT @LIAB_LIMIT_ID = R.LIMIT_DEDUC_ID              
 FROM MNT_COVERAGE_RANGES R              
 WHERE LIMIT_DEDUC_AMOUNT = @LIAB_AMOUNT              
 AND R.COV_ID = @LIABILITY_ID              
              
 SELECT @MED_LIMIT_ID = R.LIMIT_DEDUC_ID              
 FROM MNT_COVERAGE_RANGES R              
 WHERE LIMIT_DEDUC_AMOUNT = @MED_AMOUNT              
 AND R.COV_ID = @MEDPM_ID              
               
 DECLARE @CUR_BOAT_ID Int     
 DECLARE @LOOKUP_UNIQUE_ID Int          
 DECLARE @STYLE_OF_WATERCRAFT NVARCHAR(10)      
 DECLARE @WATERCRAFTLENGHT NVARCHAR(10)  
 DECLARE @TYPEOFWATERCRAFT   INT   
              
 DECLARE BOAT_CURSOR CURSOR              
    FOR               
 SELECT BOAT_ID,TYPE,LENGTH,TYPE_OF_WATERCRAFT
  FROM APP_WATERCRAFT_INFO INNER JOIN MNT_LOOKUP_VALUES ON         
    APP_WATERCRAFT_INFO.TYPE_OF_WATERCRAFT =  MNT_LOOKUP_VALUES.LOOKUP_UNIQUE_ID        
            
  WHERE CUSTOMER_ID = @CUSTOMER_ID AND              
   APP_ID = @APP_ID AND              
   APP_VERSION_ID = @APP_VERSION_ID        
              
 OPEN BOAT_CURSOR              
              
 FETCH NEXT FROM BOAT_CURSOR              
 INTO @CUR_BOAT_ID,@STYLE_OF_WATERCRAFT ,@WATERCRAFTLENGHT,@TYPEOFWATERCRAFT           
               
 --Save the coverages              
 EXEC Proc_Save_WATERCRAFT_COVERAGES              
  @CUSTOMER_ID,--@CUSTOMER_ID     int,                            
   @APP_ID,--@APP_ID     int,                            
   @APP_VERSION_ID,--@APP_VERSION_ID     smallint,                            
   @CUR_BOAT_ID,--@VEHICLE_ID smallint,                            
   -1,--@COVERAGE_ID int,                            
   @LIABILITY_ID,--@COVERAGE_CODE_ID int,                            
   @LIAB_AMOUNT,--@LIMIT_1 Decimal(18,2),                                  
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
   NULL,--@FULL_TERM_PREMIUM DECIMAL(18,2),                
   @LIAB_LIMIT_ID,--@LIMIT_ID Int  = NULL ,                
   NULL--@DEDUC_ID Int = NULL     

                     
--Uninsured Boaters  will be defaulted when Watercraft Liability is Defaulted     
 if (NOT (@STYLE_OF_WATERCRAFT = 'JS') OR (@TYPEOFWATERCRAFT = 11373) OR (@TYPEOFWATERCRAFT = 11386))
 BEGIN      
 IF NOT EXISTS   
		(                            
			SELECT * FROM APP_WATERCRAFT_COVERAGE_INFO                            
			WHERE CUSTOMER_ID = @CUSTOMER_ID AND                                            
			APP_ID = @APP_ID AND                                            
			APP_VERSION_ID = @APP_VERSION_ID AND                                            
			COVERAGE_CODE_ID = @UMBCS  AND                                  
			BOAT_ID = @CUR_BOAT_ID          
		)                            
        BEGIN                            
			EXEC Proc_Save_WATERCRAFT_COVERAGES                                                               
			@CUSTOMER_ID,--@CUSTOMER_ID    int,                                             
			@APP_ID,--@APP_ID     int,                                                                      
			@APP_VERSION_ID,-- @APP_VERSION_ID     smallint,                                                                      
			@CUR_BOAT_ID,--@VEHICLE_ID smallint,                           
			-1,--@COVERAGE_ID int,                                         
			@UMBCS,--@COVERAGE_CODE_ID int,                                                                      
			50000,--@LIMIT_1 Decimal(18,2),                                                                                   NULL,--@LIMIT_2 Decimal(18,2),                                                                         
			NULL,--@LIMIT1_AMOUNT_TEXT NVarChar(100),                                                                            
			NULL,--@LIMIT2_AMOUNT_TEXT NVarChar(100),                                                                    
			NULL ,--@DEDUCTIBLE1_AMOUNT_TEXT NVarChar(100),                                            
			NULL,--@DEDUCTIBLE2_AMOUNT_TEXT NVarChar(100),                                                                            
			NULL,--@LIMIT_1_TYPE NVarChar(5),                                                                    
			NULL,--@LIMIT_2_TYPE NVarChar(5),                                                                            
			NULL,--@DEDUCTIBLE_1 DECIMAL(18,2),                                                                            
			NULL,--@DEDUCTIBLE_2 DECIMAL(18,2),                                                                            
			NULL,--@DEDUCTIBLE_1_TYPE NVarChar(5),                                                                            
			NULL,--@DEDUCTIBLE_2_TYPE NVarChar(5),                                                                            
			NULL,--@WRITTEN_PREMIUM DECIMAL(18,2),                                                                            
			NULL,--@FULL_TERM_PREMIUM DECIMAL(18,2)
            NULL,    
			@UMBCS_LIMIT_ID,    
			NULL                                                     
			                 
			IF @@ERROR <> 0                                                                
			BEGIN                                                          
				RAISERROR ('Unable to update Boat Replacement Cost Coverage',                                                          
				16, 1)                                                                        
			    RETURN                                                          
			END                                                                  
         END   
 EXEC Proc_Save_WATERCRAFT_COVERAGES              
  @CUSTOMER_ID,--@CUSTOMER_ID     int,                            
   @APP_ID,--@APP_ID     int,                            
   @APP_VERSION_ID,--@APP_VERSION_ID     smallint,                            
   @CUR_BOAT_ID,--@VEHICLE_ID smallint,                            
   -1,--@COVERAGE_ID int,                            
   @MEDPM_ID,--@COVERAGE_CODE_ID int,                            
   @MED_AMOUNT,--@LIMIT_1 Decimal(18,2),                                  
   25000,--@LIMIT_2 Decimal(18,2),                             
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
   NULL,--@FULL_TERM_PREMIUM DECIMAL(18,2),                
   @MED_LIMIT_ID,--@LIMIT_ID Int  = NULL ,                
   NULL--@DEDUC_ID Int = NULL                        
 END      
             
        
 WHILE @@FETCH_STATUS = 0              
 BEGIN              
  FETCH NEXT FROM BOAT_CURSOR              
  INTO @CUR_BOAT_ID,@STYLE_OF_WATERCRAFT ,@WATERCRAFTLENGHT,@TYPEOFWATERCRAFT                    
              
  --Save the coverages              
 EXEC Proc_Save_WATERCRAFT_COVERAGES              
  @CUSTOMER_ID,--@CUSTOMER_ID     int,                            
   @APP_ID,--@APP_ID     int,                            
   @APP_VERSION_ID,--@APP_VERSION_ID     smallint,                            
   @CUR_BOAT_ID,--@VEHICLE_ID smallint,                 
   -1,--@COVERAGE_ID int,                            
   @LIABILITY_ID,--@COVERAGE_CODE_ID int,                            
   @LIAB_AMOUNT,--@LIMIT_1 Decimal(18,2),                                  
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
   NULL,--@FULL_TERM_PREMIUM DECIMAL(18,2),                
   @LIAB_LIMIT_ID,--@LIMIT_ID Int  = NULL ,                
   NULL--@DEDUC_ID Int = NULL       
    
  if (NOT (@STYLE_OF_WATERCRAFT = 'JS') OR (@TYPEOFWATERCRAFT = 11373) OR (@TYPEOFWATERCRAFT = 11386))
 BEGIN     
		IF NOT EXISTS   
		(                            
			SELECT * FROM APP_WATERCRAFT_COVERAGE_INFO                            
			WHERE CUSTOMER_ID = @CUSTOMER_ID AND                                            
			APP_ID = @APP_ID AND                                            
			APP_VERSION_ID = @APP_VERSION_ID AND                                            
			COVERAGE_CODE_ID = @UMBCS  AND                                  
			BOAT_ID = @CUR_BOAT_ID                                       
		)                            
        BEGIN                            
			EXEC Proc_Save_WATERCRAFT_COVERAGES                                                               
			@CUSTOMER_ID,--@CUSTOMER_ID    int,                                             
			@APP_ID,--@APP_ID     int,                                                                      
			@APP_VERSION_ID,-- @APP_VERSION_ID     smallint,                                                                      
			@CUR_BOAT_ID,--@VEHICLE_ID smallint,                           
			-1,--@COVERAGE_ID int,                                         
			@UMBCS,--@COVERAGE_CODE_ID int,                                                                      
			50000,--@LIMIT_1 Decimal(18,2),                                                                                   NULL,--@LIMIT_2 Decimal(18,2),                                                                          
			NULL,--@LIMIT1_AMOUNT_TEXT NVarChar(100),                                                                            
			NULL,--@LIMIT2_AMOUNT_TEXT NVarChar(100),                                                                    
			NULL ,--@DEDUCTIBLE1_AMOUNT_TEXT NVarChar(100),                                            
			NULL,--@DEDUCTIBLE2_AMOUNT_TEXT NVarChar(100),                                                                            
			NULL,--@LIMIT_1_TYPE NVarChar(5),                                                                    
			NULL,--@LIMIT_2_TYPE NVarChar(5),                                                                            
			NULL,--@DEDUCTIBLE_1 DECIMAL(18,2),                                                                            
			NULL,--@DEDUCTIBLE_2 DECIMAL(18,2),           
			NULL,--@DEDUCTIBLE_1_TYPE NVarChar(5),                                                                            
			NULL,--@DEDUCTIBLE_2_TYPE NVarChar(5),                                                                            
			NULL,--@WRITTEN_PREMIUM DECIMAL(18,2),                                                                            
			NULL,--@FULL_TERM_PREMIUM DECIMAL(18,2)    
			@UMBCS_LIMIT_ID,    
			NULL                                                     
			                 
			IF @@ERROR <> 0                                                                
			BEGIN                                                          
				RAISERROR ('Unable to update Boat Replacement Cost Coverage',                                                          
				16, 1)                                                                        
			    RETURN                                                          
			END                                                                  
         END   
   EXEC Proc_Save_WATERCRAFT_COVERAGES              
   @CUSTOMER_ID,--@CUSTOMER_ID     int,                            
   @APP_ID,--@APP_ID     int,                            
   @APP_VERSION_ID,--@APP_VERSION_ID     smallint,                            
   @CUR_BOAT_ID,--@VEHICLE_ID smallint,                            
   -1,--@COVERAGE_ID int,                            
   @MEDPM_ID,--@COVERAGE_CODE_ID int,                            
   @MED_AMOUNT,--@LIMIT_1 Decimal(18,2),                                  
   25000,--@LIMIT_2 Decimal(18,2),                                
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
   NULL,--@FULL_TERM_PREMIUM DECIMAL(18,2),                
   @MED_LIMIT_ID,--@LIMIT_ID Int  = NULL ,                
   NULL--@DEDUC_ID Int = NULL          
  END                    
 END              
              
 CLOSE  BOAT_CURSOR              
 DEALLOCATE BOAT_CURSOR              
              
               
  END              
                        
END                                       
                  
                
              
            
          
        
      
    
  









GO

