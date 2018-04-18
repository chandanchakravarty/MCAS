IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdatePolicyHomeAdditionalInterest]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdatePolicyHomeAdditionalInterest]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------            
Proc Name      : dbo.Proc_UpdatePolicyHomeAdditionalInterest        
Created by     : Vijay Arora          
Date           : 17-11-2005      
Purpose       : Update the Policy Home Owner Addtional Interest     
Revison History :        
Used In    : Wolverine               
 ------------------------------------------------------------                        
Date     Review By          Comments                      
------   ------------       -------------------------*/           
-- DROP PROC dbo.Proc_UpdatePolicyHomeAdditionalInterest        
CREATE    PROC [dbo].[Proc_UpdatePolicyHomeAdditionalInterest]        
(        
 @CUSTOMER_ID   INT,          
 @POLICY_ID     INT,          
 @POLICY_VERSION_ID   SMALLINT,          
 @HOLDER_ID     INT,          
 @DWELLING_ID    SMALLINT,          
 @MEMO     NVARCHAR(250),          
 @NATURE_OF_INTEREST  NVARCHAR(30),          
 @RANK     SMALLINT,          
 @LOAN_REF_NUMBER  NVARCHAR(75),          
 @MODIFIED_BY      INT,              
 @LAST_UPDATED_DATETIME DATETIME ,        
 @ADD_INT_ID   INT,        
 @HOLDER_NAME   NVARCHAR(70),          
 @HOLDER_ADD1   NVARCHAR(140),          
 @HOLDER_ADD2   NVARCHAR(140),          
 @HOLDER_CITY   NVARCHAR(80),          
 @HOLDER_COUNTRY  NVARCHAR(10),          
 @HOLDER_STATE   NVARCHAR(10),          
 @HOLDER_ZIP   VARCHAR(11),  
 @BILL_MORTAGAGEE SMALLINT = NULL  
)        
AS        
    
DECLARE @COUNT INT    
DECLARE @YES_LOOKUP_ID SMALLINT           
 DECLARE @TOTAL_YES_COUNT INT  
DECLARE @HOLDER_ID_EXIST INT           
set @YES_LOOKUP_ID = 10963   
    
 SELECT @COUNT = count(RANK)  from POL_HOME_OWNER_ADD_INT WHERE  CUSTOMER_ID = @CUSTOMER_ID AND                                
    POLICY_ID = @POLICY_ID AND                                
    POLICY_VERSION_ID = @POLICY_VERSION_ID                                
    and RANK=@RANK AND ADD_INT_ID <> @ADD_INT_ID       
    
                    
     IF @COUNT>0                                 
   BEGIN     
     RETURN -1                           
   END                          
 ELSE     
BEGIN  

IF @BILL_MORTAGAGEE = @YES_LOOKUP_ID  
	UPDATE POL_HOME_OWNER_ADD_INT SET  BILL_MORTAGAGEE = 10964  
	WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID AND                  
	DWELLING_ID = @DWELLING_ID 
