IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_ExecuteGridQuery]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_ExecuteGridQuery]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------          
Proc Name      : dbo.Proc_ExecuteGridQuery          
Created by       : Anurag Verma          
Date             : 5/26/2005          
Purpose       : retrieving data from questiontabmaster          
Revison History :          
Used In        : Wolverine          
          
Modified BY : Anurag Verma          
Modified On : 4/7/2005          
Purpose  : Adding query for LOB           
          
Modified BY : Anurag Verma          
Modified On : 4/8/2005          
Purpose  : Adding query for Lookup Description           
          
Modified BY : Amar Singh        
Modified On : 08/05/2006        
Purpose  : Adding query for Party Types        
      
Modified BY : Vijay Arora      
Modified On : 08/05/2006        
Purpose  : Adding query for Catastrophe Events      
      
Modified BY : Anurag Verma          
Modified On : 20/07/2006      
Purpose  : Adding query for Yes/No Lookup Description           
      
Modified BY : Raman Pal Singh      
Modified On : 27/07/2006      
Purpose  : Adding query for doc_type desc      
Modified BY : Pravesh Chandel      
Modified On : 08/11/2006      
Purpose  : Adding query for note_type desc      
      
Modified BY : Anurag Verma    
Modified On : 03/19/2007    
Purpose  : Adding query for module master  

Modified BY : Charles Gomes
Modified On : 25-May-2010    
Purpose     : Multilingual Support

Modified BY : Sonal
Modified On : 18-08-2010
Purpose     : to add new case for billing plan
    
------------------------------------------------------------          
          
Date     Review By          Comments          
------   ------------       -------------------------*/       
--  Proc_ExecuteGridQuery 'partytype','w001',2    
       
--DROP PROC dbo.Proc_ExecuteGridQuery          
create PROC [dbo].[Proc_ExecuteGridQuery]          
(          
@Type NVARCHAR(100),          
@AgencyId NVARCHAR(10) = NULL,
@LANG_ID INT = 1   --Added by Charles on 25-May-2010 for Multilingual Support       
)          
--@PARAM varchar(2000)=null          
          
AS          
if(lower(@Type)='user')          
begin          
	 if @agencyid is null          
	  select user_id,user_fname + ' ' + user_lname name           
	  from mnt_user_list ul WITH(NOLOCK) where is_active='Y'         
	  order by name          
	 else          
	  select user_id,user_fname + ' ' + user_lname name           
	  from mnt_user_list ul  WITH(NOLOCK)      
	  where (ul.user_system_id = @AgencyId or ul.user_system_id is null )  and is_active='Y'       
	  order by name          
end          
if(lower(@Type)='type')          
begin          
	SELECT TLT.TYPEID, ISNULL(TLM.TYPEDESC,TLT.TYPEDESC) AS TYPEDESC 
	FROM TODOLISTTYPES TLT WITH(NOLOCK)
	LEFT OUTER JOIN TODOLISTTYPES_MULTILINGUAL TLM WITH(NOLOCK)
	ON TLM.TYPEID = TLT.TYPEID AND TLM.LANG_ID = @LANG_ID
	ORDER BY ISNULL(TLM.TYPEDESC,TLT.TYPEDESC)         
end          
if(lower(@Type)='lob' or lower(@Type)='product')          
begin          
	SELECT MLM.LOB_ID,ISNULL(MLL.LOB_DESC,MLM.LOB_DESC) AS LOB_DESC 
	FROM MNT_LOB_MASTER MLM WITH(NOLOCK)
	LEFT OUTER JOIN MNT_LOB_MASTER_MULTILINGUAL MLL WITH(NOLOCK)
	ON MLL.LOB_ID = MLM.LOB_ID AND MLL.LANG_ID = @LANG_ID
	WHERE isnull(IS_ACTIVE,'Y')='Y'
	ORDER BY ISNULL(MLL.LOB_DESC,MLM.LOB_DESC)          
