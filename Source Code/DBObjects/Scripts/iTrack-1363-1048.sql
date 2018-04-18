
if not exists(select * from TRANSACTIONTYPELIST where TRANS_TYPE_ID=433)
begin
Insert into TRANSACTIONTYPELIST (TRANS_TYPE_ID,TRANS_TYPE_NAME,TRANS_TYPE_DESC,TRANS_LEVEL,IS_ACTIVE)
 VALUES(433,'Process is not committed in the system.','Process is not committed in the system.','ACCOUNTING','Y')
end
go
 
 
---MULTILINGUAL
if not exists(select * from TRANSACTIONTYPELIST_MULTILINGUAL where TRANS_TYPE_ID=433)
begin
insert into TRANSACTIONTYPELIST_MULTILINGUAL(TRANS_TYPE_ID,
TRANS_TYPE_NAME,
LANG_ID) values( 433,'Process is not committed in the system.' ,2)

end
GO


if not exists(select * from TRANSACTIONTYPELIST where TRANS_TYPE_ID=434)
begin
Insert into TRANSACTIONTYPELIST (TRANS_TYPE_ID,TRANS_TYPE_NAME,TRANS_TYPE_DESC,TRANS_LEVEL,IS_ACTIVE)
 VALUES(434,'Payment against installment already paid.','Payment against installment already paid.','ACCOUNTING','Y')
end
go
 
 
---MULTILINGUAL
if not exists(select * from TRANSACTIONTYPELIST_MULTILINGUAL where TRANS_TYPE_ID=434)
begin
insert into TRANSACTIONTYPELIST_MULTILINGUAL(TRANS_TYPE_ID,
TRANS_TYPE_NAME,
LANG_ID) values( 434,'Pagamento para prestacao paga.' ,2)

end
GO

if not exists(select * from TRANSACTIONTYPELIST where TRANS_TYPE_ID=435)
begin
Insert into TRANSACTIONTYPELIST (TRANS_TYPE_ID,TRANS_TYPE_NAME,TRANS_TYPE_DESC,TRANS_LEVEL,IS_ACTIVE)
 VALUES(435,'Payment against installment already in progress.','Payment against installment already in progress.','ACCOUNTING','Y')
end
go
 
 
---MULTILINGUAL
if not exists(select * from TRANSACTIONTYPELIST_MULTILINGUAL where TRANS_TYPE_ID=435)
begin
insert into TRANSACTIONTYPELIST_MULTILINGUAL(TRANS_TYPE_ID,
TRANS_TYPE_NAME,
LANG_ID) values( 435,'Payment against installment already in progress.' ,2)

end
GO
 
--Total Premium Collection is not equal to Net Installment Amount.
if not exists(select * from TRANSACTIONTYPELIST where TRANS_TYPE_ID=436)
begin
Insert into TRANSACTIONTYPELIST (TRANS_TYPE_ID,TRANS_TYPE_NAME,TRANS_TYPE_DESC,TRANS_LEVEL,IS_ACTIVE)
 VALUES(436,'Total Premium Collection is not equal to Net Installment Amount.','Total Premium Collection is not equal to Net Installment Amount.','ACCOUNTING','Y')
end
go
 
 
---MULTILINGUAL
if not exists(select * from TRANSACTIONTYPELIST_MULTILINGUAL where TRANS_TYPE_ID=436)
begin
insert into TRANSACTIONTYPELIST_MULTILINGUAL(TRANS_TYPE_ID,
TRANS_TYPE_NAME,
LANG_ID) values( 436,'Total Premium Collection is not equal to Net Installment Amount.' ,2)

end
GO
 

  