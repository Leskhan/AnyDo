let arrow = document.querySelectorAll('.arrow');
let subIcons = document.querySelectorAll('.sub-icons');

for (var i = 0; i < arrow.length; i++) {
    arrow[i].addEventListener("click", (e) => {
        let arrowParent = e.target.parentElement.parentElement;
        arrowParent.classList.toggle("showMenu");
        //console.log(arrowParent.childNodes[1]);

        let children = arrowParent.childNodes[1].childNodes[5].childNodes;
        children[1].classList.toggle("show");
        children[3].classList.toggle("show");
    });
}

// API

let selectedTask = {}
const taskUri = 'api/task';
const listUri = 'api/list';
var taskList = [];
var nameList = [];

function getTasks() {
    fetch(taskUri, {
        method: 'GET',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json',
        },
    })
        .then(response => response.json())
        .then(data => {
            taskList = data;
            
            for (let i = 0; i < data.length; i++) {
                _displayTasks(data[i]);
            }
        });

}

function getLists() {
    fetch(listUri, {
        method: 'GET',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json',
        },
    })
        .then(response => response.json())
        .then(data => {
            nameList = data;
            //console.log(nameList);
            
            for (let i = 0; i < data.length; i++) {
                _displayList(data[i]);
                _displayListsEditModal(data[i]);
                _displayListModal(data[i]);
            }
        });
}

function addList() {
    var newListName = document.getElementById("new-list-name").value;

    var newList = {
        name: newListName
    };

    fetch(listUri, {
        method: 'POST',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(newList)
    })
        .then(() => {
            location.reload();
        });

    addListModal.style.visibility = "hidden";
    newListName.value = "";

    //console.log(newList.name);
}

function _displayTasks(task) {
    const cardTaskList = document.getElementById('task-list');
    
    var taskListItem = document.createElement("div");
    taskListItem.setAttribute("class", "task-list-item");
    taskListItem.setAttribute("onclick", `showSelectedTask(${task.id})`);
    //console.log(taskListItem);

    var inputTaskId = document.createElement("input");
    inputTaskId.setAttribute("type", "hidden")
    inputTaskId.value = task.id;
    //console.log(inputTaskId);

    var inputTaskIsCompleted = document.createElement("input");
    inputTaskIsCompleted.setAttribute("type", "checkbox");
    inputTaskIsCompleted.setAttribute("onclick", `updateTaskStatus('input', '${task.id}', ${task.isCompleted})`);
    inputTaskIsCompleted.checked = task.isCompleted;

    var taskName = document.createElement("div");
    taskName.setAttribute("class", "task-name");
    taskName.innerText = task.name;

    taskListItem.appendChild(inputTaskId);
    taskListItem.appendChild(inputTaskIsCompleted);
    taskListItem.appendChild(taskName);

    cardTaskList.appendChild(taskListItem);
}

function _displayList(list) {
    const subMenuList = document.getElementById("sub-menu-list");

    var li = document.createElement("li");
    li.setAttribute("class", "name__list");
    var a = document.createElement("a");
    a.innerHTML = list.name;

    li.appendChild(a);
    subMenuList.appendChild(li);
}

function _displayListsEditModal(list) {
    var modal = document.getElementById("edit-list-modal__list-item");

    var divListItem = document.createElement("div");
    divListItem.setAttribute("class", "list-item");

    var idList = document.createElement("input");
    idList.setAttribute("type", "hidden");
    idList.setAttribute("id", `list_${list.id}`);
    idList.value = list.id;

    var nameList_ = document.createElement("input");
    nameList_.setAttribute("type", "text");
    nameList_.setAttribute("id", `list_${list.name}`);
    nameList_.setAttribute("onblur", `updateAfterFocus(list_${list.name}, ${list.id})`);
    nameList_.disabled = true;
    nameList_.value = list.name;

    var div = document.createElement("div");
    div.setAttribute("class", "edit-delete-buttons");

    var editButton = document.createElement("button");
    editButton.setAttribute("class", "edit-list-button");
    editButton.setAttribute("onclick", `setFocus(list_${list.name})`);
    editButton.innerText = "Edit";

    var deleteButton = document.createElement("button");
    deleteButton.setAttribute("class", "delete-list-button");
    deleteButton.setAttribute("onclick", `deleteList(${list.id})`);
    deleteButton.innerText = "Delete";

    div.appendChild(editButton);
    div.appendChild(deleteButton);

    divListItem.appendChild(idList);
    divListItem.appendChild(nameList_);
    divListItem.appendChild(div);

    modal.appendChild(divListItem);
    
}

