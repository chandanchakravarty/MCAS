IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetQuickQuoteUserDefaultXml]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetQuickQuoteUserDefaultXml]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------    
Proc Name          : Dbo.Proc_GetQuickQuoteUserDefaultXml    
Created by         : Deepak Gupta    
Date               : 29/07/2005    
Purpose            : To fetch the vin master values for xml    
Revison History    :    
Used In            : Wolverine      
------------------------------------------------------------    
Date     Review By          Comments    
------   ------------       -------------------------*/    
CREATE   PROCEDURE Dbo.Proc_GetQuickQuoteUserDefaultXml    
 @USER_ID   INT,    
 @LOB   Varchar(10),   
 @STATE Varchar(30)   
AS    
  
--Get StateName                                      
DECLARE @STATE_ID INT                                      
SELECT @STATE_ID=STATE_ID FROM MNT_COUNTRY_STATE_LIST                                      
WHERE STATE_NAME=@STATE     
  
BEGIN    
 Select DEFAULT_XML From MNT_QUICKQUOTE_USER_XML Where User_Id = @USER_ID And LOB=@Lob and STATE=@STATE_ID  
END    
    
/*    
Proc_GetQuickQuoteUserDefaultXml 96,'AUTO'    
*/    
    
  



GO

