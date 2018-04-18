--begin tran
IF  EXISTS(SELECT * FROM MNT_MENU_LIST WHERE MENU_ID = 534 and MENU_LINK='/cms/account/aspx/InterfaceFile.aspx')
begin
update MNT_MENU_LIST set IS_ACTIVE='N' where MENU_ID=534
end
--rollback tran
