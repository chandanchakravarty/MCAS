IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertUpdateQuickQuoteUserDefaultXml]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertUpdateQuickQuoteUserDefaultXml]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------      
Proc Name          : Dbo.Proc_InsertUpdateQuickQuoteUserDefaultXml      
Created by         : Deepak Gupta      
Date               : 29/07/2005      
Purpose            : To fetch the vin master values for xml      
Revison History    :      
Used In            : Wolverine        
------------------------------------------------------------      
Date     Review By          Comments      
------   ------------       -------------------------*/      
CREATE   PROCEDURE Dbo.Proc_InsertUpdateQuickQuoteUserDefaultXml      
 @USER_ID   INT,      
 @DEFAULT_XML   TEXT,      
 @LOB   varchar(10) ,  
 @STATE   varchar(30)  
      
AS     
  
--Get StateName                                    
DECLARE @STATE_ID INT                                    
SELECT @STATE_ID=STATE_ID FROM MNT_COUNTRY_STATE_LIST                                    
WHERE STATE_NAME=@STATE    
   
BEGIN      
 If Exists(Select 1 From MNT_QUICKQUOTE_USER_XML Where User_Id = @USER_ID and LOB=@LOB and STATE = @STATE_ID)      
 Begin      
  Update MNT_QUICKQUOTE_USER_XML Set DEFAULT_XML = @DEFAULT_XML WHere User_Id = @USER_ID and LOB=@LOB  and STATE = @STATE_ID   
 End      
 Else      
 Begin      
  Insert Into MNT_QUICKQUOTE_USER_XML(USER_ID,DEFAULT_XML,LOB,STATE) Values(@USER_ID,@DEFAULT_XML,@LOB,@STATE_ID)      
 End      
END      
      
/*      
      
*/      
      
      
      
      
      
    
  



GO

