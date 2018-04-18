IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetCLM_ACTIVITY_DATA]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetCLM_ACTIVITY_DATA]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

 /*----------------------------------------------------------                                                                              
Proc Name       : dbo.Proc_GetCLM_ACTIVITY_DATA                                                                  
Created by      : Sumit Chhabra                                                                            
Date            : 05/06/2006                                                                              
Purpose         : Get Policy coverages for the given claim id    
Created by      : Sumit Chhabra                                                                             
Revison History :                                                                              
Used In        : Wolverine                                                                              
  
Reviewed By : Anurag verma Reviewed On : 16-07-2007  
------------------------------------------------------------                                                                              
Date     Review By          Comments                                                                              
------   ------------       -------------------------*/                                                                              
-- DROP PROC dbo.Proc_GetCLM_ACTIVITY_DATA    
CREATE PROC [dbo].[Proc_GetCLM_ACTIVITY_DATA]    
@CLAIM_ID int,    
@ACTIVITY_ID int    ,
@LANG_ID int 
AS                                                                              
BEGIN       
DECLARE @ACTIVITY_DATE varchar(10)    
DECLARE @PAYMENT_AMOUNT decimal(20,2)    
DECLARE @ACTIVITY_DESC VARCHAR(50)  
DECLARE @ACTIVITY_REASON smallint  
DECLARE @PAYMENT_DESC VARCHAR(50)  
    
SET @PAYMENT_DESC = 'TP'  --Total Payment
	 SELECT     
	  @ACTIVITY_DATE=CONVERT(CHAR,ACTIVITY.ACTIVITY_DATE,101),  
	  @ACTIVITY_DESC = ISNULL(ISNULL(M.TYPE_DESC, DETAIL.DETAIL_TYPE_DESCRIPTION),''),  
	  @ACTIVITY_REASON = isnull(ACTIVITY_REASON,0)  
	 FROM     
	  CLM_ACTIVITY ACTIVITY WITH(NOLOCK) LEFT OUTER JOIN  
	  CLM_TYPE_DETAIL DETAIL WITH(NOLOCK) ON   ACTIVITY.ACTION_ON_PAYMENT = DETAIL.DETAIL_TYPE_ID  LEFT OUTER JOIN  
	  CLM_TYPE_DETAIL_MULTILINGUAL M  WITH(NOLOCK) ON   ACTIVITY.ACTION_ON_PAYMENT = M.DETAIL_TYPE_ID AND LANG_ID= @LANG_ID
	 WHERE    
	  ACTIVITY.CLAIM_ID=@CLAIM_ID AND    
	  ACTIVITY.ACTIVITY_ID=@ACTIVITY_ID   AND ACTIVITY.IS_ACTIVE='Y' 
	    
    
    
if (@ACTIVITY_REASON=11776)--Recovery  
begin  
	  SELECT    
	   @PAYMENT_AMOUNT = ISNULL(SUM(RECOVERY_AMOUNT),0)    
	  FROM     
	   CLM_ACTIVITY_RESERVE WITH(NOLOCK)
	  WHERE    
	   CLAIM_ID=@CLAIM_ID AND    
	   ACTIVITY_ID=@ACTIVITY_ID    AND IS_ACTIVE='Y'
end  
else if (@ACTIVITY_REASON=11775)--Indemnity Payment  
begin  
	  SELECT    
	   @PAYMENT_AMOUNT = ISNULL(SUM(PAYMENT_AMOUNT),0)    
	  FROM     
	   CLM_ACTIVITY_RESERVE    WITH(NOLOCK)
	  WHERE    
	   CLAIM_ID=@CLAIM_ID AND    
	   ACTIVITY_ID=@ACTIVITY_ID   AND IS_ACTIVE='Y' 
end  
else if (@ACTIVITY_REASON=11774)--Expense Payment  
begin  
	  SELECT    
	   @PAYMENT_AMOUNT = ISNULL(SUM(PAYMENT_AMOUNT),0)    
	  FROM     
	   CLM_ACTIVITY_EXPENSE WITH(NOLOCK) 
	  WHERE    
	   CLAIM_ID=@CLAIM_ID AND    
	   ACTIVITY_ID=@ACTIVITY_ID  AND IS_ACTIVE='Y'  
end --First Notification/New Reserve/Reserve Update/Reinsurance  
else if (@ACTIVITY_REASON=11805 OR @ACTIVITY_REASON=11836 OR   
  @ACTIVITY_REASON=11773 OR @ACTIVITY_REASON=11777)  
begin  
	  SELECT    
	   @PAYMENT_AMOUNT = ISNULL(SUM(OUTSTANDING),0)    
	  FROM     
	   CLM_ACTIVITY_RESERVE  WITH(NOLOCK)
	  WHERE    
	   CLAIM_ID=@CLAIM_ID AND ACTIVITY_ID=@ACTIVITY_ID AND IS_ACTIVE='Y' --AND     ACTIVITY_ID=@ACTIVITY_ID    
      set @PAYMENT_DESC = 'TO'  --Total Outstanding
end  
    
 SELECT @ACTIVITY_DATE AS ACTIVITY_DATE, @PAYMENT_AMOUNT AS PAYMENT_AMOUNT,@ACTIVITY_DESC AS ACTIVITY_DESC,@PAYMENT_DESC as PAYMENT_DESC    
    
END    
  
  
  
  
  
GO

