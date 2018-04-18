 
if not exists(select * from TRANSACTIONTYPELIST where TRANS_TYPE_ID=437)
begin
Insert into TRANSACTIONTYPELIST (TRANS_TYPE_ID,TRANS_TYPE_NAME,TRANS_TYPE_DESC,TRANS_LEVEL,IS_ACTIVE)
 VALUES(437,'Receipt date cannot be future date.','Receipt date cannot be future date.','ACCOUNTING','Y')
end
go
 
 
---MULTILINGUAL
if not exists(select * from TRANSACTIONTYPELIST_MULTILINGUAL where TRANS_TYPE_ID=437)
begin
insert into TRANSACTIONTYPELIST_MULTILINGUAL(TRANS_TYPE_ID,
TRANS_TYPE_NAME,
LANG_ID) values( 437,'Data de Depósito não pode ser uma data future.' ,2)

end
GO
 