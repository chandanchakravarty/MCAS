IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_Save_WATERCRAFT_COVERAGES]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_Save_WATERCRAFT_COVERAGES]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


--drop proc dbo.Proc_Save_WATERCRAFT_COVERAGES        
        
/*----------------------------------------------------------                    
Proc Name       : dbo.Proc_Save_WATERCRAFT_COVERAGES                    
Created by      : Pradeep                    
Date            : 10/21/2005                    
Purpose      : Saves records in Watercraft Coverages and inserts                  
    dependent endorsements in APP_WATERCRAFT_ENDORSEMENTS                    
Revison History :                    
Modified by      : Pravesh                    
Date            : 26 oct-2006                    
Purpose      : Saves created by,createddatetime and modify by,last modify datetime columns
Modified by      : Pravesh                    
Date            : 5 Jan-2007                    
Purpose      : to Update Unattached Equipment" And Personal Effects Coverage (unscheduled) - Actual Cash Basis  coverage for all Boats
Modified by      : Pravesh k CHANDEL                   
Date            : 31 jULY-2007                    
Purpose      : to Update LINKED ENDORSEMENTS

Used In  : Wolverine                    
------------------------------------------------------------                    
Date     Review By          Comments                    
------   ------------       -------------------------*/                    
CREATE           PROC dbo.Proc_Save_WATERCRAFT_COVERAGES                    
(                    
 @CUSTOMER_ID     int,                    
 @APP_ID     int,                    
 @APP_VERSION_ID     smallint,                    
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
 @DEDUC_ID Int = NULL,
 @CREATED_BY int=null,
 @CREATED_DATETIME datetime=null,
 @MODIFIED_BY int= null,
 @LAST_UPDATED_DATETIME datetime=null                

)                    
AS                    
                    
DECLARE @COVERAGE_ID_MAX smallint                    
BEGIN                   
    
       
    
 IF NOT EXISTS            
