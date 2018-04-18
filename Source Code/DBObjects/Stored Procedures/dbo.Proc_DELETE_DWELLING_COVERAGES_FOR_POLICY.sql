IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_DELETE_DWELLING_COVERAGES_FOR_POLICY]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_DELETE_DWELLING_COVERAGES_FOR_POLICY]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                            
Proc Name       : dbo.Proc_DELETE_DWELLING_COVERAGES_FOR_POLICY                            
Created by      : SHAFI                            
Date            : 17/02/06                            
Purpose         :Inserts a record in Dwelling coverages      
Revison History :                            
Used In  : Wolverine                            
------------------------------------------------------------                            
Date     Review By          Comments                            
------   ------------       -------------------------*/                            
CREATE           PROC Dbo.Proc_DELETE_DWELLING_COVERAGES_FOR_POLICY                            
(                            
 @CUSTOMER_ID     int,                            
 @POL_ID     int,                            
 @POL_VERSION_ID     smallint,                            
 @DWELLING_ID smallint,                            
 @COVERAGE_CODE VarChar(10)    
     
)                            
AS                            
                            
    
                     
BEGIN                            
                      
 DECLARE @COV_CODE_ID Int    
 DECLARE @END_ID smallint     
    
 SELECT @COV_CODE_ID = COVERAGE_CODE_ID    
 FROM POL_DWELLING_SECTION_COVERAGES    
 WHERE CUSTOMER_ID = @CUSTOMER_ID AND    
       POLICY_ID =  @POL_ID AND    
       POLICY_VERSION_ID =  @POL_VERSION_ID AND      
  DWELLING_ID =  @DWELLING_ID    
                                
  IF EXISTS                            
  (                            
     SELECT * FROM POL_DWELLING_SECTION_COVERAGES AD    
     where AD.CUSTOMER_ID = @CUSTOMER_ID and                             
      AD.POLICY_ID=@POL_ID and                             
      AD.POLICY_VERSION_ID = @POL_VERSION_ID                             
      and AD.DWELLING_ID = @DWELLING_ID AND                            
   AD.COVERAGE_CODE_ID = @COV_CODE_ID  
    
  )                            
                              
  BEGIN                 
               
     DELETE FROM POL_DWELLING_SECTION_COVERAGES    
     WHERE CUSTOMER_ID = @CUSTOMER_ID and                             
      POLICY_ID=@POL_ID and                             
      POLICY_VERSION_ID = @POL_VERSION_ID                             
      and DWELLING_ID = @DWELLING_ID AND                            
   COVERAGE_CODE_ID = @COV_CODE_ID  
      
   --Delete dependent Endorsements        
       SELECT @END_ID = VE.DWELLING_ENDORSEMENT_ID     
  FROM MNT_ENDORSMENT_DETAILS ED    
  INNER JOIN POL_DWELLING_ENDORSEMENTS VE ON    
   ED.ENDORSMENT_ID = VE.ENDORSEMENT_ID    
  WHERE VE.CUSTOMER_ID = @CUSTOMER_ID AND    
        VE.POLICY_ID =  @POL_ID AND    
        VE.POLICY_VERSION_ID =  @POL_VERSION_ID AND      
        ED.SELECT_COVERAGE =  @COV_CODE_ID AND    
   VE.DWELLING_ID =  @DWELLING_ID    
     
     
 IF ( @END_ID IS NOT NULL )    
 BEGIN    
  DELETE FROM POL_DWELLING_ENDORSEMENTS     
  WHERE CUSTOMER_ID = @CUSTOMER_ID AND    
         POLICY_ID =  @POL_ID AND    
         POLICY_VERSION_ID =  @POL_VERSION_ID AND      
    DWELLING_ID =  @DWELLING_ID AND    
    DWELLING_ENDORSEMENT_ID = @END_ID    
 END               
   
  END   
  
   
                            
         
END         
         
           
            
        
                       
                            
                            
                          
                          
                          
                        
                      
                    
                  
                
              
            
          
        
      
    
  
  



GO

