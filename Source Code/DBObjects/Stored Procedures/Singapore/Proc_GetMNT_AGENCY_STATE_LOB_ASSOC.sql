ALTER PROC [dbo].[Proc_GetMNT_AGENCY_STATE_LOB_ASSOC]  --32834,7                                        
(                                          
@AGENCY_ID int,    
@STATE_ID int    
)                                          
AS                                          
BEGIN          
  
  
if exists(select MNT_AGENCY_STATE_LOB_ASSOC_ID from MNT_AGENCY_STATE_LOB_ASSOC where agency_id=@agency_id and state_id=@state_id)                                  
  SELECT MLM.LOB_ID,MLM.LOB_DESC FROM MNT_LOB_STATE MLS    
   INNER JOIN MNT_LOB_MASTER MLM ON MLS.LOB_ID=MLM.LOB_ID     
   WHERE MLS.PARENT_LOB IS NULL AND STATE_ID=@STATE_ID and   
    MLM.LOB_ID not in  (select lob_id from MNT_AGENCY_STATE_LOB_ASSOC where agency_id=32833 and state_id=7)  
   ORDER BY LOB_DESC    
else  
  SELECT MLM.LOB_ID,MLM.LOB_DESC FROM MNT_LOB_STATE MLS    
   INNER JOIN MNT_LOB_MASTER MLM ON MLS.LOB_ID=MLM.LOB_ID     
   WHERE MLS.PARENT_LOB IS NULL AND STATE_ID=@STATE_ID   
     and   
    MLM.LOB_ID not in  (select lob_id from MNT_AGENCY_STATE_LOB_ASSOC where agency_id=@agency_id and state_id=@state_id)  
  ORDER BY LOB_DESC    
  
  
  
 SELECT       
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
  AGENCY_ID = @agency_id  and    
  STATE_ID = @state_id    
 ORDER BY   
  MLM.LOB_DESC    
END   


  
  
  
  
  