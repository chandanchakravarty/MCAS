IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_Save_UMBRELLA_WATERCRAFT_COVERAGES]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_Save_UMBRELLA_WATERCRAFT_COVERAGES]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------  
Proc Name       : dbo.Proc_SAVE_VEHICLE_COVERAGES  
Created by      : Pradeep  
Date            : 5/18/2005  
Purpose     :Inserts a record in APp_UMBRELLA_WATERCRAFT_COVERAGES  
Revison History :  
Used In  : Wolverine  
------------------------------------------------------------  
Date     Review By          Comments  
------   ------------       -------------------------*/  
CREATE           PROC Dbo.Proc_Save_UMBRELLA_WATERCRAFT_COVERAGES  
(  
 @CUSTOMER_ID     int,  
 @APP_ID     int,  
 @APP_VERSION_ID     smallint,  
 @BOAT_ID smallint,  
 @COVERAGE_ID int,  
 @COVERAGE_CODE_ID int,  
 @LIMIT_1 Decimal(18,2),            
 @LIMIT_2 Decimal(18,2),          
 @LIMIT1_AMOUNT_TEXT NVarChar(100),            
 @LIMIT2_AMOUNT_TEXT NVarChar(100),            
 @DEDUCTIBLE1_AMOUNT_TEXT NVarChar(100),            
 @DEDUCTIBLE2_AMOUNT_TEXT NVarChar(100),            
 @LIMIT_1_TYPE NVarChar(5),            
 @LIMIT_2_TYPE NVarChar(5),            
 @DEDUCTIBLE_1 DECIMAL(18,2),            
 @DEDUCTIBLE_2 DECIMAL(18,2),            
 @DEDUCTIBLE_1_TYPE NVarChar(5),            
 @DEDUCTIBLE_2_TYPE NVarChar(5),            
 @WRITTEN_PREMIUM DECIMAL(18,2),            
 @FULL_TERM_PREMIUM DECIMAL(18,2)    
)  
AS  
  
DECLARE @COVERAGE_ID_MAX smallint  
BEGIN  
   
 IF ( @COVERAGE_ID = -1 )  
 BEGIN  
    
  SELECT  @COVERAGE_ID_MAX = isnull(Max(COVERAGE_ID),0)+1   
  FROM APP_UMBRELLA_WATERCRAFT_COVERAGE_INFO  
  where CUSTOMER_ID = @CUSTOMER_ID and   
   APP_ID=@APP_ID and   
   APP_VERSION_ID = @APP_VERSION_ID   
   and BOAT_ID = @BOAT_ID  
  
  INSERT INTO APP_UMBRELLA_WATERCRAFT_COVERAGE_INFO  
  (  
   CUSTOMER_ID,  
   APP_ID,  
   APP_VERSION_ID,  
   BOAT_ID,  
   COVERAGE_ID,	
  COVERAGE_CODE_ID,       
     LIMIT_1,            
     LIMIT_2,            
     DEDUCTIBLE_1,            
     DEDUCTIBLE_2,             
     WRITTEN_PREMIUM,            
     FULL_TERM_PREMIUM,          
     LIMIT1_AMOUNT_TEXT,          
     LIMIT2_AMOUNT_TEXT,          
     DEDUCTIBLE1_AMOUNT_TEXT,          
     DEDUCTIBLE2_AMOUNT_TEXT    
  )  
  VALUES  
  (  
   @CUSTOMER_ID,  
   @APP_ID,  
   @APP_VERSION_ID,  
   @BOAT_ID,  
   @COVERAGE_ID_MAX,  
  @COVERAGE_CODE_ID,       
     @LIMIT_1,            
     @LIMIT_2,            
     @DEDUCTIBLE_1,            
     @DEDUCTIBLE_2,             
     @WRITTEN_PREMIUM,            
     @FULL_TERM_PREMIUM,          
     @LIMIT1_AMOUNT_TEXT,          
     @LIMIT2_AMOUNT_TEXT,          
     @DEDUCTIBLE1_AMOUNT_TEXT,          
     @DEDUCTIBLE2_AMOUNT_TEXT 
  )  

  --Insert dependent Endorsements for this coverage          
 DECLARE @ENDORSEMENT_ID Int          
 DECLARE @STATEID SmallInt                  
 DECLARE @LOBID NVarCHar(5)                  
 DECLARE @VEHICLE_ENDORSEMENT_ID int                
 --N for New Business, R for renewal                
 --DECLARE @APP_TYPE Char(1)                
                 
 --SET @APP_TYPE = 'N'                
                   
 SELECT @STATEID = STATE_ID,                  
  @LOBID = APP_LOB                  
 FROM APP_LIST                  
 WHERE CUSTOMER_ID = @CUSTOMER_ID AND                  
  APP_ID = @APP_ID AND                  
  APP_VERSION_ID = @APP_VERSION_ID                  
    
