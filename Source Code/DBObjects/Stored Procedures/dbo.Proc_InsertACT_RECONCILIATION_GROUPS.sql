IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertACT_RECONCILIATION_GROUPS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertACT_RECONCILIATION_GROUPS]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------  
Proc Name       : dbo.ACT_RECONCIAITION_GROUP  
Created by      : Vijay Joshi  
Date            : 6/29/2005  
Purpose     :Insert values into ACT_RECONCIAITION_GROUP  
Revison History :  
Used In        : Wolverine  
------------------------------------------------------------  
Date     Review By          Comments  
------   ------------       -------------------------*/  
-- drop proc dbo.Proc_InsertACT_RECONCILIATION_GROUPS  
CREATE PROC dbo.Proc_InsertACT_RECONCILIATION_GROUPS  
(  
 @GROUP_ID       int OUTPUT,  
 @RECON_ENTITY_ID      int,  
 @RECON_ENTITY_TYPE      nvarchar(10),  
 @IS_COMMITTED      nchar(2),  
 @DATE_COMMITTED      datetime,  
 @COMMITTED_BY      int,  
 @CREATED_BY      int,  
 @CREATED_DATETIME      datetime,  
 @CD_LINE_ITEM_ID int  
)  
AS  
BEGIN  
 /*Geneating the maximumgroup id fields*/  
 SELECT @GROUP_ID = IsNull(Max(GROUP_ID), 0) + 1 FROM ACT_RECONCILIATION_GROUPS  
	
 IF(@CD_LINE_ITEM_ID <> 0 ) --Incase of Reconciliation Group Details 
 BEGIN
  DELETE FROM ACT_RECONCILIATION_GROUPS WHERE CD_LINE_ITEM_ID = @CD_LINE_ITEM_ID  
 END	
  
 INSERT INTO ACT_RECONCILIATION_GROUPS  
 (  
  GROUP_ID,  
  RECON_ENTITY_ID,  
  RECON_ENTITY_TYPE,  
  IS_COMMITTED,  
  DATE_COMMITTED,  
  COMMITTED_BY,  
  IS_ACTIVE,  
  CREATED_BY,  
  CREATED_DATETIME,  
  CD_LINE_ITEM_ID  
 )  
 VALUES  
 (  
  @GROUP_ID,  
  @RECON_ENTITY_ID,  
  @RECON_ENTITY_TYPE,  
  @IS_COMMITTED,  
  @DATE_COMMITTED,  
  @COMMITTED_BY,  
  'Y',  
  @CREATED_BY,  
  @CREATED_DATETIME,  
  @CD_LINE_ITEM_ID  
 )  
END  



GO

