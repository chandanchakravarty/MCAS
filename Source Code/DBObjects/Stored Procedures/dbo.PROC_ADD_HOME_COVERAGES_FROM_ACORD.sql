IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_ADD_HOME_COVERAGES_FROM_ACORD]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_ADD_HOME_COVERAGES_FROM_ACORD]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROC PROC_ADD_HOME_COVERAGES_FROM_ACORD
(                                        
	 @CUSTOMER_ID     	INT,                                        
	 @APP_ID     		INT,                                        
	 @APP_VERSION_ID     	SMALLINT                                                         
)                                        
AS                                        
                                        
BEGIN        
	DECLARE @STATE_ID VARCHAR(100)
	DECLARE @LOB_ID VARCHAR(100)
	DECLARE @NON_SMOKER_CREDIT VARCHAR(100)       

		SELECT DWELLING_ID      
	  FROM APP_DWELLINGS_INFO      
	  WHERE CUSTOMER_ID =  @CUSTOMER_ID AND      
	  APP_ID = @APP_ID AND      
	  APP_VERSION_ID = @APP_VERSION_ID 

	 IF NOT EXISTS      
	 (      
	  SELECT DWELLING_ID      
	  FROM APP_DWELLINGS_INFO      
	  WHERE CUSTOMER_ID =  @CUSTOMER_ID AND      
	  APP_ID = @APP_ID AND      
	  APP_VERSION_ID = @APP_VERSION_ID       
	 )      
	 BEGIN      
	 	RETURN      
	 END      

	SELECT  @STATE_ID = STATE_ID,    
	 	@LOB_ID = APP_LOB    
	FROM APP_LIST    
	WHERE CUSTOMER_ID =  @CUSTOMER_ID AND    
	  APP_ID = @APP_ID AND    
	  APP_VERSION_ID = @APP_VERSION_ID
          
    
	IF(@STATE_ID = 14)  
	BEGIN  
		 SELECT @NON_SMOKER_CREDIT = ISNULL(NON_SMOKER_CREDIT,'0')      
		 FROM APP_HOME_OWNER_GEN_INFO      
		 WHERE CUSTOMER_ID =  @CUSTOMER_ID AND      
		 APP_ID = @APP_ID AND      
		 APP_VERSION_ID = @APP_VERSION_ID       
		  
		 IF (@NON_SMOKER_CREDIT = '1')
		 BEGIN      
			INSERT INTO APP_DWELLING_SECTION_COVERAGES
			(CUSTOMER_ID, APP_ID, APP_VERSION_ID, DWELLING_ID, COVERAGE_ID, COVERAGE_CODE_ID)
			values
			(@CUSTOMER_ID, @APP_ID, @APP_VERSION_ID, 1, 1, 937)
		 END   
	END
END
-- PROC_ADD_HOME_COVERAGES_FROM_ACORD 935, 262,1


GO

