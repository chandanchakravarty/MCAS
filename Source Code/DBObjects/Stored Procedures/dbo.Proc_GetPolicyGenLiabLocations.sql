IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetPolicyGenLiabLocations]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetPolicyGenLiabLocations]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------      
Proc Name       : dbo.Proc_GetPolicyGenLiabLocations      
Created by      : Ravindra  
Date            : 03/29/2006      
Purpose         : To get locations for General Liability  Policy Level  
Revison History :      
Used In         : Wolverine      
------------------------------------------------------------      
Date     Review By          Comments      
------   ------------       -------------------------*/      
CREATE PROC Dbo.Proc_GetPolicyGenLiabLocations    
(    
 @CUSTOMER_ID int,    
 @POLICY_ID int,    
 @POLICY_VERSION_ID smallint    
-- @User_Flag char(5)    
)AS      
BEGIN      

 SELECT  (CAST(ISNULL(LOC_NUM,0) AS VARCHAR(5)) + ' ' + ISNULL(LOC_ADD1,'') + ' ' + ISNULL(LOC_CITY,'')     
      + ' ' + ISNULL(LOC_STATE,'')) AS LOCATIONS
      FROM POL_LOCATIONS WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND @POLICY_VERSION_ID=@POLICY_VERSION_ID    
END      
      
      
    
  



GO

