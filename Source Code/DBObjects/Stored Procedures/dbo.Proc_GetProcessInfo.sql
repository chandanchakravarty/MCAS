IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetProcessInfo]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetProcessInfo]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------
Proc Name          : Dbo.Proc_GetProcessInfo
Created by         : Mohit Agarwal
Date               : 11/03/2006
Purpose            : To get Transaction Information  from POL_PROCESS_MASTER table
Revison History    :
Used In            :   Wolverine

Modified By        :
Reason             : 
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
CREATE PROC Dbo.Proc_GetProcessInfo
AS
BEGIN

SELECT PROCESS_ID, PROCESS_SHORTNAME, PROCESS_DESC, CREATED_DATETIME FROM 

POL_PROCESS_MASTER WHERE PROCESS_DESC IN ('Commit New Business', 'Commit Renewal Process',
'Commit Endorsement Process', 'Commit Cancellation Process','Commit Reinstate Process')  
END

  


GO

