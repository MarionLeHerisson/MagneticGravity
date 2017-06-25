/**
 * Created by Marion on 23/01/2017.
 */

var allOptions;
myAjax('', 'editeur', 'getAllOptions', [], function (ret) {
    allOptions = JSON.parse(ret);
});

// todo : save auto ajax

/*-----------------------*/
/*     S I D E B A R     */
/*-----------------------*/
/* From : http://bootsnipp.com/snippets/featured/fancy-sidebar-navigation */
$(document).ready(function () {
    var trigger = $('.hamburger'),
        // overlay = $('.overlay'),
        isClosed = false;

    trigger.click(function () {
        hamburger_cross();
    });

    function hamburger_cross() {

        if (isClosed === true) {
            // overlay.hide();
            trigger.removeClass('is-open');
            trigger.addClass('is-closed');
            isClosed = false;
        } else {
            // overlay.show();
            trigger.removeClass('is-closed');
            trigger.addClass('is-open');
            isClosed = true;
        }
    }

    $('[data-toggle="offcanvas"]').click(function () {
        $('#wrapper').toggleClass('toggled');
    });
});


function createNewBlock(assetName, imgSrc) {
    var uniqueId,
        newBlock = document.createElement("img");

    allOptions.forEach(function (option){
        if(option.ref === assetName) {
            $(newBlock).attr('data-' + option.opt_name, option.min_val);
        }
    });

    do {
        uniqueId = Math.floor((Math.random() * 99999999) + 10000000);
    } while($('#' + assetName + uniqueId) === undefined);

    $(newBlock).attr('src', 'Medias/Assets/' + imgSrc)
        .attr('draggable', 'true')
        .attr('width', 100)
        .attr('height', 50)
        .attr('id', assetName + uniqueId)
        .attr('class', assetName + ' asset')
        .attr('ondragstart', 'drag(event)')
        .attr('onmousedown', 'createSibling(event)');

    if(assetName === 'STRT' || assetName === 'MAGN' || assetName === 'BTUR' || assetName === 'SLOW') {
        $(newBlock).attr('onclick', 'createPopin("' + assetName + '","' + assetName + uniqueId + '")');
    }

    return newBlock;
}

/* DRAG N DROP */
// click on the block to drag
function drag(ev) {
    ev.dataTransfer.setData("text", ev.target.id);
}

// drag the block
function allowDrop(ev) {
    ev.preventDefault();
}

// drop the block
function drop(ev) {
    ev.preventDefault();
    var data = ev.dataTransfer.getData("text");     // dragged img id

    if(ev.target.id === 'trash') {
        manageTrash(data);
    } else {
        if(ev.target.closest('div').hasChildNodes()) {  // an element alrady exists in the div
            replaceBlock(ev, data);
        }
        else {  // the div is empty
            ev.target.appendChild(document.getElementById(data));
            $(document.getElementById(data)).addClass('dropped');
        }
    }
}

// create same block under dragged block
function createSibling(ev) {
    var assetName = ev.srcElement.id.substr(0, 4),
        imgSrc = assetName + '.png',
        newBlock,
        isNotInGrid = !$(ev.srcElement.closest('div')).hasClass('dropSquare'),
        isNotStartOrEnd = assetName !== 'STRT' && assetName !== 'BEND';

    if(isNotInGrid && isNotStartOrEnd) {

        newBlock = createNewBlock(assetName, imgSrc);
        ev.srcElement.closest('div').append(newBlock);
    }
}

// add options checked from the popin
function setOptions() {
    // close the popin
    $('#mainPopinClose').trigger('click');

    var blockId = $('#blockId').html(),
        block = $('#' + blockId),
        form = $('#optionsForm');

    form.children().each(function (key, div) {
        var input = $(div).find('input');
        var value = input.val();

        if(input.attr('name') !== undefined) {

            if(input.is('[type=checkbox]')) {
                if(input.is(':checked')) {
                    block.attr('data-' + input.attr('name'), 'true');
                } else {
                    block.attr('data-' + input.attr('name'), 'false');
                }
            }
            else if(input.is('[type=range]')) {
                block.attr('data-' + input.attr('name'), value);
            }
        }
    });
}

