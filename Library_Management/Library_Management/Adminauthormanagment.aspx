﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Adminauthormanagment.aspx.cs" Inherits="Library_Management.Adminauthormanagment" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            $(".table").prepend($("<thead></thead>").append($(this).find("tr:first"))).dataTable();
            //$('.table1').DataTable();
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container" style="margin-top: 3%; margin-bottom:7%;">
        <div class="row">
            <div class="col-md-5">
                <div class="card">
                    <div class="card-body">

                        <div class="row">
                            <div class="col">
                                <center>
                                    <h4>Author Details</h4>
                                </center>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col">
                                <center>
                                    <img src="images/Writer-Male.jpg" width="150" class="rounded-circle border-dark"/>
                                </center>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-4">
                                <label>Author ID</label>
                                <div class="form-group">
                                    <div class="input-group">
                                        <asp:TextBox cssClass="form-control" ID="TextBox1" runat="server" placeholder="ID"></asp:TextBox>
                                        <asp:Button class="btn btn-dark" ID="Button4" runat="server" Text="Go" OnClick="Button4_Click" />
                                    </div>
                                </div>
                            </div>

                            <div class="col-md-8">
                                <label>Author Name</label>
                                <div class="form-group">
                                    <asp:TextBox cssClass="form-control" ID="TextBox2" runat="server" placeholder="Author Name"></asp:TextBox>
                                </div>
                            </div>
                        </div>

                        <div class="row my-2">
                            <div class="col-md-4">
                                <div class="form-group">
                                    <asp:Button CssClass="btn btn-block btn-success btn-lg" ID="Button1" runat="server" Text="Add" OnClick="Button1_Click" />
                                </div>
                            </div>

                            <div class="col-md-4">
                                <div class="form-group">
                                    <asp:Button CssClass="btn btn-block btn-primary btn-lg" ID="Button2" runat="server" Text="Update" OnClick="Button2_Click" />
                                </div>
                            </div>

                            <div class="col-md-4">
                                <div class="form-group">
                                    <asp:Button CssClass="btn btn-block btn-danger btn-lg" ID="Button3" runat="server" Text="Delete" OnClick="Button3_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="m-2">
                    < < <a href="homepage.aspx">Back to Home</a>
                </div>
            </div>

            <div class="col-md-7">

                <div class="card">
                    <div class="card-body">
                        <div class="row">
                            <div class="col">
                                <center>
                                    <h4><span><img src="images/icon.jpg" width="60" class="rounded"/></span> Author List</h4>
                                    <asp:Label ID="Label2" cssClass="badge badge-pill badge-info" runat="server" Text="Your Books-info"></asp:Label>
                                </center>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col">
                                <hr />
                            </div>
                        </div>

                        <div class="row">
                            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ELibrary_databaseConnectionString %>" ProviderName="<%$ ConnectionStrings:ELibrary_databaseConnectionString.ProviderName %>" SelectCommand="SELECT * FROM [author_master]"></asp:SqlDataSource>
                            <div class="col">
                                <asp:GridView cssClass="table table-striped" ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="author_id" DataSourceID="SqlDataSource1">
                                    <Columns>
                                        <asp:BoundField DataField="author_id" HeaderText="author_id" ReadOnly="True" SortExpression="author_id" />
                                        <asp:BoundField DataField="author_name" HeaderText="author_name" SortExpression="author_name" />
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
