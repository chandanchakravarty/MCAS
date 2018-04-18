 IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetCustomerPolicyDetail]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetCustomerPolicyDetail]
GO

SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER OFF
GO


 
 /*----------------------------------------------------------    
Proc Name       : [Proc_GetCustomerPolicyDetail]    
Created by      : Agniswar Das    
Date            : 7/17/2011    
Purpose			: Demo    
Revison History :    
Used In        : Singapore    
------------------------------------------------------------    
Date     Review By          Comments    
------   ------------       -------------------------*/   

                                                    
CREATE  proc Dbo.Proc_GetCustomerPolicyDetail            
(                                                    
 @CUSTOMER_ID    int,                                                    
 @APP_ID    int,                                                    
 @APP_VERSION_ID   int     
           
)                                                    
as                                     
  
SELECT     *  
FROM        POL_CUSTOMER_POLICY_LIST WITH(NOLOCK)  
where ( CUSTOMER_ID=@CUSTOMER_ID and APP_ID=@APP_ID and APP_VERSION_ID=@APP_VERSION_ID )  
  