(            
 SELECT * FROM APP_WATERCRAFT_COVERAGE_INFO            
WHERE CUSTOMER_ID = @CUSTOMER_ID and                     
   APP_ID=@APP_ID and                     
   APP_VERSION_ID = @APP_VERSION_ID                     
   and BOAT_ID = @VEHICLE_ID AND            
   COVERAGE_CODE_ID =  @COVERAGE_CODE_ID                    
)                    
 BEGIN                    
                      
  SELECT  @COVERAGE_ID_MAX = isnull(Max(COVERAGE_ID),0)+1                     
  FROM APP_WATERCRAFT_COVERAGE_INFO                    
  where CUSTOMER_ID = @CUSTOMER_ID and                     
   APP_ID=@APP_ID and                     
   APP_VERSION_ID = @APP_VERSION_ID                     
   and BOAT_ID = @VEHICLE_ID                    
                    
  INSERT INTO APP_WATERCRAFT_COVERAGE_INFO      
  (                    
   CUSTOMER_ID,                    
   APP_ID,                    
   APP_VERSION_ID,          
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
     DEDUC_ID,         
     CREATED_BY,
     CREATED_DATETIME
                            
  )                    
  VALUES                    
  (                    
   @CUSTOMER_ID,                    
   @APP_ID,            
   @APP_VERSION_ID,                    
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
     @DEDUC_ID,
     @CREATED_BY,
    GETDATE()
                             
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
  @LOBID = APP_LOB,
 @APP_EFFECTIVE_DATE=APP_EFFECTIVE_DATE                                
 FROM APP_LIST                                
 WHERE CUSTOMER_ID = @CUSTOMER_ID AND                                
  APP_ID = @APP_ID AND                                
  APP_VERSION_ID = @APP_VERSION_ID                                
               
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
  FROM APP_WATERCRAFT_ENDORSEMENTS                        
  WHERE CUSTOMER_ID = @CUSTOMER_ID AND                                
    APP_ID = @APP_ID AND                                
    APP_VERSION_ID = @APP_VERSION_ID AND                        
    BOAT_ID = @VEHICLE_ID                         
-----
--BY PRAVESH FOR DEFAULT EDITION DATE            
   SELECT    @EDITION_DATE =ENDORSEMENT_ATTACH_ID  FROM MNT_ENDORSEMENT_ATTACHMENT WHERE ENDORSEMENT_ID=@ENDORSEMENT_ID AND 
   @APP_EFFECTIVE_DATE BETWEEN VALID_DATE AND ISNULL(EFFECTIVE_TO_DATE,'3000-12-12') AND  @APP_EFFECTIVE_DATE<=ISNULL(DISABLED_DATE,'3000-12-12') 
--
  INSERT INTO APP_WATERCRAFT_ENDORSEMENTS                        
  (                        
   CUSTOMER_ID,                        
   APP_ID,                
   APP_VERSION_ID,                        
   BOAT_ID,                       
   ENDORSEMENT_ID,                        
   VEHICLE_ENDORSEMENT_ID,
   EDITION_DATE                        
  )                        
  VALUES                    
  (                        
   @CUSTOMER_ID,                        
   @APP_ID,                        
   @APP_VERSION_ID,                        
   @VEHICLE_ID,                        
   @ENDORSEMENT_ID,                        
   @VEHICLE_ENDORSEMENT_ID ,                       
   @EDITION_DATE
  )                        
                        
 END       */                               
 --UPDATING LINKED ENDORSEMENTS
EXEC  Proc_UPDATE_WATERCRAFT_LINKED_ENDORSEMENTS  @CUSTOMER_ID, @APP_ID  ,@APP_VERSION_ID  ,@VEHICLE_ID ,@COVERAGE_ID   ,@COVERAGE_CODE_ID        
                   
 RETURN 1  
                    
 END                     
                     
 --Update                    
 UPDATE APP_WATERCRAFT_COVERAGE_INFO                    
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
    DEDUCTIBLE2_AMOUNT_TEXT = @DEDUCTIBLE2_AMOUNT_TEXT ,        
   LIMIT_ID = @LIMIT_ID,        
   DEDUC_ID = @DEDUC_ID,                  
   MODIFIED_BY=@MODIFIED_BY,
   LAST_UPDATED_DATETIME= GETDATE()
 
WHERE CUSTOMER_ID = @CUSTOMER_ID AND                    
  APP_ID = @APP_ID AND                    
  APP_VERSION_ID = @APP_VERSION_ID AND                    
  COVERAGE_CODE_ID = @COVERAGE_CODE_ID  AND          
  BOAT_ID = @VEHICLE_ID  
                
/*by pravesh to Update  " Unattached Equipment" And Personal Effects Coverage (unscheduled) - Actual Cash Basis" 
 for All Boat */ 
	if ( @COVERAGE_CODE_ID=26 or @COVERAGE_CODE_ID=71 or @COVERAGE_CODE_ID=823 )
	 begin  
		update APP_WATERCRAFT_COVERAGE_INFO set  Limit_1 = @LIMIT_1,LIMIT_1_TYPE = @LIMIT_1_TYPE, LIMIT_ID = @LIMIT_ID 
		where  CUSTOMER_ID = @CUSTOMER_ID AND                    
		  APP_ID = @APP_ID AND                    
		  APP_VERSION_ID = @APP_VERSION_ID AND                    
		  COVERAGE_CODE_ID = @COVERAGE_CODE_ID            
		--for coverage_code_id in (26,71,823)set Limit_1 
	end
--UPDATING LINKED ENDORSEMENTS
EXEC  Proc_UPDATE_WATERCRAFT_LINKED_ENDORSEMENTS  @CUSTOMER_ID, @APP_ID  ,@APP_VERSION_ID  ,@VEHICLE_ID ,@COVERAGE_ID   ,@COVERAGE_CODE_ID        
END                  













GO

