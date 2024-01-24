
var idRequest = 0;
var FIOUser = "";
var dataCommentRequestJson;
var dataRequest;

var headerBox = document.getElementsByClassName("headerChat");
var btSender = document.getElementById("btSender");

document.addEventListener("DOMContentLoaded", function () {

    fetch("api/GetRequest")
        .then(response => response.json())
        .(data => {

            dataRequest = data;
        })
        .catch(error => {
            console.error('Ошибка:', error)
        });
});

function GetDataRequest() {
   
}

function 

function SetCommentToform() {
    var messagePanel = document.getElementsByClassName("messagePanel");

    for (var comment of dataCommentRequestJson) {
        var def = "message-right";
        if (comment.FIO == FioUser)
            def = "message - left"
        messagePanel.innerHTML = "<div class=\"messageBox " + def + "\">" +
            "< div class=\"message\"> " + comment.Comment + "</div >" +
            "<div class=\"messageInfo\">" + comment.FIO + " " + comment.DateCreate + "</div></div > ";
    } 

btSender.addEventListener("click", async function () {
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

headerBox.addEventListener("click", async function () {
    var url = "/api/GetCommentRequest";

    var requestData = {
        idRequestRepair: idRequest
    };

    // Добавление параметров запроса к URL
    var params = new URLSearchParams(requestData);
    url = url + '?' + params.toString();

    // Опции запроса
    var requestOptions = {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json'
        }
    };

    try {
        var response = await fetch(url, requestOptions);

        if (!response.ok) {
            throw new Error(`HTTP error! Status: ${response.status}`);
        }

        dataCommentRequestJson = await response.json().parse();

        SetCommentToform();
        console.log(data);
    } catch (error) {
        console.error('Error:', error);
    }
});