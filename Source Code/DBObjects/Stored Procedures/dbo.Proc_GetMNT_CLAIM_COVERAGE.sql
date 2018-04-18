IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetMNT_CLAIM_COVERAGE]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetMNT_CLAIM_COVERAGE]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                                    
Proc Name       : dbo.Proc_GetMNT_CLAIM_COVERAGE                                    
Created by      : Mohit Agarwal                                    
Date            : 5-Dec-2007                                    
Purpose         : Fetch data in MNT_CLAIM_COVERAGE table for claim notification screen                                    
Revison History :                                    
Used In        : Wolverine                                    
------------------------------------------------------------                                    
Modified By  :          
Date   :          
Purpose  :          
------------------------------------------------------------                                                                          
Date     Review By          Comments                                    
------   ------------       -------------------------*/                                    
--DROP PROC dbo.Proc_GetMNT_CLAIM_COVERAGE                                                    
CREATE PROC dbo.Proc_GetMNT_CLAIM_COVERAGE               
(                                               
@COV_ID int                                                                            
)                             
AS                                                      
BEGIN                
    
SELECT     
  CLAIM_ID,
  COV_DES,
  STATE_ID,
  LOB_ID,
  LIMIT_1,
  DEDUCTIBLE_1,
  IS_ACTIVE,
  CREATED_BY,
  CREATED_DATETIME,
  MODIFIED_BY,
  LAST_UPDATED_DATETIME
FROM     
 MNT_CLAIM_COVERAGE    
WHERE    
 @COV_ID=COV_ID    
END       
  



GO

