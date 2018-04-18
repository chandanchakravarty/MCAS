IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetAppOldInputXML]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetAppOldInputXML]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------    
Proc Name      : dbo.Proc_GetAppOldInputXML  
Created by     : ASFA   PRAVEEN
Date           : 22-June-2006 7
Purpose        : Get the Old InputXML for the Application Which Status is Complete(Policy has been Created)
Revison History :    
Modified by   :     
Description  :     
Used In        : Wolverine    
------------------------------------------------------------    
Date     Review By          Comments    
------   ------------       -------------------------*/    
-- DROP PROC Dbo.Proc_GetAppOldInputXML    
CREATE  PROC Dbo.Proc_GetAppOldInputXML  
@CUSTOMER_ID INT,  
@app_ID INT,    
@app_VERSION_ID SMALLINT  
AS    
BEGIN    
SELECT QCQ.QUOTE_INPUT_XML , AL.APP_STATUS, QCQ.QUOTE_ID
FROM APP_LIST AL WITH (NOLOCK) INNER JOIN QOT_CUSTOMER_QUOTE_LIST QCQ WITH (NOLOCK)
ON QCQ.CUSTOMER_ID = AL.CUSTOMER_ID
AND QCQ.APP_ID=AL.APP_ID AND QCQ.APP_VERSION_ID=AL.APP_VERSION_ID
WHERE QCQ.CUSTOMER_ID=@CUSTOMER_ID  AND QCQ.APP_ID=@APP_ID  AND QCQ.APP_VERSION_ID=@APP_VERSION_ID
END    



GO

