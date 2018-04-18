IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertReinsuranceContract]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertReinsuranceContract]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------  
Proc Name       : dbo.Proc_InsertReinsuranceContract  
Created by      : nidhi  
Date            : 1/4/2006  
Purpose       :  
Revison History :  
Used In        : Wolverine  
------------------------------------------------------------  
Date     Review By          Comments  
------   ------------       -------------------------*/  
CREATE PROC Dbo.Proc_InsertReinsuranceContract  
(  
@CONTRACT_ID     int output,  
@CONTRACT_NUMBER     nvarchar(50),  
@CONTRACT_NAME_ID     int,  
@CONTRACT_TYPE     int,  
@CONTRACT_LOB     smallint,  
@CONTRACT_DESC     nvarchar(500),  
@BROKERID     int,  
@STATE_ID     int,  
@REINSURER_REFERENCE_NUM     nvarchar(50),  
@UW_YEAR     smallint,  
@ASLOB     numeric(9),  
@SUBLINE_CODE     nvarchar(20),  
@COVERAGE_CODE     nvarchar(20),  
@CESSION     decimal(5),  
@CALCULATION_BASE   int,  
@EFFECTIVE_DATE     datetime,  
@EXPIRATION_DATE     datetime,  
@CREATED_BY     smallint,  
@CREATED_DATETIME     datetime =null  
  
)  
AS  
BEGIN  
  
select @CONTRACT_ID=isnull(Max(CONTRACT_ID),0)+1 from MNT_REINSURANCE_CONTRACT  
INSERT INTO MNT_REINSURANCE_CONTRACT  
(  
CONTRACT_ID,  
CONTRACT_NUMBER,  
CONTRACT_NAME_ID,  
CONTRACT_TYPE,  
CONTRACT_LOB,  
CONTRACT_DESC,  
BROKERID,  
STATE_ID,  
REINSURER_REFERENCE_NUM,  
UW_YEAR,  
ASLOB,  
SUBLINE_CODE,  
COVERAGE_CODE,  
CESSION,  
CALCULATION_BASE,  
EFFECTIVE_DATE,  
EXPIRATION_DATE,  
CREATED_BY,  
CREATED_DATETIME,  
MODIFIED_BY,  
LAST_UPDATED_DATETIME  
)  
VALUES  
(  
@CONTRACT_ID,  
@CONTRACT_NUMBER,  
@CONTRACT_NAME_ID,  
@CONTRACT_TYPE,  
@CONTRACT_LOB,  
@CONTRACT_DESC,  
@BROKERID,  
@STATE_ID,  
@REINSURER_REFERENCE_NUM,  
@UW_YEAR,  
@ASLOB,  
@SUBLINE_CODE,  
@COVERAGE_CODE,  
@CESSION,  
@CALCULATION_BASE,  
@EFFECTIVE_DATE,  
@EXPIRATION_DATE,  
@CREATED_BY,  
@CREATED_DATETIME,  
null,  
null  
)  
END



GO

