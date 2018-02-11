$(document).ready(function () {
    $(document).keyup(function (e) {
        if (e.keyCode === 27) {
            closeMe();
        }
    });
    $("input[data-val-length-max]").each(function (index, element) {
        var length = parseInt($(this).attr("data-val-length-max"));
        $(this).prop("maxlength", length);
    });
    $('.movie').mouseenter(function () {
        $(this).find('.details').slideDown();
    });
    $('.movie').mouseleave(function () {
        $(this).find('.details').slideUp();
    });
    //$('.movie').each(function (i, elem) {
    //    if (i % 2 == 0)
    //        $(elem).find('.details').css('background-color', '#455A64');
    //    else
    //        $(elem).find('.details').css('background-color', '#F44336');
    //});
});
function fileCheck(e) {
    var acceptedExts = ['jpg', 'jpeg', 'png', 'gif'];
    var ext = /[^.]+$/.exec($(e).val())[0];
    if (acceptedExts.indexOf(ext) === -1) {
        alert('Invalid file!');
        e.value="";
        return false;
    }
    else if (e.files[0].size > 2097152) {
        alert('File too large. Max upload file size is 2MB');
        e.value = "";
        return false;
    }
}
function closeMe() {
    $('.pop-up').remove();
    $('.pop-up-overlay').remove();
}
$(document).on('click', '.pop-up-overlay', function () {
    closeMe();
});

$(document).on('click', '.cls-btn', function () {
    closeMe();
});


function isValid() {
    var re = /^\d{2}[./-]\d{2}[./-][1-2]\d{3}$/;
    console.log($('#DOB').val().substr(0, 10).match(re), $('#DOB').val().substr(0, 10));
    if ($('#txtName').val().trim() == '') {
        alert('Name cannot be empty!');
        return false;
    } else if ($('#Sex').val() == '') {
        alert('Please select gender!');
        return false;
    } else if ($('#DOB').val().trim() == '') {
        alert('DOB cannot be empty!');
        return false;
    } else if (!$('#DOB').val().substr(0, 10).match(re)) {
        alert('DOB is Invalid!');
        return false;
    }
    else
        return true;
}

