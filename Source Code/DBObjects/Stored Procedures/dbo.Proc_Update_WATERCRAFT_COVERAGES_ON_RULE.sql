IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_Update_WATERCRAFT_COVERAGES_ON_RULE]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_Update_WATERCRAFT_COVERAGES_ON_RULE]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


--drop proc Proc_Update_WATERCRAFT_COVERAGES_ON_RULE                                                  
                                                  
/*----------------------------------------------------------                                                                                  
Proc Name   : dbo.Proc_Update_WATERCRAFT_COVERAGES_ON_RULE                                                                                 
Created by  : Pradeep                                                                                  
Date        : 12/12/2005                                                                                
Purpose     :  Deletes/Inserts  relevant coverages                                                               
  when a watercraft is updated                                                                      
Revison History  :                                                                                        
------------------------------------------------------------                                                                                              
Date     Review By          Comments                                                                                            
-----------------------------------------------------------*/                             
--DROP PROC Proc_Update_WATERCRAFT_COVERAGES_ON_RULE                                                                         
CREATE   PROCEDURE Proc_Update_WATERCRAFT_COVERAGES_ON_RULE                                                                          
(                                                                           
 @CUSTOMER_ID int,                                                                          
 @APP_ID int,                                                                          
 @APP_VERSION_ID smallint,                                                                           
 @BOAT_ID smallint                                                                        
                                                                           
)                                                                          
                                                                          
As                                                                          
                                                                           
DECLARE @TYPE  Char(10)                                                                        
DECLARE @AGE Int                                                                        
DECLARE @INSURING_VALUE Decimal(10,2)                                                              
DECLARE @DATE_PURCHASED DateTime                                                              
DECLARE @LENGTH NVarChar(10)                                                               
DECLARE @LENGTHINT Int                                                            
DECLARE @YEAR Int                                                              
DECLARE @BOAT_TOWING DECIMAL(10,2)                                           
DECLARE @BOAT_REPLACE DECIMAL(10,2)                                           
DECLARE @STYLE NVarChar(5)                    
DECLARE @COVTYPE INT             
DECLARE @DEDCTIBLE_TEXT VARCHAR(10)                
DECLARE @APP_EFFECTIVE_DATE datetime    
DECLARE @DEDUCTIBLE_AMOUNT INT    
DECLARE @DEDCTIBLE_AMOUNT INT    
DECLARE @DEDUCTIBLE_TEXT INT    
                                                       
DECLARE @COV_ID Int       
DECLARE @AGC_DEDUC_ID Int       
DECLARE @STATE_ID Int                                              
DECLARE @LOB_ID int                    
DECLARE @APPEFFECTIVEDATE INT          
DECLARE @DEDUCTIBE_ID Int        
                                                                                                        
                                                    
 SELECT @STATE_ID = STATE_ID,             
        @LOB_ID = APP_LOB,                     
        @APPEFFECTIVEDATE= DATEPART(YY,APP_EFFECTIVE_DATE),    
         @APP_EFFECTIVE_DATE =APP_EFFECTIVE_DATE                 
 FROM APP_LIST                                                                          
 WHERE CUSTOMER_ID = @CUSTOMER_ID AND                                                      
   APP_ID = @APP_ID AND                                                                          
   APP_VERSION_ID =  @APP_VERSION_ID                                                                         
                                                                    
SELECT   @TYPE  = TYPE_OF_WATERCRAFT,                       
         @STYLE = TYPE,                                                                     
         @INSURING_VALUE = INSURING_VALUE    ,                                                                    
         @DATE_PURCHASED = DATE_PURCHASED  ,                                                              
         @LENGTH = LENGTH ,                                                            
         @YEAR = YEAR ,                      
         @COVTYPE=COV_TYPE_BASIS                                                              
FROM APP_WATERCRAFT_INFO INNER JOIN MNT_LOOKUP_VALUES ON                    
     APP_WATERCRAFT_INFO.TYPE_OF_WATERCRAFT=MNT_LOOKUP_VALUES.LOOKUP_UNIQUE_ID                    
     WHERE BOAT_ID = @BOAT_ID AND                                             
     CUSTOMER_ID = @CUSTOMER_ID AND                                                  
     APP_ID = @APP_ID AND                                 
     APP_VERSION_ID = @APP_VERSION_ID                           
                                 
       
 --START 001 GET THE INSURE VALUE AND SET  DEDCTIBLE ACCORDINGLY                                                          
 DECLARE  @DEDCTIBLE_SAVED INT      
      
                                                                        
     
            
   --SET THE LENGTH                                                              
 IF ( @LENGTH <> '' )                                                              
 BEGIN                                                              
  SET @LENGTHINT = CONVERT(Int,@LENGTH)                                                              
 END                        
                                                              
     
                
             
--Get Age of watercraft                
 SET @AGE = @APPEFFECTIVEDATE-@YEAR                
                                              
--Mandatory coverage: Boat Towing and Emergency Service Coverage                                                         
--Boat Towing and Emergency Service Coverage = 5% of Insuring value****                                          
  IF ( @INSURING_VALUE IS NOT NULL )                                                     
  BEGIN                                                            
   SET @BOAT_TOWING = @INSURING_VALUE *  0.05                                                        
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
                                                            
   EXEC Proc_Save_WATERCRAFT_COVERAGES                                                               
     @CUSTOMER_ID,--@CUSTOMER_ID     int,                                                                      
     @APP_ID,--@APP_ID     int,                                                                      
     @APP_VERSION_ID,-- @APP_VERSION_ID     smallint,                                                                      
     @BOAT_ID,--@VEHICLE_ID smallint,                                               
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
--End of mandatory******************************************************                                                                
                                                  
                              
                                                                          
                                             
                                              
                                                                           
                                                            
