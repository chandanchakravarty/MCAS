IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetCoveragesForGL]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetCoveragesForGL]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                                        
Proc Name       : dbo.Proc_GetCoveragesForGL                                    
Created by      : Ravindra                                       
Date            : 03-29-2006
Purpose         : 
Revison History :                                        
Used In         : Wolverine                                        
------------------------------------------------------------                                        
Date     Review By          Comments                                        
------   ------------       -------------------------    */                                    
                                    
CREATE PROCEDURE Proc_GetCoveragesForGL                                    
(                                    
  @CUSTOMER_ID int,                                    
  @APP_ID int,                                    
  @APP_VERSION_ID smallint,                                    
  @COVERAGE_CODE nchar(10)                                  
)                                    
                                    
As                                    
DECLARE @STATEID SmallInt                                    
DECLARE @LOBID NVarCHar(5)                                    
                                  
SELECT @STATEID = STATE_ID,                                    
 @LOBID = APP_LOB                                    
FROM APP_LIST                                    
WHERE CUSTOMER_ID = @CUSTOMER_ID AND                                    
 APP_ID = @APP_ID AND                                    
 APP_VERSION_ID = @APP_VERSION_ID                                    


select C.COV_ID AS COV_ID,A.LIMIT_DEDUC_ID AS LIMIT_DEDUC_ID,A.LIMIT_DEDUC_AMOUNT AS LIMIT_DEDUC_AMOUNT
 from MNT_COVERAGE C INNER JOIN MNT_COVERAGE_RANGES A ON A.COV_ID = C.COV_ID 
 WHERE C.COV_CODE =@COVERAGE_CODE AND
	C.STATE_ID = @STATEID	AND
	C.LOB_ID   = @LOBID







GO

