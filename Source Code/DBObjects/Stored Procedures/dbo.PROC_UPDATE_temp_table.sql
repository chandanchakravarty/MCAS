IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_UPDATE_temp_table]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_UPDATE_temp_table]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROC PROC_UPDATE_temp_table
@ID INT,
@NAME VARCHAR(50)
AS
BEGIN
UPDATE temp_table
SET NAME=@NAME
WHERE ID = @ID


END
GO

