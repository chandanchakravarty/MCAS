IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetWatercraftsToCopy]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetWatercraftsToCopy]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                        
Proc Name   : dbo.Proc_GetWatercrafts              
Created by  : Pradeep              
Date        : 24 October,2005              
Purpose     : Get theWatercrafts for coying coverages                        
Revison History  :                              
 ------------------------------------------------------------                                    
Date     Review By          Comments                                  
                         
------   ------------       -------------------------*/       
--drop proc Proc_GetWatercraftsToCopy                     
CREATE PROCEDURE dbo.Proc_GetWatercraftsToCopy          
(              
  @CUSTOMER_ID int,              
  @APP_ID int,              
  @APP_VERSION_ID int,              
  @VEHICLE_ID smallint,            
  @CALLED_FROM VarChar(4)              
)                  
AS                       
BEGIN                        
              
IF ( @CALLED_FROM = 'WWAT' OR @CALLED_FROM = 'HWAT' or @CALLED_FROM = 'WAT')            
 BEGIN            
             
  SELECT   BOAT_ID AS RISK_ID, 
           BOAT_ID ,            
    BOAT_NO,              
    BOAT_NAME,              
    [YEAR] as BOAT_YEAR  ,        
    MAKE,              
    MODEL,        
    LOOKUP_VALUE_DESC AS TYPE_OF_WATERCRAFT      
          
  FROM  APP_WATERCRAFT_INFO    inner join MNT_LOOKUP_VALUES on TYPE_OF_WATERCRAFT=LOOKUP_UNIQUE_ID      
     WHERE CUSTOMER_ID = @CUSTOMER_ID AND               
   APP_ID = @APP_ID AND               
   APP_VERSION_ID = @APP_VERSION_ID  AND              
   BOAT_ID <> @VEHICLE_ID              
              
 END            
            
           
                    
End              
              
              
            
          
        
      
    
  



GO

