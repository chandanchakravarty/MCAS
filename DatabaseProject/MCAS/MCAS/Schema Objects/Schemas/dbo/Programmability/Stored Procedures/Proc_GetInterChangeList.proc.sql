CREATE PROCEDURE [dbo].[Proc_GetInterChangeList]
WITH EXECUTE AS CALLER
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


