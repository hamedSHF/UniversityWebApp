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
        const div = document.createElement("div");
        div.className = "d-inline-flex bg-info rounded-2 m-2 justify-content-between";
        div.id = majorName;
        div.innerHTML = `<p class="btn btn-success text-light m-2">${majorName}</p>\n
                            <div class="btn btn-primary m-2">Edit</div>\n
                            <div class="btn btn-danger m-2" onclick=${deleteClicked('majorName')}>Delete</div>`;
        majors.insertBefore(div, addMajor);
    }
}

async function deleteClicked(major) {
    console.log(major);
    let deletedElement = document.getElementById(major);
    let response = await fetch(`${domainName}/api/major/${major}`, {
        method: 'DELETE'
    });
    if (response.ok) {
        majors.removeChild(deletedElement);
    }
}

async function editClicked(element) {

}