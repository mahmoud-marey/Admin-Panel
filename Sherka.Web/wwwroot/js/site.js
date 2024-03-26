var updatedRow;
var table;
var datatable;
var exportedCols = [];
var images_list = [];

//Alerts
function showSuccessMessage(message = "Saved Successfully!") {
	Swal.fire({
		text: message,
		icon: "success",
		buttonsStyling: false,
		confirmButtonText: "Ok",
		customClass: {
			confirmButton: "btn btn-primary"
		}
	});
}

function showErrorMessage(message = "Something went wrong, please try again") {
	Swal.fire({
		text: message,
		icon: "error",
		buttonsStyling: false,
		confirmButtonText: "Ok",
		customClass: {
			confirmButton: "btn btn-primary"
		}
	});
}
//tinyMce
function initImageList() {
	
	$('.imgname-src').each(function (i) {
		var str = $(this).text();
		const nameValue = str.split(',');		
		images_list.push({ title: nameValue[0], value: nameValue[1] });
	});
	
}


//Modals

function onModalBegin() {
	$('body :submit').attr('disabled', 'disabled').attr('data-kt-indicator', 'on');
}

function onModalSuccess(row) {
	showSuccessMessage();
	$('#Modal').modal('hide');

	var newRow = $(row);
	datatable.row.add(newRow).draw();

	if (updatedRow !== undefined) {
		datatable.row(updatedRow).remove().draw();
		updatedRow = undefined;
	}

	KTMenu.init();
	KTMenu.initGlobalHandlers();
}

function onModalComplete() {
	$('body :submit').removeAttr('disabled').removeAttr('data-kt-indicator');
}

//DataTables
if ($('th').length > 0) {
	var headers = $('th');
	$.each(headers, function (i) {
		var col = $(this);
		if (!col.hasClass('js-no-export')) {
			exportedCols.push(i);
		}
	});
}

// Class definition
var KTDatatables = function () {
	// Private functions
	var initDatatable = function () {

		// Init datatable --- more info on datatables: https://datatables.net/manual/
		datatable = $(table).DataTable({
			"info": false,
			'pageLength': 10,
		});
	}

	// Hook export buttons
	var exportButtons = () => {
		const documentTitle = $('.js-data-table').data('document-title');
		var buttons = new $.fn.dataTable.Buttons(table, {
			buttons: [
				{
					extend: 'copyHtml5',
					title: documentTitle,
					exportOptions: {
						columns: exportedCols
					}
				},
				{
					extend: 'excelHtml5',
					title: documentTitle,
					exportOptions: {
						columns: exportedCols
					}
				},
				{
					extend: 'csvHtml5',
					title: documentTitle,
					exportOptions: {
						columns: exportedCols
					}
				},
				{
					extend: 'pdfHtml5',
					title: documentTitle,
					exportOptions: {
						columns: exportedCols
					}
				}
			]
		}).container().appendTo($('#kt_datatable_example_buttons'));

		// Hook dropdown menu click event to datatable export buttons
		const exportButtons = document.querySelectorAll('#kt_datatable_example_export_menu [data-kt-export]');
		exportButtons.forEach(exportButton => {
			exportButton.addEventListener('click', e => {
				e.preventDefault();

				// Get clicked export value
				const exportValue = e.target.getAttribute('data-kt-export');
				const target = document.querySelector('.dt-buttons .buttons-' + exportValue);

				// Trigger click event on hidden datatable export buttons
				target.click();
			});
		});
	}

	// Search Datatable --- official docs reference: https://datatables.net/reference/api/search()
	var handleSearchDatatable = () => {
		const filterSearch = document.querySelector('[data-kt-filter="search"]');
		filterSearch.addEventListener('keyup', function (e) {
			datatable.search(e.target.value).draw();
		});
	}

	// Public methods
	return {
		init: function () {
			table = document.querySelector('.js-data-table');

			if (!table) {
				return;
			}

			initDatatable();
			exportButtons();
			handleSearchDatatable();
		}
	};
}();

//Handle bootstrap modal
$('body').delegate('.js-render-modal', 'click', function () {
	var modal = $('#Modal');
	var btn = $(this);

	if (btn.data('update') !== undefined) {
		updatedRow = btn.parents('tr');
		console.log(updatedRow);
	}

	modal.find('#ModalLabel').text(btn.data('title'));

	$.get({
		url: btn.data('url'),
		success: function (form) {
			modal.find('#ModalBody').html(form);
			$.validator.unobtrusive.parse(modal);
		},
		error: function () {
			showErrorMessage();
		}
	});

	modal.modal('show')
});

$(document).ready(function () { 
	//render body
	var body = $('.js-render-body')
	body.html(body.text());
	//SweetAlerts
	var message = $('#Message').text();
	if (message !== '') {
		showSuccessMessage(message);
	}

	//tinyMCE
	if ($('.js-tiny').length > 0) {
		var options = {
			selector: ".js-tiny",
			height: "382",
			plugins: 'image',
			menubar: 'insert',
			image_list: images_list
		
		};
		tinymce.init(options);
	   }
	//data tables
	KTUtil.onDOMContentLoaded(function () {
		KTDatatables.init();
	});

	$('body').delegate('.js-toggle-status', 'click', function () {
		var btn = $(this);
		var result = confirm('Are you sure you need to toggle this category?');
		if (result) {
			$.post({
				url: btn.data('url'),
				data: {
					'__RequestVerificationToken': $('input[name="__RequestVerificationToken"]').val()
				},
				success: function (lastUpdated) {
					var status = btn.parents('tr').find('.js-status');
					console.log(status.text())
					var newStatus = status.text().trim() === 'Deleted' ? 'Available' : 'Deleted';
					status.text(newStatus).toggleClass('badge-light-success badge-light-danger');
					btn.parents('tr').find('.js-last-updated').html(lastUpdated);

					showSuccessMessage("Toggled successfuly!");
				},
				error: function () {
					showErrorMessage();
				}
			});
		}
	})
});