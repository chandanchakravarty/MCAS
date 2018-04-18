IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetXMLEndorsmentDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetXMLEndorsmentDetails]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------        
Proc Name       : dbo.Proc_GetXMLEndorsmentDetails        
Created by      : Gaurav        
Date            : 8/26/2005        
Purpose       :Evaluation        
Revison History :        
Used In        : Wolverine        
------------------------------------------------------------        
Date     Review By          Comments        
------   ------------       -------------------------*/        
--drop proc Proc_GetXMLEndorsmentDetails        
CREATE PROC dbo.Proc_GetXMLEndorsmentDetails        
(        
@ENDORSMENT_ID     int        
)        
AS        
BEGIN        
select ENDORSMENT_ID,        
STATE_ID,        
LOB_ID,        
PURPOSE,        
TYPE,        
DESCRIPTION,        
TEXT,      
ENDORSEMENT_CODE,        
ENDORS_ASSOC_COVERAGE,        
SELECT_COVERAGE,        
ENDORS_PRINT,        
ENDORS_ATTACHMENT,        
IS_ACTIVE,        
CREATED_BY,        
CREATED_DATETIME,        
MODIFIED_BY,        
LAST_UPDATED_DATETIME  ,      
CONVERT(VARCHAR,ISNULL(EFFECTIVE_FROM_DATE,'1950-01-01'),101) AS EFFECTIVE_FROM_DATE,      
 CONVERT(VARCHAR,EFFECTIVE_TO_DATE ,101) AS EFFECTIVE_TO_DATE,       
 CONVERT(VARCHAR,DISABLED_DATE    ,101) AS DISABLED_DATE,    
FORM_NUMBER, CONVERT(VARCHAR,EDITION_DATE,101) AS EDITION_DATE,  
INCREASED_LIMIT,
PRINT_ORDER      
from  MNT_ENDORSMENT_DETAILS        
        
where  ENDORSMENT_ID = @ENDORSMENT_ID        
        
END        
      
      
      
      
      
    
  



GO

