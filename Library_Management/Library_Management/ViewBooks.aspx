<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="ViewBooks.aspx.cs" Inherits="Library_Management.ViewBooks" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            $(".table").prepend($("<thead></thead>").append($(this).find("tr:first"))).dataTable();
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="row">
            <div class="col-sm-12">
                <center>
                    <h3>Book Inventary List</h3>
                </center>
                <div class="row">
                    <div class="col-sm-12 col-md-12">
                        <asp:Panel CssClass="alert alert-success" role="alert" ID="Panel1" runat="server" Visible="false">
                            <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
                        </asp:Panel>
                    </div>
                </div>
                <br />
                <div class="row">
                      <div class="card">
                          <div class="card-body">
                              <div class="row">
                                  <div class="col">
                                      <center>
                                          <h4><span><img src="images/icon1.jpg" width="50" class="rounded"/></span>Inventary List</h4>
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
    </div>
</asp:Content>
