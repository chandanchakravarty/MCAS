IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_SetPolicyIS_ACTIVE]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_SetPolicyIS_ACTIVE]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*--------------------------------------------------------------------    
CREATED BY   : Vijay Arora    
CREATED DATE TIME : 04-01-2006    
PURPOSE    :  Set the IS_Active of the Policy.
REVIEW HISTORY    
REVIEW BY  DATE  PURPOSE    
    
---------------------------------------------------------------------*/    
CREATE PROCEDURE dbo.Proc_SetPolicyIS_ACTIVE    
(    
 @CUSTOMER_ID  INT,      
 @POLICY_ID    INT,       
 @POLICY_VERSION_ID  INT,       
 @IS_ACTIVE   NVARCHAR(5)    
)    
AS    
BEGIN    
 UPDATE POL_CUSTOMER_POLICY_LIST SET IS_ACTIVE = @IS_ACTIVE    
   WHERE CUSTOMER_ID = @CUSTOMER_ID AND     
      POLICY_ID = @POLICY_ID AND    
      POLICY_VERSION_ID = @POLICY_VERSION_ID    
END    
    
  



GO

