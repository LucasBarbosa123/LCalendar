//clears all the inputs in a modal
function clearModal(modalId) {
    let modal = document.getElementById(modalId)
    let modalInputs = modal.querySelectorAll('input')
    modalInputs.forEach(modalInput => {
        modalInput.value = getInputDefaultValue(modalInput.type)
    })
}

const getInputDefaultValue = (type) => inputTypesDefaultValues[type]
let inputTypesDefaultValues = {
    text : "",
    number: 0,
    date: "",
    time: ""
}

function hideModal(modalId) {
    let modalEl = document.getElementById(modalId)
    let modalInstance = bootstrap.Modal.getInstance(modalEl) || new bootstrap.Modal(modalEl)
    modalInstance.hide()
}
function showModal(modalId) {
    const myModal = new bootstrap.Modal(document.getElementById(modalId))
    myModal.show()
}
var checkWidowWidth = function () {
    if (window.innerWidth <= 1199) {
        return true;
    } else {
        return false;
    }
}