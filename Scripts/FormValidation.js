$(document).ready(function () {

    $.validator.addMethod("emailOrPhone", function (value, element) {
        return this.optional(element) ||
            /^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,6}$/.test(value) ||
            /^\d{10}$/.test(value);
    }, "Enter valid email or phone");

    for (let i = 1; i <= 31; i++) {
        $('#ddlDay').append($('<option>', { value: i, text: i }));
    }
    for (let j = 1; j <= 12; j++) {
        $('#ddlMonth').append($('<option>', { value: j, text: j }));
    }
    const currentYear = new Date().getFullYear();
    for (let k = currentYear; k >= 1900; k--) {
        $('#ddlYear').append($('<option>', { value: k, text: k }));
    }

    $.validator.addMethod("checkDate", function (value, element) {
        const day = parseInt($('#ddlDay')).val();
        const month = parseInt($('#ddlMonth')).val();
        const year = parseInt($('#ddlYear')).val();

        if (day === 0 || month === 0 || year === 0) return false;
        const date = new Date(year, month - 1, day);
        return (date.getFullYear() === year && date.getMonth() === month - 1 && date.getDate() === day)
    }, "Please choose a valid Date of Birth.");

    $('#ddlDay, #ddlMonth, #ddlYear').change(function () {
             const day = parseInt($('#ddlDay').val());
             const month = parseInt($('#ddlMonth').val());
             const year = parseInt($('#ddlYear').val());

        if (day > 0 && month > 0 && year > 0) {

            const today = new Date();
            let dob = new Date(year, month - 1, day);

        if (dob.getFullYear() === year && dob.getMonth() === month - 1) {

            let age = today.getFullYear() - dob.getFullYear();

             const m = today.getMonth() - dob.getMonth();

            if (m < 0 || (m === 0 && today.getDate() < dob.getDate())) {
                age--;

            }
            $('#txtAge').val(age)
        }
        else{
             $('#txtAge').val('');
            }
        }
    });



    $('#form2').validate({
        errorClass: "error", 
        errorElement: "span",

    });

            

    $("#txtusername").rules("add", {
        required: true,
        emailOrPhone: true,
        messages: { emailOrPhone: "Input value should be valid phone number or Email" }
    });

    $("#password").rules("add", {
        required: true,
        minlength: 6
    });

    $("#cnfrmpass").rules("add", {
        required: true,
        equalTo: "#password",
        messages: { equalTo: "Passwords do not match" }
    });

    $("#fn").rules("add", { required: true, minlength:3, maxlength: 50 });
    $("#ln").rules("add", { required: true, maxlength: 50 });
    $("#dn").rules("add", { maxlength: 50 });

    $("#address").rules("add", { required: true, maxlength: 255 });

    $("#zip").rules("add", { required: true, digits: true, maxlength: 6, minlength: 6 });
    $("#phone").rules("add", { required: true, digits: true, maxlength: 10, minlength: 10 });
    $("#mobile").rules("add", { digits: true, maxlength: 10, minlength: 10 });

    $("#ddlYear").rules("add", {
        checkDate: true,
        messages: { checkDate: "Please choose a valid Date of Birth." }
    });
});