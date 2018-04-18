IF  EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[dbo].[my_Trigger]'))
DROP TRIGGER [dbo].[my_Trigger]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TRIGGER [my_Trigger] ON TEST_TRIGGER
FOR UPDATE
AS
BEGIN
	update TEST_TRIGGER set Claint_Zip='489999'
end
GO

