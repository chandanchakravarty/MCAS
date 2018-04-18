IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetClaimCatastropheCode]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetClaimCatastropheCode]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                                        
Proc Name       : dbo.Proc_GetClaimCatastropheCode          
Created by      : Sumit Chhabra                                      
Date            : 28/04/2006                                        
Purpose         : Retrieve catastrophe event code within loss date period  
Used In        : Wolverine      
MODIFIED BY  :PRAVESH k CHANDEL  
DATE   :30 JULY 2008  
PURPOSE   :fetch all options along along with eff from date and to date without any check of date  
------------------------------------------------------------                                        
Date     Review By          Comments                                        
------   ------------       -------------------------*/                                        
-- DROP PROC dbo.Proc_GetClaimCatastropheCode   
CREATE PROC [dbo].[Proc_GetClaimCatastropheCode]                                                 
@LOB_ID INT=null          
AS                                        
BEGIN   
 
   ------------------------------------------------------------------------------
  -- MODIFIED BY SANTOSH KUMAR GAUTAM ON 22 MARCH 2011
  -- FETCH CONTRACT WITH XOL CONTRACT TYPE
  ------------------------------------------------------------------------------
  SELECT R.CONTRACT_ID AS CATASTROPHE_EVENT_ID , (R.CONTRACT_DESC+' - '+ISNULL(R.CONTRACT_NUMBER,'')) AS DESCRIPTION FROM 
  MNT_REINSURANCE_CONTRACT  R INNER JOIN
  MNT_REIN_CONTRACT_LOB L ON  L.CONTRACT_ID=R.CONTRACT_ID       
  WHERE R.CONTRACT_TYPE IN (3,4) AND L.CONTRACT_LOB=@LOB_ID
  
  
END                                  
  
  
  

GO

