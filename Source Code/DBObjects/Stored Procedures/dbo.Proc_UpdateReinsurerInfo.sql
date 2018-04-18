IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdateReinsurerInfo]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdateReinsurerInfo]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--Proc Name       : dbo.Proc_UpdateReinsurerInfo           
--Created by      : Chetna Agarwal  
--Date            : 04/05/2010            
--Purpose			:To Update Reinsurer in POL_REINSURANCE_INFO
--Revison History :            
--Used In			: Ebix Advantage            
--------------------------------------------------------------            
--Date     Review By          Comments            
--------   ------------       -------------------------*/            
--DROP PROC dbo.Proc_UpdateReinsurerInfo
CREATE PROC [dbo].[Proc_UpdateReinsurerInfo]
( 
@REINSURANCE_ID INT OUT,
@POLICY_ID INT, 
@POLICY_VERSION_ID INT, 
@CUSTOMER_ID INT,
@COMPANY_ID INT,
@CONTRACT_FACULTATIVE INT=NULL,
@CONTRACT INT=NULL,
@REINSURANCE_CEDED DECIMAL(18,2)=NULL,
@REINSURANCE_COMMISSION DECIMAL(18,2)=NULL,
@IS_ACTIVE NVARCHAR(1)=NULL,
@MODIFIED_BY INT=NULL,
@LAST_UPDATED_DATETIME DATETIME=NULL,
@REINSURER_NUMBER NVARCHAR(100),
@MAX_NO_INSTALLMENT INT = NULL ,   --Added by Aditya for TFS BUG # 2514
@RISK_ID INT = NULL,    --Added by Aditya for TFS BUG # 2514
@COMM_AMOUNT decimal(18,2) =null,  --Added by Aditya for tfs bug # 177--
@LAYER_AMOUNT decimal(18,2) =null,  --Added by Aditya for tfs bug # 177--
@REIN_PREMIUM decimal(18,2) = null  --Added by Aditya for tfs bug # 177--
)
AS
BEGIN
UPDATE POL_REINSURANCE_INFO
SET 
COMPANY_ID = @COMPANY_ID
,CUSTOMER_ID = @CUSTOMER_ID
,POLICY_ID = @POLICY_ID
,POLICY_VERSION_ID = @POLICY_VERSION_ID
,CONTRACT_FACULTATIVE = @CONTRACT_FACULTATIVE
,CONTRACT = @CONTRACT
,REINSURANCE_CEDED = @REINSURANCE_CEDED
,REINSURANCE_COMMISSION = @REINSURANCE_COMMISSION
,IS_ACTIVE = @IS_ACTIVE
,MODIFIED_BY = @MODIFIED_BY
,LAST_UPDATED_DATETIME= @LAST_UPDATED_DATETIME
,REINSURER_NUMBER =@REINSURER_NUMBER,
MAX_NO_INSTALLMENT= @MAX_NO_INSTALLMENT,  --Added by Aditya for TFS BUG # 2514
RISK_ID = @RISK_ID, --Added by Aditya for TFS BUG # 2514
COMM_AMOUNT = @COMM_AMOUNT,  --Added by Aditya for tfs bug # 177--
LAYER_AMOUNT = @LAYER_AMOUNT,  --Added by Aditya for tfs bug # 177--
REIN_PREMIUM = @REIN_PREMIUM  --Added by Aditya for tfs bug # 177--
WHERE
REINSURANCE_ID=@REINSURANCE_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID AND CUSTOMER_ID=@CUSTOMER_ID

END
GO

