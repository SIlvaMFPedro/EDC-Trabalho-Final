using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Diagnostics;
using System.Xml.Linq;
using System.Collections;
using System.IO;

namespace EDC_Trabalho_Final
{
    public partial class PresidentNews : System.Web.UI.Page
    {
        Boolean repopulate = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            string url = feedChooser.SelectedValue;

            if (url.Length == 0)
            {
                url = "https://www.whitehouse.gov/feed/blog/white-house"
            }
            XmlReader xreader = XmlReader.Create(url);
            XmlDocument xdoc = new XmlDocument();
            xdoc.Load(xreader);
            xreader.Close();

            XmlDataSource_feed.Data = xdoc.OuterXml;
            XmlDataSource_feed.DataBind();
            XmlDataSource_feed.XPath = "/rss/channel";

            XmlDocument docx = XmlDataSource_feed.GetXmlDocument();
            XmlElement root = docx.DocumentElement;
            XmlNodeList nodes = root.SelectNodes(XmlDataSource_feed.XPath);

            foreach (XmlNode node in nodes)
            {
                if (node.Attributes[0].Value.Length != 0)
                    titleLabel.Text = node.Attributes[0].Value;
                else
                    title_.Visible = false;
                if (node.Attributes[1].Value.Length != 0)
                    linkLabel.Text = node.Attributes[1].Value;
                else
                    link_.Visible = false;
                if (node.Attributes[3].Value.Length != 0)
                    descriptionLabel.Text = node.Attributes[3].Value;
                else
                    description_.Visible = false;
                if (node.Attributes[2].Value.Length != 0)
                    languageLabel.Text = node.Attributes[2].Value;
                else
                    language_.Visible = false;
                if (node.Attributes[4].Value.Length != 0)
                    ManagingEditorLabel.Text = node.Attributes[4].Value;
                else
                    managingeditor_.Visible = false;
                if (node.Attributes[5].Value.Length != 0)
                    WebMasterLabel.Text = node.Attributes[5].Value;
                else
                    webmaster_.Visible = false;
                if (node.Attributes[6].Value.Length != 0)
                    LastBuildDateLabel.Text = node.Attributes[6].Value;
                else
                    lastbuild_.Visible = false;
                if (node.Attributes[7].Value.Length != 0)
                    CategoryLabel.Text = node.Attributes[7].Value;
                else
                    category_.Visible = false;

                if (node.Attributes[8].Value.Length != 0)
                {
                    channelImage.Attributes["src"] = node.Attributes[8].Value;
                }
                else
                {
                    channelImage.Attributes["src"] = "http://vignette3.wikia.nocookie.net/max-steel-reboot/images/7/72/No_Image_Available.gif/revision/latest?cb=20130902173013";
                }

            }

            XmlNodeList node_items = root.SelectNodes("/rss/channel/item");
            String innerHtml = "";
            feed_info.Visible = true;
            foreach (XmlNode node in node_items)
            {
                String node_html = "<div class=\"col-xs-12 col-md-6 col-lg-4\"><div class=\"well\" style=\"min-height: 300px\"> <div class=\"media\"> <div class=\"media-body\"> <h4 class=\"media-heading\"><a target=\"_blank\" href=\"" + node.Attributes[2].Value + "\">" + node.Attributes[0].Value + "</a></h4> <div class=\"row\"><div class=\"col-md-6\"><small><i class=\"fa fa-tag\"></i> " + node.Attributes[3].Value + "</small></div><div class=\"col-md-6\" style=\"text-align: right\"><small><i class=\"fa fa-calendar - check - o\"></i> " + node.Attributes[4].Value + "</small></div></div><p>" + node.Attributes[1].Value + "</p></div></div></div></div>";
                innerHtml += node_html;
            }
            counter_news.Text = node_items.Count.ToString();
            news.InnerHtml = innerHtml;


