	--------------------------------------------------------------- 7 A
insert into TB_R_ORDER_CONTROL (
COMPANY_CD																							
,RECEIVE_NO																					
,STATUS_CD																				
,IMPORTER_CD																						
,[ORD_TYPE]																					
,EXPORTER_CD																						
,PACK_MONTH																				
,CFC																							
,DISABLE_FLAG																			
,TENTATIVE_REV																			
,FIRM_REV																																		
,KEIHEN_REV																																	
,TOOL_CD																			
,RECEIVE_TIME																																						
,LAST_UPDATE																																					
,RECEIVE_STAT																																
,RECEIVE_PROC_TIME																																
,CHECK_STAT																			
,CHECK_TIME																			
,DIFF_LIST_STAT																			
,DIFF_LIST_TIME																			
,DAY_BY_DAY_STAT																			
,DAY_BY_DAY_TIME																			
,K_PACK_CREATE_STAT																			
,K_PACK_CREATE_TIME																			
,M_PACK_CREATE_STAT																			
,M_PACK_CREATE_TIME																			
,END_PACK_MONTH																										
,[ORD_CONST_K]																				
,[ORD_CONST_M]																				
,[ORD_CONST_C]																				
,[ORD_CONST_F]																				
,[ORD_CONST_W]																				
,CREATED_DT																												
,CHANGED_DT																																																		
,CREATED_BY																					
,CHANGED_BY	 )
select 
@Param_CompanyCD																	
,RCVNO																				
,'1'																					
,IMPORTERDCD										
,[ODRTYPE]																			
,[EXPORTERCD]																		
,[PCMN]																				
,CFC 																				
,'0'																					
,''																					
,case when [VERSION] = 'F' then  [REVISIONNO] else '' end							
,case when [VERSION] = 'K' then  [REVISIONNO] else '' end							
,'3'																					
,replace(left(replace(convert(varchar(max),getdate(),120) , '-', '')	,14),':',' ')	
,replace(left(replace(convert(varchar(max),getdate(),120) , '-', '')	,14),':',' ')	
,case when ERRORFLAG is null or ERRORFLAG = '' then 'OK' when ERRORFLAG = 'WARNING' then 'OK(War)' 	when 	ERRORFLAG = 'ERROR' then 'ERR' 	end [STAT_STS]
,replace(left(replace(convert(varchar(max),getdate(),120) , '-', '')	,14),':',' ')	
,''																					
,''																					
,''																					
,''																					
,''																					
,''																					
,''																					
,''																					
,''																					
,''																					
,convert(int,PCMN) + 3																
,''																					
,''																					
,''																					
,''																					
,''																					
,getdate()													
,NULL																				
, @username																			
,NULL		
from [dbo].[TB_T_EXPORT_ORDER_ATTACHMENT] where ERRORFLAG in ( 'ERROR', 'WARNING')
group by   IMPORTERDCD	, [ODRTYPE]		,		 [EXPORTERCD]		, CFC ,	[PCMN] , [RCVNO]		, [REVISIONNO]	,[VERSION] , ERRORFLAG


select 'SUCCESS' [RESULT]