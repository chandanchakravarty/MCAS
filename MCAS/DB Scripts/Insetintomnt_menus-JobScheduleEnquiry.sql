IF NOT EXISTS (SELECT
    1
  FROM [MNT_Menus]
  WHERE MenuId = 276)
BEGIN
  INSERT [dbo].[MNT_Menus] ([TId], [MenuId], [TabId], [DisplayTitle], [IsMenuItem], [IsJsMenuItem], [VirtualSource], [Hyp_Link_Address], [ProductName], [UserType], [Displayimg], [DisplayOrder], [IsHeader], [SubMenu], [MainHeaderId], [IsAdmin], [AdminDisplayText], [MenuItemWidth], [MenuItemHeight], [ErrorMessDesc], [ErrorMessTitle], [ErrorMessHead], [SubTabId], [LangId], [IsActive])
    VALUES (118, 276, N'CLM_ENQ', N'Job Schedule Enquiry', N'Y', N'N', N'1', N'/ClaimProcessing/JobScheduleEnquiry?claimMode=JobScheduleEnquiry', N'CLMS', N'ADM', N'icon-double-angle-right', 276, N'N', N'Y', 217, N'N', N'Job Schedule Enquiry', 150, 17, NULL, NULL, NULL, N'0', 1, N'Y')
END