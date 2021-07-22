// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


async function expand(elem) {

    var li = $(elem).parents('li')[0];
    var ul = $(li).children('ul')[0];
    $(ul).css('display') == 'none' ? $(ul).show() : $(ul).hide();

    if (elem.classList.contains("expand")) {
        elem.classList.remove("expand");
    }
    else {
        elem.classList.add("expand");
    }
}

async function setConfirmDeleteView(elem) {

    var id = elem.getAttribute("value");

    if (id == null) {
        updateContentArea("Problems with request");
    }

    $.ajax({
        type: "GET",
        url: "/TaskOrganizer/ConfirmDelete",
        dataType: "html",
        data: {
            id
        },
        success: function (data) {
            updateContentArea(data)
        }
    })
}

async function setNewTaskView(elem) {

    var id = elem.getAttribute("value");

    if (id != null) {
        $.ajax({
            type: "GET",
            url: "/TaskOrganizer/Add",
            dataType: "html",
            data: {
                id
            },

            success: function (data) {
                updateContentArea(data)
            }
        })
    }
    else {
        $.ajax({
            type: "GET",
            url: "/TaskOrganizer/Add",
            dataType: "html",

            success: function (data) {
                updateContentArea(data)
            }
        })
    }
}

async function setTaskInfoView(elem) {

    var id = elem.getAttribute("value");

    if (id == null) {
        updateContentArea("Problems with request");
    }
    else {
        $.ajax({
            type: "GET",
            url: "/TaskOrganizer/TaskInfo",
            dataType: "html",
            data: {
                id
            },

            success: function (data) {
                updateContentArea(data)
            }
        })
    }
}

async function setTaskEditView(elem) {

    var id = elem.getAttribute("value");

    if (id == null) {
        updateContentArea("Problems with request");
    }

    $.ajax({
        type: "GET",
        url: "/TaskOrganizer/EditTask",
        dataType: "html",
        data: {
            id
        },
        success: function (data) {
            updateContentArea(data)
        }
    })
}

function updateContentArea(data) {

    $('.contentArea').empty()
    $('.contentArea').append(data)
}