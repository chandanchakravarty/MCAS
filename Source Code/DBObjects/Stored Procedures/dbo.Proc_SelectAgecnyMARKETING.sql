IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_SelectAgecnyMARKETING]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_SelectAgecnyMARKETING]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

 /*----------------------------------------------------------            
Proc Name   : dbo.Proc_SelectMarketeer           
Created by  : Shafi 
Date        : 21 June 2006  
Modified by : sonal 
Modified Date: 10 may 2010     
Purpose     :             
Revison History  :                  
 ------------------------------------------------------------                        
Date     Review By          Comments                      
DROP PROC dbo.Proc_SelectAgecnyMARKETING 
--Proc_SelectAgecnyMARKETING  12,67             
------   ------------       -------------------------*/   
CREATE  PROC [dbo].[Proc_SelectAgecnyMARKETING]      
(      
@intLobId  smallint,         
@intAgencyId  int      
)      
 AS      
BEGIN      
declare @ret varchar(255)      
declare @checkRec varchar(255)  

/* Changed because now underwriters are storing different table new code*/
SELECT @checkRec=MARKETERS FROM MNT_AGENCY_UNDERWRITERS where AGENCY_ID=@intAgencyId and LOB_ID=@intLobId   
 if (@checkRec <> '' and @checkRec is not null)          
    begin    
		
	 set @ret = 'select user_id ,        
       user_fname +'' ''+user_lname as username ,IS_ACTIVE  from mnt_user_list where user_id in (' + (SELECT  isnull(MARKETERS,'') MARKETERS 
        FROM MNT_AGENCY_UNDERWRITERS (NOLOCK)         
        where AGENCY_ID=@intAgencyId and LOB_ID=@intLobId) + ')        
     ORDER BY USER_FNAME ASC'   
     execute (@ret )  
    end   
    
/*   
if(@intLobId=1)      
begin      
SELECT @checkRec=HOME_MARKETING FROM MNT_AGENCY_LIST where AGENCY_ID=@intAgencyId      
if @checkRec <> '' and @checkRec is not null        
begin      
      
set @ret = 'select user_id ,user_fname +'' ''+user_lname as username  from mnt_user_list where user_id in (' + (SELECT  isnull(HOME_MARKETING,'') HOME_MARKETING FROM MNT_AGENCY_LIST where AGENCY_ID=@intAgencyId) + ')   
ORDER BY USER_FNAME ASC'      
execute (@ret )      
end      
end          
      
      
if(@intLobId=2)      
begin      
--Oct 5,2005:Sumit:ISNULL check has been added to private_MARKETING to check for null values    
--Previous statement has been commented    
--SELECT  @checkRec=PRIVATE_MARKETING FROM MNT_AGENCY_LIST where AGENCY_ID=@intAgencyId      
SELECT  @checkRec=ISNULL(PRIVATE_MARKETING,'') FROM MNT_AGENCY_LIST where AGENCY_ID=@intAgencyId      
--print @checkRec      
if @checkRec <> '' and @checkRec is not null        
 begin      
       
  set @ret = 'select user_id ,user_fname +'' ''+user_lname as username  from mnt_user_list where user_id in (' + (SELECT  isnull(PRIVATE_MARKETING,'') PRIVATE_MARKETING FROM MNT_AGENCY_LIST where AGENCY_ID=@intAgencyId) + ')   
ORDER BY USER_FNAME ASC'      
 -- print @ret       
  execute (@ret )      
 end       
end       
        
if(@intLobId=3)      
begin      
SELECT  @checkRec=MOTOR_MARKETING FROM MNT_AGENCY_LIST where AGENCY_ID=@intAgencyId      
if @checkRec <> '' and @checkRec is not null        
 begin      
       
  set @ret = 'select user_id ,user_fname +'' ''+user_lname as username  from mnt_user_list where user_id in (' + (SELECT  isnull(MOTOR_MARKETING,'') MOTOR_MARKETING FROM MNT_AGENCY_LIST where AGENCY_ID=@intAgencyId) + ')   
ORDER BY USER_FNAME ASC'      
  --print @ret       
  execute (@ret )      
 end       
end          
      
if(@intLobId=4)      
begin      
SELECT  @checkRec=WATER_MARKETING FROM MNT_AGENCY_LIST where AGENCY_ID=@intAgencyId      
if @checkRec <> '' and @checkRec is not null        
 begin      
       
  set @ret = 'select user_id ,user_fname +'' ''+user_lname as username  from mnt_user_list where user_id in (' + (SELECT  isnull(WATER_MARKETING,'') WATER_MARKETING FROM MNT_AGENCY_LIST where AGENCY_ID=@intAgencyId) + ')  
 ORDER BY USER_FNAME ASC'      
  --print @ret       
  execute (@ret )      
 end       
end        
      
if(@intLobId=5)      
begin      
SELECT  @checkRec=UMBRELLA_MARKETING FROM MNT_AGENCY_LIST where AGENCY_ID=@intAgencyId      
if @checkRec <> '' and @checkRec is not null        
 begin      
       
  set @ret = 'select user_id ,user_fname +'' ''+user_lname as username  from mnt_user_list where user_id in (' + (SELECT  isnull(UMBRELLA_MARKETING,'') UMBRELLA_MARKETING FROM MNT_AGENCY_LIST where AGENCY_ID=@intAgencyId) + ')   
 ORDER BY USER_FNAME ASC'      
  --print @ret       
  execute (@ret )      
 end       
end        
      
if(@intLobId=6)      
begin      
SELECT  @checkRec=RENTAL_MARKETING FROM MNT_AGENCY_LIST where AGENCY_ID=@intAgencyId      
if @checkRec <> '' and @checkRec is not null        
 begin      
       
set @ret = 'select user_id ,user_fname +'' ''+user_lname as username  from mnt_user_list where user_id in (' + (SELECT  isnull(RENTAL_MARKETING,'') RENTAL_MARKETING FROM MNT_AGENCY_LIST where AGENCY_ID=@intAgencyId) + ')   
ORDER BY USER_FNAME ASC'      
  --print @ret       
  execute (@ret )      
 end       
end       
      
if(@intLobId=7)      
begin      
SELECT  @checkRec=GENERAL_MARKETING FROM MNT_AGENCY_LIST where AGENCY_ID=@intAgencyId      
if @checkRec <> '' and @checkRec is not null        
 begin      
       
  set @ret = 'select user_id ,user_fname +'' ''+user_lname as username  from mnt_user_list where user_id in (' + (SELECT  isnull(GENERAL_MARKETING,'') GENERAL_MARKETING FROM MNT_AGENCY_LIST where AGENCY_ID=@intAgencyId) + ')   
ORDER BY USER_FNAME ASC'      
  --print @ret       
  execute (@ret )      
 end       
end  */       
    
END      
--END     
  
  
  
GO

