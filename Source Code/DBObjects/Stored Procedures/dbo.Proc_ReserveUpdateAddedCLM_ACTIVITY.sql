IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_ReserveUpdateAddedCLM_ACTIVITY]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_ReserveUpdateAddedCLM_ACTIVITY]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- 
/*----------------------------------------------------------          
Proc Name       : dbo.Proc_ReserveUpdateAddedCLM_ACTIVITY          
Created by      : Sumit Chhabra  
Date            : 06/07/2006          
Purpose      		: Checkes whether any Reserve Update Activity has been added for the current claim id
Revison History :          
Used In  : Wolverine          
------------------------------------------------------------          
Date     Review By          Comments          
------   ------------       -------------------------*/          
CREATE PROC dbo.Proc_ReserveUpdateAddedCLM_ACTIVITY          
(          
 @CLAIM_ID     int
)          
AS          
BEGIN 
declare @ACTIVITY_REASON int

set @ACTIVITY_REASON = 11773 --lookup unique id for Reserve Update

if exists(SELECT ACTIVITY_ID FROM CLM_ACTIVITY WHERE CLAIM_ID=@CLAIM_ID AND ACTIVITY_REASON = @ACTIVITY_REASON)
	return 1

return -1

END     




GO