function _displayListModal(list) {
    var modal = document.getElementById("list__modal");

    var div = document.createElement("div");
    div.setAttribute("class", "list-name");
    div.setAttribute("onclick", `changeListName("${list.name}")`);
    div.innerText = list.name;

    modal.appendChild(div);
}

function setFocus(element) {
    console.log(element);
    element.disabled = false;
    element.focus();
}

function updateAfterFocus(element, id) {
    element.disabled = true;

    var list = {
        id: id,
        name: element.value
    };

    fetch(listUri, {
        method: 'PUT',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(list)
    })
        .then(() => {
            location.reload();
        });
}

function deleteList(listId) {
    console.log(listId);
    fetch(`${listUri}/${listId}`, {
        method: 'DELETE',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json',
        },
    })
        .then(() => {
            location.reload();
        })
        .catch(error => console.error('Unable to delete item.', error));
}

function deleteSelectedTask() {
    console.log(selectedTask.id);

    fetch(`${taskUri}/${selectedTask.id}`, {
        method: 'DELETE',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json',
        },
    })
        .then(() => location.reload());
}

function showSelectedTask(id) {
    selectedTask = taskList.find(task => task.id === id);
    //console.log(id);
    //console.log(selectedTask);
    //var card = document.getElementById("task-details-selected-task");
    selectedTaskCard.style.visibility = "visible";

    var taskId = document.getElementById("task-id");
    taskId.value = selectedTask.id;

    var taskName = document.getElementById("task-name");
    taskName.value = selectedTask.name;

    var taskListButton = document.getElementById("task-list-button");
    taskListButton.innerText = nameList.find(l => l.id === selectedTask.list.id).name;


    var taskDate = document.getElementById("selected-task-date");
    if (selectedTask.endDate == null) {
        taskDate.innerText = "No";
    }
    else {
        taskDate.innerText = selectedTask.endDate.toDateString();
    }

    var taskNotes = document.getElementById("task-notes");
    if (selectedTask.notes != null || selectedTask.notes != "") {
        taskNotes.innerText = selectedTask.notes;
    }


    var taskCreatedDate = document.getElementById("task-created-date");
    taskCreatedDate.innerText = selectedTask.createdDate.toDateString();
    console.log(typeof selectedTask.createdDate);
}


/* MODAL WINDOW */

var addTaskModal = document.getElementById("add-task-modal");
var listModal = document.getElementById("list-modal");
var editListModal = document.getElementById("edit-list-modal");
var addListModal = document.getElementById("add-list-modal");
var editTagsModal = document.getElementById("edit-tags-modal");
var addTagModal = document.getElementById("add-tag-modal");
var selectedListName = document.getElementById("selected-list-name");

var taskListBtn = document.getElementById("task-list-button");
var addTagBtn = document.getElementById("add-tag-button");
var editTagsBtn = document.getElementById("edit-tags-button");
var addListBtn = document.getElementById("add-list-button");
var editListBtn = document.getElementById("edit-list-button");
var openModalBtn = document.getElementById("open-modal-button");
var closeModalBtn = document.getElementById("close-modal-button");

var selectedTaskCard = document.getElementById("task-details-selected-task");

// OPENS ADD TASK MODAL WINDOW
openModalBtn.onclick = function () {
    selectedTask = null;
    addTaskModal.style.visibility = "visible";
    selectedTaskCard.style.visibility = "hidden";
    addTaskModal.style.transition = "all 0.04s ease";
}

