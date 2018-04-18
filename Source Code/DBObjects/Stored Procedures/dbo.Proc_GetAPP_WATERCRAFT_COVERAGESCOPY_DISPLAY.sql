IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetAPP_WATERCRAFT_COVERAGESCOPY_DISPLAY]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetAPP_WATERCRAFT_COVERAGESCOPY_DISPLAY]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


--drop proc Proc_GetAPP_WATERCRAFT_COVERAGESCOPY_Display                  
/*----------------------------------------------------------                                    
Proc Name   : dbo.drop proc Proc_GetAPP_WATERCRAFT_COVERAGESCOPY_Display                            
Created by  : Shafi                          
Date        : 17 May,2006                          
Purpose     : Get the coverages IDs for copying                          
Revison History  :                                          
 ------------------------------------------------------------                                                
Date     Review By          Comments                                              
                                     
------   ------------       -------------------------*/         
        
CREATE PROCEDURE dbo.Proc_GetAPP_WATERCRAFT_COVERAGESCOPY_DISPLAY                          
(                          
 @VEHICLE_ID smallint,                          
 @CUSTOMER_ID int,                          
 @APP_ID int,                          
 @APP_VERSION_ID int                      
                     
                          
)                              
AS                                   
BEGIN                                    
         
--Substring(convert(varchar(30),convert(money,isnull(avc.LIMIT_1,0)),1),0,charindex('.',convert(varchar(30),convert(money,isnull(avc.LIMIT_1,0)),1),0))                    
                   
 --Get the Coverages                          
 SELECT AVC.*,                          
        C.COV_ID,                          
  C.COV_CODE,                          
  C.IS_MANDATORY ,          
               
  COV_DES as COV_DESC ,             
   CASE C.LIMIT_TYPE           
   WHEN 2 THEN--CONVERT(VARCHAR(20),AVC.LIMIT_1) + CONVERT(VARCHAR(20),ISNULL(AVC.LIMIT1_AMOUNT_TEXT,''))           
                Substring(convert(varchar(30),convert(money,avc.LIMIT_1),1),0,charindex('.',convert(varchar(30),convert(money,avc.LIMIT_1),1),0)) +        
                CONVERT(VARCHAR(20),ISNULL(AVC.LIMIT2_AMOUNT_TEXT,''))            + '/' +         
                Substring(convert(varchar(30),convert(money,avc.LIMIT_2),1),0,charindex('.',convert(varchar(30),convert(money,avc.LIMIT_1),1),0)+1) +        
                CONVERT(VARCHAR(20),ISNULL(AVC.LIMIT2_AMOUNT_TEXT,''))          
   ELSE Substring(convert(varchar(30),convert(money,avc.LIMIT_1),1),0,charindex('.',convert(varchar(30),convert(money,avc.LIMIT_1),1),0)) + CONVERT(VARCHAR(20),ISNULL(AVC.LIMIT1_AMOUNT_TEXT,''))          
   END AS LIMIT,          
   CASE C.DEDUCTIBLE_TYPE          
   WHEN 2 THEN Substring(convert(varchar(30),convert(money,avc.Deductible_1),1),0,charindex('.',convert(varchar(30),convert(money,avc.Deductible_1),1),0))        
                + CONVERT(VARCHAR(20),ISNULL(AVC.DEDUCTIBLE1_AMOUNT_TEXT,'')) + '/' +          
               Substring(convert(varchar(30),convert(money,avc.Deductible_2),1),0,charindex('.',convert(varchar(30),convert(money,avc.Deductible_2),1),0))  + CONVERT(VARCHAR(20),ISNULL(AVC.DEDUCTIBLE2_AMOUNT_TEXT,''))          
   ELSE Substring(convert(varchar(30),convert(money,avc.Deductible_1),1),0,charindex('.',convert(varchar(30),convert(money,avc.Deductible_1),1),0)) + CONVERT(VARCHAR(20),ISNULL(AVC.DEDUCTIBLE1_AMOUNT_TEXT,''))          
   END AS DEDUCTIBLE          
          
          
                    
            
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
              
              
                        
END                          
                          
                          
                        
                      
                    
                  
                
              
            
          
        
      
    
  



GO

