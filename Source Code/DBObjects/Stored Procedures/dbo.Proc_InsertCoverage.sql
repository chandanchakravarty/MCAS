IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertCoverage]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertCoverage]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------              
Proc Name          : Dbo.Proc_InsertCoverage              
Created by           : Mohit Gupta              
Date                    : 19/05/2005              
Purpose               :               
Revison History :              
Used In                :   Wolverine                
              
Modify date  : 03- oct-2005              
purpose   : Added @INCLUDED int,  @COVERAGE_TYPE nchar(10)     @RANK int,      @IS_MANDATORY int              
Modify By : Gaurav              
        
Modify By : Pravesh K. Chandel        
Modify date  : 17 April 2007              
purpose   : Add default Endorsemet If Coverage Typeis Endorsement Coverage      
  
Modify By : Pravesh K. Chandel        
Modify date  : 1 Aug 2007              
purpose   : generate cov_id greater than 10000 if added from maintenance  
  
Modify By : Pravesh K. Chandel        
Modify date  : 14 Jan 2008              
purpose   : Add new Field Is_system_generated  
  
Reviewed By : Anurag Verma  
Reviewed On : 25-06-2007  

Modify By : Praveen Kumar     
Modify date  : 19/08/2010                
purpose   : Add new Fields DISPLAY_ON_CLAIM ,CLAIM_RESERVE_APPLY

Modify By : aNKIT    
Modify date  :26 OCT 2010           
purpose   : Add IS_MAIN

Modify By : Abhinav  
Modify date  :8 Nov 2010           
purpose   : Add Sub_lob_id

Modify By : Shikha  
Modify date  : 30 May 2011          
purpose   : Add COV_TYPE_ABBR,SUSEP_COV_CODE
------------------------------------------------------------              
Date     Review By          Comments              
------   ------------       -------------------------*/              
--drop proc Proc_InsertCoverage              
CREATE   PROCEDURE [dbo].[Proc_InsertCoverage]                
(                 
 @COV_ID int output,                
 @COV_REF_CODE varchar(10),                
 @COV_CODE varchar(10),                
 @COV_DES varchar(250),                
 @STATE_ID int,                
 @LOB_ID int,                
 @IS_ACTIVE nchar,                
 @IS_DEFAULT bit,                
 @TYPE nchar,                
 @PURPOSE nchar,                
 @LIMIT_TYPE nchar,                
 @DEDUCTIBLE_TYPE nchar,                
 @IsLimitApplicable nchar,                
 @IsDeductApplicable nchar,                
 @INCLUDED int,                
 @COVERAGE_TYPE nchar(10),                
 @RANK int,                
 @IS_MANDATORY int,                
 @EFFECTIVE_FROM_DATE datetime,                
 @EFFECTIVE_TO_DATE datetime,                 
 @DISABLED_DATE datetime,               
 @REINSURANCE_LOB int,              
 @REINSURANCE_COV int,              
 @ASLOB int,              
 @REINSURANCE_CALC int,    
 @COMM_VEHICLE int,    
 @COMM_REIN_COV_CAT int,    
 @REIN_ASLOB int,    
 @COMM_CALC int,    
 @REIN_REPORT_BUCK int,    
 @REIN_REPORT_BUCK_COMM int,            
 @ISADDDEDUCTIBLE_APP nchar   ,        
 @ADDDEDUCTIBLE_TYPE nchar,           
 @ENDORSMENT_ID   INT  =null OUTPUT,          
 @FORM_NUMBER varchar(20)   ,    
 @IS_SYSTEM_GENERAED         char(1)='Y',   
 @MANDATORY_DATE DATETIME =NULL,  
 @NON_MANDATORY_DATE DATETIME=NULL,  
 @DEFAULT_DATE DATETIME=NULL,  
 @NON_DEFAULT_DATE DATETIME=NULL,  
 @DISPLAY_ON_CLAIM int, 
 @CLAIM_RESERVE_APPLY int,
 @IS_MAIN nchar(1),
 @SUB_LOB_ID int,
 @COV_TYPE_ABBR nvarchar(10) = NULL,
 @SUSEP_COV_CODE numeric(4,0) = NULL 
)                
AS                
BEGIN                
    
