// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.



//$(document).ready(function () {
//    $('#ShopTable').DataTable({
//        searching: true,
//        paging: true,
//        pageLength: 10,
//        // Add additional DataTables options as needed
//    });
//});




CommonPopup = (targetUrl, modalTitle) => {
    $.ajax({
        method: "GET",
        url: targetUrl,
        success: function (response) {
            $("#unique-modal .custom-body").html(response);
            $("#unique-modal .custom-title").html(modalTitle);
            $("#unique-modal").modal('show');
            $("#unique-modal .CloseUniqueModel").click(function () {
                $("#unique-modal").modal('hide');
            });
        }
    });
}





