
if exists(Select * from CLM_TYPE_DETAIL_MULTILINGUAL where DETAIL_TYPE_ID=28)
begin
Update CLM_TYPE_DETAIL_MULTILINGUAL
set TYPE_DESC='Colis�o (Animal)' where DETAIL_TYPE_ID=28
End


if exists(Select * from CLM_TYPE_DETAIL_MULTILINGUAL where DETAIL_TYPE_ID=44)
begin
Update CLM_TYPE_DETAIL_MULTILINGUAL
set TYPE_DESC='Roubo (n�o locais)' where DETAIL_TYPE_ID=44
End

if exists(Select * from CLM_TYPE_DETAIL_MULTILINGUAL where DETAIL_TYPE_ID=38)
begin
Update CLM_TYPE_DETAIL_MULTILINGUAL
set TYPE_DESC='Desaparecimento misterioso (n�o locais)' where DETAIL_TYPE_ID=38
End

if exists(Select * from CLM_TYPE_DETAIL_MULTILINGUAL where DETAIL_TYPE_ID=30)
begin
Update CLM_TYPE_DETAIL_MULTILINGUAL
set TYPE_DESC='Sismo/desmoronamento' where DETAIL_TYPE_ID=30
End

if exists(Select * from CLM_TYPE_DETAIL_MULTILINGUAL where DETAIL_TYPE_ID=480)
begin
Update CLM_TYPE_DETAIL_MULTILINGUAL
set TYPE_DESC='IPD -SIDA' where DETAIL_TYPE_ID=480
End

if exists(Select * from CLM_TYPE_DETAIL_MULTILINGUAL where DETAIL_TYPE_ID=301)
begin
Update CLM_TYPE_DETAIL_MULTILINGUAL
set TYPE_DESC='MN -SIDA' where DETAIL_TYPE_ID=301
End


