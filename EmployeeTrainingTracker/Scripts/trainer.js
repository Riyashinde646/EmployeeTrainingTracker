console.log("trainer.js loaded");


// =========================
// COMMON MESSAGE DIALOG
// =========================

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


// =========================
// SAVE TRAINER
// =========================

$(document).ready(function () {

    $("#btnSaveTrainer").click(function () {

        $(".text-danger").text("");

        var name = $("#TrainerName").val().trim();
        var email = $("#Email").val().trim();
        var phone = $("#Phone").val().trim();
        var password = $("#Password").val();
        var confirmPassword = $("#ConfirmPassword").val();

        var isValid = true;


        // Trainer Name

        if (name == "") {

            $("#nameError").text("Enter trainer name.");

            isValid = false;
        }


        // Email

        if (email == "") {

            $("#emailError").text("Enter email.");

            isValid = false;
        }
        else if (!/^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(email)) {

            $("#emailError").text("Enter a valid email.");

            isValid = false;
        }


        // Phone

        if (phone == "") {

            $("#phoneError").text("Enter phone number.");

            isValid = false;
        }
        else if (!/^\d{10}$/.test(phone)) {

            $("#phoneError").text("Phone number must be 10 digits.");

            isValid = false;
        }


        // Password

        if (password == "") {

            $("#passwordError").text("Enter password.");

            isValid = false;
        }
        else if (password.length < 6) {

            $("#passwordError").text(
                "Password must be at least 6 characters."
            );

            isValid = false;
        }


        // Confirm Password

        if (confirmPassword == "") {

            $("#confirmPasswordError").text(
                "Confirm your password."
            );

            isValid = false;
        }
        else if (password != confirmPassword) {

            $("#confirmPasswordError").text(
                "Passwords do not match."
            );

            isValid = false;
        }


        // Stop if validation fails

        if (!isValid) {
            return;
        }


        var trainer = {

            TrainerName: name,
            Email: email,
            Phone: phone,
            Password: password,
            ConfirmPassword: confirmPassword

        };


        $.ajax({

            url: '/TrainerManagement/Save',

            type: 'POST',

            data: trainer,


            success: function (response) {

                if (response.success) {

                    $("#addTrainerModal").modal("hide");

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

});


// =========================
// ACTIVATE / DEACTIVATE
// =========================

$(document).on("click", ".btnStatus", function () {

    var userID = $(this).data("id");

    var status = $(this).data("status");


    $.ajax({

        url: '/TrainerManagement/UpdateStatus',

        type: 'POST',

        data: {

            userID: userID,
            isActive: status

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


        error: function () {

            showMessage(
                "Something went wrong.",
                "Error",
                false
            );

        }

    });

});


// =========================
// GET TRAINER FOR EDIT
// =========================

$(document).on("click", ".btnEdit", function () {

    var userID = $(this).data("id");


    $.ajax({

        url: '/TrainerManagement/GetTrainer',

        type: 'GET',

        data: {
            userID: userID
        },


        success: function (trainer) {

            $("#EditUserID").val(trainer.UserID);

            $("#EditTrainerName").val(
                trainer.TrainerName
            );

            $("#EditEmail").val(
                trainer.Email
            );

            $("#EditPhone").val(
                trainer.Phone
            );


            // Clear old validation messages

            $(".edit-error").text("");


            $("#editTrainerModal").modal("show");

        },


        error: function (xhr) {

            console.log(xhr.responseText);

            showMessage(
                "Unable to get trainer details.",
                "Error",
                false
            );

        }

    });

});


// =========================
// UPDATE TRAINER
// =========================

$(document).on("click", "#btnUpdateTrainer", function () {

    $(".edit-error").text("");


    var name =
        $("#EditTrainerName").val().trim();

    var email =
        $("#EditEmail").val().trim();

    var phone =
        $("#EditPhone").val().trim();


    var isValid = true;


    // Trainer Name

    if (name == "") {

        $("#EditTrainerName")
            .closest(".form-group")
            .find(".edit-error")
            .text("Enter trainer name.");

        isValid = false;
    }


    // Email

    if (email == "") {

        $("#EditEmail")
            .closest(".form-group")
            .find(".edit-error")
            .text("Enter email.");

        isValid = false;
    }
    else if (!/^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(email)) {

        $("#EditEmail")
            .closest(".form-group")
            .find(".edit-error")
            .text("Enter a valid email.");

        isValid = false;
    }


    // Phone

    if (phone == "") {

        $("#EditPhone")
            .closest(".form-group")
            .find(".edit-error")
            .text("Enter phone number.");

        isValid = false;
    }
    else if (!/^\d{10}$/.test(phone)) {

        $("#EditPhone")
            .closest(".form-group")
            .find(".edit-error")
            .text(
                "Phone number must be 10 digits."
            );

        isValid = false;
    }


    // Stop if validation fails

    if (!isValid) {
        return;
    }


    var trainer = {

        UserID: $("#EditUserID").val(),

        TrainerName: name,

        Email: email,

        Phone: phone

    };


    $.ajax({

        url: '/TrainerManagement/EditTrainer',

        type: 'POST',

        data: trainer,


        success: function (response) {

            if (response.success) {

                $("#editTrainerModal").modal("hide");

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