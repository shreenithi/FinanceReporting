function FromSearch()
{
    var customerName = $('#customerName').val();
    var zipCode = $('#zipCode').val();
    if (customerName == "" && zipCode=="") {
        alert("Enter User ID");
        return false;
    }
    $.ajax({
        type: 'POST',
        url:'Login/gettingDataFromSearch',
        data: {
            'customerName': customerName,
            'zipCode':zipCode
        },
        
        success: function (msg) {
        alert(msg);
    }
    })
}
//(string customerName, int zipCode