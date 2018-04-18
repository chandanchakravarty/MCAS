IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetCommClassBasedOnID]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetCommClassBasedOnID]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


create proc dbo.GetCommClassBasedOnID
	@UNIQUEID VARCHAR(10)
AS
BEGIN
	SELECT 'CA' + LOOKUP_FRAME_OR_MASONRY AS CLASSCODE FROM MNT_LOOKUP_VALUES WITH (NOLOCK)
	WHERE LOOKUP_UNIQUE_ID=@UNIQUEID 
END



GO

