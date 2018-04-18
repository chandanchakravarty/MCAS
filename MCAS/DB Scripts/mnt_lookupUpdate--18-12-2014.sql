IF EXISTS (SELECT 1 FROM MNT_Lookups WITH(NOLOCK) WHERE Description='Both')
BEGIN
update MNT_Lookups set Lookupvalue=2,Lookupdesc='Both' where Description='Both'
END


