IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_ActivateMNT_CLAIM_COVERAGE_COV_ID]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_ActivateMNT_CLAIM_COVERAGE_COV_ID]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                                      
Proc Name       : dbo.Proc_ActivateMNT_CLAIM_COVERAGE_COV_ID                                      
Created by      : Mohit Agarwal                                      
Date            : 5-Dec-2007                                      
Purpose         : Activate/Deactivate data in MNT_CLAIM_COVERAGE table for claim notification screen                                      
Revison History :                                      
Used In        : Wolverine                                      
------------------------------------------------------------                                      
Modified By  :            
Date   :            
Purpose  :            
------------------------------------------------------------                                                                            
Date     Review By          Comments                                      
------   ------------       -------------------------*/                                      
--DROP PROC dbo.Proc_ActivateMNT_CLAIM_COVERAGE_COV_ID                                                      
CREATE PROC dbo.Proc_ActivateMNT_CLAIM_COVERAGE_COV_ID                 
(                                                 
@COV_ID int,
@IS_ACTIVE char(1)                                                                              
)                               
AS                                                        
BEGIN                  

IF @IS_ACTIVE = 'Y' OR @IS_ACTIVE = 'N'
   UPDATE MNT_CLAIM_COVERAGE SET IS_ACTIVE = @IS_ACTIVE
	WHERE COV_ID = @COV_ID
END         
    




GO

