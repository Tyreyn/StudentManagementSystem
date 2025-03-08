<%@ Page Title="EditStudent" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EditStudent.aspx.cs" Inherits="StudentManagementSystem.EditStudent" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main>
        <dx:ASPxGridView ID="GridViewStudents" runat="server" AutoGenerateColumns="False" KeyFieldName="StudentId">
            <Columns>
                <dx:GridViewDataTextColumn FieldName="FirstName" Caption="Imię" />
                <dx:GridViewDataTextColumn FieldName="LastName" Caption="Nazwisko" />
                <dx:GridViewDataCheckColumn FieldName="Gender" Caption="Płeć">
                    <propertiescheckedit allowgrayed="true" allowgrayedbyclick="false" />
                </dx:GridViewDataCheckColumn>
                <dx:GridViewDataDateColumn FieldName="DateOfBirth" Caption="Data urodzenia" />
                <dx:GridViewDataTextColumn FieldName="Age" Caption="Wiek" />
            </Columns>
            <SettingsEditing Mode="Batch" />
        </dx:ASPxGridView>
    </main>
</asp:Content>
