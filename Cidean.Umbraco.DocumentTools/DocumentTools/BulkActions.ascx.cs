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
using umbraco.presentation.nodeFactory;

namespace Cidean.Umbraco.DocumentTools.DocumentTools
{
    public partial class BulkActions : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            btnShow.Click += new EventHandler(btnShow_Click);
            uxGrid.ItemCommand += new DataGridCommandEventHandler(uxGrid_ItemCommand);
            uxGrid.ItemDataBound += new DataGridItemEventHandler(uxGrid_ItemDataBound);
            btnShowParent.Click += new EventHandler(btnShowParent_Click);
            /*
            btnLoad.Click += new EventHandler(btnLoad_Click);
            btnShow.Click += new EventHandler(btnShow_Click);
            btnDelete.Click += new EventHandler(btnDelete_Click);
            
            uxTree.PopulateNodesFromClient = true;
            uxTree.EnableClientScript = true;
            */
            if (!IsPostBack)
            {

               

                Document[] docs = Document.GetRootDocuments();

                uxGrid.DataSource = docs;
                uxGrid.DataBind();
                
                
            }


        }

        void btnShowParent_Click(object sender, EventArgs e)
        {
            ShowFiles(uxParentId.Value);
        }

        void uxGrid_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            try
            {
                if (!((Document)e.Item.DataItem).HasChildren)
                {
                    e.Item.Cells[3].Controls.Clear();
                }
            }
            catch (Exception ex)
            {

            }
        }

        void ShowFiles(string parentNodeId)
        {
            
                Document doc = new Document(Int32.Parse(parentNodeId));

                uxGrid.DataSource = doc.Children;
                uxGrid.DataBind();
            
        }

        void uxGrid_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            ShowFiles(e.Item.Cells[0].Text);
        }

        void btnShow_Click(object sender, EventArgs e)
        {
            string output = "";
            foreach (DataGridItem item in uxGrid.Items)
            {
                CheckBox itemCheck = ((CheckBox)item.Cells[1].FindControl("uxCheckBox"));
                if (itemCheck.Checked)
                    output += item.Cells[0].Text;
            }
            uxLabel.Text = output;
        }



       

       
    }
}