IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_GET_YearBuiltOfDewelling]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_GET_YearBuiltOfDewelling]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------    
PROC NAME                : DBO.PROC_GET_YearBuiltOfDewelling    
CREATED BY               : Shafee    
DATE                     : 10/1/2006  
PURPOSE                  : TO GET Year Of Dewelling   
REVISON HISTORY         :  WOLVERINE    
------------------------------------------------------------    
DATE     REVIEW BY          COMMENTS    
------   ------------       -------------------------*/    
-- drop proc PROC_GET_YearBuiltOfDewelling    
CREATE  PROC DBO.PROC_GET_YearBuiltOfDewelling    
(    
    
 @CUSTOMER_ID     int,        
 @APP_ID     int,        
 @APP_VERSION_ID     smallint,        
 @DWELLING_ID   int   
)    
    
AS    
BEGIN    
    
SELECT APP_INFO.APP_EFFECTIVE_DATE AS APP_EFFECTIVE_DATE,
YEAR(CONVERT(VARCHAR(20),APP_INFO.APP_EFFECTIVE_DATE,109)) AS APP_YEAR,
YEAR(CONVERT(VARCHAR(20),APP_INFO.APP_EFFECTIVE_DATE,109)) - DWEL_INFO.YEAR_BUILT  as DWELLING_AGE,
DWEL_INFO.YEAR_BUILT as YEAR_BUILT
FROM APP_DWELLINGS_INFO DWEL_INFO
INNER JOIN  APP_LIST APP_INFO ON
	    APP_INFO.CUSTOMER_ID = DWEL_INFO.CUSTOMER_ID
	AND APP_INFO.APP_ID = DWEL_INFO.APP_ID
	AND APP_INFO.APP_VERSION_ID = DWEL_INFo.APP_VERSION_ID
 WHERE
DWEL_INFO.CUSTOMER_ID=@CUSTOMER_ID AND 
DWEL_INFO.APP_ID=@APP_ID AND 
DWEL_INFO.APP_VERSION_ID=@APP_VERSION_ID AND
DWEL_INFO.DWELLING_ID=@DWELLING_ID
END    
    
    
    
  





GO

