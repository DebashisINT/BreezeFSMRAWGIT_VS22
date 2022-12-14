
<%@ Page Language="C#" AutoEventWireup="true" Inherits="pLogin"
    EnableEventValidation="false" CodeBehind="Login.aspx.cs" %>

<!DOCTYPE html>
<html>
<head>
    <title>Login to BreezeERP</title>
    <meta name="description" content="" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />

    <script type="text/javascript" src="/assests/js/jquery.min.js"></script>

    <link rel="stylesheet" href="/assests/bootstrap/css/bootstrap.min.css" />
    <link rel="stylesheet" href="/assests/bootstrap/css/bootstrap-theme.min.css" />
    
    <link rel="stylesheet" type="text/css" href="/assests/fonts/font-awesome/css/font-awesome.min.css" />
   
    <meta name="theme-color" content="#0C78B1" />
    <!-- Windows Phone -->
    <meta name="msapplication-navbutton-color" content="#0C78B1" />
    <!-- iOS Safari -->
    <meta name="apple-mobile-web-app-capable" content="yes" />
    <meta name="apple-mobile-web-app-status-bar-style" content="black-translucent" />
    <script type="text/javascript">
        window.history.forward();
        function noBack() { window.history.forward(); }
        function ForNextPage() {
            //window.open('management/ProjectMainPage.aspx','windowname1','fullscreen=yes,titlebar=no,toolbar=no,statusbar=no');
        }       

        function blinkIt() {
            if (!document.all) return;
            else {
                for (i = 0; i < document.all.tags('blink').length; i++) {
                    s = document.all.tags('blink')[i];
                    s.style.visibility = (s.style.visibility == 'visible') ? 'hidden' : 'visible';
                }
            }
        }

        function removeCookiesKeyFromStorage() {
            var data = localStorage.getItem("GridCookiesId");
            if (data != null) {
                var splitCookiesData = data.split(',');
                for (var i = 1; i < splitCookiesData.length; i++) {
                    eraseCookie(splitCookiesData[i]);
                }
            }

            localStorage.setItem("GridCookiesId", "");
        }

        function eraseCookie(name) {
            createCookie(name, "", -1);
        }

        function createCookie(name, value, days) {
            if (days) {
                var date = new Date();
                date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
                var expires = "; expires=" + date.toGMTString();
            }
            else var expires = "";
            document.cookie = name + "=" + value + expires + "; path=/";
        }
        function removeMultitabCookies() {
            eraseCookie("ERPACTIVEURL");
        }
        removeMultitabCookies();
        removeCookiesKeyFromStorage();
    </script>

    <script type="text/javascript">
    $(document).ready(function () {
        $('.form-control').focusout(function () {
            if (!$(this).val() == '') {
                $(this).addClass('hasText');
            } else {
                $(this).removeClass('hasText');
            }
        });
        
        if (!$('#txtUserName').val() == '') {
            console.log('user not null');
            $('#txtUserName').addClass('hasText');
        } else {
            $('#txtUserName').removeClass('hasText');
        }
        setTimeout(function () {

            if (!$('#txtPassword').val() == '') {
                console.log('pass not null');
                $('#txtPassword').addClass('hasText');
            } else {
                $('#txtPassword').removeClass('hasText');
            }
        }, 3000);
        

        $(".passWordView").click(function () {
            $(this).toggleClass("shActive");
            var icon = $(this).find(".fa");
            if (icon.hasClass("fa-eye")) {
                icon.removeClass("fa-eye").addClass("fa-eye-slash");
            } else {
                icon.removeClass("fa-eye-slash").addClass("fa-eye");
            }
            var x = document.getElementById("txtPassword");
            if (x.type === "password") {
                x.type = "text";
            } else {
                x.type = "password";
            }
        });

    });

    

