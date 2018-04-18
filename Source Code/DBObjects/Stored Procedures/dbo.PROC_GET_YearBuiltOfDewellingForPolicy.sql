IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_GET_YearBuiltOfDewellingForPolicy]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_GET_YearBuiltOfDewellingForPolicy]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------      
PROC NAME                : DBO.PROC_GET_YearBuiltOfDewellingForPolicy      
CREATED BY               : Shafee      
DATE                     : 10/1/2006    
PURPOSE                  : TO GET Year Of Dewelling     
REVISON HISTORY         :  WOLVERINE      
------------------------------------------------------------      
DATE     REVIEW BY          COMMENTS      
------   ------------       -------------------------*/      
CREATE  PROC DBO.PROC_GET_YearBuiltOfDewellingForPolicy      
(      
      
 @CUSTOMER_ID     int,          
 @APP_ID     int,          
 @APP_VERSION_ID     smallint,          
 @DWELLING_ID   int     
)      
      
AS      
BEGIN      
      
SELECT YEAR_BUILT  
FROM POL_DWELLINGS_INFO  WHERE  
CUSTOMER_ID=@CUSTOMER_ID AND   
POLICY_ID=@APP_ID AND   
POLICY_VERSION_ID=@APP_VERSION_ID AND  
DWELLING_ID=@DWELLING_ID  
END      
      

      
      
    
  



GO

