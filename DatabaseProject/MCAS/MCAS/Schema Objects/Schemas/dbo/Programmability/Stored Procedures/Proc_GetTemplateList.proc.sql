---------    
--Created By :: Ashish Kumar    
--Created On :: 26-11-2015    
----------    
CREATE PROCEDURE [dbo].[Proc_GetTemplateList] (@screenId int)    
AS    
  SET FMTONLY OFF;  
SELECT  
  TempIsHeaderSubChild.Template_Id,  
  TempIsHeaderSubChild.Id,  
  TempIsHeaderSubChild.parentId,  
  TempIsHeaderSubChild.ChildId,  
  TempIsHeaderSubChild.SubChildId,  
  TempIsHeaderSubChild.[ChildDescription],  
  TempIsHeaderSubChild.[SubChildDescription],  
  TempIsHeaderSubChild.ScreenId,  
  TempIsHeaderSubChild.[Description],  
  TempIsHeaderSubChild.[Filename],  
  TempIsHeaderSubChild.Display_Name,  
  TempIsHeaderSubChild.Is_Active,  
  TempIsHeaderSubChild.OutPutFormat,  
  TempIsHeaderSubChild.Is_Header,  
  TempIsHeaderSubChild.[ChildIHeader],  
  TempIsHeaderSubChild_.[Is_Header] AS [SubChildHeader]  
FROM (SELECT  
  TempIsHeaderChild.Template_Id,  
  TempIsHeaderChild.Id,  
  TempIsHeaderChild.parentId,  
  TempIsHeaderChild.ChildId,  
  TempIsHeaderChild.SubChildId,  
  TempIsHeaderChild.[ChildDescription],  
  TempIsHeaderChild.[SubChildDescription],  
  TempIsHeaderChild.ScreenId,  
  TempIsHeaderChild.[Description] AS [Description],  
  TempIsHeaderChild.[Filename] AS [Filename],  
  TempIsHeaderChild.Display_Name,  
  TempIsHeaderChild.Is_Active,  
  TempIsHeaderChild.OutPutFormat,  
  TempIsHeaderChild.Is_Header,  
  TempIsHeaderChild_.Is_Header AS [ChildIHeader]  
FROM (SELECT  
  tempSubChild.Template_Id,  
  tempSubChild.Id,  
  tempSubChild.parentId,  
  tempSubChild.ChildId,  
  tempSubChild.SubChildId,  
  tempSubChild.[ChildDescription],  
  tempSubChild_.[Description] AS [SubChildDescription],  
  tempSubChild.ScreenId,  
  tempSubChild.[Description] AS [Description],  
  tempSubChild.[Filename] AS [Filename],  
  tempSubChild.Display_Name,  
  tempSubChild.Is_Active,  
  tempSubChild.OutPutFormat,  
  tempSubChild.Is_Header  
FROM (SELECT  
  tempChild.Template_Id,  
  tempChild.Id,  
  tempChild.parentId,  
  tempChild.ChildId,  
  tempChild.SubChildId,  
  tempChild_.[Description] AS [ChildDescription],  
  tempChild.ScreenId,  
  tempChild.Description,  
  tempChild.Filename,  
  tempChild.Display_Name,  
  tempChild.Is_Active,  
  tempChild.OutPutFormat,  
  tempChild.Is_Header  
FROM (SELECT  
       final1.Template_Id,  
       final1.Id,  
       final.parentId,  
       final.ChildId,  
       final.SubChildId,  
       final1.ScreenId,  
       final1.Description,  
       final1.Filename,  
       final1.Display_Name,  
       final1.Is_Active,  
       final1.OutPutFormat,  
       final1.Is_Header  
     FROM (SELECT  
            temp.parentId,  
            temp.ChildId,  
            t3.id SubChildId  
          FROM (SELECT  
            t2.parentId ParentId,  
            t2.Id ChildId  
          FROM MNT_TEMPLATE_MASTER t1,  
               MNT_TEMPLATE_MASTER t2  
          WHERE t1.Id = t2.ParentId) temp  
          LEFT OUTER JOIN MNT_TEMPLATE_MASTER t3  
            ON temp.childId = t3.ParentId  
          -- order by ParentId    
          EXCEPT  
          ------------    
          SELECT  
            temp2.parentId,  
            temp2.ChildId,  
            temp2.SubChildId  
          FROM (SELECT  
                 temp.parentId,  
                 temp.ChildId,  
                 t3.id SubChildId  
               FROM (SELECT  
                 t2.parentId ParentId,  
                 t2.Id ChildId  
               FROM MNT_TEMPLATE_MASTER t1,  
                    MNT_TEMPLATE_MASTER t2  
               WHERE t1.Id = t2.ParentId) temp  
               LEFT OUTER JOIN MNT_TEMPLATE_MASTER t3  
                 ON temp.childId = t3.ParentId  
               WHERE t3.Id IS NULL) temp2,  
               (SELECT  
                 temp.parentId,  
                 temp.ChildId,  
                 t3.id SubChildId  
               FROM (SELECT  
                 t2.parentId ParentId,  
                 t2.Id ChildId  
               FROM MNT_TEMPLATE_MASTER t1,  
                    MNT_TEMPLATE_MASTER t2  
               WHERE t1.Id = t2.ParentId) temp  
               LEFT OUTER JOIN MNT_TEMPLATE_MASTER t3  
                 ON temp.childId = t3.ParentId  
               WHERE t3.Id IS NOT NULL) temp3  
          WHERE temp2.ChildId = temp3.SubChildId) final,  
          MNT_TEMPLATE_MASTER final1  
     WHERE final.parentId = final1.Id) tempChild,  
     MNT_TEMPLATE_MASTER tempChild_  
WHERE tempChild.ChildId = tempChild_.Id) tempSubChild  
  
LEFT OUTER JOIN MNT_TEMPLATE_MASTER tempSubChild_  
  ON tempSubChild.SubChildId = tempSubChild_.Id  
WHERE tempSubChild.ScreenId = @screenId) TempIsHeaderChild  
LEFT OUTER JOIN MNT_TEMPLATE_MASTER TempIsHeaderChild_  
  ON TempIsHeaderChild.ChildId = TempIsHeaderChild_.Id) TempIsHeaderSubChild  
LEFT OUTER JOIN MNT_TEMPLATE_MASTER TempIsHeaderSubChild_  
  ON TempIsHeaderSubChild.SubChildId = TempIsHeaderSubChild_.Id
GO


