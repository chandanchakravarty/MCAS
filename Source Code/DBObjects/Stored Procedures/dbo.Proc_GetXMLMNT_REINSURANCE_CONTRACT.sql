IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetXMLMNT_REINSURANCE_CONTRACT]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetXMLMNT_REINSURANCE_CONTRACT]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

 /*----------------------------------------------------------    
Proc Name       : dbo.ReinsuranceInformation    
Created by      : nidhi    
Date            : 1/4/2006    
Purpose       :    
Revison History :    
Used In        : Wolverine    
------------------------------------------------------------    
Date     Review By          Comments    
------   ------------       -------------------------*/    
--drop PROC Dbo.Proc_GetXMLMNT_REINSURANCE_CONTRACT 63    
CREATE PROC [dbo].[Proc_GetXMLMNT_REINSURANCE_CONTRACT]    
(    
@CONTRACT_ID     int    
)    
AS    
BEGIN    
select CONTRACT_ID,    
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
CASH_CALL_LIMIT,    
convert(varchar,EFFECTIVE_DATE,101) as EFFECTIVE_DATE ,    
convert(varchar,EXPIRATION_DATE,101) as EXPIRATION_DATE,     
MODIFIED_BY,    
LAST_UPDATED_DATETIME    
from  MNT_REINSURANCE_CONTRACT    
    
    
    
where  CONTRACT_ID = @CONTRACT_ID    
    
END    
  
  
GO

