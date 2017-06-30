<%@ Page Language="c#" CodeBehind="CopyChildren.aspx.cs" MasterPageFile="~/umbraco/masterpages/umbracoDialog.Master"
    AutoEventWireup="True" Inherits="Cidean.Umbraco.DocumentTools.CopyChildren" %>

<%@ Register TagPrefix="cc1" Namespace="umbraco.uicontrols" Assembly="controls" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript">

			function dialogHandler(id) {
				document.getElementById("copyTo").value = id;
				document.getElementById("<%= ok.ClientID %>").disabled = false;
				
				// Get node name by xmlrequest
				if (id > 0)
						umbraco.presentation.webservices.CMSNode.GetNodeName('<%=umbraco.BasePages.BasePage.umbracoUserContextID%>', id, updateName);
				else{
					//document.getElementById("pageNameContent").innerHTML = "'<strong><%=umbraco.ui.Text(umbraco.helper.Request("app"))%></strong>' <%= umbraco.ui.Text("moveOrCopy","nodeSelected") %>";
			    
			    jQuery("#pageNameContent").html("<strong><%=umbraco.ui.Text(umbraco.helper.Request("app"))%></strong> <%= umbraco.ui.Text("moveOrCopy","nodeSelected") %>");
					jQuery("#pageNameHolder").attr("class","success");
			  }
			}
			
			function updateName(result) {
				jQuery("#pageNameContent").html("'<strong>" + result + "</strong>' <%= umbraco.ui.Text("moveOrCopy","nodeSelected") %>");
				jQuery("#pageNameHolder").attr("class","success");
			}
			
			function updateTree(current, oldParent, newParent, newParentPath, copy) {
	            var currentNode = parent.findNodeById(current);
			    var oldParentNode = parent.findNodeById(oldParent);
			    var newParentNode = parent.findNodeById(newParent);
			    if (!copy) {
			        if (oldParent == "-1") {
			            if (currentNode) {
			                currentNode.remove();
			            }
			        } else if (oldParentNode) {
			            oldParentNode.reload();
			        }
			        
                    parent.tree.SyncTree(newParentPath, true);

                    /*
                    if (newParent == "-1" || newParentPath.split(",").length == 2) {
                        var docUrl = parent.tree.document.location.href;
                        if (docUrl.indexOf('?') > 0) {
                            parent.tree.document.location.href = docUrl + "&refreshViaMove=" + Math.random() * 10;
                        } else {
                            parent.tree.document.location.href = docUrl + "?refreshViaMove=" + Math.random() * 10;
                        }
                    } else {
                        parent.tree.SyncTree(newParentPath, true);
                    }*/
			    } else {
			        if (newParentNode) {
			            if (newParentNode.open) {
			                newParentNode.reload();
			            }
			        }
			    }
                
			}

function doSubmit() {document.Form1["ok"].click()}

	var functionsFrame = this;
	var tabFrame = this;
	var isDialog = true;
	var submitOnEnter = true;
	
    </script>

    <script type="text/javascript" src="../js/umbracoCheckKeys.js"></script>

    <style type="text/css">
        .propertyItemheader
        {
            width: 180px !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <input type="hidden" id="copyTo" name="copyTo" />
    
    <cc1:Feedback ID="feedback" runat="server" />
    
    <cc1:Pane ID="pane_form" runat="server" Visible="false">
        <cc1:PropertyPanel ID="PropertyPanel1" runat="server">
            <iframe src="/umbraco/TreeInit.aspx?app=<%=umbraco.helper.Request("app")%>&isDialog=true&dialogMode=id&contextMenu=false"
                style="overflow: auto; width: 100%; position: relative; height: 200px; background-color: white"
                frameborder="0"></iframe>
        </cc1:PropertyPanel>
        <cc1:PropertyPanel runat="server" ID="pp_relate" Text="Relate copied items to original">
            <asp:CheckBox runat="server" ID="RelateDocuments" Checked="false" />
        </cc1:PropertyPanel>
    </cc1:Pane>
    
    <asp:PlaceHolder ID="pane_form_notice" runat="server" Visible="false">
        <div class="notice" id="pageNameHolder" style="margin-top: 10px;">
            <p id="pageNameContent">
                <%= umbraco.ui.Text("moveOrCopy","noNodeSelected") %></p>
        </div>
    </asp:PlaceHolder>
    
    <cc1:Pane ID="pane_settings" runat="server" Visible="false">
        <cc1:PropertyPanel ID="PropertyPanel2" runat="server" Text="Master Document Type">
            <asp:ListBox id="masterType" Runat="server" cssClass="bigInput" Rows="1" SelectionMode="Single"></asp:ListBox>
        </cc1:PropertyPanel>
        
        <cc1:PropertyPanel ID="PropertyPanel3" runat="server" Text="Name">
            <asp:TextBox ID="rename" runat="server" Style="width: 350px;" CssClass="bigInput"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator1" ErrorMessage="*" ControlToValidate="rename"
                runat="server">*</asp:RequiredFieldValidator>
            
        </cc1:PropertyPanel>
        
    </cc1:Pane>
    
    <asp:Panel ID="panel_buttons" runat="server">
        <p>
            <asp:Button ID="ok" runat="server" CssClass="guiInputButton" OnClick="HandleMoveOrCopy">
            </asp:Button>
            &nbsp; <em>
                <%=umbraco.ui.Text("general", "or", this.getUser())%></em> &nbsp; <a href="#" style="color: blue"
                    onclick="top.closeModal()">
                    <%=umbraco.ui.Text("general", "cancel", this.getUser())%></a>
        </p>
    </asp:Panel>
</asp:Content>
