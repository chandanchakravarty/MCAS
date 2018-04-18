CREATE PROCEDURE [dbo].[Proc_GetCedantNameList]    
AS  
Begin  
  
Select distinct a.CedantId,b.CedantName from MNT_InsruanceM a  
left outer join MNT_Cedant b on  a.CedantId = b.CedantId   
where b.CedantId is not null ORDER BY b.CedantName  
End