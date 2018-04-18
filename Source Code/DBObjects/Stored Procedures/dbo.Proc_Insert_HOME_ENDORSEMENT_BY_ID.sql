IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_Insert_HOME_ENDORSEMENT_BY_ID]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_Insert_HOME_ENDORSEMENT_BY_ID]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                      
Proc Name   : dbo.Proc_Insert_HOME_ENDORSEMENT_BY_ID                     
Created by  : Pradeep                      
Date        : 1/01/06                    
Purpose     :  Inserts an appropriate endorsemnt in           
  APP_VEHICLE_ENDORSEMENTS          
Revison History  :                            
------------------------------------------------------------                                  
Date     Review By          Comments                             
DROP PROC    dbo.Proc_Insert_HOME_ENDORSEMENT_BY_ID 1692,129,1,233,1  
-----------------------------------------------------------*/                
CREATE   PROCEDURE [dbo].[Proc_Insert_HOME_ENDORSEMENT_BY_ID]              
(               
  @CUSTOMER_ID int,              
  @APP_ID int,              
  @APP_VERSION_ID smallint,               
  @ENDORSEMENT_ID smallint,          
  @DWELLING_ID smallint              
)              
              
As              
      
  
 IF NOT EXISTS          
 (          
   SELECT * FROM APP_DWELLING_ENDORSEMENTS          
   WHERE CUSTOMER_ID = @CUSTOMER_ID AND          
    APP_ID = @APP_ID AND          
    APP_VERSION_ID = @APP_VERSION_ID AND          
    DWELLING_ID = @DWELLING_ID  AND        
    ENDORSEMENT_ID = @ENDORSEMENT_ID         
 )      
 BEGIN      
       
 DECLARE @DWELL_END_ID Int     
---FOR EDITION DATE IN APP_DWELLING_ENDORSEMENTS  
DECLARE @EDITION_DATE VARCHAR(10)  
DECLARE @APP_EFFECTIVE_DATE DATETIME  
  
  SELECT  @APP_EFFECTIVE_DATE = APP_EFFECTIVE_DATE                                       
  FROM APP_LIST  WHERE CUSTOMER_ID = @CUSTOMER_ID AND  APP_ID = @APP_ID AND APP_VERSION_ID = @APP_VERSION_ID                 
----               
      
 SELECT  @DWELL_END_ID   = ISNULL(MAX ( DWELLING_ENDORSEMENT_ID ),0) + 1      
 FROM APP_DWELLING_ENDORSEMENTS      
 WHERE CUSTOMER_ID = @CUSTOMER_ID AND      
  APP_ID = @APP_ID AND      
  APP_VERSION_ID = @APP_VERSION_ID AND      
  DWELLING_ID = @DWELLING_ID   
 --BY PRAVESH FOR DEFAULT EDITION DATE              
   SELECT    @EDITION_DATE =ENDORSEMENT_ATTACH_ID  FROM MNT_ENDORSEMENT_ATTACHMENT WHERE ENDORSEMENT_ID=@ENDORSEMENT_ID AND   
   @APP_EFFECTIVE_DATE BETWEEN VALID_DATE AND ISNULL(EFFECTIVE_TO_DATE,'3000-12-12') AND  @APP_EFFECTIVE_DATE<=ISNULL(DISABLED_DATE,'3000-12-12')   
--  changed by pravesh on 13 may 2008 as if associated coverage is in POL_DWELLING_SECTION_COVERAGES only then insert endorsement  
DECLARE @SELECT_COV_ID Int   
DECLARE @ENDORS_ASSOC_COVERAGE nvarchar(10)  
SELECT @SELECT_COV_ID = SELECT_COVERAGE,@ENDORS_ASSOC_COVERAGE=ENDORS_ASSOC_COVERAGE FROM MNT_ENDORSMENT_DETAILS WHERE  ENDORSMENT_ID=@ENDORSEMENT_ID   
SELECT @ENDORS_ASSOC_COVERAGE,@SELECT_COV_ID  
IF (@ENDORS_ASSOC_COVERAGE='Y'  
 AND NOT EXISTS(SELECT COVERAGE_CODE_ID FROM APP_DWELLING_SECTION_COVERAGES WITH(NOLOCK)   
    WHERE  CUSTOMER_ID   = @CUSTOMER_ID AND        
      APP_ID   = @APP_ID AND      
      APP_VERSION_ID = @APP_VERSION_ID AND      
      DWELLING_ID  = @DWELLING_ID AND   
    COVERAGE_CODE_ID =@SELECT_COV_ID  
    ) AND @ENDORSEMENT_ID NOT IN (183,233) --Added by Charles on 3-Sep-09,Itrack 6355, to bypass endosement association condition when Coverage "Farm Liability (HO-73)" any one is found selected.   
 )  
BEGIN  
 RETURN 0  
END  
--END HERE  
    
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
   @ENDORSEMENT_ID ,      
   @DWELL_END_ID ,  
   @EDITION_DATE   
 )          
END              
              
RETURN 1              
              


GO

