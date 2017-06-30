<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/umbraco/masterpages/umbracoDialog.Master" CodeBehind="ReplaceWith.aspx.cs" Inherits="Cidean.Umbraco.DocumentTools.ReplaceWith" %>

<%@ Register TagPrefix="cc1" Namespace="umbraco.uicontrols" Assembly="controls" %>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
<cc1:PropertyPanel runat="server">
<asp:Label ID="uxCurrentDocumentType" runat="server" />
</cc1:PropertyPanel>
<asp:DropDownList ID="uxDocumentType" runat="server" AutoPostBack=true>
</asp:DropDownList>
<asp:DropDownList ID="uxTemplate" runat="server" >
</asp:DropDownList>
<asp:Panel ID="propertyPanel" runat="server" />
<asp:Button ID="btnReplaceWith" runat="server" Text="Replace With" />
<asp:Panel ID="uxOutputPanel" runat="server" />

<cc1:Feedback ID="feedback" runat="server" />





</asp:Content>
