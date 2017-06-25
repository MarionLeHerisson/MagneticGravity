/**
 * Created by Marion on 28/01/2017.
 */

/**
 * @param label		// String - Where error message will appear
 * @param url		// String - The Controller to be called
 * @param action	// String - The method to be called
 * @param param		// Array  - parameters
 * @param callback	// Callable - Called if success
 */
function myAjax(label, url, action, param, callback) {

    $.ajax({
        type: "POST",
        url: url,
        data: {
            action: action,
            param: param
        },
        success: callback,
        error: function () {
            showMessage(label, 'Une erreur de connexion s\'est produite. Veuillez recharger la page et réessayer.' +
                'Si l\'erreur persiste, veuillez contacter l\'équipe technique de MagneticGravity.', true);
        }
    });
}

function closePopin() {
    $('.alert-dismissible').each(function() {
        $(this).addClass('none');
    });
}

function showPopin(label, headerMsg, bodyMsg, okMsg, closeMsg, buttonClass, callback) {
    $('#' + label + 'Label').text(headerMsg);
    $('#' + label + 'Body').html(bodyMsg);
    $('#' + label + 'Ok').text(okMsg).addClass('btn-' + buttonClass).unbind('click').on('click', callback);
    $('#' + label + 'Close').text(closeMsg);
    $('#' + label + 'Trigger').trigger('click');
}

function showMessage(label, message, isError) {

    var typeAdded ='success',
        typeRemoved = 'danger';
    if(isError === true) {
        typeAdded = 'danger';
        typeRemoved = 'success';
    }

    $('#' + label + 'Msg').html(message);
    $('#' + label).removeClass('alert-' + typeRemoved).addClass('alert-' + typeAdded).removeClass('none');

    setTimeout(closePopin, 3000);
}

function showTooltip(label) {
    $('#' + label).tooltip('show');
}

function scrollToTop() {

    var body = $("html, body");
    body.animate({
            scrollTop:0
        },
        'slow'
    );
}

function scrollToBottom() {

    var body = $("html, body");
    body.animate({
            scrollTop:2000
        },
        'slow'
    );
}