SET @LOBID  = 4
	        
 SELECT  @ENDORSEMENT_ID = ENDORSMENT_ID          
 FROM MNT_ENDORSMENT_DETAILS MED          
 WHERE SELECT_COVERAGE = @COVERAGE_CODE_ID          
 AND STATE_ID = @STATEID AND          
  LOB_ID = @LOBID          
        
--print(@STATEID)        
--print(@LOBID)        
        
--print (@COVERAGE_CODE_ID)           
--print(ISNULL(@ENDORSEMENT_ID,0))        
        
 IF ( @ENDORSEMENT_ID IS NOT NULL )          
 BEGIN          
  SELECT @VEHICLE_ENDORSEMENT_ID = ISNULL(MAX(BOAT_ENDORSEMENT_ID),0) + 1          
  FROM APP_UMBRELLA_WATERCRAFT_ENDORSEMENTS          
  WHERE CUSTOMER_ID = @CUSTOMER_ID AND                  
    APP_ID = @APP_ID AND                  
    APP_VERSION_ID = @APP_VERSION_ID AND          
    BOAT_ID = @BOAT_ID           
          
  INSERT INTO APP_UMBRELLA_WATERCRAFT_ENDORSEMENTS          
  (          
   CUSTOMER_ID,          
   APP_ID,          
   APP_VERSION_ID,          
   BOAT_ID,          
   ENDORSEMENT_ID,          
   BOAT_ENDORSEMENT_ID          
  )          
  VALUES          
  (          
   @CUSTOMER_ID,          
   @APP_ID,          
   @APP_VERSION_ID,          
   @BOAT_ID,          
   @ENDORSEMENT_ID,          
   @VEHICLE_ENDORSEMENT_ID          
  )          
          
 END    

 RETURN 1  
  
 END   
   
 --Update  
 UPDATE APP_UMBRELLA_WATERCRAFT_COVERAGE_INFO  
 SET  
  COVERAGE_CODE_ID = @COVERAGE_CODE_ID,            
  LIMIT_1 = @LIMIT_1,            
  LIMIT_2 = @LIMIT_2,            
  DEDUCTIBLE_1 = @DEDUCTIBLE_1,            
  DEDUCTIBLE_2 = @DEDUCTIBLE_2,            
  WRITTEN_PREMIUM = @WRITTEN_PREMIUM,             
  FULL_TERM_PREMIUM = @FULL_TERM_PREMIUM,          
  LIMIT1_AMOUNT_TEXT = @LIMIT1_AMOUNT_TEXT,          
    LIMIT2_AMOUNT_TEXT = @LIMIT2_AMOUNT_TEXT,          
    DEDUCTIBLE1_AMOUNT_TEXT = @DEDUCTIBLE1_AMOUNT_TEXT,          
    DEDUCTIBLE2_AMOUNT_TEXT = @DEDUCTIBLE2_AMOUNT_TEXT       
 WHERE CUSTOMER_ID = @CUSTOMER_ID AND  
  APP_ID = @APP_ID AND  
  APP_VERSION_ID = @APP_VERSION_ID AND  
  COVERAGE_ID = @COVERAGE_ID AND  
  BOAT_ID = @BOAT_ID  
    
END  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  



GO