--If Jet ski, jetski lift bar****************************                               
--Remove Trailers                                                     
  IF ( @TYPE IN (11390, 11387) )                   
   BEGIN                                                                        
  --Trailers                                                              
   EXEC Proc_Delete_APP_WATERCRAFT_COVERAGES_BY_CODE @CUSTOMER_ID,                                                                        
         @APP_ID,                                                                        
     @APP_VERSION_ID,                                                                        
     'EBSMT',                 
     @BOAT_ID                                                                        
                                                    
   IF @@ERROR <> 0                                                                  
   BEGIN                                         
                                                                    
    RETURN                                                                  
   END                                                                   
  END                                                              
                                                      
--*****End of Jet ski************************************                                       
                                                              
                                                                 
--If Jetski (w/Lift Bar), remove****************************                                                               
--Section I - Covered Property, Physical Damage - Actual cash value                                             
  IF ( @TYPE = 11387 )                                                                        
   BEGIN                                                                        
  EXEC Proc_Delete_APP_WATERCRAFT_COVERAGES_BY_CODE @CUSTOMER_ID,                            
   @APP_ID,                                                                  
   @APP_VERSION_ID,                                                                        
   'EBPPDACV',         
   @BOAT_ID                                                                        
                                                                    
  IF @@ERROR <> 0                           
  BEGIN                                                
   RETURN                                                                  
  END                                                                   
                                                               
  ----Section I - Covered Property, Physical Damage - Actual cash value                                                      
    EXEC Proc_Delete_APP_WATERCRAFT_COVERAGES_BY_CODE @CUSTOMER_ID,                                                                        
         @APP_ID,                                                                        
         @APP_VERSION_ID,                                                           
         'EBPPDAV',                                                                        
            @BOAT_ID                                                                        
                                                                
    IF @@ERROR <> 0                                                                  
    BEGIN                                                                  
     RETURN                                                            
    END                                                                   
                                                                     
    --Section II - Uninsured Watercraft Liability (CSL)                        
    EXEC Proc_Delete_APP_WATERCRAFT_COVERAGES_BY_CODE @CUSTOMER_ID,                                                                        
         @APP_ID,                                                                    
    @APP_VERSION_ID,                                                                     
    'UMBCS',                                                                        
    @BOAT_ID                                                                        
                      
    IF @@ERROR <> 0                                                                  
    BEGIN                                                                  
     RETURN                                                                  
    END                                                                   
              
    --Client Entertainment Liability (OP 720)                                                                        
    EXEC Proc_Delete_APP_WATERCRAFT_COVERAGES_BY_CODE @CUSTOMER_ID,                                                                 
         @APP_ID,                                                                        
    @APP_VERSION_ID,                                                                        
    'EBSMECE',                                                                        
    @BOAT_ID                                                                         
                                                             
    IF @@ERROR <> 0                                                                  
    BEGIN                                                                  
     RETURN                                               
    END                                                                   
                                                               
    --Agreed Value (AV-100)                                                                     
    EXEC Proc_Delete_APP_WATERCRAFT_COVERAGES_BY_CODE @CUSTOMER_ID,                                                                     
         @APP_ID,                                                                        
    @APP_VERSION_ID,                                                                        
    'EBSCEAV',          
    @BOAT_ID                                                                         
                                                                  
    IF @@ERROR <> 0                                                                  
    BEGIN                                                                  
     RETURN                                                                  
          END                                                                    
 --************End of Jetski with Lift bar***********************************                                         
                                                              
   END -- END OF IF  @TYPE = 11387                                                                  
 ELSE IF  (@TYPE <> 11390)                                                           
  BEGIN                                                              
   --If other than Jet Ski w/Lift bar, remove                                                               
   --Section I - Covered Property, Physical Damage Jet Ski with Life Bar                                                              
   EXEC Proc_Delete_APP_WATERCRAFT_COVERAGES_BY_CODE @CUSTOMER_ID,                                                                        
       @APP_ID,                                                                        
    @APP_VERSION_ID,                                                                        
    'EBPPDJ',                                                                        
    @BOAT_ID                                                                        
                                                                 
   IF @@ERROR <> 0                   
   BEGIN                                   
    RETURN                                                                  
   END                                                                      
  END --END OF ELSE IF            
                                                              
--If Mini jet boat, remove****************************                                                               
 IF ( @TYPE = 11373 )                             
  BEGIN               
   --Section II - Uninsured Watercraft Liability (CSL)                                                                    
    EXEC Proc_Delete_APP_WATERCRAFT_COVERAGES_BY_CODE @CUSTOMER_ID,                                                 
     @APP_ID,                                                         
     @APP_VERSION_ID,                                                                        
     'UMBCS',                                                                        
     @BOAT_ID                                                                        
                                     
  IF @@ERROR <> 0                      
  BEGIN                                                
      RETURN                                                                  
   END                                                                 
                                   
  --Client Entertainment Liability (OP 720)                                                                 
      EXEC Proc_Delete_APP_WATERCRAFT_COVERAGES_BY_CODE @CUSTOMER_ID,                                                                        
        @APP_ID,                                   
        @APP_VERSION_ID,                                                                        
        'EBSMECE',                                                                        
        @BOAT_ID                                                                        
                                                                   
   IF @@ERROR <> 0                                                                  
   BEGIN                                                                  
    RETURN                                                                  
   END                                                          
  END -- end of if  @TYPE = 11373                              
