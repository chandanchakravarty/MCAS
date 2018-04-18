IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetLoactionNumNAddress]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetLoactionNumNAddress]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                        
Proc Name       : dbo.[Proc_GetLoactionNumNAddress]                
Created by      : Pradeep                        
Date            : 31 Mar,2010                        
Purpose         : To Get The Location Num and Address                        
Revison History :                        
Used In         : Brazil    
------------------------------------------------------------                        
Date     Review By          Comments                        
------   ------------       -------------------------        
          
*/       
-- DROP PROC dbo.[Proc_GetLoactionNumNAddress]                         
CREATE  PROC [dbo].[Proc_GetLoactionNumNAddress]       
@CUSTOMER_ID int                           
AS        
BEGIN        
SELECT DISTINCT LOCATION_ID, (ISNULL(NAME,'')+' - '+ISNULL(LOC_ADD1,'')+', '+ISNULL(Convert(varchar(10), NUMBER),'')+' ' +ISNULL(LOC_ADD2,'')+' - '+ISNULL(DISTRICT,'')+' - '+ ISNULL(LOC_CITY,'')+'/'+MNT_COUNTRY_STATE_LIST.STATE_CODE+' - '+ISNULL(LOC_ZIP,''))  AS LOCATION_ADDRESS     
FROM POL_LOCATIONS WITH(NOLOCK) 
LEFT OUTER JOIN MNT_COUNTRY_STATE_LIST ON MNT_COUNTRY_STATE_LIST.STATE_ID=POL_LOCATIONS.LOC_STATE
 where CUSTOMER_ID=@CUSTOMER_ID ORDER BY LOCATION_ID    
    
END                  
        
          
GO