function createPopin(assetName, id) {
    // get existing options
    // var dataOptions = $('#' + id).data(),
    var dataOptions = document.getElementById(id).dataset,
        html = '<form id="optionsForm">',
        def_val = '';

    allOptions.forEach(function (option) {

        if(option.ref === assetName) {
            // CHECKBOX
            if(option.opt_type === 'checkbox') {

                // if the block has options
                if(option.opt_name in dataOptions && dataOptions[option.opt_name] == 'true') {
                    def_val = 'checked';
                } else {
                    def_val = '';
                }

                html += '<div class="checkbox"><label>' +
                    '<input type="checkbox" ' +
                    'name="' + option.opt_name + '" ' +
                    def_val + '>' +
                    option.opt_desc +
                    '</label></div>';
            }

            // RANGE
            if(option.opt_type === 'range') {
                if(option.opt_name in dataOptions) {
                    def_val = dataOptions[option.opt_name];
                }
                else {
                    def_val = option.min_val;
                }
                html += '<div class="checkbox"><label>' +
                    option.opt_desc +
                    '<input type="range" ' +
                    'name="' + option.opt_name + '" ' +
                    'step="' + option.opt_step + '" ' +
                    'max="' + option.max_val + '" ' +
                    'min="' + option.min_val + '" ' +
                    'value="' + def_val + '" ' +
                    'onchange="showRange(\'' + option.opt_name + '\')" ' +
                    'onmousemove="showRange(\'' + option.opt_name + '\')">' +
                    '<p>' + def_val + '</p>' +
                    '</label></div>';
            }
        }
    });

    html += '<div id="blockId" class="none">' + id + '</div></form>';

        var title = {
            STRT: 'Options pour le bloc de départ :',
            MAGN: 'Options pour le bloc magnétique :',
            BTUR: 'Options pour la tourelle :',
            SLOW: 'Options pour la pilule de ralenti :'
        };

    showPopin('mainPopin', title[assetName], html, 'Ok', 'Annuler', 'success', setOptions);
}

function showRange(name) {
    var range = $('[name=' + name + ']');
    range.parent().find('p').text(range.val());
}

function popinResetLevel() {
    showPopin('mainPopin', 'Voulez-vous vraiment supprimer votre niveau ?',
        'Si vous décidiez de supprimer ce niveau, tous vos efforts pour le créer seront perdus !', 'Je supprime ce niveau',
        'Je continue mes modification', 'danger', resetLevel);
}

function resetLevel() {
    $('.dropped').remove();
    $('#mainPopinClose').trigger('click');
}

function manageTrash(data) {
    var assetName = data.substr(0, 4),
        isStartOrEnd = assetName === 'STRT' || assetName === 'BEND',
        hiddenTrash = $('#hidden_trash');

    if(isStartOrEnd) {
        var newBlock = createNewBlock(assetName, assetName + '.png');
        $('#drop-' + assetName).append(newBlock);
    }

    hiddenTrash.get(0).appendChild(document.getElementById(data));
    hiddenTrash.html('');   // Empty the trash
}

function replaceBlock(ev, data) {
    var childType = $(ev.target.closest('div').firstChild).attr('id').substr(0,4),
        parentType = ev.dataTransfer.getData("text").substr(0,4);

    // if this element is start or end block
    if((childType === 'STRT' && parentType !== 'STRT') || (childType === 'BEND' && parentType !== 'BEND')) {
        var StrtEndBlock = createNewBlock(childType, childType + '.png');
        $('#drop-' + childType).append(StrtEndBlock );
    }
    $(document.getElementById(data)).addClass('dropped');
    ev.target.closest('div').replaceChild(document.getElementById(data), ev.srcElement);
}

