/**
 * @license Copyright (c) 2003-2015, CKSource - Frederico Knabben. All rights reserved.
 * For licensing, see LICENSE.md or http://ckeditor.com/license
 */

CKEDITOR.editorConfig = function( config ) {
	// Define changes to default configuration here. For example:
	// config.language = 'fr';
    // config.uiColor = '#AADC6E';
    //config.toolbar = [
	//{ name: 'clipboard', groups: ['clipboard', 'undo'], items: ['Cut', 'Copy','Preview', 'Paste', 'PasteText', 'PasteFromWord', '-', 'Undo', 'Redo'] },
	//{ name: 'editing', groups: ['find', 'selection', 'spellchecker'], items: ['Scayt'] },
	//{ name: 'links', items: ['Link', 'Unlink', 'Anchor'] },
	//{ name: 'insert', items: ['Image', 'Table', 'HorizontalRule', 'SpecialChar'] },
	//{ name: 'tools', items: ['Maximize'] },
	//{ name: 'document', groups: ['mode', 'document', 'doctools'], items: ['Source'] },
	//{ name: 'others', items: ['-'] },
	//'/',
	//{ name: 'basicstyles', groups: ['basicstyles', 'cleanup'], items: ['Bold', 'Italic', 'Strike', '-', 'RemoveFormat'] },
	//{ name: 'paragraph', groups: ['list', 'indent', 'blocks', 'align', 'bidi'], items: ['NumberedList', 'BulletedList', '-', 'Outdent', 'Indent', '-', 'Blockquote'] },
	//{ name: 'styles', items: ['Styles', 'Format'] },	
    // { name: 'colors', items: ['TextColor', 'BGColor'] }
    //];
    config.skin = 'office2013';
    //config.removePlugins = 'elementspath';
    config.removePlugins = 'help, about, elementspath,save,styles,paragraph,tools,editing,forms,others,language,image,flash,smiley,iframe,document,div,bidi,selectall,showblocks,newpage,pagebreak,specialchar';
    config.removeButtons = "Styles,Format";
  
};
