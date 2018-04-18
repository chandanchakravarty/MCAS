IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetAPP_WATERCRAFT_COVERAGES_COPY]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetAPP_WATERCRAFT_COVERAGES_COPY]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


--drop proc Proc_GetAPP_WATERCRAFT_COVERAGES_COPY    
/*----------------------------------------------------------                      
Proc Name   : dbo.Proc_GetAPP_VEHICLE_COVERAGES_COPY            
Created by  : Pradeep            
Date        : 18 October,2005            
Purpose     : Get the coverages IDs for copying            
Revison History  :                            
 ------------------------------------------------------------                                  
Date     Review By          Comments                                
                       
------   ------------       -------------------------*/    
CREATE PROCEDURE dbo.Proc_GetAPP_WATERCRAFT_COVERAGES_COPY            
(            
 @VEHICLE_ID smallint,            
 @CUSTOMER_ID int,            
 @APP_ID int,            
 @APP_VERSION_ID int,          
 @CALLED_FROM VarChar(4)            
            
)                
AS                     
BEGIN                      
           
IF (   @CALLED_FROM = 'WWAT' OR @CALLED_FROM = 'HWAT' )          
BEGIN          
          
 --Get the Coverages            
 SELECT AVC.*,            
        C.COV_ID,            
  C.COV_CODE,            
  C.IS_MANDATORY              
 FROM APP_WATERCRAFT_COVERAGE_INFO AVC            
 INNER JOIN MNT_COVERAGE C ON            
  AVC.COVERAGE_CODE_ID = C.COV_ID            
 WHERE CUSTOMER_ID = @CUSTOMER_ID AND             
 APP_ID = @APP_ID AND             
 APP_VERSION_ID = @APP_VERSION_ID  AND            
 BOAT_ID = @VEHICLE_ID            
                 
 --Get the coverage ranges            
 SELECT MCR.*            
 FROM APP_WATERCRAFT_COVERAGE_INFO AVC            
 INNER JOIN MNT_COVERAGE C ON            
  AVC.COVERAGE_CODE_ID = C.COV_ID            
 INNER JOIN MNT_COVERAGE_RANGES MCR ON            
  MCR.COV_ID = C.COV_ID            
 WHERE CUSTOMER_ID = @CUSTOMER_ID AND             
 APP_ID = @APP_ID AND             
 APP_VERSION_ID = @APP_VERSION_ID  AND            
 BOAT_ID = @VEHICLE_ID        
    
--Get the State for the application                                  
SELECT CS.STATE_ID,            
       CS.STATE_NAME,          
       A.APP_LOB ,  
       YEAR(A.APP_EFFECTIVE_DATE) as APP_EFFECTIVE_DATE  
FROM APP_LIST A            
INNER JOIN MNT_COUNTRY_STATE_LIST CS ON            
 A.STATE_ID = CS.STATE_ID            
WHERE  A.CUSTOMER_ID=@CUSTOMER_ID    AND            
               A.APP_ID=@APP_ID     AND            
               A.APP_VERSION_ID=@APP_VERSION_ID AND            
    CS.COUNTRY_ID = 1            
    
--Get the Watercraft info from APP_WATERCRAFT_INFO                
SELECT INSURING_VALUE,              
 TYPE_OF_WATERCRAFT,        
 LENGTH,      
 YEAR                
FROM APP_WATERCRAFT_INFO                
WHERE  CUSTOMER_ID = @CUSTOMER_ID AND                                    
   APP_ID = @APP_ID AND                                    
   APP_VERSION_ID = @APP_VERSION_ID  AND                
   BOAT_ID = @VEHICLE_ID                         
          
END          
          
    
          
          
END            
            
            
          
        
      
    
  



GO

