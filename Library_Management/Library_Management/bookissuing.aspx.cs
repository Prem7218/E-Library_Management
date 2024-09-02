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
    public partial class bookissuing : System.Web.UI.Page
    {
        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        //Issue Button....
        protected void ButtonID1_Click(object sender, EventArgs e)
        {
            if(IfMemberBookExist())
            {
                if(CheckIfIssuedBook())
                {
                    Response.Write("<script>alert('This Member Already has this book...');</script>");
                }
                else
                {
                    issueBook();
                }
            }
        }

        // Return Button....
        protected void Button2_Click(object sender, EventArgs e)
        {
            if (IfMemberBookExist())
            {
                if (CheckIfIssuedBook())
                {
                    ReturnBook();
                }
                else
                {
                    Response.Write("<script>alert('This Entery not exist....');</script>");
                }
            }
        }

        // Go Button...
        protected void Unnamed1_Click(object sender, EventArgs e)
        {
            GoDetails();
        }

        void issueBook()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(strcon))
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }

                    if (TextBox1.Text != "" && TextBox2.Text != "")
                    {
                        SqlCommand cmd = new SqlCommand("INSERT INTO book_issue (member_id, book_id, book_name, issue_date, due_date)" +
                            " VALUES (@MemberID, @BookID, @BookName, @IssueDate, @DueDate)", con);

                        cmd.Parameters.AddWithValue("@MemberID", TextBox1.Text.Trim());
                        cmd.Parameters.AddWithValue("@BookID", TextBox2.Text.Trim());
                        cmd.Parameters.AddWithValue("@BookName", TextBox4.Text.Trim());
                        cmd.Parameters.AddWithValue("@IssueDate", TextBox11.Text.Trim());
                        cmd.Parameters.AddWithValue("@DueDate", TextBox6.Text.Trim());

                        cmd.ExecuteNonQuery();

                        cmd = new SqlCommand("UPDATE book_master SET current_stock = current_stock - 1 WHERE book_id = @BookID", con);
                        cmd.Parameters.AddWithValue("@BookID", TextBox2.Text.Trim());

                        cmd.ExecuteNonQuery();

                        con.Close();
                        Response.Write("<script>alert('Book Issued successfully.');</script>");

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

        void ReturnBook()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(strcon))
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }

                    if (TextBox1.Text != "" && TextBox2.Text != "")
                    {
                        SqlCommand cmd = new SqlCommand("DELETE FROM book_issue WHERE book_id = @BookID AND member_id = @MemberID", con);
                        cmd.Parameters.AddWithValue("@BookID", TextBox2.Text.Trim());
                        cmd.Parameters.AddWithValue("@MemberID", TextBox1.Text.Trim());

                        int ans = cmd.ExecuteNonQuery();

                        if(ans > 0)
                        {
                            cmd = new SqlCommand("UPDATE book_master SET current_stock = current_stock + 1 WHERE book_id = @BookID", con);
                            cmd.Parameters.AddWithValue("@BookID", TextBox2.Text.Trim());
                            cmd.ExecuteNonQuery();
                            con.Close();
                        }

                        con.Close();
                        Response.Write("<script>alert('Book Returend successfully.');</script>");

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


        void GoDetails()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(strcon))
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }

                    SqlCommand cmd = new SqlCommand("SELECT book_name FROM book_master WHERE book_id = @BookID", con);
                    cmd.Parameters.AddWithValue("@BookID", TextBox2.Text.Trim());
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    if (dt.Rows.Count >= 1)
                    {
                        TextBox4.Text = dt.Rows[0]["book_name"].ToString();
                    }
                    else
                    {
                        Response.Write("<script>alert('Entered Book Id Is Wrong....');</script>");
                    }

                    cmd = new SqlCommand("SELECT full_name FROM member_master WHERE member_id = @MemberID", con);
                    cmd.Parameters.AddWithValue("@MemberID", TextBox1.Text.Trim());
                    da = new SqlDataAdapter(cmd);
                    dt = new DataTable();
                    da.Fill(dt);

                    if(dt.Rows.Count >= 1)
                    {
                        TextBox3.Text = dt.Rows[0]["full_name"].ToString();
                    }
                    else
                    {
                        Response.Write("<script>alert('Entered Member Id Is Wrong....');</script>");
                    }
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }

        bool IfMemberBookExist()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(strcon))
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }

                    SqlCommand cmd1 = new SqlCommand("SELECT COUNT(1) FROM member_master WHERE member_id = @MemberID", con);
                    cmd1.Parameters.AddWithValue("@MemberID", TextBox1.Text.Trim());
                    int memberExists = (int)cmd1.ExecuteScalar();

                    SqlCommand cmd2 = new SqlCommand("SELECT COUNT(1) FROM book_master WHERE book_id = @BookID AND current_stock > 0", con);
                    cmd2.Parameters.AddWithValue("@BookID", TextBox2.Text.Trim());
                    int bookExists = (int)cmd2.ExecuteScalar();

                    con.Close();

                    if (memberExists <= 0)
                    {
                        Response.Write("<script>alert('Entered Member ID Is Wrong Please Renter and Check again....');</script>");
                        return false;
                    }
                    else
                    {
                        if(bookExists <= 0)
                        {
                            Response.Write("<script>alert('Entered Book Id Is Wrong Please Renter and Check again....');</script>");
                            return false;
                        }
                        else
                        {
                            return true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
                return false;
            }
        }

        bool CheckIfIssuedBook()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(strcon))
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }

                    SqlCommand cmd2 = new SqlCommand("SELECT * FROM book_issue WHERE book_id = @BookID AND member_id = @MemberID", con);
                    cmd2.Parameters.AddWithValue("@BookID", TextBox2.Text.Trim());
                    cmd2.Parameters.AddWithValue("@MemberID",TextBox1.Text.Trim());

                    SqlDataAdapter da = new SqlDataAdapter(cmd2);
                    DataTable dt = new DataTable(); 
                    da.Fill(dt);
                    con.Close();
                    return dt.Rows.Count >= 1;
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
                return false;
            }
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    //Check your condition here
                    DateTime dt = Convert.ToDateTime(e.Row.Cells[5].Text);
                    DateTime today = DateTime.Today;
                    if (today > dt)
                    {
                        e.Row.BackColor = System.Drawing.Color.PaleVioletRed;
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }
    }
}
