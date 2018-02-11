﻿$(document).ready(function () {
    $('#NewMovie').click(function (e) {
        e.preventDefault();
        if (isFormValid()) {
            var file = $('input[type="file"]')[0].files[0];
            var data = {
                Name: $('#txtMName').val(),
                Plot: $('#Plot').val(),
                ProducerID: $('#ProducerID').val(),
                ActorList: $('#ActorList').val(),
                Poster: file.name
            };
            console.log(data);
            var formData = new FormData();
            formData.append("file", file);

            $.ajax({
                method: 'POST',
                url: '../home/UploadFile',
                cache: false,
                data: formData,
                contentType: false,
                processData: false,
                success: function (resp) {
                    //console.log(data);
                    $.ajax({
                        method: 'POST',
                        url: '../home/NewMovie',
                        data: data,
                        success: function (data) {
                            location.href = '/';
                        },
                        error: function (err) {
                            console.log(err);
                        }
                    });
                },
                error: function (err) {
                    console.log(err);
                }
            });
        }
    });
    $(document).on('click', '#btnProducer', function (e) {
        e.preventDefault();
        if (isValid()) {
            var data = {
                name: $('#txtName').val(),
                sex: $('#Sex').val(),
                dob: $('#DOB').val()
            }
            $.ajax({
                method: 'POST',
                data: data,
                url: '../home/AddProducer',
                success: function (data) {
                    getProducers();
                    closeMe();
                },
                error: function (err) {
                    alert('err' + err.responseJSON);
                    console.log(err);

                }
            })
        }
    });
    $(document).on('click', '#btnActor', function (e) {
        e.preventDefault();
        if (isValid()) {
            var data = {
                name: $('#txtName').val(),
                sex: $('#Sex').val(),
                dob: $('#DOB').val()
            }
            $.ajax({
                method: 'POST',
                data: data,
                url: '../home/AddActor',

                success: function (data) {
                    console.log(data);
                    getActors();
                    closeMe();
                },
                error: function (err) {
                    alert('err' + err.responseJSON);
                    console.log(err);

                }
            });
        }
    });
});
function getProducers() {
    $('#ProducerID').html("");
    $.ajax({
        method: 'GET',
        url: '../home/GetProducerList',
        dataType: "JSON",
        success: function (data) {
            var str = '<option value="">Select Producer</option>';
            $.each(data, function (i, itm) {
                str += '<option value="' + itm.ID + '">' + itm.Name + '</option>';
            });
            $('#ProducerID').html(str);
        },
        error: function (err) {
            console.log(err);
        }
    });
}
function getActors() {
    $('#ActorList').html("");
    $.ajax({
        method: 'GET',
        url: '../home/GetActorList',
        dataType: "JSON",
        success: function (data) {
            var str = '<option value="">Select Actor</option>';
            $.each(data, function (i, itm) {
                str += '<option value="' + itm.ID + '">' + itm.Name + '</option>';
            });
            console.log(str);
            $('#ActorList').html(str);
        },
        error: function (err) {
            console.log(err);

        }
    });
}
function addProd() {

    $.get('GetProducer', function (html) {
        $('#AddProducer').html(html);
    });
}
function addAct() {
    $.get('GetActor', function (html) {
        $('#AddActor').html(html);
    });
}
function isFormValid() {
    if ($('#txtMName').val().trim() == '') {
        alert('Movie Name cannot be empty!');
        return false;
    } else if ($('#Plot').val() == '') {
        alert('Plot cannot be empty!');
        return false;
    } else if ($('#ProducerID').val() == '') {
        alert('Please select Producer!');
        return false;
    } else if ($('#ActorList').val() === null) {
        alert('Please select Actors!');
        return false;
    } else if ($('#Poster').val().trim() == '') {
        alert('Please upload poster!');
        return false;
    }
    else
        return true;
}