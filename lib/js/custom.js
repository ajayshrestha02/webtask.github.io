$(document).ready(function () {
  $('.tabs-stage .alltab').hide();

  // Change tab class and display content
  $('.tabs-nav a').on('click', function (event) {
    event.preventDefault();
    $('.tabs-nav li').removeClass('tab-active');
    $(this).parent().addClass('tab-active');
    $('.tabs-stage .alltab').hide();
    $($(this).attr('href')).show();
    $('html,body').animate({
      scrollTop: $(".tabs-stage").offset().top},
      'slow');
  });

  $('#demo a[href*="#"]').on('click', function (e) {
    $('.closebtn').trigger("click");
    $('html,body').animate({
         scrollTop: $($(this).attr('href')).offset().top },
         500);
        e.preventDefault();
  });
  $('.perception').on('click', function () {
    $("#perception").trigger("click");
  });
  $('.communication').on('click', function () {
    $("#communication").trigger("click");
  });
  $('.assessment').on('click', function () {
    $("#assessment").trigger("click");
  });
  $('.art').on('click', function () {
    $("#art").trigger("click");
  });
});
