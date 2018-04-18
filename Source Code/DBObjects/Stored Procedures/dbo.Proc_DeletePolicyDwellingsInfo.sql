IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_DeletePolicyDwellingsInfo]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_DeletePolicyDwellingsInfo]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------          
Proc Name    : dbo.Proc_DeletePolicyDwellingsInfo          
Created by   : Anurag Verma 
Date         : Nov 11, 2005      
Purpose     : Deletes a record from POL_DWELLINGS_INFO      
Revison History :      
Used In  :   Wolverine             
------   ------------       -------------------------*/          
CREATE  PROC dbo.Proc_DeletePolicyDwellingsInfo      
(      
 @CUSTOMER_ID Int,      
 @POL_ID Int,      
 @POL_VERSION_ID Int,      
 @DWELLING_ID smallint      
       
       
)      
      
AS      
      
      
      
BEGIN         
       
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
      
  DELETE FROM Pol_HOME_RATING_INFO      
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
      
 --Delete from APP_DWELLING_SECTION_COVERAGES  
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

