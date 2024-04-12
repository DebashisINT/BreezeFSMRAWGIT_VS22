<%--/********************************************************************************************************************
 Written by    Sanchita    V2.0.45     14-02-2024      Change Password option not working. Mantis: 27242

**********************************************************************************************************************/--%>
<%@ Page Title="Change Password" Language="C#" MasterPageFile="~/OMS/MasterPage/ERP.Master" AutoEventWireup="true" CodeBehind="frmchangepassword.aspx.cs" Inherits="ERP.OMS.Management.ToolsUtilities.frmchangepassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script language="javascript" type="text/javascript">
        function BtnCancel_Click() {
            location.href = '/OMS/Management/ProjectMainPage.aspx';
        }
        $(document).ready(function () {


            $("#TxtNewPassword").keyup(function () {
                check_pass();
            });

        });


        function check_pass() {
            var val = document.getElementById("TxtNewPassword").value;
            var meter = document.getElementById("meter");
            var no = 0;
            if (val != "") {
                // If the password length is less than or equal to 6
                if (val.length <= 6) no = 1;

                // If the password length is greater than 6 and contain any lowercase alphabet or any number or any special character
                if (val.length > 6 && (val.match(/[a-z]/) || val.match(/\d+/) || val.match(/.[!,@,#,$,%,^,&,*,?,_,~,-,(,)]/))) no = 2;

                // If the password length is greater than 6 and contain alphabet,number,special character respectively
                if (val.length > 6 && ((val.match(/[a-z]/) && val.match(/\d+/)) || (val.match(/\d+/) && val.match(/.[!,@,#,$,%,^,&,*,?,_,~,-,(,)]/)) || (val.match(/[a-z]/) && val.match(/.[!,@,#,$,%,^,&,*,?,_,~,-,(,)]/)))) no = 3;

                // If the password length is greater than 6 and must contain alphabets,numbers and special characters
                if (val.length > 6 && val.match(/[a-z]/) && val.match(/\d+/) && val.match(/.[!,@,#,$,%,^,&,*,?,_,~,-,(,)]/)) no = 4;


                $('#hdPassstrength').val(no);
                if (no == 1) {
                    $("#meter").animate({ width: '50px' }, 300);
                    meter.style.backgroundColor = "red";
                    document.getElementById("pass_type").innerHTML = "Very Weak";
                    document.getElementById("pass_type").style.color = "red";
                }

                if (no == 2) {
                    $("#meter").animate({ width: '100px' }, 300);
                    meter.style.backgroundColor = "#F5BCA9";
                    document.getElementById("pass_type").innerHTML = "Weak";
                    document.getElementById("pass_type").style.color = "#F5BCA9";
                }

                if (no == 3) {
                    $("#meter").animate({ width: '150px' }, 300);
                    meter.style.backgroundColor = "#FF8000";
                    document.getElementById("pass_type").innerHTML = "Good";
                    document.getElementById("pass_type").style.color = "#FF8000";
                }

                if (no == 4) {
                    $("#meter").animate({ width: '200px' }, 300);
                    meter.style.backgroundColor = "#00c531";
                    document.getElementById("pass_type").innerHTML = "Strong";
                    document.getElementById("pass_type").style.color = "#00c531";
                }
            }

            else {
                meter.style.backgroundColor = "white";
                document.getElementById("pass_type").style.color = "white";
                document.getElementById("pass_type").innerHTML = "";

            }
        }
    </script>
</asp:Content>



<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="breadCumb">
            <span>Change Password</span>
            <div class="crossBtnN"><a href="../master/Root_User.aspx"><i class="fa fa-times"></i></a></div>
    </div>
    <div class="container">
        <table class="TableMain100 mt-5 " style="text-align: left;">

            <tr>
                <td style="text-align: left;">
                    <br />
                    <div class="backBox p-5">
                    <asp:Panel ID="panel2" BorderColor="white" BorderWidth="1px" runat="server">
                        <table>
                            <tr>
                                <td class="Ecoheadtxt" width="200px" style="padding-bottom: 10px;">Old Password :&nbsp;<em style="color: red">*</em>
                                </td>
                                <td style="text-align: left; padding-bottom: 10px;" >
                                    <asp:TextBox ID="TxtOldPassword" runat="server" CssClass="EcoheadCon form-control" TextMode="Password" MaxLength="20"
                                        Width="160px"></asp:TextBox>
                                </td>
                                <td>

                                </td>
                            </tr>
                            <tr>
                                <td class="Ecoheadtxt" style="padding-bottom: 10px;">New Password :&nbsp;<em style="color: red">*</em>
                                </td>
                                <td style="text-align: left;  padding-bottom: 10px;">
                                    <asp:TextBox ID="TxtNewPassword" runat="server" CssClass="EcoheadCon form-control" TextMode="Password" MaxLength="20"
                                        Width="160px"></asp:TextBox>
                                </td>
                                <td>
                                        <span id="pass_type" style="font-weight: 800;padding-left: 15px;"></span>
                                    <span id="meter" style="height: 5px;"></span>
                                </td>
                            </tr>
                            <tr>
                                <td class="Ecoheadtxt">Confirm Password :&nbsp;<em style="color: red">*</em>
                                </td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="TxtConfirmPassword" runat="server" CssClass="EcoheadCon form-control" TextMode="Password" MaxLength="20"
                                        Width="160px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td></td>
                                <td class="Ecoheadtxt pt-5">
                                    <asp:Button ID="BtnSave" runat="server" Text="Update" class="btn btn-primary" OnClick="BtnSave_Click"
                                        ValidationGroup="a" />
                                    <input id="BtnCancel" type="button" value="Cancel" class="btn btn-danger" onclick="BtnCancel_Click()" />

                                <%--    <a href='javascript:history.back()' class="btn btn-danger">Go Back to Previous Page</a>--%>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TxtOldPassword"
                                        Display="None" ErrorMessage="Old PassWord Can Not Be Blank" SetFocusOnError="True"
                                        ValidationGroup="a"></asp:RequiredFieldValidator>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TxtNewPassword"
                                        Display="None" ErrorMessage="New PassWord Can Not Be Blank" SetFocusOnError="True"
                                        ValidationGroup="a"></asp:RequiredFieldValidator>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="TxtConfirmPassword"
                                        Display="None" ErrorMessage="Confirm PassWord Can Not Be Blank" SetFocusOnError="True"
                                        ValidationGroup="a"></asp:RequiredFieldValidator>
                                    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="TxtNewPassword"
                                        ControlToValidate="TxtConfirmPassword" Display="None" ErrorMessage="New Password And Confirm Password Must Be Same"
                                        SetFocusOnError="True" ValidationGroup="a"></asp:CompareValidator>
                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                                        ShowSummary="False" ValidationGroup="a" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                        </div>
                    <br />
                </td>
            </tr>
        </table>
    </div>

</asp:Content>
