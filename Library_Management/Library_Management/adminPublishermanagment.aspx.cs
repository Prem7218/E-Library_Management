using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;

namespace Library_Management
{
    public partial class adminPublishermanagment : System.Web.UI.Page
    {
        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GridView1.DataBind();
            }
        }

        // Add Details
        protected void Button1_Click(object sender, EventArgs e)
        {
            if (CheckPublisherExist())
            {
                Response.Write("<script>alert('Publisher with entered ID already exists. You cannot add another publisher with the same ID.');</script>");
            }
            else
            {
                AddDetails();
            }
        }

        // Update Details
        protected void Button2_Click(object sender, EventArgs e)
        {
            if (CheckPublisherExist())
            {
                UpdateDetails();
            }
            else
            {
                Response.Write("<script>alert('Publisher does not exist.');</script>");
            }
        }

        // Delete Details
        protected void Button3_Click(object sender, EventArgs e)
        {
            if (CheckPublisherExist())
            {
                DeleteDetails();
            }
            else
            {
                Response.Write("<script>alert('Publisher does not exist.');</script>");
            }
        }

        // Go Details
        protected void Button4_Click(object sender, EventArgs e)
        {
            GoToDetails();
        }

        private bool CheckPublisherExist()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(strcon))
                {
                    if (con.State != ConnectionState.Open)
                    {
                        con.Open();
                    }

                    SqlCommand cmd = new SqlCommand("SELECT * FROM publisher_master WHERE publisher_id = @PublisherID", con);
                    cmd.Parameters.AddWithValue("@PublisherID", TextBox1.Text.Trim());
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    return dataTable.Rows.Count >= 1;
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
                return false;
            }
        }

        private void AddDetails()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(strcon))
                {
                    if (con.State != ConnectionState.Open)
                    {
                        con.Open();
                    }

                    if (TextBox1.Text != "" && TextBox2.Text != "")
                    {
                        SqlCommand cmd = new SqlCommand("INSERT INTO publisher_master (publisher_id, publisher_name) VALUES (@PublisherID, @PublisherName)", con);

                        cmd.Parameters.AddWithValue("@PublisherID", TextBox1.Text.Trim());
                        cmd.Parameters.AddWithValue("@PublisherName", TextBox2.Text.Trim());

                        cmd.ExecuteNonQuery();
                        Response.Write("<script>alert('Publisher added successfully.');</script>");
                        ClearForm();

                        GridView1.DataBind();
                    }
                    else
                    {
                        Response.Write("<script>alert('Please Fill Both Details Then Add Publisher Details..');</script>");
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }

        private void UpdateDetails()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(strcon))
                {
                    if (con.State != ConnectionState.Open)
                    {
                        con.Open();
                    }

                    SqlCommand cmd = new SqlCommand("UPDATE publisher_master SET publisher_name = @PublisherName WHERE publisher_id = @PublisherID", con);
                    cmd.Parameters.AddWithValue("@PublisherID", TextBox1.Text.Trim());
                    cmd.Parameters.AddWithValue("@PublisherName", TextBox2.Text.Trim());

                    cmd.ExecuteNonQuery();
                    Response.Write("<script>alert('Publisher details updated successfully.');</script>");
                    ClearForm();

                    GridView1.DataBind();
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }

        private void DeleteDetails()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(strcon))
                {
                    if (con.State != ConnectionState.Open)
                    {
                        con.Open();
                    }

                    SqlCommand cmd = new SqlCommand("DELETE FROM publisher_master WHERE publisher_id = @PublisherID", con);
                    cmd.Parameters.AddWithValue("@PublisherID", TextBox1.Text.Trim());

                    cmd.ExecuteNonQuery();
                    Response.Write("<script>alert('Publisher deleted successfully.');</script>");
                    ClearForm();

                    GridView1.DataBind();
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
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

                    SqlCommand cmd = new SqlCommand("SELECT * FROM publisher_master WHERE publisher_id = @PublisherID", con);
                    cmd.Parameters.AddWithValue("@PublisherID", TextBox1.Text.Trim());

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    if (dt.Rows.Count >= 1)
                    {
                        TextBox2.Text = dt.Rows[0][1].ToString();
                    }
                    else
                    {
                        Response.Write("<script>alert('Invalid Publisher ID');</script>");
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
        }
    }
}
