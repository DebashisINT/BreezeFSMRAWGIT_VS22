<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucPartialMenu.ascx.cs" Inherits="ERP.OMS.MasterPage.UserControls.ucPartialMenu" %>

<%--<%@ OutputCache Duration="120" VaryByParam="None" VaryByCustom="userid" %>--%>

 



   
<% ERP.OMS.MVCUtility.RenderAction("Common", "_PartialMenu", new { }); %>
<div class="text-center pwred hide"> Powered by BreezeERP  </div>
