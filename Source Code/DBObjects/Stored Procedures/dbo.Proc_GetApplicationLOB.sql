IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetApplicationLOB]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetApplicationLOB]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------    
Proc Name          : Dbo.Proc_GetApplicationLOB    
Created by           : Nidhi    
Date                    : 08/06/2005    
Purpose               :     
Revison History :    
Used In                :   Wolverine    
------------------------------------------------------------    
Date     Review By          Comments    
------   ------------       -------------------------*/    
--DROP PROC Dbo.Proc_GetApplicationLOB    
CREATE PROC [dbo].[Proc_GetApplicationLOB]    
(    
    
 @CUSTOMER_ID int,    
 @APP_ID int,    
 @APP_VERSION_ID int,
 @CALLED_FROM varchar(30) = null   
)    
    
AS    
BEGIN    
 
IF ISNULL(@CALLED_FROM,'') <> 'POLICY'   
SELECT isnull(APP_LOB ,0)as LOBID,APP_NUMBER,APP_VERSION,CONVERT(NVARCHAR(20),isnull(APP_EFFECTIVE_DATE,''),101) AS APPEFFECTIVEDATE,isnull(MNT_COUNTRY_STATE_LIST.STATE_NAME,'') AS STATENAME  FROM APP_LIST WITH(NOLOCK) 
INNER JOIN MNT_COUNTRY_STATE_LIST WITH(NOLOCK) 
ON 
APP_LIST.STATE_ID = MNT_COUNTRY_STATE_LIST.STATE_ID
    
WHERE CUSTOMER_ID =@CUSTOMER_ID and APP_ID=@APP_ID and APP_VERSION_ID=@APP_VERSION_ID    

ELSE
SELECT isnull(POLICY_LOB ,0) as LOBID,POLICY_NUMBER,POLICY_DISP_VERSION,CONVERT(NVARCHAR(20),ISNULL(APP_EFFECTIVE_DATE,''),101) AS APPEFFECTIVEDATE,ISNULL(MNT_COUNTRY_STATE_LIST.STATE_NAME,'') AS STATENAME
FROM POL_CUSTOMER_POLICY_LIST WITH(NOLOCK)  
INNER JOIN MNT_COUNTRY_STATE_LIST WITH(NOLOCK) 
ON 
POL_CUSTOMER_POLICY_LIST.STATE_ID = MNT_COUNTRY_STATE_LIST.STATE_ID
   
WHERE CUSTOMER_ID =@CUSTOMER_ID and POLICY_ID=@APP_ID and POLICY_VERSION_ID=@APP_VERSION_ID    

    
END  






GO

