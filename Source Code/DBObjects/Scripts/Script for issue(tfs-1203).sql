if not exists(select * from CLM_TYPE_DETAIL_multilingual where DETAIL_TYPE_ID=621)
begin
insert into CLM_TYPE_DETAIL_multilingual(DETAIL_TYPE_ID,TYPE_DESC,LANG_ID)values(621,'Outras',2)
End

if not exists(select * from CLM_TYPE_DETAIL_multilingual where DETAIL_TYPE_ID=629)
begin
insert into CLM_TYPE_DETAIL_multilingual(DETAIL_TYPE_ID,TYPE_DESC,LANG_ID)values(629,'Olá',2)
End


if not exists(select * from CLM_TYPE_DETAIL_multilingual where DETAIL_TYPE_ID=630)
begin
insert into CLM_TYPE_DETAIL_multilingual(DETAIL_TYPE_ID,TYPE_DESC,LANG_ID)values(630,'esta é a minha descrição',2)
End


if not exists(select * from CLM_TYPE_DETAIL_multilingual where DETAIL_TYPE_ID=634)
begin
insert into CLM_TYPE_DETAIL_multilingual(DETAIL_TYPE_ID,TYPE_DESC,LANG_ID)values(634,'PERDA_TIPO_CÓDIGO',2)
End


