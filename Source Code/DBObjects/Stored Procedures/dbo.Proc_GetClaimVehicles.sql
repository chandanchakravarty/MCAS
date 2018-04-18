IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetClaimVehicles]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetClaimVehicles]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

 /*         
----------------------------------------------------------                  
Proc Name            : dbo.Proc_GetClaimVehicles        
Created by             : Vijay Arora          
Date                    : 20-06-2006        
Purpose                : To get all the vehicles for policy and Claims         
Revison History   :                  
Used In                 :   Wolverine                  
------   ------------       -------------------------*/                  
--drop proc dbo.Proc_GetClaimVehicles        
CREATE PROC dbo.Proc_GetClaimVehicles             
(                  
    @CLAIM_ID  int          
)                  
AS                  
BEGIN                 
 SELECT VEHICLE_YEAR,MAKE,MODEL,VIN,INSURED_VEHICLE_ID AS VEHICLE_ID    
  FROM CLM_INSURED_VEHICLE WHERE CLAIM_ID = @CLAIM_ID AND IS_ACTIVE= 'Y'--Done for Itrack Issue 6053 on 10 Sept 09  
    
END                  
      
    
GO

