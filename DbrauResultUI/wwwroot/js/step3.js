

function SaveButtonOnclick(id, DocID) {
    //alert(id)
    flag = true;
    //if ($.trim($("#" + id+"_Status").val()) == "") {

    //    $("#" + id + "_Status").addClass("input-validation-error");
    //    flag = false;
    //    //$("#FatherName").focus();
    //}
    //else {
    //    $("#" + id + "_Status").removeClass("input-validation-error");
    //}

    if (DocID == 5) {

        if ($.trim($("#" + id + "_Subject").val()) == "") {
            $("#" + id + "_Subject").addClass("input-validation-error");
            flag = false;
            //$("#MotherName").focus();
        }
        else {
            $("#" + id + "_Subject").removeClass("input-validation-error");
        }
    }

    if ($.trim($("#" + id + "_Board").val()) == "") {
        $("#" + id + "_Board").addClass("input-validation-error");
        flag = false;
        //$("#MotherName").focus();
    }
    else {
        $("#" + id + "_Board").removeClass("input-validation-error");
    }

    if (Number($.trim($("#" + id + "_Obt").val())) == "" || Number($.trim($("#" + id + "_Obt").val())) < 1) {
        $("#" + id + "_Obt").addClass("input-validation-error");
        flag = false;
        //$("#MotherName").focus();
    }
    else {
        $("#" + id + "_Obt").removeClass("input-validation-error");
    }
    if (Number($.trim($("#" + id + "_Total").val())) == "" || Number($.trim($("#" + id + "_Total").val())) < 1) {
        $("#" + id + "_Total").addClass("input-validation-error");
        flag = false;
        //$("#MotherName").focus();
    }
    else {
        $("#" + id + "_Total").removeClass("input-validation-error");
        if (Number($.trim($("#" + id + "_Total").val())) <= Number($.trim($("#" + id + "_Obt").val()))) {
            $("#" + id + "_Total").addClass("input-validation-error");
            flag = false;
        }
        else {
            $("#" + id + "_Total").removeClass("input-validation-error");
        }
    }

    if ($("#" + id + "_CGPA").val() == "False") {
        
    } else {
        if (Number($.trim($("#" + id + "_Percentage").val())) == "" || Number($.trim($("#" + id + "_Percentage").val())) < 1) {
            $("#" + id + "_Percentage").addClass("input-validation-error");
            flag = false;

        }
        else {
            $("#" + id + "_Percentage").removeClass("input-validation-error"); if (Number($("#" + id + "_Percentage").val()) > 100) {
                $("#" + id + "_Percentage").addClass("input-validation-error");
                flag = false;
            }
            else {
                $("#" + id + "_Percentage").removeClass("input-validation-error");
            }
        }
    }



    if ($.trim($("#" + id + "_PreRollNo").val()) == "") {
        $("#" + id + "_PreRollNo").addClass("input-validation-error");
        flag = false;
        //$("#MotherName").focus();
    }
    else {
        $("#" + id + "_PreRollNo").removeClass("input-validation-error");
    }
    if ($.trim($("#" + id + "_PassingYear").val()) == "") {
        $("#" + id + "_PassingYear").addClass("input-validation-error");
        flag = false;
        //$("#MotherName").focus();
    }
    else {
        $("#" + id + "_PassingYear").removeClass("input-validation-error");
    }
    if (flag) {

        var obj = {
            //EncrptedRoll: $("#EncrptedRoll").val(),
            EntryID: id,
            Subject: $("#" + id + "_Subject").val(),
            Board_Universty_Name: $("#" + id + "_Board").val(),
            //PassingYear: $("#" + id + "_Total").val(),
            // PreRollNo: $("#" + id + "_Status").val(),
            MarkObt: Number($("#" + id + "_Obt").val()),
            TotalMarks: Number($("#" + id + "_Total").val()),
            Percentage: Number($("#" + id + "_Percentage").val()),
            PreRollNo: $("#" + id + "_PreRollNo").val(),
            PassingYear: $("#" + id + "_PassingYear").val(),
            IsCGPA: Boolean($("#" + id + "_CGPA").val() == "True"),
        };
        // alert(JSON.stringify(obj))
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: '/Home/Step3Quali',
            data: JSON.stringify(obj),
            dataType: "json",
            beforeSend: function () {
                // setting a timeout
                $("#" + id + "btnlwait").show();
                $("#" + id + "btnl").hide();
            },
            success: function (response) {
                // alert(response)

                if (response == true) {
                    Swal.fire({
                        position: 'center',
                        icon: 'success',
                        title: 'Saved Details!',
                        showConfirmButton: false,
                        timer: 3000
                    })

                    // window.location.reload();

                }
            },
            error: function (error) {

            },
            complete: function () {
                setTimeout(function () {
                    $("#" + id + "btnlwait").hide();
                    $("#" + id + "btnl").show();
                    window.location.reload();
                }, 500);
            },
        });
    }
}