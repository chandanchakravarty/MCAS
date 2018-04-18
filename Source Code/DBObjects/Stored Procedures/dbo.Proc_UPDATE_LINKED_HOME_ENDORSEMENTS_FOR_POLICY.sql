IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UPDATE_LINKED_HOME_ENDORSEMENTS_FOR_POLICY]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UPDATE_LINKED_HOME_ENDORSEMENTS_FOR_POLICY]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                        
Proc Name       : dbo.Proc_UPDATE_LINKED_HOME_ENDORSEMENTS_FOR_POLICY                        
Created by      :SHAFI                        
Date            : 16/02/06                       
Purpose      :Inserts linked endorsemnts for this coverage                      
Revison History :                        
Used In  : Wolverine                        
------------------------------------------------------------                        
Date     Review By          Comments                        
------   ------------       -------------------------*/                        
CREATE           PROC Dbo.Proc_UPDATE_LINKED_HOME_ENDORSEMENTS_FOR_POLICY   
(                        
 @CUSTOMER_ID     int,                        
 @POL_ID     int,                        
 @POL_VERSION_ID     smallint,                        
 @DWELLING_ID smallint,                        
 @COVERAGE_ID int,                        
 @COVERAGE_CODE_ID int  
)     
    
AS     
    
BEGIN    
     
  DECLARE @STATEID SmallInt                            
  DECLARE @LOBID NVarCHar(5)         
        
 SELECT   @LOBID = POLICY_LOB,  
 @STATEID = STATE_ID                    
 FROM POL_CUSTOMER_POLICY_LIST WHERE                    
 CUSTOMER_ID = @CUSTOMER_ID AND                    
 POLICY_ID = @POL_ID AND                    
 POLICY_VERSION_ID = @POL_VERSION_ID       
    
    
  DECLARE @ENDORSEMENT_ID Int                    
                         
  DECLARE @DWELLING_ENDORSEMENT_ID int                          
     
  SELECT  @ENDORSEMENT_ID = ENDORSMENT_ID                    
  FROM MNT_ENDORSMENT_DETAILS MED                    
  WHERE SELECT_COVERAGE = @COVERAGE_CODE_ID                    
  AND STATE_ID = @STATEID AND                    
   LOB_ID = @LOBID AND        
   ENDORS_ASSOC_COVERAGE = 'Y' AND        
   IS_ACTIVE='Y'                  
                  
         
                  
  --Linked endorsements---------------------------------------------------------------------------------    
  IF ( @ENDORSEMENT_ID IS NOT NULL )                    
  BEGIN        
    IF NOT EXISTS            
    (            
    SELECT * FROM POL_DWELLING_ENDORSEMENTS            
    WHERE CUSTOMER_ID = @CUSTOMER_ID AND                            
     POLICY_ID = @POL_ID AND                            
     POLICY_VERSION_ID = @POL_VERSION_ID AND                    
     DWELLING_ID = @DWELLING_ID  AND            
     ENDORSEMENT_ID =  @ENDORSEMENT_ID                   
    )            
   BEGIN          
    SELECT @DWELLING_ENDORSEMENT_ID = ISNULL(MAX(DWELLING_ENDORSEMENT_ID),0) + 1                    
     FROM POL_DWELLING_ENDORSEMENTS                    
     WHERE CUSTOMER_ID = @CUSTOMER_ID AND                            
       POLICY_ID = @POL_ID AND                            
       POLICY_VERSION_ID = @POL_VERSION_ID AND                    
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
       @POL_ID,                    
       @POL_VERSION_ID,                    
       @DWELLING_ID,                    
       @ENDORSEMENT_ID,                    
       @DWELLING_ENDORSEMENT_ID                    
     )                    
      END            
      --End of linked endorsements ------------------    
    
 END    
        
END                       
  



GO

