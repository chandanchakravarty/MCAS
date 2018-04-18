If Not Exists( select * from MNT_Menus where MenuId=293 and TId=135 )
Begin
  Insert into MNT_Menus(TId ,MenuId  ,TabId ,DisplayTitle ,	IsMenuItem ,IsJsMenuItem ,VirtualSource ,
	                    Hyp_Link_Address ,ProductName,UserType,Displayimg ,DisplayOrder,IsHeader ,
	                    SubMenu,MainHeaderId,IsAdmin ,AdminDisplayText ,MenuItemWidth ,MenuItemHeight,
	                    ErrorMessDesc ,ErrorMessTitle,ErrorMessHead ,SubTabId ,LangId,IsActive
	                    )
   values             (135,293,'CLM_REC','Log Request','Y','N',1,'/ClaimRecoveryProcessing/ClaimLogRequest?claimMode=Recovery','CLMS','ADM','icon-double-angle-right',293,'N','Y',214,'N','Log Request',150	,17,NULL,NULL,NULL,0,1,'Y')
   
End   
