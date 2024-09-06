/*******************************************************************************************************************
Rev 1.0     Sanchita    V2.0.41     01/06/2023      FSm - Message will be fired from first tab when logged out from the 2nd tab
                                                    Refer: 26273
Rev 2.0     Pallab      V2.0.41     02/06/2023      "Session expired" message change and alert design modification for FSM
                                                    Refer: 26280
*********************************************************************************************************************/

// Rev 1.0
document.addEventListener("visibilitychange", () => {
    // it could be either hidden or visible
    if (document.visibilityState === 'visible') {
        checkSessionLogoutMasterPage();
    }
});

function checkSessionLogoutMasterPage() {
    $.ajax({
        type: "POST",
        url: "/MasterPopulate/checkSessionLogout",
        async: true,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if (response.SessionLoddedOut == '1') {
                /*Rev 2.0*/
                //jAlert('Session has expired !!!', 'Alert', function () {
                //    window.parent.location.href = '/oms/login.aspx';
                //});
                Swal.fire({
                    confirmButtonText: 'Login',
                    title: 'User Session has Expired!',
                    text: 'You have been logged out. Please log in again.',
                    allowOutsideClick: false,
                }).then((result) => {
                    if (result.isConfirmed) {
                        window.parent.location.href = '/oms/login.aspx';
                    }
                })
                /*Rev end 2.0*/
            }
        },
        error: function (response) {
            jAlert("Please try again later");
        }
    });
       
}
// End of Rev 1.0

$(document).ready(function () {
    "use strict";

    if ($('.bgImage').data('image') != '') {
       var dataBg = $('.bgImage').data('image');
       $('.bgImage').css({'background-image' : 'url(' + dataBg + ')' });
    }else {
       $('.bgImage').css({'background-image' : 'url(assets/img/bglogin.jpg)' });
    }


    // search field show hide
    $('#showSearch').on('click', function(e){
        e.preventDefault();
        if($(this).hasClass('clicked')){
            $(this).removeClass('clicked');
            $('.srcfield .text').removeClass('hide');
            $('.srcfield .srchinput').addClass('hide');
        }else{
          $(this).addClass('clicked');
          $('.srcfield .text').addClass('hide');
          $('.srcfield .srchinput').removeClass('hide');
          $('.srcfield .srchinput input[type="text"]').focus();
        }
    });


    function windowSizer() {
      var winH = $(window).height(),
          headerH = $('#header').height(),
          subH = winH - headerH ;
      $('.rightSide, .mainWraper').css({ 'min-height': subH });
    }
    windowSizer();
    $(window).resize(function(){
        windowSizer();
    });

    // Add body-small class if window less than 768px
    if ($(this).width() < 769) {
        $('body').addClass('body-small')
    } else {
        $('body').removeClass('body-small')
    }
    // Minimalize menu when screen is less than 768px
    $(window).bind("resize", function () {
        if ($(this).width() < 769) {
            $('body').addClass('body-small')
        } else {
            $('body').removeClass('body-small')
        }
    });
    // Minimalize menu
    $('.navbar-minimalize').click(function (e) {
        $("body").toggleClass("mini-navbar");
       
      
    });
    //$('.data-table').DataTable();
    $(function () {
      $('[data-toggle="tooltip"]').tooltip()
    });
    $('.Closer').click(function () {
        $('.rgth').toggleClass('full');
        $('.ltfg').toggleClass('close');
        if ($('.Closer').find('.fa').hasClass('fa-angle-right')) {
            $('.Closer ').find('.fa').removeClass('fa-angle-right').addClass('fa-angle-left');
            //alert('left');
        } else {
            $('.Closer .fa').addClass('fa-angle-right');
        }
    });


    //Menu function

    
    //mouseenter function
    var windowWidth = $(window).width();
    if (windowWidth > 768) {

       


        $('.seclevel>li').mouseenter(function (e) {
            $('.thrdlevel').hide();
            $('.seclevel>li').removeClass('active');
            $(this).addClass('active');
            $(this).find('.thrdlevel').show();
        });
    } else {
        $('.seclevel>li').click(function (e) {
            $('.thrdlevel').hide();
            $('.seclevel>li').removeClass('active');
            $(this).addClass('active');
            $(this).find('.thrdlevel').show();
        });
    }
    $('.sidenav>ul>li >a').click(function (e) {
        //e.preventDefault();
        //alert('first Level Clicked');
        $('.sidenav ul.dropdown-menu').hide();
        $('.sidenav>ul>li a').removeClass('active');
        $('.seclevel>li').removeClass('active');
        $(this).addClass('active');
        $(this).next('ul.dropdown-menu').show();
        $(this).next('ul.dropdown-menu').find('.seclevel li:first').find('.thrdlevel').show();
        $(this).next('ul.dropdown-menu').find('.seclevel li:first').addClass('active');
        $(this).addClass('active');
    });
    $('.sidenav>ul>li >a').click(function (e) {
        //e.preventDefault();
        //alert('first Level Clicked');
        //$('.sidenav ul.dropdown-menu').hide();
        $('.sidenav>ul>li a').removeClass('active');
        $('.seclevel>li').removeClass('active');
        $(this).addClass('active');
        $(this).next('ul.dropdown-menu').show();
        $(this).next('ul.dropdown-menu').find('.seclevel li:first').find('.thrdlevel').show();
        $(this).next('ul.dropdown-menu').find('.seclevel li:first').addClass('active');
        $(this).addClass('active');
    });
    
    //$('.sidenav').mouseleave(function () {
    //    $('.sidenav ul.dropdown-menu').hide();
    //});
    
    $('.seclevel>li>a').click(function () {
        
        if ($(this).hasAttr("href")) {
            
        } else {
            alert('hi');
            //do something
            $(this).closest('ul.dropdown-menu').show();
            $(this).next('ul.dropdown-menu').show();
        }
    });
    $('.rightSide, #header').click(function (e) {
        $('.sidenav ul.dropdown-menu').hide();
        $('.sidenav>ul>li a').removeClass('active');
    });

    //Link hover url showing disabled
    //setTimeout(function () {

    //    $('.sidenav a[href]').each(function () {
    //        var href = this.href;

    //        $(this).removeAttr('href').css('cursor', 'pointer').click(function () {
    //            if (href.toLowerCase().indexOf("#") >= 0) {

    //            } else {
    //                window.open(href, '_self');
    //            }
    //        });
    //    });

    //}, 500);
    if (!$('.form_main').hasClass('clearfix')) {
        $('.form_main').addClass('clearfix');
    }
    if ($(".scrollHorizontal").length) {
        $(".scrollHorizontal").mCustomScrollbar({
            axis: "x",
            theme: "rounded-dots",
            scrollInertia: 5,
            autoExpandScrollbar: true,
            advanced: { autoExpandHorizontalScroll: true }
        });
    }

    //rev 25361
    $('body').on('click', '.toggleFullScreen', function (e) {
        $(this).parent(".box-full").toggleClass("full-screen");
    });
    //rev end 25361
    
});