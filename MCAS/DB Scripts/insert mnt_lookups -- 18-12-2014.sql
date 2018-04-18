IF NOT EXISTS (SELECT
    1
  FROM MNT_Lookups WITH (NOLOCK)
  WHERE Lookupvalue = '1'
  AND Category = 'InvestigationResult')
BEGIN
  INSERT [dbo].[MNT_Lookups] ([Lookupvalue], [Lookupdesc], [Description], [Category])
    VALUES (N'1', N'Fully At Fault', N'Fully At Fault', N'InvestigationResult')
END

IF NOT EXISTS (SELECT
    1
  FROM MNT_Lookups WITH (NOLOCK)
  WHERE Lookupvalue = '2'
  AND Category = 'InvestigationResult')
BEGIN
  INSERT [dbo].[MNT_Lookups] ([Lookupvalue], [Lookupdesc], [Description], [Category])
    VALUES (N'2', N'Not At Fault', N'Not At Fault', N'InvestigationResult')
END

IF NOT EXISTS (SELECT
    1
  FROM MNT_Lookups WITH (NOLOCK)
  WHERE Lookupvalue = '3'
  AND Category = 'InvestigationResult')
BEGIN
  INSERT [dbo].[MNT_Lookups] ([Lookupvalue], [Lookupdesc], [Description], [Category])
    VALUES (N'3', N'Partial At Fault', N'Partial At Fault', N'InvestigationResult')
END

