IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetVINDetailsByVIN]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetVINDetailsByVIN]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                
Proc Name      : dbo.Proc_GetVINDetailsByVIN                
Created by       : Nidhi                
Date             : 6/4/2005                
Purpose       : retrieving data from vinmaster by vin                
Revison History :                
Used In        : Wolverine             
           
          
          
              
Modified By : Vijay Arora              
Modified Date : 24-10-2005              
Purpose : Commented the fields except VIN, Make_code, Model_year, Make_Name,         
Series_Name,  Body_type,Anti_lck_brakes       

Modified By : Praveen kasana            
Modified Date : 04-3-2006              
Purpose : Implemented hte check if Called from QQ and APP,get AIRBAG CODE in QQ          
Series_Name,  Body_type,Anti_lck_brakes              
drop proc Proc_GetVINDetailsByVIN
------------------------------------------------------------                
Date     Review By          Comments                
------   ------------       -------------------------*/                
          
CREATE PROC Dbo.Proc_GetVINDetailsByVIN                
@VIN  varchar(11)  ,  
@CALLEDFROM varchar(10)= null --If called from QQ and  APP  
AS                
BEGIN                
                
declare @vin1 varchar(10)          
declare @vin2 varchar(10)          
/*9th character has been replaced by & character */          
set @vin1=left(@vin,8) + '&' + substring(@vin,10,1)          
          
/*4th character has been replaced by & character..9th character has already been        
replaced with & already */          
set @vin2=left(@vin1,3) + '&' + substring(@vin1,5,6)          
     
/*         
select v.make_code,v.model_year,v.make_name,v.series_name,v.body_type,v.anti_lck_brakes,v.symbol,l.lookup_unique_id as AirBag from mnt_vin_master  v join mnt_lookup_values l          
 on v.airbag=l.lookup_value_code          
where l.lookup_id=20 and l.is_active='Y'          
and ((v.VIN like  @VIN1 + '%')  or (v.VIN like  @VIN2 + '%' ))         
*/         
if (@CALLEDFROM = 'QQ')  
BEGIN 

	declare @VIN_COUNT INT
	set @VIN_COUNT = 0
	SELECT @VIN_COUNT = count(V.VIN)
	FROM MNT_VIN_MASTER  V LEFT JOIN MNT_LOOKUP_VALUES L          
	ON V.AIRBAG=L.LOOKUP_VALUE_CODE          
	AND L.LOOKUP_ID=20 AND L.IS_ACTIVE='Y'     
	WHERE ((V.VIN LIKE  @VIN1 + '%')  OR (V.VIN LIKE  @VIN2 + '%' ))   
	    
	select v.make_code,v.model_year,v.make_name,v.series_name,v.body_type,
	v.anti_lck_brakes,v.symbol,l.lookup_value_code as AirBag,@VIN_COUNT as VIN_COUNT
	 from mnt_vin_master  v left join mnt_lookup_values l          
	 on v.airbag=l.lookup_value_code          
	and l.lookup_id=20 and l.is_active='Y'     
	where ((v.VIN like  @VIN1 + '%')  or (v.VIN like  @VIN2 + '%' ))  
END
ELSE  
BEGIN  
	select v.make_code,v.model_year,v.make_name,v.series_name,v.body_type,v.anti_lck_brakes,v.symbol,l.lookup_unique_id as AirBag from mnt_vin_master  v left join mnt_lookup_values l          
	on v.airbag=l.lookup_value_code          
	and l.lookup_id=20 and l.is_active='Y'     
	where ((v.VIN like  @VIN1 + '%')  or (v.VIN like  @VIN2 + '%' ))     
END      


         
END                


    
  




GO