            XmlDocument doc_1 = new XmlDocument();
            doc_1.Load(@"C:\Users\silva\Documents\EDC\tpfinal\EDC-Trabalho-Final\EDC-Trabalho-Final\App_Data\FeedsList.xml");
            XmlNodeList elemList = doc_1.GetElementsByTagName("feed");
            String[] feeds = new String[elemList.Count];
            HashSet<String> categories = new HashSet<String>();
            categories.Add("All");
            for (int i = 0; i < elemList.Count; i++)
            {
                feeds[i] = elemList[i].Attributes["url"].Value;
            }
            for (int i = 0; i < feeds.Length; i++)
            {
                XmlReader reader_1 = null;
                try
                {
                    reader_1 = XmlReader.Create(feeds[i]);
                }
                catch
                {
                    continue;
                }
                XmlDocument doc_aux = new XmlDocument();
                doc_aux.Load(reader_1);
                xreader.Close();
                //XmlNodeList aux = doc_aux.SelectNodes("/rss/channel/item//*[contains(.," + findString+")]");
                XmlNodeList aux = doc_aux.SelectNodes("/rss/channel/item/category");
                foreach (XmlNode cat in aux)
                {
                    if (!categories.Contains(cat.InnerText) && cat.InnerText != "")
                    {
                        categories.Add(cat.InnerText);
                        //teste.Value += ("\n" + cat.InnerText);
                    }
                }
            }
            if (!IsPostBack || repopulate)
            {
                DD_Category.DataSource = categories;
                DD_Category.DataBind();
                repopulate = false;
            }


        }

