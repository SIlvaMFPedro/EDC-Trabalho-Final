<%@ Page Title="Presidents Twitter feed. " Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="About.aspx.cs" Inherits="edc_trabalho_final.About" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %>.</h2>
    <h3><br /> Obama, Trump & Bush.</h3>
    

    <a class="twitter-timeline"  href="https://twitter.com/search?q=from%3APOTUS%20OR%20from%3ArealDonaldTrump%20OR%20from%3Abillclinton%20OR%20from%3AGeorgeHWBush" data-widget-id="810894819337928705">Tweets sobre from:POTUS OR from:realDonaldTrump OR from:billclinton OR from:GeorgeHWBush</a>
    <script>!function(d,s,id){var js,fjs=d.getElementsByTagName(s)[0],p=/^http:/.test(d.location)?'http':'https';if(!d.getElementById(id)){js=d.createElement(s);js.id=id;js.src=p+"://platform.twitter.com/widgets.js";fjs.parentNode.insertBefore(js,fjs);}}(document,"script","twitter-wjs");</script>
          
    <%-- <a class="twitter-timeline" href="https://twitter.com/POTUS">Tweets by POTUS</a> <script async src="//platform.twitter.com/widgets.js" charset="utf-8"></script>
             --%>
</asp:Content>
