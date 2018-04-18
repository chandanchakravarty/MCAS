IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_FetchDepositNoType]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_FetchDepositNoType]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*-----------------------------------------------  
Name			: Proc_FetchDepositNoType
CREATE BY  		: kranti singh  
CREATED DATETIME	: 15th june 2007 
PURPOSE   		: fatch deposit type and deposit number based on CD_LINE_ITEM_ID
  
------------------------------------------------*/  
--drop proc dbo.Proc_FetchDepositNoType  
CREATE PROC dbo.Proc_FetchDepositNoType  
(  
 @CD_LINE_ITEM_ID INT  
)  
as  
BEGIN     

SELECT ACDLI.DEPOSIT_TYPE,ACD.DEPOSIT_NUMBER FROM  ACT_CURRENT_DEPOSIT_LINE_ITEMS ACDLI
LEFT OUTER JOIN ACT_CURRENT_DEPOSITS ACD ON ACD.DEPOSIT_ID=ACDLI.DEPOSIT_ID
WHERE ACDLI.CD_LINE_ITEM_ID =@CD_LINE_ITEM_ID
  
END  
  


GO

