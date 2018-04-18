IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetUMRule_Excesslimit_Endorsements_Pol]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetUMRule_Excesslimit_Endorsements_Pol]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/* ======================================================================================================      
PROC NAME                : DBO.Proc_GetUMRule_Excesslimit_Endorsements_Pol        
CREATED BY               : MANOJ RATHORE                                                                                                  
DATE                     : 29 MAY,2007                                                  
PURPOSE                  : TO GET EXCESS LIMITS & ENDORSEMENTS FOR UM RULES                                                  
REVISON HISTORY          :                                                                                                  
USED IN                  : WOLVERINE                                                                                                  
======================================================================================================      
DATE     REVIEW BY          COMMENTS                                                                                                  
=====  ==============   =============================================================================*/      
-- DROP PROC Proc_GetUMRule_Excesslimit_Endorsements_Pol    
CREATE PROC dbo.Proc_GetUMRule_Excesslimit_Endorsements_Pol  
(                                                                                                  
 @CUSTOMERID    int,                                                                                                                    
 @POLICYID         int,                                                                                                                    
 @POLICYVERSIONID   int                                                                                
)                                                                                                  
AS                                                                                                      
BEGIN     
-- MANDATORY ONLY       
 DECLARE @POLICY_LIMITS varchar(22)  
 DECLARE @TERRITORY varchar(22)  
 DECLARE @IS_RECORD_EXISTS CHAR  
 IF EXISTS (SELECT CUSTOMER_ID FROM POL_UMBRELLA_LIMITS                                      
 WHERE CUSTOMER_ID=@CUSTOMERID AND POLICY_ID= @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID)  
                                               
  BEGIN     
   SET @IS_RECORD_EXISTS='N'     
   SELECT     
   @POLICY_LIMITS=convert(varchar(22),ISNULL(POLICY_LIMITS,0)),@TERRITORY=convert(varchar(22),ISNULL(TERRITORY,0))    
   FROM  POL_UMBRELLA_LIMITS                                                 
   WHERE CUSTOMER_ID=@CUSTOMERID AND POLICY_ID= @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID                                                 
  END                                                  
 ELSE                                                  
  BEGIN                                                   
  SET @POLICY_LIMITS=0      
  SET @TERRITORY=0    
  set @IS_RECORD_EXISTS ='Y'    
  END      
       
                                                 
 SELECT    
 @POLICY_LIMITS AS POLICY_LIMITS,   
 @TERRITORY AS TERRITORY ,  
 @IS_RECORD_EXISTS as IS_RECORD_EXISTS        
       
END                                                                        
  


GO

