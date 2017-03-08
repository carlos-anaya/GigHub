var GigsController = function (attendanceService) {

    var button;

    var init = function (container) {
        // This will create a function instance for each .js-toggle-attendance element
        // Moreover, if new elements are added they will not have a function instance

        // $(".js-toggle-attendance").click(toggleAttendance);

        $(container).on("click", ".js-toggle-attendance", toggleAttendance);
    };

    var toggleAttendance = function (e) {
        button = $(e.target);
        var gigId = button.attr("data-gig-id");

        if (button.hasClass("btn-default"))
            attendanceService.createAttendance(gigId, done, fail);
        else
            attendanceService.deleteAttendance(gigId, done, fail);
    };

    var done = function () {
        var text = button.text() === "Going?" ? "Going!" : "Going?";
        button.toggleClass("btn-info").toggleClass("btn-default").text(text);
    };

    var fail = function () {
        alert("Something failed!");
    };

    return {
        init: init
    };

}(AttendanceService);