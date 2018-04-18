IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_FetchReinsuranceContracts]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_FetchReinsuranceContracts]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------  
Proc Name       : Proc_FetchReinsuranceContracts  
Created by      : Ravindra  
Date            : 13-12-2006  
Purpose     :   
Revison History :  
Modified  	:Praveen Kasana
Purpose		:Contract Number - Major Participant - Contract Year
Used In  : Wolverine  
--Contract Number - Major Participant - Contract Year
------------------------------------------------------------  
Date     Review By          Comments  
------   ------------       -------------------------*/  
-- drop proc Dbo.Proc_FetchReinsuranceContracts  
create   prOC Dbo.Proc_FetchReinsuranceContracts  
as  
begin  
 /*SELECT D.CONTRACT_NUMBER + ' - ' + ISNULL(D.CONTRACT_DESC,'') + ' - ' + ISNULL(M.CONTRACT_TYPE_DESC,'') + ' - ' + ISNULL(D.CONTACT_YEAR,'')  AS Rein_Comapany_NAME,  
 CONTRACT_ID as Rein_Comapany_ID  
 FROM MNT_REINSURANCE_CONTRACT D  
 INNER JOIN MNT_REINSURANCE_CONTRACT_TYPE M   
 ON M.CONTRACTTYPEID = D.CONTRACT_TYPE  
 Order By Rein_Comapany_NAME  */
 SELECT D.CONTRACT_NUMBER + ' - ' + ISNULL(MRCL.REIN_COMAPANY_NAME,'') + ' - ' + ISNULL(D.CONTACT_YEAR,'')  AS REIN_COMAPANY_NAME,  
 D.CONTRACT_ID AS REIN_COMAPANY_ID  
 FROM MNT_REINSURANCE_CONTRACT D  
 INNER JOIN MNT_REINSURANCE_CONTRACT_TYPE M   ON M.CONTRACTTYPEID = D.CONTRACT_TYPE  
 INNER JOIN MNT_REINSURANCE_MAJORMINOR_PARTICIPATION MM ON MM.CONTRACT_ID = D.CONTRACT_ID 
 INNER JOIN MNT_REIN_COMAPANY_LIST  MRCL ON MM.REINSURANCE_COMPANY = MRCL.REIN_COMAPANY_ID  
 WHERE MM.PARTICIPATION_ID = (SELECT TOP 1 PARTICIPATION_ID FROM MNT_REINSURANCE_MAJORMINOR_PARTICIPATION WHERE 
 MNT_REINSURANCE_MAJORMINOR_PARTICIPATION.CONTRACT_ID = D.CONTRACT_ID)
 ORDER BY REIN_COMAPANY_NAME 

end  
  







GO

