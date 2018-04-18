IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdateAttentionNotes]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdateAttentionNotes]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------  
Proc Name       : dbo.Proc_UpdateAttentionNotes  
Created by      :Priya  
Date            : 4/25/2005  
Purpose         : To update the record in customer table  
Revison History :  
Used In         :   wolverine  
------------------------------------------------------------  
Date     Review By          Comments  
------   ------------       -------------------------*/  
CREATE PROC Dbo.Proc_UpdateAttentionNotes  
  
(  
@CustomerId numeric(9),  
@AttentionNote varchar(500),  
@AttentionNoteUpdated datetime  
)  
AS  
BEGIN  
 UPDATE CLT_CUSTOMER_LIST  
 SET   
  CUSTOMER_ATTENTION_NOTE =@AttentionNote,  
  ATTENTION_NOTE_UPDATED =@AttentionNoteUpdated  
    
 WHERE  
                CUSTOMER_ID=@CustomerId  
  
    
END  



GO

