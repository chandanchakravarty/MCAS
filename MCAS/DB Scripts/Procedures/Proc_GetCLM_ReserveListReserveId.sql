IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetCLM_ReserveListReserveId]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetCLM_ReserveListReserveId]
GO


CREATE Proc [dbo].[Proc_GetCLM_ReserveListReserveId]
(
@ReserveId nvarchar(100)
)
as
BEGIN
SET FMTONLY OFF;
SELECT * FROM [CLM_Reserve] where [ReserveId]=@ReserveId
END

GO


