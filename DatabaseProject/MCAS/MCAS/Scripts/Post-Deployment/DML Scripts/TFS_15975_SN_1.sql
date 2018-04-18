-- =============================================
-- Script Template
-- =============================================

IF NOT EXISTS (SELECT   1  FROM [MNT_Menus]  WHERE [MenuId] = 303  AND [TabId] = 'CLM_LOG')
BEGIN
 INSERT [dbo].[MNT_Menus] ([TId], [MenuId], [TabId], [DisplayTitle], [IsMenuItem], [IsJsMenuItem], [VirtualSource], [Hyp_Link_Address], [ProductName], [UserType], [Displayimg], [DisplayOrder], [IsHeader], [SubMenu], [MainHeaderId], [IsAdmin], [AdminDisplayText], [MenuItemWidth], [MenuItemHeight], [ErrorMessDesc], [ErrorMessTitle], [ErrorMessHead], [SubTabId], [LangId], [IsActive],[GroupType])
 VALUES (145, 303, N'CLM_LOG', N'LOG Request', N'Y', N'N', N'1', N'/ClaimRecoveryProcessing/ClaimLogRequest?claimMode=Recovery', N'CLMS', N'ADM', N'icon-double-angle-right', 303, N'N', N'Y', 293, N'N', N'Log Request', 150, 17, NULL, NULL, NULL, 0, 1, N'Y',NULL)
END

IF EXISTS (SELECT   1  FROM [MNT_Menus]  WHERE [MenuId] = 293  AND [TabId] = 'CLM_REC')
BEGIN
  UPDATE MNT_Menus SET TabId='CLM_LOG',DisplayTitle='LOG Request',IsMenuItem='Y',IsJsMenuItem='N',Hyp_Link_Address='#',Displayimg='icon-briefcase text-danger',IsHeader='Y',SubMenu='N',MainHeaderId=0 where MenuId=293
END



