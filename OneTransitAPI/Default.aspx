<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="OneTransitAPI.Default" %>

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
                OneTransit is a simple service that makes it easy to access real-time public
                transit data for over 175 transit agencies. Data is pulled from hundreds of web
                services to provide real-time data when available. When real-time data is not available,
                OneTransitAPI delivers scheduled data, which is updated every 72 hours.
            </div>
            <br />
            <div class="documentation">
                <b>Methods</b>
                <p>
                    Below is a list of the methods supported by OneTransitAPI. All requests should be
                    a HTTP GET request. OneTransitAPI responds to all methods in JSON format.
                </p>
                <div class="method">
                    <b>/v1/agencies/getList</b><br />
                    Get a list of transit agencies supported by this API.
                </div>
                <div class="method">
                    <b>/v1/routes/getList?agency=[agencyid]</b><br />
                    Get a list of routes for a specific transit agency.
                </div>
                <div class="method">
                    <b>/v1/stops/getInfo?agency=[agencyid]&stop=[stopid]</b><br />
                    Get information for a specific stop.
                </div>
                <div class="method">
                    <b>/v1/stops/getList?agency=[agencyid]</b><br />
                    Get a list of stops for a specific transit agency.
                </div>
                <div class="method">
                    <b>/v1/stops/getListByLocation?agency=[agencyid]&lat=[latitude]&lon=[longitude]&radius=[radius]</b><br />
                    Get a list of stops for a specific transit agency by location.
                </div>
                <div class="method">
                    <b>/v1/stops/getTimes?agency=[agencyid]&stop=[stopid]</b><br />
                    Get a list of times for a specific stop.
                </div>
            </div>
            <br />
            <div class="disclaimer">
                <b>Disclaimer</b>
                <p>
                    While this API is currently active and serving up responses, it is still an alpha
                    release and is not meant for production use. Use this service at your own discretion.</p>
            </div>
        </div>
        <div id="footer">
            &copy; 2011 <a href="http://www.mccormicktechnologies.com" target="_blank">McCormick
                Technologies</a>
        </div>
    </div>
</body>
</html>
