ALTER PROC [dbo].[Proc_GetMNT_AGENCY_COUNTRY_LOB_ASSOC]  --32834,7
(                                                  
@AGENCY_ID int,            
@COUNTRY_ID int      
)                                                  
AS         
                                              
BEGIN             
    
      
          
if exists(select MNT_AGENCY_STATE_LOB_ASSOC_ID from MNT_AGENCY_STATE_LOB_ASSOC where agency_id=@AGENCY_ID and state_id in(select STATE_ID from MNT_COUNTRY_STATE_LIST where COUNTRY_ID=@COUNTRY_ID))                                          
  SELECT distinct  MLM.LOB_ID,MLM.LOB_DESC FROM MNT_LOB_STATE MLS            
   INNER JOIN MNT_LOB_MASTER MLM ON MLS.LOB_ID=MLM.LOB_ID             
   WHERE MLS.PARENT_LOB IS NULL AND STATE_ID in(select STATE_ID from MNT_COUNTRY_STATE_LIST where COUNTRY_ID=@COUNTRY_ID) and           
    MLM.LOB_ID not in  (select lob_id from MNT_AGENCY_STATE_LOB_ASSOC where agency_id=@AGENCY_ID and state_id in(select STATE_ID from MNT_COUNTRY_STATE_LIST where COUNTRY_ID=@COUNTRY_ID) )          
   ORDER BY LOB_DESC            
else          
  SELECT  distinct MLM.LOB_ID,MLM.LOB_DESC FROM MNT_LOB_STATE MLS            
   INNER JOIN MNT_LOB_MASTER MLM ON MLS.LOB_ID=MLM.LOB_ID             
   WHERE MLS.PARENT_LOB IS NULL AND STATE_ID in(select STATE_ID from MNT_COUNTRY_STATE_LIST where COUNTRY_ID=@COUNTRY_ID)          
     and           
    MLM.LOB_ID not in  (select lob_id from MNT_AGENCY_STATE_LOB_ASSOC where agency_id=@agency_id and state_id in(select STATE_ID from MNT_COUNTRY_STATE_LIST where COUNTRY_ID=@COUNTRY_ID))          
  ORDER BY LOB_DESC            
          
          
          
 SELECT   distinct             
  MNT_AGENCY_STATE_LOB_ASSOC_ID,              
  AGENCY_ID,              
  STATE_ID,              
  MLM.LOB_DESC AS LOB_DESC,          
  MASL.LOB_ID,              
  MASL.CREATED_DATETIME,              
  MASL.MODIFIED_BY,              
  MASL.LAST_UPDATED_DATETIME,              
  MASL.IS_ACTIVE              
 FROM              
  MNT_AGENCY_STATE_LOB_ASSOC MASL           
 LEFT OUTER JOIN              
 MNT_LOB_MASTER MLM          
 ON          
 MASL.LOB_ID = MLM.LOB_ID          
 WHERE              
  AGENCY_ID = @AGENCY_ID  and            
  STATE_ID in(select STATE_ID from MNT_COUNTRY_STATE_LIST where COUNTRY_ID=@COUNTRY_ID)           
 ORDER BY           
  MLM.LOB_DESC            
END 

--SELECT * FROM MNT_AGENCY_STATE_LOB_ASSOC WHERE STATE_ID = 7 ORDER BY LOB_ID 

--DELETE FROM MNT_AGENCY_STATE_LOB_ASSOC
--UPDATE MNT_AGENCY_STATE_LOB_ASSOC SET STATE_ID = 92 WHERE STATE_ID = 7

--select * into MNT_AGENCY_STATE_LOB_ASSOC_13012012 from MNT_AGENCY_STATE_LOB_ASSOC



--select * from MNT_AGENCY_STATE_LOB_ASSOC

