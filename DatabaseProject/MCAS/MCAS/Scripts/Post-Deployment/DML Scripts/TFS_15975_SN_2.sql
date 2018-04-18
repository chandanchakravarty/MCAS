-- =============================================
-- Script Template
-- =============================================
IF EXISTS (SELECT 1 FROM [MNT_Menus]  WHERE [MenuId] = 303)
BEGIN
  UPDATE MNT_Menus set DisplayOrder=303,MainHeaderId=205,TabId='CLM_REG' where MenuId=303
END

IF EXISTS (SELECT 1 FROM [MNT_Menus]  WHERE [MenuId] = 293)
BEGIN
  UPDATE MNT_Menus set IsActive='N' where MenuId=293
END
