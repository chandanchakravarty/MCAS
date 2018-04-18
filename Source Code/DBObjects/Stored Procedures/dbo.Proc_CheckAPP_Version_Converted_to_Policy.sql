IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_CheckAPP_Version_Converted_to_Policy]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_CheckAPP_Version_Converted_to_Policy]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- 
/* =======================================================================                         
 Proc Name       : dbo.Proc_CheckAPP_Converted_to_Policy                                      
 Created by      : Shafi                                      
 Date            : 21 Feb. 2006                                     
 Purpose         : Check for Converted Apploication Into Policy  
  ========================================================================*/    
--    drop proc Proc_CheckAPP_Version_Converted_to_Policy
CREATE PROC Proc_CheckAPP_Version_Converted_to_Policy    
(                                            
@CUSTOMER_ID INT,                                            
@APP_ID INT,  
@APP_VERSION_ID   INT,
@convertr  INT OUTPUT    
)    
AS    
BEGIN    
IF EXISTS (SELECT * FROM POL_CUSTOMER_POLICY_LIST    
WHERE CUSTOMER_ID=@CUSTOMER_ID AND    
APP_ID=@APP_ID --AND APP_VERSION_ID = @APP_VERSION_ID 
 )    
set @convertr= 1   
ELSE    
set @convertr=2
END    
    
    
    
  





GO

