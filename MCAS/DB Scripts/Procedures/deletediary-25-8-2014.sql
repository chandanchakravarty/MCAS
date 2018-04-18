--sp_helptext Proc_ToDoDairyList

  
CREATE Proc [dbo].[Proc_ToDoDairyList]  
(  
@ListID BIGINT  
)  
as  
BEGIN  
SET FMTONLY OFF;  
Delete from dbo.TODODIARYLIST where LISTID = @ListID   
END  
  
  