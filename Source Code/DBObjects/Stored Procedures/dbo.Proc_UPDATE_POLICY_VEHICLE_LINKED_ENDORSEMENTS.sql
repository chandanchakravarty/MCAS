IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UPDATE_POLICY_VEHICLE_LINKED_ENDORSEMENTS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UPDATE_POLICY_VEHICLE_LINKED_ENDORSEMENTS]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


--drop proc dbo.Proc_UPDATE_POLICY_VEHICLE_LINKED_ENDORSEMENTS              
              
/*----------------------------------------------------------                                    
Proc Name       : dbo.Proc_UPDATE_POLICY_VEHICLE_LINKED_ENDORSEMENTS                                    
Created by      : Pradeep                                    
Date            : 02/06/2005                                    
Purpose      :Inserts linked endorsemnts for this coverage                                  
Revison History :                                    
Used In  : Wolverine                                    
------------------------------------------------------------                                    
Date     Review By          Comments                                    
------   ------------       -------------------------*/                                    
CREATE           PROC dbo.Proc_UPDATE_POLICY_VEHICLE_LINKED_ENDORSEMENTS                
(                                    
 @CUSTOMER_ID     int,                                    
 @POLICY_ID     int,                                    
 @POLICY_VERSION_ID     smallint,                                    
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
  declare  @APP_EFFECTIVE_DATE datetime                               
                           
                
    SELECT @STATEID = STATE_ID,                                        
    @LOBID = POLICY_LOB,    
   @APP_EFFECTIVE_DATE= APP_EFFECTIVE_DATE  --POLICY_EFFECTIVE_DATE    
  FROM POL_CUSTOMER_POLICY_LIST      
  WHERE CUSTOMER_ID = @CUSTOMER_ID AND                                        
    POLICY_ID = @POLICY_ID AND                                        
    POLICY_VERSION_ID = @POLICY_VERSION_ID                   
 --BY PRAVESH ON 31 JULY UPDATED ENDORSEMENT IF APPLICABLE ON iNCREASED LIMIT          
DECLARE @INCREASED_LIMIT NUMERIC
DECLARE @INCREASED_LIMIT_ID INT
DECLARE @INCREASED_LIMIT_EXIST CHAR(1)
SELECT @INCREASED_LIMIT=ISNULL(DEDUCTIBLE_1,0),@INCREASED_LIMIT_ID=ISNULL(DEDUC_ID,0)
	 FROM POL_VEHICLE_COVERAGES  with(nolock)        
	  where CUSTOMER_ID = @CUSTOMER_ID and           
	  POLICY_ID = @POLICY_ID AND                                        
	    POLICY_VERSION_ID = @POLICY_VERSION_ID          
	   and VEHICLE_ID = @VEHICLE_ID AND          
	   COVERAGE_CODE_ID = @COVERAGE_CODE_ID 
IF (@INCREASED_LIMIT<>0 OR @INCREASED_LIMIT_ID<>0)
	SET  @INCREASED_LIMIT_EXIST ='Y'
ELSE
	SET  @INCREASED_LIMIT_EXIST ='N'

DECLARE @INCREASED_LIMIT_TEMP  INT      
DECLARE @INSERT_FLAG CHAR(1)       
  --eND HERE          
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
	EXEC Proc_Delete_POL_VEHICLE_ENDORSEMENT_BY_ID_ALL_VEHICLES 
				 @CUSTOMER_ID,      
				@POLICY_ID,   
				@POLICY_VERSION_ID,           
				@ENDORSEMENT_ID

	end
  ELSE
	SET @INSERT_FLAG='Y'	                          
            
                              
  --Linked endorsements---------------------------------------------------------------------------------                
  IF ( @ENDORSEMENT_ID IS NOT NULL and  @INSERT_FLAG='Y' )                                
  BEGIN                    
    IF NOT EXISTS                        
    (                        
    SELECT * FROM POL_VEHICLE_ENDORSEMENTS                        
    WHERE CUSTOMER_ID = @CUSTOMER_ID AND                                        
    POLICY_ID = @POLICY_ID AND                                        
    POLICY_VERSION_ID = @POLICY_VERSION_ID AND                                
    VEHICLE_ID = @VEHICLE_ID  AND                        
    ENDORSEMENT_ID =  @ENDORSEMENT_ID                               
    )                        
   BEGIN    
   declare @EDITION_DATE  varchar(10)                    
    SELECT @VEHICLE_ENDORSEMENT_ID = ISNULL(MAX(VEHICLE_ENDORSEMENT_ID),0) + 1               
     FROM POL_VEHICLE_ENDORSEMENTS                                
     WHERE CUSTOMER_ID = @CUSTOMER_ID AND                      
       POLICY_ID = @POLICY_ID AND                                        
       POLICY_VERSION_ID = @POLICY_VERSION_ID AND                            
       VEHICLE_ID = @VEHICLE_ID                           
                
 --BY PRAVESH FOR DEFAULT EDITION DATE                
   SELECT    @EDITION_DATE =ENDORSEMENT_ATTACH_ID  FROM MNT_ENDORSEMENT_ATTACHMENT WHERE ENDORSEMENT_ID=@ENDORSEMENT_ID AND     
   @APP_EFFECTIVE_DATE BETWEEN VALID_DATE AND ISNULL(EFFECTIVE_TO_DATE,'3000-12-12') AND  @APP_EFFECTIVE_DATE<=ISNULL(DISABLED_DATE,'3000-12-12')     
--    
    
                    
     INSERT INTO POL_VEHICLE_ENDORSEMENTS                                
     (                                
       CUSTOMER_ID,                                
       POLICY_ID,                                
       POLICY_VERSION_ID,                                
       VEHICLE_ID,                                       
 ENDORSEMENT_ID,                             
       VEHICLE_ENDORSEMENT_ID  ,  
      EDITION_DATE                              
     )                                
     VALUES                                
     (                                
       @CUSTOMER_ID,                                
       @POLICY_ID,                                
       @POLICY_VERSION_ID,                                
       @VEHICLE_ID,                                
       @ENDORSEMENT_ID,                                
       @VEHICLE_ENDORSEMENT_ID   ,  
      @EDITION_DATE                             
     )                                
      END                        
      --End of linked endorsements ------------------                
                
 END                
              
            
          
                    
END                                   
              
            
          
        
      
    
  





GO

