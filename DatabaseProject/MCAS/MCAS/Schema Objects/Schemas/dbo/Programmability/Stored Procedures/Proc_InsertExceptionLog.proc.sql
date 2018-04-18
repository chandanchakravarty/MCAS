CREATE PROCEDURE [dbo].[Proc_InsertExceptionLog]
	@exceptiondesc [text],
	@customer_id [int] = null,
	@accidentClaim_id [int] = null,
	@policy_id [int] = null,
	@policy_version_id [smallint] = null,
	@claim_id [int] = null,
	@entity_id [int] = null,
	@entity_type [varchar](20) = null,
	@source [varchar](50) = null,
	@message [nvarchar](1000) = null,
	@class_name [varchar](100) = null,
	@method_name [varchar](100) = null,
	@query_string_params [varchar](500) = null,
	@system_id [varchar](30) = null,
	@user_id [int] = null,
	@exception_type [varchar](100) = null
WITH EXECUTE AS CALLER
AS
begin  
 insert into EXCEPTIONLOG  
 (  
  exceptiondate,  
  exceptiondesc,  
  customer_id,  
  accidentClaim_id,  
  policy_id,  
  policy_version_id,  
  claim_id,  
  entity_id,
  entity_type, 
  source,  
  message,  
  class_name,  
  method_name,  
  query_string_params,  
  system_id,  
  user_id,  
  exception_type  
 )  
 values  
 (  
  getdate(),  
  @exceptiondesc,  
  @customer_id,  
  @accidentClaim_id,  
  @policy_id,  
  @policy_version_id,  
  @claim_id,  
  @claim_id,  
  @entity_id, 
  @source,  
  @message,  
  @class_name,  
  @method_name,  
  @query_string_params,  
  @system_id,  
  @user_id,  
  @exception_type  
 )  
end


