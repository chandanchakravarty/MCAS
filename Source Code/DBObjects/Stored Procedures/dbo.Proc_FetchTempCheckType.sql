IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_FetchTempCheckType]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_FetchTempCheckType]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------          
Proc Name       : Proc_FetchCheckTypeOnId          
Created by      : kranti        
Date            : 8th june 2007        
Purpose        : Fetch check Type on check Id       
Revison History :          
        
------------------------------------------------------------          
Date     Review By          Comments          
------   ------------       --------------------------------*/          
-- DROP PROC dbo.Proc_FetchCheckTypeOnId          
CREATE PROCEDURE DBO.Proc_FetchTempCheckType          
(          
 @CHECK_ID  INT      
)        
AS        
SELECT MLV.LOOKUP_VALUE_DESC ,ACI.CHECK_TYPE FROM TEMP_ACT_CHECK_INFORMATION   ACI
LEFT OUTER JOIN MNT_LOOKUP_VALUES MLV ON MLV.LOOKUP_UNIQUE_ID =ACI.CHECK_TYPE
WHERE ACI.CHECK_ID = @CHECK_ID  






GO

