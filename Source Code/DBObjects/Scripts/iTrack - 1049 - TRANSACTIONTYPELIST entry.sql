
 if  exists(select * from TRANSACTIONTYPELIST where TRANS_TYPE_ID=434)
begin
 update TRANSACTIONTYPELIST set TRANS_TYPE_NAME='Installment already paid', TRANS_TYPE_DESC='Installment already paid' where TRANS_TYPE_ID=434
end
go
if not exists(select * from TRANSACTIONTYPELIST where TRANS_TYPE_ID=442)
begin
Insert into TRANSACTIONTYPELIST (TRANS_TYPE_ID,TRANS_TYPE_NAME,TRANS_TYPE_DESC,TRANS_LEVEL,IS_ACTIVE)
 VALUES(442,'Previous installment not paid','Previous installment not paid','ACCOUNTING','Y')
end
go
if not exists(select * from TRANSACTIONTYPELIST where TRANS_TYPE_ID=443)
begin
Insert into TRANSACTIONTYPELIST (TRANS_TYPE_ID,TRANS_TYPE_NAME,TRANS_TYPE_DESC,TRANS_LEVEL,IS_ACTIVE)
 VALUES(443,'Refund related to this installment was already received','Refund related to this installment was already received','ACCOUNTING','Y')
end
go

---MULTILINGUAL

if exists(select * from TRANSACTIONTYPELIST_MULTILINGUAL where TRANS_TYPE_ID=434)
begin
 update TRANSACTIONTYPELIST_MULTILINGUAL set TRANS_TYPE_NAME='Parcela já está paga' where TRANS_TYPE_ID=434
end
go
if not exists(select * from TRANSACTIONTYPELIST_MULTILINGUAL where TRANS_TYPE_ID=442)
begin
insert into TRANSACTIONTYPELIST_MULTILINGUAL(TRANS_TYPE_ID,
TRANS_TYPE_NAME,
LANG_ID) values( 442,'Prestacao anterior pendente de pagamento' ,2)

end
go

if not exists(select * from TRANSACTIONTYPELIST_MULTILINGUAL where TRANS_TYPE_ID=443)
begin
insert into TRANSACTIONTYPELIST_MULTILINGUAL(TRANS_TYPE_ID,
TRANS_TYPE_NAME,
LANG_ID) values( 443,'Restituiçao referente a parcela indicada já foi recebida' ,2)
end
 
go
 