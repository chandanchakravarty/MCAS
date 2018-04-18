IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_SelectunassignedAgency]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_SelectunassignedAgency]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


Create  PROC [dbo].[Proc_SelectunassignedAgency]                  
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
   execute (@ret )  
   END 
GO

