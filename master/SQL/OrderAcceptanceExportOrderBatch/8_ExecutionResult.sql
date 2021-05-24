
declare @@Param_CompanyCd varchar(max) = @CompanyCd
declare @@Param_OkorErr varchar(max) = @OkOrErr
declare @@username varchar(max) = @username

if not exists (select * from TB_R_BATCH_STATUS where COMPANY_CD =  @@Param_CompanyCd and BATCH_ID = 'U01-002' )
begin
insert into TB_R_BATCH_STATUS 
(COMPANY_CD , BATCH_ID , CLASS , BATCH_NAME , STATUS , START_TIME , CREATED_DT, CREATED_BY)
values
(@@Param_CompanyCd , 'U01-002' , 'Order Rec. TEN-Pack' , 'Order Rec. TEN-Pack', @@Param_OkorErr , getdate(), getdate(),@@username)
end
else

begin
update TB_R_BATCH_STATUS set STATUS = @@Param_OkorErr , CHANGED_BY = @@username , CHANGED_DT = getdate() where COMPANY_CD =  @@Param_CompanyCd and BATCH_ID = 'U01-002'
end
