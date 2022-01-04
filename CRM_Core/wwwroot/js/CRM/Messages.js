function ShowMandatoryMessage(htmlInput) {
    swal({
        title: 'خطا!!!',
        html: htmlInput,
        type: 'error',
        confirmButtonColor: 'rgb(39, 179, 139)',
        confirmButtonText: ' متوجه شدم. '
    }).then(function () {
    },
    ).catch(swal.noop);
}

function WarningMessage(message) {
    swal(
        'هشدار !!!',
        message,
        'warning');
}

function ErrorMessage(message) {
    var errorMessage = message == '' ? 'بروز خطا : به هنگام انجام عملیات خطایی رخ داد لطفا با پشتیبانی تماس بگیرید ' : message;
    var titleError = 'خطا !!!';

    swal(
        titleError,
        errorMessage,
        'error');
}

function SuccessMessage(message) {
    swal(
        'انجام گردید.',
        message,
        'success');
}

function QuestionMessage() {
    swal({
        title: 'آیا جهت حذف آیتم اطمینان دارید؟',
        text: "این عملیات برگشت پذیر نیست...",
        type: 'question',
        showCancelButton: true,
        confirmButtonColor: '#f44336',
        cancelButtonColor: '#777',
        confirmButtonText: 'بله، خارج شو. '
    }).then(function () {
        alert('خارج شدم');
    },
    ).catch(swal.noop);
}

