IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_GETCONTRACT_NAMEFROM_ID]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_GETCONTRACT_NAMEFROM_ID]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------  
PROC NAME                : DBO.PROC_GETCONTRACT_NAMEFROM_ID  
CREATED BY               : Shafee  
DATE                     : 09/1/2006
PURPOSE                  : TO GET Contract Name   FROM MNT_AGENCY_LIST TABLE  
REVISON HISTORY         :  WOLVERINE  
------------------------------------------------------------  
DATE     REVIEW BY          COMMENTS  
------   ------------       -------------------------*/  
CREATE  PROC DBO.PROC_GETCONTRACT_NAMEFROM_ID  
(  
  
 @Contact_id INT 
)  
  
AS  
BEGIN  
  
SELECT ISNULL(CONTRACT_NUMBER,'') +':'+ ISNULL(CONTRACT_NAME,'') as CONTRACT_NAME
FROM MNT_REINSURANCE_CONTRACT INNER JOIN MNT_CONTRACT_NAME ON 
MNT_REINSURANCE_CONTRACT.CONTRACT_NAME_ID=MNT_CONTRACT_NAME.CONTRACT_NAME_ID 
WHERE CONTRACT_ID=@Contact_id
   
END  
  
  
  



GO

