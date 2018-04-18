IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_SaveReinsurer]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_SaveReinsurer]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--Proc Name       : dbo.Proc_SaveReinsurer           
--Created by      : Chetna Agarwal  
--Date            : 03/05/2010            
--Purpose			:To Save Reinsurer in POL_REINSURANCE_INFO
--Revison History :            
--Used In			: Ebix Advantage            
--------------------------------------------------------------            
--Date     Review By          Comments            
--------   ------------       -------------------------*/            
--DROP PROC dbo.Proc_SaveReinsurer
CREATE PROC [dbo].[Proc_SaveReinsurer]
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
@CREATED_BY INT=NULL,
@CREATED_DATETIME DATETIME=NULL,
@MODIFIED_BY INT=NULL,
@LAST_UPDATED_DATETIME DATETIME=NULL,
@REINSURER_NUMBER NVARCHAR(100) = NULL,
@MAX_NO_INSTALLMENT INT = NULL , --Added by Aditya for TFS BUG # 2514
@RISK_ID INT = NULL,  --Added by Aditya for TFS BUG # 2514
@COMM_AMOUNT decimal(18,2) =null,  --Added by Aditya for tfs bug # 177--
@LAYER_AMOUNT decimal(18,2) =null, --Added by Aditya for tfs bug # 177--
@REIN_PREMIUM decimal(18,2) = null  --Added by Aditya for tfs bug # 177--
)
AS
BEGIN
SELECT  @REINSURANCE_ID=isnull(Max(REINSURANCE_ID),0)+1 from POL_REINSURANCE_INFO where POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID and CUSTOMER_ID=@CUSTOMER_ID 
INSERT INTO POL_REINSURANCE_INFO
(REINSURANCE_ID
,COMPANY_ID
,CUSTOMER_ID
,POLICY_ID
,POLICY_VERSION_ID
,CONTRACT_FACULTATIVE
,CONTRACT
,REINSURANCE_CEDED
,REINSURANCE_COMMISSION
,IS_ACTIVE
,CREATED_BY
,CREATED_DATETIME
,MODIFIED_BY
,LAST_UPDATED_DATETIME 
 ,REINSURER_NUMBER
 ,MAX_NO_INSTALLMENT,   --Added by Aditya for TFS BUG # 2514
 RISK_ID,  
 COMM_AMOUNT, --Added by Aditya for tfs bug # 177--
 LAYER_AMOUNT,  --Added by Aditya for tfs bug # 177--
 REIN_PREMIUM  --Added by Aditya for tfs bug # 177--
)
 VALUES
(
@REINSURANCE_ID,
@COMPANY_ID,
@CUSTOMER_ID,
@POLICY_ID,
@POLICY_VERSION_ID,
@CONTRACT_FACULTATIVE,
@CONTRACT ,
@REINSURANCE_CEDED,
@REINSURANCE_COMMISSION,
@IS_ACTIVE,
@CREATED_BY,
@CREATED_DATETIME,
@MODIFIED_BY ,
@LAST_UPDATED_DATETIME,
@REINSURER_NUMBER,
@MAX_NO_INSTALLMENT,   --Added by Aditya for TFS BUG # 2514
@RISK_ID,   --Added by Aditya for TFS BUG # 2514
@COMM_AMOUNT,  --Added by Aditya for tfs bug # 177--
@LAYER_AMOUNT,  --Added by Aditya for tfs bug # 177--
@REIN_PREMIUM  --Added by Aditya for tfs bug # 177--
)
END
GO

