IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_FetchCustomerId]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_FetchCustomerId]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

  --drop proc dbo.Proc_FetchCustomerId
CREATE PROCEDURE dbo.Proc_FetchCustomerId      
(      
	@POLICY_NUMBER	Varchar(50)
   )    
AS    
BEGIN 
  SELECT CUSTOMER_ID 
  FROM POL_CUSTOMER_POLICY_LIST   WITH(NOLOCK)    
  WHERE POLICY_NUMBER = @POLICY_NUMBER AND BILL_TYPE = 'DB'        
  GROUP BY IsNull(POLICY_ID,0), IsNull(POLICY_ACCOUNT_STATUS,0), CUSTOMER_ID   
END








    
GO

