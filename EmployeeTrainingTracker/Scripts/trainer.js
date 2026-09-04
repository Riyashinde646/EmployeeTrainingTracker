
//save trainer 

$(document).ready(function () {

    $("#btnSaveTrainer").click(function () {

        var trainer = {
            TrainerName: $("#TrainerName").val(),
            Email: $("#Email").val(),
            Phone: $("#Phone").val(),
            Password: $("#Password").val(),
            ConfirmPassword: $("#ConfirmPassword").val()
        };

        if (trainer.TrainerName == "") {
            $("#nameError").text("Trainer name is required.");
            return;
        }

        if (trainer.Email == "") {
            $("#emailError").text("Email is required.");
            return;
        }

        if (trainer.Password == "") {
            $("#passwordError").text("Password is required.");
            return;
        }

        if (trainer.Password != trainer.ConfirmPassword) {
            $("#confirmPasswordError").text("Passwords do not match.");
            return;
        }

        $.ajax({
            url: '/Trainer/Save',
            type: 'POST',
            data: trainer,

            success: function (response) {

                if (response.success) {

                    alert(response.message);

                    $("#addTrainerModal").modal("hide");

                    location.reload();
                }
                else {
                    alert(response.message);
                }
            },

            error: function () {
                alert("Something went wrong.");
            }
        });

    });

});

// update status 
$(document).on("click", ".btnStatus", function () {

    var userID = $(this).data("id");
    var status = $(this).data("status");

    $.ajax({
        url: '/Trainer/UpdateStatus',
        type: 'POST',
        data: {
            userID: userID,
            isActive: status
        },

        success: function (response) {

            if (response.success) {
                alert(response.message);
                location.reload();
            }
            else {
                alert(response.message);
            }
        },

        error: function () {
            alert("Something went wrong.");
        }
    });

});

// get user id for editing 
$(document).on("click", ".btnEdit", function () {

    var userID = $(this).data("id");

    $.ajax({
        url: '/Trainer/GetTrainer',
        type: 'GET',
        data: { userID: userID },

        success: function (trainer) {

            $("#EditUserID").val(trainer.UserID);
            $("#EditTrainerName").val(trainer.TrainerName);
            $("#EditEmail").val(trainer.Email);
            $("#EditPhone").val(trainer.Phone);

            $("#editTrainerModal").modal("show");
        },

        error: function () {
            alert("Unable to load trainer details.");
        }
    });

});

// editing the trainer 

$("#btnUpdateTrainer").click(function () {

    var trainer = {
        UserID: $("#EditUserID").val(),
        TrainerName: $("#EditTrainerName").val(),
        Email: $("#EditEmail").val(),
        Phone: $("#EditPhone").val()
    };

    $.ajax({
        url: '/Trainer/EditTrainer',
        type: 'POST',
        data: trainer,

        success: function (response) {

            if (response.success) {
                alert(response.message);

                $("#editTrainerModal").modal("hide");

                location.reload();
            }
            else {
                alert(response.message);
            }
        },

        error: function () {
            alert("Something went wrong.");
        }
    });

});