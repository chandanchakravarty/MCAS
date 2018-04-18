IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_DeleteReinsurance]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_DeleteReinsurance]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--Drop proc Proc_DeleteReinsurance  
Create PROC [dbo].[Proc_DeleteReinsurance]  
(      
  @REINSURANCE_ID int,  
  @POLICY_ID int,  
  @POLICY_VERSION_ID int,  
  @CUSTOMER_ID int  
)       
 AS    
 BEGIN    
 Delete from POL_REINSURANCE_INFO where REINSURANCE_ID=@REINSURANCE_ID and POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID and CUSTOMER_ID=@CUSTOMER_ID   
 END    
   

GO

