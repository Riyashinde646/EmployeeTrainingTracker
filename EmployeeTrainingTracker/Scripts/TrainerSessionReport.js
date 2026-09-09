
$(document).ready(function () {

    $("#sessionGrid").jqGrid({
        url: "/Trainer/TrainerSessionReport/GetSessionReports",
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
                width: 250
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
                width: 250
            },
            {
                name: "Resources",
                label: "Resources",
                width: 200
            },
            {
                name: "Action",
                label: "Action",
                width: 100,
                sortable: false,
                formatter: function (cellValue, options, rowObject) {

                    console.log("ROW OBJECT:", rowObject);
                    console.log("SESSION ID:", rowObject.SessionId);

                    return "<button type='button' " +
                        "class='btn btn-primary btn-sm' " +
                        "onclick='openReport(" + rowObject.SessionId + ")'>" +
                        "Report</button>";
                }
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


function showFeedback() {

    $("#feedbackSection").show();

    $("#reasonSection").hide();

    $("#sessionReason").val("");
}


function showReason() {

    $("#feedbackSection").hide();

    $("#reasonSection").show();

    $("#sessionFeedback").val("");
}


function openReport(sessionId) {

    $("#reportSessionId").val(sessionId);

    // Reset Yes / No

    $("input[name='sessionCompleted']").prop("checked", false);

    // Reset textboxes

    $("#sessionFeedback").val("");
    $("#sessionReason").val("");

    // Hide both sections initially

    $("#feedbackSection").hide();
    $("#reasonSection").hide();

    // Keep resource reset as it was

    $("#sessionResource").val("");

    $("#reportModal").modal("show");
}


function submitReport() {

    console.log("SUBMIT REPORT CLICKED");

    var sessionId = $("#reportSessionId").val();
    console.log("Session ID:", sessionId);


    // Check whether Yes or No is selected

    var selectedValue =
        $("input[name='sessionCompleted']:checked").val();

    if (!selectedValue) {

        alert("Please select Yes or No.");

        return;
    }


    var sessionDone = selectedValue === "yes";

    console.log("Session Done:", sessionDone);


    // Get feedback if Yes
    // Get reason if No

    var sessionFeedback = "";

    if (sessionDone) {

        sessionFeedback = $("#sessionFeedback").val();

        console.log("Feedback:", sessionFeedback);

    }
    else {

        sessionFeedback = $("#sessionReason").val();

        console.log("Reason:", sessionFeedback);

    }


    // Existing resource code - unchanged

    var file = $("#sessionResource")[0].files[0];

    console.log("File:", file);


    var formData = new FormData();

    formData.append("sessionId", sessionId);
    formData.append("sessionDone", sessionDone);
    formData.append("sessionFeedback", sessionFeedback);


    if (file) {
        formData.append("sessionResource", file);
    }


    console.log("FormData created");


    $.ajax({
        url: "/Trainer/TrainerSessionReport/SaveSessionReport",
        type: "POST",
        data: formData,
        contentType: false,
        processData: false,

        success: function (response) {

            console.log("Response from server:", response);

            if (response.success) {

                alert("Session report saved successfully.");

                $("#reportModal").modal("hide");

                $("#sessionGrid").trigger("reloadGrid");
            }
            else {
                alert("Unable to save session report.");
            }
        },

        error: function (xhr) {

            console.log("AJAX ERROR:", xhr.responseText);

            alert("Error while saving session report.");
        }
    });

}

