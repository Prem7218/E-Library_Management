using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Library_Management
{
    public partial class adminbookinventary : System.Web.UI.Page
    {
        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        static string global_filepath = "";
        static int global_actual_stock = 0, global_current_stock = 0, global_issued_stock = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                FillAuthorPublisherData();
            }
            GridView1.DataBind();
        }

        // Go Button....
        protected void Button1_Click(object sender, EventArgs e)
        {
            GoDetails();
        }

        // Add Button....
        protected void Button2_Click(object sender, EventArgs e)
        {
            AddNewBook();
        }

        // Update Button....
        protected void Button3_Click(object sender, EventArgs e)
        {
            if(IfBookExist())
            {
                UpdateDetailsMember();
            }
            else
            {
                Response.Write("<script>alert('Books Not Exist.');</script>");
            }
        }

        // Delete Button....
        protected void Button4_Click(object sender, EventArgs e)
        {
            if(IfBookExist())
            {
                DeleteBook();
            }
            else
            {
                Response.Write("<script>alert('Books Not Exist.');</script>");
            }
        }

        void GoDetails()
        {
            try
            {
                global_actual_stock = 0; global_current_stock = 0; global_issued_stock = 0;
                using (SqlConnection con = new SqlConnection(strcon))
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }

                    SqlCommand cmd = new SqlCommand("SELECT * FROM book_master WHERE book_id=@BookID", con);
                    cmd.Parameters.AddWithValue("@BookID", TextBox1.Text.Trim());

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    if (dt.Rows.Count >= 1)
                    {
                        TextBox1.Text = dt.Rows[0]["book_id"].ToString();
                        TextBox2.Text = dt.Rows[0]["book_name"].ToString();
                        DropDownList1.SelectedValue = dt.Rows[0]["language"].ToString().Trim();
                        DropDownList2.SelectedValue = dt.Rows[0]["publisher_name"].ToString().Trim();
                        DropDownList3.SelectedValue = dt.Rows[0]["author_name"].ToString().Trim();
                        TextBox3.Text = dt.Rows[0]["publish_date"].ToString();
                        TextBox4.Text = dt.Rows[0]["edition"].ToString();
                        TextBox5.Text = dt.Rows[0]["book_cost"].ToString();
                        TextBox6.Text = dt.Rows[0]["no_of_pages"].ToString();
                        TextBox10.Text = dt.Rows[0]["book_description"].ToString();
                        TextBox7.Text = dt.Rows[0]["actual_stock"].ToString();
                        TextBox9.Text = TextBox9.Text;

                        if (Convert.ToInt32(dt.Rows[0]["current_stock"].ToString()) == 0 && (Convert.ToInt32(dt.Rows[0]["current_stock"].ToString()) < Convert.ToInt32(dt.Rows[0]["actual_stock"].ToString())))
                        {
                            SqlCommand cmd1 = new SqlCommand("UPDATE book_master SET current_stock = actual_stock WHERE book_id=@BookID", con);
                            cmd1.Parameters.AddWithValue("@BookID", TextBox1.Text.Trim());
                            cmd1.ExecuteNonQuery();
                        }
                        
                        ListBox1.ClearSelection();
                        string[] genres = dt.Rows[0]["genre"].ToString().Trim().Split(',');
                        foreach (string genre in genres)
                        {
                            ListItem listItem = ListBox1.Items.FindByText(genre.Trim());
                            if (listItem != null)
                            {
                                listItem.Selected = true;
                            }
                        }

                        global_actual_stock = Convert.ToInt32(dt.Rows[0]["actual_stock"].ToString().Trim());
                        global_current_stock = Convert.ToInt32(dt.Rows[0]["current_stock"].ToString().Trim());
                        global_issued_stock = global_actual_stock - global_current_stock;
                        global_filepath = dt.Rows[0]["book_img_link"].ToString();
                    }

                    con.Close();
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }


        bool IfBookExist()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(strcon))
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }

                    SqlCommand cmd = new SqlCommand("SELECT * FROM book_master WHERE book_id=@BookID", con);
                    cmd.Parameters.AddWithValue("@BookID", TextBox1.Text.Trim());

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
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

        private void FillAuthorPublisherData()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(strcon))
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }

                    SqlCommand cmd = new SqlCommand("SELECT author_name FROM author_master", con);
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    dataAdapter.Fill(dt);

                    DropDownList3.DataSource = dt;
                    DropDownList3.DataValueField = "author_name";
                    DropDownList3.DataBind();

                    cmd = new SqlCommand("SELECT publisher_name FROM publisher_master", con);
                    dataAdapter = new SqlDataAdapter(cmd);
                    dt = new DataTable();
                    dataAdapter.Fill(dt);

                    DropDownList2.DataSource = dt;
                    DropDownList2.DataValueField = "publisher_name";
                    DropDownList2.DataBind();

                    con.Close();
                    GridView1.DataBind();
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }

        void AddNewBook()
        {
            if (!string.IsNullOrEmpty(TextBox1.Text) && !IfBookExist())
            {
                try
                {
                    string genres = "";

                    foreach (int i in ListBox1.GetSelectedIndices())
                    {
                        genres += ListBox1.Items[i] + ",";
                    }
                    genres = genres.Remove(genres.Length - 1);

                    string filepath = @"images/books.jpg";
                    string filename = Path.GetFileName(FileUpload1.PostedFile.FileName);
                    FileUpload1.SaveAs(Server.MapPath("bookInventory/" + filename));
                    filepath = "~/bookInventory/" + filename;

                    using (SqlConnection con = new SqlConnection(strcon))
                    {
                        if (con.State == ConnectionState.Closed)
                        {
                            con.Open();
                        }

                        if (TextBox1.Text != "" && TextBox2.Text != "")
                        {
                            SqlCommand cmd = new SqlCommand("INSERT INTO book_master (book_id, book_name, genre, author_name, publisher_name," +
                                " publish_date, language, edition, book_cost, no_of_pages, book_description, actual_stock, current_stock, book_img_link)" +
                                " VALUES" +
                                " (@BookID, @BookName, @Genre, @AuthorName, @PublisherName, @PublisherDate, @Language, @Edition, @BookCost," +
                                "@NoOfPages, @BookDescription, @ActualStock, @CurrentStock, @BookImgLink)", con);

                            cmd.Parameters.AddWithValue("@BookID", TextBox1.Text.Trim());
                            cmd.Parameters.AddWithValue("@BookName", TextBox2.Text.Trim());
                            cmd.Parameters.AddWithValue("@Genre", genres);
                            cmd.Parameters.AddWithValue("@AuthorName", DropDownList3.SelectedItem.Value);
                            cmd.Parameters.AddWithValue("@PublisherName", DropDownList2.SelectedItem.Value);
                            cmd.Parameters.AddWithValue("@PublisherDate", TextBox3.Text.Trim());
                            cmd.Parameters.AddWithValue("@Language", DropDownList1.SelectedItem.Value);
                            cmd.Parameters.AddWithValue("@Edition", TextBox4.Text.Trim());
                            cmd.Parameters.AddWithValue("@BookCost", TextBox5.Text.Trim());
                            cmd.Parameters.AddWithValue("NoOfPages", TextBox6.Text.Trim());
                            cmd.Parameters.AddWithValue("@BookDescription", TextBox10.Text.Trim());
                            cmd.Parameters.AddWithValue("ActualStock", TextBox7.Text.Trim());
                            cmd.Parameters.AddWithValue("@CurrentStock", TextBox8.Text.Trim());
                            cmd.Parameters.AddWithValue("@BookImgLink", filepath);

                            cmd.ExecuteNonQuery();
                            Response.Write("<script>alert('Books added successfully.');</script>");
                            con.Close();
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
            else
            {
                Response.Write("<script>alert('Book already exist, try next book to add....');</script>");
            }
        }

        void UpdateDetailsMember()
        {
            if(IfBookExist())
            { 
                try
                {
                    int actual_stock = Convert.ToInt32(TextBox7.Text.Trim());
                    int current_stock = Convert.ToInt32(TextBox8.Text.Trim());

                    if (global_actual_stock == actual_stock)
                    {

                    }
                    else
                    {
                        if(actual_stock < global_issued_stock)
                        {
                            Response.Write("<script>alert('Actual Stock Value Cannot Be Less Than Current Issued Books.');</script>");
                            return;
                        }
                        else
                        {
                            current_stock = actual_stock - global_issued_stock;
                            TextBox8.Text = current_stock.ToString();   
                        }
                    }

                    // Build the genres string
                    string genres = "";
                    foreach (int i in ListBox1.GetSelectedIndices())
                    {
                        genres += ListBox1.Items[i] + ",";
                    }
                    if (!string.IsNullOrEmpty(genres))
                    {
                        genres = genres.Remove(genres.Length - 1); // Remove the trailing comma
                    }

                    string filepath = "images/books.jpg";
                    string filename = Path.GetFileName(FileUpload1.PostedFile.FileName);
                    if(filename == "" || filename == null)
                    {
                        filepath = global_filepath;
                    }
                    else
                    {
                        FileUpload1.SaveAs(Server.MapPath("bookInventory/" + filename));
                        filepath = "~/bookInventory" + filename;
                    }

                    using (SqlConnection con = new SqlConnection(strcon))
                    {
                        if (con.State == ConnectionState.Closed)
                        {
                            con.Open();
                        }

                        // Create and configure the SQL command
                        SqlCommand cmd = new SqlCommand("UPDATE book_master SET book_name = @BookName, genre = @Genre, author_name = @AuthorName, " +
                            "publisher_name = @PublisherName, publish_date = @PublisherDate, language = @Language, edition = @Edition, book_cost = @BookCost, " +
                            "no_of_pages = @NoOfPages, book_description = @BookDescription, actual_stock = @ActualStock, current_stock = @CurrentStock, book_img_link = @BookImgLink " +
                            "WHERE book_id = @BookID", con);

                        // Add parameters to the SQL command
                        cmd.Parameters.AddWithValue("@BookName", TextBox2.Text.Trim());
                        cmd.Parameters.AddWithValue("@Genre", genres);
                        cmd.Parameters.AddWithValue("@AuthorName", DropDownList3.SelectedItem.Value);
                        cmd.Parameters.AddWithValue("@PublisherName", DropDownList2.SelectedItem.Value);
                        cmd.Parameters.AddWithValue("@PublisherDate", TextBox3.Text.Trim());
                        cmd.Parameters.AddWithValue("@Language", DropDownList1.SelectedItem.Value);
                        cmd.Parameters.AddWithValue("@Edition", TextBox4.Text.Trim());
                        cmd.Parameters.AddWithValue("@BookCost", TextBox5.Text.Trim());
                        cmd.Parameters.AddWithValue("@NoOfPages", TextBox6.Text.Trim());
                        cmd.Parameters.AddWithValue("@BookDescription", TextBox10.Text.Trim());
                        cmd.Parameters.AddWithValue("@ActualStock", actual_stock.ToString());
                        cmd.Parameters.AddWithValue("@CurrentStock", current_stock.ToString());
                        cmd.Parameters.AddWithValue("@BookImgLink", filepath);

                        // Execute the command
                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            Response.Write("<script>alert('Book updated successfully.');</script>");
                            ClearForm();
                            con.Close();
                            GridView1.DataBind();
                        }
                        else
                        {
                            Response.Write("<script>alert('No book found with the provided ID.');</script>");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Response.Write("<script>alert('An error occurred: " + ex.Message + "');</script>");
                }
            }
        }

        private void DeleteBook()
        {
            if (IfBookExist())
            {
                try
                {
                    //stockCheck();
                    using (SqlConnection con = new SqlConnection(strcon))
                    {
                        if (con.State == ConnectionState.Closed)
                        {
                            con.Open();
                        }

                        SqlCommand cmd = new SqlCommand("DELETE FROM book_master WHERE book_id=@book_id", con);
                        cmd.Parameters.AddWithValue("@book_id", TextBox1.Text.Trim());

                        cmd.ExecuteNonQuery();

                        cmd = new SqlCommand("UPDATE book_master SET current_stock = current_stock - 1 WHERE book_id = @BookID", con);
                        cmd.Parameters.AddWithValue("@BookID", TextBox2.Text.Trim());

                        cmd.ExecuteNonQuery();

                        con.Close();
                        Response.Write("<script>alert('Book deleted successfully.');</script>");
                        GridView1.DataBind();
                    }
                }
                catch (Exception ex)
                {
                    Response.Write("<script>alert('" + ex.Message + "');</script>");
                }
            }
            else
            {
                Response.Write("<script>alert('Invalid Book ID');</script>");
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
            TextBox8.Text = "";
            TextBox10.Text = "";
            DropDownList1.ClearSelection();
            DropDownList2.ClearSelection();
            DropDownList3.ClearSelection();
            ListBox1.ClearSelection();
            FileUpload1.Attributes.Clear();
        }
    }
}
