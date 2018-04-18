IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UPDATE_LINKED_HOME_ENDORSEMENTS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UPDATE_LINKED_HOME_ENDORSEMENTS]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                        
Proc Name       : dbo.Proc_UPDATE_LINKED_HOME_ENDORSEMENTS                        
Created by      : Pradeep                        
Date            : 01/05/2006                        
Purpose      :Inserts linked endorsemnts for this coverage                      
Revison History :                        
Used In  : Wolverine                        
------------------------------------------------------------                        
Date     Review By          Comments                        
------   ------------       -------------------------
drop proc dbo.Proc_UPDATE_LINKED_HOME_ENDORSEMENTS  
*/                        
CREATE           PROC dbo.Proc_UPDATE_LINKED_HOME_ENDORSEMENTS    
(                        
 @CUSTOMER_ID     int,                        
 @APP_ID     int,                        
 @APP_VERSION_ID     smallint,                        
 @DWELLING_ID smallint,                        
 @COVERAGE_ID int,                        
 @COVERAGE_CODE_ID int  
)     
    
AS     
    
BEGIN    
     
  DECLARE @STATEID SmallInt                            
  DECLARE @LOBID NVarCHar(5)         
  DECLARE @EDITION_DATE varchar(10)
DECLARE @APP_EFFECTIVE_DATE datetime       
    SELECT @STATEID = STATE_ID,                            
      @LOBID = APP_LOB ,
@APP_EFFECTIVE_DATE= APP_EFFECTIVE_DATE                          
  FROM APP_LIST                            
  WHERE CUSTOMER_ID = @CUSTOMER_ID AND                            
    APP_ID = @APP_ID AND                            
    APP_VERSION_ID = @APP_VERSION_ID       
    
    
  DECLARE @ENDORSEMENT_ID Int                    
                         
  DECLARE @DWELLING_ENDORSEMENT_ID int                          
     
  SELECT  @ENDORSEMENT_ID = ENDORSMENT_ID                    
  FROM MNT_ENDORSMENT_DETAILS MED                    
  WHERE SELECT_COVERAGE = @COVERAGE_CODE_ID                    
  AND STATE_ID = @STATEID AND                    
   LOB_ID = @LOBID AND        
   ENDORS_ASSOC_COVERAGE = 'Y' AND        
   IS_ACTIVE='Y'                  
                  
 --print(@STATEID)                  
 --print(@LOBID)                  
                   
 --print (@COVERAGE_CODE_ID)                     
 --print(ISNULL(@ENDORSEMENT_ID,0))                  
                  
  --Linked endorsements---------------------------------------------------------------------------------    
  IF ( @ENDORSEMENT_ID IS NOT NULL )                    
  BEGIN        
    IF NOT EXISTS            
    (            
    SELECT * FROM APP_DWELLING_ENDORSEMENTS            
    WHERE CUSTOMER_ID = @CUSTOMER_ID AND                            
     APP_ID = @APP_ID AND                            
     APP_VERSION_ID = @APP_VERSION_ID AND                    
     DWELLING_ID = @DWELLING_ID  AND            
     ENDORSEMENT_ID =  @ENDORSEMENT_ID                   
    )            
   BEGIN          
    SELECT @DWELLING_ENDORSEMENT_ID = ISNULL(MAX(DWELLING_ENDORSEMENT_ID),0) + 1                    
     FROM APP_DWELLING_ENDORSEMENTS                    
     WHERE CUSTOMER_ID = @CUSTOMER_ID AND                            
       APP_ID = @APP_ID AND                            
       APP_VERSION_ID = @APP_VERSION_ID AND                    
       DWELLING_ID = @DWELLING_ID               
             --BY PRAVESH FOR DEFAULT EDITION DATE                      
   SELECT    @EDITION_DATE =ENDORSEMENT_ATTACH_ID  FROM MNT_ENDORSEMENT_ATTACHMENT WHERE ENDORSEMENT_ID=@ENDORSEMENT_ID AND           
   @APP_EFFECTIVE_DATE BETWEEN VALID_DATE AND ISNULL(EFFECTIVE_TO_DATE,'3000-12-12') AND  @APP_EFFECTIVE_DATE<=ISNULL(DISABLED_DATE,'3000-12-12')           
--          
        
     INSERT INTO APP_DWELLING_ENDORSEMENTS                    
     (                    
       CUSTOMER_ID,                    
       APP_ID,                    
       APP_VERSION_ID,                    
       DWELLING_ID,                    
       ENDORSEMENT_ID,                    
       DWELLING_ENDORSEMENT_ID,
        EDITION_DATE                   
     )                    
     VALUES                    
     (                    
       @CUSTOMER_ID,                    
       @APP_ID,                    
       @APP_VERSION_ID,                    
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