--End of mini jet boat*****************************************                             
                                   
--If Waverunner, remove****************************                                                                  
IF ( @TYPE = 11386 )                                                                        
BEGIN                                                              
   --Section II - Uninsured Watercraft Liability (CSL)                                                                        
   EXEC Proc_Delete_APP_WATERCRAFT_COVERAGES_BY_CODE @CUSTOMER_ID,                                                                  
         @APP_ID,                                                                        
   @APP_VERSION_ID,                                                                        
         'UMBCS',                                                                        
         @BOAT_ID                                                                        
                                                                     
    IF @@ERROR <> 0                                                                  
    BEGIN                                 
     RETURN                                                                  
    END                                                              
                                                               
    --Client Entertainment Liability (OP 720)                                                                 
    EXEC Proc_Delete_APP_WATERCRAFT_COVERAGES_BY_CODE @CUSTOMER_ID,                                                                        
          @APP_ID,                                                               
      @APP_VERSION_ID,                                                                        
      'EBSMECE',                                                                        
      @BOAT_ID                                                                        
                                                
    IF @@ERROR <> 0                           
    BEGIN                                                                  
     RETURN                                                                  
    END                                                              
                                                  
    --Agreed Value (AV-100)                                              
    EXEC Proc_Delete_APP_WATERCRAFT_COVERAGES_BY_CODE @CUSTOMER_ID,                                                                        
         @APP_ID,                                                                        
         @APP_VERSION_ID,                                                                        
    'EBSCEAV',                                                                        
    @BOAT_ID                               
                                                                  
    IF @@ERROR <> 0                                                                  
    BEGIN                                                                  
     RETURN                                                                  
    END                                                               
                                                                   
  END   --end of if @TYPE = 11386                                                           
--End of Waverunner************************                                              
                                                              
--If a) age of boat is more than 20 years or                                                              
--b)market value is more than 75000 or                                                              
--c)length is more than 26 ft                                                
--remove Agreed Value (AV-100)                                                              
IF (  @LENGTHINT > 26 OR @INSURING_VALUE > 75000 OR  @AGE > 20 )                                                  
  BEGIN                                               --Agreed Value (AV-100)                                                              
       EXEC Proc_Delete_APP_WATERCRAFT_COVERAGES_BY_CODE @CUSTOMER_ID,                                        
        @APP_ID,                                                                        
        @APP_VERSION_ID,                                                                        
        'EBSCEAV',                                                                        
         @BOAT_ID                                                                         
                                                                
   IF @@ERROR <> 0                                                                  
   BEGIN                                                                  
     RETURN                                                  
   END                                            
   END                             
                                                                   
                                  
                                                         
           
                                                         
--******************************************************************                                                            
                                        
/* If boat is one of these, remove Uninsured Boaters if its exists                                        
Jet ski                                        
Jet ski with lift bar                                        
Mini Jet boat                                        
Wave runner                                        
Wave runner with lift bar                                        
*/                                        
 IF ( @STYLE = 'JS' OR @STYLE = 'IO')                                                                        
  BEGIN                                        
   EXEC  Proc_Delete_APP_WATERCRAFT_COVERAGES_BY_CODE @CUSTOMER_ID,                                                                        
         @APP_ID,                                            
         @APP_VERSION_ID,                                                                        
         'UMBCS',              
         @BOAT_ID                                                   
         IF @@ERROR <> 0                                                                  
          BEGIN                                                  
                    RAISERROR ('Unable to delete Uninsured Boaters.',16, 1)                                                              
          RETURN                        
                                                            
        END                                                  
 END                                         
--End**********************************************                                        
                                          
