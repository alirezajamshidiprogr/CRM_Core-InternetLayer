function WarningMessage(message ) {
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
    debugger;
    var isConfirm;
    swal({
        title: 'آیا اطمینان دارید؟',
        text: "این عملیات برگشت پذیر نیست...",
        type: 'question',
        showCancelButton: true,
        confirmButtonColor: '#f44336',
        cancelButtonColor: '#777',
        confirmButtonText: 'بله، حذف شود. '
    }).then(function () {
        isConfirm = true;
        //swal(
        //    'انتخاب شما حذف کردن بود.',
        //    'فایل شما با موفقیت حذف گردید.',
        //    'success'
        //).catch(swal.noop);
    }, function (dismiss) {
            if (dismiss === 'cancel') {
                isConfirm = false;
            swal(
                'لغو گردید',
                'فایل شما همچنان وجود دارد.',
                'error'
            ).catch(swal.noop);;
        }
    }).catch(swal.noop);
    debugger;
    return isConfirm;
}