IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetDiarydates]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetDiarydates]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------          
Proc Name     : dbo.Proc_GetDiarydates    
Created by      : Anurag Verma          
Date                  : 17/03/2005          
Purpose         : To retrieve appointement dates    
Revison History :          
    
modified by  :  Pravesh    
Modified Date : 29 May 2008    
    
    
Used In         : Wolverine          
------------------------------------------------------------          
Date     Review By          Comments          
------   ------------       -------------------------*/          
-- drop PROC dbo.Proc_GetDiarydates 68, 1001    
CREATE  PROC [dbo].[Proc_GetDiarydates]     
(          
 @USER_ID nvarchar(4),    
 @CUSTOMER_ID nvarchar(4) = null    
)          
AS          
SET NOCOUNT ON    
BEGIN        
 if(@USER_ID = '-1')    
 begin    
  if(@CUSTOMER_ID is null or @CUSTOMER_ID = '')    
  begin    
   select convert(varchar(50),followupdate,101) followupdate from todolist with(nolock) where isnull(LISTOPEN,'Y')='Y'    
  end    
  else    
  begin    
   select convert(varchar(50),followupdate,101) followupdate from todolist with(nolock) where CUSTOMER_ID = @CUSTOMER_ID and isnull(LISTOPEN,'Y')='Y'    
  end    
 end    
 else    
 begin    
  --uncommented / rollback changes by Pravesh as it will fail if called from cust asst. for a customer    
  --select convert(varchar(50),followupdate,101) followupdate from todolist with(nolock) where touserid=@USER_ID     
  --Commented by Asfa (02-June-2008) - iTrack 4255    
     
  if(@CUSTOMER_ID is null or @CUSTOMER_ID = '')    
  begin    
   select convert(varchar(50),followupdate,101) followupdate from todolist with(nolock)  where touserid=@USER_ID and isnull(LISTOPEN,'Y')='Y'    
  end    
  else    
  begin    
   select convert(varchar(50),followupdate,101) followupdate from todolist with(nolock) where touserid=@USER_ID and CUSTOMER_ID = @CUSTOMER_ID and isnull(LISTOPEN,'Y')='Y'    
  end    
     
 end    
END    
SET NOCOUNT OFF    
    
    
GO

