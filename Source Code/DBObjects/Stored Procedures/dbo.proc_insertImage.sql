IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_insertImage]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[proc_insertImage]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




CREATE  procedure proc_insertImage
(
	@photo image
)
as

begin
	insert into ebxdv25_image(photoImage) values(@photo)
end




GO

