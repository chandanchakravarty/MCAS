IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_CheckPolicyIsActive]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_CheckPolicyIsActive]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

 /*----------------------------------------------------------      
Proc Name          : Dbo.Proc_CheckPolicyIsActive      
Created by         : Charles Fomes     
Date               : 17/Mar/2010      
Purpose            : Get the Status of an Policy (IS_ACTIVE)          

declare @out char
exec Proc_CheckPolicyIsActive 2043,141,1,@out out
select @out

drop proc Proc_CheckPolicyIsActive
------   ------------       -------------------------*/      
CREATE   PROCEDURE dbo.Proc_CheckPolicyIsActive      
(      
 @CUSTOMER_ID INT,                                                               
 @POLICY_ID INT,                                           
 @POLICY_VERSION_ID SMALLINT,        
 @IS_ACTIVE VARCHAR(2) OUTPUT      
)      
AS      
BEGIN      
 SELECT @IS_ACTIVE = ISNULL(IS_ACTIVE,'N') FROM POL_CUSTOMER_POLICY_LIST WITH(NOLOCK)    
 WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID = @POLICY_ID AND POLICY_VERSION_ID = @POLICY_VERSION_ID    
END    
GO

