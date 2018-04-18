IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UPDATE_DWELLING_ENDORSEMENTSFORPOLICY]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UPDATE_DWELLING_ENDORSEMENTSFORPOLICY]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                              
        
Proc Name       : dbo.Dbo.Proc_UPDATE_DWELLING_ENDORSEMENTSFORPOLICY    
Created by      : SHAFI                              
Date            : 20/02/06    
Purpose      :Inserts linked endorsemnts for this coverage                            
Revison History :                              
Used In  : Wolverine                              
------------------------------------------------------------                              
Date     Review By          Comments                              
------   ------------       -------------------------*/    
--DROP PROC dbo.Proc_UPDATE_DWELLING_ENDORSEMENTSFORPOLICY                            
CREATE           PROC dbo.Proc_UPDATE_DWELLING_ENDORSEMENTSFORPOLICY          
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
    declare @APP_EFFECTIVE_DATE datetime    
         
   SELECT @STATEID = STATE_ID,                                  
    @LOBID = POLICY_LOB ,                                 
   @APP_EFFECTIVE_DATE= APP_EFFECTIVE_DATE  
  FROM POL_CUSTOMER_POLICY_LIST                                  
  WHERE CUSTOMER_ID = @CUSTOMER_ID AND                                  
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
    declare   @EDITION_DATE varchar(10)            
    SELECT @DWELLING_ENDORSEMENT_ID = ISNULL(MAX(DWELLING_ENDORSEMENT_ID),0) + 1                          
     FROM POL_DWELLING_ENDORSEMENTS                          
     WHERE CUSTOMER_ID = @CUSTOMER_ID AND                                  
       POLICY_ID = @POL_ID AND                                  
       POLICY_VERSION_ID = @POL_VERSION_ID AND                          
       DWELLING_ID = @DWELLING_ID                     
       --BY PRAVESH FOR DEFAULT EDITION DATE                  
   SELECT    @EDITION_DATE =ENDORSEMENT_ATTACH_ID  FROM MNT_ENDORSEMENT_ATTACHMENT WHERE ENDORSEMENT_ID=@ENDORSEMENT_ID AND       
   @APP_EFFECTIVE_DATE BETWEEN VALID_DATE AND ISNULL(EFFECTIVE_TO_DATE,'3000-12-12') AND  @APP_EFFECTIVE_DATE<=ISNULL(DISABLED_DATE,'3000-12-12')       
--               
     INSERT INTO POL_DWELLING_ENDORSEMENTS                          
     (                          
       CUSTOMER_ID,                          
       POLICY_ID,                          
       POLICY_VERSION_ID,                          
       DWELLING_ID,                          
       ENDORSEMENT_ID,                          
       DWELLING_ENDORSEMENT_ID ,
       EDITION_DATE                         
     )                          
     VALUES                          
     (                          
       @CUSTOMER_ID,                          
       @POL_ID,                         
       @POL_VERSION_ID,                          
       @DWELLING_ID,                          
       @ENDORSEMENT_ID,                          
       @DWELLING_ENDORSEMENT_ID ,
       @EDITION_DATE                    
     )                          
      END                  
      --End of linked endorsements ------------------          
          
 END          
          
          
              
END                             
        
      
    
  



GO

