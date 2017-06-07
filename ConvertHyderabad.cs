using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.IO;
using System.Configuration;

namespace HyderabadService
{
    class ConvertHyderabad
    {
        static public void Convert()
        {   //catalog name in databse   
            var catalog = ConfigurationManager.AppSettings["catalog"];
            // Directory where output files are stored. Output file name present in app.config
            var outputDirectory = ConfigurationManager.AppSettings["outputdirectory"];
            var dataSource = ConfigurationManager.AppSettings["datasource"];
            Logger.Log("started conversion of hyderabad files");
            if (Directory.Exists(outputDirectory))
            {    
                Logger.Log("output directory exists" + outputDirectory);
                string outputfilepath = string.Format(outputDirectory + "\\outputhyderabad{0:MMMM_dd_yyyy-hh_mm_ss__tt}.xls", DateTime.Now);
                List<List<String>> records = new List<List<string>>();
                //connection string
                string connection = string.Format(@"Data Source=" + dataSource + ";Initial Catalog=" + catalog + ";Integrated Security=True");
                SqlConnection myConnection = new SqlConnection(connection);
               // open connection
                myConnection.Open();
                try
                {
                    Logger.Log("connetion openeed");
                    SqlDataReader recordsReader = null;
                    // query to select desired columns from the database
                    SqlCommand myCommand = new SqlCommand(" select FirstName, MiddleName, LastName, Ecode, Indate, OutDate, InTimeH, InTimeM, OutTimeH, OutTimeM  from ATTENDANCE_WITHOUT_SHIFT inner join EmployeeMaster on EmployeeMaster.EmployeeCode = ATTENDANCE_WITHOUT_SHIFT.Ecode", myConnection);
                    //executes command
                    recordsReader = myCommand.ExecuteReader();
                    // headerrecord that contains names of columns
                    List<String> headerRecord = new List<string>();
                    String[] listcolumnnames = { "SNo", "Location", "Department", "EmpCode", "Name", "InDate", "InTime", "OutDate", "OutTime", "CtrlCompany", "Emp-Local" };
                    // add columnames to headerrecord
                    foreach (string columnname in listcolumnnames)
                        headerRecord.Add(columnname);
                    records.Add(headerRecord);
                    //id is the one that gives serial no. to records
                    int id = 1;
                    while (recordsReader.Read())
                    {
                        String location = "hyderabad";
                        String department = "hyderabad";
                        String ctrlCompany = "Accolite-hyderabad";
                        String EmpLocal = "Accolite-hyderabad";
                        String Outtime = recordsReader["OutTimeH"] + ":" + recordsReader["OutTimeM"];
                        String Intime = recordsReader["InTimeH"] + ":" + recordsReader["InTimeM"];
                        String Name = recordsReader["FirstName"] + "." + recordsReader["MiddleName"] + "."+ recordsReader["LastName"];
                        List<String> record = new List<string>();
                        // adds data to record in the desired column
                        record.Add(id.ToString());
                        record.Add(location);
                        record.Add(department);
                        record.Add(recordsReader["Ecode"].ToString());
                        record.Add(Name);
                        record.Add(recordsReader["Indate"].ToString());
                        record.Add(Intime);
                        record.Add(recordsReader["Outdate"].ToString());
                        record.Add(Outtime);
                        record.Add(ctrlCompany);
                        record.Add(EmpLocal);
                        // add record to the list of records
                        records.Add(record);
                        id++;

                    }
                    //it writes all records to the outputfile
                    File.WriteAllLines(outputfilepath, records.Select(x => string.Join(";", x)));
                    Logger.Log("completed conversion of hyderabad files");
                    Logger.Log("Output file " + outputfilepath + " Generated consisting of attendance data of Hyderabad employees");

                }
                catch (Exception ex)
                {
                    string subject = "Regarding Failure While Converting Attendance files of Hyderabad ";
                    string body = "Unable To convert Hyderabad Files during " + DateTime.Now.ToString() + ".Exception is " + ex;
                    // Sends mail to concerned person when conversion of files doesn't happen for the desired day
                    SendEmail.Send("vishalporwar95@gmail.com", subject, body);
                    Logger.Log("Unable to Convert Hyderabad Files\n Exception is " + ex);
                }
                try
                {
                    Logger.Log("connetion Closed");

                    myConnection.Close();
                }
                catch (Exception ex)
                {
                    Logger.Log("Unable to Close the Sql Connection \n Exception is " + ex);
                }
            }
            else
            {
                Logger.Log("Directory" + outputDirectory + " not exists");
            }
        }
    }
}


