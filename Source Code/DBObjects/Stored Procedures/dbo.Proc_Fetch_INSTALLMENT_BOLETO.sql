IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_Fetch_INSTALLMENT_BOLETO]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_Fetch_INSTALLMENT_BOLETO]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--------------------------------------------------------------                              
--Proc Name        : [Proc_Fetch_INSTALLMENT_BOLETO]                  
--Created by       : Naveen Pujari                         
--Date             : 25/11/2010                              
--Purpose          : Retrieving data from POL_INSTALLMENT_BOLETO  table to create Boleto in PDF Format, it wil be generated against  each Boletoid/Receiptid                                           
--Used In          : Ebix Advantage                          
-------------------------------------------------------------- 
		-- select * from POL_INSTALLMENT_BOLETO  
		-- exec DROP PROC Proc_Fetch_INSTALLMENT_BOLETO   467 ,1,2043
		
		
CREATE PROCEDURE   [dbo].[Proc_Fetch_INSTALLMENT_BOLETO]-- 28018,15,1
     
@CUSTOMER_ID int=NULL,   
@POLICY_ID int=NULL,        
@POLICY_VERSION_ID int=NULL      
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	
	
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
    --select CUSTOMER_ID,POLICY_ID, INSTALLEMT_ID,BOLETO_ID ,  BOLETO_HTML 
    --from  POL_INSTALLMENT_BOLETO  where   BOLETO_ID=@Boleto_ID  and OUR_NUMBER is not null
  
     SELECT     
  PIB.CUSTOMER_ID, PIB.POLICY_ID, PIB.POLICY_VERSION_ID,  PIB.BOLETO_HTML   ,DT.RELEASED_STATUS  
    FROM     
  POL_INSTALLMENT_BOLETO PIB  WITH(NOLOCK)    
  LEFT OUTER JOIN
   ACT_POLICY_INSTALLMENT_DETAILS DT WITH(NOLOCK) ON 
   DT.CUSTOMER_ID=PIB.CUSTOMER_ID AND DT.POLICY_ID=PIB.POLICY_ID AND DT.POLICY_VERSION_ID=PIB.POLICY_VERSION_ID AND DT.INSTALLMENT_NO=PIB.INSTALLMENT_NO
   AND DT.ROW_ID=PIB.INSTALLEMT_ID
    WHERE     
  PIB.CUSTOMER_ID = @CUSTOMER_ID AND     
  PIB.POLICY_ID = @POLICY_ID AND     
  PIB.POLICY_VERSION_ID = @POLICY_VERSION_ID AND ISNULL(IS_ACTIVE,'Y')='Y'    
    ORDER BY     
    PIB.POLICY_ID , PIB.POLICY_VERSION_ID  
		
    
END 

GO

