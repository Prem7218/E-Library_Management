using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Library_Management
{
    public partial class Site1 : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["role"] != null)
                {
                    if (Session["role"].Equals("user"))
                    {
                        LinkButtonHelloUsers.Text = "Hello " + Session["username"].ToString();
                        SetMainPage(false, false, true, true, false, false, false, false, false, false);
                    }
                    else if (Session["role"].Equals("admin"))
                    {
                        LinkButtonHelloUsers.Text = "Hello Admin " + Session["username"].ToString();
                        SetMainPage(false, false, true, true, false, true, true, true, true, true);
                    }
                    else
                    {
                        SetMainPage(true, true, false, false, true, false, false, false, false, false);
                    }
                }
                else
                {
                    SetMainPage(true, true, false, false, true, false, false, false, false, false);
                }
            }
            catch (Exception ex)
            {
                // Log exception or handle accordingly
                Response.Write("<script>alert('An error occurred: " + ex.Message + "');</script>");
            }
        }

        protected void LinkButtonAdminLogin_Click(object sender, EventArgs e)
        {
            Response.Redirect("adminlogin.aspx");
        }

        protected void LinkButtonAuthorManagement_Click(object sender, EventArgs e)
        {
            Response.Redirect("Adminauthormanagment.aspx");
        }

        protected void LinkButtonPublisherManagement_Click(object sender, EventArgs e)
        {
            Response.Redirect("adminPublishermanagment.aspx");
        }

        protected void LinkButtonBookInventory_Click(object sender, EventArgs e)
        {
            Response.Redirect("adminbookinventary.aspx");
        }

        protected void LinkButtonBookIssuing_Click(object sender, EventArgs e)
        {
            Response.Redirect("bookissuing.aspx");
        }

        protected void LinkButtonMemberManagement_Click(object sender, EventArgs e)
        {
            Response.Redirect("adminusermanagment.aspx");
        }

        protected void LinkButtonLogin_Click(object sender, EventArgs e)
        {
            Response.Redirect("userlogin.aspx");
        }

        protected void LinkButtonSignUp_Click(object sender, EventArgs e)
        {
            Response.Redirect("userSignup.aspx");
        }

        protected void LinkButtonLogout_Click(object sender, EventArgs e)
        {
            Session["username"] = null;
            Session["full_name"] = null;
            Session["role"] = null;
            Session["status"] = null;
            SetMainPage(true, true, false, false, true, false, false, false, false, false);
            Response.Redirect("homepage.aspx"); // Redirect to homepage after logout
        }

        protected void LinkButtonViewBooks_Click(object sender, EventArgs e)
        {
            Response.Redirect("ViewBooks.aspx");
        }

        private void SetMainPage(params bool[] visibility)
        {
            LinkButtonLogin.Visible = visibility[0];
            LinkButtonSignUp.Visible = visibility[1];
            LinkButtonLogout.Visible = visibility[2];
            LinkButtonHelloUsers.Visible = visibility[3];
            LinkButtonAdminLogin.Visible = visibility[4];
            LinkButtonAuthorManagement.Visible = visibility[5];
            LinkButtonPublisherManagement.Visible = visibility[6];
            LinkButtonBookInventory.Visible = visibility[7];
            LinkButtonBookIssuing.Visible = visibility[8];
            LinkButtonMemberManagement.Visible = visibility[9];
        }

        protected void LinkButtonHelloUsers_Click(object sender, EventArgs e)
        {
            Response.Redirect("userprofileview.aspx");
        }
    }
}
