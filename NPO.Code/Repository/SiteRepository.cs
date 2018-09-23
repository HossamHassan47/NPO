using NPO.Code;
using NPO.Code.Entity;
using NPO.Code.FilterEntity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;

namespace NPO.Code.Repository
{
    public class SiteRepository
    {



        #region ObjectDataSource
        public static DataTable GetSites(SiteFilter filter, int maximumRows, int startRowIndex)

        {
            DataTable dataTable = new DataTable();
            var sql = @"
SELECT * 
from (
    SELECT ROW_NUMBER() OVER ( ORDER BY SiteId DESC ) ROWNUMBER,
           SiteCode ,
           SiteName,
           SiteId,
            [2g],
            [3g],
            [4g]
    FROM [dbo].[Site]  
    WHERE (1 = 1)  ";

            sql += getFilterString(filter);

            sql += ") As query where  query.ROWNUMBER > " + startRowIndex + " AND query.ROWNUMBER <= " + (startRowIndex + maximumRows);

            using (SqlConnection sqlConnection = new SqlConnection(DBHelper.strConnString))
            {
                sqlConnection.Open();
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(sql, sqlConnection);
                sqlAdapter.Fill(dataTable);

            }

            return dataTable;
        }


        public static int GetSitesCount(SiteFilter filter)
        {
            int count;

            var sql = "SELECT COUNT(*) FROM Site where (1=1)" + getFilterString(filter);


            using (SqlConnection sqlConnection = new SqlConnection(DBHelper.strConnString))
            {
                SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection);
                try
                {
                    sqlConnection.Open();
                    count = (int)sqlCommand.ExecuteScalar();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    sqlConnection.Close();
                }
            }
            return count;
        }

        private static string getFilterString(SiteFilter filter)
        {
            string sql = "";

            if (!string.IsNullOrEmpty(filter.SiteCode))
            {
                sql += " AND [SiteCode] LIKE '%" + filter.SiteCode + "%'";
            }
            if (!string.IsNullOrEmpty(filter.SiteName))
            {
                sql += " AND [SiteName] LIKE '%" + filter.SiteName + "%'";
            }
            if (!(filter.CityId == 0))
            {
                sql += " AND [CityId] LIKE '%" + filter.CityId + "%'";
            }
            if (filter._2g)
            {
                sql += " AND [2g] = " + 1;
            }
            if (filter._3g)
            {
                sql += " AND [3g] = " + 1;
            }
            if (filter._4g)
            {
                sql += " AND [4g] = " + 1;
            }


            return sql;
        }

        #endregion

        #region InsertUpdateDelete
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

                sqlCommand.Parameters.Add("@4g", SqlDbType.Bit).Value = site._4g;

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

                    return -1;

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

        public bool UpdateSite(Site site)
        {
            SqlCommand cmd = new SqlCommand();
            using (SqlConnection sqlConnection = new SqlConnection(DBHelper.strConnString))
            {
                cmd.Connection = sqlConnection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Sp_Site_Update";
                cmd.Parameters.Add("@SiteId", SqlDbType.NVarChar).Value = site.SiteId;
                cmd.Parameters.Add("@SiteName", SqlDbType.NVarChar).Value = site.SiteName;
                cmd.Parameters.Add("@SiteCode", SqlDbType.NVarChar).Value = site.SiteCode;
                cmd.Parameters.Add("@CityId", SqlDbType.Int).Value = site.CityId;
                cmd.Parameters.Add("@ZoneId", SqlDbType.Int).Value = site.ZoneId;
                cmd.Parameters.Add("@2g", SqlDbType.Bit).Value = site._2g;
                cmd.Parameters.Add("@3g", SqlDbType.Bit).Value = site._3g;
                cmd.Parameters.Add("@4g", SqlDbType.Bit).Value = site._4g;
                cmd.Parameters.Add("@SiteType", SqlDbType.Int).Value = site.SiteType;
                cmd.Parameters.Add("@ControlerId2g", SqlDbType.Int).Value = site.ControlerId2g;
                cmd.Parameters.Add("@ControlerId3g", SqlDbType.Int).Value = site.ControlerId3g;
                cmd.Parameters.Add("@ControlerId4g", SqlDbType.Int).Value = site.ControlerId4g;

                try
                {
                    sqlConnection.Open();
                    cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                    return true;
                }
                catch
                {
                    return false;
                }
            }

        }

