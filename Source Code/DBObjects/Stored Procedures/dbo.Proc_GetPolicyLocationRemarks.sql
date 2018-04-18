IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetPolicyLocationRemarks]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetPolicyLocationRemarks]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------  
Proc Name          	: Dbo.Proc_GetPolicyLocationRemarks
Created by           	: Ravindra
Date                    : 03-21-206
Purpose               	: To get remarks  from POL_UMBRELLA_REAL_ESTATE_LOCATION  table  
Revison History 	:  
Used In           	:   Wolverine  
------------------------------------------------------------  
Date     Review By          Comments  
------   ------------       -------------------------*/  
CREATE PROC Dbo.Proc_GetPolicyLocationRemarks
(  
@CUSTOMER_ID  int,  
@POLICY_ID         int,   
@POLICY_VERSION_ID   smallint,  
@LOCATION_ID      smallint  
)  
AS  
BEGIN  
  
SELECT  REMARKS  FROM  POL_UMBRELLA_REAL_ESTATE_LOCATION   
WHERE CUSTOMER_ID      =  @CUSTOMER_ID AND  
      POLICY_ID           =  @POLICY_ID  AND  
      POLICY_VERSION_ID   =  @POLICY_VERSION_ID AND  
      LOCATION_ID      =  @LOCATION_ID  
  
END  



GO

