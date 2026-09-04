$(document).ready(function () {


    $("#trainingDate").datepicker({
        dateFormat: "yy-mm-dd",
        minDate: 0
    });

    $("#searchDate").datepicker({
        dateFormat: "yy-mm-dd"
    });

    $("#startTime").timepicker({
        timeFormat: "HH:mm"
    });

    $("#endTime").timepicker({
        timeFormat: "HH:mm"
    });
   

    // Load Trainers
    $.ajax({
        url: '/Training/GetTrainers',
        type: 'GET',
        success: function (data) {

            $("#ddlTrainer").empty();
            $("#ddlTrainer").append('<option value="">Select Trainer</option>');

            $.each(data, function (i, trainer) {
                $("#ddlTrainer").append(
                    '<option value="' + trainer.TrainerID + '">' +
                    trainer.TrainerName +
                    '</option>'
                )
            });
        }
    });


    // Load Topics
    $.ajax({
        url: '/Training/GetTopics',
        type: 'GET',
        success: function (data) {

            $("#ddlTopic").empty();
            $("#ddlTopic").append('<option value="">Select Topic</option>');

            $.each(data, function (i, topic) {
                $("#ddlTopic").append(
                    '<option value="' + topic.TopicId + '">' +
                    topic.TopicName +
                    '</option>'
                );
            });
        }
    });

    // Load Trainees
    $.ajax({
        url: '/Training/GetAllTrainees',
        type: 'GET',

        success: function (data) {

            $("#trainees").empty();

            $.each(data, function (i, trainee) {

                $("#trainees").append(
                    '<div>' +
                    '<label>' +
                    '<input type="checkbox" name="trainee" value="' +
                    trainee.TraineeID + '" /> ' +
                    trainee.TraineeName +
                    '</label>' +
                    '</div>'
                );

            });
        }
    });


    // Load SubTopics
    $("#ddlTopic").change(function () {

        var topicId = $(this).val();

        $("#subTopics").empty();

        if (topicId == "") {
            return;
        }

        $.ajax({
            url: '/Training/GetSubTopics',
            type: 'GET',
            data: { topicId: topicId },

            success: function (data) {

                $.each(data, function (i, subTopic) {

                    $("#subTopics").append(
                        '<div>' +
                        '<label>' +
                        '<input type="checkbox" name="subTopic" value="' +
                        subTopic.SubTopicId + '" /> ' +
                        subTopic.SubTopicName +
                        '</label>' +
                        '</div>'
                    );

                });
            }
        });

    });

    //button for assigning training
    $("#btnAssignTraining").click(function () {

        console.log("Assign Training clicked");
        $("#scheduleId").val("0");

        $("#trainingModalTitle").text("Assign Training");
        $("#btnSaveTraining").text("Save Training");

        $("#ddlTrainer").val("");
        $("#ddlTopic").val("");
        $("#subTopics").empty();
        $("#trainees").empty();
        $("#trainingDate").val("");
        $("#startTime").val("");
        $("#endTime").val("");

        //load trainees
        $.ajax({
            url: '/Training/GetAllTrainees',
            type: 'GET',

            success: function (data) {

                console.log("Trainees:", data);

                $("#trainees").empty();

                $.each(data, function (i, trainee) {

                    $("#trainees").append(
                        '<div>' +
                        '<label>' +
                        '<input type="checkbox" name="trainee" value="' +
                        trainee.TraineeID + '" /> ' +
                        trainee.TraineeName +
                        '</label>' +
                        '</div>'
                    );

                });
            },

            error: function (xhr) {
                console.log("Trainee Error:", xhr.responseText);
            }
        });

    });

    // Save Training
    $("#btnSaveTraining").click(function () {

        var subTopicIds = [];

        $("input[name='subTopic']:checked").each(function () {
            subTopicIds.push(parseInt($(this).val()));
        });


        var traineeIds = [];

        $("input[name='trainee']:checked").each(function () {
            traineeIds.push(parseInt($(this).val()));
        });

        console.log("Selected Trainees:", traineeIds);


        var model = {
            ScheduleId: parseInt($("#scheduleId").val()),
            TrainerId: parseInt($("#ddlTrainer").val()),
            TopicId: parseInt($("#ddlTopic").val()),
            SubTopicIds: subTopicIds,
            TraineeIds: traineeIds,
            TrainingDate: $("#trainingDate").val(),
            StartTime: $("#startTime").val(),
            EndTime: $("#endTime").val()
        };

        var url = "";

        if (model.ScheduleId == 0) {
            url = '/Training/SaveTraining';
        }
        else {
            url = '/Training/UpdateTraining';
        }

        $.ajax({
            url: url,
            type: 'POST',
            data: model,

            success: function (response) {

                if (response.success) {

                    alert(response.message);

                    $("#assignTrainingModal").modal("hide");

                    // Clear form
                    $("#ddlTrainer").val("");
                    $("#ddlTopic").val("");
                    $("#subTopics").empty();
                    $("#trainingDate").val("");
                    $("#startTime").val("");
                    $("#endTime").val("");

                    // Reset ScheduleId

                    $("#scheduleId").val("0");

                    $("#trainingModalTitle").text("Assign Training");
                    $("#btnSaveTraining").text("Save Training");

                    // Refresh grid
                    $("#trainingGrid").trigger("reloadGrid");
                }
            },

            error: function () {

                alert("Error while saving training.");

            }
        });

    });

    

    // Overall Training Plan jqGrid
    $("#trainingGrid").jqGrid({
        url: '/Training/GetOverallTrainingPlan',
        datatype: "json",

        colModel: [
            {
                name: "ScheduleId",
                hidden: true
            },
            {
                name: "TrainerName",
                label: "Trainer",
                width: 120
            },
            {
                name: "TopicName",
                label: "Topic",
                width: 120
            },
            {
                name: "SubTopicName",
                label: "Sub Topics",
                width: 300,
                cellattr: function () {
                    return 'style="white-space: normal;"';
                }
            },
            {
                name: "TrainingDate",
                label: "Date",
                width: 100,
                formatter: function (cellvalue) {

                    if (!cellvalue) {
                        return "";
                    }

                    var date = new Date(
                        parseInt(
                            cellvalue.replace("/Date(", "").replace(")/", "")
                        )
                    );

                    return date.toLocaleDateString();
                }
            },
            {
                name: "StartTime",
                label: "Start Time",
                width: 100,
                formatter: function (cellvalue) {

                    if (!cellvalue) {
                        return "";
                    }

                    return cellvalue.Hours.toString().padStart(2, "0") +
                        ":" +
                        cellvalue.Minutes.toString().padStart(2, "0");
                }
            },
            {
                name: "EndTime",
                label: "End Time",
                width: 100,
                formatter: function (cellvalue) {

                    if (!cellvalue) {
                        return "";
                    }

                    return cellvalue.Hours.toString().padStart(2, "0") +
                        ":" +
                        cellvalue.Minutes.toString().padStart(2, "0");
                }
            },
            {
                name: "Action",
                label: "Action",
                width: 150,
                align: "center",
                formatter: function (cellvalue, options, rowObject) {

                    return '<button class="btn btn-sm btn-primary editTraining" ' +
                        'data-id="' + rowObject.ScheduleId + '">Edit</button> ' +

                        '<button class="btn btn-sm btn-danger deleteTraining" ' +
                        'data-id="' + rowObject.ScheduleId + '">Delete</button>';
                }
            }
        ],

        height: "auto",
        rowNum: 10,
        pager: "#trainingPager",
        viewrecords: true,
        autowidth: true
    });

});
    $(document).on("click", ".editTraining", function () {

        var scheduleId = $(this).data("id");

        $.ajax({
            url: '/Training/GetTrainingById',
            type: 'GET',
            data: { scheduleId: scheduleId },

            success: function (data) {

                console.log("Training Data:", data);

                $("#scheduleId").val(scheduleId);

                $("#trainingModalTitle").text("Edit Training");
                $("#btnSaveTraining").text("Save Changes");

                // Fill Trainer
                $("#ddlTrainer").val(data.TrainerId);

                // Fill Topic
                $("#ddlTopic").val(data.TopicId);

                // Load SubTopics
                $.ajax({
                    url: '/Training/GetSubTopics',
                    type: 'GET',
                    data: { topicId: data.TopicId },

                    success: function (subTopics) {

                        $("#subTopics").empty();

                        $.each(subTopics, function (i, subTopic) {

                            var checked = "";

                            if ($.inArray(subTopic.SubTopicId, data.SubTopicIds) !== -1) {
                                checked = "checked";
                            }

                            $("#subTopics").append(
                                '<div>' +
                                '<label>' +
                                '<input type="checkbox" name="subTopic" value="' +
                                subTopic.SubTopicId + '" ' + checked + ' /> ' +
                                subTopic.SubTopicName +
                                '</label>' +
                                '</div>'
                            );

                        });
                    }
                });

                // Fill Date
                var date = new Date(
                    parseInt(
                        data.TrainingDate.replace("/Date(", "").replace(")/", "")
                    )
                );

                var year = date.getFullYear();
                var month = String(date.getMonth() + 1).padStart(2, "0");
                var day = String(date.getDate()).padStart(2, "0");

                $("#trainingDate").val(year + "-" + month + "-" + day);

                // Fill Start Time
                $("#startTime").val(
                    data.StartTime.Hours.toString().padStart(2, "0") +
                    ":" +
                    data.StartTime.Minutes.toString().padStart(2, "0")
                );

                // Fill End Time
                $("#endTime").val(
                    data.EndTime.Hours.toString().padStart(2, "0") +
                    ":" +
                    data.EndTime.Minutes.toString().padStart(2, "0")
                );

                // Open modal
                $("#assignTrainingModal").modal("show");
            },

            error: function () {

                alert("Error while loading training.");

            }
        });

    });

