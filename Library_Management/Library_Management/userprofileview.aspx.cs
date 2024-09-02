using System;
using System.Data.SqlClient;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

namespace Library_Management
{
    public partial class userprofileview : System.Web.UI.Page
    {
        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(Session["username"]?.ToString()))
                {
                    Response.Write("<script>alert('Session Expired. Please login again.');</script>");
                    Response.Redirect("userlogin.aspx");
                }
                else
                {
                    if (!IsPostBack)
                    {
                        getUserPersonaldetails();
                    }
                    getUsersBookData();
                }
            }
            catch (Exception)
            {
                Response.Write("<script>alert('Session Expired. Please login again.');</script>");
                Response.Redirect("userlogin.aspx");
            }
        }

        // Update Button
        protected void Button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Session["username"]?.ToString()))
            {
                Response.Write("<script>alert('Session Expired. Please login again.');</script>");
                Response.Redirect("userlogin.aspx");
            }
            else
            {
                UpdateUserPersonalDetails();
            }
        }

        void UpdateUserPersonalDetails()
        {
            string password = string.IsNullOrEmpty(TextBox10.Text.Trim()) ? TextBox9.Text.Trim() : TextBox10.Text.Trim();

            try
            {
                using (SqlConnection con = new SqlConnection(strcon))
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }

                    SqlCommand cmd = new SqlCommand("UPDATE member_master SET full_name = @FullName, dob = @DOB, contact_no = @ContactNo, email = @Email, " +
                        "state = @State, city = @City, pincode = @Pincode, full_address = @FullAddress, password = @Password, account_status = @AccountStatus " +
                        "WHERE member_id = @MemberID", con);

                    cmd.Parameters.AddWithValue("@FullName", TextBox1.Text.Trim());
                    cmd.Parameters.AddWithValue("@DOB", TextBox2.Text.Trim());
                    cmd.Parameters.AddWithValue("@ContactNo", TextBox3.Text.Trim());
                    cmd.Parameters.AddWithValue("@Email", TextBox4.Text.Trim());
                    cmd.Parameters.AddWithValue("@State", DropDownList1.SelectedItem.Value);
                    cmd.Parameters.AddWithValue("@City", TextBox6.Text.Trim());
                    cmd.Parameters.AddWithValue("@Pincode", TextBox5.Text.Trim());
                    cmd.Parameters.AddWithValue("@FullAddress", TextBox7.Text.Trim());
                    cmd.Parameters.AddWithValue("@Password", password);
                    cmd.Parameters.AddWithValue("@AccountStatus", "pending");
                    cmd.Parameters.AddWithValue("@MemberID", Session["username"].ToString().Trim());

                    int result = cmd.ExecuteNonQuery();
                    if (result >= 1)
                    {
                        Response.Write("<script>alert('Your details have been updated successfully.');</script>");
                        getUserPersonaldetails();
                        getUsersBookData();
                    }
                    else
                    {
                        Response.Write("<script>alert('Invalid entry.');</script>");
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Error: " + ex.Message + "');</script>");
            }
        }

        void getUsersBookData()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(strcon))
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }

                    SqlCommand cmd = new SqlCommand("SELECT * FROM book_issue WHERE member_id = @MemberID", con);
                    cmd.Parameters.AddWithValue("@MemberID", Session["username"].ToString());

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    GridView1.DataSource = dt;
                    GridView1.DataBind();
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Error: " + ex.Message + "');</script>");
            }
        }

        void getUserPersonaldetails()
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
                    cmd.Parameters.AddWithValue("@MemberID", Session["username"].ToString());

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        DataRow row = dt.Rows[0];
                        TextBox1.Text = row["full_name"].ToString();
                        TextBox2.Text = row["dob"].ToString();
                        TextBox3.Text = row["contact_no"].ToString();
                        TextBox4.Text = row["email"].ToString();
                        DropDownList1.SelectedValue = row["state"].ToString().Trim();
                        TextBox5.Text = row["pincode"].ToString();
                        TextBox6.Text = row["city"].ToString();
                        TextBox7.Text = row["full_address"].ToString();
                        TextBox8.Text = row["member_id"].ToString();
                        TextBox9.Text = row["password"].ToString();

                        Label1.Text = row["account_status"].ToString().Trim();
                        switch (row["account_status"].ToString().Trim().ToLower())
                        {
                            case "active":
                                Label1.Attributes["class"] = "badge badge-pill badge-success";
                                break;
                            case "pending":
                                Label1.Attributes["class"] = "badge badge-pill badge-warning";
                                break;
                            case "deactive":
                                Label1.Attributes["class"] = "badge badge-pill badge-danger";
                                break;
                            default:
                                Label1.Attributes["class"] = "badge badge-pill badge-secondary";
                                break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Error: " + ex.Message + "');</script>");
            }
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DateTime dueDate = Convert.ToDateTime(e.Row.Cells[5].Text);
                    if (DateTime.Today > dueDate)
                    {
                        e.Row.BackColor = System.Drawing.Color.PaleVioletRed;
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Error: " + ex.Message + "');</script>");
            }
        }
    }
}
