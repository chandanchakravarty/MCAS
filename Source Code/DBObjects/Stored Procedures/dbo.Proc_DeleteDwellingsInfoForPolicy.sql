IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_DeleteDwellingsInfoForPolicy]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_DeleteDwellingsInfoForPolicy]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------              
Proc Name    : dbo.Proc_DeleteDwellingsInfo              
Created by   : shafi              
Date         : 17 feb 2006        
Purpose     : Deletes a record from POL_DWELLINGS_INFO          
Revison History :          
Used In  :   Wolverine                 
 ------------------------------------------------------------                          
Date      Review By           Comments                        
15/10/2005      Sumit Chhabra   Query for deleting record for table POL_SQR_FOOT_IMPROVEMENTS,POL_PROTECT_DEVICES and POL_HOME_CONSTRUCTION_INFO are deleted as the tables itself have been deleted.        
3 Jul 2006  RPSINGH  Deleete from POL_OTHER_STRUCTURE_DWELLING table  
------   ------------       -------------------------*/        
CREATE  PROC dbo.Proc_DeleteDwellingsInfoForPolicy         
(          
 @CUSTOMER_ID Int,          
 @POL_ID Int,          
 @POL_VERSION_ID Int,          
 @DWELLING_ID smallint          
)          
AS         
          
BEGIN        

DECLARE @AGENCY_BILL_MORTAGAGEE SMALLINT,@INSURED_BILL_MORTAGAGEE SMALLINT
SET @AGENCY_BILL_MORTAGAGEE = 11277
SET @INSURED_BILL_MORTAGAGEE = 11278
     
           
 --Delete from Home rating          
 IF EXISTS          
 (          
  SELECT * FROM POL_HOME_RATING_INFO          
  WHERE  CUSTOMER_ID = @CUSTOMER_ID AND          
   POLICY_ID = @POL_ID AND          
   POLICY_VERSION_ID = @POL_VERSION_ID AND          
   DWELLING_ID = @DWELLING_ID          
             
 )          
 BEGIN          
          
  DELETE FROM POL_HOME_RATING_INFO          
  WHERE  CUSTOMER_ID = @CUSTOMER_ID AND          
   POLICY_ID = @POL_ID AND          
   POLICY_VERSION_ID = @POL_VERSION_ID AND          
   DWELLING_ID = @DWELLING_ID           
 END               
           
           
 --Delete from Additional interest          
 IF EXISTS          
 (          
  SELECT * FROM POL_HOME_OWNER_ADD_INT          
  WHERE  CUSTOMER_ID = @CUSTOMER_ID AND          
   POLICY_ID = @POL_ID AND          
   POLICY_VERSION_ID = @POL_VERSION_ID AND          
   DWELLING_ID = @DWELLING_ID          
             
 )          
 BEGIN          
          
  DELETE FROM POL_HOME_OWNER_ADD_INT          
  WHERE  CUSTOMER_ID = @CUSTOMER_ID AND          
   POLICY_ID = @POL_ID AND          
   POLICY_VERSION_ID = @POL_VERSION_ID AND          
   DWELLING_ID = @DWELLING_ID           
 END           
  
  
 --Added by RP  
 --Delete from POL_OTHER_STRUCTURE_DWELLING  
    
