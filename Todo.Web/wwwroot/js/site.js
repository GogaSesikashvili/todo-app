// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

suspendInterval = false;

setInterval(function () {
    if (!suspendInterval) {
        Update()
    }
}, 5000);

function Update() {
    $.get("/Todo/Reminder", function (reminders) {
        if (reminders != null) {
            suspendInterval = true;

            let output = "";

            reminders.forEach(function (reminder) {
                var desc = "";

                if (reminder.description != null) {
                    desc = reminder.description;
                }

                output += `
            <div class="toast" role="alert" aria-live="assertive" aria-atomic="true" data-bs-delay="15000">
                    <div class="toast-header">
                        <strong class="me-auto">${reminder.title}</strong>
                        <button type="button" class="btn-close" data-bs-dismiss="toast" aria-label="Close"></button>
                    </div>
                <a href="/Todo/ViewEntry/${reminder.id}">
                    <div class="toast-body">
                        ${desc}
                    </div>
                </a>
            </div>`
            });

            document.querySelector(".toast-container").innerHTML = output;

            $('.toast').toast('show');

            setTimeout(() => {
                suspendInterval = false;
            }, 17000);
        }
    })
}