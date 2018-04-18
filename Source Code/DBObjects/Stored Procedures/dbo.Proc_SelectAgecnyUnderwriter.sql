IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_SelectAgecnyUnderwriter]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_SelectAgecnyUnderwriter]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE  PROC [dbo].[Proc_SelectAgecnyUnderwriter]            
(            
@intLobId  smallint,               
@intAgencyId  int            
)            
 AS            
BEGIN            
declare @ret varchar(255)            
declare @checkRec varchar(255)    
  
/* Changed because now underwriters are storing different table*/  
SELECT @checkRec=UNDERWRITERS FROM MNT_AGENCY_UNDERWRITERS where AGENCY_ID=@intAgencyId and LOB_ID=@intLobId         
if @checkRec <> '' and @checkRec is not null  
   begin   
   
   set @ret = 'select user_id ,          
       user_fname +'' ''+user_lname as username ,IS_ACTIVE  from mnt_user_list where user_id in (' + (SELECT  isnull(UNDERWRITERS,'') UNDERWRITERS   
        FROM MNT_AGENCY_UNDERWRITERS (NOLOCK)           
        where AGENCY_ID=@intAgencyId and LOB_ID=@intLobId) + ')          
     ORDER BY USER_FNAME ASC'            
 -- print @ret             
  execute (@ret )    
 end  
          
