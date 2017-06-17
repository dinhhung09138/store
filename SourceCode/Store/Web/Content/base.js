function notification(mesage, type) {

    if (type.toLowerCase() == "error") {
        Lobibox.notify('error', {
            sound: true,
            delay: false,
            size: 'mini',
            icon: false,
            position: 'bottom right',
            msg: mesage
        });
        return;
    }
    if (type.toLowerCase() == "warning") {
        Lobibox.notify('warning', {
            sound: true,
            delay: false,
            size: 'mini',
            icon: false,
            position: 'bottom right',
            msg: mesage
        });
        return;
    }
    if (type.toLowerCase() == "success") {
        Lobibox.notify('success', {
            sound: true,
            delay: 3000,
            size: 'mini',
            icon: false,
            position: 'bottom right',
            msg: mesage
        });
        return;
    }
    
}


/*Navigation*/

var ww = document.body.clientWidth;

$(document).ready(function () {

    $(".nav li a").each(function () {
        if ($(this).next().length > 0) {
            $(this).addClass("parent");
        };
    })

    $(".toggleMenu").click(function (e) {
        e.preventDefault();
        $(this).toggleClass("active");
        $(".nav").toggle();
    });
    adjustMenu();
    selectPicker();
    fullScreenModal();
})

$(window).bind('resize orientationchange', function () {
    ww = document.body.clientWidth;
    adjustMenu();
});

var adjustMenu = function () {
    if (ww < 768) {
        $(".toggleMenu").css("display", "inline-block");
        if (!$(".toggleMenu").hasClass("active")) {
            $(".nav").hide();
        } else {
            $(".nav").show();
        }
        $(".nav li").unbind('mouseenter mouseleave');
        $(".nav li a.parent").unbind('click').bind('click', function (e) {
            // must be attached to anchor element to prevent bubbling
            e.preventDefault();
            $(this).parent("li").toggleClass("hover");
        });
    }
    else if (ww >= 768) {
        $(".toggleMenu").css("display", "none");
        $(".nav").show();
        $(".nav li").removeClass("hover");
        $(".nav li a").unbind('click');
        $(".nav li").unbind('mouseenter mouseleave').bind('mouseenter mouseleave', function () {
            // must be attached to li so that mouseleave is not triggered when hover over submenu
            $(this).toggleClass('hover');
        });
    }
}

/*End Navigation*/

/*Set full screen model*/

function fullScreenModal() {
    $(".dropdown-menu .panel-body").slimscroll({
        height: "150px",
        alwaysVisible: false,
        size: "3px"
    }).css("width", "100%");
}

/*When show model above another model*/

$(document).on('show.bs.modal', '.modal', function (event) {
    var zIndex = 1040 + (10 * $('.modal:visible').length);
    $(this).css('z-index', zIndex);
    setTimeout(function () {
        $('.modal-backdrop').not('.modal-stack').css('z-index', zIndex - 1).addClass('modal-stack');
    }, 0);
});

/*Use for dropdow select and find item*/

/*Select picker*/

function selectPicker() {
    $('.selectpicker').selectpicker({
        liveSearch: true,
        showSubtext: true
    });
}

/*Loading model when page start and stop when finish load page*/

function startLoading() {
    $('#loadingModel').modal('show');
}

function stopLoading() {
    $('#loadingModel').modal('hide');
}

$('#loadingModel').modal('show');
window.onload = stopLoading;

/*End Loading model*/

function scrollBodyModal(focusID) {
    var h = window.innerHeight - 120;
    $(".modal-body .container-fluid").slimscroll({
        height: h + "px",
        alwaysVisible: false,
        size: "3px"
    }).css("width", "100%");
    $('#inputModel').modal('show');
    $(focusID).focus();
}

window.onresize = function (event) {
    scrollBodyModal();
};

function clearInputModal() {
    $('#inputModel').modal('hide');
    $('#divContainer').empty();
}