/****** Object:  StoredProcedure [dbo].[Proc_GetPremiumNoticeDetails]    Script Date: 09/16/2011 15:53:47 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetPremiumNoticeDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetPremiumNoticeDetails]
GO


/****** Object:  StoredProcedure [dbo].[Proc_GetPremiumNoticeDetails]    Script Date: 09/16/2011 15:53:47 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


---------------------------------------------------------------
--Proc Name          : dbo.Proc_GetPremiumNoticeDetails
--Created by         :          
--Date               :  5 August 2011       
--------------------------------------------------------
--Date     Review By          Comments        
------   ------------       -------------------------*/       
-- drop proc dbo.[Proc_GetPremiumNoticeDetails]      
create  PROCEDURE [dbo].[Proc_GetPremiumNoticeDetails]      
(        
    @CUSTOMER_ID INT, 
	@POLICY_ID INT,
	@POLICY_VERSION_ID INT,
	@NOTICE_DUE_DATE DATE,
	@IS_EOD SMALLINT,
	@CARRIER_CODE NVARCHAR(20),
	@LANG_ID INT 

)        
AS       
BEGIN      
	Exec Proc_GetBase_Header_AI_PN @CUSTOMER_ID,@POLICY_ID,@NOTICE_DUE_DATE,@CARRIER_CODE,@LANG_ID
	
	Exec Proc_GetBase_TransactionHistory_AI_PN @CUSTOMER_ID,@POLICY_ID, @POLICY_VERSION_ID,'PN',null,@LANG_ID,@CARRIER_CODE
	
	Exec Proc_GetBase_InstallmentDetails_PN @CUSTOMER_ID,@POLICY_ID,@NOTICE_DUE_DATE,@LANG_ID,@CARRIER_CODE
	
	
End


GO