        public bool DeleteSite(int siteId)
        {
            SqlCommand cmd = new SqlCommand();
            using (SqlConnection sqlConnection = new SqlConnection(DBHelper.strConnString))
            {
                cmd.Connection = sqlConnection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Sp_Site_Delete";
                cmd.Parameters.Add("@SiteId", SqlDbType.NVarChar).Value = siteId;
                try
                {
                    sqlConnection.Open();
                    cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        #endregion

        #region BindDDl

        // Tech Id To know 2G , 3G or 4G
        public DataTable GetControllers(int TechID)
        {

            using (SqlConnection conDatabase = new SqlConnection(DBHelper.strConnString))
            {
                SqlCommand com = new SqlCommand("select ControllerId, ControllerName from Controller where TechnologyId=@TechID;", conDatabase);
                com.Parameters.Add("@TechID", SqlDbType.Int).Value = TechID;
                com.Connection = conDatabase;
                conDatabase.Open();

                DataTable dataTable = new DataTable();
                SqlDataAdapter daa = new SqlDataAdapter(com);
                daa.Fill(dataTable);
                conDatabase.Close();

                return dataTable;
            }

        }

        public DataTable GetCities()
        {

            using (SqlConnection conDatabase = new SqlConnection(DBHelper.strConnString))
            {
                SqlCommand com = new SqlCommand(@"
                    Select CityName ,CityId
                    From City", conDatabase);
                com.Connection = conDatabase;
                conDatabase.Open();

                DataTable dataTable = new DataTable();
                SqlDataAdapter daa = new SqlDataAdapter(com);
                daa.Fill(dataTable);
                conDatabase.Close();

                return dataTable;
            }

        }

        public DataTable CityZone(int cityId)
        {

            using (SqlConnection conDatabase = new SqlConnection(DBHelper.strConnString))
            {
                SqlCommand com = new SqlCommand(@"select ZoneId , ZoneName from Zone Where CityId = " + cityId, conDatabase);
                com.Connection = conDatabase;
                conDatabase.Open();

                DataTable dataTable = new DataTable();
                SqlDataAdapter daa = new SqlDataAdapter(com);
                daa.Fill(dataTable);
                conDatabase.Close();

                return dataTable;
            }

        }

        #endregion

        public DataTable GetSites(SiteFilter siteFilter)
        {
            DataTable dataTable = new DataTable();
            var sql = "select * from [Site] ORDER BY SiteId DESC";

            using (SqlConnection sqlConnection = new SqlConnection(DBHelper.strConnString))
            {
                sqlConnection.Open();
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(sql, sqlConnection);
                sqlAdapter.Fill(dataTable);

            }

            return dataTable;
        }
        public List<Site> GetAllSites()
        {
            List<Site> liSites = new List<Site>();
            var sql = "select SiteId , SiteCode , SiteName  from [Site]";

            using (SqlConnection sqlConnection = new SqlConnection(DBHelper.strConnString))
            {
                sqlConnection.Open();
                SqlCommand sqlcomm = new SqlCommand(sql, sqlConnection);
                using (SqlDataReader reader = sqlcomm.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Site site = new Site();
                        site.SiteName = reader["SiteName"].ToString();
                        site.SiteCode = reader["SiteCode"].ToString();
                        site.SiteId = Convert.ToInt32(reader["SiteId"]);
                        liSites.Add(site);
                    }

                }
            }

            return liSites;
        }


        #region UploadExcel

        public int GetCOntrollerId_4G(string ControllerN)
        {
            var ControllerId = -1;
            var sql = "select ControllerId from Controller where ControllerName = @ControllerN ";

            using (SqlConnection con = new SqlConnection(DBHelper.strConnString))
            {
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@ControllerN", ControllerN);
                con.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        ControllerId = Convert.ToInt32(dr["ControllerId"]);
                    }

                }
            }
            if (ControllerId == -1)
            {
                var SqlInsert = "insert into Controller values(@ControllerN,4) Select SCOPE_IDENTITY()";
                using (SqlConnection con = new SqlConnection(DBHelper.strConnString))
                {
                    SqlCommand cmd = new SqlCommand(SqlInsert, con);
                    cmd.Parameters.AddWithValue("@ControllerN", ControllerN);

                    con.Open();

                    ControllerId = Convert.ToInt32(cmd.ExecuteScalar());

                }



            }

            return ControllerId;
        }

        public int GetCOntrollerId_3G(string ControllerN)
        {
            var ControllerId = -1;
            var sql = "select ControllerId from Controller where ControllerName = @ControllerN ";

            using (SqlConnection con = new SqlConnection(DBHelper.strConnString))
            {
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@ControllerN", ControllerN);
                con.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        ControllerId = Convert.ToInt32(dr["ControllerId"]);
                        return ControllerId;
                    }
                }
            }
            if (ControllerId == -1)
            {
                //ControllerRepository controllerRepository = new ControllerRepository();
                var SqlInsert = "insert into Controller values(@ControllerN,3) Select SCOPE_IDENTITY()";
                using (SqlConnection con = new SqlConnection(DBHelper.strConnString))
                {
                    SqlCommand cmd = new SqlCommand(SqlInsert, con);
                    cmd.Parameters.AddWithValue("@ControllerN", ControllerN);

                    con.Open();

                    ControllerId = Convert.ToInt32(cmd.ExecuteScalar());

                }



            }
            return ControllerId;
        }

