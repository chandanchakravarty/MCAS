IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_ActivateDeactivateClaimAdjuster]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_ActivateDeactivateClaimAdjuster]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*-------------------------------------------------------------    
Created by      : Vijay Joshi    
Date            : 16-June-2006    
Purpose         : To activate or deactivate claim adjuster    
Revison History :    
Used In         :   wolvorine    
------------------------------------------------------------    
Modified By 	: Asfa Praveen
Date 		: 29/Aug/2007
Purpose		: Modified to correct Adjuster_Code value
------------------------------------------------------------                                                                
Date     Review By          Comments    
------   ------------       -------------------------*/    
--drop PROC dbo.Proc_ActivateDeactivateClaimAdjuster   
CREATE PROC dbo.Proc_ActivateDeactivateClaimAdjuster    
(    
 @CODE     numeric(9),    
 @IS_ACTIVE   Char(1)    
)    
AS    
BEGIN    
     
    
--If any adjuster is assigned to one or more claims, do not allow it to be deactivated    
IF(UPPER(@IS_ACTIVE)='N')    
BEGIN    
 IF EXISTS(SELECT CLAIM_ID FROM CLM_CLAIM_INFO WHERE ADJUSTER_ID=@CODE)    
  RETURN -2    
END    
    
 UPDATE CLM_ADJUSTER    
 SET     
  IS_ACTIVE = @IS_ACTIVE    
 WHERE    
ADJUSTER_ID = @CODE    
     
 RETURN 1    
    
END    
    
    
  



GO

