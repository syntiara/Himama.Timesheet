// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

function SearchName(e) {
    if (e.key === 'Enter' || e.keyCode === 13)
    {
        $.ajax({
            type: "GET",
            url: "/Users/SearchUser",
            data: { name: e.target.value },
            datatype: "data",
            beforeSend: function () {
                ShowLoader();
                HideNotFound();
                HideUsers();
            },
            success: function (response) {
                CreateOrDisplayUser(response);
            },
            failure: function () {
                alert("an error occured. Please try again later!");
            }
        });
    }
};

function ClockIn(userId, isClockIn) {
    {
        $.ajax({
            type: "GET",
            url: "/UsersAttendance/CreateTimeSheet",
            data: { userId: userId, isClockIn: isClockIn },
            datatype: "data",
            beforeSend: function () {
                ShowLoader();
                HideNotFound();
            },
            success: function () {
                HideClock(isClockIn);
            },
            failure: function () {
                alert("an error occured. Please try again later!");
            }
        });
    }
};

/* Utils for page or section displays
-------------------------------------------------- */

    function ShowLoader() { $('div .loader').removeClass('d-none'); }

    function HideLoader() { $('div .loader').addClass('d-none'); }

    function ShowNotFound() { $('div .create-user').removeClass('d-none'); }

    function HideNotFound() { $('div .create-user').addClass('d-none'); }

    function ShowUsers() { $('div .users').removeClass('d-none'); }

    function ShowError() { $(".shadow .error").removeClass("d-none"); }

    function HideError() { $(".shadow .error").addClass("d-none"); }


    function HideUsers() {
        DestroyTable();
        $('div .users').addClass('d-none');
    }

function CreateOrDisplayUser(list) {
    if (list.length == 0) {
        ShowNotFound();
    }
    else {
        const tbody = $('<tbody></tbody>');
        $(".users table").append(tbody);
        tbody.attr("id","remove-rows")
        $.each(list,function(i){
            $(".users table tbody").html( `<tr><td>${list[i].firstName}</td>
            <td> ${list[i].lastName}</td>
           <td> ${list[i].email}</td>
           <td><a href=Users/Details/${list[i].id}><span class="material-icons">visibility</span><a></td></tr>` );
         })

        ShowUsers();
    }
    HideLoader();
}

function DestroyTable() {
    var tableRows = document.getElementById('remove-rows');
    if(tableRows != null)
    {
        var parentEl = tableRows.parentElement;
        parentEl.removeChild(tableRows);    
    }
}

function HideClock(isClockIn)
    {
        if(isClockIn)
        {
            $('.clock .clock-in').removeClass('d-block');
            $('.clock .clock-in').addClass('d-none');
            $('.clock .clock-out').addClass('d-block');
            $('.clock .clock-out').removeClass('d-none');
        }
        else
        {
            $('.clock .clock-in').addClass('d-block');
            $('.clock .clock-in').removeClass('d-none');
            $('.clock .clock-out').removeClass('d-block');
            $('.clock .clock-out').addClass('d-none');
        }
    }

    const isDate = (date) => {
        return (new Date(date) !== "Invalid Date") && !isNaN(new Date(date));
      }

/* Edit event handler.
-------------------------------------------------- */

     $("body").on("click", "#tblAttendance .Edit", function Edit() {
        var row = $(this).closest("tr");
        $("td", row).each(function () {
            if ($(this).find("input").length > 0) {
                $(this).find("input").attr('readonly', false);;
            }
        });
        row.find(".Update").show();
        $(this).hide();
    });

    //Update event handler.
    $("body").on("click", "#tblAttendance .Update", function () {
        var row = $(this).closest("tr");
       
        const clockIn = row.find(".clock-in").find("input").val();
        const clockOut = row.find(".clock-out").find("input").val();

        if(isDate(clockIn) && isDate(clockOut))
        {
            HideError();

            var form_data = new FormData();    
            form_data.append('Id', row.find(".num").find(".id").html());
            form_data.append('UserId', row.find(".num").find(".userid").html());
            form_data.append('ClockIn', clockIn);
            form_data.append('ClockOut', clockOut);
    
            $("td", row).each(function () {
                if ($(this).find("input").length > 0) {
                    $(this).find("input").attr("readonly", true);;
                }
            });
            row.find(".Edit").show();
            $(this).hide();
    
            $.ajax({
                type: "POST",
                processData: false,
                contentType: false,
                url: "/UsersAttendance/Edit",
                data: form_data,
                beforeSend: function () {
                    HideError();
                },
            });
        }
        else
        {
            ShowError();
        }
    });
