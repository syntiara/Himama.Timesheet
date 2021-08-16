// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

SearchName = (e) => {
  if (e.key === "Enter" || e.keyCode === 13) {
    $.ajax({
      type: "GET",
      url: "/Users/SearchUser",
      data: { name: e.target.value },
      datatype: "data",
      beforeSend: () => {
        ShowLoader();
        HideNotFound();
        HideUsers();
      },
      success: (response) => CreateOrDisplayUser(response),
      failure: () => alert("an error occured. Please try again later!"),
    });
  }
};

ClockIn = (userId, isClockIn) => {
  {
    $.ajax({
      type: "GET",
      url: "/UsersAttendance/CreateTimeSheet",
      data: { userId: userId, isClockIn: isClockIn },
      datatype: "data",
      beforeSend: () => {
        ShowLoader();
        HideNotFound();
      },
      success: () => HideClock(isClockIn),
      failure: () => alert("an error occured. Please try again later!"),
    });
  }
};

/* Utils for page or section displays
-------------------------------------------------- */

ShowLoader = () => $("div .loader").removeClass("d-none");

HideLoader = () => $("div .loader").addClass("d-none");

ShowNotFound = () => $("div .create-user").removeClass("d-none");

HideNotFound = () => $("div .create-user").addClass("d-none");

ShowUsers = () => $("div .users").removeClass("d-none");

ShowError = () => $(".shadow .error").removeClass("d-none");

HideError = () => $(".shadow .error").addClass("d-none");

HideUsers = () => {
  DestroyTable();
  $("div .users").addClass("d-none");
};

CreateOrDisplayUser = (list) => {
  if (list.length === 0) ShowNotFound();
  else {
    const tbody = $("<tbody></tbody>");
    $(".users table").append(tbody);
    tbody.attr("id", "remove-rows");
    $.each(list, (i) => {
      $(".users table tbody").html(`<tr><td>${list[i].firstName}</td>
            <td> ${list[i].lastName}</td>
           <td> ${list[i].email}</td>
           <td><a href=Users/Details/${list[i].id}><span class="material-icons">visibility</span><a></td></tr>`);
    });

    ShowUsers();
  }
  HideLoader();
};

DestroyTable = () => {
  const tableRows = document.getElementById("remove-rows");
  if (tableRows != null) {
    const parentEl = tableRows.parentElement;
    parentEl.removeChild(tableRows);
  }
};

HideClock = (isClockIn) => {
  if (isClockIn) {
    $(".clock .clock-in").removeClass("d-block");
    $(".clock .clock-in").addClass("d-none");
    $(".clock .clock-out").addClass("d-block");
    $(".clock .clock-out").removeClass("d-none");
  } else {
    $(".clock .clock-in").addClass("d-block");
    $(".clock .clock-in").removeClass("d-none");
    $(".clock .clock-out").removeClass("d-block");
    $(".clock .clock-out").addClass("d-none");
  }
};

const isDate = (date) =>
  new Date(date) !== "Invalid Date" && !isNaN(new Date(date));

/* Edit event handler.
-------------------------------------------------- */

Edit = (element) => {
  const row = $(element).closest("tr");
  $("td", row).each(() => {
    if ($(element).find("input").length > 0)
      $(element).find("input").attr("readonly", false);
  });
  row.find(".Update").show();
  $(element).hide();
};

//Update event handler.
Update = (element) => {
  const row = $(element).closest("tr");
  const clockIn = row.find(".clock-in").find("input").val();
  const clockOut = row.find(".clock-out").find("input").val();

  if (isDate(clockIn) && isDate(clockOut)) {
    HideError();

    const form_data = new FormData();
    form_data.append("Id", row.find(".num").find(".id").html());
    form_data.append("UserId", row.find(".num").find(".userid").html());
    form_data.append("ClockIn", clockIn);
    form_data.append("ClockOut", clockOut);

    $("td", row).each(() => {
      if ($(element).find("input").length > 0) {
        $(element).find("input").attr("readonly", true);
      }
    });
    row.find(".Edit").show();
    $(element).hide();

    $.ajax({
      type: "POST",
      processData: false,
      contentType: false,
      url: "/UsersAttendance/Edit",
      data: form_data,
      beforeSend: () => HideError(),
    });
  } else {
    ShowError();
  }
};
