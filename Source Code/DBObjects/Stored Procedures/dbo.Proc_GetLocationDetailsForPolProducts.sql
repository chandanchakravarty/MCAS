IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetLocationDetailsForPolProducts]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetLocationDetailsForPolProducts]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                      
Proc Name       : dbo.Proc_GetLocationDetailsForPolProducts              
Created by      : Pradeep                      
Date            : 19 May,2010                      
Purpose         : To Get The Building Name,Address,Number,Compliment,District,City
Revison History :                      
Used In         : Brazil  
------------------------------------------------------------                      
Date     Review By          Comments                      
------   ------------       -------------------------      
*/     
-- DROP PROC dbo.Proc_GetLocationDetailsForPolProducts 2043,717                  
CREATE  PROC [dbo].[Proc_GetLocationDetailsForPolProducts]     
@CUSTOMER_ID INT, 
@POLICY_ID INT                   
AS      
BEGIN      
DECLARE @POLICY_LOBID INT
SELECT  TOP 1 @POLICY_LOBID=POLICY_LOB FROM POL_CUSTOMER_POLICY_LIST WITH(NOLOCK) WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID
IF(@POLICY_LOBID IN (14,26,27,12,16))
	BEGIN 
		SELECT DISTINCT LOCATION_ID, (ISNULL(NAME,'')+' - '+ISNULL(LOC_ADD1,'')+', '+ISNULL(CONVERT(VARCHAR(10), NUMBER),'')+' ' +ISNULL(LOC_ADD2,'')+' - '+ISNULL(DISTRICT,'')+' - '+ ISNULL(LOC_CITY,'')+'/'+MNT_COUNTRY_STATE_LIST.STATE_CODE+' - '+ISNULL(LOC_ZIP,''))  AS LOCATION_ADDRESS   
		FROM POL_LOCATIONS  WITH(NOLOCK)LEFT OUTER JOIN MNT_COUNTRY_STATE_LIST  WITH(NOLOCK) ON MNT_COUNTRY_STATE_LIST.STATE_ID=POL_LOCATIONS.LOC_STATE and POL_LOCATIONS.LOC_COUNTRY=MNT_COUNTRY_STATE_LIST.COUNTRY_ID
		WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POL_LOCATIONS.IS_ACTIVE='Y' 
		
	END
ELSE
BEGIN 
		SELECT DISTINCT LOCATION_ID, (ISNULL(NAME,'')+' - '+ISNULL(LOC_ADD1,'')+', '+ISNULL(CONVERT(VARCHAR(10), NUMBER),'')+' ' +ISNULL(LOC_ADD2,'')+' - '+ISNULL(DISTRICT,'')+' - '+ ISNULL(LOC_CITY,'')+'/'+MNT_COUNTRY_STATE_LIST.STATE_CODE+' - '+ISNULL(LOC_ZIP,''))  AS LOCATION_ADDRESS   
		FROM POL_LOCATIONS  WITH(NOLOCK)LEFT OUTER JOIN MNT_COUNTRY_STATE_LIST  WITH(NOLOCK) ON MNT_COUNTRY_STATE_LIST.STATE_ID=POL_LOCATIONS.LOC_STATE and POL_LOCATIONS.LOC_COUNTRY=MNT_COUNTRY_STATE_LIST.COUNTRY_ID 
		WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POL_LOCATIONS.IS_ACTIVE='Y' 
		AND LOCATION_ID NOT IN (SELECT DISTINCT LOCATION  FROM POL_PRODUCT_LOCATION_INFO WITH(NOLOCK) WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID)  ORDER BY LOCATION_ID  
END	
  
END                
      
        
       
GO

