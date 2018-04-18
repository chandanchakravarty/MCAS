IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetApplicationDDL]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetApplicationDDL]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------    
Proc Name          : Dbo.Proc_GetApplicationDDL    
Created by         : Pradeep    
Date               : Sep 21, 2005    
Purpose            : Gets the result sets to populate drop down lists in    
   the App Gen Info page    
Revison History    :    
Used In            :   Wolverine    
------------------------------------------------------------    
Date     Review By          Comments    
------   ------------       -------------------------*/    
CREATE  PROC Dbo.Proc_GetApplicationDDL    
(    
 @AGENCY_CODE NVarChar(8)  
)    
    
AS    
    
--Get Yes No from Lookup    
exec Proc_GetLookupValues @LookupCode = N'yesno'    
    
--Get Agency List    
IF ( @AGENCY_CODE = '' )    
BEGIN    
    
    
 SELECT AGENCY_ID,AGENCY_CODE + ' - ' +     
  AGENCY_DISPLAY_NAME as AGENCY_DISPLAY_NAME      
 FROM MNT_AGENCY_LIST     
 ORDER BY AGENCY_DISPLAY_NAME      
END    
ELSE    
BEGIN    
 SELECT AGENCY_ID,AGENCY_CODE + ' - ' +     
 AGENCY_DISPLAY_NAME as AGENCY_DISPLAY_NAME      
 FROM MNT_AGENCY_LIST     
 WHERE AGENCY_CODE = @AGENCY_CODE    
 ORDER BY AGENCY_DISPLAY_NAME      
    
END    
    
--Get Installemnt plans
SELECT   
IDEN_PLAN_ID as INSTALL_PLAN_ID,  
PLAN_CODE
FROM 
 act_install_plan_detail   
where isnull(IS_ACTIVE,'Y') <>'N'  
order by plan_code    



GO

