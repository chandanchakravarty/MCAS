IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_GETVINDATAFROMVINMASTER]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_GETVINDATAFROMVINMASTER]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                
Proc Name      : dbo.Proc_GetVINDataFromVINMASTER                
Created by       : Sumit Chhabra                
Date             : 15/11/2005                
Purpose       : retrieving data from vinmaster by vin      
Revison History :                
Used In        : Wolverine             
           
    JAC&J58X&Y    
JAC&N57X&Y    
JAC&S58X&Y    
JA3AA36G&Y    
JA3AA46G&Y    
JA3AA46L&Y    
JA3AA56L&Y    
JA3AC34G&Y    
select * from mnt_vin_master where vin='JA3AA56L&Y'     
select * from mnt_lookup_values where lookup_id=20    
------------------------------------------------------------                
Date     Review By          Comments                
------   ------------       -------------------------*/                
          
CREATE PROC DBO.PROC_GETVINDATAFROMVINMASTER                
@VINNUMBER   VARCHAR(10)          
    
AS             
/*  
DECLARE @AIRBAG_ID VARCHAR(10)       
DECLARE @AIRBAG VARCHAR(10)    
DECLARE @ANTI_LCK_BRAKES VARCHAR(10)       
DECLARE @SYMBOL VARCHAR(10)       
*/  
BEGIN          
    
select v.make_code,v.model_year,v.make_name,v.series_name,v.body_type,v.anti_lck_brakes,v.symbol,l.lookup_unique_id as AirBag from mnt_vin_master  v  left join mnt_lookup_values l    
 on v.airbag=l.lookup_value_code    
and l.lookup_id=20 and l.is_active='Y'    
where v.vin=@VINNUMBER


    
--SELECT @ANTI_LCK_BRAKES AS ANTI_LCK_BRAKES,@SYMBOL AS SYMBOL,@AIRBAG AS AIRBAG    
END    



GO

