
<%--====================================================== Revision History ===========================================================
Rev Number         DATE              VERSION          DEVELOPER           CHANGES
1.0                14-01-2023        2.0.38           Pallab              Design change if we provide wrong password: fix 
2.0                07-04-2023        2.0.39           Pallab              25805 : TEAM BEHIND and theme change features add in FSM login page
3.0                24-04-2023        2.0.39           Pallab/Sanchita     25861 : Event banner should dynamically change according to the date
4.0                06-06-2023        2.0.41           Pallab              26302 : FSM portal login page make responsive and mobile friendly
5.0                11-07-2023        2.0.42           Pallab              26550 : login page "Invalid UserID or Password!" visibility issue fix for small device
6.0                07-10-2024        2.0.49           Priti               27754 : Copyright 2024 Indus Net Technologies [2.0.49] It is to be changed to   Copyright 2024 Breeze [2.0.49]
7.0                09-10-2024        V2.0.49          Pallab/Sanchita     027763: Portal login page download APK functionality add   
====================================================== Revision History ===========================================================--%>

<%@ Page Language="C#" AutoEventWireup="true" Inherits="pLogin"
    EnableEventValidation="false" CodeBehind="Login.aspx.cs" %>

<!DOCTYPE html>
<html>
<head>
    <%--REV 6.0--%>
    <%--<title>Login to BreezeERP</title>--%>
    <title>Login to BreezeFSM</title>
    <%--REV 6.0 END--%>
    <meta name="description" content="" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />

    <link href="https://fonts.googleapis.com/css2?family=Open+Sans:wght@400;500;700;800&family=Playfair+Display:wght@900&display=swap" rel="stylesheet">

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
            padding:50px 0 60px;
            text-align:center;
            background: #e7eaff;
        }
        .fts img {
            width:50px;
            /*margin-bottom:25px;
            margin-top:20px*/
        }
        .fts h2 {
            font-family: 'Montserrat', sans-serif;
            font-size: 35px;
            line-height: normal;
            font-weight: 700;
            letter-spacing: -1px;
            color: rgba(255,91,103,1);
            margin-bottom: 3.5rem;
        }
            .fts h2 span {
                font-family: 'Open Sans', sans-serif;
                font-size: 25px;
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
            font-size: 18px;
            line-height: 25px;
            margin-bottom: 15px;
            letter-spacing: -.08rem;
            padding: 0 0.5rem;
            margin-top: 15px;
        }
        .login-about-box p
        {
            color: #4a4a4a;
        }
    </style>
    
    
    
    <%--<script src="/assests/pluggins/amchart/core.js"></script>--%>
    <%--<script src="/assests/pluggins/amchart/charts.js"></script>--%>
    <%--<script src="/assests/pluggins/amchart/themes/animated.js"></script>--%>
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
        /*Rev 2.0*/
        .screenLight .contentArea {
            width: 65%;
            overflow: hidden;
            position: relative;
            text-align: center;
            /*Rev Pallab*/
            /*background: #333 url('/assests/images/NLogin/LoginDark_bg.png') no-repeat top left;*/
            background: url('/assests/images/NLogin/LoginDark_bg2.jpg') no-repeat top left;
            /*Rev end Pallab*/
            background-size:cover;
        }
        .screenDark .contentArea
        {
            width: 65%;
            /*overflow: hidden;
            position: relative;
            text-align: center;
            background: url('/assests/images/NLogin/LoginDark_bg2.jpg') no-repeat top left;
            background-size:cover;*/
        }

        .screenDark .flexArea
        {
            background: url('/assests/images/NLogin/fsm-dark-bg.jpg') no-repeat top left;
            background-size:cover;
            position: relative;
            text-align: center;
        }
        .screenLight .formArea {
            width: 35%;
        }
        .screenDark .formArea
        {
            width: 35%;
            /*height: 95vh;*/
            /*position: absolute;
            right: 20px;
            top: 2.5vh;
            background: #fff;
            border-radius: 14px;*/
            text-align:left !important;
        }
        /*Rev end 2.0*/
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
            /*Rev Pallab*/
            /*background: #ebebeb;*/
            background: #fff;
            display: flex;
            align-items: center;
            justify-content: center;
            /*Rev end Pallab*/
            /*Rev 1.0*/
            flex-direction: column;
            /*Rev end 1.0*/
        }
        .formBox {
            margin-top: 20px;
            font-family: poppins !important;
        } 
        .formBox .form-group label {
            /*font-family: poppins;*/
            font-family: 'Open Sans', sans-serif;
            color: #686868;
        }
        .formBox .form-group {
            position:relative
        }
        .inputIcons {
            /*max-width: 14px;
            position: absolute;
            left: 15px;
            top: 38px;*/
                max-width: 20px;
                position: absolute;
                left: 10px;
                top: 37px;
        }
        .formBox .form-group input {
            border:none;
            background:#fff !important;
            border-radius:4px;
            min-height:43px;
            padding-left:40px;
            border: 1px solid #dddddd;
            font-family: 'Open Sans', sans-serif;
        }
        input:-webkit-autofill,
        input:-webkit-autofill:hover,
        input:-webkit-autofill:focus,
        input:-webkit-autofill:active {
            -webkit-box-shadow: 0 0 0 30px white inset !important;
        }
        .formBox .form-group input:focus {
            border: 1px solid #e54a55;
        }
        .loginbtn{
           /* background:#6834d4;*/
            background: #e54a55;
            color: #fff;
            padding: 10px 15px;
            margin-top: 20px;
            transition: all .3s;
            font-family: 'Open Sans', sans-serif;
        }
            .loginbtn:hover {
                box-shadow: 0px 5px 5px rgba(0,0,0,0.22);
                color: #fff;
                background: #c7333d;
            }
            .loginbtn:focus{
                color: #fff;
                outline: none;
            }
        .ftFooter {
            font-size:12px;
            margin-top: 20px;
            font-family: 'Open Sans', sans-serif;
            text-align: center;
            color: #4a4a4a;
        }
        .mlogos {
            width:100px;
            /*margin-left:-8px*/
            margin-left: 0;
            /*position: absolute;
            right: 10px;
            top: 10px;*/
            margin-bottom: 15px;
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
        /*.spaceColumn{
            padding: 60px 0;
        }*/
        
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
            font-size: 32px;
            line-height: normal;
            font-weight: 700;
            letter-spacing: -1px;
            /*color: rgba(255,91,103,1);*/
            color: #fff;
            margin-bottom:1.5rem;
        }
        .mkHs {
            font-family: 'Montserrat', sans-serif;
            font-size: 22px;
            color: #ffffff;
            margin-bottom:3rem;
        }
        .mkBoxes h5, .mkBoxes p {
            font-family: 'Open Sans', sans-serif;
        }
        .mkBoxes h5
        {
            font-size: 18px;
            color: #fff;
        }
        .mkBoxes p 
        {
                font-size: 15px;
                color: #ddd;
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
            /*display: none;*/
            border-radius: 10px;
            margin-top: 15px;
            width: 300px;
            /*height: 135px;*/
            margin-left:auto;
            margin-right: auto;
            margin-bottom: 15px;
            /*overflow: hidden;*/
        }

            .event-img img {
                border-radius: 10px;
                box-shadow: 1px 1px 10px #11111160;
                width: 100%;
            }

            .left-top-image {
            max-width: 68%;
            margin-top: 4%;
        }
            .login-about-box
            {
                padding: 15px 15px 15px;
                background: #fff;
                border-radius: 15px;
                box-shadow: 1px 1px 15px #0000001c;
            }
            .icon-img
            {
                width: 80px;
                height: 80px;
                border-radius: 50px;
                background: #d7e4ff;
                margin: auto;
                text-align: center;
                line-height: 80px;
            }

            .app-section
            {
                padding: 60px 0;
                max-width: 100%;
                padding: 0 15px;
                background: #08638d !important;
            }

            .mobile-image-part
            {
                background: url(/assests/images/NLogin/mobile-app-mockup.jpg) top center;
                min-height: 680px;
                background-size:cover;
                background-repeat:no-repeat;
            }

            .mb-5
            {
                margin-bottom: 30px;
            }

            .right-text-part
            {
                padding: 50px 120px 50px 50px;
            }

            .right-text-part h5{
                margin-bottom: 5px;
            }

            /*Rev 2.0*/

            .creditsSec {
                position: absolute;
                left: 20px;
                bottom: 20px;
                z-index: 109;
            }

            .droper {
                position: absolute;
                bottom: 90%;
                color: #fff;
                background: #17158f;
                min-width: 250px;
                border-radius: 11px;
                overflow: hidden;
                display:none;
                opacity: 0;
                -webkit-transition: all 0.3s ease-in-out;
                transition: all 0.3s ease-in-out;
                box-shadow: 0px 0px 15px rgb(0 0 0 / 20%);
            }

                .droper.active {
                    display:block;
                    opacity: 1;
                    bottom: 120%;
                }

            .mnHeader {
                padding: 7px 15px;
                border-bottom: 1px solid #0b1322;
                font-size: 1.6rem;
                font-weight: 500;
                text-transform: uppercase;
            }

            .scrCnter {
                padding: 8px 12px;
                max-height: 300px;
                overflow-y: auto;
            }

                .scrCnter ul {
                    list-style-type: none;
                    padding: 0;
                }

                    .scrCnter ul > li {
                        padding: 7px 0 7px 7px;
                        border-bottom: 1px solid #273958;
                    }

                    .scrCnter ul > li.crhdss {
                            background: #e2f5e7;
                            font-weight: 500;
                            border-bottom: 1px dashed;
                        }

                    #clCr {
                float: right;
                cursor: pointer;
            }

        .secToggler {
            width: 40px;
            height: 40px;
            text-align: center;
            border: 1px solid #9ab2ef;
            border-radius: 50%;
            display: flex;
            justify-content: center;
            align-items: center;
            transition: all 0.3s ease-in;
            cursor: pointer;
            background: #fff;
        }

        .secToggler i {
            font-size: 23px;
            cursor: pointer;
            display: inline-block;
        }

        .secToggler:hover {
            background: #e7eaff;
            border-color: #fff;
            box-shadow: 0 0 0 3px #288fce, 0 0 0 4px rgb(255 255 255 / 20%);
        }


        /*switch*/
        #switchArea {
            position: absolute;
            bottom: 11px;
            left: 60px;
            z-index: 9;
            padding: 10px;
        }
        .switch {
              position: relative;
                display: inline-block;
                width: 40px;
                height: 26px;
            }

            .switch input { 
              opacity: 0;
              width: 0;
              height: 0;
            }

            .slider {
                position: absolute;
                cursor: pointer;
                top: 0;
                left: 0;
                right: 0;
                bottom: 0;
                background-color: #c9c9c9;
                -webkit-transition: .4s;
                transition: .4s;
                border: 1px solid #163f5f;
            }

            .slider:before {
              position: absolute;
                content: "";
                height: 18px;
                width: 18px;
                left: 4px;
                bottom: 3px;
                background-color: #133e5e;
                -webkit-transition: .4s;
                transition: .4s;
            }

            input:checked + .slider, input:checked:focus + .slider {
              background-color: #f7f96a;
            }
            
            input:focus + .slider {
              background-color: #fff;
            }

            input:checked + .slider:before {
                  -webkit-transform: translateX(14px);
    -ms-transform: translateX(14px);
    transform: translateX(14px);
            }

            /* Rounded sliders */
            .slider.round {
              border-radius: 24px;
            }

            .slider.round:before {
              border-radius: 50%;
            }
            .spEmpa {
                font-weight: 600;
                color: #3a5ac6;
                font-family: 'Playfair Display', serif;
            }
            .screenDark  .spEmpa {
                color: #fff !important;
                border-bottom: 3px solid #dbdb2b;
            }
            .screenDark  .mainlist li {
                color:#fff
            }

            .img-visible{
                display: block !important;
            }

            .img-hide{
                display: none !important;
            }

            /*Rev end 2.0*/

            /*Rev 4.0*/

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
            /*.creditsSec#rc_app_1562 {
                display:none !important

            }*/
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
                 /*height:auto;*/
                 height:1000px;
                 width:100%  !important;
                 flex-direction: column-reverse;
             }
            .contentArea {
                /*display:none;*/
                width: 100% !important;
                /*height: 80%;*/
            }
            .formArea {
                width:100% !important;
                /*display:block;*/
                height:100%  !important;
                padding: 30px 15px;
            }
            .responsiveImg {
                max-width:100%
            }
            .fts h2, .mkHd {
                font-size:26px;
                margin-bottom: 20px;

            }
            .fts h2 span {
                font-size:22px;
            }
            .spaceColumn {
                padding: 7px 0;
            }

            .fts
            {
                padding: 30px 0 60px;
            }

            .login-about-box
            {
                margin-bottom: 15px;
            }

            .right-text-part
            {
                padding: 30px 20px 50px 20px;
            }

            .resp-pad-left-right
            {
                padding-left: 10px !important;
                padding-bottom: 10px !important;
            }

            .creditsSec , #switchArea
            {
                display: none;
            }
        }
         /*Rev end 4.0*/

         #lblMessage
         {
             text-align: center;
             display: block;
             font-size: 16px;
         }

         .apk-download-btn {
    position: fixed;
    top: 28%;
    right: 0;
    padding: 10px 15px;
    background: #5b0cb4;
    border-radius: 12px 0 0 12px;
    z-index: 99999;
    transition: right 0.4s ease, width 0.4s ease; /* Smooth transition */
    width: 55px; /* Initially only show the icon */
    overflow: hidden; /* Hide text */
    white-space: nowrap; /* Prevent text wrapping */
    color: #fff !important;
    font-size: 16px;
}

    .apk-download-btn:focus
    {
        outline: none !important;
    }

    .apk-download-btn:hover {
        width: 175px; /* Expand to full button width on hover */
        right: 0; /* Keep it fixed at the right */
    }

    .apk-download-btn img {
        width: 25px;
        vertical-align: middle;
        margin-right: 10px;
        /*transition: transform 0.4s ease;*/
    }

    .apk-download-btn:hover img {
        transform: rotate(360deg); /* Optional icon spin effect on hover */
    }

    .apk-download-btn span {
        opacity: 0; /* Initially hide text */
        transition: opacity 0.4s ease; /* Smooth fade-in effect */
    }

    .apk-download-btn:hover span {
        opacity: 1; /* Show text when hovered */
    }

         /*Rev 5.0*/
         @media  only screen and (min-width: 767px) and (max-width: 1320px)
         {
             .mlogos
             {
                 margin-bottom: 0px;
             }

             .event-img
             {
                 margin-bottom: 10px;
             }

             .formBox
             {
                 margin-top: 12px;
             }

             .form-group {
                margin-bottom: 10px;
            }

             .loginbtn
             {
                 margin-top: 15px;
             }
             .ftFooter
             {
                 margin-top: 8px;
             }
             .left-top-image
             {
                 margin-top: 2%;
             }
         }
         /*Rev end 5.0*/
    </style>
    
    <script>
        $(document).ready(function () {
            $(".secToggler").on("click", function () {
                $(".droper").toggleClass("active");
            });
            $("#clCr").click(function () {
                $(".droper").removeClass("active");
            });
        });
    </script>

    <script type="text/javascript">
        $(function () {
            $(".switch input").click(function () {
                var input = $(".switch input").is(':checked');
                var bodyClass = $("#themeClass").hasClass("screenDark");
                var theme = localStorage.getItem('theme');

                //alert(bodyClass)
                if (bodyClass) {
                    $("#themeClass").removeClass("screenDark").addClass("screenLight");
                    localStorage.setItem('theme', 'screenLight');
                } else {
                    $("#themeClass").removeClass("screenLight").addClass("screenDark");
                    localStorage.setItem('theme', 'screenDark');
                }
                /*Rev 2.0*/
                //image toogle
                //if ($('.light-design').hasClass('img-hide')) {
                //    $('.light-design').removeClass('img-hide');
                //    $('.dark-design').addClass('img-hide');
                //}
                //else {
                //    $('.dark-design').removeClass('img-hide');
                //    $('.light-design').addClass('img-hide');
                //}
                /*Rev end 2.0*/
            })
        })
        //window.addEventListener('load', function (event) {
        //    var theme = localStorage.getItem('theme').toString();
        //    console.log(theme)
        //    if (theme != '' || theme != undefined) {
        //        $("#themeClass").attr('class', '').addClass(theme);
        //    } else {
        //        $("#themeClass").addClass('screenDark');
        //    }

        //});
    </script>
