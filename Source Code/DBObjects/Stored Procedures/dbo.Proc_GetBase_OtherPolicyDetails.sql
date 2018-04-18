
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetBase_OtherPolicyDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetBase_OtherPolicyDetails]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



---------------------------------------------------------------
--Proc Name          : dbo.Proc_GetBase_OtherPolicyDetails  
--Created by         :          
--Date               :  5 August 2011       
--------------------------------------------------------
--Date     Review By          Comments        
------   ------------       -------------------------*/       
-- drop proc dbo.[Proc_GetBase_OtherPolicyDetails]      
CREATE  PROCEDURE [dbo].[Proc_GetBase_OtherPolicyDetails]      
(        
 	@CUSTOMER_ID INT,
	@POLICY_ID INT, 
	@AGENCY_CODE NVARCHAR(20),
	@LANG_ID INT, 
	@CARRIER_CODE NVARCHAR(20)

)        
AS       
--BEGIN      

--End