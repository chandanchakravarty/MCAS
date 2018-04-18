IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_FillTransactionType]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_FillTransactionType]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------
Proc Name       : dbo.Proc_FillTransactionType
Created by      : Swarup
Date            : 12-Oct-2007
Purpose         : To select  record in Reinsurance Transaction Type
Revison History :
Used In         :   Wolverine
------------------------------------------------------------
--drop PROC Dbo.Proc_FillTransactionType 
------   ------------       -------------------------*/

CREATE PROC Dbo.Proc_FillTransactionType 

AS

BEGIN

	DECLARE @QUERY VARCHAR(8000)   
	SET @QUERY = 'SELECT PROCESS_ID,
	PROCESS_DESC = CASE 
	WHEN PROCESS_ID = 2 THEN ''Cancellation''
		ELSE PROCESS_DESC 
	END
	FROM POL_PROCESS_MASTER WHERE PROCESS_ID IN (2,3,4,5,24,31)'
	EXECUTE(@QUERY) 	
END



GO

