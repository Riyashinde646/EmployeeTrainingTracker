$("#btnSaveTrainee").click(function () {  //save trainee button and ajax

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
                alert(response.message);

                $("#addTraineeModal").modal("hide");

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

$(".btnStatus").click(function () { // chnaging active decative status call

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

$(".btnEdit").click(function () { // ajax call for edit and opening the modal popup

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
            alert("Unable to get trainee details.");
        }
    });

});

$("#btnUpdateTrainee").click(function () { //for actualling saving changes

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
                alert(response.message);
                $("#editTraineeModal").modal("hide");
                location.reload();
            }
            else {
                alert(response.message);
            }
        },

        error: function () {
            alert("Unable to update trainee.");
        }
    });

});