select * from (
select ROW_NUMBER() over(order by COMPANY_CD asc ) r,
	   COMPANY_CD        
	   ,DEST_CD as DESTINATION_CODE        
      ,SEQ_NO 
      ,convert(varchar(max), TC_FROM, 112) TC_FROM
      ,convert(varchar(max), TC_TO, 112) TC_TO      
      ,DEST_NAME as DESTINATION_NAME       
      ,EXPORT_CD as EXPORT_CODE
      ,CAST(LEAD_TIME AS INT) [LEAD_TIME]
	  ,EKANBAN_CD as E_KANBAN
      ,CREATED_BY
      ,CREATED_DT
      ,CHANGED_BY
      ,CHANGED_DT
	  ,convert(varchar(max), CREATED_DT, 112) CREATED_DT_STR
      ,convert(varchar(max), CHANGED_DT, 112) CHANGED_DT_STR 
from TB_M_DESTINATION
where 1=1
	  and ((@DestCd is null OR @DestCd = '') OR (DEST_CD = @DestCd  ))
)
tb
where 1 = 1 and tb.r between @RowStart and @RowEnd