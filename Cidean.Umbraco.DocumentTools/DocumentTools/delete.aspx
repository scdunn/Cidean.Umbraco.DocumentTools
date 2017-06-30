<%@ Page Language="C#" MasterPageFile="~/umbraco/masterpages/umbracoDialog.Master" AutoEventWireup="true" CodeBehind="delete.aspx.cs" Inherits="Cidean.Umb.DocumentTools.DocumentTools.delete" %>

<%@ Register TagPrefix="cc1" Namespace="umbraco.uicontrols" Assembly="controls" %>

<asp:Content ContentPlaceHolderID="head" runat="server">

 
    <style type="text/css">
        .propertyItemheader
        {
            width: 180px !important;
        }
    </style>
</asp:Content>
<asp:Content ContentPlaceHolderID="body" runat="server">
    
    
    <cc1:Feedback ID="feedback" runat="server" />
    <cc1:Pane ID="pane_description" runat="server" />
    
    <cc1:Pane ID="pane_form" runat="server" >
        <cc1:PropertyPanel runat="server" ID="pp_children" Text="Children Only">
            <asp:CheckBox runat="server" ID="OnlyChildren" Checked="false" />
        </cc1:PropertyPanel>
    </cc1:Pane>

    
    <asp:Panel ID="panel_buttons" runat="server">
        <p>
            <asp:Button ID="ok" runat="server" CssClass="guiInputButton" OnClick="HandleDelete">
            </asp:Button>
            &nbsp; <em>
                <%=umbraco.ui.Text("general", "or", this.getUser())%></em> &nbsp; <a href="#" style="color: blue"
                    onclick="top.closeModal()">
                    <%=umbraco.ui.Text("general", "cancel", this.getUser())%></a>
        </p>
    </asp:Panel>
</asp:Content>