

if not exists(select * from TRANSACTIONTYPELIST where TRANS_TYPE_ID=444)
begin
Insert into TRANSACTIONTYPELIST (TRANS_TYPE_ID,TRANS_TYPE_NAME,TRANS_TYPE_DESC,TRANS_LEVEL,IS_ACTIVE)
 VALUES(444,'Refund related to this installment was already in progress','Refund related to this installment was already in progress','ACCOUNTING','Y')
end
go
 if not exists(select * from TRANSACTIONTYPELIST_MULTILINGUAL where TRANS_TYPE_ID=444)
begin
insert into TRANSACTIONTYPELIST_MULTILINGUAL(TRANS_TYPE_ID,
TRANS_TYPE_NAME,
LANG_ID) values( 444,'Restituiçao referente a parcela indicada já foi em andamento' ,2)

end

go
