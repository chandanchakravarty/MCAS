CREATE PROCEDURE Proc_ProcessReminderMail
AS  
SET NOCOUNT ON        
      
   DECLARE   @EmailText [nvarchar](2000),    
             @EmailIdFrom NVARCHAR(100),  
             @EmailIdTo NVARCHAR(100),  
             @UserDispName NVARCHAR(20),  
             @ToUserId INT,  
             @FromUserId INT,  
             @EmailBody NVARCHAR(max),  
             @EscalationTo INT,  
             @ListId INT,  
             @emailSubject NVARCHAR(100),  
             @fromUserEmailId NVARCHAR(100),
             @estimatedCompletionDt NVARCHAR(50),
             @emailType NVARCHAR(50),
             @assignTo  NVARCHAR(50)  
  
  DECLARE CUR_ReminderMail CURSOR FOR        
   
   SELECT    ToUserId,FromUserId ,EmailBody,EscalationTo,ListId,subjectLine,STARTTIME  FROM  TODODIARYLIST (NOLOCK)  
             WHERE CONVERT(DATETIME,(CONVERT(varchar, STARTTIME,101))) = CONVERT(DATETIME,(CONVERT(varchar,GETDATE(),101))) 
             AND STARTTIME IS NOT NULL AND (LISTOPEN IS NULL OR LISTOPEN='Y')
             ORDER BY CreatedDate ASC   
               
    OPEN CUR_ReminderMail        
      
   BEGIN         
     FETCH NEXT FROM CUR_ReminderMail INTO  @ToUserId,@FromUserId ,@EmailBody,@EscalationTo,@ListId,@emailSubject,@estimatedCompletionDt       
       WHILE @@FETCH_STATUS = 0        
         BEGIN    
            SELECT @EmailIdFrom=EmailId FROM MNT_Users WHERE SNo=@ToUserId  
            SELECT @assignTo=UserDispName FROM MNT_Users WHERE SNo=@ToUserId
            SELECT @EmailIdTo=EmailId,@UserDispName=UserDispName FROM MNT_Users WHERE SNo=@EscalationTo 
            SELECT @estimatedCompletionDt= CONVERT(VARCHAR,Convert(datetime,@estimatedCompletionDt),103) 
            SELECT @EmailText='Alert will expires on ' +@estimatedCompletionDt+'<br>'+'Claims Officers: ' +@assignTo +'<br>'+ISNULL(@EmailBody,'') 
            SET @fromUserEmailId=(SELECT TOP 1 FromUserEmailId from MNT_SYS_PARAMS)
            SET @emailType='Reminder' +CAST('1' AS VARCHAR)
             IF NOT EXISTS(SELECT * FROM POL_EMAIL_SPOOL WHERE EmailType=@emailType AND SourceId=@ListId )
               BEGIN  
                   INSERT INTO POL_EMAIL_SPOOL   
                     (  
                       [EMAIL_FROM],[EMAIL_TO],[EMAIL_TEXT] ,[SENT_STATUS] ,[IsActive]  
                       ,[CreatedDate] ,[CreatedBy] ,[EmailSubject]
                       ,[SourceId],[SourceType] ,[EmailType] 
                     )     
                   VALUES (  
                            ISNULL(@EmailIdFrom,@fromUserEmailId) ,@EmailIdTo ,@EmailText ,'N',  
                            'Y' ,GETDATE(),'System Generated' ,@emailSubject 
                            ,@ListId,'Alert',@emailType
                          )    
               END  
                 
            FETCH NEXT FROM CUR_ReminderMail INTO @ToUserId,@FromUserId ,@EmailBody,@EscalationTo,@ListId,@emailSubject,@estimatedCompletionDt  
         END           
       
   END        
  CLOSE CUR_ReminderMail        
  DEALLOCATE CUR_ReminderMail   
  

    
  