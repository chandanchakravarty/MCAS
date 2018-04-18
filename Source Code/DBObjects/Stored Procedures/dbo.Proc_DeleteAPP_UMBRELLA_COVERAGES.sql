IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_DeleteAPP_UMBRELLA_COVERAGES]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_DeleteAPP_UMBRELLA_COVERAGES]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO







/*----------------------------------------------------------        
Proc Name   : dbo.Proc_DeleteAPP_UMBRELLA_COVERAGES       
Created by  : PraVESHG
Date        : 11 OCT,2006      
Purpose     :         
Revison History  :              
------------------------------------------------------------                    
Date     Review By          Comments                  
drop proc dbooc_DeleteAPP_UMBRELLA_COVERAGES       
-----------------------------------------------------------*/  
CREATE   PROCEDURE dbo.Proc_DeleteAPP_UMBRELLA_COVERAGES
(	
	@CUSTOMER_ID int,
	@APP_ID int,
	@APP_VERSION_ID smallint, 
	@COVERAGE_ID smallint
)

As

DECLARE @COV_ID Int
DECLARE @END_ID smallint 

SELECT @COV_ID = COVERAGE_CODE_ID
FROM APP_UMBRELLA_COVERAGES
WHERE CUSTOMER_ID = @CUSTOMER_ID AND
      APP_ID =  @APP_ID AND
      APP_VERSION_ID =  @APP_VERSION_ID AND		
      COVERAGE_ID =  @COVERAGE_ID 

DELETE FROM APP_UMBRELLA_COVERAGES
WHERE CUSTOMER_ID = @CUSTOMER_ID AND
      APP_ID =  @APP_ID AND
      APP_VERSION_ID =  @APP_VERSION_ID AND		
      COVERAGE_ID =  @COVERAGE_ID 



RETURN 1







GO

