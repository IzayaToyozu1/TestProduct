
var idRequest = 0;

var headerBox = document.getElementsByClassName("headerChat");
var btSender = document.getElementById("btSender");

btSender.addEventListener("click", function () {
    var url = "/api/SetCommentRequest";

    var requestData = {
        idRequest: idRequest,
        comment: document.getElementById("tbMEssage").textContent
    };

    var requestOptions = {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(requestData)
    };

    fetch(url, requestOptions)
        .then(response => {
            if (!response.ok) {
                throw new Error(`HTTP error! Status: ${response.status}`);
            }
            return response.json();
        })
        .then(data => {
            console.log(data);
        })
        .catch(error => {
            console.error('Error:', error);
        });
});

geaderBox.addEventListener("click", function () {
    var url = "api"
})


