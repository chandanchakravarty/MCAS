IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_DeleteRemuneration]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_DeleteRemuneration]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


--Modified By Lalit Chauhan
--Modify date : Feb 03 ,2011
--DROP PROC Proc_DeleteRemuneration  
  
Create PROC [dbo].[Proc_DeleteRemuneration]
(    
  @CUSTOMER_ID INT,
  @POLICY_ID INT,
  @POLICY_VERSION_ID INT,
  @REMUNERATION_ID INT
)     
 AS  
 BEGIN  
	 DELETE FROM POL_REMUNERATION
	 WHERE REMUNERATION_ID=@REMUNERATION_ID AND 
	 CUSTOMER_ID = @CUSTOMER_ID AND 
	 POLICY_ID = @POLICY_ID AND 
	 POLICY_VERSION_ID = @POLICY_VERSION_ID
 END  

GO