function saveLevel() {
    // todo : erreur si deux blocs au dessus de la case départ
    var msg = '',
        data = {},
        label = 'submitTest',
        title = $('#lvlNewName').val();

    // Are start and end present ?
    if(!$('.STRT').closest('div').hasClass('dropSquare')) {
        msg += 'Vous devez utiliser le bloc de départ !<br>';
    }
    if(!$('.BEND').closest('div').hasClass('dropSquare')) {
        msg += 'Vous devez utiliser le bloc d\'arrivée !<br>';
    }
    if(title.trim() === '') {
        msg += 'Donnez donc un nom à votre niveau !<br>';
    }

    if(msg !== '') {
        showMessage(label, msg, true);
    }
    else {
        // Get level name
        data.title = title;
        var blocksTab = [];

        // Get grid content
        $('.dropSquare').each(function() {
            if($(this).html() !== '') {
                var element = {},
                    options = $(this).find('img')[0].dataset;

                element.x    = $(this).data('coord').split(',')[0];
                element.y    = $(this).data('coord').split(',')[1];
                element.type = $(this.firstChild).attr('id').substr(0,4);

                element = Object.assign(element, options);  // merge two objects

                blocksTab.push(element);
            }
        });
        data.blocks = blocksTab;

        myAjax(label, 'editeur', 'saveLevel', data, function(data){
            var dataObject = JSON.parse(data);	// transforms json return from php to js object

            if(dataObject.stat === 'ko') {
                showMessage(label, dataObject.msg, true);
            }
            else if(dataObject.stat === 'ok') {
                showMessage(label, dataObject.msg, false);
                $('#lvlCode').text("Code à entrer sur votre téléphone pour télécharger ce niveau : " +
                    dataObject.code);
            }
            else {
                showMessage(label, 'Une erreur s\'est produite. Veuillez contacter l\'équipe technique de MagneticGravity.', true);
            }
        });
    }
}

// create a dynamic grid
// todo: si heigth > width (mobile) -> msg activer rotat° + tourner écran
function createGrid() {
    var winWidth = window.innerWidth,
        winHeight = window.innerHeight,
        minWidth = 1000,
        divW = 100,
        divH = 50,
        grid = $('#grid'),
        text = '<table>';

    if(minWidth > winWidth) {
        divW = 50;
        divH = 25;
    }

    // for(var i = parseInt(winHeight/divH) + 1; i > 0; i--) {
    for(var i = 126; i > -1; i--) {
        text += '<tr>';
        // for(var j = 1; j < parseInt(winWidth/divW) - 1; j++) {
        for(var j = -1; j < 126; j++) {
            text += '<td>';
            text += '<div class="dropSquare" ondrop="drop(event)" ondragover="allowDrop(event)" data-coord="' + j + ',' + i + '"></div>';
            text += '</td>';
        }
        text += '</tr>';
    }

    text += '</table>';
    grid.html(text);
}

function fillSideBar() {
    var html = '',
        allBlocks;

    myAjax('', 'editeur', 'getAllBlocks', [], function (ret) {
        allBlocks = JSON.parse(ret);

        for(var i = 0; i < allBlocks.length; i++) {

            var id = 'drop-' + allBlocks[i]['ref'];
            html += '<li class="block-list">' +
                '<div class="block-name">' +
                '<span class="glyphicon glyphicon-question-sign block-info"></span>' +
                '<a href="#">' + allBlocks[i]['french_name'] + '</a>' +
                '</div>' +
                '<div id="' + id + '" class="dropBlocks" ondrop="drop(event)"' +
                ' ondragover="allowDrop(event)">' + createNewBlock(allBlocks[i]['ref'], allBlocks[i]['ref'] + '.png').outerHTML +
                '</div>' +
                '</li>';
        }

        $('.sidebar-trash').after(html);
    });
}

function binds() {
    $('#btn_test').bind("click", saveLevel);
}

function init() {
    createGrid();
    fillSideBar();
    binds();
}

$(document).ready(init);