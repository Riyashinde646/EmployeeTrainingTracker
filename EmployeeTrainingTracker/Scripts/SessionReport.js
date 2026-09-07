$(document).ready(function () {

    $("#sessionGrid").jqGrid({
        url: "/SessionReport/GetSessionReports",
        datatype: "json",

        colModel: [
            {
                name: "TopicName",
                label: "Topic",
                width: 150
            },
            {
                name: "SubTopicName",
                label: "Subtopic",
                width: 250,
                formatter: function (cellValue) {
                    return "<div style='white-space: normal; word-wrap: break-word;'>" +
                        cellValue +
                        "</div>";
                }
            },
            {
                name: "SessionDone",
                label: "Session Done",
                width: 120,
                formatter: function (cellValue) {
                    return cellValue ? "Yes" : "No";
                }
            },
            {
                name: "SessionFeedback",
                label: "Session Feedback",
                width: 200
            },
            {
                name: "Resources",
                label: "Resources",
                width: 200
            },
            {
                name: "SessionId",
                hidden: true
            },
            {
                name: "ScheduleId",
                hidden: true
            }
        ],

        pager: "#sessionPager",
        rowNum: 10,
        viewrecords: true,
        height: "auto",
        autowidth: true
    });

});