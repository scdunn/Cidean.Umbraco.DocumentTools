using umbraco;
using umbraco.BasePages;
using umbraco.cms;
using umbraco.cms.businesslogic;
using umbraco.cms.businesslogic.web;
using umbraco.cms.businesslogic.propertytype;
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace Cidean.Umb.DocumentTools.DocumentTools
{
    public partial class delete : UmbracoEnsuredPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ok.Text = ui.Text("general", "ok", this.getUser());
                ok.Attributes.Add("style", "width: 60px");
                Document d = new Document(int.Parse(helper.Request("id")));
                pane_description.Text = "Are you sure you want to delete document " + d.Text + "?";
            }
        }

        public void HandleDelete(object sender, EventArgs e)
        {

            Document d = new Document(int.Parse(helper.Request("id")));

            if (!OnlyChildren.Checked)
            {
                if (d.Published)
                    library.UnPublishSingleNode(d.Id);

                d.delete();

                feedback.Text = "Node " + d.Text + " has been deleted?";
                feedback.type = umbraco.uicontrols.Feedback.feedbacktype.success;
                return;
            }
            else
            {
                foreach (Document childDocument in d.Children)
                {
                    if (childDocument.Published)
                        library.UnPublishSingleNode(childDocument.Id);
                    childDocument.delete();
                }

                feedback.Text = "All child nodes of " + d.Text + " have been deleted.";
                feedback.type = umbraco.uicontrols.Feedback.feedbacktype.success;
                return;
            }

            // refresh tree
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "treeRefresh", String.Format("updateTree({0}, {1}, {2}, '{3}', false);", d.Id, d.Parent.Id, d.Id,d.Path), true);


        }
    }
}
