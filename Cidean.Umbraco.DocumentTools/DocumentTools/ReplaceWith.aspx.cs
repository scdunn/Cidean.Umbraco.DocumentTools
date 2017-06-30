using umbraco;
using umbraco.BasePages;
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
using umbraco.cms.businesslogic.web;
using umbraco.cms.businesslogic;
using umbraco.cms.businesslogic.template;
using umbraco.presentation;

namespace Cidean.Umbraco.DocumentTools
{
    public partial class ReplaceWith : UmbracoEnsuredPage
    {

        

        protected void Page_Load(object sender, EventArgs e)
        {



            //Initialize control events
            btnReplaceWith.Click += new EventHandler(btnReplaceWith_Click);
            uxDocumentType.SelectedIndexChanged += new EventHandler(uxDocumentType_SelectedIndexChanged);
            Document oldDoc = new Document(Int32.Parse(Request.QueryString["id"].ToString()));

            uxCurrentDocumentType.Text = "OLD:" + oldDoc.ContentType.Text;
            if (!IsPostBack)
            {


                //load document types and templates for first document type
                System.Collections.Generic.List<DocumentType> dts = DocumentType.GetAllAsList();
                foreach (DocumentType docType in dts)
                {
                    uxDocumentType.Items.Add(new ListItem(docType.Text,docType.Id.ToString()));
                }
                LoadTemplates();
            }
        }

        void btnReplaceWith_Click(object sender, EventArgs e)
        {
            //retrieve current document on which dialog was triggered
            Document oldDoc = new Document(Int32.Parse(Request.QueryString["id"].ToString()));

            //retrieve document types of current document and selected document type
            DocumentType oldDocType = new DocumentType(oldDoc.ContentType.Id);
            DocumentType newDocType = new DocumentType(Int32.Parse(uxDocumentType.SelectedValue));


            //store parent node id of current document
            int parentId = -1;
            try{parentId = oldDoc.Parent.Id;}catch (Exception ex){}
            
            //make a new document of the selected document type, setting the selected template and the
            //sort order of the current document
            Document newDoc = Document.MakeNew(oldDoc.Text, newDocType, new umbraco.BusinessLogic.User(0),parentId);
            newDoc.Template = Int32.Parse(uxTemplate.SelectedValue);
            newDoc.sortOrder = oldDoc.sortOrder;

            //copy current document properties to the new document for all properties where the alias
            //is the same
            foreach (umbraco.cms.businesslogic.propertytype.PropertyType prop in oldDocType.PropertyTypes)
            {
                umbraco.cms.businesslogic.property.Property newProp = newDoc.getProperty(prop.Alias);
                if(newProp != null)
                    newDoc.getProperty(prop.Alias).Value = oldDoc.getProperty(prop.Alias).Value;

            }

            //save the new document
            newDoc.Save();

            //move all child nodes of the current document under the new document
            foreach (Document child in oldDoc.Children)
                child.Move(newDoc.Id);
            
            //publish the new document and update cache
            newDoc.Publish(new umbraco.BusinessLogic.User(0));
            umbraco.library.UpdateDocumentCache(newDoc.Id);

            //delete the old document
            oldDoc.delete();

            //display success message to the user and refresh the content tree
            feedback.type = umbraco.uicontrols.Feedback.feedbacktype.success;
            feedback.Text = "Document has been replaced.";
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "treeRefresh", "refreshNode();", true);



        }


        void RenderBridge()
        {
            if (uxDocumentType.Text=="") return;
            propertyPanel.Controls.Clear();

            Document oldDoc = new Document(Int32.Parse(Request.QueryString["id"].ToString()));
            DocumentType oldDocType = new DocumentType(oldDoc.ContentType.Id);
            DocumentType newDocType = new DocumentType(Int32.Parse(uxDocumentType.SelectedValue));

            propertyPanel.Controls.Add(new LiteralControl("<table>"));
            foreach (umbraco.cms.businesslogic.propertytype.PropertyType newProp in newDocType.PropertyTypes)
            {
                propertyPanel.Controls.Add(new LiteralControl("<tr><td>"));
                propertyPanel.Controls.Add(new LiteralControl(newProp.Alias));
                propertyPanel.Controls.Add(new LiteralControl("</td><td>"));
                DropDownList drp = new DropDownList();
                drp.ID = "uxProp" + newProp.Alias.Replace(" ", "");
                drp.Items.Add(new ListItem(""));
                foreach (umbraco.cms.businesslogic.propertytype.PropertyType oldProp in oldDocType.PropertyTypes)
                {
                    drp.Items.Add(new ListItem(oldProp.Alias));
                }
                propertyPanel.Controls.Add(drp);
                propertyPanel.Controls.Add(new LiteralControl("</td></tr>"));
            }

            propertyPanel.Controls.Add(new LiteralControl("</table>"));
        }

        void uxDocumentType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //load templates in dropdown for selected document type
            LoadTemplates();
            
        }

        void LoadTemplates()
        {
            //retrieve document type object of selected document type id
            DocumentType newDocType = new DocumentType(Int32.Parse(uxDocumentType.SelectedValue));

            //clear dropdown and load templates for selected document type in dropdown
            uxTemplate.Items.Clear();
            foreach (Template template in newDocType.allowedTemplates)
                uxTemplate.Items.Add(new ListItem(template.Text, template.Id.ToString()));

            //select default template for document type
            uxTemplate.SelectedValue = newDocType.DefaultTemplate.ToString();
        }
    }
}
