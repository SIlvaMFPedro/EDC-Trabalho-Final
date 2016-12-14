<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PresidentNews.aspx.cs" Inherits="EDC_Trabalho_Final.PresidentNews" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1><i class="fa fa-rss fa-4"></i> My Feed Reader</h1>
    <hr />
      <div class="row">
        <asp:XmlDataSource ID="XmlDataSource_feed" TransformFile="~/App_Data/channel.xsl" runat="server" EnableCaching="false"></asp:XmlDataSource>
        <asp:XmlDataSource ID="auxDS" runat="server" EnableCaching="false"></asp:XmlDataSource>
        <div class="col-md-6" style="text-align: center">
            <asp:DropDownList ID="feedChooser" runat="server" CssClass="form-control" AutoPostBack="True" DataSourceID="XmlDataSourceFeedReader" DataTextField="name" DataValueField="url" OnSelectedIndexChanged="feedChooser_SelectedIndexChanged"></asp:DropDownList>
             <asp:XmlDataSource ID="XmlDataSourceFeedReader" runat="server" DataFile="~/App_Data/FeedsList.xml"></asp:XmlDataSource>
        </div>
        <div class="col-md-6" style="text-align: right; margin-top: 0px;">
            <asp:LinkButton ID="ManagePresFeeds" PostBackUrl="~/ManagePresFeeds.aspx" runat="server" CssClass="btn btn-manage"><i class="fa fa-wrench"></i>&nbsp;Manage feeds</asp:LinkButton>
        </div>
    </div>
    <div runat="server" class="row" id="feed_info">
        <div class="col-md-6">
            <h3>Feed Info</h3>
            <table class="table table-striped">
                <tbody>
                <tr id="title_" runat="server">
                    <th scope="row">Title</th>
                    <td><asp:Label ID="titleLabel" runat="server"  /></td>
                </tr>
                <tr id="link_" runat="server">
                    <th scope="row">Link</th>
                    <td><asp:Label ID="linkLabel" runat="server"  /></td>
                </tr>
                <tr id="description_" runat="server">
                    <th scope="row">Description</th>
                    <td><asp:Label ID="descriptionLabel" runat="server"  /></td>
                </tr>
                <tr id="language_" runat="server">
                    <th scope="row">Language</th>
                    <td><asp:Label ID="languageLabel" runat="server"  /></td>
                </tr>
                <tr id="managingeditor_" runat="server">
                    <th scope="row">ManagingEditor</th>
                    <td><asp:Label ID="ManagingEditorLabel" runat="server" /></td>
                </tr>
                <tr id="webmaster_" runat="server">
                    <th scope="row">WebMaster</th>
                    <td><asp:Label ID="WebMasterLabel" runat="server"  /></td>
                </tr>
                <tr id="lastbuild_" runat="server">
                    <th scope="row">LastBuildDate</th>
                    <td><asp:Label ID="LastBuildDateLabel" runat="server"  /></td>
                </tr>
                <tr id="category_" runat="server">
                    <th scope="row">Category</th>
                    <td><asp:Label ID="CategoryLabel" runat="server" Text='' /></td>
                </tr>
                </tbody>
            </table>
        </div>
        <div class="col-md-6 text-center">
            <h3>Feed Image</h3>
            <div class="row">
                <div class="col-xs-4"></div>
        		<div class="col-xs-4">
        			<img runat="server" ID="channelImage" src="http://vignette3.wikia.nocookie.net/max-steel-reboot/images/7/72/No_Image_Available.gif/revision/latest?cb=20130902173013" style="width:160px" class="img-responsive img-radio">
        		</div>
                <div class="col-xs-4"></div>
        	</div>
        </div>
    </div>
    <div class="row">
        <div class="col-md-8" style="text-align: left; margin-top: 15px; margin-left: 10px;">
            <div class="form-inline">
                <label>Keyword: </label> 
                <asp:TextBox runat="server" class="form-control" ID="ToSearch"></asp:TextBox>
                <asp:Button runat="server" Text="Search" CssClass="btn btn-manage" OnClick="Search_New" />
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-md-4" style="text-align: left; margin-top: 15px; margin-left: 10px;">
            <div class="form-inline">
                <asp:Button runat="server" Text="Search all news" CssClass="btn btn-manage" OnClick="Search_All" />
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-md-6" style="text-align: left; margin-top: 10px;">
            <asp:CheckBox runat="server" id="checkbox_" Text=" On all feeds"/>
        </div>
    </div>
    <div class="row">
        <div class="col-md-4" style="text-align: left; margin-top: 15px; margin-left: 10px;">
            <div class="form-inline">
                <label>Category: </label> 
                 <asp:DropDownList ID="DD_Category" runat="server" class="form-control" AppendDataBoundItems="true" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="DD_Category_SelectedIndexChanged"></asp:DropDownList>
            </div>
        </div>
    </div>

    <h3>Feed News <small><asp:Label runat="server" ID="counter_news" Text="10"></asp:Label></small></h3>

    <div runat="server" ID="news" class="row"></div>
</asp:Content>
