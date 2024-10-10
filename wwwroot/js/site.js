$(document).ready(function () {
    // On form submit
    $("#RegisterForm").on("submit", function (event) {
        // Slight delay to wait for client-side validation to update
        setTimeout(function () {
            // Check for validation errors
            var hasErrors = $(".alert-outline-danger").length > 0;

            if (hasErrors) {
                // Show validation messages and alert box
                $(".alert-outline-danger").show();


                // Auto-hide alert after 5 seconds
                setTimeout(function () {
                    $(".alert-outline-danger").fadeOut('slow');
                }, 5000);
            }
        }, 100); // 100ms delay
    });

    $("#LoginForm").on("submit", function (event) {
        // Slight delay to wait for client-side validation to update
        setTimeout(function () {
            // Check for validation errors
            var hasErrors = $(".alert-outline-danger").length > 0;

            if (hasErrors) {
                // Show validation messages and alert box
                $(".alert-outline-danger").show();

                // Auto-hide alert after 5 seconds
                setTimeout(function () {
                    $(".alert-outline-danger").fadeOut('slow');
                }, 5000);
            }
        }, 100); // 100ms delay
    });
});

