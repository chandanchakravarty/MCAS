IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetPolicyWatercraftsToCopy]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetPolicyWatercraftsToCopy]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


--exec Proc_GetPolicyWatercraftsToCopy     920,22,3,-1,'WWAT'



/*----------------------------------------------------------                    
Proc Name   : dbo.Proc_GetPolicyWatercraftsToCopy          
Created by  : Shafi          
Date        : 15 Feb,2006  
Purpose     : Get the Policy Watercrafts for coying coverages                    
Revison History  :                          
 ------------------------------------------------------------                                
Date     Review By          Comments                              
                     
------   ------------       -------------------------*/  
--DROP PROC Proc_GetPolicyWatercraftsToCopy                  
Create PROCEDURE dbo.Proc_GetPolicyWatercraftsToCopy      
(          
  @CUSTOMER_ID int,          
  @POL_ID int,          
  @POL_VERSION_ID int,          
  @VEHICLE_ID smallint,        
  @CALLED_FROM VarChar(4)          
)              
AS                   
BEGIN                    
          
IF ( @CALLED_FROM = 'WWAT' OR @CALLED_FROM = 'HWAT' OR  @CALLED_FROM = 'WAT' )        
 BEGIN        
         
  SELECT   BOAT_ID,          
	BOAT_ID as RISK_ID,
    BOAT_NO,          
    BOAT_NAME,          
    [YEAR] as BOAT_YEAR  ,    
    MAKE,          
    MODEL ,
    LOOKUP_VALUE_DESC AS  TYPE_OF_WATERCRAFT       
  FROM  POL_WATERCRAFT_INFO  with(nolock)   INNER JOIN MNT_LOOKUP_VALUES  with(nolock) ON
   TYPE_OF_WATERCRAFT=LOOKUP_UNIQUE_ID
     WHERE CUSTOMER_ID = @CUSTOMER_ID AND           
   POLICY_ID = @POL_ID AND           
   POLICY_VERSION_ID = @POL_VERSION_ID  AND          
   BOAT_ID <> @VEHICLE_ID          
          
 END  /*      
        
IF ( @CALLED_FROM = 'UMB')        
 BEGIN        
         
 SELECT   BOAT_ID,          
    BOAT_NO,          
    BOAT_NAME,          
     [YEAR] as BOAT_YEAR,      
    MAKE,          
    MODEL        
  FROM  POL_UMBRELLA_WATERCRAFT_INFO      
     WHERE CUSTOMER_ID = @CUSTOMER_ID AND           
   APP_ID = @APP_ID AND           
   APP_VERSION_ID = @APP_VERSION_ID  AND          
   BOAT_ID <> @VEHICLE_ID          
          
 END        
     */           
End          
          
          
        
      
    
  








GO

