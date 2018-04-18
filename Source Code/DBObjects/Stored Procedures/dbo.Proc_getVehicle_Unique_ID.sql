IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_getVehicle_Unique_ID]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_getVehicle_Unique_ID]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------      
Proc Name           : Proc_getVehicle_Unique_ID      
Created by          : Praveen kasana     
Date                : 23 feb 2006      
Purpose             : To get the information for Symbol     
Revison History     :      
Used In             : Wolverine      
------------------------------------------------------------      
Date     Review By          Comments      
------   ------------       -------------------------*/  
CREATE PROC Proc_getVehicle_Unique_ID
(

  @LOOKUP_CODE nvarchar(40) ,
  @LOOKUP_VEHICLE_TYPE varchar(40) 
 
)
As
BEGIN
--1209 personal vehcle type
--1210 commercial vehcle type
Declare  @LOOKUP_UNIQUE_ID nvarchar(40)
Declare  @VEHICLE_TYPE varchar(20)

IF (@LOOKUP_VEHICLE_TYPE= 'PERSONAL')
Set @VEHICLE_TYPE ='1209' --Personal type
else
Set @VEHICLE_TYPE ='1210' --Commercial type


Select LOOKUP_UNIQUE_ID from mnt_lookup_values
where lookup_id = @VEHICLE_TYPE and lookup_value_code = @LOOKUP_CODE


END



--Proc_getVehicle_Unique_ID 'TR','PERSONAL'


GO

