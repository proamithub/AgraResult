

function SubmitButtonOnclick() {

    flag = true;
    if ($.trim($("#FatherName").val()) == "") {

        $("#FatherName").addClass("input-validation-error");
        flag = false;
        //$("#FatherName").focus();
    }
    else {
        $("#FatherName").removeClass("input-validation-error");
    }

    if ($.trim($("#MotherName").val()) == "") {
        $("#MotherName").addClass("input-validation-error");
        flag = false;
        //$("#MotherName").focus();
    }
    else {
        $("#MotherName").removeClass("input-validation-error");
    }
    if ($.trim($("#Aadhar").val()) == "") {
        $("#Aadhar").addClass("input-validation-error");
        flag = false; //$("#Aadhar").focus();
    }
    else {
        var number = $.trim($("#Aadhar").val());
        if (number.match(/^-?\d+\.?\d*$/) && (number.match(/\d{12}/))) {
            $("#Aadhar").removeClass("input-validation-error");
        }
        else {
            flag = false;// $("#Aadhar").focus();
            if (!$("#Aadhar").hasClass("input-validation-error"))
                $("#Aadhar").addClass("input-validation-error");
        }
    }
    if ($.trim($("#Gender").val()) == "") {
        $("#Gender").addClass("input-validation-error");
        flag = false; //$("#Gender").focus();
    }
    else {
        $("#Gender").removeClass("input-validation-error");
    }

    if ($.trim($("#DOB").val()) == "") {
        $("#DOB").addClass("input-validation-error");
        flag = false;
        //$("#DOB").focus();
    }
    else {
        $("#DOB").removeClass("input-validation-error");
    } if ($.trim($("#Email").val()) == "") {
        $("#Email").addClass("input-validation-error");
        flag = false;
        //$("#DOB").focus();
    }
    else {
        $("#Email").removeClass("input-validation-error");
    }

    if ($.trim($("#Email").val()) != "") {
        var email = $.trim($("#Email").val());
        if (!validEmail(email)) {
            $("#Email").addClass("input-validation-error");
            flag = false; //$("#Email").focus();
        }
        else {
            $("#Email").removeClass("input-validation-error");
        }
    }

    if ($.trim($("#Mobile").val()) == "") {
        $("#Mobile").addClass("input-validation-error");
        flag = false; //$("#Mobile").focus();
    }
    else {
        var number = $.trim($("#Mobile").val());
        if (number.match(/^-?\d+\.?\d*$/) && (number.match(/\d{10}/))) {
            $("#Mobile").removeClass("input-validation-error");
        }
        else {
            flag = false; //$("#Mobile").focus();
            if (!$("#Mobile").hasClass("input-validation-error"))
                $("#Mobile").addClass("input-validation-error");
        }
    }
    if ($.trim($("#AltMobile").val()) == "") {
        $("#AltMobile").addClass("input-validation-error");
        flag = false; //$("#Mobile").focus();
    }
    else {
        var number = $.trim($("#AltMobile").val());
        if (number.match(/^-?\d+\.?\d*$/) && (number.match(/\d{10}/))) {
            $("#AltMobile").removeClass("input-validation-error");
        }
        else {
            flag = false; //$("#Mobile").focus();
            if (!$("#AltMobile").hasClass("input-validation-error"))
                $("#AltMobile").addClass("input-validation-error");
        }
    }

    if ($.trim($("#Category").val()) == "") {
        $("#Category").addClass("input-validation-error");
        flag = false; //$("#Category").focus();
    }
    else {
        $("#Category").removeClass("input-validation-error");
    }

    if ($.trim($("#ParmanentAddress").val()) == "") {
        $("#ParmanentAddress").addClass("input-validation-error");
        flag = false; //$("#ParmanentAddress").focus();
    }
    else {
        $("#ParmanentAddress").removeClass("input-validation-error");
    }
    if ($.trim($("#CurrentAddress").val()) == "") {
        $("#CurrentAddress").addClass("input-validation-error");
        flag = false; //$("#CurrentAddress").focus();
    }
    else {
        $("#CurrentAddress").removeClass("input-validation-error");
    }


    if ($.trim($("#CState").val()) == "") {
        $("#CState").addClass("input-validation-error");
        flag = false; //$("#CState").focus();
    }
    else {
        $("#CState").removeClass("input-validation-error");
    }
    if ($.trim($("#PState").val()) == "") {
        $("#PState").addClass("input-validation-error");
        flag = false; //$("#CState").focus();
    }
    else {
        $("#PState").removeClass("input-validation-error");
    }


    if ($.trim($("#CDistrict").val()) == "") {
        $("#CDistrict").addClass("input-validation-error");
        flag = false;
        //$("#CAddress").focus();
    }
    else {
        $("#CDistrict").removeClass("input-validation-error");
    }
    if ($.trim($("#PDistrict").val()) == "") {
        $("#PDistrict").addClass("input-validation-error");
        flag = false;
        //$("#PAddress").focus();
    }
    else {
        $("#PDistrict").removeClass("input-validation-error");
    }
    if ($.trim($("#CPin").val()) == "") {
        $("#CPin").addClass("input-validation-error");
        flag = false; //$("#CPinCode").focus();
    }
    else {
        var number = $.trim($("#CPin").val());
        if (number.match(/^-?\d+\.?\d*$/) && (number.match(/\d{6}/))) {
            $("#CPin").removeClass("input-validation-error");
        }
        else {
            flag = false; //$("#CPin").focus();
            if (!$("#CPin").hasClass("input-validation-error"))
                $("#CPin").addClass("input-validation-error");
        }
    }
    if ($.trim($("#PPin").val()) == "") {
        $("#PPin").addClass("input-validation-error");
        flag = false; //$("#PPinCode").focus();
    }
    else {
        var number = $.trim($("#PPin").val());
        if (number.match(/^-?\d+\.?\d*$/) && (number.match(/\d{6}/))) {
            $("#PPin").removeClass("input-validation-error");
        }
        else {
            flag = false;// $("#PPin").focus();
            if (!$("#PPin").hasClass("input-validation-error"))
                $("#PPin").addClass("input-validation-error");
        }
    }

    //alert($("#PState option:selected").text())

    if (flag) {
        var obj = {
            EncrptedRoll: $("#EncrptedRoll").val(),
            FatherName: $("#FatherName").val(),
            MotherName: $("#MotherName").val(),
            Email: $("#Email").val(),
            Mobile: $("#Mobile").val(),
            Aadhar: $("#Aadhar").val(),
            Gender: $("#Gender").val(),
            DOBStr: $("#DOB").val(),
            AltMobile: $("#AltMobile").val(),
            Category: $("#Category").val(),
            CurrentAddress: $("#CurrentAddress").val(),
            CState:  $("#CState option:selected").text() ,
            CDistrict: $("#CDistrict").val(),
            CPin: $("#CPin").val(),
            ParmanentAddress: $("#ParmanentAddress").val(),
            PState:  $("#PState option:selected").text() ,
            PDistrict: $("#PDistrict").val(),
            PPin: $("#PPin").val(),
            Ews: $("#Ews option:selected").val() == "true" ?true:false,
        };
     //alert(JSON.stringify(obj))
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: '/Home/Step2Profile',
            data: JSON.stringify(obj),
            dataType: "json",
            beforeSend: function () {
                // setting a timeout
                $('#btnlwait').show();
                $('#btnl').hide();
            },
            success: function (response) {
               

                if (response == 1) {

                    $('#btnlwait').hide();
                    $('#btnl').show();
                    $('#Email').focus();
                    Swal.fire({
                        position: 'center',
                        icon: 'error',
                        title: 'This Email Already Exists!',
                        showConfirmButton: false,
                        timer: 3000
                    })

                }
                if (response == 2) {

                    $('#btnlwait').hide();
                    $('#btnl').show()
                    $('#Mobile').focus();
                    Swal.fire({
                        position: 'center',
                        icon: 'error',
                        title: 'This Mobile Already Exists!',
                        showConfirmButton: false,
                        timer: 3000
                    })
                }
                if (response == 3) {
                    window.location.href = "/admission-form-step3/" + obj.EncrptedRoll; 

                }
                if (response == 4) {
                    $('#btnlwait').hide();
                    $('#btnl').show()
                    $('#Mobile').focus();
                    Swal.fire({
                        position: 'center',
                        icon: 'error',
                        title: 'Try Again Later',
                        showConfirmButton: false,
                        timer: 3000
                    })
                }
                
            },
            error: function (error) {

            }
        });
    }
}