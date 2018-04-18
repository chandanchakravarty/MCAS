IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdatePolicyMVRFlag]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdatePolicyMVRFlag]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                                                        
Proc Name                : Dbo.Proc_UpdatePolicyMVRFlag
Created by               : Vijay Arora  
Date                     : 21-03-2006  
Purpose                  : Update the Policy MVR Windows Service Flag. 
Revison History          :                                                        
Used In                  : Wolverine                                                        
------------------------------------------------------------                                                        
Date     Review By          Comments                                                        
------   ------------       -------------------------*/                                                        
CREATE proc Dbo.Proc_UpdatePolicyMVRFlag                                                     
(                                                        
 @CUSTOMER_ID    INT,                                                        
 @POLICY_ID    INT,                                                        
 @POLICY_VERSION_ID   INT       
)                                                        
AS  
BEGIN  
  
	IF EXISTS (SELECT POLICY_ID FROM POL_CUSTOMER_POLICY_LIST WHERE CUSTOMER_ID = @CUSTOMER_ID
		   AND POLICY_ID = @POLICY_ID AND POLICY_VERSION_ID = @POLICY_VERSION_ID)

	BEGIN
		UPDATE POL_CUSTOMER_POLICY_LIST SET MVR_WIN_SERVICE = 'Y'
		WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID AND POLICY_VERSION_ID = @POLICY_VERSION_ID
	END
END                                         
      
    
    
    
  



GO

