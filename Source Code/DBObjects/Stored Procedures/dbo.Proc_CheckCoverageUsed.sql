IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_CheckCoverageUsed]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_CheckCoverageUsed]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

 /*----------------------------------------------------------    
Proc Name          : Dbo.Proc_GetBodyTypeFromVINMASTER    
Created by           : Mohit Gupta    
Date                    : 19/05/2005    
Purpose               :     
Revison History :    
Used In                :   Wolverine      
------------------------------------------------------------    
Date     Review By          Comments    
------   ------------       -------------------------*/    
--drop proc Proc_CheckCoverageUsed     
CREATE   PROCEDURE [dbo]. [Proc_CheckCoverageUsed]     
(    
 @Cov_Id int,    
 @COUNT int output    
)    
AS    
BEGIN    
    
SET @COUNT=0    
    
SELECT @COUNT= COUNT(*) FROM  pol_HOME_OWNER_RECREATIONAL_VEHICLES_COVERAGES WHERE COVERAGE_CODE=@Cov_Id    
    
/*IF (@COUNT < 1)    
BEGIN    
SELECT @COUNT= COUNT(*) FROM  APP_UMBRELLA_RECREATIONAL_VEHICLES_COVERAGES WHERE COVERAGE_CODE=@Cov_Id    
END    
ELSE    
BEGIN    
RETURN    
END  */  
    
--IF (@COUNT < 1)    
--BEGIN    
--SELECT @COUNT= COUNT(*) FROM  app_UMBRELLA_WATERCRAFT_COVERAGE_INFO WHERE COVERAGE_CODE_ID=@Cov_Id    
--END    
--ELSE    
--BEGIN    
--RETURN    
--END    
    
IF (@COUNT < 1)    
BEGIN    
SELECT @COUNT= COUNT(*) FROM  pol_WATERCRAFT_COVERAGE_INFO WHERE coverage_code_id=@Cov_Id    
END    
ELSE    
BEGIN    
RETURN    
END    
    
IF (@COUNT < 1)    
BEGIN    
SELECT @COUNT= COUNT(*) FROM  pol_VEHICLE_COVERAGES WHERE COVERAGE_CODE_ID=@Cov_Id    
END    
ELSE    
BEGIN    
RETURN    
END    
    
END  
  
  
  
  
GO

