IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_CheckPolicyCreatedMode]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_CheckPolicyCreatedMode]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

    
/*               
----------------------------------------------------------                                    
Proc Name       : dbo.Proc_CheckPolicyCreatedMode                          
Created by      : SANTOSH KUMAR GAUTAM        
Date            : 22 JUNE 2011                                 
Purpose         : 
Revison History :                                    
Used In        : Ebix Advantage                                    
------------------------------------------------------------                                    
Date     Review By          Comments                                    
------   ------------       -------------------------                    
drop proc Proc_CheckPolicyCreatedMode   */

CREATE PROC [dbo].[Proc_CheckPolicyCreatedMode]    
(    
@CUSTOMER_ID INT,    
@POLICY_ID INT,    
@POLICY_VERSION_ID SMALLINT    
)    
AS    
 BEGIN   
 
  -- ADDED BY SANTOSH KUMAR GAUTAM ON 22 JUNE 2011 
  -- DISABLE GENERATE BUTTON IF POLICY IF CREATED BY ACCCEPTED COINSURANCE LOAD
  
  SELECT (CASE WHEN FROM_AS400='A' THEN 'Y' ELSE 'N' END) AS IS_ACCEPTED_COI_POLICY
  FROM   POL_CUSTOMER_POLICY_LIST
  WHERE  CUSTOMER_ID	   = @CUSTOMER_ID AND
         POLICY_ID		   = @POLICY_ID   AND
         POLICY_VERSION_ID = @POLICY_VERSION_ID
         
END
GO

