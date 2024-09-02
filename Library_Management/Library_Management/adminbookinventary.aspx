<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="adminbookinventary.aspx.cs" Inherits="Library_Management.adminbookinventary" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            $(".table").prepend($("<thead></thead>").append($(this).find("tr:first"))).dataTable();
        });

        function readURL(input) {
            if (input.files && input.files[0]) {
                var reader = new FileReader();

                reader.onload = function (e) {
                    $('#imgview').attr('src', e.target.result);
                };

                reader.readAsDataURL(input.files[0]);
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container-fluid">
        <div class="row">
            <div class="col-md-5">
                <div class="card" style="max-height: 95%">
                    <div class="card-body">
                        <div class="row">
                            <div class="col">
                                <center>
                                    <h4>Book Details</h4>
                                </center>
                            </div>
                        </div>

                       <div class="row m-1">
                            <div class="col">
                                <center>
                                    <img src="images/books.jpg" height="150" width="100" class="rounded border border-0" id="imgview"/>
                                </center>
                            </div>
                        </div>

                        <div class="row m-2">
                            <div class="mx-auto">
                                <asp:FileUpload ID="FileUpload1" onchange="readURL(this);" BorderStyle="Solid" runat="server" />
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-5">
                                <label>Book ID</label>
                                <div class="form-group">
                                    <div class="input-group">
                                        <asp:TextBox CssClass="form-control" ID="TextBox1" runat="server" placeholder="Book ID"></asp:TextBox>
                                        <asp:Button CssClass="btn btn-dark rounded" ID="Button1" runat="server" Text="Go" placeholder="Go" OnClick="Button1_Click" />
                                    </div>    
                                </div>
                            </div>

                            <div class="col-md-7">
                                <label>Book Name</label>
                                <asp:TextBox CssClass="form-control" ID="TextBox2" runat="server" placeholder="Book Name"></asp:TextBox>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-4">
                                <label>Language</label>
                                <div class="form-group">
                                    <asp:DropDownList class="form-control" ID="DropDownList1" runat="server">
                                        <asp:ListItem Text="English" Value="English" />
                                        <asp:ListItem Text="Hindi" Value="Hindi"/>
                                        <asp:ListItem Text="Marathi" Value="Marathi" />
                                        <asp:ListItem Text="French" Value="French"/>
                                        <asp:ListItem Text="German" Value="German" />
                                        <asp:ListItem Text="Urdu" Value="Urdu" />
                                    </asp:DropDownList>
                                </div>

                                <label>Publisher Name</label>
                                <div class="form-group">
                                    <asp:DropDownList class="form-control" ID="DropDownList2" runat="server">
                                        <asp:ListItem Text="Publisher1" Value="Publisher1" />
                                        <asp:ListItem Text="Publisher2" Value="Publisher2"/>
                                    </asp:DropDownList>
                                </div>
                            </div>

                            <div class="col-md-4">
                                <label>Author Name</label>
                                <div class="form-group">
                                    <asp:DropDownList class="form-control" ID="DropDownList3" runat="server">
                                        <asp:ListItem Text="A1" Value="a1" />
                                        <asp:ListItem Text="A2" Value="a2" />
                                    </asp:DropDownList>
                                </div>

                                <label>Publisher Date</label>
                                <div class="form-group">
                                    <asp:TextBox cssClass="form-control" ID="TextBox3" runat="server" placeholder="Publisher Date" TextMode="Date"></asp:TextBox>
                                </div>
                            </div>
                       
                            <div class="col-md-4">
                                <label>Genre</label>
                                <div class="form-group">
                                   <asp:ListBox CssClass="form-control" ID="ListBox1" runat="server" SelectionMode="Multiple" Rows="5">
                                      <asp:ListItem Text="Action" Value="Action" />
                                      <asp:ListItem Text="Adventure" Value="Adventure" />
                                      <asp:ListItem Text="Comic Book" Value="Comic Book" />
                                      <asp:ListItem Text="Self Help" Value="Self Help" />
                                      <asp:ListItem Text="Motivation" Value="Motivation" />
                                      <asp:ListItem Text="Healthy Living" Value="Healthy Living" />
                                      <asp:ListItem Text="Wellness" Value="Wellness" />
                                      <asp:ListItem Text="Crime" Value="Crime" />
                                      <asp:ListItem Text="Drama" Value="Drama" />
                                      <asp:ListItem Text="Fantasy" Value="Fantasy" />
                                      <asp:ListItem Text="Horror" Value="Horror" />
                                      <asp:ListItem Text="Poetry" Value="Poetry" />
                                      <asp:ListItem Text="Personal Development" Value="Personal Development" />
                                      <asp:ListItem Text="Romance" Value="Romance" />
                                      <asp:ListItem Text="Science Fiction" Value="Science Fiction" />
                                      <asp:ListItem Text="Suspense" Value="Suspense" />
                                      <asp:ListItem Text="Thriller" Value="Thriller" />
                                      <asp:ListItem Text="Art" Value="Art" />
                                      <asp:ListItem Text="Autobiography" Value="Autobiography" />
                                      <asp:ListItem Text="Encyclopedia" Value="Encyclopedia" />
                                      <asp:ListItem Text="Health" Value="Health" />
                                      <asp:ListItem Text="History" Value="History" />
                                      <asp:ListItem Text="Math" Value="Math" />
                                      <asp:ListItem Text="Textbook" Value="Textbook" />
                                      <asp:ListItem Text="Science" Value="Science" />
                                      <asp:ListItem Text="Travel" Value="Travel" />
                                   </asp:ListBox>
                                </div>
                             </div>
                        </div>

                        <div class="row">
                            <div class="col-md-4">
                                <label>Edition</label>
                                <div class="form-group">
                                    <asp:TextBox cssClass="form-control" ID="TextBox4" runat="server" placeholder="Edition"></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-md-4">
                                <label>Book Cost(per unit)</label>
                                <div class="form-group">
                                    <asp:TextBox cssClass="form-control" ID="TextBox5" runat="server" placeholder="Book Cost(per unit)" TextMode="Number"></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-md-4">
                                <label>Pages</label>
                                <div class="form-group">
                                    <asp:TextBox cssClass="form-control" ID="TextBox6" runat="server" placeholder="Pages" TextMode="Number"></asp:TextBox>
                                </div>
                            </div>
                        </div>

                         <div class="row">
                             <div class="col-md-4">
                                 <label>Actual Stock</label>
                                 <div class="form-group">
                                     <asp:TextBox cssClass="form-control" ID="TextBox7" runat="server" placeholder="Actual Stock" TextMode="Number"></asp:TextBox>
                                 </div>
                             </div>

                             <div class="col-md-4">
                                 <label>Current Stock</label>
                                 <div class="form-group"> 
                                     <asp:TextBox cssClass="form-control" ID="TextBox8" runat="server" placeholder="Current Stock" TextMode="Number" ReadOnly="true"></asp:TextBox>
                                 </div>
                             </div>

                             <div class="col-md-4">
                                 <label>Issue Book</label>
                                 <div class="form-group">
                                     <asp:TextBox cssClass="form-control" ID="TextBox9" runat="server" placeholder="Issue Book" TextMode="Number" ReadOnly="true"></asp:TextBox>
                                 </div>
                             </div>
                         </div>

                        <div class="row">
                            <div class="col">
                                <label>Boook Description</label>
                                <div class="form-group">
                                    <asp:TextBox cssClass="form-control" ID="TextBox10" runat="server" placeholder="Boook Description" TextMode="MultiLine" Rows="2"></asp:TextBox>
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-4">
                                <asp:Button CssClass="btn btn-block btn-success btn-lg" ID="Button2" runat="server" Text="Add" placeholder="Add Button" OnClick="Button2_Click"/>
                            </div>

                            <div class="col-4">
                                <asp:Button CssClass="btn btn-block btn-warning btn-lg" ID="Button3" runat="server" Text="Update" placeholder="Update Button" OnClick="Button3_Click" />
                            </div>

                            <div class="col-4">
                                <asp:Button CssClass="btn btn-block btn-danger btn-lg" ID="Button4" runat="server" Text="Delete" placeholder="Delete Button" OnClick="Button4_Click" />
                            </div>
                        </div>

                        <div class="m-2">
                            < < <a href="homepage.aspx">Back to Home</a>
                        </div>
                    </div>
                </div>
            </div>

            <div class="col-md-7">
                <div class="card">
                    <div class="card-body">
                        <div class="row">
                            <div class="col">
                                <center>
                                    <h4><span><img src="images/icon1.jpg" width="50" class="rounded"/></span>Book Inventary List</h4>
                                </center>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col">
                                <hr />
                            </div>
                        </div>

                        <div class="row">
                            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ELibrary_databaseConnectionString4 %>" ProviderName="<%$ ConnectionStrings:ELibrary_databaseConnectionString4.ProviderName %>" SelectCommand="SELECT * FROM [book_master]"></asp:SqlDataSource>
                            <div class="col">
                                <asp:GridView ID="GridView1" CssClass="table table-striped table-bordered" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource1">
                                    <Columns>
                                        <asp:BoundField DataField="book_id" HeaderText="Book ID" ReadOnly="true" SortExpression="book_id" >
                                        <HeaderStyle Font-Bold="False" />
                                        <ItemStyle Font-Bold="True" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="Book Details">
                                            <ItemTemplate>
                                                <div class="container-fluid">

                                                    <div class="row">
                                                        <div class="col-lg-10 res">
                                                            <div class="row">
                                                                <div class="col-lg-12">
                                                                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("book_name") %>' Font-Bold="True" Font-Size="X-Large"></asp:Label>   
                                                                </div>
                                                            </div>

                                                            <div class="row">
                                                                <div class="col-lg-12">

                                                                    Author:
                                                                    <asp:Label ID="Label2" runat="server" Font-Bold="True" Text='<%# Eval("author_name") %>'></asp:Label>
                                                                    &nbsp;| Genre:&nbsp;
                                                                    <asp:Label ID="Label3" runat="server" Font-Bold="True" Text='<%# Eval("genre") %>'></asp:Label>
                                                                    &nbsp;| Language:
                                                                    <asp:Label ID="Label4" runat="server" style="font-weight: 700" Text='<%# Eval("language") %>'></asp:Label>

                                                                </div>
                                                            </div>

                                                            <div class="row">
                                                                <div class="col-lg-12">

                                                                    Publisher:
                                                                    <asp:Label ID="Label6" runat="server" style="font-weight: 700" Text='<%# Eval("publisher_name") %>'></asp:Label>
                                                                    &nbsp;| Publisher-date:
                                                                    <asp:Label ID="Label7" runat="server" style="font-weight: 700" Text='<%# Eval("publish_date") %>'></asp:Label>
                                                                    &nbsp;| Pages:
                                                                    <asp:Label ID="Label8" runat="server" style="font-weight: 700" Text='<%# Eval("no_of_pages") %>'></asp:Label>
                                                                    &nbsp;| Edition:
                                                                    <asp:Label ID="Label9" runat="server" style="font-weight: 700" Text='<%# Eval("edition") %>'></asp:Label>

                                                                </div>
                                                            </div>

                                                            <div class="row">
                                                                <div class="col-lg-12">

                                                                    Cost:
                                                                    <asp:Label ID="Label5" runat="server" style="font-weight: 700" Text='<%# Eval("book_cost") %>'></asp:Label>
                                                                    &nbsp;| Actual-Stock:
                                                                    <asp:Label ID="Label10" runat="server" style="font-weight: 700" Text='<%# Eval("actual_stock") %>'></asp:Label>
                                                                    &nbsp;| Avilable:
                                                                    <asp:Label ID="Label11" runat="server" style="font-weight: 700" Text='<%# Eval("current_stock") %>'></asp:Label>

                                                                </div>
                                                            </div>

                                                            <div class="row">
                                                                <div class="col-lg-12">

                                                                    Description:
                                                                    <asp:Label ID="Label12" runat="server" style="font-weight: 700" Text='<%# Eval("book_description") %>'></asp:Label>

                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div class="col-lg-2">
                                                            <asp:Image ID="Image1" CssClass="img-fluid" runat="server" ImageUrl='<%# Eval("book_img_link") %>' />
                                                        </div>
                                                    </div>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
