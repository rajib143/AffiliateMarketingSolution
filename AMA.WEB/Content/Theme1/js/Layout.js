
$(document).ready(function () {
    //Load Category
    var BindCatagoryDiv = $("#BindCatagory");
    $.ajax({
        cache: false,
        type: "GET",
        url: "@(Url.Action("BindCategories","Common"))",
        //data: { "id": id },
        success: function (data) {

            BindCatagoryDiv.html('');
            BindCatagoryDiv.html(data);
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert('Failed to retrieve category.');
        }
    });

    //Load FlipKart ALL offers
    var DivAllOffers = $("#DivAllOffers");
    $.ajax({
        cache: false,
        type: "GET",
        url: "@(Url.Action("BindAllFipkartAllOffers","Home"))",
        //data: { "id": id },
        success: function (data) {

            DivAllOffers.html('');
            DivAllOffers.html(data);
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert('Failed to retrieve category.');
        }
    });

    //AutoComplete
    $("#ProductSearchBox").autocomplete({
        source: function (request, response) {
            $.ajax({
                url: "/api/Search",
                type: "GET",
                dataType: "json",
                data: { searchText: request.term },
                success: function (data) {
                    response($.map(data, function (item) {
                        return { label: item.title, val: item.productUrl };
                    }))

                }
            })
        },
        messages: {
            noResults: "", results: ""
        },
        select: function (e, i) {
            $("#SelectedProductUrl").val(i.item.val);
        },
        minLength: 1
    });



});


function OpenClildCategory(id) {
    var divid = "#categoryList_" + id;
    var BindSubCatagoryDiv = $(divid);

    $.ajax({
        cache: false,
        type: "GET",
        url: "@Url.Action("BindChildCategories","Common")",
        data: { "ParentId": id },
        success: function (data) {
            BindSubCatagoryDiv.html('');
            BindSubCatagoryDiv.html(data);
            $('.full-page-menu').addClass('active');
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert('Failed to retrieve category.');
        }
    });
}
function closePopup() {
    $('.full-page-menu').removeClass('active');
}

function OpenSearchOfferProduct() {
    var url = $('#SelectedProductUrl').val();
    window.open(url, "_blank");
};