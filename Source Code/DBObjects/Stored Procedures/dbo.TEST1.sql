IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TEST1]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[TEST1]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



