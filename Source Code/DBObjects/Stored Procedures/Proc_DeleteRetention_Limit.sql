    
--Created By Naveen Puajri  
--Modify date : oct 14 , 2011    
--DROP PROC Proc_DeleteRetention_Limit      
      
Create PROC [dbo].[Proc_DeleteRetention_Limit]    
(        
 @RETENTION_LIMIT_ID int  
)         
 AS      
 BEGIN      
  DELETE FROM MNT_RETENTION_LIMIT 
  WHERE RETENTION_LIMIT_ID=@RETENTION_LIMIT_ID  
   
 END 