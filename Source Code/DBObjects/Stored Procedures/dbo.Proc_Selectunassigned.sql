IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_Selectunassigned]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_Selectunassigned]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROC [dbo].[Proc_Selectunassigned]                  
(                  
@intLobId  smallint,                     
@intAgencyId  int,            
@strselectType char(3)          
 /*AUW-assigned underwriters,*/            
                  
)                  
 AS                  
BEGIN                  
                
 declare @checkMrRec varchar(500),                  
 @checkUWRec varchar(500),          
 @ret nvarchar(max),  
 @CARRIER_CODE nvarchar(10)    

SELECT @checkUWRec=UNDERWRITERS FROM MNT_AGENCY_UNDERWRITERS with(nolock) where AGENCY_ID=@intAgencyId and UNDERWRITERS='59'    
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
  SELECT  @checkUWRec=UNDERWRITER FROM POL_CUSTOMER_POLICY_LIST  with(nolock) where   UNDERWRITER='59'
   execute (@ret )  
   END 
GO

