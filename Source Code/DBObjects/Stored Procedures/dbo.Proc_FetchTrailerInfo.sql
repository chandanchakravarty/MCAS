IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_FetchTrailerInfo]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_FetchTrailerInfo]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------      
Proc Name      : dbo.Proc_FetchTrailerInfo      
Created by       : Anurag Verma      
Date             : 5/18/2005      
Purpose       : retrieving data from APP_WATERCRAFT_TRAILER_INFO      
Revison History :      
Used In        : Wolverine      
      
Modified By : Anurag Verma      
Modified On : 10/10/2005      
Purpose  : Adding Insured_Value field and removing Premium,Cost_new,Deductible,Limit_desired and trailer_value      
  
Modified By : Asfa Praveen  
Modified On : 13/June/2007  
Purpose  : Adding Trailer_Ded field foe Trailer Deductible information  
------------------------------------------------------------      
Date     Review By          Comments      
------   ------------       -------------------------*/      
--  DROP PROC dbo.Proc_FetchTrailerInfo      
CREATE  PROC dbo.Proc_FetchTrailerInfo  
@CUSTOMER_ID INT,      
@APP_ID INT,      
@APP_VERSION_ID INT,      
@TRAILER_ID INT      
AS      
      
BEGIN      
SELECT       
TRAILER_ID,      
TRAILER_NO,      
YEAR,      
MODEL,      
MANUFACTURER,      
SERIAL_NO,      
      
INSURED_VALUE,      
ASSOCIATED_BOAT,      
IS_ACTIVE,    
TRAILER_TYPE,  
TRAILER_DED,
TRAILER_DED_ID,
TRAILER_DED_AMOUNT_TEXT     
      
FROM APP_WATERCRAFT_TRAILER_INFO       
WHERE APP_ID=@APP_ID AND       
APP_VERSION_ID=@APP_VERSION_ID      
AND CUSTOMER_ID=@CUSTOMER_ID AND      
TRAILER_ID=@TRAILER_ID      
END      
      
    
    
  







GO

