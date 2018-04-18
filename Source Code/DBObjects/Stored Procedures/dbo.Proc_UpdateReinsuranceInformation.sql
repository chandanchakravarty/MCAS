IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdateReinsuranceInformation]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdateReinsuranceInformation]
GO

SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER OFF
GO




/*----------------------------------------------------------
Proc Name       : dbo.ReinsuranceInformation
Created by      : nidhi
Date            : 1/4/2006
Purpose    	  :
Revison History :
Used In 	      : Wolverine
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
CREATE PROC Dbo.Proc_UpdateReinsuranceInformation
(
@CONTRACT_ID     int,
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
@CALCULATION_BASE     int,
@EFFECTIVE_DATE     datetime,
@EXPIRATION_DATE     datetime,
@MODIFIED_BY     smallint,
@LAST_UPDATED_DATETIME     datetime
)
AS
BEGIN
Update  MNT_REINSURANCE_CONTRACT
set
CONTRACT_NUMBER  =  @CONTRACT_NUMBER,
CONTRACT_NAME_ID  =  @CONTRACT_NAME_ID,
CONTRACT_TYPE  =  @CONTRACT_TYPE,
CONTRACT_LOB  =  @CONTRACT_LOB,
CONTRACT_DESC  =  @CONTRACT_DESC,
BROKERID  =  @BROKERID,
STATE_ID  =  @STATE_ID,
REINSURER_REFERENCE_NUM  =  @REINSURER_REFERENCE_NUM,
UW_YEAR  =  @UW_YEAR,
ASLOB  =  @ASLOB,
SUBLINE_CODE  =  @SUBLINE_CODE,
COVERAGE_CODE  =  @COVERAGE_CODE,
CESSION  =  @CESSION,
CALCULATION_BASE  =  @CALCULATION_BASE,
EFFECTIVE_DATE  =  @EFFECTIVE_DATE,
EXPIRATION_DATE  =  @EXPIRATION_DATE,
MODIFIED_BY  =  @MODIFIED_BY,
LAST_UPDATED_DATETIME  =  @LAST_UPDATED_DATETIME

where 	CONTRACT_ID = @CONTRACT_ID

END



GO

