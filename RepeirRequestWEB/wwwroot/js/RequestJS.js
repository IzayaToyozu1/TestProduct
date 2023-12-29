<script>
    document.addEventListener('DOMContentLoaded', function () {

        let dictionaryRequst = {
        number: '№ заявки:',
    dateSubmission: 'Дата подачи:',
    nameObject: 'Объект:',
    cabinet: 'Кабинет:',
    ip_address: 'IP адрес:',
    fio: 'ФИО:',
    cName: 'Вид работ:',
    resName: 'Исполнитель:',
    dateEnd: 'Дата выполнения:',
    countHour: 'Продолжительность:',
    nameStatus: 'Статус:',
    strComment: 'Комментарий:',
    countDoc: 'Кол-во файлов:'
            };


    fetch("/api/GetRequestAll?start=8-11-2020&end=1-1-2023&typeService=1&withAccess=true")
                .then(response => response.json())
                .then(data => {
        data.forEach(function (item) {
            var table = document.getElementById('dataRequests');
            var elementRequest = document.createElement('div');
            elementRequest.classList.add('request');
            elementRequest.id = item.id;
            for (let key in item) {
                if (key in dictionaryRequst) {

                    var elementRequestItem = document.createElement('div');
                    elementRequestItem.classList.add('requestItem');
                    elementRequestItem.id = 'element' + key;

                    var elementRequestNameText = document.createElement('div');
                    elementRequestNameText.classList.add('valueItemReques');
                    elementRequestNameText.textContent = dictionaryRequst[key];

                    var elementRequestValue = document.createElement('div');
                    elementRequestValue.classList.add('valueItemReques');
                    elementRequestValue.textContent = item[key];

                    elementRequest.insertAdjacentElement('afterbegin', elementRequestItem);
                    elementRequestItem.insertAdjacentElement('afterbegin', elementRequestValue);
                    elementRequestItem.insertAdjacentElement('afterbegin', elementRequestNameText);
                }

                table.insertAdjacentElement('afterbegin', elementRequest);
            }
        });
        });
    function toggleSidebar() {
    var sidebar = document.getElementById("sidebar");
    var content = document.querySelector(".content");

    if (sidebar.style.width === "250px") {
        sidebar.style.width = "0";
    content.style.marginLeft = "0";
    } else {
        sidebar.style.width = "250px";
    content.style.marginLeft = "250px";
    }
}
</script>