IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetValuesCLM_REOPEN_CLAIM]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetValuesCLM_REOPEN_CLAIM]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------  
Proc Name       : dbo.Proc_GetValuesCLM_REOPEN_CLAIM  
Created by      : Vijay Arora  
Date            : 6/19/2006  
Purpose     : Evaluation  
Revison History :  
Used In  : Wolverine  
------------------------------------------------------------  
Date     Review By          Comments  
------   ------------       -------------------------*/  
CREATE PROC Dbo.Proc_GetValuesCLM_REOPEN_CLAIM  
(  
 @CLAIM_ID int,  
        @REOPEN_ID int  
)  
AS  
BEGIN  
SELECT 0 as REOPEN_COUNT,CLAIM_ID, ISNULL(U.USER_TITLE,'') + ' ' + ISNULL(U.USER_FNAME,'') + ' ' + ISNULL(U.USER_LNAME,'') AS REOPEN_NAME,  
REOPEN_ID, CONVERT(VARCHAR(10),REOPEN_DATE,101) AS REOPEN_DATE,  REOPEN_BY, REASON  
FROM CLM_REOPEN_CLAIM R  
LEFT JOIN MNT_USER_LIST U ON U.USER_ID = R.REOPEN_BY  
WHERE CLAIM_ID = @CLAIM_ID AND REOPEN_ID = @REOPEN_ID  
END


GO