var deleteScheduleId = 0;

$(document).on("click", ".deleteTraining", function () {

    deleteScheduleId = $(this).data("id");

    $("#deleteTrainingModal").modal("show");
});

$("#btnConfirmDelete").click(function () {

    $.ajax({
        url: '/Training/DeleteTraining',
        type: 'POST',
        data: { scheduleId: deleteScheduleId },

        success: function (response) {

            $("#deleteTrainingModal").modal("hide");

            $("#trainingGrid").trigger("reloadGrid");

            alert(response.message);
        },

        error: function () {

            $("#deleteTrainingModal").modal("hide");

            alert("Error deleting training.");
        }
    });
});

// Search Training Plan
$("#btnSearch").click(function () {

    var trainer = $("#searchTrainer").val();
    var topic = $("#searchTopic").val();
    var date = $("#searchDate").val();

    console.log("Trainer:", trainer);
    console.log("Topic:", topic);
    console.log("Date:", date);

    $("#trainingGrid").jqGrid("setGridParam", {
        postData: {
            trainer: trainer,
            topic: topic,
            date: date
        },
        page: 1
    }).trigger("reloadGrid");
});


// Clear Search
$("#btnClear").click(function () {

    $("#searchTrainer").val("");
    $("#searchTopic").val("");
    $("#searchDate").val("");

    $("#trainingGrid").jqGrid("setGridParam", {
        postData: {
            trainer: "",
            topic: "",
            date: ""
        },
        page: 1
    }).trigger("reloadGrid");
});