IF EXISTS (SELECT 1 FROM [MNT_Menus]  WHERE MenuId=303)
BEGIN
update MNT_Menus set VirtualSource='MCAS.Resources.ClaimProcessing.ClaimLogRequest',ProductName='CLM_LogRequest' where MenuId=303
End

