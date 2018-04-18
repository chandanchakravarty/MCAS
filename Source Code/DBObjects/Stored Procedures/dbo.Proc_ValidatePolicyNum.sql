IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_ValidatePolicyNum]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_ValidatePolicyNum]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--Text
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

/*----------------------------------------------------------          
Proc Name       : dbo.Proc_ValidatePolicyNum          
Created by      : Swastika         
Date            :       
Purpose         : Validates Policy number        
Revison History :          
Used In         : Wolverine       
Modfied and Reviewd : Pravene kasana
   
------------------------------------------------------------          
Date     Review By          Comments          
------   ------------       -------------------------*/   

/*
Cancelled by Recession - 02/20/2007                    
RESCIND

Cancelled
CANCEL 8

Cancelled - 01/11/2006                    
RESCIND 7

Cancellation in progress
UCANCL	 5

Marked for Cancellation             
SCANCEL 6

Rescind in progress
UDECPOL */

-- Proc_ValidatePolicyNum  'A1002292' , 5     
-- DROP Proc dbo.Proc_ValidatePolicyNum           
CREATE PROCEDURE dbo.Proc_ValidatePolicyNum        
(            
  @POLICY_NUMBER NVARCHAR(10),
  @OUTPUT int OUT      
)                
AS                     
        
BEGIN                          

  DECLARE @POLICY_ID smallint,@POLICY_VERSION_ID SMALLINT,@CUSTOMER_ID INT,@BILL_TYPE varchar(10)  
  SELECT     
  @POLICY_ID = IsNull(POLICY_ID,0), @POLICY_VERSION_ID= MAX(POLICY_VERSION_ID),@CUSTOMER_ID = CUSTOMER_ID    
  FROM POL_CUSTOMER_POLICY_LIST   
  WHERE POLICY_NUMBER = @POLICY_NUMBER-- AND BILL_TYPE = 'DB'    
  GROUP BY IsNull(POLICY_ID,0), IsNull(POLICY_ACCOUNT_STATUS,0), CUSTOMER_ID    
  
  if IsNull(@POLICY_ID,0) = 0     
   BEGIN    
   --Policy number is not valid, hence exiting with return status    
	 	  SET @OUTPUT = -4
		  RETURN @OUTPUT
   END  
  ELSE	 
   BEGIN 
	   SELECT @BILL_TYPE = BILL_TYPE FROM POL_CUSTOMER_POLICY_LIST WHERE POLICY_NUMBER = @POLICY_NUMBER AND POLICY_ID = @POLICY_ID AND 
			  POLICY_VERSION_ID = @POLICY_VERSION_ID 	
		 --Policy number is not DB type , hence exiting with return status   
	   IF(@BILL_TYPE <> 'DB')
	    BEGIN
		SET @OUTPUT =  -2
		RETURN @OUTPUT
	    END		
 	  else
		begin
		   DECLARE @POL_STATUS VARCHAR(100)
		   DECLARE @POL_CODE VARCHAR(20)
  
		   EXEC PROC_GETPOLICYDISPLAYSTATUS @CUSTOMER_ID,@POLICY_ID,@POLICY_VERSION_ID ,@POL_STATUS OUT,@POL_CODE OUT	
			 
		   if(@POL_CODE = 'UCANCL')
			begin
			SET @OUTPUT =  5
			RETURN @OUTPUT
			end	        	
		   if(@POL_CODE = 'SCANCEL')
			begin
			SET @OUTPUT =  6
			 RETURN @OUTPUT
			end
		   if(@POL_CODE = 'RESCIND')
			begin
			SET @OUTPUT =  7
			 RETURN @OUTPUT
			end
		   if (@POL_CODE = 'CANCEL')	 		    
			begin
			SET @OUTPUT =  8
			 RETURN @OUTPUT
			end
	            if (@POL_CODE = 'UDECPOL')	
			begin
			SET @OUTPUT =  9
			 RETURN @OUTPUT 
			end	 
		    else
		     begin
			SET @OUTPUT =  10
			RETURN @OUTPUT
		     end
	
					  
		  -- RETURN @OUTPUT
		   	
		end
--print @POL_STATUS
-- 	   SET @OUTPUT = 4
-- 	  RETURN @OUTPUT	
   	END
END      
  
-- Reinstate in progress
--    declare  @e varchar(100) 
--   exec Proc_ValidatePolicyNum  'A1002292', @e out
--  print @e



--select * from pol_customer_policy_list where policy_number='A1002292'

--   declare  @ps varchar(100) 
--   declare  @code varchar(100) 
--   exec Proc_GetPolicyDisplayStatus 1134,2,1 ,@ps out,@code out
--   print @ps
--   print @code
-- 









--10.0^6
GO

