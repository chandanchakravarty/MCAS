IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetMNT_EARTHQUAKEZONE]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetMNT_EARTHQUAKEZONE]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                                                  
Proc Name        :          Proc_GetMNT_EARTHQUAKEZONE                                                   
Created by       :          Praveen Kasana                                                 
Date             :          28 April 2006                                                
Purpose          :          Get the EarthQuake ZOnes from MNT_TERRITORY_ZONES
Revison History  :                                                  
Used In          :           Wolverine                                                  
------------------------------------------------------------                                                  
Date     Review By          Comments                                                  
------   ------------       -------------------------*/              
      
CREATE PROC DBO.Proc_GetMNT_EARTHQUAKEZONE                                       
(  

@LOBCODE VARCHAR(10),   
@STATE_NAME  VARCHAR(20),                                                        
@ZIP VARCHAR(20)                    
                             
             
                              
)                                                 
AS                                                  
BEGIN                     
                  
--Get StateName                                
DECLARE @STATE_ID INT                                
SELECT @STATE_ID=STATE_ID FROM MNT_COUNTRY_STATE_LIST                                
WHERE STATE_NAME=@STATE_NAME                        
                  
--Get LOB ID from LOBCODE                     
DECLARE @APP_LOB INT                                    
SELECT @APP_LOB=LOB_ID          
FROM MNT_LOB_MASTER WHERE LOB_CODE=@LOBCODE          
                          
SELECT ISNULL(EARTHQUAKE_ZONE,'0') FROM MNT_TERRITORY_CODES 
WHERE ZIP=@ZIP AND STATE =@STATE_ID AND LOBID= @APP_LOB                
         
       
END                           
          
        
                              
                                
                      
                  
                 
              
            
          
        
      
    
  



GO

