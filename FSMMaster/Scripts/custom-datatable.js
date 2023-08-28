$(document).ready(function ()
{
    $('#TableId').DataTable(
    {
        "columnDefs": [
            { "width": "5%", "targets": [0] },
            { "className": "text-center custom-middle-align", "targets": [0, 1, 2, 3, 4, 5, 6] },
        ],
        "language":
            {
                "processing": "<div class='overlay custom-loader-background'><i class='fa fa-cog fa-spin custom-loader-color'></i></div>"
            },
        "processing": true,
        "serverSide": true,
        "ajax":
            {
                "url": "Default.aspx/GetData",
                "contentType": "application/json",
                "type": "GET",
                "dataType": "JSON",
                "data": function (d)
                {
                    return d;
                },
                "dataSrc": function (json)
                {
                    json.draw = json.d.draw;
                    json.recordsTotal = json.d.recordsTotal;
                    json.recordsFiltered = json.d.recordsFiltered;
                    json.data = json.d.data;

                    var return_data = json;
                    return return_data.data;
                }
            },
        "columns": [
                    { "data": "Sr" },
                    { "data": "OrderTrackNumber" },
                    { "data": "Quantity" },
                    { "data": "ProductName" },
                    { "data": "SpecialOffer" },
                    { "data": "UnitPrice" },
                    { "data": "UnitPriceDiscount" }
        ]
    });
});
