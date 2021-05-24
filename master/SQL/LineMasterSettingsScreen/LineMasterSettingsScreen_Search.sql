select * from (
select ROW_NUMBER() over(order by COMPANY_CD asc ) r,
	   COMPANY_CD        
	   ,LINE_CD        
      ,SEQ_NO
      ,convert(varchar(max), TC_FROM, 111)TC_FROM 
      ,convert(varchar(max), TC_TO, 111)TC_TO        
      ,LINE_NAME        
      ,PROCESS_CD
      ,CREATED_BY
      ,convert(varchar(max), CREATED_DT, 111) CREATED_DT
      ,CHANGED_BY
      ,convert(varchar(max), CHANGED_DT, 111) CHANGED_DT
	  ,convert(varchar(max), CREATED_DT, 111) CREATED_DT_STR
      ,convert(varchar(max), CHANGED_DT, 111) CHANGED_DT_STR
	 --,dbo.fn_FormatDate(CREATED_DT) as CREATED_DT_STR
	 --,dbo.fn_FormatDate(CHANGED_DT) as CHANGED_DT_STR  
from TB_M_LINE
where 1=1
	  and ((@PROCESS_CD is null OR @PROCESS_CD = '') OR (PROCESS_CD like '%' +@PROCESS_CD+'%' ))
	  and ((@LINE_CD is null OR @LINE_CD = '') OR (LINE_CD like '%' +@LINE_CD+'%' ))
)
tb
where 1 = 1 and tb.r between @RowStart and @RowEnd