IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetNissData]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetNissData]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------        
Proc Name        : dbo.Proc_GetNissData       
Created by       : Manoj Rathore 
Date             : 22 May 2009 
Purpose          : To Get Calendar year,Accounting from and to date
Revison History  :        
Used In          : Wolverine       

------------------------------------------------------------ */  

CREATE PROC dbo.Proc_GetNissData
AS
BEGIN 
	SELECT YEAR(MAX(FISCAL_BEGIN_DATE))-1 AS CALENDAR_YEAR,
	convert(varchar(12),max(FISCAL_BEGIN_DATE),101) AS ACCOUNTING_FROM_YEAR,
	convert(varchar(12),max(FISCAL_END_DATE),101) AS ACCOUNTING_TO_YEAR FROM ACT_GENERAL_LEDGER
END




GO

