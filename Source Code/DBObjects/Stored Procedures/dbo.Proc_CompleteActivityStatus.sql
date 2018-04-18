IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_CompleteActivityStatus]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_CompleteActivityStatus]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------          
Proc Name       : dbo.Proc_CompleteActivityStatus          
Created by      : Sumit Chhabra
Date            : 06/20/2006          
Purpose   		  : To change the status of various activites at claims to be complete
Revison History :          
Used In  : Wolverine          
------------------------------------------------------------          
Date     Review By          Comments          
------   ------------       -------------------------*/          
CREATE PROC dbo.Proc_CompleteActivityStatus          
(          
 @CLAIM_ID     int          
)          
AS          
BEGIN          
declare @ACTIVITY_STATUS_COMPLETE int
set @ACTIVITY_STATUS_COMPLETE=11801

update clm_activity set activity_status = @ACTIVITY_STATUS_COMPLETE where claim_id=@claim_id

END 


GO

