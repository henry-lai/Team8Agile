using LocationsAndRouting;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;


namespace AgileCmd
{
    // stablish connection with the server
    public class ServerData
    {
        public List<DataRow> data { get; set; }
      //  List<DataRow> data;
        string connectionString = null;
        SqlConnection conn;

        public ServerData()
        {
            // connection string to stablish connection with the database
            connectionString = "Server = tcp:agileuniprojectserver.database.windows.net,1433; " +
                "Initial Catalog = AgileDB; " +
                "Persist Security Info = False; " +
                "User ID = tsween; " +
                "Password =RapidR0lls; " +
                "MultipleActiveResultSets = False; " +
                "Encrypt = True; " +
                "TrustServerCertificate = False; " +
                "Connection Timeout = 30;";
        }

        // pull out data from the database and return result as a List
        public List<DataRow> ReadDatabase(string searchItem, Dictionary<string,double> Filters) {
            conn = new SqlConnection(connectionString);

            try
            {
                data = new List<DataRow>();

                using (conn)
                {
                    string spName = @"dbo.[uspSearchForProcedure]";

                    //define the SqlCommand object
                    SqlCommand oCommand = new SqlCommand(spName, conn);

                    //Set SqlParameter - the user input parameter value will be set from the command line
                    SqlParameter param1 = new SqlParameter();
                    param1.ParameterName = "UserInput";
                    param1.SqlDbType = SqlDbType.Text;
                    param1.Value = searchItem;

                    //add the parameter to the SqlCommand object
                    oCommand.Parameters.Add(param1);
                    conn.Open();

                    oCommand.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader oReader = oCommand.ExecuteReader())
                    {
                        
                        while (oReader.Read())
                        {
                            if (Convert.ToDouble(oReader["average_total_payments"].ToString()) <= Filters["MaxCost"]) {
                                String definition = oReader["drg_definition"].ToString();
                                String providerID = oReader["provider_id"].ToString();
                                String providerName = oReader["provider_name"].ToString();
                                String street = oReader["provider_street_address"].ToString();
                                String city = oReader["provider_city"].ToString();
                                String state = oReader["provider_state"].ToString();
                                String zip = oReader["provider_zip"].ToString();
                                String reference = oReader["hospital_referral_region"].ToString();
                                int discharge = Convert.ToInt32(oReader["total_discharges"].ToString());
                                Double cost = Convert.ToDouble(oReader["average_total_payments"].ToString());

                                // create hospital object
                                Address add = new Address(street, city, state, zip);
                                DataRow dt = new DataRow(definition, providerID, providerName, add, reference, discharge, cost, 0);

                                
                                // adding search results in a list
                                data.Add(dt);
                            }

                        }
                    }
                }
                conn.Close(); // close the database connection
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return data;

        }
    }
}
