IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_LOBLIST]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_LOBLIST]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


  /*----------------------------------------------------------        
Proc Name   : dbo.Proc_LOBLIST       
Created by  : Gaurav        
Date        : 24 Aug ,2005      
Purpose     :         
Revison History  :              
 ------------------------------------------------------------                    
Date     Review By          Comments   

Modified By: Charles Gomes
Date       : 25-May-2010
Purpose    : Multilingual Support               

DROP PROC Proc_LOBLIST 2         
------   ------------       -------------------------*/        
CREATE PROCEDURE [dbo].[Proc_LOBLIST] 
@LANG_ID INT = 1     
AS       
BEGIN        
 SELECT        
 MLM.LOB_ID, ISNULL(MLL.LOB_DESC, MLM.LOB_DESC) AS LOB_DESC
 FROM  MNT_LOB_MASTER MLM WITH(NOLOCK)
 LEFT OUTER JOIN MNT_LOB_MASTER_MULTILINGUAL MLL WITH(NOLOCK)  
 ON MLL.LOB_ID = MLM.LOB_ID AND MLL.LANG_ID = @LANG_ID 
 WHERE ISNULL(MLM.IS_ACTIVE,'Y') = 'Y'   
 ORDER BY ISNULL(MLL.LOB_DESC, MLM.LOB_DESC) ASC  
                  
End  
  

GO

