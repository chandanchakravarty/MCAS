If Exists ( SELECT * FROM MNt_menus where TabId='DIARY' and MenuId in (286,287,288,289,290,291,292) )
  Begin
    update MNt_menus set Hyp_Link_Address='/UserAdmin/UnderConstruction' where MenuId in (286,287,288,289,290,291,292)
  End