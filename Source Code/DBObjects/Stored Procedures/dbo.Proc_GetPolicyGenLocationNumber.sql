IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetPolicyGenLocationNumber]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetPolicyGenLocationNumber]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------            
Proc Name       : dbo.Proc_GetPolicyGenLocationNumber            
Created by      : Sumit Chhabra            
Date            : 10/10/2005            
Purpose         : To Get the new location number            
Revison History :       
------------------------------------------------------------            
Date     Review By          Comments            
------   ------------       -------------------------*/            
CREATE PROC dbo.Proc_GetPolicyGenLocationNumber            
(            
 @CUSTOMER_ID varchar(5),    
 @POLICY_ID varchar(5),    
 @POLICY_VERSION_ID varchar(5),     
 @CODE INT OUTPUT            
)            
AS            
            
BEGIN            
      

 SELECT  @CODE =ISNULL(MAX(ISNULL(LOC_NUM,0)),0) +1  FROM POL_LOCATIONS WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID     

            
END            
    



GO

