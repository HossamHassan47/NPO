using NPO.Code;
using NPO.Code.Entity;
using NPO.Code.FilterEntity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;


namespace NPO.Code.Repository
{
    public class SiteRepository
    {
        public List<Site> GetAllSites()
        {
            return new List<Site>();
        }
        public DataTable GetSites(SiteFilter filter)
        {
            DataTable dataTable = new DataTable();
            //  var sql = "SELECT  [SiteCode],[SiteName],[SiteType],[2g],[3g],[4g],[ControlerId2g],[ControlerId3g] ,[ControlerId4g]FROM [NPODB].[dbo].[Site] WHERE (1 = 1) ";

            var sql = getSelectStatment(filter);
            using (SqlConnection sqlConnection = new SqlConnection(DBHelper.strConnString))
            {
                sqlConnection.Open();
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(sql, sqlConnection);
                sqlAdapter.Fill(dataTable);
            }

            return dataTable;
        }

        private string getSelectStatment(SiteFilter filter)
        {
            var sql = "SELECT  [SiteCode] ,[SiteName] FROM [NPODB].[dbo].[Site] WHERE (1 = 1) ";


            if (!string.IsNullOrEmpty(filter.SiteName))
            {
                sql += " AND SiteName LIKE '%" + filter.SiteName + "%'";
            }
            if (!string.IsNullOrEmpty(filter.SiteCode))
            {
                sql += " AND SiteCode LIKE '%" + filter.SiteCode + "%'";
            }
          
            return sql;
        }

        public int InsertNewSite(Site site)
        {
            int siteId = 0;
            // Insert ite into database then return the ID
            using (SqlConnection sqlConnection = new SqlConnection(DBHelper.strConnString))
            {
                SqlCommand sqlCommand = new SqlCommand();

                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlCommand.CommandText = "Sp_Site_Insert";

                sqlCommand.Parameters.Add("@SiteCode", SqlDbType.VarChar).Value = site.SiteCode;

                sqlCommand.Parameters.Add("@SiteName", SqlDbType.VarChar).Value = site.SiteName;

                sqlCommand.Parameters.Add("@RegionId", SqlDbType.Int).Value = site.RegionId;

                sqlCommand.Parameters.Add("@CityId", SqlDbType.Int).Value = site.CityId;

                sqlCommand.Parameters.Add("@ZoneId", SqlDbType.Int).Value = site.ZoneId;

                sqlCommand.Parameters.Add("@SiteType", SqlDbType.Int).Value = site.SiteType;

                sqlCommand.Parameters.Add("@2g", SqlDbType.Bit).Value = site._2g;

                sqlCommand.Parameters.Add("@3g", SqlDbType.Bit).Value = site._3g;

                sqlCommand.Parameters.Add("@4g", SqlDbType.Bit).Value = site._3g;

                sqlCommand.Parameters.Add("@ControlerId2g", SqlDbType.Int).Value = site.ControlerId2g;

                sqlCommand.Parameters.Add("@ControlerId3g", SqlDbType.Int).Value = site.ControlerId3g;

                sqlCommand.Parameters.Add("@ControlerId4g", SqlDbType.Int).Value = site.ControlerId4g;

                sqlCommand.Parameters.Add("@IsDeleted", SqlDbType.Bit).Value = site.IsDeleted;



                //Add the output parameter to the command object
                SqlParameter outPutParameter = new SqlParameter();
                outPutParameter.ParameterName = "@SiteId";
                outPutParameter.SqlDbType = SqlDbType.Int;
                outPutParameter.Direction = ParameterDirection.Output;
                sqlCommand.Parameters.Add(outPutParameter);

                sqlCommand.Connection = sqlConnection;

                try

                {
                    sqlConnection.Open();
                    sqlCommand.ExecuteNonQuery();
                    siteId = int.Parse(outPutParameter.Value.ToString());
                }

                catch (Exception ex)

                {

                    throw ex;

                }

                finally

                {

                    sqlConnection.Close();

                    sqlConnection.Dispose();
                }

            }
            // return the inserted user id
            return siteId;
        }




        public int UpdateSite(Site site)
        {
            // Update Site then return the ID
            return 0;
        }

        public bool DeleteSite(Site site)
        {
            // Delete Site
            return true;
        }
    }
}
