using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;

namespace Library_Management
{
    public partial class Adminauthormanagment : System.Web.UI.Page
    {
        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GridView1.DataBind();
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (CheckAuthorIdExist())
            {
                Response.Write("<script>alert('Author with entered ID already exists. You cannot add another author with the same ID.');</script>");
            }
            else
            {
                AddNewAuthor();
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            if (CheckAuthorIdExist())
            {
                UpdateAuthorDetails();
            }
            else
            {
                Response.Write("<script>alert('Author does not exist.');</script>");
            }
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            if (CheckAuthorIdExist())
            {
                DeleteAuthorDetail();
            }
            else
            {
                Response.Write("<script>alert('Author does not exist.');</script>");
            }
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            GetAuthorById();
        }

        bool CheckAuthorIdExist()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(strcon))
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }

                    SqlCommand cmd = new SqlCommand("SELECT * FROM author_master WHERE author_id=@Author_id", con);
                    cmd.Parameters.AddWithValue("@Author_id", TextBox1.Text.Trim());

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    return dt.Rows.Count >= 1;
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
                return false;
            }
        }

        void AddNewAuthor()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(strcon))
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }

                    if(TextBox1.Text != "" && TextBox2.Text != "")
                    {
                        SqlCommand cmd = new SqlCommand("INSERT INTO author_master (author_id, author_name) VALUES (@AuthorID, @AuthorName)", con);
                        cmd.Parameters.AddWithValue("@AuthorID", TextBox1.Text.Trim());
                        cmd.Parameters.AddWithValue("@AuthorName", TextBox2.Text.Trim());

                        cmd.ExecuteNonQuery();
                        Response.Write("<script>alert('Author added successfully.');</script>");
                        ClearForm();

                        GridView1.DataBind();
                    }
                    else
                    {
                        Response.Write("<script>alert('Please Fill Both Details Then Add Author Details..');</script>");
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }

        void UpdateAuthorDetails()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(strcon))
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }

                    SqlCommand cmd = new SqlCommand("UPDATE author_master SET author_name = @AuthorName WHERE author_id=@AuthorID", con);
                    cmd.Parameters.AddWithValue("@AuthorID", TextBox1.Text.Trim());
                    cmd.Parameters.AddWithValue("@AuthorName", TextBox2.Text.Trim());

                    cmd.ExecuteNonQuery();
                    Response.Write("<script>alert('Author details updated successfully.');</script>");
                    ClearForm();

                    GridView1.DataBind();
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }

        void DeleteAuthorDetail()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(strcon))
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }

                    SqlCommand cmd = new SqlCommand("DELETE FROM author_master WHERE author_id = @AuthorID", con);
                    cmd.Parameters.AddWithValue("@AuthorID", TextBox1.Text.Trim());

                    cmd.ExecuteNonQuery();
                    Response.Write("<script>alert('Author deleted successfully.');</script>");
                    ClearForm();

                    GridView1.DataBind();
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }

        void GetAuthorById()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(strcon))
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }

                    SqlCommand cmd = new SqlCommand("SELECT * FROM author_master WHERE author_id=@AuthorID", con);
                    cmd.Parameters.AddWithValue("@AuthorID", TextBox1.Text.Trim());

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    if (dt.Rows.Count >= 1)
                    {
                        TextBox2.Text = dt.Rows[0][1].ToString();
                    }
                    else
                    {
                        Response.Write("<script>alert('Invalid Author ID');</script>");
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