/*       
if(@intLobId=1)            
begin            
SELECT @checkRec=HOME_UNDERWRITER FROM MNT_AGENCY_LIST (NOLOCK) where AGENCY_ID=@intAgencyId            
if @checkRec <> '' and @checkRec is not null              
begin            
            
set @ret = 'select user_id ,user_fname +'' ''+user_lname as username          
--Done for Itrack Issue 6658 on 2 Nov 09          
,IS_ACTIVE          
 from mnt_user_list where user_id in (' + (SELECT  isnull(HOME_UNDERWRITER,'') HOME_UNDERWRITER FROM MNT_AGENCY_LIST (NOLOCK) where AGENCY_ID=@intAgencyId) + ')          
ORDER BY USER_FNAME ASC'            
          
execute (@ret )            
end            
end                
            
            
else if(@intLobId=2)            
begin            
--Oct 5,2005:Sumit:ISNULL check has been added to private_underwriter to check for null values          
--Previous statement has been commented          
--SELECT  @checkRec=PRIVATE_UNDERWRITER FROM MNT_AGENCY_LIST where AGENCY_ID=@intAgencyId            
SELECT  @checkRec=ISNULL(PRIVATE_UNDERWRITER,'') FROM MNT_AGENCY_LIST (NOLOCK) where AGENCY_ID=@intAgencyId            
--print @checkRec            
if @checkRec <> '' and @checkRec is not null              
 begin            
             
  set @ret = 'select user_id ,          
       user_fname +'' ''+user_lname as username ,IS_ACTIVE  from mnt_user_list           
              where user_id in (' + (SELECT  isnull(PRIVATE_UNDERWRITER,'') PRIVATE_UNDERWRITER           
     FROM MNT_AGENCY_LIST (NOLOCK)           
    where AGENCY_ID=@intAgencyId) + ')          
  ORDER BY USER_FNAME ASC'            
 -- print @ret             
  execute (@ret )            
 end             
end             
              
else if(@intLobId=3)            
begin            
SELECT  @checkRec=MOTOR_UNDERWRITER FROM MNT_AGENCY_LIST (NOLOCK)  where AGENCY_ID=@intAgencyId            
if @checkRec <> '' and @checkRec is not null              
 begin            
             
  set @ret = 'select user_id ,user_fname +'' ''+user_lname as username ,IS_ACTIVE from mnt_user_list where user_id in (' + (SELECT  isnull(MOTOR_UNDERWRITER,'') MOTOR_UNDERWRITER FROM MNT_AGENCY_LIST (NOLOCK) where AGENCY_ID=@intAgencyId) + ')          
ORDER BY USER_FNAME ASC'            
  --print @ret             
  execute (@ret )            
 end             
end                
            
else if(@intLobId=4)         
begin            
SELECT  @checkRec=WATER_UNDERWRITER FROM MNT_AGENCY_LIST (NOLOCK) where AGENCY_ID=@intAgencyId            
if @checkRec <> '' and @checkRec is not null              
 begin            
             
  set @ret = 'select user_id ,user_fname +'' ''+user_lname as username ,IS_ACTIVE from mnt_user_list where user_id in (' + (SELECT  isnull(WATER_UNDERWRITER,'') WATER_UNDERWRITER FROM MNT_AGENCY_LIST (NOLOCK) where AGENCY_ID=@intAgencyId) + ')          
 ORDER BY USER_FNAME ASC'            
  --print @ret             
  execute (@ret )            
 end             
end              
            
else if(@intLobId=5)       
begin            
SELECT  @checkRec=UMBRELLA_UNDERWRITER FROM MNT_AGENCY_LIST (NOLOCK) where AGENCY_ID=@intAgencyId            
if @checkRec <> '' and @checkRec is not null              
 begin            
             
  set @ret = 'select user_id ,user_fname +'' ''+user_lname as username ,IS_ACTIVE from mnt_user_list where user_id in (' + (SELECT  isnull(UMBRELLA_UNDERWRITER,'') UMBRELLA_UNDERWRITER FROM MNT_AGENCY_LIST (NOLOCK) where AGENCY_ID=@intAgencyId) + ')     
  
     
     
ORDER BY USER_FNAME ASC'            
  --print @ret             
  execute (@ret )            
 end             
end              
            
else if(@intLobId=6)            
begin            
SELECT  @checkRec=RENTAL_UNDERWRITER FROM MNT_AGENCY_LIST (NOLOCK) where AGENCY_ID=@intAgencyId            
if @checkRec <> '' and @checkRec is not null              
 begin            
             
  set @ret = 'select user_id ,user_fname +'' ''+user_lname as username ,IS_ACTIVE from mnt_user_list where user_id in (' + (SELECT  isnull(RENTAL_UNDERWRITER,'') RENTAL_UNDERWRITER FROM MNT_AGENCY_LIST (NOLOCK) where AGENCY_ID=@intAgencyId) + ')          
 ORDER BY USER_FNAME ASC'            
  --print @ret             
  execute (@ret )            
 end             
end             
            
else if(@intLobId=7)            
begin            
SELECT  @checkRec=GENERAL_UNDERWRITER FROM MNT_AGENCY_LIST (NOLOCK) where AGENCY_ID=@intAgencyId            
if @checkRec <> '' and @checkRec is not null              
 begin            
             
  set @ret = 'select user_id ,user_fname +'' ''+user_lname as username ,IS_ACTIVE from mnt_user_list where user_id in (' + (SELECT  isnull(GENERAL_UNDERWRITER,'') GENERAL_UNDERWRITER FROM MNT_AGENCY_LIST (NOLOCK) where AGENCY_ID=@intAgencyId) + ')        
  
   
ORDER BY USER_FNAME ASC'            
  --print @ret             
  execute (@ret )            
 end             
end           
      
--Added by Charles on 8-Mar-2010 for Aviation LOB      
else if(@intLobId=8)            
begin            
SELECT  @checkRec=PRIVATE_UNDERWRITER FROM MNT_AGENCY_LIST (NOLOCK) where AGENCY_ID=@intAgencyId            
if @checkRec <> '' and @checkRec is not null              
 begin            
             
  set @ret = 'select user_id ,user_fname +'' ''+user_lname as username ,IS_ACTIVE from mnt_user_list where user_id in (' + (SELECT  isnull(PRIVATE_UNDERWRITER,'') PRIVATE_UNDERWRITER FROM MNT_AGENCY_LIST (NOLOCK) where AGENCY_ID=@intAgencyId) + ')        
  
   
ORDER BY USER_FNAME ASC'            
  --print @ret             
  execute (@ret )            
 end             
end         
--Added till here         
    
--Added by Charles on 21-Apr-2010 for Other LOB      
else     
begin            
SELECT  @checkRec=PRIVATE_UNDERWRITER FROM MNT_AGENCY_LIST (NOLOCK) where AGENCY_ID=@intAgencyId            
if @checkRec <> '' and @checkRec is not null              
 begin            
             
  set @ret = 'select user_id ,user_fname +'' ''+user_lname as username ,IS_ACTIVE from mnt_user_list where user_id in (' + (SELECT  isnull(PRIVATE_UNDERWRITER,'') PRIVATE_UNDERWRITER FROM MNT_AGENCY_LIST (NOLOCK) where AGENCY_ID=@intAgencyId) + ')        
  
   
ORDER BY USER_FNAME ASC'            
  --print @ret             
  execute (@ret )            
 end             
end */        
--Added till here              
     
END            
GO

