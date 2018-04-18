if not exists(Select Top 1* from MNT_Menus where MenuId=304)
begin
INSERT [dbo].[MNT_Menus] ([TId], [MenuId], [TabId], [DisplayTitle], [IsMenuItem], [IsJsMenuItem], [VirtualSource], [Hyp_Link_Address], [ProductName], [UserType], [Displayimg], [DisplayOrder], [IsHeader], [SubMenu], [MainHeaderId], [IsAdmin], [AdminDisplayText], [MenuItemWidth], [MenuItemHeight], [ErrorMessDesc], [ErrorMessTitle], [ErrorMessHead], [SubTabId], [LangId], [IsActive], [GroupType]) VALUES (146 , 304, N'S_ADMN', N'Organization Access', N'N', N'N', N'MCAS.Web.Objects.Resources.UserAdmin.OrganizationAccess', N'UserAdmin/OrganizationAccess', N'ADM', N'ADM', N'icon-list-ol text-warning', 304, N'N', N'Y', 114, N'N', N'Organization Access', 150, 17, NULL, NULL, NULL, N'10', 1, N'Y', NULL)
End

