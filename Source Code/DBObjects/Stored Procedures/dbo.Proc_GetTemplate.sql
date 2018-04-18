IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetTemplate]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetTemplate]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                                        
Proc Name               : Dbo.Proc_GetTemplate                                        
Created by              : Swarup                                        
Date                    : 31/07/07                                      
Purpose                 : To get the information of  template                                         
Revison History    :                                        
                               
------------------------------------------------------------                                        
Date     Review By          Comments                                        
------   ------------       -------------------------*/                                        
-- drop PROC Dbo.Proc_GetTemplate     
CREATE  PROC Dbo.Proc_GetTemplate                                     
                                      
AS                                        
                                        
BEGIN   

  SELECT     
	Convert(numeric,JOURNAL_ENTRY_NO) JOURNAL_ENTRY_NO,Convert(varchar,ACT_JOURNAL_MASTER.CREATED_DATETIME,109) AJM,
	Convert(Varchar,TRANS_DATE,101) TRANS_DATE,
	CASE WHEN Len(DESCRIPTION)>28 THEN Substring(DESCRIPTION,0,28) + '...' ELSE DESCRIPTION END DESCRIPTION,
	convert(varchar(30),convert(money,SUM(IsNull(ACT_JOURNAL_LINE_ITEMS.AMOUNT,0))),1) PROFF,ACT_JOURNAL_MASTER.JOURNAL_ID,
	ACT_JOURNAL_MASTER.IS_ACTIVE

  FROM         
   	ACT_JOURNAL_MASTER 
	INNER JOIN ACT_JOURNAL_LINE_ITEMS ON ACT_JOURNAL_MASTER.JOURNAL_ID = ACT_JOURNAL_LINE_ITEMS.JOURNAL_ID 
	
  WHERE     
	IsNull(IS_COMMITED,'')<>'Y' AND IsNull(JOURNAL_GROUP_TYPE,'') = 'TP'  and ACT_JOURNAL_MASTER.IS_ACTIVE = 'Y'

  GROUP BY 
	ACT_JOURNAL_MASTER.JOURNAL_ID,ACT_JOURNAL_MASTER.CREATED_DATETIME,JOURNAL_ENTRY_NO,TRANS_DATE,
	DESCRIPTION,ACT_JOURNAL_MASTER.JOURNAL_ID,ACT_JOURNAL_MASTER.IS_ACTIVE 
 END   
 






GO

