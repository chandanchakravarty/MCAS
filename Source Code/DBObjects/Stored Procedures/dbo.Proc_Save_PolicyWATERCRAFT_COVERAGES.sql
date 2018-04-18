IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_Save_PolicyWATERCRAFT_COVERAGES]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_Save_PolicyWATERCRAFT_COVERAGES]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------              
Proc Name       : dbo.Proc_Save_PolicyWATERCRAFT_COVERAGES                        
Created by      : shafi              
Date            : 14/02/06          
Purpose      : Saves records in Watercraft Coverages and inserts            
              dependent endorsements in POL_WATERCRAFT_ENDORSEMENTS              
Revison History :              
Used In  : Wolverine              
drop proc Proc_Save_PolicyWATERCRAFT_COVERAGES
------------------------------------------------------------              
Date     Review By          Comments              
------   ------------       -------------------------*/              
--drop proc dbo.Proc_Save_PolicyWATERCRAFT_COVERAGES             
CREATE           PROC dbo.Proc_Save_PolicyWATERCRAFT_COVERAGES             
(              
 @CUSTOMER_ID     int,              
 @POL_ID     int,              
 @POL_VERSION_ID     smallint,              
 @VEHICLE_ID smallint,              
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
 @FULL_TERM_PREMIUM DECIMAL(18,2),
 @LIMIT_ID Int  = NULL ,  
 @DEDUC_ID Int = NULL                     
)              
AS              
              
DECLARE @COVERAGE_ID_MAX smallint              
BEGIN              
               
 IF NOT EXISTS      
(      
 SELECT * FROM POL_WATERCRAFT_COVERAGE_INFO      
WHERE CUSTOMER_ID = @CUSTOMER_ID and               
   POLICY_ID=@POL_ID and               
   POLICY_VERSION_ID = @POL_VERSION_ID               
   and BOAT_ID = @VEHICLE_ID AND      
   COVERAGE_CODE_ID =  @COVERAGE_CODE_ID              
)              
 BEGIN              
                
  SELECT  @COVERAGE_ID_MAX = isnull(Max(COVERAGE_ID),0)+1               
  FROM POL_WATERCRAFT_COVERAGE_INFO              
  where CUSTOMER_ID = @CUSTOMER_ID and               
   POLICY_ID=@POL_ID and               
   POLICY_VERSION_ID = @POL_VERSION_ID               
   and BOAT_ID = @VEHICLE_ID              
              
  INSERT INTO POL_WATERCRAFT_COVERAGE_INFO              
  (              
   CUSTOMER_ID,              
   POLICY_ID,              
   POLICY_VERSION_ID,              
   BOAT_ID,              
   COVERAGE_ID,              
   COVERAGE_CODE_ID,              
   LIMIT_1_TYPE,                    
     LIMIT_2_TYPE,                    
     DEDUCTIBLE_1_TYPE,                    
     DEDUCTIBLE_2_TYPE,                    
     LIMIT_1,                    
     LIMIT_2,                    
     DEDUCTIBLE_1,                    
     DEDUCTIBLE_2,                     
     WRITTEN_PREMIUM,                    
     FULL_TERM_PREMIUM,                  
     LIMIT1_AMOUNT_TEXT,                  
     LIMIT2_AMOUNT_TEXT,                  
     DEDUCTIBLE1_AMOUNT_TEXT,                  
     DEDUCTIBLE2_AMOUNT_TEXT,
     LIMIT_ID,  
     DEDUC_ID                        
  )              
  VALUES              
  (              
   @CUSTOMER_ID,         
   @POL_ID,              
   @POL_VERSION_ID,              
   @VEHICLE_ID,              
   @COVERAGE_ID_MAX,              
   @COVERAGE_CODE_ID,               
  @LIMIT_1_TYPE,                    
     @LIMIT_2_TYPE,                    
  @DEDUCTIBLE_1_TYPE,                    
     @DEDUCTIBLE_2_TYPE,                 
     @LIMIT_1,                    
     @LIMIT_2,                    
     @DEDUCTIBLE_1,                    
     @DEDUCTIBLE_2,                     
     @WRITTEN_PREMIUM,                    
     @FULL_TERM_PREMIUM,                  
     @LIMIT1_AMOUNT_TEXT,                  
     @LIMIT2_AMOUNT_TEXT,     
     @DEDUCTIBLE1_AMOUNT_TEXT,                  
     @DEDUCTIBLE2_AMOUNT_TEXT,
     @LIMIT_ID,  
     @DEDUC_ID                         
  )            
             
  --Insert dependent Endorsements for this coverage  
