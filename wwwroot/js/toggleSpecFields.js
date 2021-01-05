const specFields = document.querySelector('#specFields');
const specialization = document.querySelector('#specialization');
const employeePositionSelect = document.querySelector('#employeePosition');
const pwzNumber = document.querySelector('#pwzNumber');

if (employeePositionSelect.value === 'Doctor') {
    specFields.classList.remove('hidden');
}

function toggleSpecFields(e){
    if (e.target.value === 'Doctor') {
        specFields.classList.remove('hidden');
    } else {
        specFields.classList.add('hidden');
        specialization.value = '';
        pwzNumber.value = '';
    }
};

employeePositionSelect.addEventListener('change', toggleSpecFields);