const addMajor = document.getElementById("addMajor");
const majors = document.getElementById("majors");
addMajor.onclick = function () {
    const div = document.createElement("div");
    div.className = "form-group d-inline-flex";
    div.innerHTML = `<input type='text' class='form-control m-2' name='major' required>
                    \n<button class='btn btn-primary m-2'>Done</button>`;
    majors.insertBefore(div, addMajor);
    div.children[1].onclick = doneClicked;
};
function doneClicked() {
    const major = document.getElementById("major");
    let response = await fetch('/Major/Add', {
        method: 'POST',
        body: major.value
    }).then(res => res.json());
    if (!response.ok) {
        alert(response['message']);
    }
    else {
        const div = document.createElement("div");
        div.className = "d-inline-flex bg-info rounded-2 m-2";
        div.innerHTML = `<p class="btn btn-success m-2 flex-fill">${major.value}</p>\n
                            <div class="btn btn-primary m-2">Edit</div>\n
                            <div class="btn btn-danger m-2">Delete</div>`;
        majors.insertBefore(div, addMajor);
    }
}