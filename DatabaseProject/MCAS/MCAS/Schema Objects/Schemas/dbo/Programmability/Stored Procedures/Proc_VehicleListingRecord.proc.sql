CREATE PROCEDURE [dbo].[Proc_VehicleListingRecord]
WITH EXECUTE AS CALLER
AS
BEGIN                              
            
         
     BEGIN        
      -- SELECT RECORDS                         
          select UploadFileName, UploadPath from MNT_VehicleListingUpload     
 where Status='INCOMPLETE' and IS_ACTIVE='Y' and Is_processed='N' order by UploadedDate    
                
     END                                  
 END