SET @HOLDER_ID_EXIST=0
if ( @HOLDER_ID = 0 ) 
	begin
		SELECT  @HOLDER_ID_EXIST = HOLDER_ID FROM POL_HOME_OWNER_ADD_INT
			WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID AND                  
			DWELLING_ID = @DWELLING_ID AND ADD_INT_ID = @ADD_INT_ID  
		IF (ISNULL(@HOLDER_ID_EXIST,0)!=0)
			SET @HOLDER_ID=@HOLDER_ID_EXIST
	end

 IF ( @HOLDER_ID = 0 )        
 BEGIN        
  UPDATE POL_HOME_OWNER_ADD_INT        
  SET         
   MEMO=@MEMO,        
   NATURE_OF_INTEREST=@NATURE_OF_INTEREST,        
   RANK=@RANK,        
   LOAN_REF_NUMBER=@LOAN_REF_NUMBER,        
   MODIFIED_BY=@MODIFIED_BY,         
   LAST_UPDATED_DATETIME=@LAST_UPDATED_DATETIME,        
   HOLDER_NAME = @HOLDER_NAME,        
   HOLDER_ADD1 = @HOLDER_ADD1,        
   HOLDER_ADD2 = @HOLDER_ADD2,        
   HOLDER_CITY = @HOLDER_CITY,        
   HOLDER_COUNTRY = @HOLDER_COUNTRY,        
   HOLDER_STATE = @HOLDER_STATE,        
   HOLDER_ZIP = @HOLDER_ZIP ,
   BILL_MORTAGAGEE=@BILL_MORTAGAGEE  
  WHERE         
   CUSTOMER_ID  = @CUSTOMER_ID and        
   POLICY_ID   = @POLICY_ID and        
   POLICY_VERSION_ID = @POLICY_VERSION_ID and        
   ADD_INT_ID  = @ADD_INT_ID and        
   DWELLING_ID  = @DWELLING_ID      
   
	 IF EXISTS(SELECT CUSTOMER_ID FROM POL_HOME_OWNER_ADD_INT  WHERE CUSTOMER_ID = @CUSTOMER_ID AND                    
	   POLICY_ID = @POLICY_ID AND    POLICY_VERSION_ID = @POLICY_VERSION_ID AND                    
	   DWELLING_ID = @DWELLING_ID AND   ADD_INT_ID  = @ADD_INT_ID  AND ISNULL(IS_ACTIVE,'N')='Y')    
	  BEGIN
			set @TOTAL_YES_COUNT=0 --check if any yes add int to bill this to mortegagee if no Yes then update app list's dwellin id and add_int_id
			SELECT @TOTAL_YES_COUNT=COUNT(ADD_INT_ID) FROM POL_HOME_OWNER_ADD_INT  WHERE CUSTOMER_ID = @CUSTOMER_ID AND                    
							POLICY_ID = @POLICY_ID AND    POLICY_VERSION_ID = @POLICY_VERSION_ID
							AND DWELLING_ID = @DWELLING_ID AND  BILL_MORTAGAGEE=@YES_LOOKUP_ID
		  IF (@BILL_MORTAGAGEE = @YES_LOOKUP_ID)        
				UPDATE POL_CUSTOMER_POLICY_LIST SET DWELLING_ID=@DWELLING_ID,ADD_INT_ID=@ADD_INT_ID         
				WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID  
		  ELSE IF ( @TOTAL_YES_COUNT=0)
				UPDATE POL_CUSTOMER_POLICY_LIST SET DWELLING_ID=0,ADD_INT_ID=0         
				WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID   
			                        
	   END      
 END        
 ELSE        
 BEGIN        
  UPDATE POL_HOME_OWNER_ADD_INT        
  SET         
   MEMO=@MEMO,        
   NATURE_OF_INTEREST=@NATURE_OF_INTEREST,        
   RANK=@RANK,        
   LOAN_REF_NUMBER=@LOAN_REF_NUMBER,        
   MODIFIED_BY=@MODIFIED_BY,         
   LAST_UPDATED_DATETIME=@LAST_UPDATED_DATETIME,
    BILL_MORTAGAGEE=@BILL_MORTAGAGEE       
  WHERE         
   CUSTOMER_ID  = @CUSTOMER_ID and        
   POLICY_ID   = @POLICY_ID and        
   POLICY_VERSION_ID = @POLICY_VERSION_ID and        
   ADD_INT_ID  = @ADD_INT_ID and        
   DWELLING_ID  = @DWELLING_ID    
  
	IF EXISTS(SELECT CUSTOMER_ID FROM POL_HOME_OWNER_ADD_INT  WHERE CUSTOMER_ID = @CUSTOMER_ID AND                    
	   POLICY_ID = @POLICY_ID AND    POLICY_VERSION_ID = @POLICY_VERSION_ID AND                    
	   DWELLING_ID = @DWELLING_ID AND   ADD_INT_ID  = @ADD_INT_ID  AND ISNULL(IS_ACTIVE,'N')='Y')    
	 BEGIN 
			set @TOTAL_YES_COUNT=0 --check if any yes add int to bill this to mortegagee if no Yes then update app list's dwellin id and add_int_id
			SELECT @TOTAL_YES_COUNT=COUNT(ADD_INT_ID) FROM POL_HOME_OWNER_ADD_INT  WHERE CUSTOMER_ID = @CUSTOMER_ID AND                    
						POLICY_ID = @POLICY_ID AND    POLICY_VERSION_ID = @POLICY_VERSION_ID
						AND DWELLING_ID = @DWELLING_ID AND  BILL_MORTAGAGEE=@YES_LOOKUP_ID
			IF (@BILL_MORTAGAGEE = @YES_LOOKUP_ID)        
				UPDATE POL_CUSTOMER_POLICY_LIST SET DWELLING_ID=@DWELLING_ID,ADD_INT_ID=@ADD_INT_ID         
				WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID 
			ELSE IF ( @TOTAL_YES_COUNT=0)
				UPDATE POL_CUSTOMER_POLICY_LIST SET DWELLING_ID=0,ADD_INT_ID=0         
				WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID   
	  END             
          
       
          
  UPDATE MNT_HOLDER_INTEREST_LIST        
   SET HOLDER_ADD1 = @HOLDER_ADD1,        
    HOLDER_ADD2 = @HOLDER_ADD2,        
    HOLDER_CITY = @HOLDER_CITY,        
    HOLDER_COUNTRY = @HOLDER_COUNTRY,        
    HOLDER_STATE = @HOLDER_STATE,        
    HOLDER_ZIP = @HOLDER_ZIP        
   WHERE HOLDER_ID = @HOLDER_ID      

  IF @@ERROR <> 0        
  BEGIN         
   RETURN -4         
  END 
  
 END        
         
END        
        
    
    
    
    
    
  







GO

