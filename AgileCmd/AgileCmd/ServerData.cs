using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace AgileCmd
{
    public class ServerData
    {
        List<DataRow> data;

        /* redo most of this *********************************************************************************/

        public ServerData()
        {
            string connectionString = null;
            SqlConnection conn;

            connectionString = "Server = tcp:agileuniprojectserver.database.windows.net,1433; " +
                "Initial Catalog = AgileDB; " +
                "Persist Security Info = False; " +
                "User ID = tsween; " +
                "Password =RapidR0lls; " +
                "MultipleActiveResultSets = False; " +
                "Encrypt = True; " +
                "TrustServerCertificate = False; " +
                "Connection Timeout = 30;";

            conn = new SqlConnection(connectionString);

            try
            {
                data = new List<DataRow>();
                //List<Address> address = new List<Address>();

                using (conn)
                {
                    string oString = "Select * from dbo.Medical_Procedure"; // alter to use stored proc 
                    SqlCommand oCommand = new SqlCommand(oString, conn);
                    conn.Open();

                    using (SqlDataReader oReader = oCommand.ExecuteReader())
                    {
                        while (oReader.Read())
                        {
                            String definition = oReader["drg_definition"].ToString();
                            String providerID = oReader["provider_id"].ToString();
                            String providerName = oReader["provider_name"].ToString();
                            String street = oReader["provider_street_address"].ToString();
                            String city = oReader["provider_city"].ToString();
                            String state = oReader["provider_state"].ToString();
                            String zip = oReader["provider_zip"].ToString();
                            String reference = oReader["hospital_referral_region"].ToString();
                            long discharge = Convert.ToInt64(oReader["total_discharge"].ToString());
                            double cost = Convert.ToDouble(oReader["provider_id"].ToString());

                            Address add = new Address(street, city, state, zip);
                            DataRow dt = new DataRow(definition, providerID, providerName, add, reference, discharge, cost);

                            data.Add(dt);
                        }
                    }

                }

                conn.Close();
            }
            catch (Exception ex)
            {

            }


        }

        public List<DataRow> Data { get => data; set => data = value; }

    }
}
