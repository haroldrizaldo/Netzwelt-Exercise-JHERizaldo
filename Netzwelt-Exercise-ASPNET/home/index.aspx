<%@ Page Title="Home" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="Netzwelt_Exercise_ASPNET.home.index" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

<asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
		<div class="row">
			<div class="col-md-8">
				<div class="form-horizontal">
					<h2>Territories</h2>
					<p>Here are the list of territories</p>

					<div id="container"></div>
					<asp:TreeView ID="TreeViewTerritories" runat="server"></asp:TreeView>

				</div>
			</div>
		</div>
	</ContentTemplate>
</asp:UpdatePanel>

</asp:Content>
