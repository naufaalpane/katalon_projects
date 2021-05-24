select count(1) FROM TB_M_DESTINATION
where 1=1
	  and ((@DestCd is null OR @DestCd = '') OR (DEST_CD = @DestCd ))