IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetXMLPOL_WATER_MVR_INFORMATION]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetXMLPOL_WATER_MVR_INFORMATION]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                    
Proc Name        :            dbo.Proc_GetXMLPOL_WATER_MVR_INFORMATION                     
Created by         :           Anurag verma                    
Date                :           08/11/2005                    
Purpose           :           Get the table information in the form of xml data                    
Revison History  :                    
Used In             :           Wolverine                    
------------------------------------------------------------                    
Date     Review By          Comments        
drop PROC Dbo.Proc_GetXMLPOL_WATER_MVR_INFORMATION            
------   ------------       -------------------------*/                    
CREATE PROC Dbo.Proc_GetXMLPOL_WATER_MVR_INFORMATION    
(                    
          
 @POL_WATER_MVR_ID  INT ,    
 @CUSTOMER_ID INT,    
 @POLICY_ID INT,    
 @POLICY_VERSION_ID INT,    
 @DRIVER_ID INT                          
)                    
AS                    
BEGIN                    
 SELECT P.APP_WATER_MVR_ID,                    
 P.CUSTOMER_ID,                    
 P.Policy_ID,                    
 P.Policy_VERSION_ID,                    
 P.DRIVER_ID,                
 P.VIOLATION_ID,                    
 P.DRIVER_ID,                    
 P.MVR_AMOUNT,                    
 P.MVR_DEATH,                    
 convert(char,P.mvr_date,101) MVR_DATE,      
 convert(char,P.occurence_date,101) OCCURENCE_DATE,     
 P.DETAILS,   
 case when P.POINTS_ASSIGNED is null then '' else convert(varchar,P.POINTS_ASSIGNED) end POINTS_ASSIGNED,   
 case when P.ADJUST_VIOLATION_POINTS is null then '' else convert(varchar,P.ADJUST_VIOLATION_POINTS) end ADJUST_VIOLATION_POINTS,                                       
 P.IS_ACTIVE,  
  (M.VIOLATION_DES + ' (' + CAST(M.MVR_POINTS AS VARCHAR) +  '/' +  CAST(M.SD_POINTS AS VARCHAR)  +  ')' )AS VIOLATION_DES    
 FROM  POL_WATERCRAFT_MVR_INFORMATION P JOIN MNT_VIOLATIONS M  
 ON P.VIOLATION_ID=M.VIOLATION_ID  
 WHERE  APP_WATER_MVR_ID=@POL_WATER_MVR_ID and                       
 P.CUSTOMER_ID=@CUSTOMER_ID AND    
 P.POLICY_ID= @POLICY_ID AND    
 P.POLICY_VERSION_ID= @POLICY_VERSION_ID AND    
 P.DRIVER_ID= @DRIVER_ID  
END    
    
    
    
  



GO

