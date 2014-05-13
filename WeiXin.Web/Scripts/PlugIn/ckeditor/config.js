/*
Copyright (c) 2003-2012, CKSource - Frederico Knabben. All rights reserved.
For licensing, see LICENSE.html or http://ckeditor.com/license
*/

//CKEDITOR.editorConfig = function( config )
//{
//	// Define changes to default configuration here. For example:
//	// config.language = 'fr';
//	// config.uiColor = '#AADC6E';
//};

CKEDITOR.on('instanceReady', function (ev) {
    with (ev.editor.dataProcessor.writer) {
        setRules("p", { indent: false, breakAfterOpen: false, breakBeforeClose: false });
        setRules("h1", { indent: false, breakAfterOpen: false, breakBeforeClose: false });
        setRules("h2", { indent: false, breakAfterOpen: false, breakBeforeClose: false });
        setRules("h3", { indent: false, breakAfterOpen: false, breakBeforeClose: false });
        setRules("h4", { indent: false, breakAfterOpen: false, breakBeforeClose: false });
        setRules("h5", { indent: false, breakAfterOpen: false, breakBeforeClose: false });
        setRules("div", { indent: false, breakAfterOpen: false, breakBeforeClose: false });
        setRules("table", { indent: false, breakAfterOpen: false, breakBeforeClose: false });
        setRules("tr", { indent: false, breakAfterOpen: false, breakBeforeClose: false });
        setRules("td", { indent: false, breakAfterOpen: false, breakBeforeClose: false });
        setRules("iframe", { indent: false, breakAfterOpen: false, breakBeforeClose: false });
        setRules("li", { indent: false, breakAfterOpen: false, breakBeforeClose: false });
        setRules("ul", { indent: false, breakAfterOpen: false, breakBeforeClose: false });
        setRules("ol", { indent: false, breakAfterOpen: false, breakBeforeClose: false });
    }
});
