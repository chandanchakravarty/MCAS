IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_DELETE_LINKED_POL_HOME_ENDORSEMENTS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_DELETE_LINKED_POL_HOME_ENDORSEMENTS]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                      
Proc Name       : dbo.Proc_DELETE_LINKED_POL_HOME_ENDORSEMENTS                      
Created by      : SHAFI                      
Date            : 06-28-2006
Purpose      	: Delete linked endorsemnts for this coverage                    
Revison History :                      
Used In  : Wolverine                      
------------------------------------------------------------                      
Date     Review By          Comments                      
------   ------------       -------------------------*/                      
--drop proc Proc_DELETE_LINKED_HOME_ENDORSEMENTS
CREATE   PROC dbo.Proc_DELETE_LINKED_POL_HOME_ENDORSEMENTS                      
(                      
	@CUSTOMER_ID     int,                      
	@POLICY_ID     int,                      
	@POLICY_VERSION_ID     smallint,                      
	@DWELLING_ID smallint,                      
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
	IF EXISTS          
	(          
		SELECT ENDORSEMENT_ID  FROM POL_DWELLING_ENDORSEMENTS          
		WHERE CUSTOMER_ID = @CUSTOMER_ID AND                          
		POLICY_ID = @POLICY_ID AND                          
		POLICY_VERSION_ID = @POLICY_VERSION_ID AND                  
		DWELLING_ID = @DWELLING_ID  AND          
		ENDORSEMENT_ID =  @ENDORSEMENT_ID                 
	)          
	BEGIN        
		DELETE 
		FROM POL_DWELLING_ENDORSEMENTS                  
		WHERE 
		CUSTOMER_ID = @CUSTOMER_ID
		AND POLICY_ID = @POLICY_ID 
		AND POLICY_VERSION_ID = @POLICY_VERSION_ID
		AND DWELLING_ID = @DWELLING_ID             
		AND ENDORSEMENT_ID =  @ENDORSEMENT_ID    
	END          
	--End of linked endorsements ------------------  
	
END  
      
END                     







GO

