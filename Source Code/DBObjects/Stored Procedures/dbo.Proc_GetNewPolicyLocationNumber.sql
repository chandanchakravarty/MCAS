IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetNewPolicyLocationNumber]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetNewPolicyLocationNumber]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------            
Proc Name       : dbo.Proc_GetNewPolicyLocationNumber            
Created by      : Ravindra
Date            : 03-21-2006
Purpose         : To Get the new location number for policy           
Revison History :       
Used In         :   Wolverine            
------------------------------------------------------------            
Date     Review By          Comments            
------   ------------       -------------------------*/            
--drop proc  dbo.Proc_GetNewPolicyLocationNumber            
CREATE PROC dbo.Proc_GetNewPolicyLocationNumber            
(            
 @CUSTOMER_ID  INT,  
 @POLICY_ID   INT,  
 @POLICY_VERSION_ID INT 
)            
AS            
            
           
 DECLARE @NEW_LOCATION_NO BigInt
    
 SELECT  @NEW_LOCATION_NO =ISNULL(MAX(ISNULL(LOCATION_NUMBER,0)),0)
	 FROM POL_UMBRELLA_REAL_ESTATE_LOCATION  
	 WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID     


	IF @NEW_LOCATION_NO = 2147483647
	BEGIN
		SELECT -1
		RETURN		
	END
	SELECT @NEW_LOCATION_NO + 1
      
    



GO

