IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetPolicyNewBoatNo]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetPolicyNewBoatNo]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------      
Proc Name          	: Dbo.Proc_GetPolicyNewBoatNo      
Created by           	: Vijay Arora
Date                    : 21-11-2005
Purpose               	: To get the new insured boat number      
Revison History 	:      
Used In                	:   Wolverine      
------------------------------------------------------------      
Date     Review By          Comments      
------   ------------       -------------------------*/      
CREATE PROC Dbo.Proc_GetPolicyNewBoatNo      
@CUSTOMER_ID INT,      
@POLICY_ID INT,      
@POLICY_VERSION_ID INT,      
@CALLEDFROM varchar(4)       
as      
BEGIN      
      
 if (@CALLEDFROM ='WAT' or @CALLEDFROM='RENT' or @CALLEDFROM='HOME')      
 begin      
  SELECT    (isnull(MAX(BOAT_NO),0)) +1 as BOAT_NO      
  FROM         POL_WATERCRAFT_INFO WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID      
 end      
 else if (@CALLEDFROM ='UMB')      
 begin      
  SELECT    (isnull(MAX(BOAT_NO),0)) +1 as BOAT_NO      
  FROM         POL_UMBRELLA_WATERCRAFT_INFO WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID      
 end      
END      
    
  



GO

