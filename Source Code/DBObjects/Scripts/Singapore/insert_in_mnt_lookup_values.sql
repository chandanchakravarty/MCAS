

insert into MNT_LOOKUP_VALUES values(
(select MAX(lookup_unique_id)+1 from MNT_LOOKUP_VALUES),'940',
(select max(lookup_value_id)+1 from MNT_LOOKUP_VALUES where LOOKUP_ID=940),
 'CBC','Cash Before Cover',1,'Y','','','DB','')



insert into MNT_LOOKUP_VALUES values(
(select MAX(lookup_unique_id)+1 from MNT_LOOKUP_VALUES),'940',
(select max(lookup_value_id)+1 from MNT_LOOKUP_VALUES where LOOKUP_ID=940),
 'CR','Credit',1,'Y','','','DB','')
 
 
 
insert into MNT_LOOKUP_VALUES values(
(select MAX(lookup_unique_id)+1 from MNT_LOOKUP_VALUES),'940',
(select max(lookup_value_id)+1 from MNT_LOOKUP_VALUES where LOOKUP_ID=940),
 'IN','Installment',1,'Y','','','DB','')



select * from MNT_LOOKUP_VALUES order by LOOKUP_UNIQUE_ID desc
select MAX(lookup_unique_id)+1 from MNT_LOOKUP_VALUES
select max(lookup_value_id)+1 from MNT_LOOKUP_VALUES where LOOKUP_ID=940

