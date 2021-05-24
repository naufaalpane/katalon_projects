/************************************************************************************************
 * Program History : 
 * 
 * Project Name     : IPPCS (Procurement Control System)
 * Client Name      : PT. TMMIN (Toyota Manufacturing Motor Indonesia)
 * Function Id      : 
 * Function Name    : 
 * Function Group   : 
 * Program Id       : 
 * Program Name     : 
 * Program Type     : Console Application
 * Description      : This Console is used for Common Batch IPPCS.
 * Environment      : .NET 4.0, ASP MVC 4.0
 * Author           : FID.Ridwan
 * Version          : 01.00.00
 * Creation Date    : 10/09/2020 16.10.00
 *                                                                                                          *
 * Update history		Re-fix date				Person in charge				Description					*
 *
 * Copyright(C) 2020 - . All Rights Reserved                                                                                              
 *************************************************************************************************/

using NQCEkanbanDataSending.Helper.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
namespace NQCEkanbanDataSending
{
    class Program
    {
        
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("NQC E-Kanban Data Sending is started");
                //string functionId = args[0];//"TransferPostingtoICS";
                string functionId = "E_KanbanDataSending";
                Assembly assembly = typeof(Program).Assembly; // in the same assembly!

                Type type = assembly.GetType("NQCEkanbanDataSending.AppCode." + functionId);
                BaseBatch batch = (BaseBatch)Activator.CreateInstance(type);
                batch.ExecuteBatch();
                Console.WriteLine("NQC E-Kanban Data Sending is ended");
                //NQCEkanbanDataSending.AppCode.TPIPPCStoICS baru = new AppCode.TPIPPCStoICS();
                //baru.ExecuteBatch();

                //TPIPPCStoICS1 TP = new TPIPPCStoICS1();
                //TP.ExecuteBatchTP();
                Thread.Sleep(7000);
            }
            catch (Exception AE)
            {
                Console.WriteLine(AE.Message);
                Console.WriteLine("NQC E-Kanban Data Sending is ended");
                Thread.Sleep(7000);
            }
        }
    }
}