        public int GetCOntrollerId_2G(string ControllerN)
        {
            var ControllerId = -1;
            var sql = "select ControllerId from Controller where ControllerName = @ControllerN ";

            using (SqlConnection con = new SqlConnection(DBHelper.strConnString))
            {
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@ControllerN", ControllerN);
                con.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        ControllerId = Convert.ToInt32(dr["ControllerId"]);
                        return ControllerId;
                    }
                }
            }
            if (ControllerId == -1)
            {
                //ControllerRepository controllerRepository = new ControllerRepository();
                var SqlInsert = "insert into Controller values(@ControllerN,2) Select SCOPE_IDENTITY() ";
                using (SqlConnection con = new SqlConnection(DBHelper.strConnString))
                {
                    SqlCommand cmd = new SqlCommand(SqlInsert, con);
                    cmd.Parameters.AddWithValue("@ControllerN", ControllerN);
                    con.Open();
                    ControllerId = Convert.ToInt32(cmd.ExecuteScalar());
                  
                }



            }
            return ControllerId;
        }

        public int GetTypeId(string Type)
        {
            var TypeId = -1;
            var sql = "select TypeId from SiteType where Type = @Type";

            using (SqlConnection con = new SqlConnection(DBHelper.strConnString))
            {
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@Type", Type);
                con.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        TypeId = Convert.ToInt32(dr["TypeId"]);
                    }
                }
            }

            return TypeId;
        }

        public int GetCityZoneId(string cityZoneName, int cityId)
        {
            var CityZoneid = -1;
            var sql = "select ZoneId from Zone where ZoneName = @cityZoneName And CityId = @cityId  ";

            using (SqlConnection con = new SqlConnection(DBHelper.strConnString))
            {
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@cityZoneName", cityZoneName);
                cmd.Parameters.AddWithValue("@cityId", cityId);

                con.Open();


                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        CityZoneid = Convert.ToInt32(dr["ZoneId"]);
                    }
                }
            }

            if (CityZoneid == -1)
            {
                var SqlInsert = "insert into Zone values (@cityZoneName,@cityId)  Select SCOPE_IDENTITY()";
                using (SqlConnection con = new SqlConnection(DBHelper.strConnString))
                {
                    SqlCommand cmd = new SqlCommand(SqlInsert, con);
                    cmd.Parameters.AddWithValue("@cityZoneName", cityZoneName);
                    cmd.Parameters.AddWithValue("@cityId", cityId);


                    con.Open();

                    CityZoneid = Convert.ToInt32(cmd.ExecuteScalar());
                }

            }

            return CityZoneid;
        }

        public int GetCityId(string CityN, int Regionid)
        {
            var Cityid = -1;
            var sql = "select CityId from City where CityName = @CityN ";

            using (SqlConnection con = new SqlConnection(DBHelper.strConnString))
            {
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@CityN", CityN);

                con.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        Cityid = Convert.ToInt32(dr["CityId"]);
                    }
                }
            }
            if (Cityid == -1)
            {
                var SqlInsert = "insert into City values (@CityN,@Regionid) Select SCOPE_IDENTITY() ";
                using (SqlConnection con = new SqlConnection(DBHelper.strConnString))
                {
                    SqlCommand cmd = new SqlCommand(SqlInsert, con);
                    cmd.Parameters.AddWithValue("@CityN", CityN);
                    cmd.Parameters.AddWithValue("@Regionid", Regionid);
                    con.Open();
                    Cityid =  Convert.ToInt32(cmd.ExecuteScalar());
                }
            }

            return Cityid;
        }

        public int GetRegionId(string RegionN)
        {
            var Regionid = -1;
            var sql = "select RegionId from Region where RegionName = @RegionN";

            using (SqlConnection con = new SqlConnection(DBHelper.strConnString))
            {
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@RegionN", RegionN);
                con.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        Regionid = Convert.ToInt32(dr["RegionId"]);
                    }
                }
            }
            return Regionid;
        }

        public int CheckSiteExist(string sitecode)
        {
            var siteid = -1;
            var sql = "select SiteId from [Site] where SiteCode = @sitecode";

            using (SqlConnection con = new SqlConnection(DBHelper.strConnString))
            {
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@sitecode", sitecode);
                con.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        siteid = Convert.ToInt32(dr["SiteId"]);
                    }
                }
            }
            return siteid;
        }
    }
    #endregion

}

