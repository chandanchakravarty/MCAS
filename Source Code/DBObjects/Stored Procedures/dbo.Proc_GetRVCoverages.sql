IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetRVCoverages]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetRVCoverages]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--begin tran
--drop proc Proc_GetRVCoverages 
--go
/*                            
----------------------------------------------------------                                    
Proc Name       : dbo.Proc_GetRVCoverages                                
Created by      : Raghav                                  
Date            : 17 Nov,2009                                    
Purpose         : Selects a single record for Coverage E and Coverage F                                     
Revison History :                                    
Used In         : Wolverine                                    
------------------------------------------------------------                                    
Date     Review By          Comments                                    
------   ------------       -------------------------                                   
*/ 
CREATE PROC Proc_GetRVCoverages
(
    @CUSTOMER_ID int,                                
	@APP_POL_ID int,                                
	@APP_POL_VERSION_ID int , 
    @CALLEDFROM VARCHAR(20)
)
AS
BEGIN 
DECLARE @DWELLING_ID INT  

IF @CALLEDFROM = 'APP'
BEGIN
	 
     SELECT @DWELLING_ID = DWELLING_ID 
		FROM APP_DWELLINGS_INFO 
     WHERE CUSTOMER_ID = @CUSTOMER_ID AND APP_ID = @APP_POL_ID AND 
	 APP_VERSION_ID = @APP_POL_VERSION_ID AND IS_ACTIVE = 'Y'    
	 	   
		   SELECT CASE  WHEN LIMIT_1 IS NULL THEN LIMIT1_AMOUNT_TEXT ELSE CONVERT(VARCHAR(20),LIMIT_1) END AS LIMIT_1 , COVERAGE_CODE_ID
           FROM APP_DWELLING_SECTION_COVERAGES WHERE  CUSTOMER_ID = @CUSTOMER_ID 
		   AND APP_ID = @APP_POL_ID AND 
		   APP_VERSION_ID = @APP_POL_VERSION_ID
		   AND DWELLING_ID = @DWELLING_ID 
		    AND COVERAGE_CODE_ID IN (10,13,170,171)
    
END
ELSE IF @CALLEDFROM = 'POL'    
BEGIN      
	  SELECT @DWELLING_ID = DWELLING_ID FROM POL_DWELLINGS_INFO 
      WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @APP_POL_ID AND 
	  POLICY_VERSION_ID = @APP_POL_VERSION_ID AND IS_ACTIVE = 'Y'    


	
       SELECT CASE  WHEN LIMIT_1 IS NULL THEN LIMIT1_AMOUNT_TEXT ELSE CONVERT(VARCHAR(20),LIMIT_1) END AS LIMIT_1, COVERAGE_CODE_ID
       FROM POL_DWELLING_SECTION_COVERAGES WHERE  CUSTOMER_ID = @CUSTOMER_ID 
       AND POLICY_ID = @APP_POL_ID AND 
	   POLICY_VERSION_ID = @APP_POL_VERSION_ID
       AND DWELLING_ID = @DWELLING_ID 
       AND COVERAGE_CODE_ID IN (10,13,170,171)
	
END 
END 

--go
--exec Proc_Get_Cov_Sec_2 1692,225, 1 , 'App'
--rollback tran




GO

