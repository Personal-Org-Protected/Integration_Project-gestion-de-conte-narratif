// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

import { get } from "jquery";

// Write your JavaScript code.




//$(document).ready(function () {
//    let $Span = $('#Quotes')
//    let Url = 'https://zenquotes.io/api/quotes '
//    let $footer = $('footer #errorMsg')
//    function addElmt(elemt) {
//        if (elemt == null) { alert("data not found"); }
//        $Span.append("<p>" + elemt.q + "</p>");
//        $Span.append("<br />");
//        $Span.append("<h5>" + elemt.a +"</h5>");
//    };
//    $('#Trigger').on('click',  function () {

//        $.ajax({
//            type: 'GET',
//            url: Url,
//            dataType: 'json',
//            success:  function (response) {
//                addElmt(response);
//            },
//            error:  function (result) {
//                alert('error loading : ' + result.fail);
//                $footer.append(" error : " + result.status + "<br />" + result.fail + "<br />" + "<br />" + result);

//            }
//        });
//    });


//});

//$(document).ready(function () {
//    let $Span = $('#Quotes')
//    let Url = 'https://quotes15.p.rapidapi.com/quotes/random/'
//    let $footer = $('footer #errorMsg')
//    function addElmt(elemt) {
//        if (elemt == null) { alert("data not found"); }
//        $Span.append("<p>" + elemt.content + "</p>");
//        $Span.append("<br />");
//        $Span.append("<h5>" + elemt.originator.name + "</h5>");
//    };
//    $('#Trigger').on('click', function () {

//        const settings = {
//            "async": true,
//            "crossDomain": true,
//            "url": "https://quotes15.p.rapidapi.com/quotes/random/",
//            "method": "GET",
//            "headers": {
//                "X-RapidAPI-Host": "quotes15.p.rapidapi.com",
//                "X-RapidAPI-Key": "26d658afa9msh2390603ead6f874p1b48fdjsn369ee6b4749f"
//            }
//        };

//        $.ajax(settings).done(function (response) {
//            addElmt(response);
//        });
//    });


//});

//$(document).ready(function () {
//    let $Span = $('#Quotes')
//    let Url = 'https://quotes15.p.rapidapi.com/quotes/random/'
//    let $footer = $('footer #errorMsg')
//    function addElmt(elemt) {
//        if (elemt == null) { alert("data not found"); }
//        $Span.append("<p>" + elemt.content + "</p>");
//        $Span.append("<br />");
//        $Span.append("<h5>" + elemt.originator.name + "</h5>");
//    };
//    $('#Trigger').on('click', function () {

//        $.ajax({
//            async: true,
//            crossDomain: true,
//            url: Url,
//            method: "GET",
//            headers: {
//                "X-RapidAPI-Host": "quotes15.p.rapidapi.com",
//                "X-RapidAPI-Key": "26d658afa9msh2390603ead6f874p1b48fdjsn369ee6b4749f"
//            },
//            success: function (response) {
//                addElmt(response);
//            },
//            error:  function (result) {
//                alert('error loading : ' + result.fail);
//                $footer.append(" error : " + result.status + "<br />" + result.fail + "<br />" + "<br />" + result);

//            }

//        })
//    });


//});