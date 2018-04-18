IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetXMLDiff]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetXMLDiff]
GO

CREATE PROC [dbo].[Proc_GetXMLDiff] (@TranAuditId int)

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
  
  BEGIN TRY
  
    SELECT @XML1 = OldData  FROM MNT_TransactionAuditLog  WHERE TranAuditId = @TranAuditId
    SELECT @XML2 = NewData  FROM MNT_TransactionAuditLog  WHERE TranAuditId = @TranAuditId
    SELECT @Tname = @XML2.value('local-name(/*[1])','varchar(100)')from MNT_TransactionAuditLog WHERE TranAuditId  = @TranAuditId
    ;
    WITH XML1
    AS (SELECT
      T.N.value('local-name(.)', 'nvarchar(100)') AS NodeName,
      T.N.value('.', 'nvarchar(100)') AS Value
    FROM @XML1.nodes('/*[local-name(.)=sql:variable("@Tname")]/*, /*[local-name(.)=sql:variable("@Tname")]/@*') AS T (N)),
    XML2
    AS (SELECT
      T.N.value('local-name(.)', 'nvarchar(100)') AS NodeName,
      T.N.value('.', 'nvarchar(100)') AS Value
    FROM @XML2.nodes('/*[local-name(.)=sql:variable("@Tname")]/*, /*[local-name(.)=sql:variable("@Tname")]/@*') AS T (N))
    SELECT
      COALESCE(XML1.NodeName, XML2.NodeName) AS NodeName,
      XML1.Value AS OldVal,
      XML2.Value AS NewVal
    FROM XML1
    FULL OUTER JOIN XML2
      ON XML1.NodeName = XML2.NodeName
    WHERE COALESCE(XML1.Value, '') <> COALESCE(XML2.Value, '')
    
  END TRY
  
  BEGIN CATCH
  
    SELECT
      @ERROR_NUMBER = ERROR_NUMBER(),
      @ERROR_SEVERITY = ERROR_SEVERITY(),
      @ERROR_STATE = ERROR_STATE(),
      @ERROR_PROCEDURE = ERROR_PROCEDURE(),
      @ERROR_LINE = ERROR_LINE(),
      @ERROR_MESSAGE = ERROR_MESSAGE()

    SET @ERROR_MESSAGE_1 = 'Error Occured :' + ISNULL(@ERROR_PROCEDURE, '') + ' Error Severity :' + CONVERT(nvarchar, ISNULL(@ERROR_SEVERITY, '')) + ' Error State:' + CONVERT(nvarchar, ISNULL(@ERROR_STATE, '')) + ' Error Line Number:' + CONVERT(nvarchar, ISNULL(@ERROR_LINE, '')) + +' Error Description :' + ISNULL(@ERROR_MESSAGE, '')

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





GO


