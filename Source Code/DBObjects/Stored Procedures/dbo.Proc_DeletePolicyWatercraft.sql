IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_DeletePolicyWatercraft]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_DeletePolicyWatercraft]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

 /***********************************MODIFICATION HISTORY************************************  
Modified By : RPSINGH  
Date     : 25 May 2006    
Purpose   : Equipment will be no longer attach to a boat.   
    So for deletion of boat no need to check for associated equipment  
***********************************************************************/  
-- DROP PROC dbo.Proc_DeletePolicyWatercraft                
CREATE  PROC dbo.Proc_DeletePolicyWatercraft                
(                
    @CUSTOMER_ID int,              
    @POLICY_ID int,              
    @POLICY_VERSION_ID smallint,                
    @BOAT_ID INT,    
    @RESULT INT = NULL OUTPUT               
)                
AS                
BEGIN               
    
-- IF EXISTS THEN RETURN -2  ELSE DELETE THE RECORD  // SWASTIKA    
 IF EXISTS( SELECT  ASSOCIATED_BOAT FROM POL_WATERCRAFT_TRAILER_INFO  WHERE         
 CUSTOMER_ID = @CUSTOMER_ID AND  POLICY_ID =  @POLICY_ID AND POLICY_VERSION_ID =  @POLICY_VERSION_ID AND         
 ASSOCIATED_BOAT= @BOAT_ID AND ISNULL(IS_ACTIVE,'N') = 'Y') --IS_ACTIVE added by Charles on 17-Nov-09 for Itrack 6600   
     
 BEGIN     
  SET @RESULT=-2    
  RETURN @RESULT    
 END     
 ELSE    
BEGIN     
          
 DELETE FROM  POL_WATERCRAFT_COV_ADD_INT              
 WHERE               
      CUSTOMER_ID = @CUSTOMER_ID AND              
      POLICY_ID =  @POLICY_ID AND              
      POLICY_VERSION_ID =  @POLICY_VERSION_ID AND               
      BOAT_ID = @BOAT_ID            
          
 DELETE FROM   POL_WATERCRAFT_COVERAGE_INFO        
 WHERE               
      CUSTOMER_ID = @CUSTOMER_ID AND              
      POLICY_ID =  @POLICY_ID AND              
      POLICY_VERSION_ID =  @POLICY_VERSION_ID AND               
      BOAT_ID = @BOAT_ID            
        
--Endorsements      
DELETE FROM   POL_WATERCRAFT_ENDORSEMENTS        
 WHERE               
      CUSTOMER_ID = @CUSTOMER_ID AND              
      POLICY_ID =  @POLICY_ID AND              
      POLICY_VERSION_ID =  @POLICY_VERSION_ID AND               
      BOAT_ID = @BOAT_ID            
       
      
DELETE FROM  POL_WATERCRAFT_ENGINE_INFO              
 WHERE               
      CUSTOMER_ID = @CUSTOMER_ID AND              
      POLICY_ID =  @POLICY_ID AND              
      POLICY_VERSION_ID =  @POLICY_VERSION_ID AND               
      ASSOCIATED_BOAT  = @BOAT_ID            
          
 DELETE FROM  POL_WATERCRAFT_INFO              
 WHERE               
      CUSTOMER_ID = @CUSTOMER_ID AND              
      POLICY_ID =  @POLICY_ID AND              
      POLICY_VERSION_ID =  @POLICY_VERSION_ID AND               
      BOAT_ID = @BOAT_ID          
    
--Assign Boats  
 DELETE FROM POL_OPERATOR_ASSIGNED_BOAT    
 WHERE  
 CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID =  @POLICY_ID AND   
 POLICY_VERSION_ID = @POLICY_VERSION_ID   
 AND BOAT_ID = @BOAT_ID            
  
   
--UPDATE TABLES WHERE THIS VEHICLE IS ASSIGNED    
 UPDATE POL_WATERCRAFT_DRIVER_DETAILS            
 SET VEHICLE_ID=NULL          
 WHERE           
   CUSTOMER_ID = @CUSTOMER_ID AND              
      POLICY_ID =  @POLICY_ID AND              
      POLICY_VERSION_ID =  @POLICY_VERSION_ID AND               
    VEHICLE_ID = @BOAT_ID          
       
 UPDATE POL_WATERCRAFT_TRAILER_INFO        
 SET ASSOCIATED_BOAT=NULL          
 WHERE           
    CUSTOMER_ID = @CUSTOMER_ID AND              
      POLICY_ID =  @POLICY_ID AND              
      POLICY_VERSION_ID =  @POLICY_VERSION_ID AND               
    ASSOCIATED_BOAT= @BOAT_ID         
       
 UPDATE POL_WATERCRAFT_EQUIP_DETAILLS        
 SET  ASSOCIATED_BOAT=NULL        
 WHERE        
    CUSTOMER_ID = @CUSTOMER_ID AND              
      POLICY_ID =  @POLICY_ID AND              
      POLICY_VERSION_ID =  @POLICY_VERSION_ID AND               
    ASSOCIATED_BOAT= @BOAT_ID     
  
--DELETE THE HO-865 ENDORSMENT WHEN THE NO.OF WATERCRAFT IS 0  
DECLARE @BOATCOUNT INT  
SELECT @BOATCOUNT=COUNT(CUSTOMER_ID) FROM POL_WATERCRAFT_INFO              
 WHERE               
      CUSTOMER_ID = @CUSTOMER_ID AND              
      POLICY_ID =  @POLICY_ID AND              
      POLICY_VERSION_ID =  @POLICY_VERSION_ID          
     
IF @BOATCOUNT=0  
  BEGIN  
    DELETE FROM  POL_DWELLING_ENDORSEMENTS WHERE ENDORSEMENT_ID IN (294,295) AND  
      CUSTOMER_ID = @CUSTOMER_ID AND              
      POLICY_ID =  @POLICY_ID AND              
      POLICY_VERSION_ID =  @POLICY_VERSION_ID   
  
-- Added by Asfa (23-Jan-2008) - iTrack issue #3473  
    DELETE from POL_WATERCRAFT_GEN_INFO     
    WHERE               
      CUSTOMER_ID = @CUSTOMER_ID AND              
      POLICY_ID =  @POLICY_ID AND              
      POLICY_VERSION_ID =  @POLICY_VERSION_ID  
     
  END       
              
              
       
 SET @RESULT=1    
 RETURN @RESULT    
 END         
END           
    
    
    
    
    
    
    
    
  
  
  
  
  
  
  
GO

