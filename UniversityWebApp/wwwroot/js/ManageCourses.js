const addMajor = document.getElementById("addMajor");
const addTopic = document.getElementById("addTopic");
const majors = document.getElementById("majors");
const search = document.getElementById("searchBox");
const topics = document.getElementById("topics");
const modalBody = document.getElementById("modalBody");
const addSelectedTopics = document.getElementById("addSelectedTopics");
const domainName = 'https://localhost:7145';
let newMajor;
let newTopic;
let clickedMajor;
let lastClickedMajor;
let checkTopicSelectionCounter = 0;

search.onkeyup = function () {
    if (search.value) {
        for (let i = 0; i < majors.children.length; i++) {
            let major = majors.children[i];
            if (major.id.toLowerCase().startsWith(search.value.toLowerCase())) {
                major.style = 'diplay: block !important';
            }
            else {
                major.style = 'display: none !important';
            }
        }
    }
    else {
        for (let i = 0; i < majors.children.length; i++) {
            majors.children[i].style = '';
        }
    }
}
addMajor.onclick = function () {
    newMajor = createInputElement();
    majors.insertBefore(newMajor, majors.lastChild);
    newMajor.children[1].onclick = doneClicked;
};
addTopic.onclick = function () {
    newTopic = createInputElement();
    modalBody.appendChild(newTopic, modalBody.lastChild);
    newTopic.children[1].onclick = topicDoneClicked;
};
addSelectedTopics.onclick = function () {

}
$('#topicModal').on('shown.bs.modal', async function (e) {
    let response = await fetch(`${domainName}/api/topic`, {
        method: "GET",
        headers: { "Content-Type": "application/json" }
    });
    if (response.ok) {
        let list = await response.json();
        if (list) {
            for (let i = 0; i < list.length; i++) {
                modalBody.append(createTopicSelectionElement(list[i]["title"], list[i]["id"]));
            }
        }
    }
});
$('#topicModal').on('hidden.bs.modal', function (e) {
    modalBody.innerHTML = '';
})
async function topicDoneClicked() {
    let topicName = newTopic.children[0].value;
    let response = await fetch(`${domainName}/api/topic/addTopic`, {
        method: "POST",
        body: JSON.stringify(topicName),
        headers: { "Content-Type": "application/json" }
    });
    if (response.status == 201) {
        modalBody.removeChild(newTopic);
        modalBody.appendChild(createTopicSelectionElement(topicName, await response.json()), modalBody.lastChild);
    }
}
async function doneClicked() {
    let majorName = newMajor.children[0].value;
    let response = await fetch(`${domainName}/api/major/add`, {
        method: 'POST',
        body: JSON.stringify({ Title: `${majorName}` }),
        headers: { "Content-Type": "application/json" }
    });
    if (response.status != 201) {
        alert(response['message']);
    }
    else {
        majors.removeChild(newMajor);
        majors.insertBefore(createMajorElement(majorName), majors.lastChild);
    }
}

async function deleteClicked(major) {
    let deletedElement = document.getElementById(major);
    let response = await fetch(`${domainName}/api/major/${major}`, {
        method: 'DELETE'
    });
    if (response.ok) {
        majors.removeChild(deletedElement);
    }
}

async function deleteTopicMajorClicked(topic) {
    let deletedTopic = document.getElementById(topic);
    let response = await fetch(`${domainName}/api/topic/delete`, {
        method: 'DELETE',
        body: JSON.stringify({
            MajorName: `${clickedMajor}`,
            TopicName: `${topic}`
        })
    });
    if (response.ok) {
        topics.removeChild(deletedTopic);
    }
}
async function deleteTopicClicked(topic) {
    let parent = topic.parentElement;
    let response = await fetch(`${domainName}/api/topic/deleteTopic/${parent.children[0].dataset.id}`, {
        method: "DELETE",
    });
    if (response.ok) {
        modalBody.removeChild(parent);
    }
}
function editClicked(major) {
    let element = document.getElementById(`${major}`);
    let newNode = document.createElement("div");
    newNode.className = "form-group d-inline-flex";
    newNode.innerHTML = `<input type='text' class='form-control m-2' name='major' placeholder=${major} required>
                    \n<button class='btn btn-primary m-2' onclick=updateClicked(this,'${major}')>Update</button>
                    \n<button class='btn btn-danger m-2' onclick=cancelUpdateClicked(this,'${major}')>Cancel</button>`;
    element.replaceWith(newNode);
}

