IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertCustomerNotes]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertCustomerNotes]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/* modify by : Pravesh   
   dated     : 9/11/2006  
 purpose     : correct todolist entry  
--drop proc Proc_InsertCustomerNotes    
  
Modified By : Anurag verma  
Modified On : 14/03/2007  
Purpose  : removing diary entry of pink slip from stored procedure to clscustomernotes.cs   
*/  
CREATE  PROC dbo.Proc_InsertCustomerNotes                              
( 
   @TO_FOLLOWUP_ID int=null,                             
   @CUSTOMER_ID  int,                      
   @NOTES_SUBJECT  nvarchar(255),                           
   @NOTES_TYPE       int,                  
   @POLICY_ID  smallint,                  
   @POLICY_VER_TRACKING_ID  smallint,             
   @CLAIMS_ID  int,                  
   @NOTES_DESC  text,                  
   @VISIBLE_TO_AGENCY nvarchar(1),                  
   @CREATED_BY  int,                  
   @CREATED_DATETIME datetime  ,           
   @LAST_UPDATED_DATETIME  datetime,           
   @NOTES_ID   int output ,       
   @QQ_APP_POL NVARCHAR(10)=null,    
   @DIARY_ITEM_REQ  char(1),    
   @FOLLOW_UP_DATE  datetime=null              
)                              
AS                              
BEGIN                  
   SELECT  @NOTES_ID = isnull(Max(NOTES_ID),0)+1 FROM CLT_CUSTOMER_NOTES                 
     INSERT INTO CLT_CUSTOMER_NOTES                              
     (
     TO_FOLLOWUP_ID,                    
     NOTES_ID,                  
     CUSTOMER_ID,                  
     NOTES_SUBJECT,                  
     NOTES_TYPE,                  
     POLICY_ID,            
     POLICY_VER_TRACKING_ID ,                 
     CLAIMS_ID,                    
     NOTES_DESC,                  
     VISIBLE_TO_AGENCY,                  
     CREATED_BY,                  
     CREATED_DATETIME ,          
     LAST_UPDATED_DATETIME   ,      
     QQ_APP_POL,    
     DIARY_ITEM_REQ,    
     FOLLOW_UP_DATE      
     )                              
     VALUES                              
     ( 
     @TO_FOLLOWUP_ID,                     
     @NOTES_ID,                          
     @CUSTOMER_ID,                  
     @NOTES_SUBJECT,                  
     @NOTES_TYPE,                  
     @POLICY_ID,            
     @POLICY_VER_TRACKING_ID   ,               
     @CLAIMS_ID,                  
     @NOTES_DESC,                  
     @VISIBLE_TO_AGENCY,                  
     @CREATED_BY,                  
     @CREATED_DATETIME,          
     @LAST_UPDATED_DATETIME    ,      
     @QQ_APP_POL,    
     @DIARY_ITEM_REQ,    
     @FOLLOW_UP_DATE                           
    )                     
      /*  
     IF (@NOTES_TYPE = 11932)  -- if it is "Pink Slip"        
     BEGIN        
        
  DECLARE @TEMP_POLICY_NUMBER char(10)            
  DECLARE @TEMP_UNDERWRITER int            
             
  SELECT @CUSTOMER_ID = CUSTOMER_ID, @POLICY_ID = POLICY_ID, @POLICY_VER_TRACKING_ID = POLICY_VERSION_ID        
         FROM CLM_CLAIM_INFO WHERE CLAIM_ID = @CLAIMS_ID         
         
  SELECT @TEMP_POLICY_NUMBER = POLICY_NUMBER, @TEMP_UNDERWRITER = UNDERWRITER            
  FROM POL_CUSTOMER_POLICY_LIST WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID         
         AND POLICY_VERSION_ID=@POLICY_VER_TRACKING_ID            
         
   INSERT INTO TODOLIST            
   (            
    RECBYSYSTEM,RECDATE,FOLLOWUPDATE,LISTTYPEID,POLICYCLIENTID,POLICYID,POLICYVERSION,            
    POLICYCARRIERID,POLICYBROKERID,SUBJECTLINE,LISTOPEN,SYSTEMFOLLOWUPID,PRIORITY,TOUSERID,            
    FROMUSERID,STARTTIME,ENDTIME,NOTE,PROPOSALVERSION,QUOTEID,CLAIMID,CLAIMMOVEMENTID,TOENTITYID,            
    FROMENTITYID,CUSTOMER_ID,APP_ID,APP_VERSION_ID,POLICY_ID,POLICY_VERSION_ID            
   )            
   VALUES        
   (            
    null,GETDATE(),GETDATE(),19,@CUSTOMER_ID,@POLICY_ID,            
    @POLICY_VER_TRACKING_ID,null,null,'A Pink Slip has been selected in Customer Notes','Y',            
    null,'M',@TEMP_UNDERWRITER,@CREATED_BY,null,null,null,null,null,null,null,null,null,            
    @CUSTOMER_ID,null,null,@POLICY_ID,@POLICY_VER_TRACKING_ID            
   )            
     END*/            
  
END     
        
    
        
        
        
        
        
          
          
        
        
        
        
        
      
    
  
  
  
  



GO

