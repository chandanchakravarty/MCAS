IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_UPDATE_POL_POLICY_REJECTION]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_UPDATE_POL_POLICY_REJECTION]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

          
 /*----------------------------------------------------------                    
Proc Name       : dbo.POL_POLICY_REJECTION            
Created by      : Pradeep Kushwaha  
Date            : 08/27/2010                    
Purpose         : Update records in POL_POLICY_REJECTION Table.                    
Revison History :                    
Used In        : Ebix Advantage                    
------------------------------------------------------------                    
Date     Review By          Comments                    
------   ------------       -------------------------*/                    
--DROP PROC dbo.PROC_UPDATE_POL_POLICY_REJECTION   
     
CREATE PROC [dbo].[PROC_UPDATE_POL_POLICY_REJECTION]       
(        
@CUSTOMER_ID INT,        
@POLICY_ID INT,        
@POLICY_VERSION_ID SMALLINT,        
@REJECT_REASON_ID SMALLINT OUT,  
@REASON_TYPE_ID INT,
@REASON_DESC NVARCHAR(4000)=NULL,
@MODIFIED_BY INT,        
@LAST_UPDATED_DATETIME DATETIME        
        
)        
AS         
BEGIN        
      
UPDATE POL_POLICY_REJECTION        
    
SET         
REASON_TYPE_ID= @REASON_TYPE_ID ,
REASON_DESC=@REASON_DESC ,
MODIFIED_BY=@MODIFIED_BY,    
LAST_UPDATED_DATETIME=@LAST_UPDATED_DATETIME    
        
    
where   CUSTOMER_ID=@CUSTOMER_ID AND    
		POLICY_ID=@POLICY_ID AND    
		POLICY_VERSION_ID=@POLICY_VERSION_ID AND    
		REJECT_REASON_ID=@REJECT_REASON_ID    
END 
GO

