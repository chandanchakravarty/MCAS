IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetXMLEndorsementAttachment]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetXMLEndorsementAttachment]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------    
Proc Name       : dbo.Proc_GetXMLEndorsementAttachment    
Created by      : Gaurav    
Date            : 10/20/2005    
Purpose       :Evaluation    
Revison History :    
Used In        : Wolverine    
------------------------------------------------------------    
Date     Review By          Comments    
------   ------------       -------------------------*/    
CREATE PROC dbo.Proc_GetXMLEndorsementAttachment    
(    
@ENDORSEMENT_ATTACH_ID     int    
)    
AS    
BEGIN    
select ENDORSEMENT_ATTACH_ID,    
ENDORSEMENT_ID,    
ATTACH_FILE,    
CONVERT(varchar(10),VALID_DATE,101) as VALID_DATE ,
CONVERT(VARCHAR,EFFECTIVE_TO_DATE ,101) AS EFFECTIVE_TO_DATE,       
 CONVERT(VARCHAR,DISABLED_DATE    ,101) AS DISABLED_DATE,    
 FORM_NUMBER, CONVERT(VARCHAR,EDITION_DATE,101) AS EDITION_DATE 
  
from  MNT_ENDORSEMENT_ATTACHMENT    

    
    
    
where  ENDORSEMENT_ATTACH_ID = @ENDORSEMENT_ATTACH_ID    
    
END    
    
  
  
  



GO

