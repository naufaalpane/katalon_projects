using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GPPSU.Models.OrderStatusListScreen
{
    public class OrderStatusListScreen
    {

		public string COMPANY_CD				{ get; set; }
		public long RECEIVE_NO					{ get; set; }
		public string STATUS_CD				{ get; set; }
		public string IMPORTER_CD 				{ get; set; }
		public string ORD_TYPE 				{ get; set; }
		public string EXPORTER_CD 				{ get; set; }
		public string PACK_MONTH 				{ get; set; }
		public string CFC 						{ get; set; }
		public string DISABLE_FLAG 			{ get; set; }
		public string TENTATIVE_REV 			{ get; set; }
		public string FIRM_REV 				{ get; set; }
		public string KEIHEN_REV 				{ get; set; }
		public string TOOL_CD 					{ get; set; }

		public string RECEIVE_DATE			{ get; set; }
		public string RECEIVE_TIME 			{ get; set; }

		public string LAST_UPDATE 				{ get; set; }
		public string LAST_UPDATE_DATE			{ get; set; }
		public string LAST_UPDATE_TIME			{ get; set; }

		public string RECEIVE_STAT				{ get; set; }
		public string RECEIVE_PROC_TIME 		{ get; set; }
		public string CHECK_STAT 				{ get; set; }
		public string CHECK_TIME 				{ get; set; }
		public string DIFF_LIST_STAT 			{ get; set; }
		public string DIFF_LIST_TIME			{ get; set; }
		public string DAY_BY_DAY_STAT 			{ get; set; }
		public string DAY_BY_DAY_TIME 			{ get; set; }
		public string K_PACK_CREATE_STAT		{ get; set; }
		public string K_PACK_CREATE_TIME		{ get; set; }
		public string M_PACK_CREATE_STAT		{ get; set; }
		public string M_PACK_CREATE_TIME		{ get; set; }
		public string END_PACK_MONTH			{ get; set; }
		public string ORD_CONST_K 				{ get; set; }
		public string ORD_CONST_M 				{ get; set; }
		public string ORD_CONST_C 				{ get; set; }
		public string ORD_CONST_F 				{ get; set; }
		public string ORD_CONST_W 				{ get; set; }
		public string CREATED_DT 				{ get; set; }
		public string CREATED_BY				{ get; set; }

		public string DEST_CODE { get; set; }
		public string DEST_NAME { get; set; }

		public string VERSION { get; set; }
	}
}