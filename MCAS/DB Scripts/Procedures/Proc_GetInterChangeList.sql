IF EXISTS (SELECT
    *
  FROM sys.objects
  WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetInterChangeList]')
  AND type IN (N'P', N'PC'))
  DROP PROCEDURE [dbo].[Proc_GetInterChangeList]
GO


CREATE PROC [dbo].[Proc_GetInterChangeList]
AS
BEGIN

  SET FMTONLY OFF;
  SELECT
    [Id],
    [InterchangeName],
    [Address1],
    [Address2],
    [Address3],
    [City],
    [State],
    [Country],
    [PostalCode],
    [Status],
    [EffectiveFrom],
    [EffectiveTo],
    [Remarks],
    [ModifiedDate],
    [CreatedDate],
    [CreatedBy],
    [ModifiedBy]
  FROM [MNT_InterChange]
END
GO