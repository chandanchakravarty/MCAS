IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetPolUmbrellaVehiclesForCoverage]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetPolUmbrellaVehiclesForCoverage]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/******************************************************************    
    
    
********************************************************************/    
--drop proc Proc_GetPolUmbrellaVehiclesForCoverage       
CREATE   PROCEDURE dbo.Proc_GetPolUmbrellaVehiclesForCoverage       
(      
@CUSTOMER_ID  int,      
@POLICY_ID               int,      
@POLICY_VERSION_ID  int      
      
)      
AS      
BEGIN      
DECLARE @STATE_ID INT      
DECLARE @LOB_ID int      
  
select @LOB_ID = POLICY_LOB,@STATE_ID = STATE_ID from   
 POL_CUSTOMER_POLICY_LIST   
where    
 CUSTOMER_ID = @CUSTOMER_ID and POLICY_ID =  @POLICY_ID AND POLICY_VERSION_ID =  @POLICY_VERSION_ID      
      
      
  
SELECT      
isnull(PV.CUSTOMER_ID ,0) as CUSTOMER_ID,      
isnull(PV.VEHICLE_ID,0) as VEHICLE_ID,        
isnull(PV.POLICY_ID ,0) as POLICY_ID,      
isnull(PV.POLICY_VERSION_ID ,0) as POLICY_VERSION_ID,      
isnull(PV.INSURED_VEH_NUMBER,0) as INSURED_VEH_NUMBER,       
isnull(PV.VEHICLE_YEAR,0) as VEHICLE_YEAR,      
isnull(PV.MAKE, '') as MAKE,      
isnull(PV.MODEL, '') as MODEL,      
isnull(PV.VIN, '') as VIN,      
PCPL.POLICY_NUMBER,      
PCPL.POLICY_DISP_VERSION,      
isnull(PV.BODY_TYPE, '') as BODY_TYPE,      
isnull(PV.GRG_ADD1, '') as GRG_ADD1,      
isnull(PV.GRG_ADD2, '') as GRG_ADD2,      
isnull(PV.GRG_CITY, '') as GRG_CITY,      
isnull(PV.GRG_COUNTRY, '') as GRG_COUNTRY,      
isnull(PV.GRG_STATE, '') as GRG_STATE,      
isnull(PV.GRG_ZIP, '') as GRG_ZIP,      
isnull(PV.REGISTERED_STATE, '') as REGISTERED_STATE,      
isnull(PV.TERRITORY, '') as TERRITORY,      
isnull(PV.CLASS, '') as CLASS,      
isnull(PV.REGN_PLATE_NUMBER, '') as REGN_PLATE_NUMBER,      
isnull(PV.ST_AMT_TYPE, 0) as ST_AMT_TYPE,      
isnull(PV.AMOUNT, 0) as AMOUNT,       
isnull(PV.SYMBOL, '') as SYMBOL,      
isnull(PV.VEHICLE_AGE, 0) as VEHICLE_AGE      
FROM       POL_UMBRELLA_VEHICLE_INFO PV      
INNER JOIN POL_CUSTOMER_POLICY_LIST PCPL      
ON PV.CUSTOMER_ID = PCPL.CUSTOMER_ID AND PV.POLICY_ID = PCPL.POLICY_ID AND PV.POLICY_VERSION_ID = PCPL.POLICY_VERSION_ID      
 AND PCPL.POLICY_LOB = @LOB_ID      
 AND PCPL.STATE_ID=@STATE_ID       
 AND isnull(PV.IS_ACTIVE,'Y')='Y'     
WHERE    PV.CUSTOMER_ID = @CUSTOMER_ID        
 ORDER BY PCPL.POLICY_NUMBER    
 end      
    
  



GO

