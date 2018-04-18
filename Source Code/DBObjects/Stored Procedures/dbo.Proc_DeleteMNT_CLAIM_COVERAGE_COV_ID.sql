IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_DeleteMNT_CLAIM_COVERAGE_COV_ID]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_DeleteMNT_CLAIM_COVERAGE_COV_ID]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                                    
Proc Name       : dbo.Proc_DeleteMNT_CLAIM_COVERAGE_COV_ID                                    
Created by      : Mohit Agarwal                                    
Date            : 5-Dec-2007                                    
Purpose         : Delete data in MNT_CLAIM_COVERAGE table for claim notification screen                                    
Revison History :                                    
Used In        : Wolverine                                    
------------------------------------------------------------                                    
Modified By  :          
Date   :          
Purpose  :          
------------------------------------------------------------                                                                          
Date     Review By          Comments                                    
------   ------------       -------------------------*/                                    
--DROP PROC dbo.Proc_DeleteMNT_CLAIM_COVERAGE_COV_ID                                                    
CREATE PROC dbo.Proc_DeleteMNT_CLAIM_COVERAGE_COV_ID               
(                                               
@COV_ID int                                                                            
)                             
AS                                                      
BEGIN                
DECLARE @CLAIM_ID int
DECLARE @COV_ID_CLAIM int

SELECT @CLAIM_ID=CLAIM_ID, @COV_ID_CLAIM= COV_ID_CLAIM FROM MNT_CLAIM_COVERAGE WHERE COV_ID = @COV_ID

DELETE FROM MNT_CLAIM_COVERAGE WHERE COV_ID = @COV_ID  

UPDATE MNT_CLAIM_COVERAGE SET COV_ID_CLAIM = COV_ID_CLAIM-1 WHERE COV_ID_CLAIM > @COV_ID_CLAIM AND  @CLAIM_ID=CLAIM_ID

END       
  


GO

