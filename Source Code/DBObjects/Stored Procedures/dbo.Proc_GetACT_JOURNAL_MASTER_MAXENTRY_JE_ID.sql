IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetACT_JOURNAL_MASTER_MAXENTRY_JE_ID]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetACT_JOURNAL_MASTER_MAXENTRY_JE_ID]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------  
Proc Name       : Dbo.Proc_GetACT_JOURNAL_MASTER_MAXENTRY_JE_ID  
Created by      : Praveen Kasana
Date            : 11/28/2005  
Purpose     	: To Get the maximum journal ID
Revison History :  
Used In  : Wolverine  
------------------------------------------------------------  
Date     Review By          Comments
drop proc   Dbo.Proc_GetACT_JOURNAL_MASTER_MAXENTRY_JE_ID
------   ------------       -------------------------*/  
CREATE PROC Dbo.Proc_GetACT_JOURNAL_MASTER_MAXENTRY_JE_ID
(  
 @JOURNAL_ID int OUTPUT   
)  
AS  
BEGIN  
 /*Retreiving the maximim journal id*/  
  
 SELECT @JOURNAL_ID = IsNull(Max(Convert(numeric, RTrim(Ltrim(JOURNAL_ID)))), 0) 
 FROM ACT_JOURNAL_MASTER  
  
 return @JOURNAL_ID  
END  
  
  







GO

