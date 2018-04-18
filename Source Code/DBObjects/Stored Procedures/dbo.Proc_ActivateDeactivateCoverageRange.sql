IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_ActivateDeactivateCoverageRange]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_ActivateDeactivateCoverageRange]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------  
Proc Name          : Dbo.Proc_ActivateDeactivateCoverageRange  
Created by           : Mohit Gupta  
Date                    : 19/05/2005  
Purpose               :   
Revison History :  
Used In                :   Wolverine   
Modified BY            :   Shafi
Date                   : 17/04/06
Purpose                : Pass status From The BL Layer
------------------------------------------------------------  
Date     Review By          Comments  
------   ------------       -------------------------*/ 
--drop proc Proc_ActivateDeactivateCoverageRange
CREATE   PROCEDURE Proc_ActivateDeactivateCoverageRange  
(  
             @LIMIT_DEDUC_ID int,  
             @STATUS CHAR(1)     
                
)  
AS  
BEGIN  
UPDATE MNT_COVERAGE_RANGES  
SET   
IS_ACTIVE=@STATUS  
WHERE   
LIMIT_DEDUC_ID=@LIMIT_DEDUC_ID  
END  
  
  





GO

