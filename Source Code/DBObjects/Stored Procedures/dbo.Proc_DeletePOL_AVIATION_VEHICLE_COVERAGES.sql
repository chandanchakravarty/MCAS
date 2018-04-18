IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_DeletePOL_AVIATION_VEHICLE_COVERAGES]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_DeletePOL_AVIATION_VEHICLE_COVERAGES]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------        
Proc Name   : dbo.Proc_DeletePOL_AVIATION_VEHICLE_COVERAGES       
Created by  : Pravesh K Chandel     
Date        : 15 Jan,2010      
Purpose     :         
Revison History  :              
------------------------------------------------------------                    
Date     Review By          Comments                  
-----------------------------------------------------------*/  
CREATE   PROCEDURE Proc_DeletePOL_AVIATION_VEHICLE_COVERAGES
(	
	@CUSTOMER_ID int,
	@POLICY_ID int,
	@POLICY_VERSION_ID smallint, 
	@COVERAGE_ID smallint,
	@VEHICLE_ID smallint
)

As

DECLARE @COV_ID Int
DECLARE @END_ID smallint 

--SELECT @COV_ID = COVERAGE_CODE_ID
--FROM POL_AVIATION_VEHICLE_COVERAGES
--WHERE CUSTOMER_ID = @CUSTOMER_ID AND
--      POLICY_ID =  @POLICY_ID AND
--      POLICY_VERSION_ID =  @POLICY_VERSION_ID AND		
--      COVERAGE_ID =  @COVERAGE_ID AND
--	VEHICLE_ID = 	@VEHICLE_ID

DELETE FROM POL_AVIATION_VEHICLE_COVERAGES
WHERE CUSTOMER_ID = @CUSTOMER_ID AND
      POLICY_ID =  @POLICY_ID AND
      POLICY_VERSION_ID =  @POLICY_VERSION_ID AND		
      COVERAGE_ID =  @COVERAGE_ID AND
	VEHICLE_ID = 	@VEHICLE_ID



RETURN 1







GO

