IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetCustVehicleInfoPolicy]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetCustVehicleInfoPolicy]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- Drop proc Proc_GetCustVehicleInfoPolicy   
 CREATE PROCEDURE [dbo].[Proc_GetCustVehicleInfoPolicy]       
(      
@CUSTOMERID  int,      
@POLID        int,      
@POLVERSIONID  int      
)      
AS      
BEGIN      
      
 DECLARE @LOBID int, @STATID int      
 SELECT @LOBID = POLICY_LOB,@STATID = STATE_ID FROM POL_CUSTOMER_POLICY_LIST WHERE CUSTOMER_ID = @CUSTOMERID and POLICY_ID =  @POLID AND POLICY_VERSION_ID =  @POLVERSIONID      
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
 PL.APP_STATUS,
CASE WHEN UPPER(PL.APP_STATUS)='APPLICATION' THEN  PL.APP_NUMBER 
ELSE PL.POLICY_NUMBER END AS APPPOL_NUMBER,
CASE WHEN UPPER(PL.APP_STATUS)='APPLICATION' THEN  PL.APP_VERSION
ELSE PL.POLICY_DISP_VERSION END AS APPPOL_VERSION_ID,
 PL.POLICY_NUMBER,      
 PL.POLICY_DISP_VERSION, 
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
 FROM       POL_VEHICLES PV      
 INNER JOIN POL_CUSTOMER_POLICY_LIST PL      
 ON PV.CUSTOMER_ID = PL.CUSTOMER_ID AND PV.POLICY_ID = PL.POLICY_ID AND PV.POLICY_VERSION_ID = PL.POLICY_VERSION_ID      
 AND (PL.POLICY_LOB = @LOBID AND PL.STATE_ID=@STATID AND isnull(PV.IS_ACTIVE,'Y')='Y')    
 WHERE    PV.CUSTOMER_ID = @CUSTOMERID      
END      
  
  

GO

