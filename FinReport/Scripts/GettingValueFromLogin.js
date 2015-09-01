function ValidateUser() {
    var userid = $('#loginName').val();
    var userpwd = $('#loginPassword').val();
    if (userid == "") {
        alert("Enter User ID");
        return false;
    }
    else if (userpwd == "") {
        alert("Enter Password");
        return false;
    }

    //validate user
    $.ajax({
        type: 'POST',
        url: 'Login/gettingData',
        data: {
            'loginName': userid,
            'loginPassword': userpwd
        },
        success: function (msg) {
            alert(msg);
        }
    });


}