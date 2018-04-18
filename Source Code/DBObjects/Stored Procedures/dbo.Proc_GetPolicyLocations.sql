IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetPolicyLocations]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetPolicyLocations]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                        
Proc Name       : dbo.Proc_GetPolicyLocations                        
Created by      : Vijay Arora                        
Date            : 5/2/2006                        
Purpose      : To get the records from Pol_Location table for Claims.                      
Revison History :                        
Used In  : Wolverine                        
------------------------------------------------------------                        
Date     Review By          Comments                        
------   ------------       -------------------------*/                        
--DROP PROC dbo.Proc_GetPolicyLocations                     
CREATE PROC dbo.Proc_GetPolicyLocations                        
(                        
@CLAIM_ID     int,          
@LOCATION_ID int  = null                       
)                        
AS                        
BEGIN                        
                      
DECLARE @CUSTOMER_ID int                      
DECLARE @POLICY_ID int                      
DECLARE @POLICY_VERSION_ID int                      
DECLARE @LOB_ID int            
                      
SELECT @CUSTOMER_ID=CCI.CUSTOMER_ID, @POLICY_ID = CCI.POLICY_ID, @POLICY_VERSION_ID = CCI.POLICY_VERSION_ID,          
 @LOB_ID = PCPL.POLICY_LOB          
 FROM CLM_CLAIM_INFO CCI JOIN POL_CUSTOMER_POLICY_LIST PCPL ON           
 CCI.CUSTOMER_ID = PCPL.CUSTOMER_ID AND          
 CCI.POLICY_ID = PCPL.POLICY_ID AND          
 CCI.POLICY_VERSION_ID = PCPL.POLICY_VERSION_ID            
 WHERE CLAIM_ID = @CLAIM_ID                      
                      
            
IF(@LOB_ID=5)--when LOB is umbrella, fetch the data from different table            
begin          
 if(@LOCATION_ID IS NOT NULL AND @LOCATION_ID<>0)            
  SELECT             
   LOCATION_ID, '' AS DESCRIPTION,ADDRESS_1, ADDRESS_2, CITY,STATE,ZIPCODE,1 AS COUNTRY,LOCATION_NUMBER AS LOC_NUM               
 FROM             
   POL_UMBRELLA_REAL_ESTATE_LOCATION             
 WHERE             
  CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID AND POLICY_VERSION_ID = @POLICY_VERSION_ID AND LOCATION_ID=@LOCATION_ID          
 AND IS_ACTIVE='Y'          
 else          
  SELECT             
    LOCATION_ID, '' AS DESCRIPTION,ADDRESS_1, ADDRESS_2, CITY,STATE,ZIPCODE,1 AS COUNTRY,LOCATION_NUMBER AS LOC_NUM               
  FROM             
    POL_UMBRELLA_REAL_ESTATE_LOCATION             
  WHERE             
   CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID AND POLICY_VERSION_ID = @POLICY_VERSION_ID AND IS_ACTIVE='Y'                
end          
else            
begin          
 if(@LOCATION_ID IS NOT NULL AND @LOCATION_ID<>0)          
  SELECT             
    LOCATION_ID, DESCRIPTION,LOC_ADD1 AS ADDRESS_1, LOC_ADD2 AS ADDRESS_2, LOC_CITY AS CITY,LOC_STATE AS STATE,        
  LOC_ZIP AS ZIPCODE,LOC_COUNTRY AS COUNTRY,LOC_NUM                
  FROM             
    POL_LOCATIONS             
  WHERE             
    CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID AND POLICY_VERSION_ID = @POLICY_VERSION_ID AND LOCATION_ID=@LOCATION_ID          
    AND IS_ACTIVE='Y'              
 else          
	SELECT PL.LOCATION_ID, [DESCRIPTION],LOC_ADD1 AS ADDRESS_1, LOC_ADD2 AS ADDRESS_2, LOC_CITY AS CITY,LOC_NUM,
				LOC_STATE AS STATE,LOC_ZIP AS ZIPCODE,LOC_COUNTRY AS COUNTRY,MNT_COUNTRY_STATE_LIST.STATE_NAME AS STATE_NAME
	FROM 
		POL_LOCATIONS PL
	JOIN 
		POL_DWELLINGS_INFO PDI 
	ON 
		PDI.CUSTOMER_ID=PL.CUSTOMER_ID AND PDI.POLICY_ID=PL.POLICY_ID AND 
		PDI.POLICY_VERSION_ID = PL.POLICY_VERSION_ID AND PDI.LOCATION_ID=PL.LOCATION_ID     
	LEFT OUTER JOIN 
		MNT_COUNTRY_STATE_LIST 
	ON 
		PL.LOC_STATE = MNT_COUNTRY_STATE_LIST.STATE_ID      
	WHERE 
		PL.CUSTOMER_ID = @CUSTOMER_ID AND 
		PL.POLICY_ID = @POLICY_ID AND 
		PL.POLICY_VERSION_ID = @POLICY_VERSION_ID AND 
		PL.IS_ACTIVE='Y' AND 
		PDI.IS_ACTIVE='Y' 		
		--Done for Itrack Issue 6892 on 18 Jan 2010
		AND PL.LOCATION_ID NOT IN (SELECT ISNULL(POLICY_LOCATION_ID,0) FROM CLM_INSURED_LOCATION WHERE CLAIM_ID=@CLAIM_ID AND IS_ACTIVE='Y')              
	end          
END                        
    

GO