</script>
    


     <style>
        .logo-wrap > img {
            width: auto;
            max-width: 100%;
            height: auto;
        }

        .backColor {
            width: 80%;
            height: 200%;
            position: fixed;
            background: #288fce;
            z-index: 2;
            top: -200px;
            left: -504px;
            transform: rotate(72deg);
        }

        .centerd-box {
            position: relative;
            background-color: #fff;
            padding: 30px 40px;
            top: 120px;
            max-width: 390px;
            width: 100%;
            margin: 0 auto;
            border-radius: 61Px;
            box-shadow: 0 6px 22px rgba(0, 0, 0, 0.3);
            z-index: 5;
        }

            .centerd-box .logo-wrap {
                margin-bottom: 25px;
                text-align: center;
                margin-top: 18px;
            }

        .logo-wrap > img {
            width: auto;
            max-width: 62%;
            height: auto;
        }

        .btn-login {
        }

        .btn-login {
            background: #288fce;
            border: none;
            -webkit-font-smoothing: antialiased;
            font-weight: 400;
            font-size: 21px;
            color: #fff;
            box-shadow: 0px 5px 8px rgba(0,0,0,0.23);
            /* width: 83px !important; */
            height: auto !important;
            border-radius: 20px !important;
            padding: 15px 0;
            text-transform: uppercase;
        }

            .btn-login:hover, .btn-login:focus, .btn-login:active, .btn-login.active {
                background: #2088C7;
                border: none;
                -webkit-font-smoothing: antialiased;
                font-weight: 400;
                font-size: 21px;
                color: #fff;
                box-shadow: 0px 5px 8px rgba(0,0,0,0.27);
                /* width: 83px !important; */
                height: auto !important;
                border-radius: 20px !important;
                padding: 15px 0;
                text-transform: uppercase;
                opacity: 1;
                outline: none;
            }

        .loginArea .form-control {
            border: none;
            border-bottom: 1px solid #e0e0e0;
            text-align: center;
            font-size: 16px !important;
            box-shadow: none !important;
        }

            .loginArea .form-control:focus {
                border: none;
                border-bottom: 1px solid #B2B0B0;
            }

                .loginArea .form-control:focus + label,
                .loginArea .form-control.hasText + label {
                    -webkit-transform: translate(-50%, -30px);
                    -moz-transform: translate(-50%, -30px);
                    transform: translate(-50%, -30px);
                }

        .loginArea .form-group {
            position: relative;
            text-align: center;
        }

            .loginArea .form-group.inp {
                margin: 50px 0;
            }

            .loginArea .form-group label {
                position: absolute;
                left: 50%;
                top: 5px;
                -webkit-transform: translateX(-50%);
                -moz-transform: translateX(-50%);
                transform: translateX(-50%);
                -webkit-transition: all 0.1s ease;
                -moz-transition: all 0.1s ease;
                transition: all 0.1s ease;
                cursor: inherit;
            }

        .mT40 {
            margin-top: 40px;
        }

        .fixedfooter {
            position: fixed;
            width: 100%;
            text-align: center;
            height: 50px;
            bottom: 0;
            z-index: 30;
            background: transparent;
            color: #fff;
        }

        .fts{
            padding:60px 0;
            text-align:center
        }
        .fts img {
            width:80px;
            margin-bottom:25px;
            margin-top:20px
        }
        .fts h2 {
            font-family: 'Montserrat', sans-serif;
            font-size: 40px;
            line-height: normal;
            font-weight: 700;
            letter-spacing: -1px;
            color: rgba(255,91,103,1);
            margin-bottom: 3.5rem;
        }
            .fts h2 span {
                font-family: 'Open Sans', sans-serif;
                font-size: 30px;
                line-height: 45px;
                font-weight: 600;
                color: rgba(39,32,99,1);
                display: block;
                clear: both;
            }
        .hdn {
            text-align: center;
            font-family: 'Open Sans', sans-serif;
            font-weight: 600;
            color: rgba(39,32,99,1);
            font-size: 21px;
            line-height: 30px;
            margin-bottom: 15px;
            letter-spacing: -.08rem;
            padding: 0 0.5rem;
        }
    </style>
    
    
    
    <script src="/assests/pluggins/amchart/core.js"></script>
    <script src="/assests/pluggins/amchart/charts.js"></script>
    <script src="/assests/pluggins/amchart/themes/animated.js"></script>
    <script>
        $(document).ready(function () {
            $('.form-control').focusout(function () {
                if (!$(this).val() == '') {
                    $(this).addClass('hasText');
                } else {
                    $(this).removeClass('hasText');
                }
            });

        });
       
        // chart
        am4core.ready(function () {

            // Themes begin
            am4core.useTheme(am4themes_animated);
            // Themes end

            /**
             * Define data for each year
             */
            var chartData = {
                "1995": [
                  { "sector": "Agriculture", "size": 1 },
                  { "sector": "Mining and Quarrying", "size": 5 },
                  { "sector": "Manufacturing", "size": 12 },
                  { "sector": "Electricity and Water", "size": 8 },
                  { "sector": "Construction", "size": 4 },
                ],
                "1996": [
                  { "sector": "Agriculture", "size": 13 },
                  { "sector": "Mining and Quarrying", "size": 7 },
                  { "sector": "Manufacturing", "size": 3 },
                  { "sector": "Electricity and Water", "size": 8 },
                  { "sector": "Construction", "size": 5 },
                ],
                "1997": [
                  { "sector": "Agriculture", "size": 4 },
                  { "sector": "Mining and Quarrying", "size": 13 },
                  { "sector": "Manufacturing", "size": 2 },
                  { "sector": "Electricity and Water", "size": 5 },
                  { "sector": "Construction", "size": 8 },
                ],
                "1998": [
                  { "sector": "Agriculture", "size": 13 },
                  { "sector": "Mining and Quarrying", "size": 1 },
                  { "sector": "Manufacturing", "size": 7 },
                  { "sector": "Electricity and Water", "size": 3 },
                  { "sector": "Construction", "size": 6 },
                ],
                "1999": [
                  { "sector": "Agriculture", "size": 8 },
                  { "sector": "Mining and Quarrying", "size": 3 },
                  { "sector": "Manufacturing", "size": 10 },
                  { "sector": "Electricity and Water", "size": 4 },
                  { "sector": "Construction", "size": 11 },
                ]
            };

            // Create chart instance
            var chart = am4core.create("chartdiv", am4charts.PieChart);

            // Add data
            chart.data = [
              { "sector": "Agriculture", "size": 6 },
              { "sector": "Mining and Quarrying", "size": 10 },
              { "sector": "Manufacturing", "size": 3 },
              { "sector": "Electricity and Water", "size": 8 },
              { "sector": "Construction", "size": 2 },

            ];

            // Add label

            var label = chart.seriesContainer.createChild(am4core.Label);
            label.text = "1995";
            label.horizontalCenter = "middle";
            label.verticalCenter = "middle";
            label.fontSize = 50;

            // Add and configure Series
            var pieSeries = chart.series.push(new am4charts.PieSeries());
            pieSeries.dataFields.value = "size";
            pieSeries.dataFields.category = "sector";
            pieSeries.ticks.template.disabled = true;
            pieSeries.alignLabels = false;
            pieSeries.labels.template.text = "{value.percent.formatNumber('#.')}%";
            pieSeries.labels.template.radius = am4core.percent(-40);
            pieSeries.labels.template.fill = am4core.color("white");
            pieSeries.hideCredits = true;
            pieSeries.tooltip.disabled = true;
            pieSeries.colors.list = [
                   am4core.color("#845EC2"),
  am4core.color("#D65DB1"),
  am4core.color("#FF6F91"),
  am4core.color("#FF9671"),
  am4core.color("#FFC75F"),
  am4core.color("#F9F871"),
            ];
            // Animate chart data
            var currentYear = 1995;
            function getCurrentData() {
                label.text = currentYear;
                var data = chartData[currentYear];
                currentYear++;
                if (currentYear > 1999)
                    currentYear = 1995;
                return data;
            }

            function loop() {
                //chart.allLabels[0].text = currentYear;
                var data = getCurrentData();
                for (var i = 0; i < data.length; i++) {
                    chart.data[i].size = data[i].size;
                }
                chart.invalidateRawData();
                chart.setTimeout(loop, 1500);
            }

            loop();

        }); // end am4core.ready()
    </script>
    <link href="/assests/css/newlogin.css" rel="stylesheet" />
     <style type="text/css">
        #chartdiv {
            width: 100%;
            height: 100%;
        }
        .chartArea {
            position: absolute;
            width: 200px;
            height: 200px;
            background: #fff;
            bottom: 20px;
            right: 150px;
            border-radius: 50px;
            box-shadow: 0px 8px 5px rgb(0 0 0 / 20%);
            overflow:hidden
        }
        .hider {
            width:70px;
            height:30px;
            background:#fff;
            z-index:999;
            position:absolute;
            bottom:0;
            left:0;
            border-radius:0 15px 0 0
        } .mainLogin {
            height: 100vh;
            width: 100%;
        }
        .flexArea  {
            display: flex;
            width: 100%;
            height: 100vh;
        }
        .contentArea {
            width: 65%;
            overflow: hidden;
            position: relative;
            text-align: center;
            background: #333 url('/assests/images/NLogin/LoginDark_bg.png') no-repeat top left;
            background-size:cover
        }
        .formArea {
            width: 35%;
        }
        .scrImage {
            max-width: 50%;
            bottom: -15px;
            position: absolute;
            
            transform: translateX(-70%);
        }
        .tagLine {
            color:#fff;
            margin-top:80px
        } 
        .tagLine h1, .tagLine h4, .tagLine h3, .tagLine h2 {
            font-family: opcen;
        }
            .tagLine .emp {
                color: #f5d853;
                font-size:3.2em;
                margin-bottom:20px
            }
        .xoptions li {
            display:inline-block;
            list-style-type:none;
            color:#fff;
            line-height:25px;
            margin-right:15px;
            margin-top:25px
        }
        .xoptions li>img {
            width:18px;
            transform:translateY(-2px);
            margin-right:4px
        }
        .formArea {
            padding: 35px 50px;
            box-sizing: border-box;
            background: #ebebeb;
        }
        .formBox {
            margin-top: 20px;
            font-family: poppins !important;
        } 
        .formBox .form-group label {
            font-family: poppins;
        }
        .formBox .form-group {
            position:relative
        }
        .inputIcons {
            max-width: 14px;
            position: absolute;
            left: 15px;
            top: 38px;
        }
        .formBox .form-group input {
            border:none;
            background:#fff !important;
            border-radius:4px;
            min-height:43px;
            padding-left:40px
        }
        input:-webkit-autofill,
        input:-webkit-autofill:hover,
        input:-webkit-autofill:focus,
        input:-webkit-autofill:active {
            -webkit-box-shadow: 0 0 0 30px white inset !important;
        }
        .loginbtn{
            background:#6834d4;
            color:#fff;
            padding:10px 15px;
            margin-top:10px
        }
            .loginbtn:hover {
                box-shadow: 0px 5px 5px rgba(0,0,0,0.22);
                color: #fff;
                background: #5927C1;
            }
        .ftFooter {
            font-size:12px;
            margin-top:20px
        }
        .mlogos {
            width:180px;
            margin-left:-8px
        }
    </style> 
    <style type="text/css">
        .styleList {
            list-style-type:none;
            margin-top:23px;
            display:block;
            padding-left:15px
        }
        .styleList li {
                font-family: "Segoe UI","Helvetica Neue",Helvetica,Verdana,"san-serif";
            line-height:35px;
            font-size:1.7rem;
            padding-left:30px;
            background:transparent url("/assests/images/verified.png") no-repeat center left
        }
        .socialIcons {
            list-style-type:none;
            margin-top:25px
        }
        .socialIcons>li {
            display: inline-block;
        }
         .socialIcons>li {
             margin-right:20px
         }
        .socialIcons>li>div{
            width: 25px;
            display: flex;
            height: 25px;
            background: #ebebeb;
            border-radius: 4px;
            /* line-height: 118px; */
            justify-content: center;
            align-items: center;;
        }
        .lgLink {
            background: #6967bf;
            color: #fff;
            display: inline-block;
            padding: 5px 15px;
        }
        .spaceColumn{
            padding: 60px 0;
        }
        @media only screen and (max-width: 762px){
            .textSection {
                display:none
            }
            .formBox {
                margin:50px auto
            }
            .container {
                width:100%
            }
            .lister >li {
                margin-bottom:10px
            }
            .creditsSec#rc_app_1562 {
                display:none !important

            }
            .lister li:last-child {
                margin-right: 35px ;
            }
        }
        @media only screen and (min-width: 763px){

            .onlySm {
                display:none;
            }
        }
         @media only screen and (max-width: 760px){
             .flexArea {
                 height:auto;
                 width:100%
             }
            .contentArea {
                display:none;
            }
            .formArea {
                width:100%;
                display:block;
                height:100%
            }
            .responsiveImg {
                max-width:100%
            }
            .fts h2, .mkHd {
                font-size:28px;

            }
            .fts h2 span {
                font-size:22px;
            }
            .spaceColumn {
                padding: 7px 0;
            }
        }
        /*login button*/
        .cta {
          position: relative;
          font-size:18px;
          margin: auto;
          padding: 19px 22px;
          transition: all 0.2s ease;
          text-decoration: none;
            color: inherit;
        }
        .cta:before {
          content: "";
          position: absolute;
          top: 0;
          left: 0;
          display: block;
          border-radius: 28px;
          background: rgba(248, 102, 96, 0.5);
          width: 56px;
          height: 56px;
          transition: all 0.3s ease;
        }
        .cta span {
          position: relative;
            font-size: 13px;
            line-height: 18px;
            font-weight: 600;
            letter-spacing: 0.25em;
            text-transform: uppercase;
            vertical-align: middle;
        }
        .cta svg {
          position: relative;
          top: 0;
          margin-left: 10px;
          fill: none;
          stroke-linecap: round;
          stroke-linejoin: round;
          stroke: #111;
          stroke-width: 2;
          transform: translateX(-5px);
          transition: all 0.3s ease;
        }
        .cta:hover {
            text-decoration:none;
            color:#fff
        }
            .cta:hover:before {
                width: 100%;
                background: #f8666f;
                top: 1px;
                height: 55px;
            }
        .cta:hover svg {
          transform: translateX(0);
        }
        .cta:active {
          transform: scale(0.96);
        }
        /*Effect Social*/
        .effect .buttons {
          margin-top: 50px;
          display: flex;
          justify-content: center;
        }

        .effect a {
            text-decoration: none !important;
            color: #fff;
            width: 40px;
            height: 40px;
            display: flex;
            align-items: center;
            justify-content: center;
            border-radius: 10px;
            margin-right: 20px;
            font-size: 17px;
            overflow: hidden;
            position: relative;
        }
        .effect a i {
          position: relative;
          z-index: 3;
        }
        .effect a.fb {
          background-color: #3b5998;
        }
        .effect a.tw {
          background-color: #00aced;
        }
        .effect a.g-plus {
          background-color: #dd4b39;
        }
        .effect a.dribbble {
          background-color: #ea4c89;
        }
        .effect a.pinterest {
          background-color: #cb2027;
        }
        .effect a.insta {
          background-color: #bc2a8d;
        }
        .effect a.in {
          background-color: #007bb6;
        }
        .effect a.vimeo {
          background-color: #1ab7ea;
        }
        .effect.egeon a {
          transition: transform 0.2s linear 0s, border-radius 0.2s linear 0.2s;
        }
        .effect.egeon a i {
          transition: transform 0.2s linear 0s;
        }
        .effect.egeon a:hover {
          transform: rotate(-90deg);
          border-top-left-radius: 50%;
          border-top-right-radius: 50%;
          border-bottom-left-radius: 50%;
        }
        .effect.egeon a:hover i {
          transform: rotate(90deg);
        }
        .mkHd {
            font-family: 'Montserrat', sans-serif;
            font-size: 30px;
            line-height: normal;
            font-weight: 700;
            letter-spacing: -1px;
            color: rgba(255,91,103,1);
            margin-bottom:1.5rem;
        }
        .mkHs {
            font-family: 'Montserrat', sans-serif;
            font-size: 18px;
            color: #565656;
            margin-bottom:3rem;
        }
        .mkBoxes h5, .mkBoxes p {
            font-family:Poppins;
        }
        .passWordView {
            position: absolute;
            right: 14px;
            top: 38px;
            width: 21px;
            height: 20px;
            font-size: 13px;
            display: flex;
            justify-content: center;
            align-items: center;
            border-radius: 5px;
            cursor: pointer;
        }

        .event-img {
            display: none;
            border-radius: 10px;
            margin-top: 15px;
            width: 300px;
            height: 135px;
            /*overflow: hidden;*/
        }

            .event-img img {
                border-radius: 10px;
                box-shadow: 1px 1px 10px #11111160;
                width: 100%;
            }
    </style>
    
