IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetAcordQuoteDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetAcordQuoteDetails]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                          
Proc Name       : dbo.Proc_GetAcordQuoteDetails                          
Created by      : nidhi                          
Date                : 27th dec '06                          
Purpose          :                          
Revison History :                          
Used In  : Wolverine                          
                          
          
------------------------------------------------------------                          
Date     Review By          Comments                          
------   ------------       -------------------------*/      
                
CREATE PROC dbo.Proc_GetAcordQuoteDetails      
(                          
	@AGENCY_ID     int                           
)                          
AS        
BEGIN                      
	SELECT 
		ACORD_QUOTE_NUMBER,
		INSURANCE_SVC_RQ,
		AGENCY_ID,
		ACORD_XML,
		QQ_XML,
		CREATED_DATETIME
	FROM  ACORD_QUOTE_DETAILS
	--WHERE AGENCY_ID=@AGENCY_ID
 order by CREATED_DATETIME desc

END    







GO

