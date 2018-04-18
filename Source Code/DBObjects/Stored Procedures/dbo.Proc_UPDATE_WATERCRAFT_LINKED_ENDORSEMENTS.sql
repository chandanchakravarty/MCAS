IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UPDATE_WATERCRAFT_LINKED_ENDORSEMENTS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UPDATE_WATERCRAFT_LINKED_ENDORSEMENTS]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                                
Proc Name       : dbo.Proc_UPDATE_WATERCRAFT_LINKED_ENDORSEMENTS                                
Created by      : Pravesh K Chandel                              
Date            : 31 July 2007                                
Purpose      :   Inserts linked endorsemnts for Watercraft coverages                              
Revison History :      
Used In  : Wolverine                                
------------------------------------------------------------                                
Date     Review By          Comments                                
------   ------------       -------------------------
drop proc dbo.Proc_UPDATE_WATERCRAFT_LINKED_ENDORSEMENTS
*/ 
			                     
CREATE           PROC dbo.Proc_UPDATE_WATERCRAFT_LINKED_ENDORSEMENTS   
(                                
 @CUSTOMER_ID     int,                                
 @APP_ID     int,                                
 @APP_VERSION_ID     smallint,                                
 @VEHICLE_ID smallint,                                
 @COVERAGE_ID int,                                
 @COVERAGE_CODE_ID int          
)             
            
AS             
            
BEGIN            
             
  DECLARE @STATEID SmallInt                                    
  DECLARE @LOBID NVarCHar(5)                 

  DECLARE @ENDORSEMENT_ID Int                                                     
  DECLARE @VEHICLE_ENDORSEMENT_ID int   
  DECLARE @EDITION_DATE VARCHAR(10)
  DECLARE @APP_EFFECTIVE_DATE DATETIME

  --------
  --Insert dependent Endorsements for this coverage                        
 SELECT @STATEID = STATE_ID,                                
  @LOBID = APP_LOB,
 @APP_EFFECTIVE_DATE=APP_EFFECTIVE_DATE                                
 FROM APP_LIST                                
 WHERE CUSTOMER_ID = @CUSTOMER_ID AND                                
  APP_ID = @APP_ID AND                                
  APP_VERSION_ID = @APP_VERSION_ID                                
               
SET @LOBID = 4              
 -------------
DECLARE @INCREASED_LIMIT NUMERIC
DECLARE @INCREASED_LIMIT_ID INT
DECLARE @INCREASED_LIMIT_EXIST CHAR(1)
SELECT @INCREASED_LIMIT=ISNULL(DEDUCTIBLE_1,0),@INCREASED_LIMIT_ID=ISNULL(DEDUC_ID,0)
	 FROM APP_WATERCRAFT_COVERAGE_INFO  with(nolock)        
	  where CUSTOMER_ID = @CUSTOMER_ID and           
	   APP_ID=@APP_ID and           
	   APP_VERSION_ID = @APP_VERSION_ID           
	   and BOAT_ID = @VEHICLE_ID AND          
	   COVERAGE_CODE_ID = @COVERAGE_CODE_ID 
IF (@INCREASED_LIMIT<>0 OR @INCREASED_LIMIT_ID<>0)
	SET  @INCREASED_LIMIT_EXIST ='Y'
ELSE
	SET  @INCREASED_LIMIT_EXIST ='N'
  --eND HERE
DECLARE @INCREASED_LIMIT_TEMP  INT      
DECLARE @INSERT_FLAG CHAR(1)
  --Get the endorsementst associated with  this coverage if any           
  SELECT  @ENDORSEMENT_ID = ENDORSMENT_ID ,@INCREASED_LIMIT_TEMP=ISNULL(INCREASED_LIMIT,0)
  FROM MNT_ENDORSMENT_DETAILS MED                            
  WHERE SELECT_COVERAGE = @COVERAGE_CODE_ID                            
  AND STATE_ID = @STATEID AND                            
   LOB_ID = @LOBID AND                
   ENDORS_ASSOC_COVERAGE = 'Y' AND                
   IS_ACTIVE='Y' 

  IF (@INCREASED_LIMIT_TEMP=10963 AND @INCREASED_LIMIT_EXIST='N')                
	begin
	SET @INSERT_FLAG='N'	                          
	EXEC Proc_Delete_APP_WATERCRAFT_ENDORSEMENT_BY_ID 
				 @CUSTOMER_ID,      
				@APP_ID,   
				@APP_VERSION_ID,           
				@ENDORSEMENT_ID , 
				@VEHICLE_ID
	end
  ELSE
	SET @INSERT_FLAG='Y'	

 IF ( @ENDORSEMENT_ID IS NOT NULL AND @INSERT_FLAG='Y')                        
 BEGIN  

 IF NOT EXISTS                    
    (                    
    SELECT * FROM APP_WATERCRAFT_ENDORSEMENTS   with(nolock)                  
    WHERE CUSTOMER_ID = @CUSTOMER_ID AND              
    APP_ID = @APP_ID AND                                    
    APP_VERSION_ID = @APP_VERSION_ID AND                            
    BOAT_ID = @VEHICLE_ID  AND                    
    ENDORSEMENT_ID =  @ENDORSEMENT_ID                           
    )         
 BEGIN             
  SELECT @VEHICLE_ENDORSEMENT_ID = ISNULL(MAX(VEHICLE_ENDORSEMENT_ID),0) + 1                        
  FROM APP_WATERCRAFT_ENDORSEMENTS   with(nolock)                      
  WHERE CUSTOMER_ID = @CUSTOMER_ID AND                                
    APP_ID = @APP_ID AND                                
    APP_VERSION_ID = @APP_VERSION_ID AND                        
    BOAT_ID = @VEHICLE_ID                         
-----
--BY PRAVESH FOR DEFAULT EDITION DATE            
   SELECT    @EDITION_DATE =ENDORSEMENT_ATTACH_ID  FROM MNT_ENDORSEMENT_ATTACHMENT with(nolock)  WHERE ENDORSEMENT_ID=@ENDORSEMENT_ID AND 
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

 END
                       
END                    
                    
                  
                    
 END                     
--
                           













GO

