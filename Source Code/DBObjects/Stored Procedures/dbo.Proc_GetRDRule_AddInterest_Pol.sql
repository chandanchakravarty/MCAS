IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetRDRule_AddInterest_Pol]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetRDRule_AddInterest_Pol]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/* ----------------------------------------------------------                                                                                                                                    
Proc Name                : Dbo.Proc_GetRDRule_AddInterest_Pol  1151,634,1,1                                                                                                                                
Created by               : Manoj Rathore                                                                                                                                    
Date                     : 15 June.,2007                                                                                    
Purpose                  : To get the Additional Interest at policy level for RD rules                                                                                    
Revison History          :                                                                                                                                    
Used In                  : Wolverine                                                                                                                                    
------------------------------------------------------------                                                                                                                                    
Date     Review By          Comments                                                                                                                                    
------   ------------       -------------------------*/              
-- DROP PROC Proc_GetRDRule_AddInterest_Pol                                                                                                             
CREATE proc dbo.Proc_GetRDRule_AddInterest_Pol                                                                              
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
 DECLARE @BILLMORTAGAGEE CHAR  
                                 
 IF EXISTS (SELECT CUSTOMER_ID FROM POL_HOME_OWNER_ADD_INT     WITH(NOLOCK)                                                                                                  
 WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID= @POLICY_ID AND POLICY_VERSION_ID = @POLICY_VERSION_ID AND DWELLING_ID=@DWELLING_ID and IS_ACTIVE='Y')                                                                                    
 BEGIN         
	 SELECT @BILL_MORTAGAGEE_COUNT=COUNT(BILL_MORTAGAGEE)                                           
	 FROM POL_HOME_OWNER_ADD_INT  WITH(NOLOCK)                                                                                  
	 WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID= @POLICY_ID AND POLICY_VERSION_ID = @POLICY_VERSION_ID AND DWELLING_ID=@DWELLING_ID                     
         AND ISNULL(BILL_MORTAGAGEE,0) = 10963 and IS_ACTIVE='Y'
	 END       
ELSE         
	 BEGIN        
	 SET @BILL_MORTAGAGEE_COUNT=0
	 END 
  IF EXISTS(SELECT BILL_TYPE_ID FROM POL_CUSTOMER_POLICY_LIST WITH(NOLOCK)  WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID= @POLICY_ID AND POLICY_VERSION_ID = @POLICY_VERSION_ID and IS_ACTIVE='Y'
   AND (POLICY_LOB=1 OR POLICY_LOB=6) AND (BILL_TYPE_ID=11276  )) --OR BILL_TYPE_ID=  11278 or BILL_TYPE_ID=11276) ) CAHNGED BY PRAVESH ON 3 APRIL AS MORTEGAGEE SINCE INCEPTION WILL BE CONSIDERED NOW
  BEGIN
		IF(@BILL_MORTAGAGEE_COUNT>0)
			BEGIN
				SET @BILLMORTAGAGEE='N'
			END                     
	        ELSE
			BEGIN
				SET @BILLMORTAGAGEE='Y'
			END
 END 
 ELSE
			BEGIN
				SET @BILLMORTAGAGEE='N'
			END
SELECT                 
 @BILLMORTAGAGEE AS BILLMORTAGAGEE
END 







GO

