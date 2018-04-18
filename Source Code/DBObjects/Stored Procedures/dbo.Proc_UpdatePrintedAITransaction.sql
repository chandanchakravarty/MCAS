
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdatePrintedAITransaction]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdatePrintedAITransaction]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

---------------------------------------------------------------
--Proc Name          : dbo.Proc_UpdatePrintedAITransaction
--Created by         :          
--Date               :  5 August 2011       
--------------------------------------------------------
--Date     Review By          Comments        
------   ------------       -------------------------*/       
-- drop proc dbo.[Proc_UpdatePrintedAITransaction]      
CREATE  PROCEDURE [dbo].[Proc_UpdatePrintedAITransaction]      
(        
    @CUSTOMER_ID INT, 
    @POLICY_ID INT,
	@NOTICE_DUE_DATE DATE,
	@LANG_ID INT,
	@CARRIER_CODE NVARCHAR(20)


)        
AS       
--BEGIN      

--End