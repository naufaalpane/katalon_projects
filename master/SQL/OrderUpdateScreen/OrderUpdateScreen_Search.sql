select * from (
select ROW_NUMBER() over(order by PART_NO asc ) r,
	   B.COMPANY_CD,
	   B.PART_NO,
	   B.RE_EXPORT_CD,
	   B.LOT_SIZE,
	   B.DETAIL_NO,
	   B.RECEIVE_NO,
	   C.YYMM,
	   

	   
	   C.DAY_ORD_VOL_01,
	   C.DAY_ORD_VOL_02,
	   C.DAY_ORD_VOL_03,
	   C.DAY_ORD_VOL_04,
	   C.DAY_ORD_VOL_05,
	   C.DAY_ORD_VOL_06,
	   C.DAY_ORD_VOL_07,
	   C.DAY_ORD_VOL_08,
	   C.DAY_ORD_VOL_09,
	   C.DAY_ORD_VOL_10,
	   C.DAY_ORD_VOL_11,
	   C.DAY_ORD_VOL_12,
	   C.DAY_ORD_VOL_13,
	   C.DAY_ORD_VOL_14,
	   C.DAY_ORD_VOL_15,
	   C.DAY_ORD_VOL_16,
	   C.DAY_ORD_VOL_17,
	   C.DAY_ORD_VOL_18,
	   C.DAY_ORD_VOL_19,
	   C.DAY_ORD_VOL_20,
	   C.DAY_ORD_VOL_21,
	   C.DAY_ORD_VOL_22,
	   C.DAY_ORD_VOL_23,
	   C.DAY_ORD_VOL_24,
	   C.DAY_ORD_VOL_25,
	   C.DAY_ORD_VOL_26,
	   C.DAY_ORD_VOL_27,
	   C.DAY_ORD_VOL_28,
	   C.DAY_ORD_VOL_29,
	   C.DAY_ORD_VOL_30,
	   C.DAY_ORD_VOL_31

from TB_R_ORDER_SPEC_RIO B
LEFT JOIN TB_R_ORDER_CONTROL_RIO A ON A.COMPANY_CD = B.COMPANY_CD AND A.RECEIVE_NO = B.RECEIVE_NO
LEFT JOIN TB_R_ORDER_MONTHLY C ON C.COMPANY_CD = B.COMPANY_CD AND C.RECEIVE_NO = B.RECEIVE_NO AND C.DETAIL_NO = B.DETAIL_NO
where 1=1
	  and ((@company_cd is null OR @company_cd = '') OR (A.COMPANY_CD = @company_cd ))
	  and ((@status_cd is null OR @status_cd = '') OR (A.STATUS_CD = @status_cd ))
	  and ((@importer_cd is null OR @importer_cd = '') OR (A.IMPORTER_CD = @importer_cd ))
	  and ((@exporter_cd is null OR @exporter_cd = '') OR (A.EXPORTER_CD = @exporter_cd ))
	  and ((@cfc is null OR @cfc = '') OR (A.CFC = @cfc ))
	  and ((@order_type is null OR @order_type = '') OR (A.ORD_TYPE = @order_type ))
	  and ((@pack_month is null OR @pack_month = '') OR (A.PACK_MONTH = @pack_month ))
	  and ((@re_export_cd is null OR @re_export_cd = '') OR (B.RE_EXPORT_CD = @re_export_cd ))
	  and ((@disable_flag is null OR @disable_flag = '') OR (A.DISABLE_FLAG = @disable_flag ))
	  and C.YYMM = @yymm
)
tb
where 1 = 1 and tb.r between @RowStart and @RowEnd