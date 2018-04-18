IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetApplicationState]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetApplicationState]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------    
Proc Name              : Dbo.Proc_GetApplicationState    
Created by             : Pradeep Iyer    
Date                   : Oct 7 , 2005    
Purpose                :     
Revison History        :    
Used In                :   Wolverine      
------------------------------------------------------------    
Date     Review By          Comments    
------   ------------       -------------------------*/   
--DROP PROC Proc_GetApplicationState 
CREATE    PROCEDURE dbo.Proc_GetApplicationState    
(    
 @CUSTOMER_ID int,    
 @APP_ID int,    
 @APP_VERSION_ID int     
     
)    
AS    
BEGIN    
    
SELECT CS.STATE_ID,    
       substring(upper(CS.STATE_NAME),1,1) + substring(lower(CS.STATE_NAME),2,len(CS.STATE_NAME)-1) as STATE_NAME,  
       A.APP_LOB as LOB_ID,
       A.APP_EFFECTIVE_DATE,
	CS.STATE_CODE,
       YEAR(A.APP_EFFECTIVE_DATE ) as APP_YEAR
FROM APP_LIST A    
INNER JOIN MNT_COUNTRY_STATE_LIST CS ON    
 A.STATE_ID = CS.STATE_ID    
WHERE  A.CUSTOMER_ID=@CUSTOMER_ID    AND    
               A.APP_ID=@APP_ID     AND    
               A.APP_VERSION_ID=@APP_VERSION_ID AND    
  CS.COUNTRY_ID = 1    
    
END    
    
  









GO

