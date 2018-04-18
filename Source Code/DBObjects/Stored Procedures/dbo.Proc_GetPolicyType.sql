IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetPolicyType]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetPolicyType]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------    
Proc Name       : Dbo.Proc_GetPolicyType      
Created by      : shafi    
Date            : 1/12/2005    
Purpose     : Get the Plicy Type Of Application
Revison History :    
Used In  : Wolverine     
    
------------------------------------------------------------    
Date     Review By          Comments    
------   ------------       -------------------------*/    
CREATE        PROC Dbo.Proc_GetPolicyType    
(    
@CUSTOMER_ID int,    
@APP_ID  int,    
@APP_VERSION_ID smallint,
@POLICYID     int output     
   
)    
AS    
BEGIN    
    

Set @POLICYID= (SELECT POLICY_TYPE FROM APP_LIST WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID and APP_VERSION_ID=@APP_VERSION_ID and APP_LOB=6)    

END   


GO

