IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdateUserLastVisitedPages]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdateUserLastVisitedPages]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------      
Proc Name       : dbo.Proc_UpdateUserLastVisitedPages      
Created by      : Sibin     
Date            : 3/18/2009      
	Purpose         : To get User Last Visited Pages      
Revison History :      
Used In         :   Wolverine      
------------------------------------------------------------      
Date     Review By          Comments      
------   ------------       -------------------------*/      
 -- drop proc Proc_UpdateUserLastVisitedPages      
CREATE PROC dbo.Proc_UpdateUserLastVisitedPages      
(      
  @PAGE_INFO nvarchar(200),   
  @CALLED_FROM VARCHAR(20),      
  @USER_SYSTEM_ID varchar(8),	
  @USER_ID int 
)    
AS    
BEGIN      
    
 IF(UPPER(@CALLED_FROM) = 'CUSTOMER')    
  BEGIN
	If not exists(select 1 from LAST_VISITED_ITEMS WHERE USER_ID= @USER_ID AND  USER_SYSTEM_ID = @USER_SYSTEM_ID)     
	Begin
		insert into LAST_VISITED_ITEMS 
		select @USER_ID,@USER_SYSTEM_ID, @PAGE_INFO,NULL,NULL,NULL,NULL
	end
	else 
	begin
		UPDATE LAST_VISITED_ITEMS SET LAST_VISITED_CUSTOMER = @PAGE_INFO WHERE USER_ID= @USER_ID AND  USER_SYSTEM_ID = @USER_SYSTEM_ID
	end
  END    
 IF(UPPER(@CALLED_FROM) = 'APPLICATION')    
  BEGIN  
	If not exists(select 1 from LAST_VISITED_ITEMS WHERE USER_ID= @USER_ID AND  USER_SYSTEM_ID = @USER_SYSTEM_ID)     
	begin
		insert into LAST_VISITED_ITEMS 
		select @USER_ID,@USER_SYSTEM_ID, NULL,@PAGE_INFO,NULL,NULL,NULL
	end
	else
	Begin
		UPDATE LAST_VISITED_ITEMS SET LAST_VISITED_APPLICATION = @PAGE_INFO WHERE USER_ID= @USER_ID AND  USER_SYSTEM_ID = @USER_SYSTEM_ID    
	end
  END    
IF(UPPER(@CALLED_FROM) = 'POLICY')    
  BEGIN   
	If not exists(select 1 from LAST_VISITED_ITEMS WHERE USER_ID= @USER_ID AND  USER_SYSTEM_ID = @USER_SYSTEM_ID)     
	begin
		insert into LAST_VISITED_ITEMS 
		select @USER_ID,@USER_SYSTEM_ID, NULL,NULL,@PAGE_INFO,NULL,NULL
	end  
	else
	begin
		UPDATE LAST_VISITED_ITEMS SET LAST_VISITED_POLICY = @PAGE_INFO WHERE USER_ID= @USER_ID AND  USER_SYSTEM_ID = @USER_SYSTEM_ID    
	end
  END    
IF(UPPER(@CALLED_FROM) = 'CLAIM')    
  BEGIN    
	If not exists(select 1 from LAST_VISITED_ITEMS WHERE USER_ID= @USER_ID AND  USER_SYSTEM_ID = @USER_SYSTEM_ID)     
	begin
		insert into LAST_VISITED_ITEMS 
		select @USER_ID,@USER_SYSTEM_ID, NULL,NULL,NULL,@PAGE_INFO,NULL
	end  
	else
	begin 
		UPDATE LAST_VISITED_ITEMS SET LAST_VISITED_CLAIM = @PAGE_INFO WHERE USER_ID= @USER_ID AND  USER_SYSTEM_ID = @USER_SYSTEM_ID   
	end
  END    
IF(UPPER(@CALLED_FROM) = 'QUOTE')    
  BEGIN     
	If not exists(select 1 from LAST_VISITED_ITEMS WHERE USER_ID= @USER_ID AND  USER_SYSTEM_ID = @USER_SYSTEM_ID)     
	begin
		insert into LAST_VISITED_ITEMS 
		select @USER_ID,@USER_SYSTEM_ID, NULL,NULL,NULL,NULL,@PAGE_INFO
	end  
	else
	begin
		UPDATE LAST_VISITED_ITEMS SET LAST_VISITED_QUOTE = @PAGE_INFO WHERE USER_ID= @USER_ID AND  USER_SYSTEM_ID = @USER_SYSTEM_ID   
	end
  END    
    
END






 

GO

