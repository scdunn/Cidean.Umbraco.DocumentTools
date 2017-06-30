using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

using umbraco.BusinessLogic;
using umbraco.cms.presentation.Trees;
using umbraco.interfaces;
using System.Data.SqlClient;

using umbraco.cms.businesslogic.web;
using umbraco.presentation.nodeFactory;

namespace Cidean.Umb.DocumentTools
{


    public class CopyChildrenAction : IAction
    {
        //create singleton
        private static readonly CopyChildrenAction _instance = new CopyChildrenAction();

        public static CopyChildrenAction Instance
        {
            get { return _instance; }
        }

        #region IAction Members

        public char Letter
        {
            get
            {
                // TODO:  Add ActionNew.Letter getter implementation
                return '<';
            }
        }

        public string JsFunctionName
        {
            get
            {

                // TODO:  Add ActionNew.JsFunctionName getter implementation
                return "openModal('/umbraco/plugins/DocumentTools/moveorcopy.aspx?app=content&mode=copy&id=' + nodeID, 'Copy', 500, 540);";
            }
        }

        public string JsSource
        {
            get
            {
                // TODO:  Add ActionNew.JsSource getter implementation
                return "";
            }
        }

        public string Alias
        {
            get
            {
                // TODO:  Add ActionNew.Alias getter implementation
                return "copy";
            }
        }

        public string Icon
        {
            get
            {
                // TODO:  Add ActionNew.Icon getter implementation
                return "copy.small.png";
            }
        }

        public bool ShowInNotifier
        {
            get
            {
                // TODO:  Add ActionNew.ShowInNotifier getter implementation
                return true;
            }
        }
        public bool CanBePermissionAssigned
        {
            get
            {
                // TODO:  Add ActionNew.ShowInNotifier getter implementation
                return true;
            }
        }
        #endregion
    }
    public class MoveChildrenAction : IAction
    {
        //create singleton
        private static readonly MoveChildrenAction _instance = new MoveChildrenAction();

        public static MoveChildrenAction Instance
        {
            get { return _instance; }
        }

        #region IAction Members

        public char Letter
        {
            get
            {
                // TODO:  Add ActionNew.Letter getter implementation
                return '>';
            }
        }

        public string JsFunctionName
        {
            get
            {

                // TODO:  Add ActionNew.JsFunctionName getter implementation
                return "openModal('/umbraco/plugins/DocumentTools/moveorcopy.aspx?app=content&mode=cut&id=' + nodeID, 'Move', 500, 540);";
            }
        }

        public string JsSource
        {
            get
            {
                // TODO:  Add ActionNew.JsSource getter implementation
                return "";
            }
        }

        public string Alias
        {
            get
            {
                // TODO:  Add ActionNew.Alias getter implementation
                return "move";
            }
        }

        public string Icon
        {
            get
            {
                // TODO:  Add ActionNew.Icon getter implementation
                return "cut.small.png";
            }
        }

        public bool ShowInNotifier
        {
            get
            {
                // TODO:  Add ActionNew.ShowInNotifier getter implementation
                return true;
            }
        }
        public bool CanBePermissionAssigned
        {
            get
            {
                // TODO:  Add ActionNew.ShowInNotifier getter implementation
                return true;
            }
        }
        #endregion
    }


    public class DeleteChildrenAction : IAction
    {
        //create singleton
        private static readonly DeleteChildrenAction _instance = new DeleteChildrenAction();

        public static DeleteChildrenAction Instance
        {
            get { return _instance; }
        }

        #region IAction Members

        public char Letter
        {
            get
            {
                // TODO:  Add ActionNew.Letter getter implementation
                return '+';
            }
        }

        public string JsFunctionName
        {
            get
            {

                // TODO:  Add ActionNew.JsFunctionName getter implementation
                return "openModal('/umbraco/plugins/DocumentTools/delete.aspx?app=content&mode=cut&id=' + nodeID, 'Delete', 500, 540);";
            }
        }

        public string JsSource
        {
            get
            {
                // TODO:  Add ActionNew.JsSource getter implementation
                return "";
            }
        }

        public string Alias
        {
            get
            {
                // TODO:  Add ActionNew.Alias getter implementation
                return "delete";
            }
        }

        public string Icon
        {
            get
            {
                // TODO:  Add ActionNew.Icon getter implementation
                return "delete.small.png";
            }
        }

        public bool ShowInNotifier
        {
            get
            {
                // TODO:  Add ActionNew.ShowInNotifier getter implementation
                return true;
            }
        }
        public bool CanBePermissionAssigned
        {
            get
            {
                // TODO:  Add ActionNew.ShowInNotifier getter implementation
                return true;
            }
        }
        #endregion
    }


    public class ReplaceWithAction : IAction
    {
        //create singleton
        private static readonly ReplaceWithAction _instance = new ReplaceWithAction();

        public static ReplaceWithAction Instance
        {
            get { return _instance; }
        }

        #region IAction Members

        public char Letter
        {
            get
            {
                // TODO:  Add ActionNew.Letter getter implementation
                return 'R';
            }
        }

        public string JsFunctionName
        {
            get
            {

                // TODO:  Add ActionNew.JsFunctionName getter implementation
                return "openModal('/umbraco/plugins/DocumentTools/replacewith.aspx?id=' + nodeID, 'Replace With Document', 500, 540);";
            }
        }

        public string JsSource
        {
            get
            {
                // TODO:  Add ActionNew.JsSource getter implementation
                return "";
            }
        }

        public string Alias
        {
            get
            {
                // TODO:  Add ActionNew.Alias getter implementation
                return "cidean_replacewith";
            }
        }

        public string Icon
        {
            get
            {
                // TODO:  Add ActionNew.Icon getter implementation
                return "importDocumenttype.png";
            }
        }

        public bool ShowInNotifier
        {
            get
            {
                // TODO:  Add ActionNew.ShowInNotifier getter implementation
                return true;
            }
        }
        public bool CanBePermissionAssigned
        {
            get
            {
                // TODO:  Add ActionNew.ShowInNotifier getter implementation
                return true;
            }
        }
        #endregion
    }

    public class MoveChildrenBase : ApplicationBase
    {

        public MoveChildrenBase()
        {
            BaseContentTree.BeforeNodeRender += new BaseTree.BeforeNodeRenderEventHandler(BaseTree_BeforeNodeRender);

        }

        private void BaseTree_BeforeNodeRender(ref XmlTree sender, ref XmlTreeNode node, EventArgs e)
        {
            if (node.Menu != null)
            {
                node.Menu.Insert(5, CopyChildrenAction.Instance);
                node.Menu.Insert(6, MoveChildrenAction.Instance);
                node.Menu.Insert(7, DeleteChildrenAction.Instance);
                node.Menu.Insert(8, ReplaceWithAction.Instance);
            }

        }
    }
}
