IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetHORule_AddInterest_Pol]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetHORule_AddInterest_Pol]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/* ----------------------------------------------------------                                                                                                                                        
Proc Name                : Dbo.Proc_GetHORule_AddInterest_Pol  1151,634,1,1                                                                                                                                    
Created by               : Manoj Rathore                                                                                                                                        
Date                     : 25 Oct.,2007                                                                                        
Purpose                  : To get the Additional Interest at policy level for RD rules                                                                                        
Revison History          :                                                                                                                                        
Used In                  : Wolverine                                                                                                                                        
------------------------------------------------------------                                                                                                                                        
Date     Review By          Comments                                                                                                                                        
------   ------------       -------------------------*/                  
-- DROP PROC Proc_GetHORule_AddInterest_Pol 1707,1,1,1                                                                                                                
CREATE proc dbo.Proc_GetHORule_AddInterest_Pol                                                                                  
(                                                                                                                                        
@CUSTOMER_ID    int,                                                                                                                                  
@POLICY_ID    int,                                                                                                                                  
@POLICY_VERSION_ID   int,                                                                                       
@DWELLING_ID int                                                                                                                          
)                                                                                                                                        
AS                                                                                                                                            
BEGIN                                                                                           
 -- Mandatory      
                                                                                         
 DECLARE @BILL_MORTAGAGEE_COUNT INT     
 DECLARE @HOME_BILLMORTAGAGEE CHAR   
--Added by Charles on 9-Sep-09 for Itrack 6370  
 DECLARE @MORE_MORTAGAGEE CHAR   
 DECLARE @MORE_MORTAGAGEE_COUNT CHAR    
 DECLARE @STATE_ID INT    
 SELECT @STATE_ID=STATE_ID FROM POL_CUSTOMER_POLICY_LIST WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID= @POLICY_ID AND POLICY_VERSION_ID = @POLICY_VERSION_ID AND IS_ACTIVE='Y'                                                                                 
  
   
 SELECT @MORE_MORTAGAGEE_COUNT=COUNT(ADD_INT_ID) FROM POL_HOME_OWNER_ADD_INT WITH(NOLOCK)  
 WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID= @POLICY_ID AND POLICY_VERSION_ID = @POLICY_VERSION_ID AND DWELLING_ID=@DWELLING_ID     
 AND IS_ACTIVE='Y' AND @STATE_ID=14 --AND NATURE_OF_INTEREST='11867' --Commented by Charles on 5-Oct-09 for Itrack 6370  
 IF(@MORE_MORTAGAGEE_COUNT>2)    
  BEGIN    
   SET @MORE_MORTAGAGEE='Y'    
  END       
  ELSE       BEGIN    
   SET @MORE_MORTAGAGEE='N'    
  END   
--Added till here     
                                     
 IF EXISTS (SELECT CUSTOMER_ID FROM POL_HOME_OWNER_ADD_INT     WITH(NOLOCK)                                                                                                      
 WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID= @POLICY_ID AND POLICY_VERSION_ID = @POLICY_VERSION_ID AND DWELLING_ID=@DWELLING_ID and IS_ACTIVE='Y')                                                                                        
 BEGIN             
  SELECT @BILL_MORTAGAGEE_COUNT=COUNT(BILL_MORTAGAGEE)                                               
  FROM POL_HOME_OWNER_ADD_INT  WITH(NOLOCK)                                                                                      
  WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID= @POLICY_ID AND POLICY_VERSION_ID = @POLICY_VERSION_ID AND DWELLING_ID=@DWELLING_ID and IS_ACTIVE='Y'                        
    AND ISNULL(BILL_MORTAGAGEE,0) = 10963    
  END           
ELSE             
  BEGIN            
  SET @BILL_MORTAGAGEE_COUNT=0    
  END     
  IF EXISTS(SELECT BILL_TYPE_ID FROM POL_CUSTOMER_POLICY_LIST WITH(NOLOCK)  WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID= @POLICY_ID AND POLICY_VERSION_ID = @POLICY_VERSION_ID and IS_ACTIVE='Y'    
   AND (POLICY_LOB=1 OR POLICY_LOB=6) AND (BILL_TYPE_ID=11276)) -- OR BILL_TYPE_ID=  11278 or BILL_TYPE_ID=11277) ) CHANGED BY PRAVESH ON 3RD APRIL 2008 AS MORTGAGEE SINCE INCEPTION WILL BE CONSIDERED    
  BEGIN    
  IF(@BILL_MORTAGAGEE_COUNT>0)    
   BEGIN    
    SET @HOME_BILLMORTAGAGEE='N'    
   END                         
         ELSE    
   BEGIN    
    SET @HOME_BILLMORTAGAGEE='Y'    
   END    
 END     
 ELSE    
   BEGIN    
    SET @HOME_BILLMORTAGAGEE='N'    
   END    
SELECT                     
 @HOME_BILLMORTAGAGEE AS HOME_BILLMORTAGAGEE,  
 @MORE_MORTAGAGEE AS MORE_MORTAGAGEE  --Added by Charles on 9-Sep-09 for Itrack 6370     
END     
    
GO

