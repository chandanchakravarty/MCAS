/*----------------------------------------------------------                                                
Proc Name        :   Proc_GetMNT_AGENCY_COUNTRY_LOB_ASSOC                                                 
Created by         : Kuldeep Saxena                                              
Date                :29/11/2011                                              
Purpose           :  Get the table informatoin in the form of xml data                                                
Revison History  :                                                
Used In             :            
Modified By         : Rahul Kr Dwivedi            
Modified On         : 02 Dec 2011            
Purpose             :   to get the products country wise (eg. singapore)          
----------------------------------------------------------                                                
Date     Review By          Comments                        
DROP PROC dbo.Proc_GetMNT_AGENCY_STATE_LOB_ASSOC              
----   ------------       -------------------------*/                                                
alter PROC [dbo].[Proc_GetMNT_AGENCY_COUNTRY_LOB_ASSOC]
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