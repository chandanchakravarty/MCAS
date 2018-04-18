
---itrack-1422
If exists(select * from MNT_LOOKUP_VALUES_MULTILINGUAL where LOOKUP_UNIQUE_ID=14722)
Begin
Update MNT_LOOKUP_VALUES_MULTILINGUAL
set LOOKUP_VALUE_DESC='Julgamento favorável à Cia. em demais instâncias'
where LOOKUP_UNIQUE_ID=14722
End

If exists(select * from MNT_LOOKUP_VALUES_MULTILINGUAL where LOOKUP_UNIQUE_ID=14723)
Begin
Update MNT_LOOKUP_VALUES_MULTILINGUAL
set LOOKUP_VALUE_DESC='Julgamento desfavorável à Cia. em demais instâncias'
where LOOKUP_UNIQUE_ID=14723
End


If exists(select * from MNT_LOOKUP_VALUES_MULTILINGUAL where LOOKUP_UNIQUE_ID=14724)
Begin
Update MNT_LOOKUP_VALUES_MULTILINGUAL
set LOOKUP_VALUE_DESC='Julgamento favorável à Cia. transitado e julgado'
where LOOKUP_UNIQUE_ID=14724
End

