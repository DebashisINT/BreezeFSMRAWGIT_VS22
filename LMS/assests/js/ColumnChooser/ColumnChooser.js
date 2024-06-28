function onShowChooserClick(s, e) {
    UpdateCustomizationWindowVisibility();
}
var visibleColumn = [];
function grid_CustomizationWindowCloseUp(s, e) {
    UpdateButtonText();

    for (var i = 0; i <= GridSalesSummaryDateWise.GetColumnsCount() ; i++) {
        if (GridSalesSummaryDateWise.GetColumn(i) != null) {
            if (GridSalesSummaryDateWise.GetColumn(i).visible == false) {
                console.log(GridSalesSummaryDateWise.GetColumn(i).fieldName);
                visibleColumn.push(GridSalesSummaryDateWise.GetColumn(i).fieldName);
            }
        }
    }

    addPageRetention(visibleColumn);
}
function UpdateCustomizationWindowVisibility() {
    if (GridSalesSummaryDateWise.IsCustomizationWindowVisible())
        GridSalesSummaryDateWise.HideCustomizationWindow();
    else
        GridSalesSummaryDateWise.ShowCustomizationWindow();
    UpdateButtonText();
}
function UpdateButtonText() {
    var text = GridSalesSummaryDateWise.IsCustomizationWindowVisible() ? "Hide" : "Show";
    text += " Column Chooser";
    btShowColumnChooser.SetText(text);
}

