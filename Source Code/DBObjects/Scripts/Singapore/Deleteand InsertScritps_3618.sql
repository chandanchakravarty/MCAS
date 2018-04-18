 
   delete from MNT_LOB_STATE where LOB_ID = 1
   
   go	  
   insert into MNT_LOB_STATE 
   select top 1 1,92,'' from MNT_LOB_STATE 
   
   go

   update MNT_LOOKUP_VALUES set LOOKUP_VALUE_DESC = 'Property' where LOOKUP_UNIQUE_ID = 8955

   go
 
   
   
   
