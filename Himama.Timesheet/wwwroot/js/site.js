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

/* Utils for page or section displays
-------------------------------------------------- */

function ShowLoader() {
    $('div .loader').removeClass('d-none');
}

function HideLoader() {
    $('div .loader').addClass('d-none');
}

function ShowNotFound() {
    $('div .create-user').removeClass('d-none');
}

function HideNotFound() {
    $('div .create-user').addClass('d-none');
}

function ShowUsers() {
    $('div .users').removeClass('d-none');
}

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