IF EXISTS          
 (          
  SELECT * FROM POL_OTHER_STRUCTURE_DWELLING          
  WHERE  CUSTOMER_ID = @CUSTOMER_ID AND          
   POLICY_ID = @POL_ID AND          
   POLICY_VERSION_ID = @POL_VERSION_ID AND          
   DWELLING_ID = @DWELLING_ID          
             
 )          
 BEGIN          
          
  DELETE FROM POL_OTHER_STRUCTURE_DWELLING          
  WHERE  CUSTOMER_ID = @CUSTOMER_ID AND          
   POLICY_ID = @POL_ID AND          
   POLICY_VERSION_ID = @POL_VERSION_ID AND          
   DWELLING_ID = @DWELLING_ID           
 END        
 --end of additoin by RP  
  
          
 --Delete from Coverages          
 IF EXISTS          
 (          
  SELECT * FROM POL_DWELLING_COVERAGE          
  WHERE  CUSTOMER_ID = @CUSTOMER_ID AND          
   POLICY_ID = @POL_ID AND          
   POLICY_VERSION_ID = @POL_VERSION_ID AND          
   DWELLING_ID = @DWELLING_ID          
             
 )          
 BEGIN          
          
  DELETE FROM POL_DWELLING_COVERAGE          
  WHERE  CUSTOMER_ID = @CUSTOMER_ID AND          
   POLICY_ID = @POL_ID AND          
   POLICY_VERSION_ID = @POL_VERSION_ID AND          
   DWELLING_ID = @DWELLING_ID           
 END           
      
      
 --Delete from Endorsements      
 IF EXISTS          
 (          
  SELECT * FROM POL_DWELLING_ENDORSEMENTS          
  WHERE  CUSTOMER_ID = @CUSTOMER_ID AND          
   POLICY_ID = @POL_ID AND          
   POLICY_VERSION_ID = @POL_VERSION_ID AND          
   DWELLING_ID = @DWELLING_ID          
             
 )          
 BEGIN          
          
  DELETE FROM POL_DWELLING_ENDORSEMENTS          
  WHERE  CUSTOMER_ID = @CUSTOMER_ID AND          
   POLICY_ID = @POL_ID AND          
   POLICY_VERSION_ID = @POL_VERSION_ID AND          
   DWELLING_ID = @DWELLING_ID           
 END           
          
 --Delete from POL_DWELLING_SECTION_COVERAGES      
 IF EXISTS          
 (          
  SELECT * FROM POL_DWELLING_SECTION_COVERAGES          
  WHERE  CUSTOMER_ID = @CUSTOMER_ID AND          
   POLICY_ID = @POL_ID AND          
   POLICY_VERSION_ID = @POL_VERSION_ID AND          
   DWELLING_ID = @DWELLING_ID          
             
 )          
 BEGIN          
          
  DELETE FROM POL_DWELLING_SECTION_COVERAGES          
  WHERE  CUSTOMER_ID = @CUSTOMER_ID AND          
   POLICY_ID = @POL_ID AND   
   POLICY_VERSION_ID = @POL_VERSION_ID AND   
   DWELLING_ID = @DWELLING_ID           
 END           
       
  
--Delete from POL_OTHER_LOCATIONS      
 IF EXISTS          
 (          
  SELECT * FROM POL_OTHER_LOCATIONS          
  WHERE  CUSTOMER_ID = @CUSTOMER_ID AND          
   POLICY_ID = @POL_ID AND          
   POLICY_VERSION_ID = @POL_VERSION_ID AND          
   DWELLING_ID = @DWELLING_ID          
             
 )          
 BEGIN          
          
  DELETE FROM POL_OTHER_LOCATIONS          
  WHERE  CUSTOMER_ID = @CUSTOMER_ID AND          
   POLICY_ID = @POL_ID AND          
   POLICY_VERSION_ID = @POL_VERSION_ID AND   
   DWELLING_ID = @DWELLING_ID           
 END   


--When the record is being deleted and the current record is the selected mortagagee for the application,    
--set the mortagagee value to 0 for the application    
IF exists(SELECT CUSTOMER_ID FROM POL_CUSTOMER_POLICY_LIST WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POL_ID     
  AND POLICY_VERSION_ID=@POL_VERSION_ID     
  AND DWELLING_ID=@DWELLING_ID     
  AND ISNULL(IS_ACTIVE,'N')='Y'    
  AND BILL_TYPE_ID IN (@AGENCY_BILL_MORTAGAGEE,@INSURED_BILL_MORTAGAGEE))    
 UPDATE POL_CUSTOMER_POLICY_LIST SET DWELLING_ID = 0,ADD_INT_ID = 0 WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POL_ID AND               
    POLICY_VERSION_ID=@POL_VERSION_ID  
         
 DELETE FROM POL_DWELLINGS_INFO          
 WHERE  CUSTOMER_ID = @CUSTOMER_ID AND          
  POLICY_ID = @POL_ID AND          
  POLICY_VERSION_ID = @POL_VERSION_ID AND          
  DWELLING_ID = @DWELLING_ID          
           
 IF @@ERROR <> 0          
 BEGIN          
  RETURN -1          
 END          
          
 RETURN 1          
END          
    
  
  



GO

