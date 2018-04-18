IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_DELETE_DWELLING_COVERAGES_BY_ID]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_DELETE_DWELLING_COVERAGES_BY_ID]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


--drop proc Proc_DELETE_DWELLING_COVERAGES_BY_ID

 /*----------------------------------------------------------                            
Proc Name       : dbo.Proc_DELETE_DWELLING_COVERAGES_BY_ID                            
Created by      : Pradeep                            
Date            : 12/29/2005                            
Purpose      :Inserts a record in Dwelling coverages      
Revison History :                            
Used In  : Wolverine                            
------------------------------------------------------------                            
Date     Review By          Comments                            
------   ------------       -------------------------*/                            
CREATE           PROC Dbo.Proc_DELETE_DWELLING_COVERAGES_BY_ID                            
(                            
 @CUSTOMER_ID     int,                            
 @APP_ID     int,                            
 @APP_VERSION_ID     smallint,                            
 @DWELLING_ID smallint,                            
 @COV_CODE_ID Int    
     
)                            
AS                            
                            
    
                     
BEGIN                            
                      

 DECLARE @END_ID smallint     
                                    
  IF EXISTS                            
  (                            
     SELECT * FROM APP_DWELLING_SECTION_COVERAGES AD    
     where AD.CUSTOMER_ID = @CUSTOMER_ID and                             
      AD.APP_ID=@APP_ID and                             
      AD.APP_VERSION_ID = @APP_VERSION_ID                             
      and AD.DWELLING_ID = @DWELLING_ID AND                            
   AD.COVERAGE_CODE_ID = @COV_CODE_ID  
    
  )                            
                              
  BEGIN                 
               
     DELETE FROM APP_DWELLING_SECTION_COVERAGES    
     WHERE CUSTOMER_ID = @CUSTOMER_ID and                             
      APP_ID=@APP_ID and                             
      APP_VERSION_ID = @APP_VERSION_ID                             
      and DWELLING_ID = @DWELLING_ID AND                            
   COVERAGE_CODE_ID = @COV_CODE_ID  
      
   --Delete dependent Endorsements        
       SELECT @END_ID = VE.DWELLING_ENDORSEMENT_ID     
  FROM MNT_ENDORSMENT_DETAILS ED    
  INNER JOIN APP_DWELLING_ENDORSEMENTS VE ON    
   ED.ENDORSMENT_ID = VE.ENDORSEMENT_ID    
  WHERE VE.CUSTOMER_ID = @CUSTOMER_ID AND    
        VE.APP_ID =  @APP_ID AND    
        VE.APP_VERSION_ID =  @APP_VERSION_ID AND      
        ED.SELECT_COVERAGE =  @COV_CODE_ID AND    
   VE.DWELLING_ID =  @DWELLING_ID    
     
     
 IF ( @END_ID IS NOT NULL )    
 BEGIN    
  DELETE FROM APP_DWELLING_ENDORSEMENTS     
  WHERE CUSTOMER_ID = @CUSTOMER_ID AND    
         APP_ID =  @APP_ID AND    
         APP_VERSION_ID =  @APP_VERSION_ID AND      
    DWELLING_ID =  @DWELLING_ID AND    
    DWELLING_ENDORSEMENT_ID = @END_ID    
 END               
   
  END   
  
   
                            
         
END         
         
           
            
        
                       
                            
                            
                          
                          
                          
                        
                      
                    
                  
                
              
            
          
        
      
    
  
  



GO

