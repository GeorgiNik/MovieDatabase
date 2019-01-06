(function (movieDbApp, $, undefined) {

    //Public Property
    movieDbApp.showPostConfig = {
        rateMovieUrl: '',
    };

    //Public Method
    movieDbApp.init = function () {
        initRatedEvents();
    };

    //Private Method
    function rateMovie(rating) {
        var postId = $("#postId").val();
        var url = movieDbApp.showPostConfig.rateMovieUrl;
        var token = $('input[name="__RequestVerificationToken"]').val();

        $.ajax({
            url: url,
            type: "post",
            data: { __RequestVerificationToken: token, postId: postId, rating: rating },
            success: function (data) {
                $('.rating-value').html(data.overallRating.toFixed(1));
            },
            error: function (jqXHR, textStatus, errorThrown) {
                console.log(textStatus, errorThrown);
            }
        });
    }

    function initRatedEvents() {
        $(document).on('movieRated', function (e, ratingValue) {
            rateMovie(ratingValue);
        });
    }

}(window.movieDbApp = window.movieDbApp || {}, jQuery));