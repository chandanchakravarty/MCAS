IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_ActivateDeactivateWatercraft]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_ActivateDeactivateWatercraft]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

  /****** Object:  Stored Procedure dbo.Proc_ActivateDeactivateWatercraft    Script Date: 5/25/2006 4:10:01 PM ******/  
/*----------------------------------------------------------                    
Proc Name       : dbo.Proc_ActivateDeactivateWatercraft            
Created by      : Priya Arora              
Date            : 26/12/2005                              
Purpose         :Activate/ Deactivate Watercraft           
Revison History :                    
Used In         : Wolverine                    
------------------------------------------------------------              
Modified By  : Ashwani      
Date   : 13 Feb. 2006      
Purpose  : To check the boat_id is being used in APP_WATERCRAFT_EQUIP_DETAILLS or APP_WATERCRAFT_TRAILER_INFO,      
    then we can't deactivate the Boat.      
                 
------------------------------------------------------------                    
Date     Review By          Comments                    
------   ------------       -------------------------  
  
Modified By : RPSINGH  
Date     : 25 May 2006    
Purpose   : Equipment will be no longer attach to a boat.   
    So for deletion of boat no need to check for associated equipment  
  
*/                    
--drop proc Proc_ActivateDeactivateWatercraft  
CREATE  PROC DBO.Proc_ActivateDeactivateWatercraft                    
(                    
@CUSTOMER_ID     INT,                    
@APP_ID     INT,                    
@APP_VERSION_ID     SMALLINT,                    
@BOAT_ID     SMALLINT,            
@IS_ACTIVE NCHAR(2),      
@RESULT  INT  =NULL OUTPUT              
)                    
AS                    
BEGIN                    
            
IF(@IS_ACTIVE='N')      
 BEGIN       
  IF EXISTS( SELECT  ASSOCIATED_BOAT FROM APP_WATERCRAFT_TRAILER_INFO  WHERE           
  CUSTOMER_ID = @CUSTOMER_ID AND  APP_ID =  @APP_ID AND APP_VERSION_ID =  @APP_VERSION_ID AND           
  ASSOCIATED_BOAT= @BOAT_ID AND ISNULL(IS_ACTIVE,'N') = 'Y')  --IS_ACTIVE added by Charles on 23-Oct-09 for Itrack 6600
   BEGIN       
    SET @RESULT=-2      
    RETURN @RESULT      
   END       
  ELSE      
   BEGIN       
    UPDATE APP_WATERCRAFT_INFO       
    SET IS_ACTIVE=@IS_ACTIVE       
    WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID AND            
          BOAT_ID=@BOAT_ID     
  
   UPDATE  APP_WATERCRAFT_DRIVER_DETAILS      
     SET VEHICLE_ID=NULL      
     WHERE  CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID       
      AND VEHICLE_ID = @BOAT_ID                 
          
    SET @RESULT=1      
    RETURN @RESULT      
   END       
END      
 ELSE      
 BEGIN       
      
 UPDATE APP_WATERCRAFT_INFO       
 SET IS_ACTIVE=@IS_ACTIVE       
 WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID AND            
       BOAT_ID=@BOAT_ID          
 SET @RESULT=1      
    
       
         
    
 RETURN @RESULT            
 END      
END            
          
        
      
    
  
  
  
GO

