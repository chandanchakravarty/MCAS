IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetXMLPOL_OTHER_LOCATIONS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetXMLPOL_OTHER_LOCATIONS]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------      
Proc Name       : dbo.Proc_GetXMLPOL_OTHER_LOCATIONS  
Created by      : Swastika Gaur  
Date            : 20th jun'06  
Purpose         : Selects the location for a POLICY (Homeowner)  
Revison History :      
Used In         : Wolverine      
------------------------------------------------------------      
Date     Review By          Comments      
------   ------------       -------------------------     
*/  
-- drop proc dbo.Proc_GetXMLPOL_OTHER_LOCATIONS  
CREATE PROCEDURE dbo.Proc_GetXMLPOL_OTHER_LOCATIONS  
(  
 @CUSTOMER_ID Int,  
 @POLICY_ID Int,  
 @POLICY_VERSION_ID smallint,  
 @LOCATION_ID int  
)  
As  
SELECT *  
FROM POL_OTHER_LOCATIONS AL (NOLOCK)  
LEFT OUTER JOIN MNT_COUNTRY_STATE_LIST SL ON AL.LOC_STATE = SL.STATE_ID  
WHERE AL.CUSTOMER_ID = @CUSTOMER_ID AND  
      AL.POLICY_ID = @POLICY_ID AND  
      AL.POLICY_VERSION_ID = @POLICY_VERSION_ID  AND  
      AL.LOCATION_ID = @LOCATION_ID  
  
  


GO

