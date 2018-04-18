/****** Object:  StoredProcedure [dbo].[Proc_InsertExceptionLog]    Script Date: 07/22/2014 15:58:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

CREATE procedure [dbo].[Proc_InsertExceptionLog]  
(  
@exceptiondesc text,  
@customer_id int=null,  
@app_id int=null,  
@app_version_id smallint=null,  
@policy_id int=null,  
@policy_version_id smallint=null,  
@claim_id int=null,  
@qq_id int=null,  
@source varchar(50)=null,  
@message varchar(500)=null,  
@class_name varchar(100)=null,  
@method_name varchar(100)=null,  
@query_string_params varchar(500)=null,  
@system_id varchar(30)=null,  
@user_id int = null,  
@lob_id int = null,  
@exception_type varchar(100)=null  
)  
as  
begin  
 insert into EXCEPTIONLOG  
 (  
  exceptiondate,  
  exceptiondesc,  
  customer_id,  
  app_id,  
  app_version_id,  
  policy_id,  
  policy_version_id,  
  claim_id,  
  qq_id,  
  source,  
  message,  
  class_name,  
  method_name,  
  query_string_params,  
  system_id,  
  user_id,  
  lob_id,  
  exception_type  
  
 )  
 values  
 (  
  getdate(),  
  @exceptiondesc,  
  @customer_id,  
  @app_id,  
  @app_version_id,  
  @policy_id,  
  @policy_version_id,  
  @claim_id,  
  @qq_id,  
  @source,  
  @message,  
  @class_name,  
  @method_name,  
  @query_string_params,  
  @system_id,  
  @user_id,  
  @lob_id,  
  @exception_type  
 )  
end
GO
