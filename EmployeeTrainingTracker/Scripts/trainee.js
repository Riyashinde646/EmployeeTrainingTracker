
// Common dialog message
function showMessage(message, title, reloadPage) {

    $("#messageDialog").attr("title", title);

    $("#messageText").text(message);

    $("#messageDialog").dialog({
        modal: true,
        width: 400,
        buttons: {
            OK: function () {

                $(this).dialog("close");

                if (reloadPage) {
                    location.reload();
                }
            }
        }
    });
}


// Save Trainee
$("#btnSaveTrainee").click(function () {

    $(".text-danger").text("");

    var name = $("#TraineeName").val();
    var email = $("#Email").val();
    var phone = $("#Phone").val();
    var password = $("#Password").val();
    var confirmPassword = $("#ConfirmPassword").val();

    var isValid = true;

    if (name == "") {
        $("#nameError").text("Enter trainee name");
        isValid = false;
    }

    if (email == "") {
        $("#emailError").text("Enter email");
        isValid = false;
    }

    if (phone == "") {
        $("#phoneError").text("Enter phone number");
        isValid = false;
    }

    if (password == "") {
        $("#passwordError").text("Enter password");
        isValid = false;
    }

    if (confirmPassword == "") {
        $("#confirmPasswordError").text("Confirm your password");
        isValid = false;
    }
    else if (password != confirmPassword) {
        $("#confirmPasswordError").text("Passwords do not match");
        isValid = false;
    }

    if (!isValid) {
        return;
    }

    var trainee = {
        TraineeName: name,
        Email: email,
        Phone: phone,
        Department: $("#Department").val(),
        Designation: $("#Designation").val(),
        JoiningDate: $("#JoiningDate").val(),
        Password: password
    };

    $.ajax({
        url: '/Trainee/SaveTrainee',
        type: 'POST',
        data: trainee,

        success: function (response) {

            if (response.success) {

                $("#addTraineeModal").modal("hide");

                showMessage(
                    response.message,
                    "Success",
                    true
                );

            }
            else {

                showMessage(
                    response.message,
                    "Error",
                    false
                );

            }
        },

        error: function () {

            showMessage(
                "Something went wrong.",
                "Error",
                false
            );

        }
    });

});


// Activate / Deactivate Trainee
$(document).on("click", ".btnStatus", function () {

    var userID = $(this).data("id");
    var isActive = $(this).data("status");

    $.ajax({
        url: '/Trainee/UpdateStatus',
        type: 'POST',

        data: {
            userID: userID,
            isActive: isActive
        },

        success: function (response) {

            if (response.success) {

                showMessage(
                    response.message,
                    "Success",
                    true
                );

            }
            else {

                showMessage(
                    response.message,
                    "Error",
                    false
                );

            }
        },

        error: function (xhr) {

            console.log("Status:", xhr.status);
            console.log("URL:", xhr.responseURL);
            console.log(xhr.responseText);

            showMessage(
                "Something went wrong.",
                "Error",
                false
            );

        }
    });

});


// Edit Trainee
$(document).on("click", ".btnEdit", function () {

    var userID = $(this).data("id");

    $.ajax({
        url: '/Trainee/GetTrainee',
        type: 'GET',

        data: {
            userID: userID
        },

        success: function (trainee) {

            $("#EditUserID").val(trainee.UserID);
            $("#EditTraineeName").val(trainee.TraineeName);
            $("#EditPhone").val(trainee.Phone);
            $("#EditDepartment").val(trainee.Department);
            $("#EditDesignation").val(trainee.Designation);

            $("#editTraineeModal").modal("show");

        },

        error: function () {

            showMessage(
                "Unable to get trainee details.",
                "Error",
                false
            );

        }
    });

});


// Update Trainee
$("#btnUpdateTrainee").click(function () {

    var trainee = {

        UserID: $("#EditUserID").val(),
        TraineeName: $("#EditTraineeName").val(),
        Phone: $("#EditPhone").val(),
        Department: $("#EditDepartment").val(),
        Designation: $("#EditDesignation").val()

    };

    $.ajax({

        url: '/Trainee/UpdateTrainee',
        type: 'POST',
        data: trainee,

        success: function (response) {

            if (response.success) {

                $("#editTraineeModal").modal("hide");

                showMessage(
                    response.message,
                    "Success",
                    true
                );

            }
            else {

                showMessage(
                    response.message,
                    "Error",
                    false
                );

            }
        },

        error: function () {

            showMessage(
                "Unable to update trainee.",
                "Error",
                false
            );

        }

    });

});


