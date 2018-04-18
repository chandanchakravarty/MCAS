IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_ActivateDeactivateXOLInformation]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_ActivateDeactivateXOLInformation]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

          
 /*----------------------------------------------------------                                                        
Proc Name             : Dbo.Proc_ActivateDeactivateXOLInformation                                                        
Created by            : Santosh Kumar Gautam                                                       
Date                  : 17 March 2011                                                  
Purpose               : To activate/deactivate the XOL information              
Revison History       :                                                        
Used In               : Maintenance module              
------------------------------------------------------------                                                        
Date     Review By          Comments                           
                  
drop Proc Proc_ActivateDeactivateXOLInformation                                               
------   ------------       -------------------------*/      
CREATE PROC [dbo].[Proc_ActivateDeactivateXOLInformation]      
(      
@XOL_ID				 int,
@IS_ACTIVE			 char(1)  
)      
AS      
BEGIN  

 UPDATE MNT_XOL_INFORMATION 
 SET IS_ACTIVE=@IS_ACTIVE
 WHERE XOL_ID=@XOL_ID


END    
  
  
  
  
GO

