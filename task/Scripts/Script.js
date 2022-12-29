
function Delete(url) {
    if (confirm('Are You sure to delete this record ?') == true) {
        $.ajax({
            type: 'POST',
            url: url,
            success: function (response) {
                alert("success")
                //$.ajax({
                //    type: 'POST',
                //    url: "~/Student/Index"
                //});
                 window.location = 'https://localhost:44302/Student/Index';
            },
            error: function () {
                    
            }
        });
    }
}
