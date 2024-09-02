using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Library_Management
{
    public partial class adminusermanagment : System.Web.UI.Page
    {
        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GridView1.DataBind();
            }
        }

        // Go Button....
        protected void LinkButton4_Click(object sender, EventArgs e)
        {
            GoToDetails();
        }

        // Green Button....
        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            if(IfMemberExist())
            {
                UpdateMemberFunction("active");
            }
            else
            {
                Response.Write("<script>alert('Invalid Member ID');</script>");
            }
        }

        // Yellow Button....
        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            if(IfMemberExist())
            {
                UpdateMemberFunction("pending");
            }
            else
            {
                Response.Write("<script>alert('Invalid Member ID');</script>");
            }
        }

        // Red Button....
        protected void LinkButton3_Click(object sender, EventArgs e)
        {
            if(IfMemberExist())
            {
                UpdateMemberFunction("deactive");
            }
            else
            {
                Response.Write("<script>alert('Invalid Member ID');</script>");
            }
        }

        // Delete Member Details....
        protected void Button3_Click(object sender, EventArgs e)
        {
            if(IfMemberExist())
            {
                DeleteAllDetails();
            }
            else
            {
                Response.Write("<script>alert('Invalid Member ID');</script>");
            }
        }

        private bool IfMemberExist()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(strcon))
                {
                    if (con.State != System.Data.ConnectionState.Open)
                    {
                        con.Open();
                    }

                    SqlCommand cmd = new SqlCommand("SELECT * FROM member_master WHERE member_id = @MemberID", con);
                    cmd.Parameters.AddWithValue("@MemberID", TextBox2.Text.Trim());
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    return dt.Rows.Count >= 1;
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
                return false;
            }
        }

        private void GoToDetails()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(strcon))
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }

                    SqlCommand cmd = new SqlCommand("SELECT * FROM member_master WHERE member_id = @MemberID", con);
                    cmd.Parameters.AddWithValue("@MemberID", TextBox2.Text.Trim());

                    SqlDataReader dr = cmd.ExecuteReader();

                    if (dr.Read())
                    {
                        TextBox1.Text = dr.GetValue(0).ToString();  // Full Name....
                        TextBox11.Text = dr.GetValue(10).ToString();  // Account Status....
                        TextBox12.Text = dr.GetValue(3).ToString(); // Email ID.....
                        TextBox4.Text = dr.GetValue(1).ToString();  // DOB..... 
                        TextBox3.Text  = dr.GetValue(2).ToString();  // Contact ID....
                        DropDownList1.Text = dr.GetValue(4).ToString(); // State.....
                        TextBox6.Text  = dr.GetValue(5).ToString();  // City.....
                        TextBox5.Text  = dr.GetValue(6).ToString();  // Pin Code....
                        TextBox7.Text  = dr.GetValue(7).ToString(); // Address......
                    }
                    else
                    {
                        Response.Write("<script>alert('Invalid Member ID');</script>");
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }

        void UpdateMemberFunction(string status)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(strcon))
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }

                    SqlCommand cmd = new SqlCommand("UPDATE member_master SET account_status ='" +
                      status + "' WHERE member_id ='" + TextBox2.Text.Trim() + "'", con);

                    cmd.ExecuteNonQuery();
                    con.Close();

                    ClearForm();
                    Response.Write("<script>alert('Member Status Updated);</script>");
                    GridView1.DataBind(); 

                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }

        private void DeleteAllDetails()
        {
            try
            {
                using(SqlConnection con = new SqlConnection(strcon))
                {
                    if(con.State == ConnectionState.Closed)
                    {
                        con.Open() ;
                    }

                    if (TextBox11.Text == "deactive")
                    {
                        SqlCommand cmd = new SqlCommand("DELETE FROM member_master WHERE member_id = @MemberID", con);
                        cmd.Parameters.AddWithValue("@MemberID", TextBox2.Text.Trim());

                        cmd.ExecuteNonQuery();
                        con.Close();
                        Response.Write("<script>alert('Publisher deleted successfully.');</script>");
                        ClearForm();

                        GridView1.DataBind();
                    }
                    else
                    {
                        Response.Write("<script>alert('Members who are Active or Pending cannot be deleted.');</script>");
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }

        private void ClearForm()
        {
            TextBox1.Text = "";
            TextBox2.Text = "";
            TextBox3.Text = "";
            TextBox4.Text = "";
            TextBox5.Text = "";
            TextBox6.Text = "";
            TextBox7.Text = "";
            TextBox11.Text = "";
            TextBox12.Text = "";
            DropDownList1.Text = "";
        }
    }
}
