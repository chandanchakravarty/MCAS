IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UPDATE_DWELLING_ENDORSEMENTS_FOR_POLICY]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UPDATE_DWELLING_ENDORSEMENTS_FOR_POLICY]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                        
  
Proc Name       : dbo.Proc_UPDATE_VEHICLE_ENDORSEMENTS                        
Created by      : Pradeep                        
Date            : 12/23/2005                        
Purpose      :Inserts linked endorsemnts for this coverage                      
Revison History :   
MODIFIED by      : Pravesh K Chandel                              
Date            : 31 July 2007                                
Purpose      :   UPDATE linked endorsemEnts for IF INCREASED LIMIT ISAPPLICABLE                           
Used In  : Wolverine                        
------------------------------------------------------------                        
Date     Review By          Comments                        
------   ------------       -------------------------
drop proc Proc_UPDATE_DWELLING_ENDORSEMENTS_FOR_POLICY
*/    
			                     
CREATE           PROC Dbo.Proc_UPDATE_DWELLING_ENDORSEMENTS_FOR_POLICY    
(                        
 @CUSTOMER_ID     int,                        
 @POLICY_ID     int,                        
 @POLICY_VERSION_ID     smallint,                        
 @DWELLING_ID smallint,                        
 @COVERAGE_ID int,                        
 @COVERAGE_CODE_ID int    
)     
    
AS     
    
BEGIN    
     
  DECLARE @STATEID SmallInt                            
  DECLARE @LOBID NVarCHar(5)         
        
  SELECT @STATEID = STATE_ID,                            
  @LOBID = POLICY_LOB                            
  FROM POL_CUSTOMER_POLICY_LIST
  WHERE CUSTOMER_ID = @CUSTOMER_ID AND                            
    POLICY_ID = @POLICY_ID AND                            
    POLICY_VERSION_ID = @POLICY_VERSION_ID       
    
    
  DECLARE @ENDORSEMENT_ID Int                    
                         
  DECLARE @DWELLING_ENDORSEMENT_ID int   
   -------------
DECLARE @INCREASED_LIMIT NUMERIC
DECLARE @INCREASED_LIMIT_ID INT
DECLARE @INCREASED_LIMIT_EXIST CHAR(1)
SELECT @INCREASED_LIMIT=ISNULL(DEDUCTIBLE_1,0),@INCREASED_LIMIT_ID=ISNULL(DEDUC_ID,0)
	 FROM POL_DWELLING_SECTION_COVERAGES  with(nolock)        
	  where CUSTOMER_ID = @CUSTOMER_ID and           
	  POLICY_ID=@POLICY_ID and           
	   POLICY_VERSION_ID = @POLICY_VERSION_ID         
	   and DWELLING_ID = @DWELLING_ID AND          
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
	EXEC Proc_Delete_HOME_ENDORSEMENT_BY_ID_FOR_POLICY  
				@CUSTOMER_ID,           
				@POLICY_ID,
				@POLICY_VERSION_ID,
				@ENDORSEMENT_ID,
				@DWELLING_ID
	end
  ELSE
	SET @INSERT_FLAG='Y'	            
                  
          --Linked endorsements---------------------------------------------------------------------------------    
  IF ( @ENDORSEMENT_ID IS NOT NULL AND @INSERT_FLAG='Y' )                    
  BEGIN        
    IF NOT EXISTS            
    (            
    SELECT * FROM POL_DWELLING_ENDORSEMENTS            
    WHERE CUSTOMER_ID = @CUSTOMER_ID AND                            
    POLICY_ID = @POLICY_ID AND                            
    POLICY_VERSION_ID = @POLICY_VERSION_ID AND                    
    DWELLING_ID = @DWELLING_ID  AND            
    ENDORSEMENT_ID =  @ENDORSEMENT_ID                   
    )            
   BEGIN          
    SELECT @DWELLING_ENDORSEMENT_ID = ISNULL(MAX(DWELLING_ENDORSEMENT_ID),0) + 1                    
     FROM POL_DWELLING_ENDORSEMENTS                    
     WHERE CUSTOMER_ID = @CUSTOMER_ID AND                            
       POLICY_ID = @POLICY_ID AND                            
       POLICY_VERSION_ID = @POLICY_VERSION_ID AND                    
       DWELLING_ID = @DWELLING_ID               
                
     INSERT INTO POL_DWELLING_ENDORSEMENTS                    
     (                    
       CUSTOMER_ID,                    
       POLICY_ID,                    
       POLICY_VERSION_ID,                    
       DWELLING_ID,                    
       ENDORSEMENT_ID,                    
       DWELLING_ENDORSEMENT_ID                    
     )                    
VALUES                    
     (                    
       @CUSTOMER_ID,                    
       @POLICY_ID,                    
       @POLICY_VERSION_ID,                    
       @DWELLING_ID,                    
       @ENDORSEMENT_ID,                    
       @DWELLING_ENDORSEMENT_ID               
     )                    
      END            
      --End of linked endorsements ------------------    
    
 END    
    
    
        
END                       
  







GO

