CREATE PROC [dbo].[Proc_GetXMLDiff]    
(    
 @TranAuditId int          
 )     
AS        
  DECLARE @XML1 xml        
  DECLARE @XML2 xml        
  DECLARE @Tname nvarchar(50)        
  DECLARE @ERROR_NUMBER int        
  DECLARE @ERROR_SEVERITY int        
  DECLARE @ERROR_STATE int        
  DECLARE @ERROR_PROCEDURE varchar(512)        
  DECLARE @ERROR_LINE int        
  DECLARE @ERROR_MESSAGE nvarchar(max)        
  DECLARE @ERROR_MESSAGE_1 nvarchar(max)        
  DECLARE @IsRowExists bit;     
  BEGIN TRY        
      SELECT @XML1 = ChangedColumns  FROM MNT_TransactionAuditLog  WHERE TranAuditId = @TranAuditId   
      SET @IsRowExists =@XML1.exist('//Row')    
      IF(@IsRowExists=1)  
        BEGIN  
          SELECT     
          xmlData.Col.value('@field','varchar(max)')As NodeName    
          ,xmlData.Col.value('@OldValue','varchar(max)') As  OldVal    
          ,xmlData.Col.value('@NewValue','varchar(max)') As  NewVal    
          FROM @XML1.nodes('//ChangeXml//Row//Column') xmlData(Col);  
        END  
      ELSE  
        BEGIN  
          SELECT       
          xmlData.Col.value('@field','varchar(max)')As NodeName      
         ,xmlData.Col.value('@OldValue','varchar(max)') As  OldVal      
         ,xmlData.Col.value('@NewValue','varchar(max)') As  NewVal      
         FROM @XML1.nodes('//ChangeXml/Column') xmlData(Col);  
        END  
           
  END TRY        
          
  BEGIN CATCH        
          
    SELECT        
      @ERROR_NUMBER = ERROR_NUMBER(),        
      @ERROR_SEVERITY = ERROR_SEVERITY(),        
      @ERROR_STATE = ERROR_STATE(),        
      @ERROR_PROCEDURE = ERROR_PROCEDURE(),        
      @ERROR_LINE = ERROR_LINE(),        
      @ERROR_MESSAGE = ERROR_MESSAGE()        
        
    SET @ERROR_MESSAGE_1 = 'Error Occured :' + ISNULL(@ERROR_PROCEDURE, '') + ' Error Severity :' + CONVERT(nvarchar, ISNULL(@ERROR_SEVERITY, '')) + ' Error State:' + CONVERT(nvarchar, ISNULL(@ERROR_STATE, '')) + ' Error Line Number:' + CONVERT(nvarchar, 
  
    
      
ISNULL(@ERROR_LINE, '')) + +' Error Description :' + ISNULL(@ERROR_MESSAGE, '')        
        
    EXEC dbo.Proc_InsertExceptionLog @exceptiondesc = @ERROR_MESSAGE_1,        
                                     @customer_id = NULL,        
                                     @app_id = NULL,        
                                     @app_version_id = NULL,        
                                     @policy_id = NULL,        
                                     @policy_version_id = NULL,        
                                     @claim_id = NULL,        
                                     @qq_id = NULL,        
                                     @source = @ERROR_PROCEDURE,        
                                     @message = @ERROR_MESSAGE_1,        
                                     @class_name = NULL,        
                                     @method_name = NULL,        
                                     @query_string_params = NULL,        
                                     @system_id = NULL,        
                                     @user_id = NULL,        
                                     @lob_id = NULL,        
                                     @exception_type = 'SqlException'        
        
  END CATCH        