        protected void Search_New(object sender, EventArgs e)
        {
            if (checkbox_.Checked)
            {
                XmlDocument doc_1 = new XmlDocument();
                doc_1.Load(@"C:\Users\silva\Documents\EDC\tpfinal\EDC-Trabalho-Final\EDC-Trabalho-Final\App_Data\FeedsList.xml");
                XmlNodeList elemList = doc_1.GetElementsByTagName("feed");
                String[] feeds = new String[elemList.Count];
                for (int i = 0; i < elemList.Count; i++)
                {
                    feeds[i] = elemList[i].Attributes["url"].Value;
                }
                string findString = ToSearch.Text;
                if (findString.Equals(""))
                    feed_info.Visible = true;
                else
                    feed_info.Visible = false;
                HashSet<XmlNode> toprint = new HashSet<XmlNode>();

                for (int i = 0; i < feeds.Length; i++)
                {
                    XmlReader reader = XmlReader.Create(feeds[i]);
                    XmlDocument doc_aux = new XmlDocument();
                    doc_aux.Load(reader);
                    reader.Close();
                    XmlNodeList src = doc_aux.SelectNodes("/rss/channel/title");
                    XmlNode source = doc_aux.CreateNode(XmlNodeType.Element, "sourcesw", "");
                    source.InnerText = src[0].InnerText;
                    toprint.Add(source);
                    XmlNodeList aux = doc_aux.SelectNodes("/rss/channel/item");
                    Boolean insert = true;
                    foreach (XmlNode mynode in aux)
                    {
                        XmlNodeList childs = mynode.ChildNodes;
                        foreach (XmlNode mynode_2 in childs)
                        {
                            if (mynode_2.InnerText.Contains(findString))
                            {
                                foreach (XmlNode test in toprint)
                                {
                                    try
                                    {
                                        if (test["title"].InnerText.Equals(mynode["title"].InnerText))
                                            insert = false;
                                    }
                                    catch (Exception) { }
                                }
                                if (insert)
                                {
                                    toprint.Add(mynode);
                                    //text_area.Value += ("\n" + mynode.OuterXml.ToString());
                                }
                            }
                        }
                    }

                }
                String innerHtml = "";
                counter_news.Text = toprint.Count.ToString();
                String helpsource = "";
                foreach (XmlNode node__ in toprint)
                {
                    try
                    {
                        if (node__.LocalName.Equals("sourcesw"))
                            helpsource = node__.InnerText;
                        if (!node__["category"].InnerText.Equals(""))
                        {
                            String node_html = "<div class=\"col-xs-12 col-md-6 col-lg-4\"><div class=\"well\" style=\"min-height: 300px\"> <div class=\"media\"> <div class=\"media-body\"> <h4 class=\"media-heading\"><a style=\" color: #0f1011;text-decoration: none;font-weight: bold;\"target=\"_blank\" href=\"" + node__["link"].InnerText + "\">" + node__["title"].InnerText + "</a></h4> <div class=\"row\"><div class=\"col-md-6\"><small><i class=\"fa fa-tag\"></i> " + node__["category"].InnerText + "</small></div><div class=\"col-md-6\" style=\"text-align: right\"><small><i class=\"fa fa-calendar - check - o\"></i> " + node__["pubDate"].InnerText + "</small></div></div><p>" + node__["description"].InnerText + "</p></div></div><small> " + helpsource + "</small></div></div>";
                            innerHtml += node_html;
                        }
                        else
                        {
                            String node_html = "<div class=\"col-xs-12 col-md-6 col-lg-4\"><div class=\"well\" style=\"min-height: 300px\"> <div class=\"media\"> <div class=\"media-body\"> <h4 class=\"media-heading\"><a style=\" color: #0f1011;text-decoration: none;font-weight: bold;\"target=\"_blank\" href=\"" + node__["link"].InnerText + "\">" + node__["title"].InnerText + "</a></h4> <div class=\"row\"><div class=\"col-md-6\"><small><i class=\"fa fa-tag\"></i> " + "" + "</small></div><div class=\"col-md-6\" style=\"text-align: right\"><small><i class=\"fa fa-calendar - check - o\"></i> " + node__["pubDate"].InnerText + "</small></div></div><p>" + node__["description"].InnerText + "</p></div></div><small> " + helpsource + "</small></div></div>";
                            innerHtml += node_html;
                        }
                    }
                    catch (Exception)
                    {

                    }


                }
                news.InnerHtml = innerHtml;
            }
            else
            {
                string url = feedChooser.SelectedValue;
                if (url.Length == 0)
                {
                    url = "https://www.whitehouse.gov/feed/blog/white-house";
                }
                XmlDocument doc_1 = new XmlDocument();
                string findString = ToSearch.Text;
                if (findString.Equals(""))
                    feed_info.Visible = true;
                else
                    feed_info.Visible = false;
                HashSet<XmlNode> toprint = new HashSet<XmlNode>();
                XmlReader reader = XmlReader.Create(url);
                XmlDocument doc_aux = new XmlDocument();
                doc_aux.Load(reader);
                reader.Close();
                XmlNodeList aux = doc_aux.SelectNodes("/rss/channel/item");
                Boolean insert = true;
                foreach (XmlNode mynode in aux)
                {
                    XmlNodeList childs = mynode.ChildNodes;
                    if (findString.Equals("") || findString.Equals(" "))
                    {
                        toprint.Add(mynode);
                    }
                    else
                    {
                        foreach (XmlNode mynode_2 in childs)
                        {
                            if (mynode_2.InnerText.Contains(findString))
                            {
                                foreach (XmlNode test in toprint)
                                {
                                    if (test["title"].InnerText.Equals(mynode["title"].InnerText))
                                        insert = false;
                                }
                                if (insert)
                                {
                                    toprint.Add(mynode);
                                }
                            }
                        }
                    }

                }
                String innerHtml = "";
                counter_news.Text = toprint.Count.ToString();
                foreach (XmlNode node__ in toprint)
                {
                    try
                    {
                        if (!node__["category"].InnerText.Equals(""))
                        {
                            String node_html = "<div class=\"col-xs-12 col-md-6 col-lg-4\"><div class=\"well\" style=\"min-height: 300px\"> <div class=\"media\"> <div class=\"media-body\"> <h4 class=\"media-heading\"><a style=\" color: #0f1011;text-decoration: none;font-weight: bold;\"target=\"_blank\" href=\"" + node__["link"].InnerText + "\">" + node__["title"].InnerText + "</a></h4> <div class=\"row\"><div class=\"col-md-6\"><small><i class=\"fa fa-tag\"></i> " + node__["category"].InnerText + "</small></div><div class=\"col-md-6\" style=\"text-align: right\"><small><i class=\"fa fa-calendar - check - o\"></i> " + node__["pubDate"].InnerText + "</small></div></div><p>" + node__["description"].InnerText + "</p></div></div></div></div>";
                            innerHtml += node_html;
                        }
                        else
                        {
                            String node_html = "<div class=\"col-xs-12 col-md-6 col-lg-4\"><div class=\"well\" style=\"min-height: 300px\"> <div class=\"media\"> <div class=\"media-body\"> <h4 class=\"media-heading\"><a style=\" color: #0f1011;text-decoration: none;font-weight: bold;\"target=\"_blank\" href=\"" + node__["link"].InnerText + "\">" + node__["title"].InnerText + "</a></h4> <div class=\"row\"><div class=\"col-md-6\"><small><i class=\"fa fa-tag\"></i> " + "" + "</small></div><div class=\"col-md-6\" style=\"text-align: right\"><small><i class=\"fa fa-calendar - check - o\"></i> " + node__["pubDate"].InnerText + "</small></div></div><p>" + node__["description"].InnerText + "</p></div></div></div></div>";
                            innerHtml += node_html;
                        }
                    }
                    catch (Exception)
                    {

                    }


                }
                news.InnerHtml = innerHtml;
            }

        }

