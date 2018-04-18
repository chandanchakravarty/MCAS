IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetPolicyDwellingInfoByID]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetPolicyDwellingInfoByID]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--drop proc Proc_GetPolicyDwellingInfoByID  
CREATE   PROCEDURE dbo.Proc_GetPolicyDwellingInfoByID            
(            
 @CUSTOMER_ID Int,            
 @POL_ID Int,            
 @POL_VERSION_ID SmallInt,            
 @DWELLING_ID SmallInt            
)            
        
As            
      
  
SELECT             
 PDI.CUSTOMER_ID ,            
 PDI.POLICY_ID,            
 PDI.POLICY_VERSION_ID,            
 PDI.DWELLING_ID ,            
 PDI.DWELLING_NUMBER,            
 PDI.LOCATION_ID,            
 PDI.SUB_LOC_ID ,            
 PDI.YEAR_BUILT,            
 PDI.PURCHASE_YEAR,            
 floor(PDI.PURCHASE_PRICE) PURCHASE_PRICE,            
 floor(PDI.MARKET_VALUE) MARKET_VALUE,            
 floor(PDI.REPLACEMENT_COST) REPLACEMENT_COST,            
 PDI.BUILDING_TYPE,            
 PDI.OCCUPANCY,            
 PDI.NEED_OF_UNITS,            
 PDI.USAGE,            
 PDI.NEIGHBOURS_VISIBLE,            
 --IS_VACENT_OCCUPY,            
 --IS_RENTED_IN_PART,            
 PDI.OCCUPIED_DAILY,            
 PDI.NO_WEEKS_RENTED,            
 --IS_DWELLING_OWNED_BY_OTHER            
 PDI.IS_ACTIVE,          
 PDI.CREATED_BY,            
 PDI.CREATED_DATETIME,            
 PDI.MODIFIED_BY,            
 PDI.LAST_UPDATED_DATETIME,            
 --IS_DWELLING_OWNED_BY_OTHER,            
 CONVERT(VARCHAR(10),PDI.LOCATION_ID) + ',' +             
 CONVERT(VARCHAR(10),ISNULL(PDI.SUB_LOC_ID,0)) as LOC_SUBLOC             
 --COMMENTDWELLINGOWNED             
 ,PDI.MONTHS_RENTED,    
 PDI.REPLACEMENTCOST_COVA, PL.LOC_NUM, 
CONVERT(VARCHAR(20), PL.LOC_NUM)+ '-' + ISNULL(PL.LOC_ADD1,'')+ ' ' + ISNULL(PL.LOC_ADD2,'')+ ',' + ISNULL(PL.LOC_CITY,'')+' ' 
+isnull((select STATE_NAME from MNT_COUNTRY_STATE_LIST   where STATE_ID = PL.LOC_STATE) ,'') + ' ' + ISNULL(PL.LOC_ZIP,'') AS LOCATION      
  
FROM POL_DWELLINGS_INFO PDI  
  
LEFT OUTER JOIN POL_LOCATIONS PL on  
  
PDI.LOCATION_ID = PL.LOCATION_ID  and pdi.customer_id=pl.customer_id and pdi.policy_id=pl.policy_id and pdi.policy_version_id=pl.policy_version_id        
  
WHERE PDI.CUSTOMER_ID = @CUSTOMER_ID AND            
  
      PDI.POLICY_ID = @POL_ID AND             
  
      PDI.POLICY_VERSION_ID = @POL_VERSION_ID AND            
  
      PDI.DWELLING_ID = @DWELLING_ID            
  


GO

