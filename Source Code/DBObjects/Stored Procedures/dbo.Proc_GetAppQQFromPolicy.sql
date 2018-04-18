IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetAppQQFromPolicy]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetAppQQFromPolicy]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



/*----------------------------------------------------------            
Proc Name      : dbo.Proc_GetAppQQFromPolicy            
Created by       : Mohit Agarwal            
Date             : 5-Jun-2007            
Purpose       : retrieving Application and Quote from Policy            
Revison History :           
Modified By	:Pravesh K Chandel 
Modfied Date	:17 sep 2007
Purpose		: Remove Locking
Used In        : Wolverine            
        
Modified By : 
Modified On : 
Purpose  :   

Reviewed By	:	Anurag Verma
Reviewed On	:	12-07-2007
------------------------------------------------------------            
Date     Review By          Comments            
------   ------------       -------------------------*/            
--DROP PROC Dbo.Proc_GetAppQQFromPolicy 1118,1,3          
CREATE PROC dbo.Proc_GetAppQQFromPolicy            
(@CUSTOMER_ID INT,            
 @POLICY_ID INT,
 @POLICY_VERSION_ID INT
)
AS            
BEGIN            

SELECT POL.APP_ID, POL.APP_VERSION_ID, QQ.QQ_ID FROM POL_CUSTOMER_POLICY_LIST POL with(nolock)
LEFT OUTER JOIN CLT_QUICKQUOTE_LIST QQ with(nolock) ON POL.APP_ID = QQ.APP_ID AND POL.APP_VERSION_ID = QQ.APP_VERSION_ID
				AND POL.CUSTOMER_ID = QQ.CUSTOMER_ID
WHERE POL.CUSTOMER_ID = @CUSTOMER_ID AND POL.POLICY_ID = @POLICY_ID 
		AND POL.POLICY_VERSION_ID=@POLICY_VERSION_ID
            
END            
      
        
        
        
        
        
        
        
      
    
  
  








GO

