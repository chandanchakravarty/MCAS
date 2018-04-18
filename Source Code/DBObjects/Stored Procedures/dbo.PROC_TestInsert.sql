IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_TestInsert]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_TestInsert]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROC PROC_TestInsert
AS
BEGIN 
	BEGIN TRAN A
	INSERT INTO TEST_TRAN VALUES('ABCD')
	COMMIT TRAN A
END

GO