function editTopicClicked(topic) {
    let parent = topic.parentElement;
    let oldTopic = parent.children[1].innerText;
    let id = parent.children[0].dataset.id;
    let editedTopic = document.createElement("div");
    editedTopic.innerHTML = `<input type='text' class='form-control m-2' name='major' placeholder=${oldTopic} required>
                    \n<button class='btn btn-primary m-2' onclick=updateTopicClicked(this,'${id}')>Update</button>
                    \n<button class='btn btn-danger m-2' onclick=cancelUpdateClicked(this)>Cancel</button>`;
    parent.replaceWith(editedTopic);
}
function cancelUpdateClicked(node, id) {
    node.parentElement.replaceWith(createMajorElement(id));
}

function updateClicked(node, oldTitle) {
    let newTitle = node.parentElement.children[0].value;
    if (newTitle) {
        $.ajax({
            url: `${domainName}/api/major`,
            contentType: "application/json",
            type: "PUT",
            data: JSON.stringify({ OldTitle: oldTitle, NewTitle: newTitle }),
            success: function () {
                let newElement = createMajorElement(newTitle);
                node.parentElement.replaceWith(newElement);
            }
        })
    }
}

function updateTopicClicked(node, id) {
    let parent = node.parentElement;
    let newValue = parent.children[0].value;
    $.ajax({
        url: `${domainName}/api/topic/updateTopic`,
        contentType: "application/json",
        type: "PUT",
        data: JSON.stringify({ Id: id, Name: newValue }),
        success: function () {
            parent.replaceWith(createTopicSelectionElement(newValue, id));
        }
    })
}
async function majorClicked(element, major) {
    if (lastClickedMajor) {
        lastClickedMajor.classList.remove("btn-warning");
        lastClickedMajor.classList.add("btn-success");
    }
    clickedMajor = major;
    let response = await fetch(`${domainName}/api/topic/${major}`, {
        method: "GET",
        headers: { "Content-Type": "application/json" }
    });
    if (response.ok) {
        element.classList.remove("btn-success");
        element.classList.add("btn-warning");
        clearTopicSection();
        let res = await response.json();
        if (res.lenght != 0) {
            for (let i = 0; i < res.length; i++) {
                topics.appendChild(createTopicElement(res[i]));
            }
        }
        else {
            alert("No topics yet");
        }
    }
    lastClickedMajor = element;
}
function topicSelected(topic) {
    if (topic.checked) {
        addSelectedTopics.disabled = false;
        checkTopicSelectionCounter++;
    }
    else {
        checkTopicSelectionCounter--;
    }
    addSelectedTopics.disabled = checkTopicSelectionCounter ? false : true;
}
function createMajorElement(name) {
    let div = document.createElement("div");
    div.className = "d-inline-flex bg-info rounded-2 m-2 justify-content-between";
    div.id = name;
    div.innerHTML = `<p class="btn btn-success text-light m-2" onclick=majorClicked(this, '${name}')>${name}</p>\n
                            <div class="btn btn-primary m-2" onclick="editClicked('${name}')">Edit</div>\n
                            <div class="btn btn-danger m-2" onclick="deleteClicked('${name}')">Delete</div>`;
    return div;
}

function createTopicElement(name) {
    let div = document.createElement("div");
    div.className = "d-inline-flex bg-info rounded-2 m-2 justify-content-between";
    div.id = name;
    div.innerHTML = `<p class="btn btn-warning text-light m-2">${name}</p>\n
                            <div class="btn btn-primary m-2" onclick="editClicked('${name}')">Edit</div>\n
                            <div class="btn btn-danger m-2" onclick="deleteTopicMajorClicked('${name}')">Delete</div>`;
    return div;
}

function createTopicSelectionElement(name, Id) {
    let div = document.createElement("div");
    div.className = "bg-info rounded-1 d-inline-flex justify-content-between align-items-center m-2";
    div.innerHTML = `<input class="form-check-input m-2" type="checkbox" onchange="topicSelected(this)" data-id="${Id}">
                     <label class="form-check-label fs-3 m-2">${name}</label>
                     <div class="btn btn-primary m-2" onclick="editTopicClicked(this)">Edit</div>
                     <div class="btn btn-danger m-2" onclick="deleteTopicClicked(this)">Delete</div>`;
    return div;
}

function createInputElement() {
    let div = document.createElement("div");
    div.className = "form-group d-inline-flex";
    div.innerHTML = `<input type='text' class='form-control m-2' required>
                    \n<button class='btn btn-primary m-2'>Done</button>`;
    return div;
}

let clearTopicSection = () => {
    topics.innerHTML = '';
};