// ADDS NEW TASK
closeModalBtn.onclick = function () {
    addTaskModal.style.visibility = "hidden";

    const currentDate = new Date();
    var list = document.getElementById("selected-list-name").innerText;

    const newTask = {
        name: document.getElementById("new-task-name").value,
        endDate: document.getElementById("new-task-date").value,
        notes: document.getElementById("new-task-notes").value,
        listModelId: nameList.find(l => l.name === list).id,
    };

    fetch(taskUri, {
        method: 'POST',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(newTask)
    })
        .then(() => location.reload());

        console.log(newTask);
}

// UPDATES TASK
function updateTask() {
    var taskId = document.getElementById("task-id");
    var taskName = document.getElementById("task-name");
    var listName = document.getElementById("task-list-button");
    var taskNotes = document.getElementById("task-notes");

    var listId = nameList.find(l => l.name == listName.innerText);
    console.log(listId);

    var task = {
        id: taskId.value,
        name: taskName.value,
        listModelId: listId.id,
        notes: taskNotes.value
    };

    fetch(taskUri, {
        method: 'PUT',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(task)
    })
        .then(() => location.reload());

}

// UPDATES TASK STATUS
function updateTaskStatus(target, id, isCompleted) {
    console.log(target, id, isCompleted);

    if (target == 'button') {
        if (selectedTask != null) {
            var taskId = document.getElementById("task-id");
            if (selectedTask.isCompleted == false) {
                _updateTaskStatus(taskId.value, true);
                // fetch(`${taskUri}/UpdateTaskStatus/${taskId.value}/${true}`, {
                //     method: 'PUT',
                //     headers: {
                //         'Accept': 'application/json',
                //         'Content-Type': 'application/json',
                //     }
                // })
                //     .then(() => location.reload());
            }

            console.log(taskId.value, selectedTask.isCompleted);
        }
    }
    else {
        var _isCompleted;
        if (isCompleted == true) {
            _isCompleted = false;
        }
        else {
            _isCompleted = true;
        }

        _updateTaskStatus(id, _isCompleted);
    }
}

function _updateTaskStatus(id, isCompleted) {
    fetch(`${taskUri}/UpdateTaskStatus/${id}/${isCompleted}`, {
        method: 'PUT',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json',
        }
    })
        .then(() => location.reload());
}

editListBtn.onclick = function () {
    editListModal.style.visibility = "visible";
}

addListBtn.onclick = function () {
    addListModal.style.visibility = "visible";
}

editTagsBtn.onclick = function () {
    editTagsModal.style.visibility = "visible";
}

addTagBtn.onclick = function () {
    addTagModal.style.visibility = "visible";
}

taskListBtn.onclick = function () {
    listModal.style.visibility = "visible";
    console.log("daw");
}

window.onclick = function (e) {
    if (e.target == addTaskModal) {
        addTaskModal.style.visibility = "hidden";
    }
    else if (e.target == listModal) {
        listModal.style.visibility = "hidden";
    }
    else if (e.target == editListModal) {
        editListModal.style.visibility = "hidden";
    }
    else if (e.target == addListModal) {
        addListModal.style.visibility = "hidden";
    }
    else if (e.target == editTagsModal) {
        editTagsModal.style.visibility = "hidden";
    }
    else if (e.target == addTagModal) {
        addTagModal.style.visibility = "hidden";
    }
}

selectedListName.onclick = function () {
    listModal.style.visibility = "visible";
}

function changeListName(e) {
    if (selectedTask == null) {
        selectedListName.innerText = e;
    }
    else {
        document.getElementById("task-list-button").innerText = e;
        updateTask();
    }

    listModal.style.visibility = "hidden";
}


// LISTS

var mainListName = document.getElementById("main-name-list");

function changeMainListName(listName) {
    //console.log(mainListName);
    mainListName.innerText = listName;
}
