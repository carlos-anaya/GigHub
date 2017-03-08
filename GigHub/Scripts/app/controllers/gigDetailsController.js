var GigDetailsController = function (followingsService) {
    var button;

    var init = function (container) {
        $(container).on("click", ".js-toggle-following", toggleFollowing);
    };

    var toggleFollowing = function (e) {
        button = $(e.target);
        var followeeId = button.attr("data-followee-id");

        if (button.hasClass("btn-info"))
            followingsService.follow(followeeId, done, fail);
        else
            followingsService.unfollow(followeeId, done, fail);
    };

    var done = function () {
        var text = button.text() === "Follow!" ? "Unfollow" : "Follow!";
        button.toggleClass("btn-info").toggleClass("btn-warning").text(text);
    };

    var fail = function () {
        alert("Something failed");
    };

    return {
        init: init
    };

}(FollowingsService);