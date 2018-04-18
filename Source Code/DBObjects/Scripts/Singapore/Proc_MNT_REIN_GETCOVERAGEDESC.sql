 --drop  PROCEDURE dbo.Proc_MNT_REIN_GETCOVERAGEDESC           
alter PROCEDURE dbo.Proc_MNT_REIN_GETCOVERAGEDESC      
(             
 @STATE_ID nvarchar(10),            
 @LOB_ID nvarchar(10)           
             
)            
AS            
BEGIN            
 SELECT    COV_CODE, COV_DES ,COV_ID     
 FROM MNT_COVERAGE      
 WHERE STATE_ID=@STATE_ID 
 --and LOB_ID=@LOB_ID and REINSURANCE_LOB=10963   
    
 UNION     
    
 SELECT    COV_CODE, COV_DES ,COV_ID     
 FROM MNT_REINSURANCE_COVERAGE      
 WHERE STATE_ID=@STATE_ID 
 --and LOB_ID=@LOB_ID and REINSURANCE_LOB=10963 ORDER BY COV_DES    
END    
    
    
    
  
  