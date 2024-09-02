using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Library_Management
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(strcon))
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();

                    // Use parameterized query to prevent SQL injection
                    SqlCommand cmd = new SqlCommand("SELECT * FROM admin_login WHERE member_id=@member_id AND password=@password", con);
                    cmd.Parameters.AddWithValue("@member_id", TextBox1.Text.Trim());
                    cmd.Parameters.AddWithValue("@password", TextBox2.Text.Trim());

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            //Response.Write("<script>alert('Welcome " + reader["member_id"].ToString() + "');</script>");
                            Session["username"] = reader.GetValue(0).ToString();
                            Session["full_name"] = reader.GetValue(2).ToString();
                            Session["role"] = "admin";
                        }
                        Response.Redirect(url : "homepage.aspx");
                    }
                    else
                    {
                        Response.Write("<script>alert('Invalid User Credentials');</script>");
                    }
                }
            }
            catch (Exception ex)
            {
                LogException(ex);
                Response.Write("<script>alert('An error occurred while processing your request. Please try again later.');</script>");
            }
        }

        private void LogException(Exception ex)
        {
            // Log detailed exception information
            string logPath = Server.MapPath("~/ErrorLog.txt");
            System.IO.File.AppendAllText(logPath, DateTime.Now.ToString() + " - " + ex.ToString() + Environment.NewLine);
        }
    }
}
