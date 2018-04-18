IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetCustVehicleInfoForCoverage]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetCustVehicleInfoForCoverage]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



--Drop Proc Proc_GetCustVehicleInfoForCoverage                                                                                                                                                                                                                                                            
/*----------------------------------------------------------  
Proc Name           : Dbo.Proc_GetCustVehicleInfoForCoverage  
Created by            : Mohit Gupta  
Date                    : 05-05-2005  
Purpose                :   
Revison History  :  
Used In                 :   Wolverine    
-------------------------------------------------------------  
Modified By   : Shafee  
Modified Date  : 09-01-2006  
Purpose   : Added the State ID Condition.  
------------------------------------------------------------  
Modified By    : Ashwani  
Modified Date  : 06 Feb. 2006 
Purpose        : Added Is_Active condition.
------------------------------------------------------------  
Date     Review By          Comments  
----------   ------------       ----------------------------*/   
CREATE PROCEDURE Proc_GetCustVehicleInfoForCoverage     
(    
@CUSTOMERID  int,    
@APPID        int,    
@APPVERSIONID  int    
    
)    
AS    
BEGIN    
    
DECLARE @LOBID int    
DECLARE @STATID INT  
select @LOBID = APP_LOB from APP_LIST where  CUSTOMER_ID = @CUSTOMERID and APP_ID =  @APPID AND APP_VERSION_ID =  @APPVERSIONID    
SELECT @STATID = STATE_ID FROM APP_LIST WHERE  CUSTOMER_ID = @CUSTOMERID AND APP_ID =  @APPID AND APP_VERSION_ID =  @APPVERSIONID      
    
SELECT    
isnull(AV.CUSTOMER_ID ,0) as CUSTOMER_ID,    
isnull(AV.VEHICLE_ID,0) as VEHICLE_ID,      
isnull(AV.APP_ID ,0) as APP_ID,    
isnull(AV.APP_VERSION_ID ,0) as APP_VERSION_ID,    
isnull(AV.INSURED_VEH_NUMBER,0) as INSURED_VEH_NUMBER,     
isnull(AV.VEHICLE_YEAR,0) as VEHICLE_YEAR,    
isnull(AV.MAKE, '') as MAKE,    
isnull(AV.MODEL, '') as MODEL,    
isnull(AV.VIN, '') as VIN,    
AL.APP_NUMBER,    
AL.APP_VERSION,    
isnull(AV.BODY_TYPE, '') as BODY_TYPE,    
isnull(AV.GRG_ADD1, '') as GRG_ADD1,    
isnull(AV.GRG_ADD2, '') as GRG_ADD2,    
isnull(AV.GRG_CITY, '') as GRG_CITY,    
isnull(AV.GRG_COUNTRY, '') as GRG_COUNTRY,    
isnull(AV.GRG_STATE, '') as GRG_STATE,    
isnull(AV.GRG_ZIP, '') as GRG_ZIP,    
isnull(AV.REGISTERED_STATE, '') as REGISTERED_STATE,    
isnull(AV.TERRITORY, '') as TERRITORY,    
isnull(AV.CLASS, '') as CLASS,    
isnull(AV.REGN_PLATE_NUMBER, '') as REGN_PLATE_NUMBER,    
isnull(AV.ST_AMT_TYPE, 0) as ST_AMT_TYPE,    
isnull(AV.AMOUNT, 0) as AMOUNT,     
isnull(AV.SYMBOL, '') as SYMBOL,    
isnull(AV.VEHICLE_AGE, 0) as VEHICLE_AGE    
FROM       APP_VEHICLES AV    
INNER JOIN APP_LIST AL    
ON AV.CUSTOMER_ID = AL.CUSTOMER_ID AND AV.APP_ID = AL.APP_ID AND AV.APP_VERSION_ID = AL.APP_VERSION_ID    
AND (AL.APP_LOB = @LOBID    
-- Added by Shafee  
AND AL.STATE_ID=@STATID 
-- isnull(AV.IS_ACTIVE,'Y')='Y' is Added by Ashwani on 06 Feb. 2006
AND isnull(AV.IS_ACTIVE,'Y')='Y')  
WHERE    AV.CUSTOMER_ID = @CUSTOMERID    
--  AND (  APP_ID <>  @APPID OR  (APP_ID =  @APPID AND APP_VERSION_ID <>  @APPVERSIONID) )     
-- Commeted by Rajan on 04 Jun 2005    
ORDER BY AL.APP_NUMBER -- Added by Swastika on 13th Apr'06 for GI #2579
END    
  





GO

