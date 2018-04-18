IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetAppNumber]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetAppNumber]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

/*----------------------------------------------------------    
Proc Name	: Dbo.Proc_GetAppNumber    
Created by	: Asfa Praveen   
Date		: 19/Dec/2007
Purpose		: To get Application number based on App_id    
Revison History	:    
Used In		: Wolverine      
------------------------------------------------------------    
Date     Review By          Comments    
------   ------------       --------------------------------
Modify by	:
Date		:
Purpose		:
------------------------------------------------------------*/  

-- DROP PROC dbo.Proc_GetAppNumber         
CREATE PROC dbo.Proc_GetAppNumber          
(            
 @CUSTOMER_ID int,
 @CALLED_FOR VARCHAR(20),        
 @APP_ID INT = NULL,
 @QQ_ID INT = NULL
)            
AS            
BEGIN
  IF(@CALLED_FOR='CUSTOMER') --OR (@QQ_ID IS NOT NULL))
    BEGIN
      SELECT AL.APP_NUMBER,  CQL.QQ_NUMBER ,ISNULL(CUSTOMER_FIRST_NAME,'') + ' ' + ISNULL(CUSTOMER_LAST_NAME,'') AS CUSTOMER_NAME
	  FROM 
		CLT_QUICKQUOTE_LIST CQL 
		LEFT OUTER JOIN 
	      APP_LIST AL ON AL.CUSTOMER_ID=CQL.CUSTOMER_ID AND AL.APP_ID=CQL.APP_ID
		LEFT OUTER JOIN 
		CLT_CUSTOMER_LIST CLT ON CLT.CUSTOMER_ID = AL.CUSTOMER_ID
      WHERE CQL.CUSTOMER_ID=@CUSTOMER_ID AND CQL.QQ_ID=@QQ_ID
    END
  ELSE IF(@CALLED_FOR='APPLICATION')
    BEGIN
      SELECT AL.APP_NUMBER,  CQL.QQ_NUMBER ,ISNULL(CUSTOMER_FIRST_NAME,'') + ' ' + ISNULL(CUSTOMER_LAST_NAME,'') AS CUSTOMER_NAME
      FROM 
		APP_LIST AL 
		LEFT OUTER JOIN 
		CLT_QUICKQUOTE_LIST CQL ON AL.CUSTOMER_ID=CQL.CUSTOMER_ID AND AL.APP_ID=CQL.APP_ID
		LEFT OUTER JOIN 
		CLT_CUSTOMER_LIST CLT ON CLT.CUSTOMER_ID = AL.CUSTOMER_ID
      WHERE AL.CUSTOMER_ID=@CUSTOMER_ID AND AL.APP_ID=@APP_ID
    END
END      
      

    
  










GO

