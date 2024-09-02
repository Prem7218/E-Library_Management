using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;

namespace Library_Management
{
    public partial class userlogin : System.Web.UI.Page
    {
        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        // User Login.
        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(strcon))
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();

                    // Use parameterized query to prevent SQL injection
                    SqlCommand cmd = new SqlCommand("SELECT * FROM member_master WHERE member_id=@member_id AND password=@password", con);
                    cmd.Parameters.AddWithValue("@member_id", TextBox1.Text.Trim());
                    cmd.Parameters.AddWithValue("@password", TextBox2.Text.Trim());

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Response.Write("<script>alert('" + reader.GetValue(8).ToString() + "');</script>");
                            Session["username"] = reader.GetValue(8).ToString();
                            Session["full_name"] = reader.GetValue(0).ToString();
                            Session["role"] = "user";
                            Session["status"] = reader.GetValue(10).ToString();
                        }
                        Response.Redirect(url: "homepage.aspx");
                    }
                    else
                    {
                        Response.Write("<script>alert('Invalid User Credentials');</script>");
                    }
                }
            }
            catch (Exception ex)
            {
                // Consider logging the exception instead of displaying it to the user
                Response.Write("<script>alert('An error occurred: " + ex.Message + "');</script>");
            }
        }

        // User Sign UP.
        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Write("<script>alert('User Sign Up');</script>");
        }
    }
}
