IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetClaimsPinkSlipLookup]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetClaimsPinkSlipLookup]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                                                
Proc Name       : dbo.Proc_GetClaimsPinkSlipLookup                                          
Created by      : Sumit Chhabra                                              
Date            : 04/04/2006                                                
Purpose         : Get Lookup Data for Pink Slip User and Pink Slip Types    
Created by      : Sumit Chhabra                                               
Revison History :                                                
Used In        : Wolverine                                                
------------------------------------------------------------                                                
Date     Review By          Comments                                                
------   ------------       -------------------------*/                                                
--drop PROC dbo.Proc_GetClaimsPinkSlipLookup  682                        
CREATE PROC [dbo].[Proc_GetClaimsPinkSlipLookup]                          
@CLAIM_ID int = null    
AS                                                
BEGIN                                                 
declare @STATEMENT varchar(5000)    
declare @PINK_SLIP varchar(100)    
    
    
--==============================================================================
-- MODIFIED BY SANTOSH KUMAR GAUTAM ON 25 MARCH 2011 FOR ITRACK: 1035
-- HERE I AM CHANGING THE WHERE CLAUSE NO THIS PROC DOES NOT RETURN ANY DATA    
--==============================================================================


--Select users who have chosen pink slip types    
if @CLAIM_ID IS NOT NULL AND @CLAIM_ID<>0--When claim has been added, fetch selected users at the top    
BEGIN    
SELECT @PINK_SLIP=ISNULL(RECIEVE_PINK_SLIP_USERS_LIST,0) FROM CLM_CLAIM_INFO WITH (NOLOCK) WHERE CLAIM_ID=@CLAIM_ID    
set @STATEMENT='select USER_ID,(ISNULL(USER_FNAME,'''') + '' '' +  ISNULL(USER_LNAME,'''') + '' ( '' + ISNULL(USER_LOGIN_ID,'''') + '' )'') AS USER_NAME,1 as tab_num From mnt_user_list  WITH (NOLOCK) where USER_SYSTEM_ID=''@@@@''  AND IS_ACTIVE = ''Y''   
AND USER_ID in (' + @PINK_SLIP     
+ ')    
    union    
    select USER_ID,(ISNULL(USER_FNAME,'''') + '' '' +  ISNULL(USER_LNAME,'''') + '' ( '' + ISNULL(USER_LOGIN_ID,'''') + '' )'') AS USER_NAME,2 as tab_num From mnt_user_list  WITH (NOLOCK) where  USER_SYSTEM_ID=''@@@@''  AND IS_ACTIVE = ''Y'' 
    AND PINK_SLIP_NOTIFY=''Y'' and USER_ID not    
 in (' + @PINK_SLIP + '    
    
    
)    
    order by USER_NAME '     
    PRINT @STATEMENT  
exec (@STATEMENT)    
END    
ELSE    
 select USER_ID,(ISNULL(USER_FNAME,'') + ' ' + ISNULL(USER_LNAME,'') + ' ( ' + ISNULL(USER_LOGIN_ID,'') + ' )') AS USER_NAME From mnt_user_list  WITH (NOLOCK) where  USER_SYSTEM_ID='@@@@' AND IS_ACTIVE = 'Y' AND  isnull(PINK_SLIP_NOTIFY,'N')='Y' ORDER BY 
  
    
USER_NAME                      
             
    
    
if @CLAIM_ID IS NOT NULL AND @CLAIM_ID<>0--When claim has been added, fetch selected users at the top    
BEGIN    
SELECT @PINK_SLIP=ISNULL(PINK_SLIP_TYPE_LIST,0) FROM CLM_CLAIM_INFO WITH (NOLOCK) WHERE CLAIM_ID=@CLAIM_ID    
set @STATEMENT='select v.lookup_unique_id,v.lookup_value_desc,v.lookup_value_code,1 as tab_num from mnt_lookup_values v join     
    mnt_lookup_tables t on v.lookup_id = t.lookup_id where t.lookup_name = ''PSTYP'' and v.lookup_unique_id in (' + @PINK_SLIP + ') AND v.IS_ACTIVE=''Y''    
    union    
    select v.lookup_unique_id,v.lookup_value_desc,v.lookup_value_code,2 as tab_num from mnt_lookup_values v join     
    mnt_lookup_tables t on v.lookup_id = t.lookup_id where t.lookup_name = ''PSTYP'' and v.lookup_unique_id not in (' + @PINK_SLIP + ') AND v.IS_ACTIVE=''Y''    
    order by lookup_value_desc '     
print @STATEMENT    
exec (@STATEMENT)    
END    
ELSE    
  select v.lookup_unique_id,v.lookup_value_desc,v.lookup_value_code 
  from  mnt_lookup_values v join     
		mnt_lookup_tables t on v.lookup_id = t.lookup_id
  where t.lookup_name = 'PSTYP' AND V.IS_ACTIVE='Y'
  order by v.lookup_value_desc    
--Select pink slip types    
--exec Proc_GetLookupValues 'PSTYP',null,1                     
END 
GO

