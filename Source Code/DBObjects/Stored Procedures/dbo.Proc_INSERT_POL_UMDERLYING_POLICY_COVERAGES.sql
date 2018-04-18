IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_INSERT_POL_UMDERLYING_POLICY_COVERAGES]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_INSERT_POL_UMDERLYING_POLICY_COVERAGES]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*  ----------------------------------------------------------              
Proc Name       : dbo.Proc_INSERT_POL_UMDERLYING_POLICY_COVERAGES          
Created by      : Ravindra        
Date            : 03-22-2006        
Purpose         : To Insert Data in  POL_UMBRELLA_UNDERLYING_POLICIES_COVERAGES          
Revison History :              
Used In         : Wolverine              

Reviewed By	:	Anurag verma
Reviewed On	:	12-07-2007
------------------------------------------------------------              
Date     Review By          Comments              
------   ------------       -------------------------             
*/   
--drop PROC dbo.Proc_INSERT_POL_UMDERLYING_POLICY_COVERAGES           
CREATE PROC dbo.Proc_INSERT_POL_UMDERLYING_POLICY_COVERAGES                                
(                                
 @CUSTOMER_ID     int,                                
 @POLICY_ID     int,                                
 @POLICY_VERSION_ID     smallint,                                
 @POLICY_NUMBER     varchar(75),                                
 @COVERAGE_DESC varchar(150),          
 @COVERAGE_AMOUNT varchar (100),          
 @POLICY_TEXT varchar(75),          
 @IS_POLICY bit,    
 @COV_CODE varchar(10),
 @POLICY_COMPANY nvarchar(150),
 @COVERAGE_TYPE nvarchar (10)=null         
)                                
AS                                
BEGIN                                
          
          
if exists(
	SELECT CUSTOMER_ID FROM  POL_UMBRELLA_UNDERLYING_POLICIES_COVERAGES  WITH(NOLOCK)     
		WHERE  CUSTOMER_ID=@CUSTOMER_ID      
		AND POLICY_ID=@POLICY_ID       
		AND POLICY_VERSION_ID=@POLICY_VERSION_ID      
		AND POLICY_NUMBER=@POLICY_NUMBER
		AND POLICY_COMPANY = @POLICY_COMPANY   
		 AND COV_CODE=@COV_CODE)
	return -2
          
--Insert values in POL_UMBRELLA_UNDERLYING_POLICIES_COVERAGES          
          
          
INSERT INTO POL_UMBRELLA_UNDERLYING_POLICIES_COVERAGES          
(          
 CUSTOMER_ID,                                
 POLICY_ID,                                
 POLICY_VERSION_ID,                                
 POLICY_NUMBER,          
 COVERAGE_DESC ,          
 COVERAGE_AMOUNT ,          
 POLICY_TEXT,      
 IS_POLICY,    
 COV_CODE,
 COVERAGE_TYPE,
POLICY_COMPANY          
)          
          
VALUES          
(          
 @CUSTOMER_ID ,                                
 @POLICY_ID ,                                
 @POLICY_VERSION_ID ,                                
 @POLICY_NUMBER  ,          
 @COVERAGE_DESC ,          
 @COVERAGE_AMOUNT ,          
 @POLICY_TEXT,      
 @IS_POLICY,    
 @COV_CODE,
 @COVERAGE_TYPE      ,
@POLICY_COMPANY
)          
                                
END                            
         


GO

