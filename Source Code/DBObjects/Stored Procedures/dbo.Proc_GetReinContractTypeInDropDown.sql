IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetReinContractTypeInDropDown]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetReinContractTypeInDropDown]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

 /*----------------------------------------------------------  
Proc Name       : dbo.Proc_GetReinContractTypeInDropDown  
Created by      : Manoj Rathore  
Date            : 7th Sep 2007  
Purpose     : to parent Reinsurance Contact Type  in drop down  
Used In  : Wolverine  
------------------------------------------------------------  
Date     Review By          Comments  
------   ------------       -------------------------*/  
--drop proc dbo.Proc_GetReinContractTypeInDropDown   
CREATE  PROC [dbo].[Proc_GetReinContractTypeInDropDown]   
@LANG_ID INT =1
AS  
BEGIN  
 SELECT M.CONTRACTTYPEID,ISNULL(L.CONTRACT_TYPE_DESC,M.CONTRACT_TYPE_DESC) AS  CONTRACT_TYPE_DESC 
 FROM MNT_REINSURANCE_CONTRACT_TYPE  M LEFT OUTER JOIN
      MNT_REINSURANCE_CONTRACT_TYPE_MULTILINGUAL L ON M.CONTRACTTYPEID=L.CONTRACTTYPEID and L.LANG_ID=@LANG_ID
 WHERE ISNULL(M.IS_ACTIVE,'Y')='Y' ORDER BY CONTRACT_TYPE_DESC    
  
END  
  
  
  
GO

