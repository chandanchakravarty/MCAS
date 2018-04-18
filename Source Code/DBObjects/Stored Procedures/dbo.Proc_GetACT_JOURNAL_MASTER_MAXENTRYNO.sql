IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetACT_JOURNAL_MASTER_MAXENTRYNO]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetACT_JOURNAL_MASTER_MAXENTRYNO]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



/*----------------------------------------------------------  
Proc Name       : Dbo.Proc_GetACT_JOURNAL_MASTER_MAXENTRYNO  
Created by      : Vijay Joshi  
Date            : 6/9/2005  
Purpose     : To Get the maximum journal entry no   
Revison History :  
Used In  : Wolverine  
------------------------------------------------------------  
Date     Review By          Comments  
------   ------------       -------------------------*/  
CREATE PROC Dbo.Proc_GetACT_JOURNAL_MASTER_MAXENTRYNO  
(  
 @JOURNAL_ENTRY_NO int OUTPUT   
)  
AS  
BEGIN  
 /*Retreiving the maximim journal entry id*/  
  
 SELECT @JOURNAL_ENTRY_NO = IsNull(Max(Convert(numeric, RTrim(Ltrim(JOURNAL_ENTRY_NO)))), 0) + 1  
 FROM ACT_JOURNAL_MASTER  
  
 return @JOURNAL_ENTRY_NO  
END  
  
  




GO

