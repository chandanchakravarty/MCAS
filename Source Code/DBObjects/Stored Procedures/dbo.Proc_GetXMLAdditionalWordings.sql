IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetXMLAdditionalWordings]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetXMLAdditionalWordings]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------          
Proc Name       : dbo.Proc_GetXMLAdditionalWordings          
Created by      : Swarup         
Date            : 23-Feb-2007         
Purpose       :To get old values           
Revison History :          
Used In        : Wolverine          
------------------------------------------------------------          
Date     Review By          Comments          
------   ------------       -------------------------*/      
--Drop proc dbo.Proc_GetXMLAdditionalWordings        
CREATE PROC dbo.Proc_GetXMLAdditionalWordings          
(          
@WORDINGS_ID     int          
)          
AS          
BEGIN          
select WORDINGS_ID,    
 STATE_ID,    
 PROCESS_ID,    
 LOB_ID,    
 ISNULL(PDF_WORDINGS,'') AS PDF_WORDINGS  
from  MNT_PROCESS_WORDINGS          
         
where  WORDINGS_ID = @WORDINGS_ID        
          
END          
          
    
       
        
        
      
    
  


GO

