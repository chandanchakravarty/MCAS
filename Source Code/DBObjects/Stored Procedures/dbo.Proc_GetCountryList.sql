IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetCountryList]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetCountryList]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------          
Proc Name       : dbo.Proc_GetCountryList          
CREATED BY		: Asfa Praveen
CREATED DATE	: Apr 22, 2008
Purpose         : Fetch Country List by Country_id     
Revison History :          
Used In         : Wolvorine          
------------------------------------------------------------          
Date     Review By          Comments          
------   ------------       -------------------------*/    
--DROP PROC dbo.Proc_GetCountryList '1, 2'
CREATE PROC dbo.Proc_GetCountryList          
(          
 @COUNTRY_ID NVARCHAR(50)          
)          
AS          
BEGIN   
  DECLARE @STRSQL VARCHAR(1000)  
  SET @STRSQL = 'SELECT COUNTRY_NAME FROM MNT_COUNTRY_LIST WHERE COUNTRY_ID in (' + @COUNTRY_ID + ')'  
  EXEC (@STRSQL)  
END          
          
          
        
    
  




GO

