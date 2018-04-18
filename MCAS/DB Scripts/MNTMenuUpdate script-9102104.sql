UPDATE [MNT_Menus] SET [IsActive] = 'N' WHERE [MenuId]='226'
UPDATE [MNT_Menus] SET [IsActive] = 'N' WHERE [MenuId]='227'
UPDATE [MNT_Menus] SET [IsActive] = 'N' WHERE [MenuId]='228'
UPDATE [MNT_Menus] SET [IsActive] = 'N' WHERE [MenuId]='229'
UPDATE [MNT_Menus] SET [IsActive] = 'N' WHERE [MenuId]='225'
UPDATE [MNT_Menus] SET [IsActive] = 'N' WHERE [MenuId]='230'
UPDATE [MNT_Menus] SET [IsActive] = 'N' WHERE [MenuId]='237'
UPDATE [MNT_Menus] SET [IsActive] = 'N' WHERE [MenuId]='239'
UPDATE [MNT_Menus] SET [AdminDisplayText] = 'Accident' WHERE [MenuId]='130'
UPDATE [MNT_Menus] SET [AdminDisplayText] = 'Own Damage' WHERE [MenuId]='131'
UPDATE [MNT_Menus] SET [AdminDisplayText] = 'PD/BI' WHERE [MenuId]='132'
UPDATE [MNT_Menus] SET [AdminDisplayText] = 'Notes' WHERE [MenuId]='133'
UPDATE [MNT_Menus] SET [AdminDisplayText] = 'Tasks' WHERE [MenuId]='134'
UPDATE [MNT_Menus] SET [AdminDisplayText] = 'Mandate' WHERE [MenuId]='135'
UPDATE [MNT_Menus] SET [AdminDisplayText] = 'Attachments' WHERE [MenuId]='136'
UPDATE [MNT_Menus] SET [AdminDisplayText] = 'Diary' WHERE [MenuId]='137'
UPDATE [MNT_Menus] SET [AdminDisplayText] = 'Reserve' WHERE [MenuId]='138'
UPDATE [MNT_Menus] SET [AdminDisplayText] = 'Payment' WHERE [MenuId]='139'
UPDATE [MNT_Menus] SET [AdminDisplayText] = 'Transactions History' WHERE [MenuId]='140'
update MNT_Menus set SubMenu='Y' where MenuId in(218,219)
update MNT_Menus set SubMenu='Y' where MenuId in(215,216)
update MNT_Menus set SubMenu='Y' where MenuId in(211,212,213)
update MNT_Menus set SubMenu='Y' where MenuId in(206,207,208,209)
update MNT_Menus set ProductName ='CLM' where TabId='CLAIM'
update MNT_Menus set ProductName='DRY' where MenuId=129