--by pravesh on 1-Aug-2007 generate Cov_id greater then 10000 if added through Maintenance set up    
SELECT @COV_ID = IsNull(Max(COV_ID),10000) + 1 FROM MNT_COVERAGE  where   COV_ID > 10000    
--SELECT @COV_ID = IsNull(Max(COV_ID),0) + 1 FROM MNT_COVERAGE      
INSERT INTO  MNT_COVERAGE                
(                
COV_ID,COV_REF_CODE,COV_CODE,COV_DES,STATE_ID,LOB_ID,IS_ACTIVE, IS_DEFAULT,TYPE,PURPOSE,LIMIT_TYPE,DEDUCTIBLE_TYPE,IsLimitApplicable,IsDeductApplicable,INCLUDED ,COVERAGE_TYPE                 
,RANK,IS_MANDATORY,EFFECTIVE_FROM_DATE,EFFECTIVE_TO_DATE,DISABLED_DATE,REINSURANCE_LOB,REINSURANCE_COV,ASLOB,REINSURANCE_CALC,ISADDDEDUCTIBLE_APP,ADDDEDUCTIBLE_TYPE,FORM_NUMBER,    
COMM_VEHICLE,COMM_REIN_COV_CAT,REIN_ASLOB,COMM_CALC,REIN_REPORT_BUCK,REIN_REPORT_BUCK_COMM,IS_SYSTEM_GENERAED,MANDATORY_DATE,NON_MANDATORY_DATE,DEFAULT_DATE,NON_DEFAULT_DATE
,DISPLAY_ON_CLAIM,CLAIM_RESERVE_APPLY,IS_MAIN,SUB_LOB_ID,COV_TYPE_ABBR,SUSEP_COV_CODE)
VALUES                
(                
@COV_ID,@COV_REF_CODE,@COV_CODE,@COV_DES,@STATE_ID,@LOB_ID,@IS_ACTIVE,@IS_DEFAULT,@TYPE,@PURPOSE,@LIMIT_TYPE,@DEDUCTIBLE_TYPE,@IsLimitApplicable,@IsDeductApplicable,@INCLUDED ,@COVERAGE_TYPE         
,@RANK,@IS_MANDATORY,@EFFECTIVE_FROM_DATE,@EFFECTIVE_TO_DATE,@DISABLED_DATE,@REINSURANCE_LOB,@REINSURANCE_COV,@ASLOB,@REINSURANCE_CALC,@ISADDDEDUCTIBLE_APP,@ADDDEDUCTIBLE_TYPE,@FORM_NUMBER,    
@COMM_VEHICLE,@COMM_REIN_COV_CAT,@REIN_ASLOB,@COMM_CALC,@REIN_REPORT_BUCK,@REIN_REPORT_BUCK_COMM,@IS_SYSTEM_GENERAED,@MANDATORY_DATE,@NON_MANDATORY_DATE,@DEFAULT_DATE,@NON_DEFAULT_DATE 
,@DISPLAY_ON_CLAIM ,@CLAIM_RESERVE_APPLY,@IS_MAIN,@SUB_LOB_ID,@COV_TYPE_ABBR,@SUSEP_COV_CODE 
)                
          
--added by Pravesh ot add Associated Endorsement if Coverage Type is Endorsement Coverage          
if  (@TYPE ='2')          
begin          
declare @TMP_ENDORSMENT_ID int          
declare @CREATED_DATETIME datetime          
declare @ENDO_TYPE CHAR(1)          
select @CREATED_DATETIME=getdate()          
IF (@IS_MANDATORY='1')          
 SET @ENDO_TYPE='M'          
ELSE          
 SET @ENDO_TYPE='O'          
exec Proc_InsertEndorsmentDetails          
 @TMP_ENDORSMENT_ID out,                  
 @STATE_ID,           
 @LOB_ID,                  
 @PURPOSE ,                  
 @ENDO_TYPE , --@TYPE                   
 @COV_DES,   --@DESCRIPTION                   
 '',              
 @COV_CODE,  --@ENDORSEMENT_CODE           
 'Y' , --@ENDORS_ASSOC_COVERAGE               
 @COV_ID,  --@SELECT_COVERAGE            
 'N'   ,   --@ENDORS_PRINT nchar(1),                  
 @EFFECTIVE_FROM_DATE ,          
 @EFFECTIVE_TO_DATE ,          
 @DISABLED_DATE ,          
 @IS_ACTIVE     ,          
 0,  --@CREATED_BY              
 @CREATED_DATETIME  ,          
 '',  --@FORM_NUMBER           
 null  --@EDITION_DATE           
          
SET @ENDORSMENT_ID=@TMP_ENDORSMENT_ID          
end          
      
exec PROC_REPLICATE_MASTER 'COV_ID',@COV_ID,'MNT_COVERAGE','MNT_COVERAGE_MULTILINGUAL','COV_DES'  
          
END                
             

    
GO

