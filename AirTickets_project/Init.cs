using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirTickets_project
{
    public static class Init
    {
        private static SqlParameter[] Fill(int[] value)
        {
            SqlParameter[] sqls = new SqlParameter[3];
            sqls[0] = new SqlParameter("@FlightsId", value[0]);
            sqls[2] = new SqlParameter("@Cost", value[1]);
            sqls[1] = new SqlParameter("@TypeId", value[2]);
            return sqls;
        }
        public static void Initialize()
        {
            using (SqlConnection cn = new SqlConnection("Server=DESKTOP-1LJ593T\\SQLEXPRESS; Database=Air_Tickets; Trusted_Connection=true"))
            {
                cn.Open();
                List<int> Flights_ = new List<int>();
                SqlCommand cmd = new SqlCommand(@"Select * from Flights", cn);
                using (SqlDataReader rd = cmd.ExecuteReader())
                {
                    while (rd.Read())

                        Flights_.Add((int)rd["Flights_Id"]);
                }
                foreach (var id in Flights_)
                {
                    SqlCommand cm = cn.CreateCommand();
                    cm.Parameters.Add(new SqlParameter("@id", id));
                    cm.CommandText = @"Select count(1) from Tickets where Tickets.Flights_Id=@id";
                    int res = (int)cm.ExecuteScalar();
                    if (res == 0)
                    {
                        for (int i = 0; i < 300; i++)
                        {
                            SqlCommand ins = new SqlCommand(@"Insert into Tickets values ( @FlightsId, @TypeId, @Cost, null)", cn);
                            ins.Parameters.AddRange(Fill(new int[] { id, i < 150 ? 100 : i<250? 400:600, i<150?0:i<250?1:2 }));
                            ins.ExecuteNonQuery();
                        }
                    }
                }
            }
        }
    }
}