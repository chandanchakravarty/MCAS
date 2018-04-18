IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetLocationInfoDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetLocationInfoDetails]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
/*----------------------------------------------------------                        
Proc Name       : dbo.[Proc_GetLocationInfoDetails]                
Created by      : Chetna Agarwal                       
Date            : 22 April,2010                        
Purpose         : To Get The Location Num and Address                        
Revison History :                        
Used In         : Brazil    
------------------------------------------------------------                        
Date     Review By          Comments                        
------   ------------       -------------------------        
          
*/       
-- DROP PROC dbo.[Proc_GetLocationInfoDetails] 28033                       
CREATE  PROC [dbo].[Proc_GetLocationInfoDetails] 
@CUSTOMER_ID int                  
AS        
BEGIN        
SELECT DISTINCT LOC_NUM ,     
 (ISNULL(CONVERT(VARCHAR(20),LOC_NUM),'')+' - ' +ISNULL(NAME,'')+' - '+ISNULL(LOC_ADD1,'')+', '+ISNULL(CONVERT(VARCHAR(50),NUMBER),'')+' ' +ISNULL(LOC_ADD2,'')+' - '+ISNULL(DISTRICT,'')+' - '+ ISNULL(LOC_CITY,'')+'/'+isnull(MNT_COUNTRY_STATE_LIST.STATE_CODE,'')+' - '+ISNULL(LOC_ZIP,''))  AS LOCATION_ADDRESS
FROM POL_LOCATIONS with (nolock)
left outer JOIN MNT_COUNTRY_STATE_LIST ON MNT_COUNTRY_STATE_LIST.STATE_ID=POL_LOCATIONS.LOC_STATE
where CUSTOMER_ID=@CUSTOMER_ID ORDER BY LOC_NUM  
    
END                  
        

GO

