IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_DeleteUMBRELLA_WATERCRAFT_COVERAGES]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_DeleteUMBRELLA_WATERCRAFT_COVERAGES]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------          
Proc Name   : dbo.Proc_DeleteUMBRELLA_WATERCRAFT_COVERAGES         
Created by  : Pradeep          
Date        : 5 July,2005        
Purpose     :           
Revison History  :                
 ------------------------------------------------------------                      
Date     Review By          Comments                    
           
------   ------------       -------------------------*/    
  
CREATE       PROCEDURE Proc_DeleteUMBRELLA_WATERCRAFT_COVERAGES  
(   
 @CUSTOMER_ID int,  
 @APP_ID int,  
 @APP_VERSION_ID smallint,   
 @BOAT_ID smallint,  
 @COVERAGE_ID smallint  
)  
  
As  

DECLARE @COV_ID Int    
DECLARE @END_ID smallint     
  
SELECT @COV_ID = COVERAGE_CODE_ID    
FROM APP_UMBRELLA_WATERCRAFT_COVERAGE_INFO    
WHERE CUSTOMER_ID = @CUSTOMER_ID AND    
      APP_ID =  @APP_ID AND    
      APP_VERSION_ID =  @APP_VERSION_ID AND      
      COVERAGE_ID =  @COVERAGE_ID AND    
 BOAT_ID =  @BOAT_ID    
  
DELETE FROM APP_UMBRELLA_WATERCRAFT_COVERAGE_INFO  
WHERE CUSTOMER_ID = @CUSTOMER_ID AND  
      APP_ID =  @APP_ID AND  
      APP_VERSION_ID =  @APP_VERSION_ID AND  
      BOAT_ID = @BOAT_ID AND     
      COVERAGE_ID =  @COVERAGE_ID   
   

--Delete dependent endorsements from Vehicle endorsements    
    
 SELECT @END_ID = VE.BOAT_ENDORSEMENT_ID     
 FROM MNT_ENDORSMENT_DETAILS ED    
 INNER JOIN APP_UMBRELLA_WATERCRAFT_ENDORSEMENTS VE ON    
  ED.ENDORSMENT_ID = VE.ENDORSEMENT_ID    
 WHERE VE.CUSTOMER_ID = @CUSTOMER_ID AND    
       VE.APP_ID =  @APP_ID AND    
       VE.APP_VERSION_ID =  @APP_VERSION_ID AND      
       ED.SELECT_COVERAGE =  @COV_ID AND    
   VE.BOAT_ID =  @BOAT_ID    
    
    
IF ( @END_ID IS NOT NULL )    
BEGIN    
	DELETE FROM APP_UMBRELLA_WATERCRAFT_ENDORSEMENTS     
	WHERE CUSTOMER_ID = @CUSTOMER_ID AND    
	       APP_ID =  @APP_ID AND    
	       APP_VERSION_ID =  @APP_VERSION_ID AND      
	  BOAT_ID =  @BOAT_ID AND    
	  BOAT_ENDORSEMENT_ID = @END_ID    
END    

RETURN 1     
  
  
  
  
  
  
  
  
  
  
  
  



GO

