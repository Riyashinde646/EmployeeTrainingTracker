$(document).ready(function () {

    $("#assessmentGrid").jqGrid({
        url: "/Assessment/GetAssessments",
        datatype: "json",

        colModel: [

            {
                name: "TraineeName",
                label: "Trainee",
                width: 120
            },

            {
                name: "TopicName",
                label: "Topic",
                width: 120
            },

            {
                name: "SubTopicName",
                label: "Subtopic",
                width: 150
            },

            {
                name: "AssignmentDone",
                label: "Assignment Done",
                width: 120,
                formatter: function (cellValue) {
                    return cellValue ? "Yes" : "No";
                }
            },

            {
                name: "TestConducted",
                label: "Test Conducted",
                width: 120,
                formatter: function (cellValue) {
                    return cellValue ? "Yes" : "No";
                }
            },

            {
                name: "TestMarks",
                label: "Test Marks",
                width: 100
            },

            {
                name: "IndividualFeedback",
                label: "Individual Feedback",
                width: 200
            },

            {
                name: "AssessmentId",
                hidden: true
            },

            {
                name: "ScheduleId",
                hidden: true
            },

            {
                name: "TraineeId",
                hidden: true
            },

            {
                name: "SubTopicId",
                hidden: true
            }
        ],

        pager: "#assessmentPager",

        rowNum: 10,

        viewrecords: true,

        height: "auto",

        autowidth: true
    });

});