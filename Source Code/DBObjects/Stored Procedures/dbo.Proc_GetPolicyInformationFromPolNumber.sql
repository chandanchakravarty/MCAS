IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetPolicyInformationFromPolNumber]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetPolicyInformationFromPolNumber]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                      
Proc Name       : dbo.Proc_GetPolicyInformationFromPolNumber                  
Created by      : Swarup                 
Date            : 13-06-2007  
Purpose         : To get Policy Information from application information                  
Revison History :                      
Used In         : Wolverine                      
              
Reviewed By	:	Anurag verma
Reviewed On	:	25-06-2007
------------------------------------------------------------                      
Date     Review By          Comments                      
------   ------------       -------------------------*/                      
  
--drop PROCEDURE dbo.Proc_GetPolicyInformationFromPolNumber 'A1004144'  
CREATE PROCEDURE dbo.Proc_GetPolicyInformationFromPolNumber                  
(                      
 @POLICY_NUMBER varchar(100)  
)                          
AS                    
BEGIN   
--DECLARE @POLICY_INFO VARCHAR (100)  
  
SELECT  (POLICY_NUMBER + '  - ' + CONVERT(VARCHAR,POLICY_ID) + ' - ' + CONVERT(VARCHAR,POLICY_VERSION_ID)) AS POLICY_INFO,  
CUSTOMER_ID,  
POLICY_ID,  
POLICY_VERSION_ID,BILL_TYPE  
FROM POL_CUSTOMER_POLICY_LIST WHERE  
POLICY_NUMBER=@POLICY_NUMBER 
ORDER BY POLICY_VERSION_ID DESC 
--SET @POLICY_INFO = POLICY_INFO  
--RETURN @POLICY_INFO  
END  
  
  
  





GO

