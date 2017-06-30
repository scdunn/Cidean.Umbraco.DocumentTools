<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BulkActions.ascx.cs" Inherits="Cidean.Umbraco.DocumentTools.DocumentTools.BulkActions" %>

<asp:Button ID="btnLoad" Runat="server" Text="Load" />
<asp:LinkButton ID="btnShowParent" runat="server" Text="Show Parent"  Visible=false/>
<asp:HiddenField ID="uxParentId" runat="server" Value="-1" />
<asp:DataGrid ID="uxGrid" runat="server" AutoGenerateColumns=false>
<Columns>
<asp:BoundColumn DataField="ID" />
<asp:TemplateColumn>
<ItemTemplate>
<asp:CheckBox ID="uxCheckBox" runat=server />
</ItemTemplate>
</asp:TemplateColumn>
<asp:BoundColumn DataField="text" />
<asp:ButtonColumn ButtonType=LinkButton Text="List Files"  CommandName="view" />
</Columns>
</asp:DataGrid>
<p>

<asp:Button ID="btnShow" Runat="server" Text="Show" />
<asp:Button ID="btnDelete" Runat="server" Text="Delete" />
<asp:Label ID="uxLabel" runat="server" />
</p>
