IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetPOL_POL_CO_INSURANCE]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetPOL_POL_CO_INSURANCE]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

 -- drop procedure  Proc_GetPOL_POL_CO_INSURANCE
--Proc_GetPOL_POL_CO_INSURANCE 2156,21,1,0,null
CREATE PROC [dbo].[Proc_GetPOL_POL_CO_INSURANCE]    
@CUSTOMER_ID INT,    
@POLICY_ID INT,    
@POLICY_VERSION_ID INT,    
@COMPANY_ID INT,    
@COINSURANCE_ID INT =NULL    
AS    
BEGIN 

 SELECT COINSURANCE_ID,CO_INSURER_NAME,LEADER_FOLLOWER,COINSURANCE_PERCENT,COINSURANCE_FEE,BROKER_COMMISSION,TRANSACTION_ID,
 LEADER_POLICY_NUMBER,MNT_REIN_COMAPANY_LIST.REIN_COMAPANY_NAME,  
 MNT_REIN_COMAPANY_LIST.REIN_COMAPANY_ID,MNT_REIN_COMAPANY_LIST.REIN_COMAPANY_CODE,ENDORSEMENT_POLICY_NUMBER
 FROM POL_CO_INSURANCE WITH(NOLOCK)  
 join MNT_REIN_COMAPANY_LIST   
 on POL_CO_INSURANCE.COMPANY_ID=MNT_REIN_COMAPANY_LIST.REIN_COMAPANY_ID  
 WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID AND POLICY_VERSION_ID = @POLICY_VERSION_ID     
 AND COINSURANCE_ID = ISNULL(@COINSURANCE_ID,COINSURANCE_ID) ORDER BY CO_INSURER_NAME    
END  


GO

