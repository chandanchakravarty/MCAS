IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_DeleteHomeCoverage]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_DeleteHomeCoverage]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                      
Proc Name       : dbo.Proc_DeleteHomeCoverage    
Created by      : Ravindra    
Date            : 06-14-2006    
Purpose         : Delete record from  Home Coverages     
Revison History :                      
Used In  : Wolverine                      
------------------------------------------------------------                      
Date     Review By          Comments                      
------   ------------       -------------------------*/             
--- drop proc Proc_DeleteHomeCoverage    
CREATE  proc  Proc_DeleteHomeCoverage    
(                      
 @CUSTOMER_ID     int,                      
 @APP_ID     int,                      
 @APP_VERSION_ID     smallint,                      
 @DWELLING_ID smallint,                      
 @COVERAGE_CODE VarChar(10)                     
)                      
AS                      
                      
DECLARE @COVERAGE_CODE_ID int           
DECLARE @LOB_ID int           
         
BEGIN   
--Modified By Shafi By Shafi  
                 
          
 SET @COVERAGE_CODE_ID = 0    
    
 SELECT @COVERAGE_CODE_ID = ISNULL(MNT.COV_ID ,0 )    
 FROM MNT_COVERAGE MNT INNER JOIN APP_LIST APP ON          
    MNT.LOB_ID = APP.APP_LOB AND     
    MNT.STATE_ID=APP.STATE_ID     
 WHERE CUSTOMER_ID = @CUSTOMER_ID AND          
  APP.APP_ID = @APP_ID AND          
  APP.APP_VERSION_ID = @APP_VERSION_ID and         
  MNT.COV_CODE = @COVERAGE_CODE AND           
  MNT.IS_ACTIVE = 'Y'          
          
    
          
 IF (  @COVERAGE_CODE_ID = 0 )          
 BEGIN          
  RETURN          
 END    
    
    
                   
 IF EXISTS              
 (              
  SELECT COVERAGE_CODE_ID FROM APP_DWELLING_SECTION_COVERAGES              
  WHERE CUSTOMER_ID = @CUSTOMER_ID and                       
  APP_ID=@APP_ID and                       
  APP_VERSION_ID = @APP_VERSION_ID                       
  and DWELLING_ID = @DWELLING_ID AND              
  COVERAGE_CODE_ID =  @COVERAGE_CODE_ID                      
 )                      
 BEGIN                      
                DELETE FROM APP_DWELLING_SECTION_COVERAGES              
  WHERE CUSTOMER_ID = @CUSTOMER_ID and                       
  APP_ID=@APP_ID and                       
  APP_VERSION_ID = @APP_VERSION_ID                       
  and DWELLING_ID = @DWELLING_ID AND              
  COVERAGE_CODE_ID =  @COVERAGE_CODE_ID    
      
  EXEC Proc_DELETE_LINKED_HOME_ENDORSEMENTS @CUSTOMER_ID,@APP_ID,@APP_VERSION_ID,    
        @DWELLING_ID,@COVERAGE_CODE_ID    
 END                       
                
 IF @@ERROR <> 0          
 BEGIN          
   RAISERROR ('Unable to remove coverage from Watercraft.', 16, 1)          
          
 END               
 --************************************************************         
      
    
                  
END    
    
    
    
    
    
    
  



GO