</head>
<body onload="noBack();setInterval('blinkIt()',500);" onpageshow="if (event.persisted) noBack();" onunload="">

    
    <div class="mainLogin">
         <div class="flexArea">
             <div class="contentArea">
                 <div class="tagLine"><h3>Field Sales Management</h3><h1 class="emp">Make Your Field Agents FUTURE READY!</h1><h4>Simple, Intuitive and Easy to use</h4></div>
                 <div class="xoptions">
                     <ul>
                         <li><img src="/assests/images/NLogin/checked.png" /> Live Tracking</li>
                         <li><img src="/assests/images/NLogin/checked.png" /> Infographic Data Presentation</li>
                         <li><img src="/assests/images/NLogin/checked.png" /> Attendance & Visit</li>
                     </ul>
                 </div>
                 <div>
                     <img src="/assests/images/NLogin/screen1.png" class="scrImage wow bounceInUp"  />
                     <div class="chartArea"><div class="hider"></div><div id="chartdiv"></div></div>
                 </div>
                 
             </div>
            
             <div class="formArea">
                
                      <div><asp:Label ID="lblMessage" runat="server" Text="" ForeColor="Red"></asp:Label></div>
                     <div><img src="/assests/images/NLogin/logo.png" class="mlogos" /></div>
                    <div class="event-img">
                         <img src="/assests/images/event-banner.jpg" />
                     </div>
                     <div style="margin: 25px 0 5px 0; font-size: 18px; color: #3737bb; font-weight: 600; font-family: poppins; ">Login to your Account</div>
                     <p style="font-family: poppins;">A single dashboard to manage all your team members with real time updates and notifications.</p>
                    <form action="" method="post" runat="server" novalidate="novalidate">
                    <input id="rurl" name="rurl" runat="server" type="hidden" value="" />
                     <div class="formBox">
                         <div class="form-group">
                             <label for="username">Username</label>
                             <img src="/assests/images/NLogin/user.png" class="inputIcons" />
                            <asp:TextBox ID="txtUserName" CssClass="form-control" runat="server" placeholder="Type your username" TabIndex="1"></asp:TextBox>
                         </div>
                         <div style="text-align: left">
                            <em style="color: red; position: relative; right: -160px; top: -35px;"></em>
                            <span style="color: red;">
                                <asp:RequiredFieldValidator ID="rqvUserName" runat="server"
                                    ControlToValidate="txtUserName"
                                    ErrorMessage="Please enter User Name."
                                    ForeColor="Red" ValidationGroup="login" Display="Dynamic">
                                </asp:RequiredFieldValidator></span>
                        </div>
                         <div class="form-group">
                             <label for="password">Password</label>
                             <img src="/assests/images/NLogin/password.png" class="inputIcons" />
                            <asp:TextBox ID="txtPassword" CssClass="form-control" placeholder="Type your password" runat="server" TextMode="Password" TabIndex="2"></asp:TextBox>
                             <span class="passWordView"><i class="fa fa-eye-slash"></i></span>
                         </div>
                         <div style="text-align: left">
                            <em style="color: red; position: relative; right: 55px; top: 160px;"></em>
                            <span style="color: red">
                                <asp:RequiredFieldValidator ID="rqvPassword" runat="server"
                                    ControlToValidate="txtPassword"
                                    ErrorMessage="Please enter Password."
                                    ForeColor="Red" ValidationGroup="login" Display="Dynamic">
                                </asp:RequiredFieldValidator></span>
                        </div>
                         <div style="font-size:12px">Application best viewed at <a href="#">1280 x 720</a> resolution in <a href="#">Google Chrome</a> 59 or above</div>
                         <asp:Button ID="Submit1" ValidationGroup="login" runat="server" CssClass="btn btn-block loginbtn" Text="Submit" OnClick="Login_User" TabIndex="3" />
						<asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" TabIndex="4" CssClass="compemail hide" OnClick="LinkButton1_Click1">Forgot  Password?</asp:LinkButton>

                         <div class="ftFooter">
                             © Copyright <span id="yearCP"></span> Indus Net Technologies
                             <asp:Label ID="lblVersion" runat="server" Text="1.0.4" /> 
                            <a href="Management/Master/view-version-features.aspx" target="_blank" style="display:none">( What's New )</a><br />
                            
                         </div>
                     </div>
                    </form>
                </div>
         </div>
     </div>
    <div class="fts">
             <div class="container">
                 <h2 class="">
                     Automate Processes. Manage Teams. <br>Acquire Leads. Close Sales.
                     <span>All-in-One Platform</span>
                 </h2>
             <div class="row spaceColumn">
                 
                 <div class="col-md-4 wow slideInLeft" data-wow-duration="2s" data-wow-delay="5s">
                     <img src="/assests/images/NLogin/007-destination.png" />
                     <div class="hdn">Geo-Tracking & Route  <br /> Optimization</div>
                     <div>System will intelligently capture geolocation at attendance, and will be allowed if it falls in Distribution or first visit location.</div>
                 </div>
                 <div class="col-md-4 wow slideInLeft" data-wow-duration="2s" data-wow-delay="7s">
                     <img src="/assests/images/NLogin/004-user-interface.png" />
                     <div class="hdn">IMEI/OTP based  <br /> authentication</div>
                     <div>Each user will be locked with this phone IMEI and will not be able to login on another device without admin action.</div>
                 </div>
                 <div class="col-md-4 wow slideInLeft" data-wow-duration="2s" data-wow-delay="9s">
                     <img src="/assests/images/NLogin/001-dashboard.png" />
                     <div class="hdn">Intuitive Dashboard & <br /> Auto-Reporting</div>
                     <div>A single dashboard to manage all your team members with real time updates and notifications for updates and voilations.</div>
                 </div>
             </div>
         </div>
     </div>
    <div>
        <div class="container">
            <div class="row">
                <div class="col-md-6"> 
                    <img src="/assests/images/NLogin/loginPh.png" class="responsiveImg" />
                </div>
                <div class="col-md-6">
                    <h2 class="mkHd"> Make Your Field Agents FUTURE READY!</h2>
                    <h4 class="mkHs">Accelerate Sales. Escalate Revenue.</h4>

                    <div class="row mkBoxes">
                        <div class="col-sm-6">
                            <h5>Smart Route Management</h5>
                            <p>Stop worrying about planning .Automated route suggestion as per locations to be visited. Time saved is revenue earned.</p>
                        </div>
                        <div class="col-sm-6">
                            <h5>One-Click OrderManagement</h5>
                            <p>Hassle-free order creation through a single click. Eliminates onsite visits for order acquisition & saves time real time.</p>
                        </div>
                        <div class="col-sm-6">
                            <h5>Automated Reporting</h5>
                            <p>Automatically generate reports periodically to know what is going on and take decisions real time.</p>
                        </div>
                        <div class="col-sm-6">
                            <h5>Travel Reimbursement Management</h5>
                            <p>No more hassles of having multiple portals for filling reimbursements. Single interface for all kinds of updates with status tracking.</p>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div>
        <div class="container text-center" style="padding:80px 0">
            <h3 style="font-family: 'opcen', Montserrat, sans-serif !important;font-size:26px">Log in to Start Your Digital Journey with Breeze .</h3>
            <div style="height:30px"></div>
            <a href="#" class="cta" id="toLogin">
              <span>Login Now</span>
              <svg width="13px" height="10px" viewBox="0 0 13 10">
                <path d="M1,5 L11,5"></path>
                <polyline points="8 1 12 5 8 9"></polyline>
              </svg>
            </a>
            <div></div>
            <div class="socialArea">
                 <div class="effect egeon">
                    <div class="buttons">
                      <a href="https://www.facebook.com/BreezeERPforSMBs/?source=breezeerp_contact" target="_blank" class="fb" title="Join us on Facebook"><i class="fa fa-facebook" aria-hidden="true"></i></a>
                      <a href="https://www.youtube.com/channel/UC56pxWV5D4VB2mrO-Xi-S1g" target="_blank" class="g-plus" title="Join us on Youtube"><i class="fa fa-youtube" aria-hidden="true"></i></a>
                      <a href="https://www.linkedin.com/company/breezeerp/?source=breezeerp_contact" target="_blank" class="in" title="Join us on Linked In"><i class="fa fa-linkedin" aria-hidden="true"></i></a>
                    </div>
                  </div>
            </div>
            
        </div>
    </div>

   
    <div class="backColor hide"></div>
    <div class="bgImage hide">
        <img src="/assests/images/Login.jpg" style="width: 100%; height: 100%;" alt="" />
    </div>
    <div class="container boxWraper hide">
        <div class="centerd-box">
            <div class="logo-wrap">
                <img src="/assests/images/logo.png" width="230" height="70" />
            </div>
            <div>
                

            </div>
            <div class="loginArea">
                
                    <div class="form-group inp mT40">
                        <div>
                            <label for="txtUserName">Username</label>
                            
                            
                        </div>
                        
                    </div>
                    <div class="form-group inp">
                        <label for="txtPassword">Password</label>
                        <div class="relative">
                            <span class="passWordView"><i class="fa fa-eye"></i></span>
                            
                        </div>
                    </div>
                    
                    <%--<div style="text-align: left">
                            <input data-val="true" data-val-required="The RememberMe field is required." id="RememberMe" name="RememberMe" type="checkbox" value="true"><input name="RememberMe" type="hidden" value="false">
                            Remember Me
                        </div>--%>
                    <div class="form-group" style="text-align: center">
                        
                        
                  
                        
                     <%--   <asp:Label ID="lblmac" runat="server"></asp:Label>--%>


                          </div>

               
            </div>
        </div>
    </div>
    
    
    <%--<script type="text/javascript" src="/assests/bootstrap/js/bootstrap.min.js"></script>--%>
    <script type="text/javascript" src="/assests/bootstrap/js/bootstrap.min.js"></script>
      <script type="text/javascript" src="/assests/js/main.js?v1.0"></script>
        <script type="text/javascript">
            const d = new Date();
            let year = d.getFullYear();
            document.getElementById("yearCP").innerHTML = year;
        </script>
</body>
</html>
