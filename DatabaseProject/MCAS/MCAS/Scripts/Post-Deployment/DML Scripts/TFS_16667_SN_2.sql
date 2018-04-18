if exists(select Top 1* from MNT_Menus where MenuId=302)
Begin
update MNT_Menus set VirtualSource='MCAS.Web.Objects.Resources.Masters.BusCaptain'  where MenuId=302
End



if exists(select Top 1* from MNT_Menus where MenuId=301)
Begin
update MNT_Menus set VirtualSource='MCAS.Web.Objects.Resources.Masters.BusCaptain'  where MenuId=301
End
