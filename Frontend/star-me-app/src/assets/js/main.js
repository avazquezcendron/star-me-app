(function ($) {
  "use strict";

  $("a.page-scroll").bind("click", function (event) {
    var $anchor = $(this);
    $("html, body")
      .stop()
      .animate(
        {
          scrollTop: $($anchor.attr("href")).offset().top - 50,
        },
        1000
      );
    event.preventDefault();
  });

  $(window).scroll(function () {
    var scroll = $(window).scrollTop();

    // adjust scroll to top
    if (scroll >= 600) {
        $('.scroll-top').addClass('active');
    } else {
        $('.scroll-top').removeClass('active');
    }
    return false;
});

  $(document).ready(function () {
    // Target your .container, .wrapper, .post, etc.
    $(".post-list").fitVids();
  });
})(jQuery);
