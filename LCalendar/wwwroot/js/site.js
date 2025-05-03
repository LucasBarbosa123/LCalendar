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