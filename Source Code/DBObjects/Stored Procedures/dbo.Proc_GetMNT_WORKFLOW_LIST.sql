IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetMNT_WORKFLOW_LIST]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetMNT_WORKFLOW_LIST]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

 --Modified By : Charles Gomes
 --Modified On : 13-Apr-2010    
 --purpose	 : Added Multilingual Support MNT_SCREEN_LIST_MULTILINGUAL
 
 --drop proc Proc_GetMNT_WORKFLOW_LIST '486',2
 CREATE procedure [dbo].[Proc_GetMNT_WORKFLOW_LIST]  
(  
 @SCREEN_ID varchar(20),
 @LANG_ID int = 1  
)  
AS  
BEGIN  
 select  a.WORKFLOW_ID,a.GROUP_ID,a.SCREEN_ID,a.TABLE_NAME,a.PRIMARY_KEYS,isnull(bl.SCREEN_DESC,b.SCREEN_DESC) as SCREEN_DESC,
 a.WORKFLOW_ORDER,c.MENU_LINK,A.TAB_NUMBER  
 from MNT_WORKFLOW_LIST a with(nolock)
 inner  join MNT_SCREEN_LIST b with(nolock) on a.SCREEN_ID = b.SCREEN_ID and   
 a.SCREEN_ID = @SCREEN_ID and a.IS_ACTIVE <> 0  
 left outer join MNT_SCREEN_LIST_MULTILINGUAL bl on bl.SCREEN_ID = b.SCREEN_ID and bl.LANG_ID = @LANG_ID
 inner join MNT_MENU_LIST c with(nolock) on c.MENU_ID = a.menu_id  
END  
GO

