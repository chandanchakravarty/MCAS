IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetLEASEDPUR]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetLEASEDPUR]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                    
Proc Name       : dbo.Proc_GetLEASEDPUR
Created by      : Swarup                    
Date            : 12 July,2007                    
Purpose         : To Get The LEASEDPUR Codes                    
Revison History :                    
Used In         : Wolverine                    
------------------------------------------------------------                    
Date     Review By          Comments                    
------   ------------       -------------------------    
      
*/   
-- DROP PROC dbo.Proc_GetLEASEDPUR                     
CREATE  PROC dbo.Proc_GetLEASEDPUR                
AS    
BEGIN                
SELECT LOOKUP_UNIQUE_ID, LOOKUP_VALUE_ID,LOOKUP_VALUE_DESC FROM MNT_LOOKUP_VALUES WHERE LOOKUP_ID=1330  ORDER BY LOOKUP_VALUE_ID  
END              
    
      
 
    
  
   
    
    




GO

