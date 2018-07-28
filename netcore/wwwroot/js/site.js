function ShowMessage(msg) {
    toastr.success(msg);
}

function ShowMessageError(msg) {
    toastr.error(msg);
}

//Initialize Select2 Elements
$('.select2').select2({
    dropdownAutoWidth: 'true',
    width: '100%'
})

//Date picker
$('.datepicker').datepicker({
    autoclose: true
})