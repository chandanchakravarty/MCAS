 
 IF EXISTS(Select * from MNT_COVERAGE_EXTRA_MULTILINGUAL where COV_ID=50018 and LANG_ID=2)
 BEGIN
 Update MNT_COVERAGE_EXTRA_MULTILINGUAL
 set COV_DES='Honorários' 
 where COV_ID=50018 and LANG_ID=2
 END
