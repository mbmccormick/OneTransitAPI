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
                data for over 175 transit agencies.
            </div>
            <br />
            <div class="registration">
                <form id="form1" runat="server">
                <b>Registration</b>
                <p>
                    Use the form below to register your application for an API key. The API key will
                    be sent to the email address that you provide.
                </p>
                <asp:TextBox ID="txtEmailAddress" runat="server"></asp:TextBox>
                <asp:Button ID="btnSubmit" runat="server" UseSubmitBehavior="true" Text="Register"
                    OnClick="btnSubmit_Click" />
                </form>
                <br />
                <b>Terms of Service</b>
                <p>
                    We (the folks at McCormick Technologies) run a public transit API service called
                    OneTransitAPI.com and would love for you to use it. Our basic service is free, and
                    we offer paid upgrades for advanced features such as high-bandwidth transactions
                    and analytics. Our service is designed to give you the most up-to-date information
                    as possible and encourage you to devlop unique and innovative applications. However,
                    be responsible in what you create.</p>
                <p>
                    (Note, we’ve copied this Terms of Service under a Creative Commons Sharealike license
                    from WordPress.com, which means you’re more than welcome to steal it and repurpose
                    it for your own use, just make sure to replace references to us with ones to you,
                    and if you want we’d appreciate a link to WordPress.com somewhere on your site.
                    They spent a lot of money and time on this, and other people shouldn’t need to do
                    the same.)</p>
            </div>
        </div>
        <div id="footer">
            &copy; 2011 <a href="http://www.mccormicktechnologies.com" target="_blank">McCormick
                Technologies</a>
        </div>
    </div>
</body>
</html>