end   
if(lower(@Type)='lookupdesc')          
begin          
	select MLT.LOOKUP_ID,ISNULL(MLTM.LOOKUP_DESC , MLT.LOOKUP_DESC) AS LOOKUP_DESC from mnt_lookup_tables MLT WITH(NOLOCK)
	LEFT OUTER JOIN MNT_LOOKUP_TABLES_MULTILINGUAL MLTM WITH(NOLOCK)
	ON MLTM.LANG_ID = @LANG_ID AND MLTM.LOOKUP_ID = MLT.LOOKUP_ID       
end          
if(lower(@Type)='LookUpValueDesc')          
begin          
	SELECT MLV.LOOKUP_UNIQUE_ID,ISNULL(MLM.LOOKUP_VALUE_DESC,MLV.LOOKUP_VALUE_DESC) AS LOOKUP_VALUE_DESC 
	FROM MNT_LOOKUP_VALUES MLV WITH(NOLOCK)  
	LEFT OUTER JOIN MNT_LOOKUP_VALUES_MULTILINGUAL MLM WITH(NOLOCK)  
	ON MLM.LANG_ID = @LANG_ID AND MLM.LOOKUP_UNIQUE_ID = MLV.LOOKUP_UNIQUE_ID      
end          
if(lower(@Type)='partytype')          
begin          
	Select CTD.Detail_Type_ID, ISNULL(CTD_M.TYPE_DESC,CTD.DETAIL_TYPE_DESCRIPTION) DETAIL_TYPE_DESCRIPTION From Clm_Type_Detail CTD WITH(NOLOCK) 
	LEFT OUTER JOIN CLM_TYPE_DETAIL_MULTILINGUAL CTD_M WITH(NOLOCK) ON CTD_M.LANG_ID = @LANG_ID AND CTD_M.DETAIL_TYPE_ID  = CTD.Detail_Type_ID 
	Where Type_ID = 2 AND  is_active='Y' ORDER BY DETAIL_TYPE_DESCRIPTION  -- Party Types      
end          
if(lower(@Type)='ctet')          
begin          
	Select Detail_Type_ID, Detail_Type_Description From Clm_Type_Detail WITH(NOLOCK) Where Type_ID = 1  ORDER BY DETAIL_TYPE_DESCRIPTION-- Catastrophe Event Types      
end      
if(lower(@Type)='YESNO')          
begin          
	select case when MLV.LOOKUP_UNIQUE_ID=10963 then 1      
	when MLV.lookup_unique_id=10964 then 0 end lookup_unique_id,      
	ISNULL(MLM.lookup_value_desc, MLV.lookup_value_desc) AS lookup_value_desc from mnt_lookup_values MLV WITH(NOLOCK)
	LEFT OUTER JOIN MNT_LOOKUP_VALUES_MULTILINGUAL MLM WITH(NOLOCK) 
	ON MLM.LANG_ID = @LANG_ID AND MLM.LOOKUP_UNIQUE_ID = MLV.LOOKUP_UNIQUE_ID
	where MLV.lookup_value_desc='Yes' or MLV.lookup_value_desc='No'          
end      
if(lower(@Type)='doc_type')          
Begin      
	SELECT MLV.LOOKUP_UNIQUE_ID,ISNULL(MLM.LOOKUP_VALUE_DESC , MLV.LOOKUP_VALUE_DESC) AS LOOKUP_VALUE_DESC FROM MNT_LOOKUP_VALUES MLV WITH(NOLOCK)
	LEFT OUTER JOIN MNT_LOOKUP_VALUES_MULTILINGUAL MLM WITH(NOLOCK)
	ON MLM.LANG_ID = @LANG_ID AND MLM.LOOKUP_UNIQUE_ID = MLV.LOOKUP_UNIQUE_ID
	 WHERE MLV.LOOKUP_ID =1275      
End      
--added by pravesh    
if(lower(@Type)='note_type')          
Begin      
	SELECT MLV.LOOKUP_UNIQUE_ID,ISNULL(MLM.LOOKUP_VALUE_DESC,MLV.LOOKUP_VALUE_DESC) AS LOOKUP_VALUE_DESC FROM MNT_LOOKUP_VALUES MLV WITH(NOLOCK)
	LEFT OUTER JOIN MNT_LOOKUP_VALUES_MULTILINGUAL MLM WITH(NOLOCK) 
	ON MLM.LANG_ID = @LANG_ID AND MLM.LOOKUP_UNIQUE_ID = MLV.LOOKUP_UNIQUE_ID
	WHERE MLV.LOOKUP_ID =950  order by ISNULL(MLM.LOOKUP_VALUE_DESC,MLV.LOOKUP_VALUE_DESC)    
