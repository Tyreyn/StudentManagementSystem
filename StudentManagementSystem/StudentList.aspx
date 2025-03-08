<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="StudentList.aspx.cs" Inherits="StudentManagementSystem._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        function onCustomButtonClick(s, e) {
            var studentId = s.GetRowKey(e.visibleIndex);

            if (e.buttonID === "Delete") {
                if (confirm("Czy na pewno chcesz usunąć tego studenta?")) {
                    console.log("delete");
                    DeleteStudent(studentId);
                }
            } else if (e.buttonID === "Edit") {
                GridViewStudents.GetRowValues(e.visibleIndex, 'StudentId;FirstName;LastName;Gender;DateOfBirth;Age', function (values) {
                    console.log("Edytuj studenta: " + values[1] + " " + values[2]);
                    alert(values);
                    window.location.href = "EditStudent.aspx?data=" + encodeURIComponent(JSON.stringify(values));
                });
            }
        }

        function DeleteStudent(studentId) {
            $.ajax({
                type: "POST",
                url: "StudentList.aspx/DeleteStudent",
                data: JSON.stringify({ id: studentId }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                xhrFields: { withCredentials: true },
                success: function (response) {
                    alert("Student usunięty!");
                    GridViewStudents.Refresh(); // Odświeżenie tabeli
                },
                error: function (xhr, status, error) {
                    alert("Błąd podczas usuwania: " + xhr.responseText);
                }
            });
        }

    </script>

    <main>
        <dx:aspxgridview id="GridViewStudents" ClientInstanceName="GridViewStudents" runat="server" autogeneratecolumns="False" keyfieldname="StudentId" onfocusedrowchanged="GridViewStudents_FocusedRowChanged">
            <settings showheaderfilterbutton="true" />
            <settingsbehavior allowfocusedrow="true" processfocusedrowchangedonserver="true" />
            <settings verticalscrollbarmode="Visible" verticalscrollbarstyle="VirtualSmooth" />
            <clientsideevents custombuttonclick="onCustomButtonClick" />
            <settingspager>
                <pagesizeitemsettings visible="true" />
            </settingspager>
            <columns>
                <dx:gridviewcommandcolumn visibleindex="0" width="100px">
                    <custombuttons>
                        <dx:gridviewcommandcolumncustombutton text="Usuń" id="Delete" />
                        <dx:gridviewcommandcolumncustombutton text="Edytuj" id="Edit" />
                    </custombuttons>
                </dx:gridviewcommandcolumn>
                <dx:gridviewdatatextcolumn fieldname="FirstName" caption="Imię" />
                <dx:gridviewdatatextcolumn fieldname="LastName" caption="Nazwisko" />
                <dx:gridviewdatatextcolumn fieldname="Gender" caption="Płeć" />
                <dx:gridviewdatadatecolumn fieldname="DateOfBirth" caption="Data urodzenia" />
                <dx:gridviewdatatextcolumn fieldname="Age" caption="Wiek" />
            </columns>
        </dx:aspxgridview>
    </main>
</asp:Content>


