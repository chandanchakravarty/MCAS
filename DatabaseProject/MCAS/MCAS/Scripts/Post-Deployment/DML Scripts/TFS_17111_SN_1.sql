Update MNT_Lookups set Lookupdesc='Alerts' where Category='TaskSelection' and Lookupdesc='Diary'
Update MNT_TableDesc set TableDesc='Alerts' where TableName in('TODODIARYLIST' ,'TODODIARYLISTs')
Update MNT_TableDesc set TableDesc='Alerts Reassignment' where TableDesc='ReAssignmentDairy' and TableName='Claim_ReAssignmentDairy' 

Update MNT_Menus set AdminDisplayText='Alert',DisplayTitle='Alert' where  MenuId in(136,281)
