const addMajor = document.getElementById("addMajor");
const addTopic = document.getElementById("addTopic");
const majors = document.getElementById("majors");
const search = document.getElementById("searchBox");
const topics = document.getElementById("topics");
const domainName = 'https://localhost:7145';
let newMajor;
let newTopic;
let clickedMajor;
let lastClickedMajor;

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
    topics.insertBefore(newTopic, topics.lastChild);
    newTopic.children[1].onclick = topicDoneClicked;
}
async function topicDoneClicked() {
    let topicName = newTopic.children[0].value;
    let response = await fetch(`${domainName}/api/topic/add`, {
        method: "POST",
        body: JSON.stringify({
            MajorName: `${clickedMajor}`,
            TopicName: `${topicName}`
        }),
        headers: { "Content-Type": "application/json" }
    });
    if (response.status == 201) {
        topics.removeChild(newTopic);
        topics.insertBefore(createTopicElement(topicName), topics.lastChild);
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

async function deleteTopicClicked(topic) {
    let deletedTopic = document.getElementById(topic);
    let response = await fetch(`${domainName}/api/topic/delete`, {
        method: 'DELETE',
        body: JSON.stringify({
            MajorName: `${clickedMajor}`,
            TopicName: `${topic}`
        }),
        headers: { "Content-Type": "application/json" }
    });
    if (response.ok) {
        topics.removeChild(deletedTopic);
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
                            <div class="btn btn-danger m-2" onclick="deleteTopicClicked('${name}')">Delete</div>`;
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