        protected void Search_All(object sender, EventArgs e)
        {
            XmlDocument doc_1 = new XmlDocument();
            doc_1.Load(@"C:\Users\silva\Documents\EDC\tpfinal\EDC-Trabalho-Final\EDC-Trabalho-Final\App_Data\FeedsList.xml");
            XmlNodeList elemList = doc_1.GetElementsByTagName("feed");
            feed_info.Visible = false;
            String[] feeds = new String[elemList.Count];
            for (int i = 0; i < elemList.Count; i++)
            {
                feeds[i] = elemList[i].Attributes["url"].Value;
            }
            HashSet<XmlNode> toprint = new HashSet<XmlNode>();
            for (int i = 0; i < feeds.Length; i++)
            {
                XmlReader reader = XmlReader.Create(feeds[i]);
                XmlDocument doc_aux = new XmlDocument();
                doc_aux.Load(reader);
                reader.Close();
                XmlNodeList src = doc_aux.SelectNodes("/rss/channel/title");
                XmlNode source = doc_aux.CreateNode(XmlNodeType.Element, "sourcesw", "");
                source.InnerText = src[0].InnerText;
                toprint.Add(source);
                XmlNodeList aux = doc_aux.SelectNodes("/rss/channel/item");
                foreach (XmlNode mynode in aux)
                {
                    toprint.Add(mynode);
                    //text_area.Value += ("\n" + mynode.OuterXml.ToString());
                }

            }
            String innerHtml = "";
            counter_news.Text = toprint.Count.ToString();
            String helpsource = "";
            foreach (XmlNode node__ in toprint)
            {
                try
                {
                    if (node__.LocalName.Equals("sourcesw"))
                        helpsource = node__.InnerText;
                    if (!node__["category"].InnerText.Equals(""))
                    {
                        String node_html = "<div class=\"col-xs-12 col-md-6 col-lg-4\"><div class=\"well\" style=\"min-height: 300px\"> <div class=\"media\"> <div class=\"media-body\"> <h4 class=\"media-heading\"><a style=\" color: #0f1011;text-decoration: none;font-weight: bold;\"target=\"_blank\" href=\"" + node__["link"].InnerText + "\">" + node__["title"].InnerText + "</a></h4> <div class=\"row\"><div class=\"col-md-6\"><small><i class=\"fa fa-tag\"></i> " + node__["category"].InnerText + "</small></div><div class=\"col-md-6\" style=\"text-align: right\"><small><i class=\"fa fa-calendar - check - o\"></i> " + node__["pubDate"].InnerText + "</small></div></div><p>" + node__["description"].InnerText + "</p></div></div><small> " + helpsource + "</small></div></div>";
                        innerHtml += node_html;
                    }
                    else
                    {
                        String node_html = "<div class=\"col-xs-12 col-md-6 col-lg-4\"><div class=\"well\" style=\"min-height: 300px\"> <div class=\"media\"> <div class=\"media-body\"> <h4 class=\"media-heading\"><a style=\" color: #0f1011;text-decoration: none;font-weight: bold;\"target=\"_blank\" href=\"" + node__["link"].InnerText + "\">" + node__["title"].InnerText + "</a></h4> <div class=\"row\"><div class=\"col-md-6\"><small><i class=\"fa fa-tag\"></i> " + "" + "</small></div><div class=\"col-md-6\" style=\"text-align: right\"><small><i class=\"fa fa-calendar - check - o\"></i> " + node__["pubDate"].InnerText + "</small></div></div><p>" + node__["description"].InnerText + "</p></div></div><small> " + helpsource + "</small></div></div>";
                        innerHtml += node_html;
                    }
                }
                catch (Exception)
                {

                }


            }
            news.InnerHtml = innerHtml;
        }

