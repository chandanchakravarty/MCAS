IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MNT_REIN_GETXML_SPECIALACCEPTANCEAMOUNT]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[MNT_REIN_GETXML_SPECIALACCEPTANCEAMOUNT]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



  /*    
CREATED BY   : Deepak Batra    
CREATED DATE  : 16 Jan 2006    
Purpose   : To implement the update on the Contract Name Screen    
Modified by :Pravesh k Chandel    
Modified Date :14 aug 2007    
Purpose  : to fetch the record   
*/    
--drop proc [dbo]. [MNT_REIN_GETXML_SPECIALACCEPTANCEAMOUNT]     
CREATE PROCEDURE [dbo].[MNT_REIN_GETXML_SPECIALACCEPTANCEAMOUNT]   
(    
 @SPECIAL_ACCEPTANCE_LIMIT_ID INT    
     
)    
AS    
BEGIN    
    
SELECT SPECIAL_ACCEPTANCE_LIMIT_ID,SPECIAL_ACCEPTANCE_LIMIT, EFFECTIVE_DATE,LOB_ID,IS_ACTIVE    
FROM   MNT_REIN_SPECIAL_ACCEPTANCE_AMOUNT    
WHERE  SPECIAL_ACCEPTANCE_LIMIT_ID = @SPECIAL_ACCEPTANCE_LIMIT_ID    
END    
    
    
    
    
  
  
  


GO

