const addMajor = document.getElementById("addMajor");
const majors = document.getElementById("majors");
const domainName = 'https://localhost:7145';
let newMajor;
addMajor.onclick = function () {
    newMajor = document.createElement("div");
    newMajor.className = "form-group d-inline-flex";
    newMajor.innerHTML = `<input type='text' class='form-control m-2' name='major' required>
                    \n<button class='btn btn-primary m-2'>Done</button>`;
    majors.insertBefore(newMajor, addMajor);
    newMajor.children[1].onclick = doneClicked;
};
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
        majors.insertBefore(createMajorElement(majorName), addMajor);
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

function editClicked(major) {
    let element = document.getElementById(`${major}`);
    let newNode = document.createElement("div");
    newNode.className = "form-group d-inline-flex";
    newNode.innerHTML = `<input type='text' class='form-control m-2' name='major' placeholder=${major} required>
                    \n<button class='btn btn-primary m-2'>Update</button>
                    \n<button class='btn btn-danger m-2' onclick=cancelUpdateClicked(this,${'"' + major + '"'})>Cancel</button>`;
    element.replaceWith(newNode);
}

function cancelUpdateClicked(node, id) {
    node.parentElement.replaceWith(createMajorElement(id));
}

function createMajorElement(name) {
    let div = document.createElement("div");
    div.className = "d-inline-flex bg-info rounded-2 m-2 justify-content-between";
    div.id = name;
    div.innerHTML = `<p class="btn btn-success text-light m-2">${name}</p>\n
                            <div class="btn btn-primary m-2" onclick=editClicked(${'"' + name + '"'})>Edit</div>\n
                            <div class="btn btn-danger m-2" onclick=deleteClicked(${'"' + name + '"'})>Delete</div>`;
    return div;
}