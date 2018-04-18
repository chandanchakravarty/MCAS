IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_SelectAgecny_MrkUW]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_SelectAgecny_MrkUW]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                
Proc Name       : dbo.Proc_SelectAgecny_MrkUW              
Created by      : Sonal     
Date            : 13 may 2010              
Purpose         :To fetch assigned marketers and underwriters                
Revison History :                
Used In   : Ebix Advantage                
------------------------------------------------------------                
Date     Review By          Comments                
------   ------------       -------------------------*/                
--DROP PROC Proc_SelectAgecny_MrkUW  9,67,'POL'  
Create  PROC [dbo].[Proc_SelectAgecny_MrkUW]                
(                
@intLobId  smallint,                   
@intAgencyId  int,          
@strselectType char(3)        
 /*AM- assigned marketers,AUW-assigned underwriters,UM-Unassigned marketers, UUW-Unassigned underwriters*/          
                
)                
 AS                
BEGIN                
              
 declare @checkMrRec varchar(500),                
 @checkUWRec varchar(500),        
 @ret nvarchar(max),--varchar(500),  --Changed size, Charles (4-Jun-10)
 @CARRIER_CODE nvarchar(10)      
       
 --Added by Charles on 24-May-2010 for Itrack 51        
SELECT  @CARRIER_CODE=ISNULL(REIN.REIN_COMAPANY_CODE,'') FROM MNT_SYSTEM_PARAMS SYSP WITH(NOLOCK)         
INNER JOIN MNT_REIN_COMAPANY_LIST REIN WITH(NOLOCK) ON REIN.REIN_COMAPANY_ID = SYSP.SYS_CARRIER_ID        
         
 SELECT @checkUWRec=UNDERWRITERS, @checkMrRec=MARKETERS FROM MNT_AGENCY_UNDERWRITERS with(nolock) where AGENCY_ID=@intAgencyId and LOB_ID=@intLobId        
 --SELECT @checkMrRec=MARKETERS FROM MNT_AGENCY_UNDERWRITERS with(nolock) where AGENCY_ID=@intAgencyId and LOB_ID=@intLobId           
         
  if (@strselectType ='AM')          
    begin           
     if (@checkMrRec <> '' and @checkMrRec is not null)                  
  begin            
  set @ret = 'select user_id ,                    
   user_fname +'' ''+user_lname as username ,IS_ACTIVE  from mnt_user_list WITH(NOLOCK) where user_id in (' + (SELECT  CASE WHEN SUBSTRING(ISNULL(MARKETERS,''),LEN(ISNULL(MARKETERS,'')),1) = ',' THEN 
SUBSTRING(ISNULL(MARKETERS,''),1,LEN(ISNULL(MARKETERS,''))-1) ELSE ISNULL(MARKETERS,'') END               
   FROM MNT_AGENCY_UNDERWRITERS (NOLOCK)                     
   where AGENCY_ID=@intAgencyId and LOB_ID=@intLobId) + ')                    
   ORDER BY USER_FNAME ASC'         
  end           
 end          
                
  if (@strselectType ='AUW')          
              
      begin          
        if (@checkUWRec <> '' and @checkUWRec is not null)                  
   begin          
   set @ret = 'select user_id ,                    
   user_fname +'' ''+user_lname as username ,IS_ACTIVE  from mnt_user_list WITH(NOLOCK) where user_id in (' + (SELECT  CASE WHEN SUBSTRING(ISNULL(UNDERWRITERS,''),LEN(ISNULL(UNDERWRITERS,'')),1) = ',' THEN 
SUBSTRING(ISNULL(UNDERWRITERS,''),1,LEN(ISNULL(UNDERWRITERS,''))-1) ELSE ISNULL(UNDERWRITERS,'') END UNDERWRITERS             
   FROM MNT_AGENCY_UNDERWRITERS (NOLOCK)                     
   where AGENCY_ID=@intAgencyId and LOB_ID=@intLobId) + ')                    
   ORDER BY USER_FNAME ASC'         
   end          
  end          
                
  if (@strselectType ='UM')             
  begin            
  if @checkMrRec <> '' and @checkMrRec is not null          
  begin                    
   set @ret ='SELECT USER_ID,USER_FNAME+'' ''+USER_LNAME AS USER_NAME FROM MNT_USER_LIST MUL WITH(NOLOCK)             
   INNER JOIN MNT_USER_TYPES MUT WITH(NOLOCK)             
   ON MUL.USER_TYPE_ID=MUT.USER_TYPE_ID             
   WHERE USER_SYSTEM_ID='''+@CARRIER_CODE+''' and  user_id not in (' + (SELECT  CASE WHEN SUBSTRING(ISNULL(MARKETERS,''),LEN(ISNULL(MARKETERS,'')),1) = ',' THEN 
SUBSTRING(ISNULL(MARKETERS,''),1,LEN(ISNULL(MARKETERS,''))-1) ELSE ISNULL(MARKETERS,'') END FROM MNT_AGENCY_UNDERWRITERS (NOLOCK)                     
   where AGENCY_ID=@intAgencyId and LOB_ID=@intLobId) + ')            
   ORDER BY USER_FNAME ASC'         
  --print @ret          
          
  end         
  else        
  begin        
   set @ret ='SELECT USER_ID,USER_FNAME+'' ''+USER_LNAME AS USER_NAME FROM MNT_USER_LIST MUL WITH(NOLOCK)             
   INNER JOIN MNT_USER_TYPES MUT WITH(NOLOCK)             
   ON MUL.USER_TYPE_ID=MUT.USER_TYPE_ID             
   WHERE USER_SYSTEM_ID='''+@CARRIER_CODE+''' ORDER BY USER_FNAME ASC'         
  end           
    end          
                
    if (@strselectType ='UUW')          
      begin          
       if @checkUWRec <> '' and @checkUWRec is not null          
  begin                 
   set @ret =' SELECT USER_ID,USER_FNAME+'' ''+USER_LNAME AS USER_NAME FROM MNT_USER_LIST MUL WITH(NOLOCK)           
   INNER JOIN MNT_USER_TYPES MUT WITH(NOLOCK)           
   ON MUL.USER_TYPE_ID=MUT.USER_TYPE_ID            
   AND MUT.USER_TYPE_CODE=''UWT''            
   WHERE USER_SYSTEM_ID='''+@CARRIER_CODE+''' and user_id not in (' + (SELECT  CASE WHEN SUBSTRING(ISNULL(UNDERWRITERS,''),LEN(ISNULL(UNDERWRITERS,'')),1) = ',' THEN 
SUBSTRING(ISNULL(UNDERWRITERS,''),1,LEN(ISNULL(UNDERWRITERS,''))-1) ELSE ISNULL(UNDERWRITERS,'') END FROM MNT_AGENCY_UNDERWRITERS (NOLOCK)                     
   where AGENCY_ID=@intAgencyId and LOB_ID=@intLobId) + ') order by MUL.USER_FNAME ASC'             
           
 --print (@ret)        
         
  end         
  else        
  begin        
   set @ret ='SELECT USER_ID,USER_FNAME+'' ''+USER_LNAME AS USER_NAME FROM MNT_USER_LIST MUL WITH(NOLOCK)           
   INNER JOIN MNT_USER_TYPES MUT WITH(NOLOCK)           
   ON MUL.USER_TYPE_ID=MUT.USER_TYPE_ID            
   AND MUT.USER_TYPE_CODE=''UWT''            
   WHERE USER_SYSTEM_ID='''+@CARRIER_CODE+''' order by MUL.USER_FNAME ASC'        
  end        
    end 
    if (@strselectType ='POL')
    
    BEGIN
       set  @ret ='SELECT DISTINCT UNDERWRITER from POL_CUSTOMER_POLICY_LIST WHERE AGENCY_ID='+cast(@intAgencyId as varchar(25))
       +' AND POLICY_LOB='+cast(@intLobId as varchar(25))+' AND UNDERWRITER IS NOT NULL AND UNDERWRITER!=0'          
    
   --print @ret
    END
         
    execute (@ret )           
              
END 
GO

