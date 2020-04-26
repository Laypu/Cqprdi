/**
 * @license Copyright (c) 2003-2019, CKSource - Frederico Knabben. All rights reserved.
 * For licensing, see https://ckeditor.com/legal/ckeditor-oss-license
 */

CKEDITOR.editorConfig = function (config) {

	config.toolbarGroups = [
		{ name: 'clipboard', groups: ['clipboard', 'undo'] },
		{ name: 'links', groups: ['links'] },
		{ name: 'forms', groups: ['forms'] },
		{ name: 'editing', groups: ['find', 'selection', 'spellchecker', 'editing'] },
		{ name: 'insert', groups: ['insert'] },
		{ name: 'document', groups: ['mode', 'document', 'doctools'] },
		'/',
		{ name: 'basicstyles', groups: ['basicstyles', 'cleanup'] },
		{ name: 'paragraph', groups: ['list', 'indent', 'blocks', 'align', 'bidi', 'paragraph'] },
		{ name: 'styles', groups: ['styles'] },
		{ name: 'colors', groups: ['colors'] },
		{ name: 'tools', groups: ['tools'] },
		{ name: 'others', groups: ['others'] }
	];

	//config.removeButtons = 'Underline,Subscript,Superscript';
	config.allowedContent = true;
	// Set the most common block elements.
	config.format_tags = 'p;h1;h2;h3;pre';
	//config.baseFloatZIndex = 10060;

	config.defaultLanguage = 'zh';
	config.extraPlugins = 'colordialog';

	// Simplify the dialog windows.
	config.removeDialogTabs = 'image:advanced;link:advanced';

	config.removeButtons = 'Save,Templates,NewPage,Preview,Print,Form,Checkbox,Radio,TextField,Textarea,Select,Button,ImageButton,HiddenField,Flash,PageBreak,Iframe,Language,CreateDiv,Maximize,ShowBlocks,About';
};
