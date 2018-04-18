IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_DeleteAPP_HOME_COVERAGES]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_DeleteAPP_HOME_COVERAGES]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------          
Proc Name   : dbo.Proc_DeleteAPP_HOME_COVERAGES         
Created by  : gAURAV          
Date        : 16 June,2005        
Purpose     :           
Revison History  :                
------------------------------------------------------------                      
Date     Review By          Comments                    
-----------------------------------------------------------*/    
CREATE PROCEDURE Proc_DeleteAPP_HOME_COVERAGES  
(   
 @CUSTOMER_ID int,  
 @APP_ID int,  
 @APP_VERSION_ID smallint,   
 @COVERAGE_ID smallint,
 @DWELLING_ID Smallint  
)  
  
As  

DECLARE @COV_ID INT
DECLARE @END_ID Int

--Delete dependent endorsements from DWELLING endorsements  
SELECT @COV_ID = COVERAGE_CODE_ID  
FROM APP_DWELLING_SECTION_COVERAGES  
WHERE CUSTOMER_ID = @CUSTOMER_ID AND  
      APP_ID =  @APP_ID AND  
      APP_VERSION_ID =  @APP_VERSION_ID AND    
      COVERAGE_ID =  @COVERAGE_ID AND  
 DWELLING_ID =  @DWELLING_ID  

SELECT @END_ID = VE.DWELLING_ENDORSEMENT_ID   
 FROM MNT_ENDORSMENT_DETAILS ED  
 INNER JOIN APP_DWELLING_ENDORSEMENTS VE ON  
  ED.ENDORSMENT_ID = VE.ENDORSEMENT_ID  
 WHERE VE.CUSTOMER_ID = @CUSTOMER_ID AND  
       VE.APP_ID =  @APP_ID AND  
       VE.APP_VERSION_ID =  @APP_VERSION_ID AND    
       ED.SELECT_COVERAGE =  @COV_ID AND  
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
--------------------------

DELETE FROM APP_DWELLING_SECTION_COVERAGES  
WHERE CUSTOMER_ID = @CUSTOMER_ID AND  
      APP_ID =  @APP_ID AND  
      APP_VERSION_ID =  @APP_VERSION_ID AND    
      COVERAGE_ID =  @COVERAGE_ID  AND
	DWELLING_ID =  @DWELLING_ID

  
RETURN 1



GO