--If JetSki, Jetski w/Lift Bar, Jetski Trailer, Default                                          
--Section 1 - Covered Property Damage Jet Ski EBPPDJ                                          
--else default                                           
--Section 1 - Covered Property Damage - Agreed Value EBPPDAV                                          
 IF ( @STYLE = 'JS' )                                                                        
  BEGIN                                          
    EXEC   @COV_ID = Proc_Get_WATERCRAFT_COVERAGE_ID @CUSTOMER_ID,                                        
           @APP_ID,                                                              
 @APP_VERSION_ID,                                                              
           'EBPPDJ'                                                              
          IF ( @COV_ID = 0 )                                                          
      BEGIN                                         
       RAISERROR ('COV_ID not found for Section 1 - Covered Property Damage Jet Ski',                                                          
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
         SELECT @AGC_DEDUC_ID = R.LIMIT_DEDUC_ID                  
           FROM MNT_COVERAGE_RANGES R                  
          WHERE LIMIT_DEDUC_AMOUNT = 250                  
   AND R.COV_ID = @COV_ID    
        --Default Value                                                  
        SET @DEDUCTIBLE_AMOUNT=250  
        EXEC Proc_Save_WATERCRAFT_COVERAGES                            
        @CUSTOMER_ID,--@CUSTOMER_ID     int,                                                                      
         @APP_ID,--@APP_ID     int,                                                                      
        @APP_VERSION_ID,-- @APP_VERSION_ID     smallint,                                                                      
         @BOAT_ID,--@VEHICLE_ID smallint,                                                                      
        -1,--@COVERAGE_ID int,                                                                      
         @COV_ID,--@COVERAGE_CODE_ID int,                                                                      
         @INSURING_VALUE,--@LIMIT_1 Decimal(18,2),                                                                            
         NULL,--@LIMIT_2 Decimal(18,2),                                                                          
         NULL,--@LIMIT1_AMOUNT_TEXT NVarChar(100),                                                  
         NULL,--@LIMIT2_AMOUNT_TEXT NVarChar(100),                                    
         NULL,--@DEDUCTIBLE1_AMOUNT_TEXT NVarChar(100),                                        
         NULL,--@DEDUCTIBLE2_AMOUNT_TEXT NVarChar(100),                                       
         NULL,--@LIMIT_1_TYPE NVarChar(5),                                                                    
         NULL,--@LIMIT_2_TYPE NVarChar(5),                        
         @DEDUCTIBLE_AMOUNT,--@DEDUCTIBLE_1 DECIMAL(18,2),                                                                            
         NULL,--@DEDUCTIBLE_2 DECIMAL(18,2),                                                                            
         NULL,--@DEDUCTIBLE_1_TYPE NVarChar(5),                                 
         NULL,--@DEDUCTIBLE_2_TYPE NVarChar(5),                                                                            
         NULL,--@WRITTEN_PREMIUM DECIMAL(18,2),                                                                 
         NULL,--@FULL_TERM_PREMIUM DECIMAL(18,2)    
         NULL,  
         @AGC_DEDUC_ID                               
       END                          
    ELSE                            
     BEGIN                            
     SELECT @DEDUCTIBLE_AMOUNT  =ISNULL(Deductible_1,0),    
            @AGC_DEDUC_ID      =isnull(DEDUC_ID,0)  
            FROM APP_WATERCRAFT_COVERAGE_INFO    
            WHERE BOAT_ID = @BOAT_ID AND                                             
   CUSTOMER_ID = @CUSTOMER_ID AND                                                  
   APP_ID = @APP_ID AND                                 
   APP_VERSION_ID = @APP_VERSION_ID AND    
   COVERAGE_CODE_ID = @COV_ID       
  
  
        EXEC Proc_UPDATE_WATERCRAFT_COVERAGE_BY_ID                                            
        @CUSTOMER_ID,--@CUSTOMER_ID     int,                                            
        @APP_ID,--@APP_ID     int,                                            
        @APP_VERSION_ID,--@APP_VERSION_ID     smallint,                                            
        @BOAT_ID,--@BOAT_ID smallint,                                            
        @COV_ID,--@COVERAGE_CODE_ID int,                                            
        @INSURING_VALUE,--@LIMIT_1 Decimal(18,2),                                                  
        NULL,--@LIMIT_2 Decimal(18,2),                                                
        NULL,--@LIMIT1_AMOUNT_TEXT NVarChar(100),                                                  
        NULL,--@LIMIT2_AMOUNT_TEXT NVarChar(100),                                                
        NULL,--@LIMIT_ID Int  = NULL ,                                
        @AGC_DEDUC_ID,--@DEDUC_ID Int = NULL                                      
     @DEDUCTIBLE_AMOUNT--@Deductible_1                                       
      END       
      --SET THE DEDUCTIBLE FOR BOAT REPLACEMENT COST                                      
      SET   @DEDCTIBLE_SAVED    =@DEDUCTIBLE_AMOUNT                                   
  --Delete Agreed Value or Actual Value if it exists                                      
                                 
   EXEC Proc_Delete_APP_WATERCRAFT_COVERAGES_BY_CODE @CUSTOMER_ID,                                                                        
   @APP_ID,                                                                        
   @APP_VERSION_ID,                                                                        
   'EBPPDACV',                                                                        
   @BOAT_ID                                       
   IF @@ERROR <> 0                                                                  
   BEGIN                                                          
        RAISERROR ('Unable to delete Section 1 - Covered Property Damage - Actual Cash Value',                                                          
   16, 1)                                                              
   RETURN      
   END                 
                                        
   --EBPPDAV Section 1 - Covered Property Damage - Agreed Value                                          
   EXEC Proc_Delete_APP_WATERCRAFT_COVERAGES_BY_CODE @CUSTOMER_ID,                                                                        
   @APP_ID,                                                                       
   @APP_VERSION_ID,                                                                        
   'EBPPDAV',                                                                        
   @BOAT_ID                             
   IF @@ERROR <> 0                                                      
   BEGIN                                                          
         RAISERROR ('Unable to delete Section 1 - Covered Property Damage - Agreed Value',                                                          
   16, 1)                                                              
   RETURN                                                          
   END                                                   
                                                 
   END                                          
 ELSE     
                    
  BEGIN                     
   --START 002 GET DECUCTIBLE_ID     
    
  
   IF @APP_EFFECTIVE_DATE >= '2006-03-01'     
   BEGIN     
      SET @DEDCTIBLE_SAVED=0    
      SELECT   @DEDCTIBLE_SAVED = ISNULL(Deductible_1  ,0)    
      FROM APP_WATERCRAFT_COVERAGE_INFO      
      WHERE BOAT_ID = @BOAT_ID AND                                             
      CUSTOMER_ID = @CUSTOMER_ID AND                                                  
      APP_ID = @APP_ID AND                                 
      APP_VERSION_ID = @APP_VERSION_ID AND      
      coverage_code_id IN (11,15,59,61,817,818) AND      
      ISNULL(DEDUCTIBLE1_AMOUNT_TEXT,'')=''    
    
       
  --  IF    @INSURING_VALUE <=10000        
  --   SET   @DEDCTIBLE_TEXT= 100      
  --  ELSE IF    @INSURING_VALUE > 1000 AND  @INSURING_VALUE <= 25000       
  --   SET   @DEDCTIBLE_TEXT= 250      
  --  ELSE      
  --   SET   @DEDCTIBLE_TEXT= 500      
  --      
  --  IF ISNULL(@DEDCTIBLE_SAVED,0)=0 OR @DEDCTIBLE_SAVED < @DEDCTIBLE_TEXT      
  --   SET  @DEDCTIBLE_SAVED=@DEDCTIBLE_TEXT      
            
    
     IF    @INSURING_VALUE <= 10000        
     BEGIN    
    
       SET   @DEDUCTIBLE_AMOUNT=100    
       SET   @DEDCTIBLE_TEXT= ''    
     END    
     ELSE IF    @INSURING_VALUE > 10000 AND  @INSURING_VALUE <= 25000       
     BEGIN    
        SET   @DEDUCTIBLE_AMOUNT=250    
        SET   @DEDCTIBLE_TEXT= ''    
     END    
     ELSE      
     BEGIN    
       SET   @DEDUCTIBLE_AMOUNT= 500      
       SET   @DEDCTIBLE_TEXT= ''    
     END    
    
    
      
      IF ISNULL(@DEDCTIBLE_SAVED,0)=0 OR @DEDCTIBLE_SAVED < @DEDUCTIBLE_AMOUNT      
      BEGIN  
             SET  @DEDCTIBLE_SAVED=@DEDUCTIBLE_AMOUNT      
      END  
             SET @DEDCTIBLE_TEXT=''    
  
        END     
        ELSE    
     BEGIN     
   SET @DEDUCTIBLE_AMOUNT=0    
     
   SELECT  @DEDUCTIBLE_AMOUNT  =ISNULL(Deductible_1,0),    
   @DEDCTIBLE_TEXT    =ISNULL(DEDUCTIBLE1_AMOUNT_TEXT,'')    
   FROM APP_WATERCRAFT_COVERAGE_INFO    
   WHERE BOAT_ID = @BOAT_ID AND                                             
   CUSTOMER_ID = @CUSTOMER_ID AND                                                  
   APP_ID = @APP_ID AND                                 
   APP_VERSION_ID = @APP_VERSION_ID AND    
   COVERAGE_CODE_ID IN (11,15,59,61,817,818)    
   PRINT 'CALLED'    
     
   IF @DEDUCTIBLE_AMOUNT=0 OR @DEDCTIBLE_TEXT=''    
   BEGIN    
    SET @DEDUCTIBLE_AMOUNT=100    
    SET @DEDCTIBLE_TEXT='-1%'    
   END    
   ELSE    
   BEGIN    
    SET @DEDCTIBLE_SAVED=@DEDUCTIBLE_AMOUNT    
   END    
    SET  @DEDCTIBLE_SAVED=@DEDUCTIBLE_AMOUNT      
  
    
             
        END      
                                                       
     
-- END 001    
  IF(@COVTYPE = 11759 )                    
   BEGIN                      
    EXEC Proc_Delete_APP_WATERCRAFT_COVERAGES_BY_CODE @CUSTOMER_ID,                                                                        
    @APP_ID,                        
    @APP_VERSION_ID,                                                          
    'EBPPDACV',                                                                        
    @BOAT_ID                                                   
    IF @@ERROR <> 0                                                                  
    BEGIN                                                          
          RAISERROR ('Section 1 - Covered Property Damage - Actual Cash Value',                                                          
    16, 1)                                                              
    RETURN                                                          
       END                 
            
    EXEC @COV_ID = Proc_Get_WATERCRAFT_COVERAGE_ID @CUSTOMER_ID,                                                              
    @APP_ID,                                                              
    @APP_VERSION_ID,                                                              
    'EBPPDAV'                                                              
    IF ( @COV_ID = 0 )                                                          
    BEGIN                                                          
    RAISERROR ('COV_ID not found for Section II - Liability (CSL)   ',                                                        
    16, 1)                                                          
                                                
          END                                           
      
     
    SELECT @AGC_DEDUC_ID = R.LIMIT_DEDUC_ID                    
    FROM MNT_COVERAGE_RANGES R                    
    WHERE LIMIT_DEDUC_AMOUNT = @DEDCTIBLE_SAVED                    
    AND R.COV_ID = @COV_ID  AND ISNULL(LIMIT_DEDUC_AMOUNT_TEXT,'')=@DEDCTIBLE_TEXT    
   
     
     
    
            
  --Either insert/update Section 1 - Covered Property Damage - Agreed Value                             
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
            @INSURING_VALUE,--@LIMIT_1 Decimal(18,2),                                                                            
            NULL,--@LIMIT_2 Decimal(18,2),                                                                          
            NULL,--@LIMIT1_AMOUNT_TEXT NVarChar(100),                                                                           
            NULL,--@LIMIT2_AMOUNT_TEXT NVarChar(100),                                                                     
            @DEDCTIBLE_TEXT ,--@DEDUCTIBLE1_AMOUNT_TEXT NVarChar(100),                        
            NULL,--@DEDUCTIBLE2_AMOUNT_TEXT NVarChar(100),                                                                            
            NULL,--@LIMIT_1_TYPE NVarChar(5),                                                                    
            NULL,--@LIMIT_2_TYPE NVarChar(5),                                                                            
            @DEDCTIBLE_SAVED,--@DEDUCTIBLE_1 DECIMAL(18,2),                                                                            
            NULL,--@DEDUCTIBLE_2 DECIMAL(18,2),                                                                            
            NULL,--@DEDUCTIBLE_1_TYPE NVarChar(5),                                                                            
            NULL,--@DEDUCTIBLE_2_TYPE NVarChar(5),                                                                            
            NULL,--@WRITTEN_PREMIUM DECIMAL(18,2),                                                                            
            NULL,--@FULL_TERM_PREMIUM DECIMAL(18,2)    
            NULL,    
            @AGC_DEDUC_ID                                           
      END                            
      ELSE                            
      BEGIN                            
    EXEC Proc_UPDATE_WATERCRAFT_COVERAGE_BY_ID                                                                    
    @CUSTOMER_ID,--@CUSTOMER_ID     int,                                                
    @APP_ID,--@POLICY_ID     int,                                             
    @APP_VERSION_ID,--@POLICY_VERSION_ID     smallint,                                                
    @BOAT_ID,--@BOAT_ID smallint,                                                
    @COV_ID,--@COVERAGE_CODE_ID int,                                                
    @INSURING_VALUE,--@LIMIT_1 Decimal(18,2),                                                      
    NULL,--@LIMIT_2 Decimal(18,2),                                                    
    NULL,--@LIMIT1_AMOUNT_TEXT NVarChar(100),                                                      
    NULL,--@LIMIT2_AMOUNT_TEXT NVarChar(100),                                                  
    NULL,--@LIMIT_ID Int  = NULL ,                                    
    @AGC_DEDUC_ID,--@DEDUC_ID Int = NULL      
    @DEDCTIBLE_SAVED,--  @Deductible_1     
    @DEDCTIBLE_TEXT--DEDCTIBLE_TEXT       
                                         
      END                      
   END                    
 ELSE                    
   BEGIN                    
   EXEC Proc_Delete_APP_WATERCRAFT_COVERAGES_BY_CODE @CUSTOMER_ID,                                                                        
      @APP_ID,                                                                        
   @APP_VERSION_ID,                                                                        
   'EBPPDAV',                                                                        
   @BOAT_ID                                                   
   IF @@ERROR <> 0                                                                  
   BEGIN                          
             RAISERROR ('Unable to delete Section 1 - Covered Property Damage Jet Ski',                                                          
       16, 1)                                                              
   RETURN                                                          
   END                         
    END                             
   --End of Agreed value************************************************                            
                            
  --If Actual value exists, update the limit amount---------------                     
                    
 IF(@COVTYPE = 11758)                    
 BEGIN                      
     EXEC Proc_Delete_APP_WATERCRAFT_COVERAGES_BY_CODE @CUSTOMER_ID,                                                                        
        @APP_ID,                              
     @APP_VERSION_ID,                                                                        
     'EBPPDAV',                                                                        
     @BOAT_ID                                                IF @@ERROR <> 0                                                                  
      BEGIN                                                          
                RAISERROR ('Unable to delete Section 1 - Covered Property Damage Jet Ski',                                                          
          16, 1)                                                       
      RETURN                                                          
      END                   
     EXEC @COV_ID = Proc_Get_WATERCRAFT_COVERAGE_ID                             
     @CUSTOMER_ID,                                                                
     @APP_ID,                                                                
     @APP_VERSION_ID,                                                                
     'EBPPDACV'                                                      
     IF ( @COV_ID = 0 )                                                            
      BEGIN                                                            
      RAISERROR ('COV_ID not found for Section 1 - Covered Property Damage - Actual Cash Value',                                                            
           16, 1)                                                            
                                                              
      END       
   --START 002 GET DECUCTIBLE_ID      
     SELECT @AGC_DEDUC_ID = R.LIMIT_DEDUC_ID                    
    FROM MNT_COVERAGE_RANGES R                    
    WHERE LIMIT_DEDUC_AMOUNT = @DEDCTIBLE_SAVED                    
    AND R.COV_ID = @COV_ID  AND ISNULL(LIMIT_DEDUC_AMOUNT_TEXT,'')=@DEDCTIBLE_TEXT      
     -- END  002                       
                  
                                
    IF NOT EXISTS                            
    (                            
     SELECT * FROM  APP_WATERCRAFT_COVERAGE_INFO                            
     WHERE CUSTOMER_ID = @CUSTOMER_ID AND                            
     APP_ID = @APP_ID AND                            
     APP_VERSION_ID = @APP_VERSION_ID AND                            
     BOAT_ID = @BOAT_ID AND                            
     COVERAGE_CODE_ID = @COV_ID                            
    )                                        
               
      BEGIN           
                 
           EXEC Proc_Save_WATERCRAFT_COVERAGES                                                               
            @CUSTOMER_ID,--@CUSTOMER_ID     int,                                                                      
            @APP_ID,--@APP_ID     int,                                                                      
            @APP_VERSION_ID,-- @APP_VERSION_ID     smallint,                                                                      
            @BOAT_ID,--@VEHICLE_ID smallint,                                                                      
            -1,--@COVERAGE_ID int,                     
            @COV_ID,--@COVERAGE_CODE_ID int,                                                                      
            @INSURING_VALUE,--@LIMIT_1 Decimal(18,2),                                                                            
            NULL,--@LIMIT_2 Decimal(18,2),                                                                          
            NULL,--@LIMIT1_AMOUNT_TEXT NVarChar(100),                                                                           
            NULL,--@LIMIT2_AMOUNT_TEXT NVarChar(100),                                                                     
            @DEDCTIBLE_TEXT ,--@DEDUCTIBLE1_AMOUNT_TEXT NVarChar(100),                                            
            NULL,--@DEDUCTIBLE2_AMOUNT_TEXT NVarChar(100),                                         
            NULL,--@LIMIT_1_TYPE NVarChar(5),                                                                    
            NULL,--@LIMIT_2_TYPE NVarChar(5),                                                                            
            @DEDCTIBLE_SAVED,--@DEDUCTIBLE_1 DECIMAL(18,2),                                                                            
            NULL,--@DEDUCTIBLE_2 DECIMAL(18,2),                                                                            
            NULL,--@DEDUCTIBLE_1_TYPE NVarChar(5),                                                                            
            NULL,--@DEDUCTIBLE_2_TYPE NVarChar(5),                                                                            
            NULL,--@WRITTEN_PREMIUM DECIMAL(18,2),                                                                            
            NULL,--@FULL_TERM_PREMIUM DECIMAL(18,2)    
            NULL,    
            @AGC_DEDUC_ID                 
          END                    
          ELSE                    
          BEGIN                             
          
          EXEC Proc_UPDATE_WATERCRAFT_COVERAGE_BY_ID                                                                    
          @CUSTOMER_ID,--@CUSTOMER_ID     int,                                                
          @APP_ID,--@POLICY_ID     int,                                             
          @APP_VERSION_ID,--@POLICY_VERSION_ID     smallint,                                                
          @BOAT_ID,--@BOAT_ID smallint,                                                
          @COV_ID,--@COVERAGE_CODE_ID int,                                                
          @INSURING_VALUE,--@LIMIT_1 Decimal(18,2),                                                      
          NULL,--@LIMIT_2 Decimal(18,2),                                                    
          NULL,--@LIMIT1_AMOUNT_TEXT NVarChar(100),                                                      
          NULL,--@LIMIT2_AMOUNT_TEXT NVarChar(100),                                                  
          NULL,--@LIMIT_ID Int  = NULL ,                                    
          @AGC_DEDUC_ID,--@DEDUC_ID Int = NULL      
          @DEDCTIBLE_SAVED,--  @Deductible_1       
          @DEDCTIBLE_TEXT--DEDCTIBLE_TEXT                              
          END                         
      END                    
   ELSE                    
 BEGIN                    
     EXEC Proc_Delete_APP_WATERCRAFT_COVERAGES_BY_CODE @CUSTOMER_ID,                                                                       
     @APP_ID,                                         
     @APP_VERSION_ID,                                                                        
     'EBPPDACV',                                                                        
     @BOAT_ID                                                   
     IF @@ERROR <> 0                                                                  
      BEGIN                                                          
            RAISERROR ('Unable to delete Section 1 - Covered Property Damage Jet Ski',                                                          
      16, 1)                                                              
      RETURN                                              
       END                     
      END                    
                          
                               
  -- **End of actual cash value******************************************************************                            
                                          
     --EBPPDJ Section 1 - Covered Property Damage Jet Ski                                          
     --Delete  Section 1 - Covered Property Damage Jet Ski if it exists                                                        
     EXEC Proc_Delete_APP_WATERCRAFT_COVERAGES_BY_CODE @CUSTOMER_ID,                         
     @APP_ID,                                                                        
     @APP_VERSION_ID,                                                                        
     'EBPPDJ',                                                                        
     @BOAT_ID                                                   
     IF @@ERROR <> 0                                                                  
      BEGIN                                                          
            RAISERROR ('Unable to delete Section 1 - Covered Property Damage Jet Ski',                                                          
      16, 1)                                                              
      RETURN                                                          
     END                             
 END    
  
--START(BRCC) Boat Replacement Cost Coverage  
--If age of Boat is not more than 5 years old,                                                               
--this will become mandatory coverage                                                              
--Boat Replacement Cost Coverage                                         
 IF ( @INSURING_VALUE IS NOT NULL )                                                     
 BEGIN                                                            
  SET @BOAT_REPLACE = @INSURING_VALUE                                                         
 END                                                
 ELSE                                                
 BEGIN                                                            
  SET @BOAT_REPLACE = 0                                                
 END     
IF (@AGE <= 5 AND @COVTYPE =11758 )                                                               
   BEGIN                                                              
    EXEC @COV_ID = Proc_Get_WATERCRAFT_COVERAGE_ID @CUSTOMER_ID,                         
       @APP_ID,                                                    
       @APP_VERSION_ID,                                                              
       'BRCC'                                        
                                                               
    IF ( @COV_ID = 0 )                                                          
    BEGIN                                                          
           RAISERROR ('COV_ID not found for Boat Replacement Cost Coverage',16, 1)                                                          
    END                                                          
                             
  IF NOT EXISTS                            
  (                            
   SELECT * FROM APP_WATERCRAFT_COVERAGE_INFO                            
   WHERE CUSTOMER_ID = @CUSTOMER_ID AND                                            
   APP_ID = @APP_ID AND                                            
   APP_VERSION_ID = @APP_VERSION_ID AND                                            
   COVERAGE_CODE_ID = @COV_ID  AND                                  
   BOAT_ID = @BOAT_ID                                       
  )                            
         BEGIN                            
                                                                
    EXEC Proc_Save_WATERCRAFT_COVERAGES                                                               
      @CUSTOMER_ID,--@CUSTOMER_ID    int,                                             
       @APP_ID,--@APP_ID     int,                                                                      
      @APP_VERSION_ID,-- @APP_VERSION_ID     smallint,                                                                      
       @BOAT_ID,--@VEHICLE_ID smallint,                           
       -1,--@COVERAGE_ID int,                                         
       @COV_ID,--@COVERAGE_CODE_ID int,                                                                      
       @BOAT_REPLACE,--@LIMIT_1 Decimal(18,2),                                                                                   NULL,--@LIMIT_2 Decimal(18,2),                                                                          
       NULL,--@LIMIT1_AMOUNT_TEXT NVarChar(100),                                                                            
       NULL,--@LIMIT2_AMOUNT_TEXT NVarChar(100),                                                                    
       @DEDCTIBLE_TEXT ,--@DEDUCTIBLE1_AMOUNT_TEXT NVarChar(100),                                            
    NULL,--@DEDUCTIBLE2_AMOUNT_TEXT NVarChar(100),                                                                            
    NULL,--@LIMIT_1_TYPE NVarChar(5),                                                                    
    NULL,--@LIMIT_2_TYPE NVarChar(5),                                                                            
    @DEDCTIBLE_SAVED,--@DEDUCTIBLE_1 DECIMAL(18,2),                                                                            
    NULL,--@DEDUCTIBLE_2 DECIMAL(18,2),                                                                            
    NULL,--@DEDUCTIBLE_1_TYPE NVarChar(5),                                                                            
    NULL,--@DEDUCTIBLE_2_TYPE NVarChar(5),                                                                            
    NULL,--@WRITTEN_PREMIUM DECIMAL(18,2),                                                                            
    NULL,--@FULL_TERM_PREMIUM DECIMAL(18,2)    
    NULL,    
       NULL                                                     
                                                             
       IF @@ERROR <> 0                                                                
       BEGIN                                                          
             RAISERROR ('Unable to update Boat Replacement Cost Coverage',                                                          
             16, 1)                                                                        
         RETURN                                                          
       END                                                                  
         END                            
        ELSE                            
         BEGIN                            
      EXEC Proc_UPDATE_WATERCRAFT_COVERAGE_BY_ID                                                                
      @CUSTOMER_ID,--@CUSTOMER_ID     int,                                              
      @APP_ID,--@APP_ID     int,                                              
      @APP_VERSION_ID,--@APP_VERSION_ID     smallint,                                              
      @BOAT_ID,--@BOAT_ID smallint,                                              
      @COV_ID,--@COVERAGE_CODE_ID int,                                              
      @BOAT_REPLACE,--@LIMIT_1 Decimal(18,2),                                                    
      NULL,--@LIMIT_2 Decimal(18,2),                                                  
      NULL,--@LIMIT1_AMOUNT_TEXT NVarChar(100),                                   
      NULL,--@LIMIT2_AMOUNT_TEXT NVarChar(100),                                                
      NULL,--@LIMIT_ID Int  = NULL ,                                  
      NULL,--@DEDUC_ID Int = NULL      
      @DEDCTIBLE_SAVED,--  @Deductible_1     
      @DEDCTIBLE_TEXT--DEDCTIBLE_TEXT       
                           
      END                            
                                                                  
    END                                             
 ELSE                                              
    BEGIN                                              
   --Delete Boat Replacement Cost Coverage if it exists                                              
     EXEC Proc_Delete_APP_WATERCRAFT_COVERAGES_BY_CODE @CUSTOMER_ID,                                                                        
           @APP_ID,                                                                        
           @APP_VERSION_ID,                                                        
           'BRCC',                                                                        
           @BOAT_ID                                                   
      IF @@ERROR <> 0                 
           BEGIN                                                          
                   RAISERROR ('Unable to delete Boat Replacement Cost Coverage',16, 1)                                                              
                   RETURN                                    
             END                                                              
    END                                  
                                                      
--END (BRCC)  
                                          
--End**********************************************                                          
                                    
--If Watercrfat is oneof these types, then default Medical payments to                                 
--1000 if value greater than 1000    
                                              
IF  @STYLE = 'JS' OR @TYPE=11373 OR @TYPE=11386
                                   
 BEGIN                                
  DECLARE @LIMIT_ID Int                                
                           
  IF ( @STATE_ID = 14 ) SET @LIMIT_ID = 803                                
  IF ( @STATE_ID = 22 ) SET @LIMIT_ID = 808                                
  IF ( @STATE_ID = 49 ) SET @LIMIT_ID = 981                             
                           
  IF EXISTS                                
  (                                
   SELECT * FROM APP_WATERCRAFT_COVERAGE_INFO                                
   WHERE BOAT_ID = @BOAT_ID AND                                                                        
   CUSTOMER_ID = @CUSTOMER_ID AND                                                                        
   APP_ID = @APP_ID AND                                                                
   APP_VERSION_ID = @APP_VERSION_ID AND                                
   COVERAGE_CODE_ID IN (21,68,821) AND                                
   LIMIT_1 > 1000                                 
  )                                
   BEGIN                                
    UPDATE APP_WATERCRAFT_COVERAGE_INFO                                
    SET LIMIT_ID = @LIMIT_ID,                                
    LIMIT_1 = 1000                                 
    WHERE BOAT_ID = @BOAT_ID AND                                              
    CUSTOMER_ID = @CUSTOMER_ID AND                                                                        
    APP_ID = @APP_ID AND                    
    APP_VERSION_ID = @APP_VERSION_ID AND                                
    COVERAGE_CODE_ID IN (21,68,821)                                 
                                  
          END                                
 END                                
--************************************************************************                                             
                                                            
RETURN 1                                                                          
                                                                          
                                                                          
                                                                          
                                                                        
                                                
                                                 
                                                                  
                                                                
                                                              
                                                            
                                 
                                                        
                                                      
                                                    
                                          
                                                
                                              
                                            
                                      
                                        
                                      
                                    
                                  
                                
                              
                            
                          
                        
                      
                    
                  
                
              
            
          
        
      
     
    
     
  



GO