</head>
<body onload="noBack();setInterval('blinkIt()',500);" onpageshow="if (event.persisted) noBack();" onunload="">

   

    <%--Rev 2.0--%>
    <div id="switchArea" class="">
        <label class="switch">
          <input type="checkbox" checked />
          <span class="slider round"></span>
        </label>
    </div>
    <%--Rev end 2.0--%>
    <%--Rev 2.0 : "themeClass" id and "screenLight" class add --%>
    <div id="themeClass" class="mainLogin screenLight">
         <div class="flexArea">
             <div class="contentArea img-wrapper">
                 <%--<div class="tagLine"><h3>Field Sales Management</h3><h1 class="emp">Make Your Field Agents FUTURE READY!</h1><h4>Simple, Intuitive and Easy to use</h4></div>
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
                 </div>--%>
                 <img src="/assests/images/NLogin/left-top-image.png" class="left-top-image light-design"  />
                 <%--Rev 2.0 : img add--%>
                 <%--<img src="/assests/images/NLogin/fsm-left-lgt-design.png" class="left-top-image dark-design img-hide" />--%>
                 <%--Rev end 2.0--%>
             </div>
            
             <div class="formArea">
                
                      <%--<div><asp:Label ID="lblMessage" runat="server" Text="" ForeColor="Red"></asp:Label></div>--%>
                     <div><img src="/assests/images/NLogin/logo.png" class="mlogos" /></div>
                 <div class="login-part">
                    <div class="event-img">
                        <%--Rev 3.0 : id="EV1" & runat="server" added--%>
                         <%--<img src="/assests/images/Events/event-banner.jpg" />--%>   
                         <img id="EV1" runat="server" src="" />
                        <%--End of Rev 3.0--%>
                     </div>
                     <div style="margin: 5px 0 10px 0; font-size: 22px; color: #3737bb; font-weight: 600; font-family: 'Open Sans', sans-serif; text-align: center; ">Login to your Account</div>
                     <%--<p style="font-family: poppins;">A single dashboard to manage all your team members with real time updates and notifications.</p>--%>
                     <div><asp:Label ID="lblMessage" runat="server" Text="" ForeColor="Red"></asp:Label></div>
                    <form action="" method="post" runat="server" novalidate="novalidate">
                    <input id="rurl" name="rurl" runat="server" type="hidden" value="" />
                     <div class="formBox">
                         <div class="form-group">
                             <label for="username">Username</label>
                             <%--<img src="/assests/images/NLogin/user.png" class="inputIcons" />--%>
                             <img src="/assests/images/NLogin/user-2.png" class="inputIcons" />
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
                             <%--<img src="/assests/images/NLogin/password.png" class="inputIcons" />--%>
                              <img src="/assests/images/NLogin/password-2.png" class="inputIcons" />
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
                         <div style="font-size:12px; font-family: 'Open Sans', sans-serif; color: #4a4a4a; text-align: center;">Application best viewed at <a href="#">1280 x 720</a> resolution in <a href="#">Google Chrome</a> 59 or above</div>
                         <asp:Button ID="Submit1" ValidationGroup="login" runat="server" CssClass="btn btn-block loginbtn" Text="Submit" OnClick="Login_User" TabIndex="3" />
						<asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" TabIndex="4" CssClass="compemail hide" OnClick="LinkButton1_Click1">Forgot  Password?</asp:LinkButton>

                          <%--Rev 7.0--%>
                            <%--<a class="btn btn-primary" href="/assests/ITC_v_4.5.3_LIVE.apk">Download APK</a>--%>
                            <asp:LinkButton ID="lnlDownloaderapk" runat="server" OnClick="lnlDownloaderapk_Click" CssClass="btn apk-download-btn"><img src="/assests/images/android-icon.png" class="" /> Download APK</asp:LinkButton>
                                <%--<button ID="lnlDownloaderAPK" runat="server" OnClick="lnlDownloaderapk_Click" Class="btn apk-download-btn">
                                    <img src="/assests/images/android-icon.png" class="" /> Download APK
                                </button>--%>
                            <%--End of Rev 7.0--%>

                         <div class="ftFooter">
                            <%-- REV 6.0--%>
                             <%--© Copyright <span id="yearCP"></span> Indus Net Technologies--%>
                              © Copyright <span id="yearCP"></span> Breeze 
                             <%-- REV 6.0 END--%>
                             <asp:Label ID="lblVersion" runat="server" Text="1.0.4" /> 
                            <a href="Management/Master/view-version-features.aspx" target="_blank" style="display:none">( What's New )</a><br />
                            
                         </div>

                            

                     </div>
                    </form>
                     </div>
                </div>
         </div>

        <%--Rev 2.0 : TEAM BEHIND part add--%>
        <!--Credit-->
         <div class="creditsSec " id="rc_app_1562">
            <div class="secToggler" id="rc_app_15652"><img src="/assests/images/group.png" style="width:28px" /></div>
            <div class="droper">
                <div class="mnHeader">Team Behind <span id="clCr"><i class="fa fa-close"></i></span></div>
                <div class="scrCnter" id="rc_app_156655">
                    <ul class="tc_s5621f_fg">
                        
                        <li><span class="nameCR">Abhinav Dahiwade</span></li>
                        <li><span class="nameCR">Abhishek Munshi</span></li>
                        <li><span class="nameCR">Ananya Deb</span></li>
                        <li><span class="nameCR">Ankan Das</span></li>
                        <li><span class="nameCR">Arindam Ghosal</span></li>
                        <li><span class="nameCR">Arunabha Saha</span></li>
                        <li><span class="nameCR">Ashmita Roy Chowdhury</span></li>
                        <li><span class="nameCR">Avijit Bonu</span></li>
                        <li><span class="nameCR">Bapi Dutta</span></li> 
                        <li><span class="nameCR">Bhaskar Chatterjee</span></li>
                        <li><span class="nameCR">Chinmoy Maiti</span></li>
                        <li class="tc_sfsf_fg"><span class="nameCR">Debashis Talukder</span></li>
                        <li><span class="nameCR">Debjyoti Dhar</span></li>
                        <li><span class="nameCR">Deep Narayan Mahajan</span></li>
                        <li><span class="nameCR">Goutam Kumar Das</span></li>
                        <li><span class="nameCR">Indranil Dey</span></li>
                        <li><span class="nameCR">Jitendra Jha</span></li>
                        <li><span class="nameCR">Kaushik Gupta</span></li>
                        <li><span class="nameCR">Maynak Nandi</span></li>
                        <li><span class="nameCR">Pallab Mukherjee</span></li>
                        <li><span class="nameCR">Pijush Kumar Bhattacharya</span></li>
                         <li><span class="nameCR">Pratik Ghosh</span></li>
                        <li class="tc_s5221f_fg"><span class="nameCR">Priti Ghosh</span></li>
                        <li><span class="nameCR">Priyanka</span></li>
                        <li><span class="nameCR">Rajdip Mukherjee</span></li>
                        <li><span class="nameCR">Saheli Bhattacharya</span></li>
                        <li><span class="nameCR">Saikat Das</span></li> 
                        <li><span class="nameCR">Sanchita Saha</span></li>
                        <li><span class="nameCR">Sanjoy Ganguly</span></li>
                        <li><span class="nameCR">Santanu Mukherjee</span></li>
                        <li><span class="nameCR">Sayantani Mandal</span></li>
                        <li><span class="nameCR">Shantanu Saha</span></li>
                        <li><span class="nameCR">Sneha das</span></li>
                        <li><span class="nameCR">Sourav Goswami</span></li>
                        <li><span class="nameCR">Subhra Mukherjee</span></li>
                        <li><span class="nameCR">Sudip Biswas</span></li>
                        <li><span class="nameCR">Sudip Kumar Pal</span></li>
                        <li><span class="nameCR">Suman Bachar</span></li>
                        <li><span class="nameCR">Suman Roy</span></li>  
                        <li><span class="nameCR">Surojit Chatterjee</span></li>
                        <li><span class="nameCR">Susanta Kundu</span></li>
                        <li><span class="nameCR">Suvankar Dey</span></li> 
                        <li><span class="nameCR">Swatilekha Mukherjee</span></li>   
                        <li><span class="nameCR">Tanmoy Ghosh</span></li>
                        
                        
                    </ul>
                </div>
            </div>
        </div>
        <%--Rev end 2.0--%>
     </div>
    <div class="fts">
             <div class="container">
                 <h2 class="">
                     Automate Processes. Manage Teams. <br>Acquire Leads. Close Sales.
                     <span>All-in-One Platform</span>
                 </h2>
             <div class="row spaceColumn">
                 
                 <div class="col-md-4 wow slideInLeft" data-wow-duration="2s" data-wow-delay="5s">
                     <div class="login-about-box">
                        <div class="icon-img"><img src="/assests/images/NLogin/007-destination.png" /></div>
                        <div class="hdn">Geo-Tracking & Route  <br /> Optimization</div>
                        <p>System will intelligently capture geolocation at attendance, and will be allowed if it falls in Distribution or first visit location.</p>
                     </div>
                 </div>
                 <div class="col-md-4 wow slideInLeft" data-wow-duration="2s" data-wow-delay="7s">
                     <div class="login-about-box">
                        <div class="icon-img"><img src="/assests/images/NLogin/004-user-interface.png" /></div>
                        <div class="hdn">IMEI/OTP based  <br /> authentication</div>
                        <p>Each user will be locked with this phone IMEI and will not be able to login on another device without admin action.</p>
                     </div>
                 </div>
                 <div class="col-md-4 wow slideInLeft" data-wow-duration="2s" data-wow-delay="9s">
                     <div class="login-about-box">
                         <div class="icon-img"><img src="/assests/images/NLogin/001-dashboard.png" /></div>
                         <div class="hdn">Intuitive Dashboard & <br /> Auto-Reporting</div>
                         <p>A single dashboard to manage all your team members with real time updates and notifications for updates and voilations.</p>
                     </div>
                 </div>
             </div>
         </div>
     </div>
    <div class="app-section">
        <%--<div class="container">--%>
            <div class="row">
                <div class="col-md-6 mobile-image-part"> 
                   <%-- <img src="/assests/images/NLogin/loginPh.png" class="responsiveImg" />--%>
                    <%--<img src="/assests/images/NLogin/mobile-app-mockup.jpg" class="responsiveImg image-fluid" />--%>
                </div>
                <div class="col-md-6 right-text-part">
                    <h2 class="mkHd"> Make Your Field Agents FUTURE READY!</h2>
                    <h4 class="mkHs">Accelerate Sales. Escalate Revenue.</h4>

                    <div class="row mkBoxes">
                        <div class="col-sm-12 mb-5">
                            <h5>Smart Route Management</h5>
                            <p>Stop worrying about planning .Automated route suggestion as per locations to be visited. Time saved is revenue earned.</p>
                        </div>
                        <div class="col-sm-12 mb-5">
                            <h5>One-Click OrderManagement</h5>
                            <p>Hassle-free order creation through a single click. Eliminates onsite visits for order acquisition & saves time real time.</p>
                        </div>
                        <div class="col-sm-12 mb-5">
                            <h5>Automated Reporting</h5>
                            <p>Automatically generate reports periodically to know what is going on and take decisions real time.</p>
                        </div>
                        <div class="col-sm-12 mb-5">
                            <h5>Travel Reimbursement Management</h5>
                            <p>No more hassles of having multiple portals for filling reimbursements. Single interface for all kinds of updates with status tracking.</p>
                        </div>
                    </div>
                </div>
            </div>
        <%--</div>--%>
    </div>
    <div>
        <div class="container text-center resp-pad-left-right" style="padding:50px 0">
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
