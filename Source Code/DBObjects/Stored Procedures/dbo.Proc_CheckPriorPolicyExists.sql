IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_CheckPriorPolicyExists]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_CheckPriorPolicyExists]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------          
Proc Name       : dbo.Proc_CheckPriorPolicyExists          
Created by      : Sibin Thomas Philip      
Date            : 26 Nov 09         
Purpose         : To check whether Prior Policy already exists.          
Revison History :          
Used In        : Wolverine          
------------------------------------------------------------          
Date     Review By          Comments          
------   ------------       -------------------------*/          
-- DROP  PROC dbo.Proc_CheckPriorPolicyExists          
CREATE   PROC dbo.Proc_CheckPriorPolicyExists          
(          
  @CUSTOMER_ID int,        
  @OLD_POLICY_NUMBER nvarchar(20), 
  @LOB VARCHAR(5)
)

AS
BEGIN

 SELECT * FROM APP_PRIOR_CARRIER_INFO 
 WHERE CUSTOMER_ID = @CUSTOMER_ID AND OLD_POLICY_NUMBER = @OLD_POLICY_NUMBER AND LOB = @LOB 

END






GO

