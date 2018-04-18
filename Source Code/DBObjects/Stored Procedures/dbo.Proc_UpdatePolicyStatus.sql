IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdatePolicyStatus]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdatePolicyStatus]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

 /*----------------------------------------------------------        
Proc Name       : dbo.Proc_UpdatePolicyStatus    
Created by      : Vijay Arora    
Date            : 27/10/2005        
Purpose        : Update the Policy Status.    
Revison History :        
Used In         : Wolverine          
------------------------------------------------------------        
Date     Review By          Comments  
DROP PROC Proc_UpdatePolicyStatus      
------   ------------       -------------------------*/        
CREATE PROC [dbo].[Proc_UpdatePolicyStatus]    
@CUSTOMER_ID INT,    
@POLICY_ID INT,    
@POLICY_VERSION_ID SMALLINT,    
@RESULT INT OUTPUT    ,
@POLICY_STATUS NVARCHAR(50) = NULL
AS    
BEGIN    

IF(@POLICY_STATUS IS NULL)
	SELECT @POLICY_STATUS = 'Normal'


UPDATE POL_CUSTOMER_POLICY_LIST     
SET    
 POLICY_STATUS = @POLICY_STATUS --'Normal'    Modified By lalit Jan 18,2011 
WHERE     
CUSTOMER_ID = @CUSTOMER_ID    
AND POLICY_ID = @POLICY_ID    
AND POLICY_VERSION_ID = @POLICY_VERSION_ID    
    
SET @RESULT = 1    
RETURN @RESULT    
END    
  
  
GO

