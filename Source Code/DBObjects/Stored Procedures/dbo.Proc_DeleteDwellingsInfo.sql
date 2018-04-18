IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_DeleteDwellingsInfo]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_DeleteDwellingsInfo]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- 
/*----------------------------------------------------------                
Proc Name    : dbo.Proc_DeleteDwellingsInfo                
Created by   : Pradeep Iyer                
Date         : May 16, 2005            
Purpose     : Deletes a record from APP_DWELLINGS_INFO            
Revison History :            
Used In  :   Wolverine                   
 ------------------------------------------------------------                            
Date      Review By           Comments                          
15/10/2005      Sumit Chhabra  Query for deleting record for table APP_SQR_FOOT_IMPROVEMENTS,APP_PROTECT_DEVICES and APP_HOME_CONSTRUCTION_INFO are deleted as the tables itself have been deleted.          
------   ------------       -------------------------*/         
-- drop proc dbo.Proc_DeleteDwellingsInfo                   
CREATE  PROC dbo.Proc_DeleteDwellingsInfo            
(            
 @CUSTOMER_ID Int,            
 @APP_ID Int,            
 @APP_VERSION_ID Int,            
 @DWELLING_ID smallint            
             
             
)            
            
AS            
BEGIN        

 DECLARE @AGENCY_BILL_MORTAGAGEE SMALLINT,@INSURED_BILL_MORTAGAGEE SMALLINT,@MORTAGAGEE_INCEPTION SMALLINT  
 SET @AGENCY_BILL_MORTAGAGEE = 11277  
 SET @INSURED_BILL_MORTAGAGEE = 11278
 SET @MORTAGAGEE_INCEPTION = 11276         
             
 --Delete from Home rating            
            
  
  DELETE FROM APP_OTHER_STRUCTURE_DWELLING            
  WHERE  CUSTOMER_ID = @CUSTOMER_ID AND            
   APP_ID = @APP_ID AND            
   APP_VERSION_ID = @APP_VERSION_ID AND            
   DWELLING_ID = @DWELLING_ID    
  
            
  DELETE FROM APP_HOME_RATING_INFO            
  WHERE  CUSTOMER_ID = @CUSTOMER_ID AND            
   APP_ID = @APP_ID AND            
   APP_VERSION_ID = @APP_VERSION_ID AND            
   DWELLING_ID = @DWELLING_ID      
    
    
--When the record is being deleted and the current record is the selected mortagagee for the application,  
--set the mortagagee value to 0 for the application  
IF exists(SELECT CUSTOMER_ID FROM APP_LIST WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID   
  AND APP_VERSION_ID=@APP_VERSION_ID   
  AND DWELLING_ID=@DWELLING_ID   
  AND ISNULL(IS_ACTIVE,'N')='Y'  
  AND BILL_TYPE_ID IN (@AGENCY_BILL_MORTAGAGEE,@INSURED_BILL_MORTAGAGEE,@MORTAGAGEE_INCEPTION))  
 UPDATE APP_LIST SET DWELLING_ID = 0,ADD_INT_ID = 0 WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND             
    APP_VERSION_ID=@APP_VERSION_ID 
 
  DELETE FROM APP_HOME_OWNER_ADD_INT            
  WHERE  CUSTOMER_ID = @CUSTOMER_ID AND            
   APP_ID = @APP_ID AND            
   APP_VERSION_ID = @APP_VERSION_ID AND            
   DWELLING_ID = @DWELLING_ID             
    
   DELETE FROM APP_OTHER_LOCATIONS           
   WHERE  CUSTOMER_ID = @CUSTOMER_ID AND            
   APP_ID = @APP_ID AND            
   APP_VERSION_ID = @APP_VERSION_ID AND            
   DWELLING_ID = @DWELLING_ID                
            
            
  DELETE FROM APP_DWELLING_COVERAGE            
  WHERE  CUSTOMER_ID = @CUSTOMER_ID AND            
   APP_ID = @APP_ID AND            
   APP_VERSION_ID = @APP_VERSION_ID AND            
   DWELLING_ID = @DWELLING_ID             
          
            
  DELETE FROM APP_DWELLING_ENDORSEMENTS            
  WHERE  CUSTOMER_ID = @CUSTOMER_ID AND            
   APP_ID = @APP_ID AND            
   APP_VERSION_ID = @APP_VERSION_ID AND            
   DWELLING_ID = @DWELLING_ID             
           
          
            
  DELETE FROM APP_DWELLING_SECTION_COVERAGES            
  WHERE  CUSTOMER_ID = @CUSTOMER_ID AND            
   APP_ID = @APP_ID AND            
   APP_VERSION_ID = @APP_VERSION_ID AND            
   DWELLING_ID = @DWELLING_ID             
    
         
           
 DELETE FROM APP_DWELLINGS_INFO            
 WHERE  CUSTOMER_ID = @CUSTOMER_ID AND            
  APP_ID = @APP_ID AND            
  APP_VERSION_ID = @APP_VERSION_ID AND 
  DWELLING_ID = @DWELLING_ID            
             
 IF @@ERROR <> 0            
 BEGIN            
  RETURN -1            
 END            
            
 RETURN 1            
END            
      
  
  
  





GO