        protected void DD_Category_SelectedIndexChanged(object sender, EventArgs e)
        {
            string cat = DD_Category.SelectedItem.Value;
            if (checkbox_.Checked)
            {
                XmlDocument doc_1 = new XmlDocument();
                doc_1.Load(@"C:\Users\silva\Documents\EDC\tpfinal\EDC-Trabalho-Final\EDC-Trabalho-Final\App_Data\FeedsList.xml");
                XmlNodeList elemList = doc_1.GetElementsByTagName("feed");
                String[] feeds = new String[elemList.Count];
                for (int i = 0; i < elemList.Count; i++)
                {
                    feeds[i] = elemList[i].Attributes["url"].Value;
                }
                feed_info.Visible = false;
                HashSet<XmlNode> toprint = new HashSet<XmlNode>();
                //var toprint = new List<XmlNode>();
                for (int i = 0; i < feeds.Length; i++)
                {
                    XmlReader reader = XmlReader.Create(feeds[i]);
                    XmlDocument doc_aux = new XmlDocument();
                    doc_aux.Load(reader);
                    reader.Close();
                    XmlNodeList src = doc_aux.SelectNodes("/rss/channel/title");
                    XmlNode source = doc_aux.CreateNode(XmlNodeType.Element, "sourcesw", "");
                    source.InnerText = src[0].InnerText;
                    toprint.Add(source);
                    XmlNodeList aux = doc_aux.SelectNodes("/rss/channel/item");
                    foreach (XmlNode mynode in aux)
                    {
                        try
                        {
                            if (mynode["category"].InnerText.Equals(cat))
                            {
                                toprint.Add(mynode);
                            }
                            else if (cat.Equals("All"))
                            {
                                toprint.Add(mynode);
                            }
                        }
                        catch (Exception)
                        {

                        }

                    }

                }
                String innerHtml = "";
                counter_news.Text = toprint.Count.ToString();
                String helpsource = "";
                foreach (XmlNode node__ in toprint)
                {
                    try
                    {
                        if (node__.LocalName.Equals("sourcesw"))
                            helpsource = node__.InnerText;
                        if (!node__["category"].InnerText.Equals(""))
                        {
                            String node_html = "<div class=\"col-xs-12 col-md-6 col-lg-4\"><div class=\"well\" style=\"min-height: 300px\"> <div class=\"media\"> <div class=\"media-body\"> <h4 class=\"media-heading\"><a style=\" color: #0f1011;text-decoration: none;font-weight: bold;\"target=\"_blank\" href=\"" + node__["link"].InnerText + "\">" + node__["title"].InnerText + "</a></h4> <div class=\"row\"><div class=\"col-md-6\"><small><i class=\"fa fa-tag\"></i> " + node__["category"].InnerText + "</small></div><div class=\"col-md-6\" style=\"text-align: right\"><small><i class=\"fa fa-calendar - check - o\"></i> " + node__["pubDate"].InnerText + "</small></div></div><p>" + node__["description"].InnerText + "</p></div></div><small> " + helpsource + "</small></div></div>";
                            innerHtml += node_html;
                        }
                        else
                        {
                            String node_html = "<div class=\"col-xs-12 col-md-6 col-lg-4\"><div class=\"well\" style=\"min-height: 300px\"> <div class=\"media\"> <div class=\"media-body\"> <h4 class=\"media-heading\"><a style=\" color: #0f1011;text-decoration: none;font-weight: bold;\"target=\"_blank\" href=\"" + node__["link"].InnerText + "\">" + node__["title"].InnerText + "</a></h4> <div class=\"row\"><div class=\"col-md-6\"><small><i class=\"fa fa-tag\"></i> " + "" + "</small></div><div class=\"col-md-6\" style=\"text-align: right\"><small><i class=\"fa fa-calendar - check - o\"></i> " + node__["pubDate"].InnerText + "</small></div></div><p>" + node__["description"].InnerText + "</p></div></div><small> " + helpsource + "</small></div></div>";
                            innerHtml += node_html;
                        }
                    }
                    catch (Exception)
                    {

                    }
                }
                news.InnerHtml = innerHtml;
            }
            else
            {
                string url = feedChooser.SelectedValue;
                if (url.Length == 0)
                {
                    url = "https://www.whitehouse.gov/feed/blog/white-house";
                }
                XmlDocument doc_1 = new XmlDocument();
                HashSet<XmlNode> toprint = new HashSet<XmlNode>();
                XmlReader reader = XmlReader.Create(url);
                XmlDocument doc_aux = new XmlDocument();
                doc_aux.Load(reader);
                reader.Close();
                XmlNodeList aux = doc_aux.SelectNodes("/rss/channel/item");
                foreach (XmlNode mynode in aux)
                {
                    try
                    {
                        if (mynode["category"].InnerText.Equals(cat))
                        {
                            toprint.Add(mynode);
                        }
                        else if (cat.Equals("All"))
                        {
                            toprint.Add(mynode);
                        }
                    }
                    catch (Exception)
                    {

                    }

                }
                String innerHtml = "";

                foreach (XmlNode node__ in toprint)
                {
                    String node_html = "<div class=\"col-xs-12 col-md-6 col-lg-4\"><div class=\"well\" style=\"min-height: 300px\"> <div class=\"media\"> <div class=\"media-body\"> <h4 class=\"media-heading\"><a style=\" color: #0f1011;text-decoration: none;font-weight: bold;\" target=\"_blank\" href=\"" + node__["link"].InnerText + "\">" + node__["title"].InnerText + "</a></h4> <div class=\"row\"><div class=\"col-md-6\"><small><i class=\"fa fa-tag\"></i> " + node__["category"].InnerText + "</small></div><div class=\"col-md-6\" style=\"text-align: right\"><small><i class=\"fa fa-calendar - check - o\"></i> " + node__["pubDate"].InnerText + "</small></div></div><p>" + node__["description"].InnerText + "</p></div></div></div></div>";
                    innerHtml += node_html;
                }
                news.InnerHtml = innerHtml;
            }

        }

        protected void feedChooser_SelectedIndexChanged(object sender, EventArgs e)
        {
            repopulate = true;
            Page_Load(sender, e);
        }
    }
}