End      
--added by Anurag Verma    
if(lower(@Type)='module')          
Begin      
	SELECT mm_module_id,mm_module_name FROM MNT_module_master WITH(NOLOCK) order by mm_module_name    
End 


if(lower(@Type)='MNTLOB') -- added by sonal to add all option in billing plan grid for lobs         
begin          
	 SELECT '0'  LOB_ID,'All' AS LOB_DESC    
	UNION  
	 
   SELECT MLM.LOB_ID,ISNULL(MLL.LOB_DESC,MLM.LOB_DESC) AS LOB_DESC   
   FROM MNT_LOB_MASTER MLM WITH(NOLOCK)  
   LEFT OUTER JOIN MNT_LOB_MASTER_MULTILINGUAL MLL WITH(NOLOCK)  
   ON MLL.LOB_ID = MLM.LOB_ID AND MLL.LANG_ID =@LANG_ID order by LOB_DESC         
end      
  
  
  
  IF(LOWER(@Type)='POLSTATUS')
  BEGIN  
  SELECT ISNULL(PPSMM.POLICY_STATUS_CODE, ISNULL(PPSM.POLICY_STATUS_CODE,'')) AS POLICY_STATUS_CODE,ISNULL(PPSMM.POLICY_DESCRIPTION,ISNULL(PPSM.POLICY_DESCRIPTION,''))
   AS POLICY_STATUS   FROM POL_POLICY_STATUS_MASTER PPSM WITH(NOLOCK) 
  LEFT OUTER JOIN POL_POLICY_STATUS_MASTER_MULTILINGUAL PPSMM WITH(NOLOCK) ON PPSM.POLICY_STATUS_CODE=PPSMM.POLICY_STATUS_CODE AND PPSMM.LANG_ID=@LANG_ID 
    WHERE IS_ACTIVE   = 'Y' AND ISNULL(PPSMM.POLICY_STATUS_CODE, PPSM.POLICY_STATUS_CODE)NOT IN('APPLICATION', 'INCOMPLETE','COMPLETE','REJECT','REFERRED') 
  END
  
   IF(LOWER(@Type)='APPSTATUS')
  BEGIN  
  SELECT ISNULL(PPSMM.POLICY_STATUS_CODE, ISNULL(PPSM.POLICY_STATUS_CODE,'')) AS POLICY_STATUS_CODE,ISNULL(PPSMM.POLICY_DESCRIPTION,ISNULL(PPSM.POLICY_DESCRIPTION,''))
   AS APP_STATUS   FROM POL_POLICY_STATUS_MASTER PPSM WITH(NOLOCK) 
  LEFT OUTER JOIN POL_POLICY_STATUS_MASTER_MULTILINGUAL PPSMM WITH(NOLOCK) ON PPSM.POLICY_STATUS_CODE=PPSMM.POLICY_STATUS_CODE AND PPSMM.LANG_ID=@LANG_ID 
    WHERE IS_ACTIVE   = 'Y' AND ISNULL(PPSMM.POLICY_STATUS_CODE, PPSM.POLICY_STATUS_CODE) 
    IN('REJECT','APPLICATION','REFERRED')
  END
  
  IF(LOWER(@Type)= 'ACTIVITYSTATUS')
  BEGIN
  SELECT MLV3.LOOKUP_UNIQUE_ID, 	
  ISNULL(N4.LOOKUP_VALUE_DESC,MLV3.LOOKUP_VALUE_DESC) AS ACTIVITY_STATUS FROM MNT_LOOKUP_VALUES MLV3
  LEFT JOIN MNT_LOOKUP_VALUES_MULTILINGUAL N4 ON MLV3.LOOKUP_UNIQUE_ID = N4.LOOKUP_UNIQUE_ID AND N4.LANG_ID = @LANG_ID
  WHERE MLV3.LOOKUP_UNIQUE_ID = '11800' OR MLV3.LOOKUP_UNIQUE_ID = '11801'
  END
  

GO

