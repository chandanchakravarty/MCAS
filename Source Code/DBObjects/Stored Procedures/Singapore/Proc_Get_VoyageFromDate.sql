---------------------------------------------------------------      
--Proc Name          : dbo.[Proc_Get_VoyageFromDate]  
--Created by         : Ruchika Chauhan          
--Date               : 21-March-2012             
--------------------------------------------------------      
--Date     Review By          Comments              
------   ------------       -------------------------*/             
-- drop proc dbo.[Proc_Get_VoyageFromDate]            

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_Get_VoyageFromDate]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_Get_VoyageFromDate]
GO


CREATE  PROCEDURE [dbo].Proc_Get_VoyageFromDate
(
		@CUSTOMER_ID int          
       ,@POLICY_ID int          
       ,@POLICY_VERSION_ID smallint          
       ,@QUOTE_ID int   
) 
AS             
BEGIN            
select PCPL.APP_EFFECTIVE_DATE from POL_CUSTOMER_POLICY_LIST PCPL, CLT_QUICKQUOTE_LIST CQL 
where PCPL.CUSTOMER_ID=CQL.CUSTOMER_ID and
CQL.QQ_ID=@QUOTE_ID and PCPL.CUSTOMER_ID=@CUSTOMER_ID and PCPL.POLICY_ID=@POLICY_ID and PCPL.POLICY_VERSION_ID=@POLICY_VERSION_ID

END
  
  
--exec Proc_Get_VoyageFromDate