IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_CheckUser]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_CheckUser]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================      
-- Modified by :  Pradeep Kushwaha     
-- Modified date: 29-Sep-2010      
-- Reason  :    Get the BASE_CURRENCY from MNT_SYSTEM_PARAMS   
-- DROP PROC Proc_CheckUser  'W001','pradeep','pk'    
-- =============================================        
              
CREATE   PROC [dbo].[Proc_CheckUser]                    
(                          
                    
  @USER_SYSTEM_ID      nvarchar(8),                          
  @USER_LOGIN_ID     nvarchar(20),                          
  @USER_PWD     nvarchar(30)                    
                    
)                          
AS                          
SET NOCOUNT ON                    
BEGIN               
              
-------------added by Pravesh              
DECLARE @EXISTING nVarchar(100)               
DECLARE @USER_BAD_LOGINS smallint,              
  @SYS_BAD_LOGON_ATTMPT smallint,              
  @IS_USER_LOCKED CHAR ,        
  @SYS_BASE_CURRENCY INT,        
  @SYS_CARRIER_ID INT,        
  @SYS_CARRIER_CODE nvarchar(10),
  @SYS_NAVIGATION NVARCHAR(20)        
            
  SET @IS_USER_LOCKED='N'              
SELECT @SYS_BAD_LOGON_ATTMPT=SYS_BAD_LOGON_ATTMPT ,@SYS_BASE_CURRENCY=BASE_CURRENCY,@SYS_CARRIER_ID= SYS_CARRIER_ID
,@SYS_NAVIGATION = SYS_NAVIGATION
 FROM MNT_SYSTEM_PARAMS              
select @SYS_CARRIER_CODE = REIN_COMAPANY_CODE from MNT_REIN_COMAPANY_LIST with(nolock) where REIN_COMAPANY_ID=@SYS_CARRIER_ID        
              
SELECT @EXISTING = dbo.fun_DecriptText(USER_PWD) ,@USER_BAD_LOGINS=isnull(USER_BAD_LOGINS,0)              
 FROM MNT_USER_LIST  ,MNT_AGENCY_LIST AGENCY       
 WHERE USER_LOGIN_ID = @USER_LOGIN_ID       
 --and  User_system_Id=@USER_SYSTEM_ID               
 and  User_system_Id= case when @USER_SYSTEM_ID=@SYS_CARRIER_CODE then USER_SYSTEM_ID else AGENCY.AGENCY_CODE end        
if(@USER_BAD_LOGINS >= @SYS_BAD_LOGON_ATTMPT and isnull(@SYS_BAD_LOGON_ATTMPT,0)!=0)              
  BEGIN              
 UPDATE MNT_USER_LIST SET USER_LOCKED='1',USER_LOCKED_DATETIME=GETDATE() WHERE USER_LOGIN_ID = @USER_LOGIN_ID and  User_system_Id=@USER_SYSTEM_ID               
 SET @IS_USER_LOCKED='Y'              
 END              
if not exists(              
  select distinct user_id, user_login_id,user_system_id,user_image_folder,user_color_scheme,grid_size,                    
  --(case isnull(user_title,'')  when '' then + '' else user_title + ' ' end )  +               
  isnull(user_fname,'') + ' ' + isnull(user_lname,'') user_name,user_type_id,                
  UserList.is_active,          
  --AGENCY.IS_Active as Agency_IsActive,                  
  --user_title + ' ' +  user_fname + ' ' + user_lname user_name                    
  isnull(CHANGE_PWD_NEXT_LOGIN,0) as CHANGE_PWD_NEXT_LOGIN,              
  USER_LOCKED       ,      
  ISNULL(USER_SPR,'N') USER_SPR      
   from MNT_USER_LIST  UserList,MNT_AGENCY_LIST AGENCY                     
  where         
  UserList.USER_SYSTEM_ID=case when @USER_SYSTEM_ID=@SYS_CARRIER_CODE then UserList.USER_SYSTEM_ID else AGENCY.AGENCY_CODE end        
  --and User_system_Id=@USER_SYSTEM_ID       
  and user_login_id=@USER_LOGIN_ID               
  --and dbo.fun_DecriptText(USER_PWD)=@USER_PWD                   
  and CAST(@EXISTING AS VARBINARY) =  CAST(@USER_PWD AS VARBINARY)              
 )              
 UPDATE MNT_USER_LIST SET USER_BAD_LOGINS= ISNULL(USER_BAD_LOGINS,0) +  1 WHERE USER_LOGIN_ID = @USER_LOGIN_ID and  User_system_Id=@USER_SYSTEM_ID               