/*                
 DECLARE @ENDORSEMENT_ID Int                  
 DECLARE @STATEID SmallInt                          
 DECLARE @LOBID NVarCHar(5)                          
 DECLARE @VEHICLE_ENDORSEMENT_ID int   
 DECLARE @APP_EFFECTIVE_DATE DATETIME                         
 --N for New Business, R for renewal                        
 --DECLARE @APP_TYPE Char(1)                        
                         
 --SET @APP_TYPE = 'N'                        
       
 SELECT @STATEID = STATE_ID,                          
  @LOBID = POLICY_LOB ,@APP_EFFECTIVE_DATE = APP_EFFECTIVE_DATE     
 FROM POL_CUSTOMER_POLICY_LIST       
 WHERE CUSTOMER_ID = @CUSTOMER_ID AND                          
  POLICY_ID = @POL_ID AND                          
  POLICY_VERSION_ID = @POL_VERSION_ID                          
         
SET @LOBID = 4        
                  
 SELECT  @ENDORSEMENT_ID = ENDORSMENT_ID                  
 FROM MNT_ENDORSMENT_DETAILS MED                  
 WHERE SELECT_COVERAGE = @COVERAGE_CODE_ID                  
 AND STATE_ID = @STATEID AND                  
  LOB_ID = @LOBID                  
and  ENDORS_ASSOC_COVERAGE='Y'                   
--print(@STATEID)                
--print(@LOBID)                
                
--print (@COVERAGE_CODE_ID)                   
--print(ISNULL(@ENDORSEMENT_ID,0))                
                
 IF ( @ENDORSEMENT_ID IS NOT NULL )                  
 BEGIN   
   DECLARE @EDITION_DATE   VARCHAR(10)                  
  SELECT @VEHICLE_ENDORSEMENT_ID = ISNULL(MAX(VEHICLE_ENDORSEMENT_ID),0) + 1                  
  FROM POL_WATERCRAFT_ENDORSEMENTS                  
  WHERE CUSTOMER_ID = @CUSTOMER_ID AND                          
    POLICY_ID = @POL_ID AND                          
    POLICY_VERSION_ID = @POL_VERSION_ID AND                  
    BOAT_ID = @VEHICLE_ID                   
   -----
--BY PRAVESH FOR DEFAULT EDITION DATE            
   SELECT    @EDITION_DATE =ENDORSEMENT_ATTACH_ID  FROM MNT_ENDORSEMENT_ATTACHMENT WHERE ENDORSEMENT_ID=@ENDORSEMENT_ID AND 
   @APP_EFFECTIVE_DATE BETWEEN VALID_DATE AND ISNULL(EFFECTIVE_TO_DATE,'3000-12-12') AND  @APP_EFFECTIVE_DATE<=ISNULL(DISABLED_DATE,'3000-12-12') 
--
IF  NOT EXISTS(SELECT ENDORSEMENT_ID FROM POL_WATERCRAFT_ENDORSEMENTS 
		WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID =@POL_ID 
		AND POLICY_VERSION_ID=@POL_VERSION_ID AND BOAT_ID=@VEHICLE_ID and ENDORSEMENT_ID=@ENDORSEMENT_ID)
	BEGIN
               
	  INSERT INTO POL_WATERCRAFT_ENDORSEMENTS                  
	  (                  
	   CUSTOMER_ID,                  
	   POLICY_ID,                  
	   POLICY_VERSION_ID,                  
	   BOAT_ID,                  
	   ENDORSEMENT_ID,                  
	   VEHICLE_ENDORSEMENT_ID ,
	   EDITION_DATE                  
	  )                  
	  VALUES                  
	  (                  
	   @CUSTOMER_ID,                  
	   @POL_ID,                  
	   @POL_VERSION_ID,                  
	   @VEHICLE_ID,                  
	   @ENDORSEMENT_ID,                  
	   @VEHICLE_ENDORSEMENT_ID ,
	   @EDITION_DATE                  
	  )                  
  END                
 END   */ 
  exec Proc_UPDATE_WATERCRAFT_LINKED_ENDORSEMENTS_POLICY  @CUSTOMER_ID  ,@POL_ID, @POL_VERSION_ID, @VEHICLE_ID, @COVERAGE_ID_MAX,  @COVERAGE_CODE_ID                           
              
 RETURN 1              
              
 END               
               
 --Update              
 UPDATE POL_WATERCRAFT_COVERAGE_INFO              
 SET              
  LIMIT_1_TYPE = @LIMIT_1_TYPE,                    
  LIMIT_2_TYPE = @LIMIT_2_TYPE,                    
  LIMIT_1 = @LIMIT_1,                    
  LIMIT_2 = @LIMIT_2,                    
  DEDUCTIBLE_1_TYPE = @DEDUCTIBLE_1_TYPE,                    
  DEDUCTIBLE_2_TYPE = @DEDUCTIBLE_2_TYPE,                    
  DEDUCTIBLE_1 = @DEDUCTIBLE_1,                    
  DEDUCTIBLE_2 = @DEDUCTIBLE_2,                    
  WRITTEN_PREMIUM = @WRITTEN_PREMIUM,                     
  FULL_TERM_PREMIUM = @FULL_TERM_PREMIUM,                  
  LIMIT1_AMOUNT_TEXT = @LIMIT1_AMOUNT_TEXT,                  
    LIMIT2_AMOUNT_TEXT = @LIMIT2_AMOUNT_TEXT,                  
    DEDUCTIBLE1_AMOUNT_TEXT = @DEDUCTIBLE1_AMOUNT_TEXT,                  
    DEDUCTIBLE2_AMOUNT_TEXT = @DEDUCTIBLE2_AMOUNT_TEXT,
    LIMIT_ID = @LIMIT_ID,  
    DEDUC_ID = @DEDUC_ID                   
 WHERE CUSTOMER_ID = @CUSTOMER_ID AND              
  POLICY_ID = @POL_ID AND              
  POLICY_VERSION_ID = @POL_VERSION_ID AND              
  COVERAGE_CODE_ID = @COVERAGE_CODE_ID  AND    
  BOAT_ID = @VEHICLE_ID          

  --Update POL_CUSTOMER_POLICY_LIST Table   for all_data_valid
  
	UPDATE POL_CUSTOMER_POLICY_LIST SET ALL_DATA_VALID =0
	WHERE CUSTOMER_ID=@CUSTOMER_ID 
	AND POLICY_ID=@POL_ID 
	AND POLICY_VERSION_ID=@POL_VERSION_ID  

/*by pravesh to Update  " Unattached Equipment" And Personal Effects Coverage (unscheduled) - Actual Cash Basis" 
 for All Boat */ 
	if ( @COVERAGE_CODE_ID=26 or @COVERAGE_CODE_ID=71 or @COVERAGE_CODE_ID=823 )
	 begin  
		update POL_WATERCRAFT_COVERAGE_INFO  set  Limit_1 = @LIMIT_1,LIMIT_1_TYPE = @LIMIT_1_TYPE, LIMIT_ID = @LIMIT_ID 
		where  CUSTOMER_ID = @CUSTOMER_ID AND                    
		 POLICY_ID = @POL_ID AND              
		  POLICY_VERSION_ID = @POL_VERSION_ID AND              
		  COVERAGE_CODE_ID = @COVERAGE_CODE_ID   
		--for coverage_code_id in (26,71,823)set Limit_1 
	end	
   exec Proc_UPDATE_WATERCRAFT_LINKED_ENDORSEMENTS_POLICY  @CUSTOMER_ID  ,@POL_ID, @POL_VERSION_ID, @VEHICLE_ID, @COVERAGE_ID_MAX,  @COVERAGE_CODE_ID                           
                      
                 
END            
            
  

      
    
  














GO

