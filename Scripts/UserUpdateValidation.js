$(document).ready(function () {
    if ($('#formup').length > 0) {
        $.validator.addMethod("validDobTextbox", function (value, element) {
            if (this.optional(element)) return true;

            const parts = value.split('/');
            if (parts.length !== 3) return false;

            const day = parseInt(parts[0], 10);
            const month = parseInt(parts[1], 10) - 1;
            const year = parseInt(parts[2], 10);

            const dob = new Date(year, month, day);
            const today = new Date();
            today.setHours(0, 0, 0, 0);

            return (
                dob.getFullYear() === year &&
                dob.getMonth() === month &&
                dob.getDate() === day &&
                dob <= today &&
                year >= 1900
            );
        }, "Enter a valid Date of Birth (dd/MM/yyyy)");

        $.validator.addMethod("checkGender", function (value, element) {
            if (this.optional(element)) return true;
            return /^(male|female)$/i.test(value.trim());
        }, "Gender must be Male or Female");

        $('#formup').validate({
            ignore: ":hidden",
            errorClass: "error",
            errorElement: "span"
        });


        $("#txtFn").rules("add", {
            required: true,
            minlength: 3,
            maxlength: 50,
            messages: { required: "First Name is required" }
        });

        $("#txtLn").rules("add", {
            required: true,
            maxlength: 50,
            messages: { required: "Last Name is required" }
        });

        $("#txtDn").rules("add", {
            maxlength: 50
        });

        $("#txtDob").rules("add", {
            required: true,
            validDobTextbox: true
        });

        $("#txtGender").rules("add", {
            required: true,
            checkGender: true
        });

        $("#txtAddress").rules("add", {
            required: true,
            minlength: 10,
            maxlength: 255
        });

        $("#txtZip").rules("add", {
            required: true,
            digits: true,
            minlength: 6,
            maxlength: 6
        });

        $("#txtPhone").rules("add", {
            digits: true,
            minlength: 10,
            maxlength: 10
        });

        $("#txtMobile").rules("add", {
            digits: true,
            minlength: 10,
            maxlength: 10
        });
    }
});