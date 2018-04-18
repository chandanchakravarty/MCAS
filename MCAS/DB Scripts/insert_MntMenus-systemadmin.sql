
IF NOT EXISTS (SELECT 1 FROM [MNT_Menus] WHERE [MenuId] = 299)
BEGIN
Insert into MNT_Menus ([TId], [MenuId], [TabId], [DisplayTitle], [IsMenuItem], [IsJsMenuItem], 
[VirtualSource], [Hyp_Link_Address],[ProductName], [UserType], [Displayimg], [DisplayOrder],
[IsHeader], [SubMenu], [MainHeaderId], [IsAdmin], [AdminDisplayText],[MenuItemWidth], [MenuItemHeight],
[ErrorMessDesc], [ErrorMessTitle], [ErrorMessHead], [SubTabId], [LangId], [IsActive],[GroupType])
values(141,299,'S_ADMN','Vehicle Listing Upload','N','N',1,'/Masters/VehicleUploadIndex','ADM',N'ADM', 
 N'icon-list-ol text-warning',299, N'N', N'Y',101, N'N', N'Vehicle Listing Upload',
 150, 17,NULL, NULL, NULL,0,1, N'Y',NULL)
END
IF NOT EXISTS (SELECT 1 FROM [MNT_Menus] WHERE [MenuId] = 300)
BEGIN
Insert into MNT_Menus ([TId], [MenuId], [TabId], [DisplayTitle], [IsMenuItem], [IsJsMenuItem], 
[VirtualSource], [Hyp_Link_Address],[ProductName], [UserType], [Displayimg], [DisplayOrder],
[IsHeader], [SubMenu], [MainHeaderId], [IsAdmin], [AdminDisplayText],[MenuItemWidth], [MenuItemHeight],
[ErrorMessDesc], [ErrorMessTitle], [ErrorMessHead], [SubTabId], [LangId], [IsActive],[GroupType])
values(142,300,'S_ADMN','Vehicle Listing Details','N','N',1,'/Masters/VehicleUploadListAll','ADM',N'ADM', 
 N'icon-list-ol text-warning',300, N'N', N'Y',101, N'N', N'Vehicle Listing Details',
 150, 17,NULL, NULL, NULL, 0,1, N'Y',NULL)
END 
IF NOT EXISTS (SELECT 1 FROM [MNT_Menus] WHERE [MenuId] = 301)
BEGIN
 Insert into MNT_Menus ([TId], [MenuId], [TabId], [DisplayTitle], [IsMenuItem], [IsJsMenuItem], 
[VirtualSource], [Hyp_Link_Address],[ProductName], [UserType], [Displayimg], [DisplayOrder],
[IsHeader], [SubMenu], [MainHeaderId], [IsAdmin], [AdminDisplayText],[MenuItemWidth], [MenuItemHeight],
[ErrorMessDesc], [ErrorMessTitle], [ErrorMessHead], [SubTabId], [LangId], [IsActive],[GroupType])
values(143,301,'S_ADMN','Bus Captain Listing','N','N',1,'/Masters/VehicleBusCaptainIndex','ADM',N'ADM', 
 N'icon-list-ol text-warning',301, N'N', N'Y',101, N'N', N'Bus Captain Listing',
 150, 17,NULL, NULL, NULL, 0,1, N'Y',NULL)
 END
IF NOT EXISTS (SELECT 1 FROM [MNT_Menus] WHERE [MenuId] = 302)
BEGIN
Insert into MNT_Menus ([TId], [MenuId], [TabId], [DisplayTitle], [IsMenuItem], [IsJsMenuItem], 
[VirtualSource], [Hyp_Link_Address],[ProductName], [UserType], [Displayimg], [DisplayOrder],
[IsHeader], [SubMenu], [MainHeaderId], [IsAdmin], [AdminDisplayText],[MenuItemWidth], [MenuItemHeight],
[ErrorMessDesc], [ErrorMessTitle], [ErrorMessHead], [SubTabId], [LangId], [IsActive],[GroupType])
values(144,302,'S_ADMN','Bus Captain Listing Details','N','N',1,'/Masters/VehicleBusCaptainEditor','ADM',N'ADM', 
 N'icon-list-ol text-warning',302, N'N', N'Y',101, N'N', N'Bus Captain Listing Details',
 150, 17,NULL, NULL, NULL, 0,1, N'Y',NULL)
END

IF EXISTS (SELECT 1 FROM [MNT_Menus] WHERE [MenuId] = 101)
BEGIN
 update MNT_Menus set IsHeader='Y' where MenuId=101
END

IF  EXISTS (SELECT 1 FROM [MNT_Menus] WHERE MenuId in(257,258))
BEGIN
update MNT_Menus set IsActive='N' where MenuId in(257,258)
END
IF  EXISTS (SELECT 1 FROM [MNT_Menus] WHERE MenuId in(267,268))
BEGIN
update MNT_Menus set IsActive='N' where MenuId in(267,268)
END

