IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertQuote_Pol]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertQuote_Pol]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/* ----------------------------------------------------------                          
 Proc Name      : dbo.Proc_InsertQuote_Pol                         
 Created by    : Ashwani                  
 Date           : 07 Mar. 2006                        
 Purpose       : Insert the record into QOT_CUSTOMER_QUOTE_LIST_POL_POL Table                        
 Revison History :                      
 Used In    :   Wolverine                                     
                    
 ------------------------------------------------------------                                      
 Date     Review By          Comments                                    
 -------------   ------------       -------------------------*/                                     
-- drop proc dbo.Proc_InsertQuote_Pol                                      
        
CREATE PROC [dbo].[Proc_InsertQuote_Pol]                      
(                     
 @CUSTOMER_ID int,                      
 @POLICY_ID int,                      
 @POLICY_VERSION_ID int,                      
 @QUOTE_TYPE nvarchar(12),                                 
 @QUOTE_DESCRIPTION varchar(125),                      
 @IS_ACCEPTED nchar(1)='N',                      
 @IS_ACTIVE nchar(1)='Y',                      
 @CREATED_BY int,                      
 @CREATED_DATETIME datetime,                      
 @QUOTE_XML text,                   
 @QUOTE_INPUT_XML text,              
 @QUOTE_ID int,      
 @RATE_EFFECTIVE_DATE datetime,    
 @BUSINESS_TYPE varchar(15),                            
 @QID int  output                      
)                      
AS                      
BEGIN                      
        
declare @QUOTE_VERSION_ID smallint                       
declare @QUOTE_NUMBER nvarchar(75)              
        
-- get the QUOTE_TYPe as LOB_CODE here               
declare @POLICY_LOB nvarchar(10)              
declare @LOB_CODE varchar(20)            
      
if(@QUOTE_TYPE <> 'HOME-BOAT')            
  BEGIN             
  select @POLICY_LOB=POLICY_LOB from POL_CUSTOMER_POLICY_LIST  WITH(NOLOCK)               
  where CUSTOMER_ID=@CUSTOMER_ID and POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID            
        
  select @QUOTE_TYPE=LOB_CODE from MNT_LOB_MASTER WITH(NOLOCK) where LOB_ID=@POLICY_LOB            
        
        
  delete from QOT_CUSTOMER_QUOTE_LIST_POL                   
  WHERE CUSTOMER_ID = @CUSTOMER_ID          
  AND POLICY_ID =@POLICY_ID         
  AND POLICY_VERSION_ID=@POLICY_VERSION_ID                     
  END       
  ELSE IF(@QUOTE_TYPE = 'HOME-BOAT')           
  BEGIN      
  DELETE FROM QOT_CUSTOMER_QUOTE_LIST_POL                
  WHERE CUSTOMER_ID = @CUSTOMER_ID        
  AND POLICY_ID =@POLICY_ID         
  AND POLICY_VERSION_ID=@POLICY_VERSION_ID       
  AND QUOTE_TYPE='HOME-BOAT'            
  END      
             
-----------------------------------------------------------------------------------      
        
      
select @QUOTE_ID = ISNULL(MAX(QUOTE_ID),0) + 1 from QOT_CUSTOMER_QUOTE_LIST_POL    WITH(NOLOCK)        
where CUSTOMER_ID=@CUSTOMER_ID and  POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID ;                   
      
select @QUOTE_VERSION_ID   = isnull(max(QUOTE_VERSION_ID),0)+1                     
from  QOT_CUSTOMER_QUOTE_LIST_POL    WITH(NOLOCK)                 
where CUSTOMER_ID=@CUSTOMER_ID and POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID and QUOTE_ID=@QUOTE_ID;                              
      
-- declare @QUOTE_NUMBER nvarchar(75)                          
select @QUOTE_NUMBER= 'Q-' + app_number                     
from POl_CUSTOMER_POLICY_LIST with(nolock)                   
where CUSTOMER_ID = @CUSTOMER_ID  and POLICY_ID =@POLICY_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID                 
                     
                      
INSERT INTO QOT_CUSTOMER_QUOTE_LIST_POL                   
(                      
CUSTOMER_ID,                      
QUOTE_ID,                      
QUOTE_VERSION_ID,                      
POLICY_ID,                      
POLICY_VERSION_ID,                      
QUOTE_TYPE,                      
QUOTE_NUMBER,                      
QUOTE_DESCRIPTION,           
IS_ACCEPTED,                      
IS_ACTIVE,               
CREATED_BY,                      
CREATED_DATETIME,                      
QUOTE_XML,                  
QUOTE_INPUT_XML,      
RATE_EFFECTIVE_DATE,    
BUSINESS_TYPE      
)                      
VALUES                      
(                       
@CUSTOMER_ID,                      
@QUOTE_ID,                   
@QUOTE_VERSION_ID,              
@POLICY_ID,                      
@POLICY_VERSION_ID,                      
@QUOTE_TYPE,                      
@QUOTE_NUMBER,                      
@QUOTE_DESCRIPTION,                      
@IS_ACCEPTED,              
@IS_ACTIVE,                      
@CREATED_BY,                      
@CREATED_DATETIME,                      
@QUOTE_XML ,               
@QUOTE_INPUT_XML,      
@RATE_EFFECTIVE_DATE,    
@BUSINESS_TYPE                      
)                      
set @QID= @QUOTE_ID                      
return @QUOTE_ID;                      
END     
  
  
GO

