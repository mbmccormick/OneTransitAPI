<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="OneTransitAPI.Register" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>OneTransit</title>
    <link rel="Stylesheet" href="StyleSheet.css" />
</head>
<body>
    <div id="page">
        <div id="header">
            <h1>
                <a href="/">OneTransit</a>
            </h1>
            <br />
            <p>
                Single API for accessing real-time public transit data.</p>
        </div>
        <div id="content">
            <div class="introduction">
                OneTransit is a simple service that makes it easy to access real-time public transit
                data for over 175 transit agencies. Data is pulled from hundreds of web services
                to provide real-time data when available. When real-time data is not available,
                OneTransitAPI delivers scheduled data, which is updated every 72 hours.
            </div>
            <br />
            <div class="registration">
                <form id="form1" runat="server">
                <b>Registration</b>
                <p>
                    Use the form below to register your application for an API key. The API key will
                    be sent to the email address that you provide.
                </p>
                <b>Email Address</b>
                <br />
                <asp:TextBox ID="txtEmailAddress" runat="server"></asp:TextBox>
                <br />
                <br />
                <asp:Button ID="btnSubmit" runat="server" UseSubmitBehavior="true" />
                </form>
            </div>
        </div>
        <div id="footer">
            &copy; 2011 <a href="http://www.mccormicktechnologies.com" target="_blank">McCormick
                Technologies</a>
        </div>
    </div>
</body>
</html>
