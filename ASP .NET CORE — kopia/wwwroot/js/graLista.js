var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#DT_load').DataTable({
        "ajax": {
            "url": "api/gra",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "tytul", "width": "20%" },
            { "data": "Firma", "width": "20%" },
            { "data": "Gatunek", "width": "20%" },
            { "data": "Ocena", "width": "20%" },
            {
                "data": "id",
                "render": function (data) {
                    return `
                        <div class="text-center">
                            <a href="/Listy/Edytuj_gre?id=${data}" class="btn btn-success text-white pointer" style="width: 50px;">Edytuj</a>
                            &nbsp;
                            <button asp-page-handler="Delete" asp-route-id="@item.Id" onclick="return confirm('Jesteś pewien, że chcesz usunąć element?')" class="btn btn-danger btn-sm" style="min-width: 50px;">Usuń</button>
                        </div>`;
                }, "width": "20%"
            }
        ],
        "language": {
            "emptyTable": "Brak gier."
        },
        "width": "100%"
    });
}