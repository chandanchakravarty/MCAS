IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_DeleteWatercraft]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_DeleteWatercraft]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

 /****** Object:  Stored Procedure dbo.Proc_DeleteWatercraft    Script Date: 5/25/2006 11:43:45 AM ******/  
/*----------------------------------------------------------            
Proc Name       : dbo.Proc_DeleteWatercraft            
Created by      : Priya            
Date            : 11 Nov,2005            
Purpose         : To delete record from APP_UMBRELLA_WATERCRAFT_INFO Table            
Revison History :            
Used In         :   wolverine            
------------------------------------------------------------            
Modified By : Ashwani    
Date     : 13 Feb. 2006    
Purpose   : To check the boat_id is being used in APP_WATERCRAFT_EQUIP_DETAILLS or APP_WATERCRAFT_TRAILER_INFO,    
        @RESULT int output param is added             
  
Modified By : RPSINGH  
Date     : 25 May 2006    
Purpose   : Equipment will be no longer attach to a boat.   
    So for deletion of boat no need to check for associated equipment  
  
Modified By : PKASANA  
Date     : 13 2ep 2007    
Purpose   : Assigned boats will be no longer attach to a boat.   
  
  
------------------------------------------------------------            
Date     Review By          Comments            
------   ------------       -------------------------*/            
--drop proc dbo.Proc_DeleteWatercraft            
CREATE   PROC dbo.Proc_DeleteWatercraft            
(            
    @CUSTOMER_ID INT,          
    @APP_ID INT,          
    @APP_VERSION_ID SMALLINT,            
    @BOAT_ID INT ,    
    @RESULT  INT  =NULL OUTPUT            
)            
AS            
BEGIN            
    
 -- IF EXISTS THEN RETURN -2  ELSE DELETE THE RECORD    
 IF EXISTS( SELECT  ASSOCIATED_BOAT FROM APP_WATERCRAFT_TRAILER_INFO  WHERE         
 CUSTOMER_ID = @CUSTOMER_ID AND  APP_ID =  @APP_ID AND APP_VERSION_ID =  @APP_VERSION_ID AND         
 ASSOCIATED_BOAT= @BOAT_ID AND ISNULL(IS_ACTIVE,'N') = 'Y')   --IS_ACTIVE added by Charles on 17-Nov-09 for Itrack 6600 
     
 BEGIN     
  SET @RESULT=-2    
  RETURN @RESULT    
 END     
 ELSE    
 BEGIN     
    
 DELETE FROM APP_WATERCRAFT_ENDORSEMENTS        
 WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID AND  BOAT_ID=@BOAT_ID        
     
     
 DELETE FROM APP_WATERCRAFT_COV_ADD_INT      
 WHERE  CUSTOMER_ID=@CUSTOMER_ID AND  APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID AND BOAT_ID=@BOAT_ID      
     
 DELETE FROM APP_WATERCRAFT_COVERAGE_INFO      
 WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID AND BOAT_ID=@BOAT_ID      
     
 DELETE FROM APP_WATERCRAFT_ENGINE_INFO      
 WHERE  CUSTOMER_ID=@CUSTOMER_ID AND  APP_ID=@APP_ID AND  APP_VERSION_ID=@APP_VERSION_ID AND ASSOCIATED_BOAT=@BOAT_ID      
     
 DELETE FROM  APP_WATERCRAFT_INFO        
 WHERE  CUSTOMER_ID = @CUSTOMER_ID AND APP_ID =  @APP_ID AND APP_VERSION_ID = @APP_VERSION_ID AND BOAT_ID = @BOAT_ID          
  
 DELETE FROM APP_OPERATOR_ASSIGNED_BOAT  
 WHERE  CUSTOMER_ID = @CUSTOMER_ID AND APP_ID =  @APP_ID AND APP_VERSION_ID = @APP_VERSION_ID AND BOAT_ID = @BOAT_ID          
  
  
 -- UPDATE APP_UMBRELLA_OPERATOR_INFO        
 UPDATE APP_WATERCRAFT_DRIVER_DETAILS          
 SET VEHICLE_ID=NULL        
 WHERE         
 CUSTOMER_ID = @CUSTOMER_ID AND        
 APP_ID =  @APP_ID AND        
 APP_VERSION_ID =  @APP_VERSION_ID AND         
 VEHICLE_ID = @BOAT_ID        
     
 UPDATE APP_WATERCRAFT_TRAILER_INFO      
 SET ASSOCIATED_BOAT=NULL        
 WHERE         
 CUSTOMER_ID = @CUSTOMER_ID AND        
 APP_ID =  @APP_ID AND        
 APP_VERSION_ID =  @APP_VERSION_ID AND         
 ASSOCIATED_BOAT= @BOAT_ID       
     
 UPDATE APP_WATERCRAFT_EQUIP_DETAILLS      
 SET  ASSOCIATED_BOAT=NULL      
 WHERE      
 CUSTOMER_ID = @CUSTOMER_ID AND        
 APP_ID =  @APP_ID AND        
 APP_VERSION_ID =  @APP_VERSION_ID AND         
 ASSOCIATED_BOAT= @BOAT_ID   
  
--DELETE THE HO-865 ENDORSMENT WHEN THE NO.OF WATERCRAFT IS 0  
DECLARE @BOATCOUNT INT  
SELECT @BOATCOUNT=COUNT(CUSTOMER_ID) FROM APP_WATERCRAFT_INFO        
 WHERE  CUSTOMER_ID = @CUSTOMER_ID AND APP_ID =  @APP_ID AND APP_VERSION_ID = @APP_VERSION_ID  
IF @BOATCOUNT=0  
  BEGIN  
    DELETE FROM  APP_DWELLING_ENDORSEMENTS WHERE ENDORSEMENT_ID IN (294,295) AND   
            CUSTOMER_ID = @CUSTOMER_ID AND        
            APP_ID =  @APP_ID AND        
            APP_VERSION_ID =  @APP_VERSION_ID  
  
-- Added by Asfa (23-Jan-2008) - iTrack issue #3473  
    DELETE from APP_WATERCRAFT_GEN_INFO WHERE               
      CUSTOMER_ID = @CUSTOMER_ID AND        
      APP_ID =  @APP_ID AND        
      APP_VERSION_ID =  @APP_VERSION_ID  
  END       
       
 --     
 SET @RESULT=1    
 RETURN @RESULT    
 END          
END       
      
    
  
  
  
  
  
  
  
GO

