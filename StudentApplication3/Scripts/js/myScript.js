// script 1 for create
document.getElementById("dob").addEventListener("change", function () {
    var dobInput = document.getElementById("dob").value;
    var dob = new Date(dobInput);
    var today = new Date();
    var errorMessage = document.getElementById("dobError");

    if (dob > today) {
        errorMessage.textContent = "Date of Birth cannot be greater than today.";
        document.getElementById("age").value = "";
    } else {
        errorMessage.textContent = "";
        var age = today.getFullYear() - dob.getFullYear();
        var monthDiff = today.getMonth() - dob.getMonth();
        if (monthDiff < 0 || (monthDiff === 0 && today.getDate() < dob.getDate())) {
            age--;
        }
        document.getElementById("age").value = age;
    }
});


$('#studentForm').submit(function (e) {
    e.preventDefault();
    if (!$("#studentForm").valid()) {
        return; // Prevent submission if the form is invalid
    }

    $.ajax({
        url: '@Url.Action("Create", "Student")',
        type: 'POST',
        data: $(this).serialize(),
        success: function (response) {
            if (response.success) {
                alert(response.message);
                window.location.href = '@Url.Action("Index", "Student")';
            } else {

            }
        },
        error: function () {
            alert('An error occurred. Please try again.');
        }
    });
});

// script 2 for create

$(document).ready(function () {
    $("#studentForm").validate({
        rules: {
            Name: {
                required: true,
                minlength: 3
            },
            Email: {
                required: true,
                email: true
            },
            DOB: {
                required: true,
                date: true
            },
            Gender: {
                required: true
            },
            center_id: {
                required: true
            }
        },
        messages: {
            Name: {
                required: "Please enter your name.",
                minlength: "Name must be at least 3 characters long."
            },
            Email: {
                required: "Please enter your email.",
                email: "Enter a valid email address."
            },
            DOB: {
                required: "Please select your Date of Birth.",
                date: "Enter a valid date."
            },
            Gender: {
                required: "Please select your gender."
            },
            center_id: {
                required: "Please select your CDAC Center."
            }
        }
    });


});

// Date of Birth Validation and Age Calculation
$("#dob").change(function () {
    var dobInput = $("#dob").val();
    var dob = new Date(dobInput);
    var today = new Date();
    var errorMessage = $("#dobError");

    if (dob > today) {
        errorMessage.text("Date of Birth cannot be in the future.");
        $("#age").val("");
    } else {
        errorMessage.text("");
        var age = today.getFullYear() - dob.getFullYear();
        var monthDiff = today.getMonth() - dob.getMonth();
        if (monthDiff < 0 || (monthDiff === 0 && today.getDate() < dob.getDate())) {
            age--;
        }
        $("#age").val(age);
    }
});


document.getElementById("txtName").addEventListener("input", function () {
    const nameInput = this.value;
    const nameError = document.getElementById("nameError");

    const namePattern = /^[A-Za-z\s]+$/; // Only letters and spaces

    if (nameInput.length < 3) {
        nameError.textContent = "Name must be at least 3 characters long.";
    } else if (!namePattern.test(nameInput)) {
        nameError.textContent = "Name can only contain letters and spaces.";
    } else {
        nameError.textContent = "";
    }
});

document.getElementById("studentForm").addEventListener("submit", function (e) {
    const nameInput = document.getElementById("txtName").value;
    const nameError = document.getElementById("nameError");

    if (nameInput.length < 3 || !/^[A-Za-z\s]+$/.test(nameInput)) {
        e.preventDefault(); // Prevent form submission
        alert("Please fix the name field errors before submitting.");
    }
});