else              
 UPDATE MNT_USER_LIST SET USER_BAD_LOGINS= null WHERE USER_LOGIN_ID = @USER_LOGIN_ID and  User_system_Id=@USER_SYSTEM_ID               
---------End Here              
              
select distinct user_id, user_login_id,user_system_id,user_image_folder,user_color_scheme,UserList.LANG_ID,Lang_Code,grid_size,            
                  
--(case isnull(user_title,'')  when '' then + '' else user_title + ' ' end )  +               
isnull(user_fname,'') + ' ' + isnull(user_lname,'') user_name,user_type_id,             
             
UserList.is_active,          
case when @USER_SYSTEM_ID=@SYS_CARRIER_CODE then 'Y' else AGENCY.IS_Active end  as Agency_IsActive                  
--user_title + ' ' +  user_fname + ' ' + user_lname user_name                    
,isnull(CHANGE_PWD_NEXT_LOGIN,0) as CHANGE_PWD_NEXT_LOGIN,              
          
case when isnull(USER_LOCKED,'0')='1' then 'Y' else 'N' end as USER_LOCKED  ,@SYS_BASE_CURRENCY as BASE_CURRENCY              
   ,ISNULL(USER_SPR,'N') USER_SPR  --Added  by Lalit March 28,2011 .For sumit anyway rule  
   ,ISNULL(@SYS_NAVIGATION,'') AS    SYS_NAVIGATION
from MNT_USER_LIST  UserList          
          
left join MNT_LANGUAGE_MASTER LANG_MASTER on LANG_MASTER.LANG_ID=UserList.LANG_ID --Added By Chetna          
          
,MNT_AGENCY_LIST AGENCY                  
              
where         
--UserList.USER_SYSTEM_ID=AGENCY.AGENCY_CODE         
UserList.USER_SYSTEM_ID=case when @USER_SYSTEM_ID=@SYS_CARRIER_CODE then UserList.USER_SYSTEM_ID else AGENCY.AGENCY_CODE end        
--and User_system_Id=@USER_SYSTEM_ID       
and user_login_id=@USER_LOGIN_ID           
              
--and dbo.fun_DecriptText(USER_PWD)=@USER_PWD                   
and CAST(@EXISTING AS VARBINARY) =  CAST(@USER_PWD AS VARBINARY)              
--and   AGENCY.IS_Active = 'Y'                
--table 1              
          
          
          
          
          
IF(ISNUMERIC(@USER_LOGIN_ID) = 1)              
 select user_id, user_login_id,user_system_id                   
 from MNT_USER_LIST  UserList,MNT_AGENCY_LIST AGENCY                     
 where UserList.USER_SYSTEM_ID=AGENCY.AGENCY_CODE       
 --and User_system_Id=@USER_SYSTEM_ID       
 and  User_system_Id= case when @USER_SYSTEM_ID=@SYS_CARRIER_CODE then USER_SYSTEM_ID else AGENCY.AGENCY_CODE end        
 and user_id=@USER_LOGIN_ID                   
else              
 select '' user_id, '' user_login_id,'' USER_SYSTEM_ID                    
              
--table 2               
SELECT @IS_USER_LOCKED AS IS_USER_LOCKED           
      
SELECT CONVERT(NVARCHAR(MAX), PHONE_FORMAT +'^'+ ACCOUNT_FORMAT + '^'+ ZIP_FORMAT + '^'+ DATE_FORMAT ) AS BASE_FORMAT,PHONE_FORMAT FROM MNT_SYSTEM_PARAMS with(nolock)    
    
END                    
SET NOCOUNT OFF    
    
--select * from MNT_SYSTEM_PARAMS 