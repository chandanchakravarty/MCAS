IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_Fetch_Module]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_Fetch_Module]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

  /*----------------------------------------------------------         
             
Date     Review By          Comments     
  
Modified By: Pradeep Kushwaha
Date       : 22-July-2010  
Purpose    : Multilingual Support                 
  
DROP PROC Proc_Fetch_Module 'F' , 2           
------   ------------       -------------------------*/    

CREATE PROC [dbo].[Proc_Fetch_Module]  
@type nchar(1)='D'  ,
@LANG_ID smallint=1
AS  
BEGIN  

SELECT distinct ISNULL(MM_L.MM_MODULE_ID,MM.MM_MODULE_ID) as MM_MODULE_ID , ISNULL(MM_L.MM_MODULE_NAME, MM.MM_MODULE_NAME) AS MM_MODULE_NAME 
FROM  MNT_MODULE_MASTER MM  with(nolock) 
inner join mnt_module_diarytype_association mda with(nolock) on mm.mm_module_id=mda.mda_module_id   
inner join todolisttypes tdl with(nolock) on tdl.typeid=mda.mda_diarytype_id  
left join MNT_MODULE_MASTER_MULTILINGUAL MM_L with(nolock)  on MM_L.MM_MODULE_ID=MM.MM_MODULE_ID and MM_L.LANG_ID=@LANG_ID     
where  tdl.type_flag=@type order by  ISNULL(MM_L.MM_MODULE_NAME, MM.MM_MODULE_NAME) 
END  
  
 
 
 
 
  
  
  
  

GO

