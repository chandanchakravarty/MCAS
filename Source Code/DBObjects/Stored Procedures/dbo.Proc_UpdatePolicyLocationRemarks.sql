IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdatePolicyLocationRemarks]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdatePolicyLocationRemarks]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



/*----------------------------------------------------------    
Proc Name       : dbo.Proc_UpdatePolicyLocationRemarks
Created by      : Ravindra
Date            : 03-21-2006
Purpose         : To update the record in real estate table for policy   
Revison History :    
Used In         :   wolverine    
    
------------------------------------------------------------    
Date     Review By          Comments    
------   ------------       -------------------------*/    
CREATE  PROC Dbo.Proc_UpdatePolicyLocationRemarks
(    
@CUSTOMER_ID  int,    
@POLICY_ID        int,    
@POLICY_VERSION_ID   smallint,    
@LOCATION_ID      smallint,    
@REMARKS  nvarchar(100)  
)    
AS    
BEGIN    
    
 DECLARE @REMARKS_VALUE NVARCHAR(100)  
 DECLARE @RETURNVALUE INT  
   
 SELECT @REMARKS_VALUE=REMARKS FROM POL_UMBRELLA_REAL_ESTATE_LOCATION  
 WHERE  CUSTOMER_ID=@CUSTOMER_ID AND    POLICY_ID     =@POLICY_ID  AND    POLICY_VERSION_ID=@POLICY_VERSION_ID AND    
 LOCATION_ID    =@LOCATION_ID    
 IF(@REMARKS_VALUE IS NOT NULL)  
 BEGIN   
 SET @RETURNVALUE=-2  
 END  
 ELSE   
 BEGIN   
  SET @RETURNVALUE=1  
 END  
 ---  
 UPDATE POL_UMBRELLA_REAL_ESTATE_LOCATION    
 SET REMARKS =@REMARKS    
 WHERE  CUSTOMER_ID=@CUSTOMER_ID AND    POLICY_ID     =@POLICY_ID  AND    POLICY_VERSION_ID=@POLICY_VERSION_ID AND    
  LOCATION_ID    =@LOCATION_ID    
  
 RETURN @RETURNVALUE  
      
END    
    
  



GO

