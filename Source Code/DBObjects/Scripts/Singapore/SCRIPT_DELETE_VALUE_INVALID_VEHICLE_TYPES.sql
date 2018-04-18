delete from POL_VEHICLE_ENDORSEMENTS where VEHICLE_ID in (select VEHICLE_ID from POL_VEHICLES where BODY_TYPE LIKE '%[^0-9]%')
delete from POL_VEHICLE_COVERAGES where VEHICLE_ID in (select VEHICLE_ID from POL_VEHICLES where BODY_TYPE LIKE '%[^0-9]%')
delete from POL_ADD_OTHER_INT where VEHICLE_ID in (select VEHICLE_ID from POL_VEHICLES where BODY_TYPE LIKE '%[^0-9]%')